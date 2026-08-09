using UnityEngine;

[CreateAssetMenu(fileName = "BeatMapSettings", menuName = "Scriptable Objects/BeatMapSettings")]
public class BeatMapSettings : ScriptableObject
{
    [Header("Beatmap Settings")]
    public BeatObjectSettings[] beatMap;
    public float introLength;
    public float beatSpeed;
    public string beatMapName;
    public string audioClipFilePath;

    // For Use in EDITOR ONLY
    [SerializeField] AudioClip audioClip; 

    public void PrintFields()
    {
        Debug.Log(string.Format(
            "Audio File Path: {0}, Intro Length: {1}, Beat Speed: {2}, BeatMap Name: {3}", 
            audioClipFilePath, introLength, beatSpeed, beatMapName
        ));
    }
}
