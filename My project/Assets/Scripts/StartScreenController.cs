using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenController : MonoBehaviour
{
    // M�todos para cada bot�n
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
        // Cargar la escena de cr�ditos
        SceneManager.LoadScene("Credits");
    }

    public void OnBackButton()
    {
        // Salir del juego
        SceneManager.LoadScene("StartScreen");
    }
}
