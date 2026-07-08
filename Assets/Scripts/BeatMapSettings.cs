using UnityEngine;

[CreateAssetMenu(fileName = "BeatMapSettings", menuName = "Scriptable Objects/BeatMapSettings")]
public class BeatMapSettings : ScriptableObject
{
    [Header("Beatmap Settings")]
    // Where you actually make the beatmap. Its simply an array of BeatSetting objects. 
    // They get spawned in the lane that you choose one after another. 
    [SerializeField] BeatObject[] beatMap;

    [Tooltip("Time before the first note gets spawned")]
    [SerializeField] float introLength = 0f; 
    [Tooltip("Time interval between the spawns of each note after timeBeforeStart has elapsed")]
    [SerializeField] float defaultSpawnInterval = .5f;
    [Tooltip("Speed of each beat")]
    [SerializeField] float defaultBeatSpeed = 3f;
    [SerializeField] AudioClip song;

    public BeatObject[] GetBeatMap()
    {
        return beatMap;
    }

    public float GetIntroLength()
    {
        return introLength;
    }

    public float GetSpawnInterval()
    {
        return defaultSpawnInterval;
    }

    public float GetBeatSpeed()
    {
        return defaultBeatSpeed;
    }
}
