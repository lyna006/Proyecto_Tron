using UnityEngine;

public class BotSpawner : MonoBehaviour
{
    public GameObject botPrefab;  // Prefab del bot
    public float spawnInterval = 5f;  // Intervalo de tiempo para spawn
    public float xRange = 8f;  // Rango en el eje X
    public float yRange = 4f;  // Rango en el eje Y
    private int botCount = 0;  // Contador de bots generados
    public int maxBots = 2;  // Número máximo de bots en la escena

    void Start()
    {
        // Genera los bots al iniciar
        for (int i = 0; i < maxBots; i++)
        {
            SpawnBot();
        }

        // Inicia la generación periódica si se desea
        InvokeRepeating("SpawnBot", 2f, spawnInterval);
    }

    void SpawnBot()
    {
        if (botCount < maxBots)
        {
            Vector2 spawnPosition = new Vector2(Random.Range(-xRange, xRange), Random.Range(-yRange, yRange));
            Instantiate(botPrefab, spawnPosition, Quaternion.identity);
            botCount++;
        }
    }
}
