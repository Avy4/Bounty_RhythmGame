using UnityEngine;

public class Visualiser : MonoBehaviour
{   
    [SerializeField] GameObject circlePrefab;
    [SerializeField] Sprite[] hitObjectSprites;
    [SerializeField] GameObject playhead;
    [SerializeField] AudioSource audioSource;
    public float songLength = default;
    private float introTime;
    private float offset = default;

    void Update()
    {   

        if (audioSource.isPlaying && songLength != default)
        {
            playhead.transform.position += new Vector3(Time.deltaTime / songLength * 17, 0, 0);
            offset = introTime / songLength * 17;
        }

    }

    public void SetOffset(float iT)
    {
        introTime = iT;
    }

    public void Visualise(BeatObjectSettings obj)
    {
        float spawnPosition_x = (obj.GetSpawnTime() / songLength * 17) - 8.5f + offset;
        float spawnPosition_y = -3.5f + obj.GetLane();

        Vector3 spawnPosition = new Vector3(spawnPosition_x, spawnPosition_y, 0);
        GameObject instance = Instantiate(circlePrefab, spawnPosition, Quaternion.identity);
        instance.GetComponent<SpriteRenderer>().sprite = hitObjectSprites[obj.GetLane()];
    }

}
