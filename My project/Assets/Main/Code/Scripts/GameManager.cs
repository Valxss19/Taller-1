using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // por si usas Image en corazones

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum EstadoJuego { Inicio, Jugando, Pausa, Victoria, Derrota }
    public EstadoJuego estadoActual;

    [Header("Referencias")]
    [SerializeField] private GameObject obstaculo;
    [SerializeField] private GameObject menuInicio;
    [SerializeField] private GameObject menuPausa;
    [SerializeField] private GameObject menuVictoria;
    [SerializeField] private GameObject menuGameOver;
    [SerializeField] private GameObject[] corazones; // <<< AÑADIDO: referencias a los corazones en la UI

    [Header("Ajustes iniciales")]
    [SerializeField] private float tiempoInicial = 60f;
    [SerializeField] private int vidaInicial = 3;

    // Estado interno
    private float tiempoRestante;
    private int vidaActual;
    private int puntos = 0;
    private bool tieneLlave = false;

    private void Awake()
    {
        // Singleton simple
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        // Empieza en menú de inicio
        CambiarEstado(EstadoJuego.Inicio);
    }

    private void Update()
    {
        // Lógica principal por estado usando switch
        switch (estadoActual)
        {
            case EstadoJuego.Inicio:
                Time.timeScale = 0f; // pausa mientras está el menú de inicio
                break;

            case EstadoJuego.Jugando:
                Time.timeScale = 1f;
                tiempoRestante -= Time.deltaTime;

                // Condiciones de derrota por tiempo o vida
                if (tiempoRestante <= 0f || vidaActual <= 0)
                {
                    CambiarEstado(EstadoJuego.Derrota);
                }
                break;

            case EstadoJuego.Pausa:
                Time.timeScale = 0f;
                break;

            case EstadoJuego.Victoria:
                Time.timeScale = 0f;
                break;

            case EstadoJuego.Derrota:
                Time.timeScale = 0f;
                break;
        }
    }

    // =====================
    // MÉTODOS PÚBLICOS DE JUEGO (llamables por otros scripts)
    // =====================

    public void SumarPuntos(int cantidad)
    {
        puntos += cantidad;
        Debug.Log("Puntos: " + puntos);

        if (puntos >= 10)
        {
            if (obstaculo != null)
            {
                obstaculo.SetActive(false);
                Debug.Log("¡Obstáculo eliminado!");
            }
            else
            {
                Debug.LogWarning("GameManager: obstaculo no asignado en el Inspector.");
            }
        }
    }

    public void SumarPunto()
    {
        SumarPuntos(1);
    }

    public void SumarVida(int cantidad)
    {
        vidaActual += cantidad;
        if (vidaActual > vidaInicial) vidaActual = vidaInicial;
        ActualizarCorazones();
        Debug.Log("Vida aumentada: " + vidaActual);
    }

    public void SumarTiempo(float cantidad)
    {
        tiempoRestante += cantidad;
        Debug.Log("Tiempo añadido: +" + cantidad + "s (restante: " + Mathf.CeilToInt(tiempoRestante) + "s)");
    }

    public void RestarVida(int cantidad)
    {
        vidaActual -= cantidad;
        ActualizarCorazones();
        Debug.Log("Vida actual: " + vidaActual);
        if (vidaActual <= 0) CambiarEstado(EstadoJuego.Derrota);
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
            CambiarEstado(EstadoJuego.Victoria);
        }
        else
        {
            Debug.Log("La puerta está cerrada. Necesitas la llave.");
        }
    }

    public void ReiniciarNivel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void CambiarEstado(EstadoJuego nuevoEstado)
    {
        if (menuInicio) menuInicio.SetActive(false);
        if (menuPausa) menuPausa.SetActive(false);
        if (menuVictoria) menuVictoria.SetActive(false);
        if (menuGameOver) menuGameOver.SetActive(false);

        estadoActual = nuevoEstado;

        switch (nuevoEstado)
        {
            case EstadoJuego.Inicio:
                if (menuInicio) menuInicio.SetActive(true);
                break;

            case EstadoJuego.Jugando:
                tiempoRestante = tiempoInicial;
                vidaActual = vidaInicial;
                puntos = 0;
                tieneLlave = false;
                if (obstaculo != null) obstaculo.SetActive(true);
                if (menuInicio) menuInicio.SetActive(false);
                ActualizarCorazones(); // <<< AÑADIDO: mostrar todos los corazones al iniciar
                break;

            case EstadoJuego.Pausa:
                if (menuPausa) menuPausa.SetActive(true);
                break;

            case EstadoJuego.Victoria:
                if (menuVictoria) menuVictoria.SetActive(true);
                break;

            case EstadoJuego.Derrota:
                if (menuGameOver) menuGameOver.SetActive(true);
                break;
        }
    }

    public void IniciarJuego() => CambiarEstado(EstadoJuego.Jugando);
    public void PausarJuego() => CambiarEstado(EstadoJuego.Pausa);
    public void ReanudarJuego() => CambiarEstado(EstadoJuego.Jugando);
    public void ReintentarJuego()
    {
        CambiarEstado(EstadoJuego.Jugando);
    }
    public void SalirJuego()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }

    public void ResetearEstado()
    {
        tiempoRestante = tiempoInicial;
        vidaActual = vidaInicial;
        puntos = 0;
        tieneLlave = false;
        if (obstaculo != null) obstaculo.SetActive(true);
        ActualizarCorazones();
        Debug.Log("GameManager: estado reseteado");
    }

    public float GetTiempo() => tiempoRestante;
    public int GetVida() => vidaActual;
    public int GetPuntos() => puntos;
    public bool TieneLlave() => tieneLlave;

    // =====================
    // MÉTODO NUEVO PARA LOS CORAZONES
    // =====================
    private void ActualizarCorazones()
    {
        if (corazones == null || corazones.Length == 0) return;

        for (int i = 0; i < corazones.Length; i++)
        {
            corazones[i].SetActive(i < vidaActual);
        }
    }
}
