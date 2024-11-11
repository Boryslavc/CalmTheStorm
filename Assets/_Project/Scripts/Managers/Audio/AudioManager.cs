using UnityEngine;


/// <summary>
/// Class is used to connect events and spesific sound play.
/// </summary>
public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private SoundData environmentSound;
    [SerializeField] private SoundData heartbeatSound;

    private float heartbeatSoundVolume = 0.4f;

    private float HeartBeatVolume
    {
        get
        {
            return heartbeatSoundVolume;
        }
        set
        {
            if (value < 0)
                heartbeatSoundVolume = 0;
            else if (value > 1)
                heartbeatSoundVolume = 1;
            else
                heartbeatSoundVolume = value;
        }
    }

    private void Start()
    {
        // initial start of sounds
        PlayEnvironmentSound(new RhythmGameFinishEvent(0));
        ChangeHeartbeatVolume(new RhythmGameFinishEvent(1)); 

        var eventBus = UtilitiesHolder.Instance.GetUtility<EventBus>();
        eventBus.Subscribe<RhythmGameStartEvent>(StopEnvironmentSound);
        eventBus.Subscribe<RhythmGameFinishEvent>(PlayEnvironmentSound);
    }

    private void OnDestroy()
    {
        var eventBus = UtilitiesHolder.Instance.GetUtility<EventBus>();
        eventBus.Unsubscribe<RhythmGameStartEvent>(StopEnvironmentSound);
        eventBus.Unsubscribe<RhythmGameFinishEvent>(PlayEnvironmentSound);
    }

    private void StopEnvironmentSound(RhythmGameStartEvent signal) =>
        AudioPlayer.Instance.StopPlaying(environmentSound);
    

    private void PlayEnvironmentSound(RhythmGameFinishEvent signal)
    {
        AudioPlayer.Instance.Play(environmentSound);
        
        // Depending on the outcome increase or decrease the volume to reflect the stress level.
        ChangeHeartbeatVolume(signal);
    }
    private void ChangeHeartbeatVolume(RhythmGameFinishEvent signal)
    {
        if (signal.stressChangeValue < 0) //meaning player won
            HeartBeatVolume -= 0.2f; 
        else if (signal.stressChangeValue > 0) // meaning player lost
            HeartBeatVolume += 0.2f;


        if (!AudioPlayer.Instance.IsPlaying(heartbeatSound))
            AudioPlayer.Instance.Play(heartbeatSound);
        else
            AudioPlayer.Instance.ChangeVolume(heartbeatSound, HeartBeatVolume);
    }
}
