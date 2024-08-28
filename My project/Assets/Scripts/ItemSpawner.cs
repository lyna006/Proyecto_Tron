using UnityEngine;
using System.Collections.Generic;

public class ItemSpawner : MonoBehaviour
{
    public GameObject lengthItemPrefab; // Prefab del ítem a generar
    public float spawnInterval = 20f; // Intervalo entre cada generación de ítem
    public float spawnRangeX = 10f; // Rango en el eje X para la posición de spawn
    public float spawnRangeY = 5f;  // Rango en el eje Y para la posición de spawn
    public int maxItems = 2; // Número máximo de ítems en pantalla

    private float nextSpawnTime = 0f; // Tiempo para la próxima generación
    private List<GameObject> spawnedItems = new List<GameObject>(); // Lista para controlar los ítems generados

    void Update()
    {
        // Elimina los ítems que han sido destruidos
        spawnedItems.RemoveAll(item => item == null);

        // Solo genera un nuevo ítem si hay menos de maxItems en pantalla
        if (Time.time >= nextSpawnTime && spawnedItems.Count < maxItems)
        {
            SpawnItem();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void SpawnItem()
    {
        // Verifica si el prefab del ítem está asignado
        if (lengthItemPrefab == null)
        {
            Debug.LogError("lengthItemPrefab is not assigned! Please assign it in the Inspector.");
            return;
        }

        // Calcula una posición aleatoria dentro del rango especificado
        Vector2 spawnPosition = new Vector2(
            Random.Range(-spawnRangeX, spawnRangeX),
            Random.Range(-spawnRangeY, spawnRangeY)
        );

        // Genera una instancia del prefab en la posición calculada
        GameObject newItem = Instantiate(lengthItemPrefab, spawnPosition, Quaternion.identity);
        spawnedItems.Add(newItem); // Añade el nuevo ítem a la lista de ítems generados
    }
}
