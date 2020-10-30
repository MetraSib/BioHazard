using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public GameObject gameMenu;

    public void ContinueButton() 
    {
        gameMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void SetGameMenu()
    {
        gameMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void WinRestartButton()
    {
        int pastLevel=--StaticSaveData.levelIndex;
        StaticSaveData.levelData = PlayerPrefs.GetString("Level " + pastLevel);
        SceneManager.LoadScene(2);
        Time.timeScale = 1;
    }

    public void LoseRestartButton()
    {
        SceneManager.LoadScene(2);
        Time.timeScale = 1;
    }

    public void LoadMenu() 
    {
        SceneManager.LoadScene(1);
    }
    
}
