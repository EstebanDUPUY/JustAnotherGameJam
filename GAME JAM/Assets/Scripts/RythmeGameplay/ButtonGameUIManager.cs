using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonGameUIManager : MonoBehaviour
{
    public void LoadMenu()
    {
        AudioManager.Instance.StopAllSongs();
        DataRythmeScore.Instance.ResetScore();
        SceneManager.LoadScene("0-MainMenu");
    }

    public void ReloadGame()
    {
        AudioManager.Instance.StopAllSongs();
        DataRythmeScore.Instance.ResetScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
