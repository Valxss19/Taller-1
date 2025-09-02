using UnityEngine;
using TMPro; // Si usas TextMeshPro

public class LifeManager : MonoBehaviour
{
    public static LifeManager instance; // Para acceder desde otros scripts

    [SerializeField] private int vidaMaxima = 3; // Vida inicial
    private int vidaActual;

    [SerializeField] private TMP_Text textoVida; // Para mostrar la vida en pantalla

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        vidaActual = vidaMaxima;
        ActualizarTexto();
    }

    // M�todo para restar vida
    public void QuitarVida(int cantidad)
    {
        vidaActual -= cantidad;
        if (vidaActual < 0) vidaActual = 0;

        ActualizarTexto();

        if (vidaActual == 0)
        {
            Debug.Log("Game Over!");
            // Aqu� puedes reiniciar el nivel, mostrar un men�, etc.
        }
    }

    // M�todo para sumar vida (si recoges un �tem)
    public void AgregarVida(int cantidad)
    {
        vidaActual += cantidad;
        if (vidaActual > vidaMaxima) vidaActual = vidaMaxima;

        ActualizarTexto();
    }

    private void ActualizarTexto()
    {
        if (textoVida != null)
            textoVida.text = "Vida: " + vidaActual;
    }
}
