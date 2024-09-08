using UnityEngine;

public class BotController : MonoBehaviour
{
    public float speed = 3f; // Velocidad del bot
    public float changeDirectionInterval = 2f; // Intervalo para cambiar de dirección
    public Sprite leftSprite; // Sprite cuando se mueve a la izquierda
    public Sprite rightSprite; // Sprite cuando se mueve a la derecha

    private Vector2 direction; // Dirección del movimiento
    private Rigidbody2D rb; // Referencia al Rigidbody2D del bot
    private float nextDirectionChangeTime; // Tiempo para el próximo cambio de dirección
    private SpriteRenderer spriteRenderer; // Referencia al SpriteRenderer del bot

    private Vector2 screenBounds; // Límites de la pantalla
    private float botWidth;
    private float botHeight;

    // Inicializa el BotController con los parámetros necesarios
    public void Initialize(Vector2 screenBounds)
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Obtener referencia al SpriteRenderer

        if (rb == null || spriteRenderer == null)
        {
            Debug.LogError("Rigidbody2D or SpriteRenderer not found on BotController.");
            return;
        }

        this.screenBounds = screenBounds;
        SetRandomDirection(); // Inicializa con una dirección aleatoria
        nextDirectionChangeTime = Time.time + changeDirectionInterval; // Establece el próximo cambio de dirección

        // Calcular el tamaño del bot
        botWidth = spriteRenderer.bounds.extents.x;
        botHeight = spriteRenderer.bounds.extents.y;
    }

    void Update()
    {
        if (rb == null || spriteRenderer == null)
        {
            return; // Evita la actualización si los componentes no están correctamente asignados
        }

        // Movimiento del bot
        rb.velocity = direction * speed;

        // Cambiar dirección si ha pasado el intervalo
        if (Time.time >= nextDirectionChangeTime)
        {
            SetRandomDirection();
            nextDirectionChangeTime = Time.time + changeDirectionInterval;
        }

        // Detectar colisiones con obstáculos
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1f, LayerMask.GetMask("Walls", "Trails"));
        if (hit.collider != null)
        {
            SetRandomDirection();
        }

        // Mantener al bot dentro de los límites de la pantalla
        KeepBotInBounds();

        // Actualizar el sprite según la dirección
        UpdateSprite();
    }

    void SetRandomDirection()
    {
        // Direcciones posibles: derecha, arriba, izquierda, abajo
        Vector2[] directions = { Vector2.right, Vector2.up, Vector2.left, Vector2.down };
        direction = directions[Random.Range(0, directions.Length)];
    }

    void KeepBotInBounds()
    {
        Vector3 pos = transform.position;

        if (pos.x > screenBounds.x - botWidth)
        {
            pos.x = screenBounds.x - botWidth;
            SetRandomDirection();
        }

        if (pos.x < -screenBounds.x + botWidth)
        {
            pos.x = -screenBounds.x + botWidth;
            SetRandomDirection();
        }

        if (pos.y > screenBounds.y - botHeight)
        {
            pos.y = screenBounds.y - botHeight;
            SetRandomDirection();
        }

        if (pos.y < -screenBounds.y + botHeight)
        {
            pos.y = -screenBounds.y + botHeight;
            SetRandomDirection();
        }

        transform.position = pos;
    }

    void UpdateSprite()
    {
        if (direction == Vector2.left)
        {
            spriteRenderer.sprite = leftSprite;
        }
        else if (direction == Vector2.right)
        {
            spriteRenderer.sprite = rightSprite;
        }
    }
}
