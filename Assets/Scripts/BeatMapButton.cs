using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class BeatMapButton : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI buttonText;
    private string beatMapJSON;

    public void setBeatMapJSON(string name)
    {
        beatMapJSON = name;
        buttonText.text = name;
    }

    public void OnClick()
    {
        Utilities.currentBeatmap = beatMapJSON;
        SceneManager.LoadScene(2);
    }
}
