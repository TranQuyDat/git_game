using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveData", menuName = "section3/wavedata")]
public class wavedata : ScriptableObject
{
    [Range(1,10)] public int totalgroup;
    [Range(1, 10)] public int mintotalenemi;
    [Range(1, 10)] public int maxtotalenemi;
    [Range(1, 10)] public int speedmultiplier;
}
