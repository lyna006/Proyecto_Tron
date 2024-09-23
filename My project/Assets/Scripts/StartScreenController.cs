using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenController : MonoBehaviour
{
    // Métodos para cada botón
    public void OnPlayButton()
    {
        // Cargar la escena del juego
        SceneManager.LoadScene("Game");
    }

    public void OnInstButton()
    {
        // Cargar la escena de instrucciones
        SceneManager.LoadScene("Instructions");
    }

    public void OnCredButton()
    {
        // Cargar la escena de créditos
        SceneManager.LoadScene("Credits");
    }

    public void OnBackButton()
    {
        // Salir del juego
        SceneManager.LoadScene("StartScreen");
    }
}
