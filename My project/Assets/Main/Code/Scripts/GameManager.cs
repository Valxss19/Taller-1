using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // 👈 Necesario para usar TextMeshPro

public class GameManager : MonoBehaviour
{
    public int puntos = 0; // Contador de huesos
    [SerializeField] private GameObject obstaculo; // El objeto que bloquea la llave

    private bool tieneLlave = false; // Estado de la llave

    [SerializeField] private float tiempoInicial = 60f;
    private float tiempoRestante;

    [SerializeField] private int vidaInicial = 3;
    private int vidaActual;

    // 👇 Referencias a los textos UI
    [SerializeField] private TextMeshProUGUI tiempoTexto;
    [SerializeField] private TextMeshProUGUI vidaTexto;
    [SerializeField] private TextMeshProUGUI puntosTexto;
    [SerializeField] private TextMeshProUGUI llaveTexto;

    private void Start()
    {
        tiempoRestante = tiempoInicial;
        vidaActual = vidaInicial;

        ActualizarUI();
    }

    private void Update()
    {
        // ⏳ Tiempo
        tiempoRestante -= Time.deltaTime;

        if (tiempoRestante <= 0)
        {
            ReiniciarNivel();
        }

        ActualizarUI();
    }

    public void SumarPunto()
    {
        puntos++;

        if (puntos >= 10)
        {
            obstaculo.SetActive(false);
        }

        ActualizarUI();
    }

    public void SumarTiempo(float extra)
    {
        tiempoRestante += extra;
        ActualizarUI();
    }

    public void SumarVida(int extra)
    {
        vidaActual += extra;
        ActualizarUI();
    }

    public void RestarVida(int daño)
    {
        vidaActual -= daño;

        if (vidaActual <= 0)
        {
            ReiniciarNivel();
        }

        ActualizarUI();
    }

    public void RecogerLlave()
    {
        tieneLlave = true;
        ActualizarUI();
    }

    public void LlegarPuerta()
    {
        if (tieneLlave)
        {
            llaveTexto.text = "¡GANASTE!"; // 👈 Mensaje final en UI
            Time.timeScale = 0f;
        }
        else
        {
            llaveTexto.text = "Llave: No (necesitas la llave)";
        }
    }

    private void ReiniciarNivel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // 👇 Método que actualiza toda la UI
    private void ActualizarUI()
    {
        if (tiempoTexto != null) tiempoTexto.text = "Tiempo: " + Mathf.CeilToInt(tiempoRestante);
        if (vidaTexto != null) vidaTexto.text = "Vida: " + vidaActual;
        if (puntosTexto != null) puntosTexto.text = "Puntos: " + puntos;
        if (llaveTexto != null) llaveTexto.text = "Llave: " + (tieneLlave ? "Sí" : "No");
    }
    // 👇 Getters para acceder desde otros scripts
public float GetTiempo()
{
    return tiempoRestante;
}

public int GetVida()
{
    return vidaActual;
}

public int GetPuntos()
{
    return puntos;
}

public bool TieneLlave()
{
    return tieneLlave;
}

}
