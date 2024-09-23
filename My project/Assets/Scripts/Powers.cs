using UnityEngine;

public class Power : MonoBehaviour
{
    public BoxCollider2D gridArea; // Asigna esto desde el inspector

    private void Start()
    {
        RandomizePosition();
    }

    public void RandomizePosition()
    {
        if (gridArea == null)
        {
            Debug.LogError("GridArea no asignada en el inspector para el objeto: " + gameObject.name);
            return;
        }

        Bounds bounds = gridArea.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
    }
}