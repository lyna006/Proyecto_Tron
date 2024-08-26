using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject itemPrefab;  // Prefab del ítem que aumenta la longitud de la estela
    public float spawnInterval = 5f;  // Intervalo de tiempo para spawn
    public float xRange = 8f;  // Rango en el eje X
    public float yRange = 4f;  // Rango en el eje Y
    public int maxItemsOnScreen = 3;  // Máximo número de ítems en pantalla

    private float nextSpawnTime;

    private void Start()
    {
        if (itemPrefab == null)
        {
            Debug.LogError("Item prefab is not assigned.");
            return;
        }

        nextSpawnTime = Time.time + 2f;  // Comienza a spawnear después de 2 segundos
    }

    private void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            // Verifica si el número de ítems en la pantalla es menor que el límite
            if (CountItemsOnScreen() < maxItemsOnScreen)
            {
                SpawnItem();
            }
            nextSpawnTime = Time.time + spawnInterval;  // Establece el siguiente tiempo de spawn
        }
    }

    private void SpawnItem()
    {
        Vector2 spawnPosition = new Vector2(
            Random.Range(-xRange, xRange),
            Random.Range(-yRange, yRange)
        );

        Debug.Log($"Spawning item at: {spawnPosition}");

        Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
    }

    private int CountItemsOnScreen()
    {
        // Cuenta el número de ítems en la pantalla
        GameObject[] items = GameObject.FindGameObjectsWithTag("LengthItem"); // Reemplaza "ItemTag" con el tag correcto para tus ítems
        return items.Length;
    }
}
