using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int puntos = 0; // Contador de huesos
    [SerializeField] private GameObject obstaculo; // El objeto que bloquea la llave

    private bool tieneLlave = false; // Estado de la llave

    public void SumarPunto()
    {
        puntos++;
        Debug.Log("Puntos: " + puntos);

        if (puntos >= 10)
        {
            obstaculo.SetActive(false); // Desaparece el obstáculo
            Debug.Log("¡Obstáculo eliminado!");
        }
    }

    public void RecogerLlave()
    {
        tieneLlave = true;
        Debug.Log("Llave recogida");
    }

    public void LlegarPuerta()
    {
        if (tieneLlave)
        {
            Debug.Log("¡GANASTE!");
            Time.timeScale = 0f; // Pausa el juego
        }
        else
        {
            Debug.Log("Necesitas la llave para abrir la puerta");
        }
    }
}
