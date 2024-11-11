using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


/// <summary>
/// Class that actually controlls the audio.
/// </summary>
public class AudioPlayer : Singleton<AudioPlayer>
{
    private List<AudioSource> soundPlayers = new List<AudioSource>(7);
    private Dictionary<SoundData, AudioSource> activeSounds =
        new Dictionary<SoundData, AudioSource>();

    private GameObject musicParent;


    public bool IsPlaying(SoundData data) => activeSounds.ContainsKey(data);

    //Create a set of gameobjects with audiosoure components
    private void OnEnable()
    {
        musicParent = new GameObject("Music");
        musicParent.transform.parent = this.transform;

        for (int i = 0; i < 10; i++)
        {
            var child = new GameObject("Player");
            child.transform.parent = musicParent.transform;

            soundPlayers.Add(child.AddComponent<AudioSource>());
        }
    }

    public void Play(SoundData soundData)
    {
        var player = GetAudioPlayer();

        TuneAudioSource(player,soundData);

        // Add sound to dictionary to be able to stop it later. 
        // If sound is not looping it will stop by itself.
        if (soundData.IsLooping && !activeSounds.ContainsKey(soundData)) 
            activeSounds.Add(soundData, player);

        player.Play();
    }
    private AudioSource GetAudioPlayer()
    {
        AudioSource source = null;

        foreach (var player in soundPlayers)
        {
            if (!player.isPlaying)
            {
                source = player;
                return source;
            }
        }

        if(source == null)
        {
            var child = new GameObject("Music Player");
            child.transform.parent = musicParent.transform;

            source = child.AddComponent<AudioSource>();
            
            soundPlayers.Add(source);
        }
        return source;
    }
    private void TuneAudioSource(AudioSource source, SoundData data)
    {
        source.clip = data.clip;

        source.spatialBlend = data.Is3D ? 1 : 0;

        source.volume = data.Volume;

        source.loop = data.IsLooping;

        source.pitch = data.Pitch;
    }

    public void StopPlaying(SoundData soundData)
    {
        if (!activeSounds.ContainsKey(soundData))
            Debug.Log($"{soundData.name} is not currently playing");
        else
        {
            var source = activeSounds[soundData];
            source.DOFade(0, 0.5f).OnKill(() => Remove(soundData));
        }
    }

    public void ChangeVolume(SoundData soundData, float valueInPercent)
    {
        if(valueInPercent >= 0 &&  valueInPercent <= 1)
            soundData.Volume += valueInPercent;
        else
        {
            Debug.LogError("Value Out Of Bound Exception. Source: Audioplayer.");
            return;
        }

        // If is currently playing - increase volume in AudioSource
        if (activeSounds.ContainsKey(soundData))
            activeSounds[soundData].DOFade(soundData.Volume, 0.3f);
    }

    private void Remove(SoundData soundData)
    {
        activeSounds.Remove(soundData);
    }
}
