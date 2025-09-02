using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // para reiniciar la escena

public class TiempoManager : MonoBehaviour
{
    public float tiempo = 60f; // tiempo inicial
    public TextMeshProUGUI tiempoTexto; // referencia al texto en UI

    private void Update()
    {
        // Reducir el tiempo cada segundo
        tiempo -= Time.deltaTime;

        // Evitar que muestre números negativos
        if (tiempo < 0)
            tiempo = 0;

        // Actualizar el texto en pantalla
        tiempoTexto.text = "Tiempo: " + Mathf.Ceil(tiempo).ToString();

        // Si el tiempo llega a 0, reiniciar la escena
        if (tiempo <= 0)
        {
            ReiniciarEscena();
        }
    }

    // Si el jugador toca un muslito azul
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MuslitoAzul"))
        {
            tiempo += 10f; // sumar 10 segundos
            Destroy(collision.gameObject); // eliminar el muslito
        }
    }

    void ReiniciarEscena()
    {
        // Recarga la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
