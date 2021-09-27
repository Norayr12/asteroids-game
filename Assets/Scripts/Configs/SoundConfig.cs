using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SFX", menuName = "Config/SFX")]
public class SoundConfig : ScriptableObject
{
    [Header("SFX clips")]
    public AudioClip ShootSound;
    public AudioClip ExplosionSound;
    public AudioClip ThrustSound;
}
