using UnityEngine;

[CreateAssetMenu(fileName = "BeatMap", menuName = "Scriptable Objects/BeatMap")]
public class BeatMap : ScriptableObject
{
    [Header("Beatmap Settings")]
    // Where you actually make the beatmap. Its simply an array of BeatSetting objects. 
    // They get spawned in the lane that you choose one after another. 
    [SerializeField] BeatSetting[] beatMap;

    [Tooltip("Time before the first note gets spawned")]
    [SerializeField] float timeBeforeStart= .5f; 
    [Tooltip("Time interval between the spawns of each note after timeBeforeStart has elapsed")]
    [SerializeField] float spawnIntervalDefault = .5f;
    [Tooltip("Speed of each beat")]
    [SerializeField] float beatSpeedDefault = 3f;

    public BeatSetting[] GetBeatMap()
    {
        return beatMap;
    }

    public float GetTimeBeforeStart()
    {
        return timeBeforeStart;
    }

    public float GetSpawnInterval()
    {
        return spawnIntervalDefault;
    }

    public float GetBeatSpeed()
    {
        return beatSpeedDefault;
    }
}
