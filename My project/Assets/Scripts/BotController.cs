using UnityEngine;

public class BotController : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 direction;

    void Start()
    {
        // Inicialmente, elige una dirección aleatoria
        ChangeDirection();
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        // Cambiar de dirección aleatoriamente cada cierto tiempo
        if (Random.value < 0.01f)
        {
            ChangeDirection();
        }

        // Asegúrate de que el bot no se salga de la pantalla
        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, -8f, 8f); // Ajusta según el tamaño de tu pantalla
        position.y = Mathf.Clamp(position.y, -4f, 4f);
        transform.position = position;
    }

    void ChangeDirection()
    {
        // Elige una nueva dirección aleatoria
        direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}
