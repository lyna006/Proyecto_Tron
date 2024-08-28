using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonBack : MonoBehaviour
{
    public Button backButton; // Asigna el botón desde el Inspector

    public string startScreenSceneName = "StartScreen"; // Nombre de la escena de inicio

    void Start()
    {
        if (backButton != null)
        {
            backButton.onClick.AddListener(OnBackButtonClick);
        }
        else
        {
            Debug.LogError("Back Button is not assigned!");
        }
    }

    void OnBackButtonClick()
    {
        SceneManager.LoadScene(startScreenSceneName);
    }
}
