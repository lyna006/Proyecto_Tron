using UnityEngine;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    private Vector2 _direction = Vector2.right;
    private List<Transform> _segments = new List<Transform>();
    public Transform segmentPrefab;
    public int initialSize = 3;

    private bool isShieldActive = false;
    private bool isHyperSpeedActive = false;
    private float hyperSpeedDuration = 5f; // Ejemplo de duración
    private float shieldDuration = 5f; // Ejemplo de duración
    private float hyperSpeedMultiplier = 2f; // Ejemplo de multiplicador de velocidad

    private void Start()
    {
        ResetState();
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

        if (isHyperSpeedActive)
        {
            // Reduce the duration of the hyper speed effect
            hyperSpeedDuration -= Time.deltaTime;
            if (hyperSpeedDuration <= 0)
            {
                DeactivateHyperSpeed();
            }
        }

        if (isShieldActive)
        {
            // Reduce the duration of the shield effect
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
    }

    public void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);
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
        for (int i = 1; i < this.initialSize; i++)
        {
            _segments.Add(Instantiate(this.segmentPrefab));
        }
        this.transform.position = Vector3.zero;
    }

    public void ActivateShield()
    {
        isShieldActive = true;
        // Aquí puedes agregar la lógica visual para mostrar el escudo
        // Por ejemplo, cambiar el color o agregar un efecto visual
    }

    public void DeactivateShield()
    {
        isShieldActive = false;
        // Aquí puedes agregar la lógica visual para ocultar el escudo
    }

    public void ActivateHyperSpeed()
    {
        isHyperSpeedActive = true;
        hyperSpeedDuration = 5f; // Ajusta la duración como desees
        // Aquí puedes agregar la lógica visual para mostrar la hiper velocidad
    }

    public void DeactivateHyperSpeed()
    {
        isHyperSpeedActive = false;
        // Aquí puedes agregar la lógica visual para ocultar la hiper velocidad
    }

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
