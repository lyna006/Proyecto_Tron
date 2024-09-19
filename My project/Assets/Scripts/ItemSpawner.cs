using UnityEngine;
using System.Collections.Generic;

public class ItemSpawner : MonoBehaviour
{
    public GameObject lengthItemPrefab; // Prefab del �tem a generar
    public float spawnInterval = 20f; // Intervalo entre cada generaci�n de �tem
    public float spawnRangeX = 10f; // Rango en el eje X para la posici�n de spawn
    public float spawnRangeY = 5f;  // Rango en el eje Y para la posici�n de spawn
    public int maxItems = 2; // N�mero m�ximo de �tems en pantalla

    private float nextSpawnTime = 0f; // Tiempo para la pr�xima generaci�n
    private List<GameObject> spawnedItems = new List<GameObject>(); // Lista para controlar los �tems generados

    void Update()
    {
        // Elimina los �tems que han sido destruidos
        spawnedItems.RemoveAll(item => item == null);

        // Solo genera un nuevo �tem si hay menos de maxItems en pantalla
        if (Time.time >= nextSpawnTime && spawnedItems.Count < maxItems)
        {
            SpawnItem();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void SpawnItem()
    {
        // Verifica si el prefab del �tem est� asignado
        if (lengthItemPrefab == null)
        {
            Debug.LogError("lengthItemPrefab is not assigned! Please assign it in the Inspector.");
            return;
        }

        // Calcula una posici�n aleatoria dentro del rango especificado
        Vector2 spawnPosition = new Vector2(
            Random.Range(-spawnRangeX, spawnRangeX),
            Random.Range(-spawnRangeY, spawnRangeY)
        );

        // Genera una instancia del prefab en la posici�n calculada
        GameObject newItem = Instantiate(lengthItemPrefab, spawnPosition, Quaternion.identity);
        spawnedItems.Add(newItem); // A�ade el nuevo �tem a la lista de �tems generados
    }
}
