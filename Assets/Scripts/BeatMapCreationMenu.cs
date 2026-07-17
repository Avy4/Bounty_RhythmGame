using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.InputSystem;

public class BeatMapCreationMenu : MonoBehaviour
{   
    [Header("Beatmap Public Refs, Editor Use Only")]
    [SerializeField] TMP_InputField introTimeInput;
    [SerializeField] TMP_InputField beatSpeedInput;
    [SerializeField] TMP_InputField beatmapNameInput;
    [SerializeField] TMP_InputField songFileNameInput;
    [SerializeField] AudioSource audioSource;
    // ALWAYS USE LANE ONE. IT HAS 4 INDICES INSTEAD OF THREE.
    [SerializeField] LineRenderer exampleLane;
    private float spawnToPerfectMoveTime;
    [SerializeField] BeatMapCreation newBeatMap;
    public void StartButton()
    {   
        string musicFilePath = Application.persistentDataPath + "/Music/" + songFileNameInput.text;

        if (File.Exists(musicFilePath))
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

            StartCoroutine(Utilities.GetAudioClipFromMusicPath(musicFilePath, audioSource));
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
        Debug.Log("ajdasj");
        var lane = inputValue.Get<float>();

        if (audioSource.isPlaying)
        {
            float beatTiming = audioSource.time;
            
            if (beatTiming> spawnToPerfectMoveTime)
            {
                BeatObjectSettings temp = new BeatObjectSettings();
                float spawnTiming = beatTiming - spawnToPerfectMoveTime;

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
                        // Should Never Be Reached
                        break;
                }

                newBeatMap.AddBeat(temp);
            } 
        }
    }
}
