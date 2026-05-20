using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{

    private string levelScene = "LevelScene";
    private string mainMenu = "MainMenu";  
    public void StartButton()
    {
        SceneManager.LoadSceneAsync(levelScene);
    }

    void StoryButton()
    {
        
    }

    void SettingsButton()
    {
        
    }

    public void MainMenuButton()
    {
        SceneManager.LoadSceneAsync(mainMenu);
    }
}
