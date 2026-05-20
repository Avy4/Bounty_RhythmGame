using UnityEngine;

[System.Serializable]
public class BeatSetting
{
    [SerializeField] BeatManager.Lane spawnLane;
    [SerializeField] float timeBeforeSpawnNext = 0;
    [SerializeField] float beatSpeed = 0;

    public BeatManager.Lane GetLane()
    {
        return spawnLane;
    }

    public float GetSpeed()
    {
        return beatSpeed;
    }

    public float GetSpawnInterval()
    {
        return timeBeforeSpawnNext;
    }
}
