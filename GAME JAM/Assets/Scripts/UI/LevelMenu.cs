using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
    /*
    private Button[] buttons;

    private void Awake()
    {
        buttons = GetComponentsInChildren<Button>();

        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
        for (int i = 0; i < unlockedLevel; i++)
        {
            buttons[i].interactable = true;
        }
    }
    */
    public void OpenLevel(int levelId)
    {
        string levelName;

        switch (levelId)
        {
            case 1:
                AudioManager.Instance.SetCurrentSong(DataSongs.SongName.Level1);
                break;
            case 2:
                AudioManager.Instance.SetCurrentSong(DataSongs.SongName.Level2);
                break;
            default:
                break;
        }


        levelName = "Level" + levelId;
        SceneManager.LoadScene("LevelRythm"); 
    }
}
