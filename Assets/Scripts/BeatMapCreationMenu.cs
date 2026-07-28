using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.InputSystem;
using UnityEditor.Search;

public class BeatMapCreationMenu : MonoBehaviour
{   
    [Header("Beatmap Public Refs, Editor Use Only")]
    [SerializeField] TMP_InputField introTimeInput;
    [SerializeField] TMP_InputField beatSpeedInput;
    [SerializeField] TMP_InputField beatmapNameInput;
    [SerializeField] TMP_InputField songFileNameInput;
    [SerializeField] AudioSource audioSource;
    [SerializeField] Visualiser visualiser;
    // ALWAYS USE LANE ONE. IT HAS 4 INDICES INSTEAD OF THREE.
    [SerializeField] LineRenderer exampleLane;
    private float spawnToPerfectMoveTime;
    private float offset = .15f;
    [SerializeField] BeatMapCreation newBeatMap;
    public void StartButton()
    {   
        string musicFilePath = "/Music/" + songFileNameInput.text;
        string applicationMusicFilePath = Application.persistentDataPath + musicFilePath;

        if (File.Exists(applicationMusicFilePath))
        {   
            
            float introTime = 1f;
            float beatSpeed = 3f;
            string beatmapName = "Untitled";

            if (float.TryParse(introTimeInput.text, out float resultIntro)) {
                introTime = resultIntro;
            }

            if (float.TryParse(beatSpeedInput.text, out float resultSpeed)) {
                beatSpeed = resultSpeed;
            }

            if (beatmapNameInput.text != "") {
                beatmapName = beatmapNameInput.text;
            }

            spawnToPerfectMoveTime = Utilities.GetBeatSpawnOffset(exampleLane, beatSpeed);
            newBeatMap = new BeatMapCreation(musicFilePath, introTime, beatSpeed, beatmapName);
            StartCoroutine(Utilities.GetAudioClipFromMusicPath(applicationMusicFilePath, audioSource, true));  
        }
    }

    public void PauseButton()
    {
        if (audioSource.isPlaying) {
            audioSource.Pause();
        }

        else
        {
            audioSource.UnPause();
        }
    }

    public void SaveButton()
    {   
        newBeatMap.SaveBeatMap();
        Utilities.BeatMapSettingsToJSON(newBeatMap.GetNewBeatMap());
    }

    void OnAddBeat(InputValue inputValue)
    {   
        var lane = inputValue.Get<float>();

        if (audioSource.isPlaying)
        {
            if (visualiser.songLength == default)
            {
                visualiser.songLength = Utilities.currentSongLength;    
            }

            float beatTiming = audioSource.time;
            
            if (beatTiming > spawnToPerfectMoveTime)
            {
                BeatObjectSettings temp = new BeatObjectSettings();
                float spawnTiming = beatTiming - spawnToPerfectMoveTime - offset;

                switch (lane)
                {
                    case 1:
                        temp.Init(BeatObjectSettings.Lane.ONE, spawnTiming);
                        break;
                    case 2:
                        temp.Init(BeatObjectSettings.Lane.TWO, spawnTiming);
                        break;
                    case 3:
                        temp.Init(BeatObjectSettings.Lane.THREE, spawnTiming);
                        break;
                    case 4:
                        temp.Init(BeatObjectSettings.Lane.FOUR, spawnTiming);
                        break;
                    default:
                        return;
                }

                newBeatMap.AddBeat(temp);
                visualiser.Visualise(temp);
            } 
        }
    }
}
