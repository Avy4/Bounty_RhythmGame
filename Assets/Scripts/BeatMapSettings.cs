using UnityEngine;

[CreateAssetMenu(fileName = "BeatMapSettings", menuName = "Scriptable Objects/BeatMapSettings")]
public class BeatMapSettings : ScriptableObject
{
    [Header("Beatmap Settings")]
    public BeatObjectSettings[] beatMap {get; set;}
    public float introLength {get; set;}
    public float beatSpeed {get; set;}  
    public string beatMapName {get; set;}
    public string audioClipFilePath {get; set;}

    // For Use in EDITOR ONLY
    [SerializeField] AudioClip audioClip; 
}
