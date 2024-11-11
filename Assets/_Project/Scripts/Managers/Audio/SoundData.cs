using UnityEngine;

[CreateAssetMenu(fileName = "Sound")]
public class SoundData : ScriptableObject
{
    public AudioClip clip;

    [Range(0,256)] public float Priority = 128;
    [Range(0,1)] public float Volume = 1;
    [Range(-3,3)] public float Pitch = 1;
    public bool Is3D = false;
    public bool IsLooping = false;
}
