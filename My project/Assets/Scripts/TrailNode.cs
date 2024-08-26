using UnityEngine;

public class TrailNode : MonoBehaviour
{
    public TrailNode next;  // Referencia al siguiente nodo en la estela

    void Update()
    {
        if (next != null && next.gameObject != null)
        {
            // Actualiza la posición del nodo para seguir al nodo siguiente
            transform.position = next.transform.position;
        }
    }
}
