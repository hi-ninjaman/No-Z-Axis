using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject levelSelect;
    public GameObject mainMenuSelect;


    public void endGame()
    {
        Application.Quit();
    }
    
    public void LevelSelect(string StartScene)
    {
        SceneManager.LoadScene(StartScene);
    }

    public void levelselectbutton()
    {
        mainMenuSelect.SetActive(false);
        levelSelect.SetActive(true);
    }
}
