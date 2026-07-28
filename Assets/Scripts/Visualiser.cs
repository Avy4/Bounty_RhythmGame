using UnityEngine;

public class Visualiser : MonoBehaviour
{   
    [SerializeField] GameObject circlePrefab;
    public float songLength = default;
    public void Visualise(BeatObjectSettings obj)
    {
        Debug.Log(songLength);
        float spawnPosition_x = (obj.GetSpawnTime() / songLength * 18) - 8.5f;
        float spawnPosition_y = -3.5f + obj.GetLane();

        Vector3 spawnPosition = new Vector3(spawnPosition_x, spawnPosition_y, 0);
        Instantiate(circlePrefab, spawnPosition, Quaternion.identity);
    }

}
