using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Button startButton;
    public Button instructionsButton;
    public Button creditsButton;

    public string startSceneName = "SampleScene";
    public string instructionsSceneName = "Instructions";
    public string creditsSceneName = "Credits";

    void Start()
    {
        // Verifica que los botones están asignados
        if (startButton != null)
            startButton.onClick.AddListener(OnStartButtonClick);

        if (instructionsButton != null)
            instructionsButton.onClick.AddListener(OnInstructionsButtonClick);

        if (creditsButton != null)
            creditsButton.onClick.AddListener(OnCreditsButtonClick);
    }

    void OnStartButtonClick()
    {
        SceneManager.LoadScene(startSceneName);
    }

    void OnInstructionsButtonClick()
    {
        SceneManager.LoadScene(instructionsSceneName);
    }

    void OnCreditsButtonClick()
    {
        SceneManager.LoadScene(creditsSceneName);
    }
}
