using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GetBeatmap : MonoBehaviour
{
    [SerializeField] GameObject buttonPrefab;
    private string beatMapFilePath;
    private List<string> listOfBeatMaps;
    void Start()
    {   
        beatMapFilePath = Application.persistentDataPath + "/BeatMaps/";
        listOfBeatMaps = new List<string>();
        RefreshBeatMapList();
        DisplayRefreshedBeatMaps();
    }

    void RefreshBeatMapList()
    {
        if (Directory.Exists(beatMapFilePath))
        {
            foreach (string filePath in Directory.GetFiles(beatMapFilePath))
            {   
                // Redundant check?
                if (File.Exists(filePath))
                {
                    if (!listOfBeatMaps.Contains(filePath))
                    {   
                        // Adds the name.json file to the list
                        listOfBeatMaps.Add(filePath.Split(beatMapFilePath)[1]);
                    }
                }
            }
        }
    }

    void DisplayRefreshedBeatMaps()
    {
        foreach (string s in listOfBeatMaps)
        {
            GameObject button = Instantiate(buttonPrefab);
            button.GetComponent<BeatMapButton>().setBeatMapJSON(s);  
            button.transform.SetParent(transform);
        }
    }
}
