using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("BasketballGame"); // Change this to your game scene's name
    }

    public void QuitGame()
    {
        Debug.Log("Game Quit!"); // This will show in the console when quitting
        Application.Quit(); // This only works in a built game, not in the editor
    }
}
