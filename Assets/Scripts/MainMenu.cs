using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button[] levelButtons;
    private void Start()
    {
        LoadLevelData();
    }

    private void LoadLevelData() 
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            int level = i + 2;
            if (PlayerPrefs.HasKey("Level " + level)) levelButtons[i].interactable = true;
            else levelButtons[i].interactable = false;
        }
    }

    //public void ResetLevelData()
    //{
    //    PlayerPrefs.DeleteAll();
    //}

    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(2);
        Time.timeScale = 1;
        StaticSaveData.levelIndex = level;
    }

    public void LevelData(string data)
    {
        StaticSaveData.levelData = data;
    }

    public void VolumeOn()
    {
        AudioListener.volume = 1;
    }
    public void VolumeOff()
    {
        AudioListener.volume = 0;
    }
    public void QuitGame() 
    {
        Application.Quit();
    }
}
