using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel; // Panel de "Game Over"
    public PlayerController player; // Referencia al PlayerController

    private void Start()
    {
        // Asegurarse de que el panel de Game Over esté desactivado al inicio
        gameOverPanel.SetActive(false);
    }

    // Método para mostrar el panel de Game Over
    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true); // Mostrar el panel de Game Over
        Time.timeScale = 0f; // Pausar el juego
    }

    // Método para reiniciar el juego
    public void Retry()
    {
        Time.timeScale = 1f; // Reanudar el tiempo
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Recargar la escena actual
    }

    // Método para salir del juego
    public void ExitGame()
    {
        Application.Quit(); // Salir del juego
    }
}
