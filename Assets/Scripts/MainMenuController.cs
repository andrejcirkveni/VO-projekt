using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameMode
{
    public static bool IsSingleplayer;
}

public class MainMenuController : MonoBehaviour
{
    public void OnSingleplayerClicked()
    {
        GameMode.IsSingleplayer = true;
        SceneManager.LoadScene("GameScene");
    }

    public void OnMultiplayerClicked()
    {
        GameMode.IsSingleplayer = false;
        SceneManager.LoadScene("GameScene");
    }
}
