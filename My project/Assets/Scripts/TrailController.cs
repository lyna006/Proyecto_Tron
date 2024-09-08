using UnityEngine;
using System.Collections.Generic;

public class TrailController : MonoBehaviour
{
    public GameObject trailNodePrefab;
    public int maxNodes = 3;  // Tamaño inicial de la estela
    public float nodeSpawnInterval = 0.1f;

    private LinkedList<GameObject> trailNodes = new LinkedList<GameObject>();
    private float lastNodeSpawnTime;

    void Update()
    {
        if (Time.time - lastNodeSpawnTime > nodeSpawnInterval)
        {
            SpawnTrailNode();
            lastNodeSpawnTime = Time.time;
        }

        // Fade out and remove old nodes
        var node = trailNodes.Last;
        while (node != null)
        {
            var current = node;
            node = node.Previous;

            GameObject gameObject = current.Value;

            if (gameObject == null)
            {
                trailNodes.Remove(current);
                continue;
            }

            SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();

            if (sprite == null)
            {
                trailNodes.Remove(current);
                Destroy(gameObject);
                continue;
            }

            Color color = sprite.color;
            color.a -= Time.deltaTime / 2; // Ajusta la velocidad de desvanecimiento aquí
            sprite.color = color;

            if (color.a <= 0)
            {
                Destroy(gameObject);
                trailNodes.Remove(current);
            }
        }
    }

    void SpawnTrailNode()
    {
        if (trailNodes.Count >= maxNodes)
        {
            Destroy(trailNodes.First.Value);
            trailNodes.RemoveFirst();
        }

        GameObject newNode = Instantiate(trailNodePrefab, transform.position, Quaternion.identity);
        newNode.GetComponent<TrailNode>().owner = this.gameObject; // Asigna el jugador como propietario
        trailNodes.AddLast(newNode);
    }

    // Método para ajustar el tamaño de la estela en tiempo real
    public void SetMaxNodes(int newMaxNodes)
    {
        maxNodes = newMaxNodes;
        Debug.Log("Nuevo tamaño máximo de nodos: " + newMaxNodes);

        // Si ya hay más nodos de los permitidos, eliminarlos
        while (trailNodes.Count > maxNodes)
        {
            Destroy(trailNodes.First.Value);
            trailNodes.RemoveFirst();
        }
    }
}
