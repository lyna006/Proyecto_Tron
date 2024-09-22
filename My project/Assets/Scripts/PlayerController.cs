using UnityEngine;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    private Vector2 _direction = Vector2.right;
    private List<Transform> _segments = new List<Transform>();
    public Transform segmentPrefab;
    public int initialSize = 3;
    float moveSpeed = 2.0f;

    private bool isShieldActive = false;
    private bool isHyperSpeedActive = false;
    private float hyperSpeedDuration = 5f;
    private float shieldDuration = 5f;
    private float hyperSpeedMultiplier = 2f;

    private GridManager gridManager;
    private Nodo2 nodoActual;

    void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
        ResetState();
        nodoActual = gridManager.primerNodo; // Inicia en el primer nodo
        transform.position = nodoActual.posicion; // Ajustar según tu grid
    }

    private void Update()
    {
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

        // Manejo de duración de poderes
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
        if (_direction != Vector2.zero)
        {
            Mover();
        }
    }

    private void Mover()
    {
        // Calcula la nueva posición del nodo
        Vector2 nuevaPosicion = nodoActual.posicion + _direction;

        // Verifica si el nodo existe en el grid
        Nodo2 siguienteNodo = gridManager.GetNodoEn(nuevaPosicion);
        if (siguienteNodo != null)
        {
            nodoActual = siguienteNodo;
            transform.position = nodoActual.posicion;

            // Añadir un nuevo segmento si se mueve
            Grow();
        }
    }

    public void Grow()
    {
        if (_segments.Count < initialSize)
        {
            Transform segment = Instantiate(segmentPrefab);
            segment.position = _segments[_segments.Count - 1].position; // Posición del último segmento
            _segments.Add(segment);
        }
    }

    public void ResetState()
    {
        for (int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }
        _segments.Clear();
        _segments.Add(this.transform);
        for (int i = 1; i < this.initialSize; i++)
        {
            _segments.Add(Instantiate(this.segmentPrefab));
        }
        this.transform.position = Vector3.zero; // Verifica esto
    }

    public void ActivateShield() { isShieldActive = true; }
    public void DeactivateShield() { isShieldActive = false; }
    public void ActivateHyperSpeed() { isHyperSpeedActive = true; hyperSpeedDuration = 5f; }
    public void DeactivateHyperSpeed() { isHyperSpeedActive = false; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Item")
        {
            Grow();
        }
        else if (other.tag == "Obstacle")
        {
            ResetState();
        }
    }
}
