using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverActions : MonoBehaviour
{
    public GameObject gameOverUI; // Asigna tu panel de Game Over en el Inspector

    void Start()
    {
        // Asegúrate de que el panel de Game Over esté desactivado al iniciar el juego
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }
    }

    public void ShowGameOver()
    {
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
            Time.timeScale = 0f; // Pausa el juego
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Reanuda el tiempo
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
