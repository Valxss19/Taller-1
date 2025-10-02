using UnityEngine;
using UnityEngine.UI;

public class VidaJugador : MonoBehaviour
{
    public int vida = 3; // La vida inicial del jugador
    public Image[] corazones; // Aquí arrastraremos los corazones desde el inspector
    public Sprite corazonLleno; 
    public Sprite corazonVacio;

    void Update()
    {
        // Nos aseguramos de que la vida no se pase de límites
        if (vida > corazones.Length)
            vida = corazones.Length;

        // Dibujar los corazones según la vida
        for (int i = 0; i < corazones.Length; i++)
        {
            if (i < vida)
                corazones[i].sprite = corazonLleno; // corazón encendido
            else
                corazones[i].sprite = corazonVacio; // corazón apagado
        }
    }

    // Método para perder vida
    public void QuitarVida(int cantidad)
    {
        vida -= cantidad;
        if (vida < 0) vida = 0;
    }

    // Método para curarse
    public void CurarVida(int cantidad)
    {
        vida += cantidad;
        if (vida > corazones.Length) vida = corazones.Length;
    }
}
