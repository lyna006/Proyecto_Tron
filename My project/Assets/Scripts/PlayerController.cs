using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f; // Velocidad del jugador
    private Rigidbody2D rb; // Referencia al Rigidbody2D del jugador
    private Vector2 screenBounds; // Límites de la pantalla

    // Inicializa el PlayerController con los parámetros necesarios
    public void InitializePlayer(Vector2 screenBounds)
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D not found on PlayerController.");
            return;
        }

        // Calcular los límites de la pantalla
        this.screenBounds = screenBounds;
    }

    void Update()
    {
        // Movimiento del jugador
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical) * speed;
        rb.velocity = movement;

        // Mantener al jugador dentro de los límites de la pantalla
        KeepPlayerInBounds();
    }

    void KeepPlayerInBounds()
    {
        Vector3 pos = transform.position;

        if (pos.x > screenBounds.x)
        {
            pos.x = screenBounds.x;
        }

        if (pos.x < -screenBounds.x)
        {
            pos.x = -screenBounds.x;
        }

        if (pos.y > screenBounds.y)
        {
            pos.y = screenBounds.y;
        }

        if (pos.y < -screenBounds.y)
        {
            pos.y = -screenBounds.y;
        }

        transform.position = pos;
    }
}
