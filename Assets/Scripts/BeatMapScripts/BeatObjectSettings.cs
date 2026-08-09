using UnityEngine;

[System.Serializable]
public class BeatObjectSettings
{
    public enum Lane: int {
        ONE = 0, TWO = 1, THREE = 2, FOUR = 3
    }
    [SerializeField] Lane spawnLane;
    [SerializeField] float spawnTime;
    
    public void Init(Lane lane, float time)
    {
        spawnLane = lane;
        spawnTime = time;
    }

    public int GetLane()
    {
        return (int)spawnLane;
    }

    public float GetSpawnTime()
    {
        return spawnTime;
    }
}
