using UnityEngine;

public class BotController : PlayerController
{
    public BoxCollider2D gridArea; // Asigna esto desde el inspector
    public int intervaloCambioDireccion = 1; // Intervalo de tiempo para cambiar de dirección 
    private float temp = 0.0f;

    protected override void Start()
    {
        base.Start(); // Llama al método Start de PlayerController
        // Aquí puedes inicializar cualquier cosa adicional que necesites para el bot
    }

    private void Update()
    {
        // Actualiza el temporizador
        temp += Time.deltaTime;

        // Cambia la dirección cada 'intervaloCambioDireccion' segundos
        if (temp >= intervaloCambioDireccion)
        {
            temp = 0.0f;
            CambiarDireccion();
        }
    }

    private void CambiarDireccion()
    {
        Vector2 nuevaDireccion;
        do
        {
            int direccionAleatoria = Random.Range(0, 4);

            if (direccionAleatoria == 0)
            {
                nuevaDireccion = Vector2.up;
            }
            else if (direccionAleatoria == 1)
            {
                nuevaDireccion = Vector2.down;
            }
            else if (direccionAleatoria == 2)
            {
                nuevaDireccion = Vector2.right;
            }
            else
            {
                nuevaDireccion = Vector2.left;
            }
        } while (!EsDireccionValida(nuevaDireccion));

        _direction = nuevaDireccion; // Cambia la dirección solo si es válida
    }

    private bool EsDireccionValida(Vector2 direccion)
    {
        // Comprueba si la nueva dirección no llevará al bot fuera de los límites del grid
        Vector3 futuraPosicion = transform.position + (Vector3)direccion;
        return gridArea.bounds.Contains(futuraPosicion);
    }

    protected override void FixedUpdate()
    {
        // Mueve al bot y actualiza su estela
        base.FixedUpdate(); // Llama al método base para manejar el movimiento y la estela

        // Calcular la futura posición
        Vector3 futuraPosicion = transform.position + (Vector3)_direction;

        // Solo mover si la futura posición es válida
        if (EsDireccionValida(_direction))
        {
            transform.position = futuraPosicion;
        }
        else
        {
            // Cambia la dirección si colisionaría con el borde
            CambiarDireccion();
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            // Cambiar de dirección al colisionar con un obstáculo
            CambiarDireccion();
        }
        else
        {
            base.OnTriggerEnter2D(other); // Llama al método base para manejar otras colisiones
        }
    }
}