using UnityEngine;

public class Manager : MonoBehaviour
{
    public PlayerController playerController;
    public BotSpawner botSpawner;
    public GridManager gridManager;
    public GameObject lengthItem;
    public GameObject gameOverCanvas;

    void Start()
    {
        InitializeGame();
    }

    void InitializeGame()
    {
        Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        gridManager.InitializeGrid(); // Inicializa la cuadrícula
        playerController.InitializePlayer(screenBounds); // Configura el jugador
        botSpawner.InitializeSpawner(screenBounds); // Configura los bots
        lengthItem.SetActive(true); // Activa el item para aumentar el tamaño
        gameOverCanvas.SetActive(false); // Asegura que el GameOverPanel esté desactivado al inicio
    }


    public void GameOver()
    {
        gameOverCanvas.SetActive(true); // Muestra el GameOverPanel cuando el jugador pierde
    }
}

