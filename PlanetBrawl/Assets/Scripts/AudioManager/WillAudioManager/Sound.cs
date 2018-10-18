using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range (0,1)]
    public float volume = 0.8f;
    [Range (0.1f,3)]
    public float pitch = 1;
    public bool loop;
    [Range(0, 1)]
    public float spatialBlend;
    public float maxDistance = 50;
    [HideInInspector]
    public AudioSource source;

}
