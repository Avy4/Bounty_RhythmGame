using System.Collections.Generic;
using UnityEngine;

public class BeatMapCreation
{
    private BeatMapSettings beatMapSettings;
    private List<BeatObjectSettings> beatMap = new List<BeatObjectSettings>();
    public BeatMapCreation(string sFP, float iT, float bS, string bMN)
    {
        beatMapSettings = ScriptableObject.CreateInstance<BeatMapSettings>();
        
        beatMapSettings.audioClipFilePath = sFP;
        beatMapSettings.introLength = iT;
        beatMapSettings.beatSpeed = bS;
        beatMapSettings.beatMapName = bMN;

    }
    public void AddBeat(BeatObjectSettings newBeatObject)
    {
        beatMap.Add(newBeatObject);
    }

    public void SaveBeatMap()
    {
        beatMapSettings.beatMap = beatMap.ToArray();
    }

    public BeatMapSettings GetNewBeatMap()
    {
        return beatMapSettings;
    }
}
