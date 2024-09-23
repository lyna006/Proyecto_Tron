using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    protected Vector2 _direction = Vector2.right;
    protected List<Transform> _segments = new List<Transform>();
    public Transform segmentPrefab;
    public int initialSize = 3;

    private bool isShieldActive = false;
    private bool isHyperSpeedActive = false;
    private float hyperSpeedDuration = 5f;
    private float shieldDuration = 5f;
    private float hyperSpeedMultiplier = 2f;
    private Renderer playerRenderer;
    private GameObject shieldEffectInstance; // Referencia al efecto visual del escudo
    public GameObject shieldEffectPrefab; // Prefab del efecto visual del escudo
    public float fuel;
    public float fuelConsumptionRate = 5f;
    public Slider fuelSlider;
    private int stepsSinceLastFuelConsumption = 0;
    public float speed;
    private Vector3 lastPosition;

    // Referencia al GameOverManager para mostrar la pantalla de Game Over
    public GameOverManager gameOverManager;

    protected virtual void Start()
    {
        playerRenderer = GetComponent<Renderer>();
        ResetState();
        lastPosition = transform.position;
        fuel = 100;
        UpdateFuelUI();
    }

    protected virtual void Update()
    {
        HandleInput();

        if (isHyperSpeedActive)
        {
            hyperSpeedDuration -= Time.deltaTime;
            if (hyperSpeedDuration <= 0)
            {
                DeactivateHyperSpeed();
            }
        }

        if (isShieldActive)
        {
            shieldDuration -= Time.deltaTime;
            if (shieldDuration <= 0)
            {
                DeactivateShield();
            }
        }

        if (fuel <= 0)
        {
            // Mostrar Game Over si el jugador se queda sin combustible
            gameOverManager.ShowGameOver();
            Destroy(gameObject); // Destruir el jugador
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _direction = Vector2.right;
        }
    }

    protected virtual void FixedUpdate()
    {
        for (int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }

        float moveSpeed = 1.0f;
        if (isHyperSpeedActive)
        {
            moveSpeed *= hyperSpeedMultiplier;
        }

        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + _direction.x * moveSpeed,
            Mathf.Round(this.transform.position.y) + _direction.y * moveSpeed,
            0.0f
        );

        ConsumeFuel();
    }

    protected virtual void ConsumeFuel()
    {
        if (fuel > 0)
        {
            fuel -= fuelConsumptionRate * Time.deltaTime;
            UpdateFuelUI();
        }
        else
        {
            Debug.Log("Out of Fuel!");
        }
    }

    public void Grow()
    {
        Transform segment = Instantiate(segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;
        _segments.Add(segment);
    }

    public void ResetState()
    {
        for (int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }
        _segments.Clear();
        _segments.Add(this.transform);
        for (int i = 1; i < initialSize; i++)
        {
            _segments.Add(Instantiate(segmentPrefab));
        }
        transform.position = Vector3.zero;
        fuel = 100f;
        UpdateFuelUI();
    }

    public void ActivateShield()
    {
        isShieldActive = true;
        shieldDuration = 5f; // Duración del escudo
        playerRenderer.material.color = Color.blue; // Cambia el color del jugador

        // Instancia el efecto de escudo
        shieldEffectInstance = Instantiate(shieldEffectPrefab, transform.position, Quaternion.identity, transform);
        Debug.Log("Shield Activated!");
    }

    public void DeactivateShield()
    {
        isShieldActive = false;
        playerRenderer.material.color = Color.white; // Vuelve al color original

        // Destruye el efecto de escudo
        if (shieldEffectInstance != null)
        {
            Destroy(shieldEffectInstance);
        }
        Debug.Log("Shield Deactivated!");
    }

    public void ActivateHyperSpeed()
    {
        isHyperSpeedActive = true;
        hyperSpeedDuration = 5f;
        Debug.Log("HyperSpeed Activated!");
    }

    public void DeactivateHyperSpeed()
    {
        isHyperSpeedActive = false;
        Debug.Log("HyperSpeed Deactivated!");
    }

    private void UpdateFuelUI()
    {
        fuelSlider.value = fuel;
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {
            Grow();
        }
        else if (other.CompareTag("Obstacle"))
        {
            // Mostrar pantalla de Game Over al chocar con un obstáculo
            gameOverManager.ShowGameOver();
            Destroy(gameObject); // Destruir el jugador
        }
        else if (other.CompareTag("Bomb"))
        {
            if (!isShieldActive) // Si el escudo NO está activo
            {
                gameOverManager.ShowGameOver(); // Mostrar pantalla de Game Over
                Destroy(gameObject); // Destruir el jugador
                Debug.Log("¡El jugador ha sido destruido por una bomba!");
            }
        }
        else if (other.CompareTag("Shield"))
        {
            ActivateShield();
            // Reposiciona el ítem en lugar de destruirlo
            Power powerComponent = other.GetComponent<Power>();
            if (powerComponent != null)
            {
                powerComponent.RandomizePosition(); // Reaparece el ítem en una nueva posición
            }
            else
            {
                Debug.LogWarning("El objeto Shield no tiene el componente 'Item'.");
            }
        }
        else if (other.CompareTag("Speed"))
        {
            ActivateHyperSpeed();
            // Reposiciona el ítem en lugar de destruirlo
            Power powerComponent = other.GetComponent<Power>();
            if (powerComponent != null)
            {
                powerComponent.RandomizePosition(); // Reaparece el ítem en una nueva posición
            }
            else
            {
                Debug.LogWarning("El objeto Speed no tiene el componente 'Item'.");
            }
        }
    }

    public virtual void CollectFuel(float amount)
    {
        fuel += amount;
        if (fuel > 100)
        {
            fuel = 100;
        }
        UpdateFuelUI();
    }
}
