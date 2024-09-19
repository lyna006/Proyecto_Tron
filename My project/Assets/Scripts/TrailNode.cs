using UnityEngine;

public class TrailNode : MonoBehaviour
{
    public TrailNode next;  // Referencia al siguiente nodo en la estela
    public GameObject owner; // Referencia al objeto que posee la estela

    void Update()
    {
        if (next != null)
        {
            // Actualiza la posición del nodo para seguir al nodo siguiente
            transform.position = next.transform.position;
        }
    }
}
