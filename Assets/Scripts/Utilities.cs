using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public static class Utilities
{   
    public static string beatMapsFilePath = Application.persistentDataPath + "/BeatMaps/";
    public static List<AudioClip> audioClips = new List<AudioClip>();
    public static BeatMapSettings JSONToBeatMapSettings(string filepath)
    {   
        string filePath = beatMapsFilePath + filepath + ".json";

        if (File.Exists(filePath))
        {
            string JSONData = File.ReadAllText(filePath);
            BeatMapSettings BMPS = ScriptableObject.CreateInstance<BeatMapSettings>();
            JsonUtility.FromJsonOverwrite(JSONData, BMPS);
            return BMPS;
        }

        return null;
    }

    public static void BeatMapSettingsToJSON(BeatMapSettings settings)
    {
        string filePath = Path.Combine(beatMapsFilePath + settings.beatMapName + ".json"); 
        string JSONData = JsonUtility.ToJson(settings);
        File.WriteAllText(filePath, JSONData);
    }

    public static IEnumerator GetAudioClipFromMusicPath(string path, AudioSource source)
    {
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(path, AudioType.MPEG))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {   
                Debug.Log("error has occured");
            }

            else
            {  
                AudioClip newSong = DownloadHandlerAudioClip.GetContent(www);

                if (!audioClips.Contains(newSong))
                {
                    audioClips.Add(newSong);
                }

                source.clip = newSong;
                source.Play();
            }
        }
    }

    public static float GetBeatSpawnOffset(LineRenderer lane, float beatSpeed) {
        return GetLineLength(lane) / beatSpeed;
    }

    private static float GetLineLength(LineRenderer lane)
    {   
        float totalLength = 0;

        Vector3[] linePoints = new Vector3[lane.positionCount];
        lane.GetPositions(linePoints);

        // We want to get the point where the beat is crossing through the perfect area.
        for (int i = 0; i < linePoints.Length - 2; i++)
        {
            totalLength += Vector3.Distance(linePoints[i], linePoints[i + 1]);
        }

        return totalLength;
    }
}
