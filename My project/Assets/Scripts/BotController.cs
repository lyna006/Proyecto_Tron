using UnityEngine;

public class BotController : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 direction;

    void Start()
    {
        // Inicialmente, elige una direcci�n aleatoria
        ChangeDirection();
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        // Cambiar de direcci�n aleatoriamente cada cierto tiempo
        if (Random.value < 0.01f)
        {
            ChangeDirection();
        }

        // Aseg�rate de que el bot no se salga de la pantalla
        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, -8f, 8f); // Ajusta seg�n el tama�o de tu pantalla
        position.y = Mathf.Clamp(position.y, -4f, 4f);
        transform.position = position;
    }

    void ChangeDirection()
    {
        // Elige una nueva direcci�n aleatoria
        direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}
