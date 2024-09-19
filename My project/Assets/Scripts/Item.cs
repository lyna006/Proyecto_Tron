using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType
    {
        Fuel,
        TailGrowth,
        Bomb
    }

    public ItemType type;
    public BoxCollider2D gridArea;

    private void Start()
    {
        RandomizePosition();
    }

    private void RandomizePosition()
    {
        Bounds bounds = this.gridArea.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HandleItemPickup(other.gameObject);
        }
    }

    private void HandleItemPickup(GameObject player)
    {
        switch (type)
        {
            case ItemType.Fuel:
                Debug.Log("¡El jugador ha tomado combustible!");
                RandomizePosition(); 
                break;

            case ItemType.TailGrowth:
                Debug.Log("¡El jugador ha tomado un ítem de crecimiento de estela!");
                RandomizePosition();
                break;

            case ItemType.Bomb:
                Explode(player);
                break;
        }
    }

    private void Explode(GameObject player)
    {
        Debug.Log("¡Bomba! El jugador ha explotado.");
        Destroy(player);  
    }
}
