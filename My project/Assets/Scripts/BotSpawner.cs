using UnityEngine;

public class BotSpawner : MonoBehaviour
{
    public GameObject botPrefab;
    public int botCount = 5;

    private Vector2 screenBounds;

    // Inicializa el BotSpawner con los parámetros necesarios
    public void InitializeSpawner(Vector2 screenBounds)
    {
        this.screenBounds = screenBounds;
        SpawnBots();
    }

    void SpawnBots()
    {
        for (int i = 0; i < botCount; i++)
        {
            GameObject bot = Instantiate(botPrefab, RandomPosition(), Quaternion.identity);
            BotController botController = bot.GetComponent<BotController>();
            if (botController != null)
            {
                botController.Initialize(screenBounds);
            }
        }
    }

    Vector2 RandomPosition()
    {
        float x = Random.Range(-screenBounds.x, screenBounds.x);
        float y = Random.Range(-screenBounds.y, screenBounds.y);
        return new Vector2(x, y);
    }
}
