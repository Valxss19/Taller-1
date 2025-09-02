

using UnityEngine;
using TMPro;

public class Hueso : MonoBehaviour
{
    public int puntos = 0; // contador de huesos
    public TextMeshProUGUI puntosTexto; // texto en la UI
    public GameObject obstaculo; // obstáculo que bloquea la llave

    private void Start()
    {
        ActualizarUI();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hueso"))
        {
            puntos += 1;
            Destroy(collision.gameObject);
            ActualizarUI();

            if (puntos >= 10 && obstaculo != null)
            {
                Destroy(obstaculo);
            }
        }
    }

    void ActualizarUI()
    {
        puntosTexto.text = "Puntos: " + puntos.ToString();
    }
}


