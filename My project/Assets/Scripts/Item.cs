using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType
    {
        Fuel,
        TailGrowth,
        Bomb,
        Shield,
        HyperSpeed
    }

    public ItemType type;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HandleItemPickup(other.gameObject); // Maneja la recolección del ítem
        }
    }

    public void HandleItemPickup(GameObject player)
    {
        switch (type)
        {
            case ItemType.Fuel:
                player.GetComponent<PlayerController>().CollectFuel(20);
                break;
            case ItemType.TailGrowth:
                player.GetComponent<PlayerController>().Grow();
                break;
            case ItemType.Shield:
                player.GetComponent<PlayerController>().ActivateShield();
                break;
            case ItemType.HyperSpeed:
                player.GetComponent<PlayerController>().ActivateHyperSpeed();
                break;
        }
        RandomizePosition(); // Reposicionar ítem después de ser recogido
    }

    private void Explode(GameObject player)
    {
        Debug.Log("¡Bomba! El jugador ha explotado.");
        Destroy(player); // Destruye el jugador (puede cambiarse según la lógica del juego)
    }
}