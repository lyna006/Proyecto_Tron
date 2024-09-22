using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    // Variables de movimiento
    private Vector2 _direction = Vector2.right;
    private List<Transform> _segments = new List<Transform>();
    public Transform segmentPrefab;
    public int initialSize = 3;

    // Variables de poderes
    private bool isShieldActive = false;
    private bool isHyperSpeedActive = false;
    private float hyperSpeedDuration = 5f;
    private float shieldDuration = 5f;
    private float hyperSpeedMultiplier = 2f;

    // Variables de combustible
    public float fuel = 100f; // Valor de combustible inicial
    public float fuelConsumptionRate = 5f; // Combustible consumido cada 5 elementos de malla
    public Slider fuelSlider; // Referencia al UI Slider para la barra de combustible
    private int stepsSinceLastFuelConsumption = 0; // Pasos desde la última vez que se consumió combustible

    // Variables para manejar la distancia recorrida
    private Vector3 lastPosition;

    private void Start()
    {
        // Inicializa el estado del jugador y la barra de combustible
        ResetState();
        lastPosition = transform.position;
        fuel = 100;
        UpdateFuelUI();
    }

    private void Update()
    {
        // Manejo de dirección de movimiento con teclas
        if (Input.GetKeyDown(KeyCode.W))
        {
            _direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            _direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            _direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            _direction = Vector2.right;
        }

        // Controles de duración de poderes
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
    }

    private void FixedUpdate()
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

        // Consume combustible cada vez que se mueve
        ConsumeFuel();
    }

    private void ConsumeFuel()
    {
        if (fuel > 0)
        {
            fuel -= 1; // Ajusta la cantidad consumida según lo necesites
            UpdateFuelUI();
        }
        else
        {
            Debug.Log("Out of Fuel!");
        }
    }


    // Método para crecer la estela del jugador
    public void Grow()
    {
        Transform segment = Instantiate(segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;
        _segments.Add(segment);
    }

    // Resetea el estado inicial del jugador y la estela
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
        fuel = 100f; // Restablece el combustible al máximo
        UpdateFuelUI();
    }

    // Métodos para activar y desactivar el escudo
    public void ActivateShield()
    {
        isShieldActive = true;
        shieldDuration = 5f;
        Debug.Log("Shield Activated!");
    }

    public void DeactivateShield()
    {
        isShieldActive = false;
        Debug.Log("Shield Deactivated!");
    }

    // Métodos para activar y desactivar la hiper velocidad
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

    // Método para consumir combustible


    // Actualiza la barra de combustible en la interfaz de usuario
    private void UpdateFuelUI()
    {
        fuelSlider.value = fuel; // Asegúrate de que fuelSlider esté asignado en el Inspector
    }


    // Manejo de colisiones con objetos
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {
            Grow(); // Crece la estela al recoger un ítem
        }
        else if (other.CompareTag("Obstacle"))
        {
            ResetState(); // Reinicia el estado al chocar con un obstáculo
        }
        else if (other.CompareTag("Fuel"))
        {
            CollectFuel(20); // Ejemplo: añade 20 unidades de combustible al recoger el ítem
            Destroy(other.gameObject); // Destruye el ítem después de recogerlo
        }
    }

    private void CollectFuel(float amount)
    {
        fuel += amount;
        if (fuel > 100)
        {
            fuel = 100; // Asegúrate de que no supere el máximo
        }
        UpdateFuelUI(); // Actualiza la UI del combustible
    }

}
