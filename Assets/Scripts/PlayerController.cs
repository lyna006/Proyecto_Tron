using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public Sprite spriteDerecha;
    public Sprite spriteIzquierda;
    public GameObject trailNodePrefab;  // Prefab del nodo de la estela

    private Vector2 direction = Vector2.zero;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Configura el TrailController en el jugador
        TrailController trailController = gameObject.AddComponent<TrailController>();
        trailController.trailNodePrefab = trailNodePrefab;
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
        transform.position = newPosition;
    }

    void UpdateSprite()
    {
        if (direction == Vector2.left)
            spriteRenderer.sprite = spriteIzquierda;
        else if (direction == Vector2.right)
            spriteRenderer.sprite = spriteDerecha;
    }
}
