using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Menu
{
    /// <summary>
    /// This class is used to manage the main menu buttons.
    /// </summary>
    public class MainMenuButtons : MonoBehaviour
    {
        public void OnStartButtonClicked()
        {
            Debug.Log("Start button clicked. Game starting...");
            SceneManager.LoadSceneAsync("Game");
        }

        public void OnExitButtonClicked()
        {
            Debug.Log("Exit button clicked. Exiting game...");
            Application.Quit();
        }
    }
}
