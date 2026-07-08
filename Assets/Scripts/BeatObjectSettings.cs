using UnityEngine;

[System.Serializable]
public class BeatObjectSettings
{
    private enum Lane: int {
        ONE = 0, TWO = 1, THREE = 2, FOUR = 3
    }
    [SerializeField] Lane spawnLane;
    [SerializeField] float individualSpawnInterval = 0;
    [SerializeField] float individualBeatSpeed = 0;

    public int GetLane()
    {
        return (int)spawnLane;
    }

    public float GetSpeed()
    {
        return individualBeatSpeed;
    }

    public float GetSpawnInterval()
    {
        return individualSpawnInterval;
    }
}
