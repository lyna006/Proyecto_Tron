using UnityEngine;

public class BotController : MonoBehaviour
{
    public float speed = 3f; // Velocidad del bot
    public float changeDirectionInterval = 2f; // Intervalo para cambiar de direcci�n
    public Sprite leftSprite; // Sprite cuando se mueve a la izquierda
    public Sprite rightSprite; // Sprite cuando se mueve a la derecha

    private Vector2 direction; // Direcci�n del movimiento
    private Rigidbody2D rb; // Referencia al Rigidbody2D del bot
    private float nextDirectionChangeTime; // Tiempo para el pr�ximo cambio de direcci�n
    private SpriteRenderer spriteRenderer; // Referencia al SpriteRenderer del bot

    private Vector2 screenBounds; // L�mites de la pantalla
    private float botWidth;
    private float botHeight;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Obtener referencia al SpriteRenderer
        SetRandomDirection(); // Inicializa con una direcci�n aleatoria
        nextDirectionChangeTime = Time.time + changeDirectionInterval; // Establece el pr�ximo cambio de direcci�n

        // Calcular los l�mites de la pantalla
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        botWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x;
        botHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y;
    }

    void Update()
    {
        // Movimiento del bot
        rb.velocity = direction * speed;

        // Cambiar direcci�n si ha pasado el intervalo
        if (Time.time >= nextDirectionChangeTime)
        {
            SetRandomDirection();
            nextDirectionChangeTime = Time.time + changeDirectionInterval;
        }

        // Detectar colisiones con obst�culos
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1f, LayerMask.GetMask("Walls", "Trails"));
        if (hit.collider != null)
        {
            SetRandomDirection();
        }

        // Mantener al bot dentro de los l�mites de la pantalla
        KeepBotInBounds();

        // Actualizar el sprite seg�n la direcci�n
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
