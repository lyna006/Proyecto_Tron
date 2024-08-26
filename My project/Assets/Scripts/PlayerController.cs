using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public Sprite spriteDerecha;
    public Sprite spriteIzquierda;
    public GameObject trailNodePrefab;  // Prefab del nodo de la estela
    public GameObject lengthItemPrefab; // Prefab del item que aumenta la longitud de la estela
    public float trailLengthIncrease = 3; // Cantidad en la que se aumenta la longitud de la estela al recoger un item

    private Vector2 direction = Vector2.zero;
    private SpriteRenderer spriteRenderer;
    private TrailController trailController;
    private Camera mainCamera;  // Referencia a la cámara principal

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        trailController = gameObject.AddComponent<TrailController>();
        trailController.trailNodePrefab = trailNodePrefab;
        trailController.maxNodes = 3; // Establece la longitud inicial de la estela

        mainCamera = Camera.main;  // Obtiene la referencia a la cámara principal
    }

    void Update()
    {
        HandleInput();
        if (direction != Vector2.zero)
        {
            Move();
            UpdateSprite();
        }
    }

    void HandleInput()
    {
        if (Input.GetKey(KeyCode.UpArrow)) direction = Vector2.up;
        else if (Input.GetKey(KeyCode.DownArrow)) direction = Vector2.down;
        else if (Input.GetKey(KeyCode.LeftArrow)) direction = Vector2.left;
        else if (Input.GetKey(KeyCode.RightArrow)) direction = Vector2.right;
        else direction = Vector2.zero;
    }

    void Move()
    {
        Vector3 newPosition = transform.position + (Vector3)direction * speed * Time.deltaTime;
        newPosition = ClampPositionToScreen(newPosition);  // Limita la posición dentro de la pantalla
        transform.position = newPosition;
    }

    void UpdateSprite()
    {
        if (direction == Vector2.left)
            spriteRenderer.sprite = spriteIzquierda;
        else if (direction == Vector2.right)
            spriteRenderer.sprite = spriteDerecha;
    }

    Vector3 ClampPositionToScreen(Vector3 position)
    {
        // Calcula los límites de la pantalla en coordenadas del mundo
        float screenWidth = Camera.main.orthographicSize * Camera.main.aspect;
        float screenHeight = Camera.main.orthographicSize;

        // Obtiene el tamaño del sprite en coordenadas del mundo
        float spriteWidth = spriteRenderer.bounds.size.x * 0.5f;
        float spriteHeight = spriteRenderer.bounds.size.y * 0.5f;

        // Limita la posición del jugador dentro de los límites de la pantalla
        position.x = Mathf.Clamp(position.x, -screenWidth + spriteWidth, screenWidth - spriteWidth);
        position.y = Mathf.Clamp(position.y, -screenHeight + spriteHeight, screenHeight - spriteHeight);

        return position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("LengthItem"))
        {
            // Aumenta la longitud de la estela
            trailController.SetMaxNodes(trailController.maxNodes + 5);  // Ajusta el valor según tus necesidades
            Destroy(other.gameObject);
        }
    }
}
