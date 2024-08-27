using UnityEngine;
using System.Collections.Generic;

public class TrailController : MonoBehaviour
{
    public GameObject trailNodePrefab;
    public int maxNodes = 3;  // Tamaño inicial de la estela
    public float nodeSpawnInterval = 0.1f;

    private List<GameObject> trailNodes = new List<GameObject>();
    private float lastNodeSpawnTime;

    void Update()
    {
        if (Time.time - lastNodeSpawnTime > nodeSpawnInterval)
        {
            SpawnTrailNode();
            lastNodeSpawnTime = Time.time;
        }

        // Fade out and remove old nodes
        for (int i = trailNodes.Count - 1; i >= 0; i--)
        {
            GameObject node = trailNodes[i];
            SpriteRenderer sprite = node.GetComponent<SpriteRenderer>();
            Color color = sprite.color;
            color.a -= Time.deltaTime / 3; // Ajusta la velocidad de desvanecimiento aquí
            sprite.color = color;

            if (color.a <= 0)
            {
                Destroy(node);
                trailNodes.RemoveAt(i);
            }
        }
    }

    void SpawnTrailNode()
    {
        if (trailNodes.Count >= maxNodes)
        {
            Destroy(trailNodes[0]);
            trailNodes.RemoveAt(0);
        }

        GameObject newNode = Instantiate(trailNodePrefab, transform.position, Quaternion.identity);
        newNode.GetComponent<TrailNode>().owner = this.gameObject; // Asigna el jugador como propietario
        trailNodes.Add(newNode);
    }

    // Método para ajustar el tamaño de la estela en tiempo real
    public void SetMaxNodes(int newMaxNodes)
    {
        maxNodes = newMaxNodes;
        Debug.Log("Nuevo tamaño máximo de nodos: " + newMaxNodes);

        // Si ya hay más nodos de los permitidos, eliminarlos
        while (trailNodes.Count > maxNodes)
        {
            Destroy(trailNodes[0]);
            trailNodes.RemoveAt(0);
        }
    }
}
