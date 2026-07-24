using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class BeatMapManager : MonoBehaviour
{
    // Initialize this with the lanes 1-4 in order.
    [Header("Initialization")]
    [SerializeField] LineRenderer[] lanes;
    [SerializeField] GameObject beatPrefab;
    [SerializeField] AudioSource audioPlayer;

    // Variables
    [SerializeField] BeatMapSettings beatMapSettings;
    [SerializeField] string beatMapName;
    
    // The Actual BeatMap and the Queue
    private BeatObjectSettings[] beatMap;
    private Queue<BeatObjectSettings> beatMapQueue;

    // Timing Related BeatMap Settings
    private float introLength;
    private float beatSpeed;
    // ------------------------------
    private bool hasStarted = false;
    private bool hasBeats = true;

    private float timeElapsed = 0.0f;
    private float beatSpawnTime;

    void Start()
    {   
        string beatSettingsPath = Utilities.beatMapsFilePath + Utilities.currentBeatmap;
        // string beatSettingsPath = Utilities.beatMapsFilePath + beatMapName;
        beatMapSettings = ScriptableObject.CreateInstance<BeatMapSettings>();

        if (File.Exists(beatSettingsPath))
        {   
            beatMapSettings = Utilities.JSONToBeatMapSettings(beatSettingsPath);
            Initialize();
        }
    }

    void Initialize()
    {   
        beatMap = beatMapSettings.beatMap;
        introLength = beatMapSettings.introLength;
        beatSpeed = beatMapSettings.beatSpeed;
        beatMapQueue = new Queue<BeatObjectSettings>(beatMap);

        var await = StartCoroutine(Utilities.GetAudioClipFromMusicPath(Application.persistentDataPath + beatMapSettings.audioClipFilePath, audioPlayer, false));
    }

    void Update()
    {   
        if (!hasStarted)
        {
            introLength -= Time.deltaTime;
            if (introLength <= 0)
            {   
                hasStarted = true;
                audioPlayer.Play();
                beatSpawnTime = beatMapQueue.Peek().GetSpawnTime();
            } 
        }
        
        else if (hasBeats)
        {   
            timeElapsed += Time.deltaTime;
            if (timeElapsed >= beatSpawnTime)
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
            BeatObjectSettings currentBeatSetting = beatMapQueue.Dequeue();
            
            // Get the LineRenderer (i.e the lane that the beat gets spawned on)
            int currentBeatLaneIdx = currentBeatSetting.GetLane();
            LineRenderer currentLine = lanes[currentBeatLaneIdx];

            // Create a new beat and initialize it
            GameObject newBeat = Instantiate(beatPrefab);
            newBeat.GetComponent<BeatObjectManager>().Initialize(currentLine, beatSpeed);

            if (beatMapQueue.TryPeek(out BeatObjectSettings nextBeatSetting)) {
                beatSpawnTime = nextBeatSetting.GetSpawnTime();
            }
        }

        // If the beatmap is finished then we set hasBeats to false to stop spawning anything. 
        else
        {
            hasBeats = false;
        }
    }
}
