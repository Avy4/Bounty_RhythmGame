using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{

    private string beatMapSelection = "BeatMapSelection";
    private string beatMapCreation = "BeatmapCreator";
    private string mainMenu = "MainMenu";  
    public void StartButton()
    {
        SceneManager.LoadSceneAsync(beatMapSelection);
    }

    public void BeatMapButton()
    {
        SceneManager.LoadSceneAsync(beatMapCreation);
    }

    public void MainMenuButton()
    {
        SceneManager.LoadSceneAsync(mainMenu);
    }

    public void Reset() {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
}
