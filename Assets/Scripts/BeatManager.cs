using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class BeatManager : MonoBehaviour
{
    public enum Lane: int {
        ONE = 0, TWO = 1, THREE = 2, FOUR = 3
    }

    [Header("Initialization")]
    // Initialize this with the lanes 1-4 in order.
    [SerializeField] LineRenderer[] lanes;
    [SerializeField] GameObject beatPrefab;
    [SerializeField] BeatMap beatMapContainer;
    [SerializeField] AudioClip song;
    private Queue<BeatSetting> beatMapQueue;

    // These mirror the variables in BeatMap
    // There are private references here so you can easily switch out beatmaps
    // Defaults
    private BeatSetting[] beatMap;
    private AudioSource audioPlayer; 
    private float timeBeforeStart;
    private float defaultSpawnInterval;
    private float defaultBeatSpeed;
    private float spawnInterval;
    private bool hasStarted = false;
    private bool hasBeats = true;

    void Start()
    {   
        if (beatMapContainer)
        {   
            // Init the private vars from beatMapContainer
            beatMap = beatMapContainer.GetBeatMap();
            timeBeforeStart = beatMapContainer.GetTimeBeforeStart();
            defaultSpawnInterval = beatMapContainer.GetSpawnInterval();
            defaultBeatSpeed = beatMapContainer.GetBeatSpeed();

            // Just a simple check to ensure that you created a beatmap.
            if (beatMap.Length > 0)
            {
                // Conv the array to a queue.
                beatMapQueue = new Queue<BeatSetting>(beatMap); 
            }
        }

        audioPlayer = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!hasStarted)
        {
            timeBeforeStart -= Time.deltaTime;
            if (timeBeforeStart <= 0)
            {
                hasStarted = true;
                audioPlayer.Play();
                SpawnBeat();
            } 
        }
        
        else if (hasBeats)
        {   
            spawnInterval -= Time.deltaTime;
            if (spawnInterval <= 0)
            {
                SpawnBeat();
            }
        }
    }

    void SpawnBeat()
    {   
        // Check to make sure the beatmap hasn't finished.
        if (!(beatMapQueue.Count == 0))
        {   
            // Get the BeatSetting object
            BeatSetting currentBeatSetting = beatMapQueue.Dequeue();
            
            // Get the LineRenderer (i.e the lane that the beat gets spawned on)
            int currentBeatLaneIdx = (int)currentBeatSetting.GetLane();
            LineRenderer currentLine = lanes[currentBeatLaneIdx];

            // Get the speed that the beat moves at
            float currentBeatSpeed = currentBeatSetting.GetSpeed();
            if (currentBeatSpeed == 0)
            {
                currentBeatSpeed = defaultBeatSpeed;
            }

            // Get the time interval before the next beat
            float currentSpawnInterval = currentBeatSetting.GetSpawnInterval();
            if (currentSpawnInterval == 0)
            {
                spawnInterval = defaultSpawnInterval;
            }
            else
            {
                spawnInterval = currentSpawnInterval;
            }
            
            // Create a new beat and initialize it
            GameObject newBeat = Instantiate(beatPrefab);
            newBeat.GetComponent<Beat>().Initialize(currentLine, currentBeatSpeed);
        }

        // If the beatmap is finished then we set hasBeats to false to stop spawning anything. 
        else
        {
            hasBeats = false;
            Debug.Log("No more beats to spawn!");
        }
    }
}
