using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonGameUIManager : MonoBehaviour
{
    public void LoadMenu()
    {
        AudioManager.Instance.StopAllSongs();
        DataRythmeScore.Instance.ResetScore();
        SceneManager.LoadScene("0-MainMenu");
        AudioManager.Instance.StopSong();
        Time.timeScale = 1;
        AudioManager.Instance.PlayTitleSong(true);
    }

    public void ReloadGame()
    {
        AudioManager.Instance.StopAllSongs();
        DataRythmeScore.Instance.ResetScore();
        AudioManager.Instance.StopSong();
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
