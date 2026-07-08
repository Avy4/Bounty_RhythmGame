using System.Collections.Generic;
using UnityEngine;

public class BeatMapManager : MonoBehaviour
{
    // Initialize this with the lanes 1-4 in order.
    [Header("Initialization")]
    [SerializeField] LineRenderer[] lanes;
    [SerializeField] GameObject beatPrefab;
    [SerializeField] BeatMapSettings beatMapSettings;

    // Audio Related Variables    
    private AudioClip songToPlay;
    private AudioSource audioPlayer;

    // The Actual BeatMap and the Queue
    private BeatObject[] beatMap;
    private Queue<BeatObject> beatMapQueue;

    // Timing Related BeatMap Settings
    private float introLength;
    private float defaultSpawnInterval;
    private float defaultBeatSpeed;
    private float spawnInterval;
    // ------------------------------
    private bool hasStarted = false;
    private bool hasBeats = true;

    void Start()
    {   
        // Checks if you have a BeatMap Settings object
        if (beatMapSettings)
        {   
            // Init the private vars from BeatMap Settings
            beatMap = beatMapSettings.GetBeatMap();
            introLength = beatMapSettings.GetIntroLength();
            defaultSpawnInterval = beatMapSettings.GetSpawnInterval();
            defaultBeatSpeed = beatMapSettings.GetBeatSpeed();

            // Just a simple check to ensure that the beatMap actually contains BeatObjects
            if (beatMap.Length > 0)
            {
                // Conv the array to a queue.
                beatMapQueue = new Queue<BeatObject>(beatMap); 
            }
        }

        // Init the AudioSource to play the song
        audioPlayer = GetComponent<AudioSource>();
    }

    void Update()
    {   
        if (!hasStarted)
        {
            introLength -= Time.deltaTime;
            if (introLength <= 0)
            {
                hasStarted = true;
                audioPlayer.PlayOneShot(songToPlay);
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
        // Check to make sure that the BeatMap has more objects to hit
        if (!(beatMapQueue.Count == 0))
        {   
            // Get the current BeatObject
            BeatObject currentBeatSetting = beatMapQueue.Dequeue();
            
            // Get the LineRenderer (i.e the lane that the beat gets spawned on)
            int currentBeatLaneIdx = currentBeatSetting.GetLane();
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
            newBeat.GetComponent<BeatObjectManager>().Initialize(currentLine, currentBeatSpeed);
        }

        // If the beatmap is finished then we set hasBeats to false to stop spawning anything. 
        else
        {
            hasBeats = false;
        }
    }
}
