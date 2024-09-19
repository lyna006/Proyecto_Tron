using UnityEngine;

public class Power : MonoBehaviour
{
    public enum PowerType
    {
        Shield,
        HyperSpeed
    }

    public PowerType powerType; // Define el tipo de poder para este objeto

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica si el jugador ha colisionado con el poder
        if (other.CompareTag("Player"))
        {
            // Referencia al script del jugador
            PlayerController player = other.GetComponent<PlayerController>();

            if (player != null)
            {
                // Activa el poder correspondiente basado en el tipo de poder
                switch (powerType)
                {
                    case PowerType.Shield:
                        player.ActivateShield();
                        break;
                    case PowerType.HyperSpeed:
                        player.ActivateHyperSpeed();
                        break;
                }

                // Destruye el objeto de poder después de ser recolectado
                Destroy(gameObject);
            }
        }
    }
}
