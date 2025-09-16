using UnityEngine;
using UnityEngine.SceneManagement;

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

    // Añade N puntos (usado internamente)
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

    // Acceso rápido (script más simple llama a esto)
    public void SumarPunto()
    {
        SumarPuntos(1);
    }

    // Sumar vida (positivo) — otros scripts pueden llamarlo
    public void SumarVida(int cantidad)
    {
        vidaActual += cantidad;
        Debug.Log("Vida aumentada: " + vidaActual);
    }

    // Sumar tiempo (por muslito azul)
    public void SumarTiempo(float cantidad)
    {
        tiempoRestante += cantidad;
        Debug.Log("Tiempo añadido: +" + cantidad + "s (restante: " + Mathf.CeilToInt(tiempoRestante) + "s)");
    }

    // Restar vida (trampas)
    public void RestarVida(int cantidad)
    {
        vidaActual -= cantidad;
        Debug.Log("Vida actual: " + vidaActual);
        if (vidaActual <= 0) CambiarEstado(EstadoJuego.Derrota);
    }

    // Recoger llave
    public void RecogerLlave()
    {
        tieneLlave = true;
        Debug.Log("Llave recogida");
    }

    // Intentar pasar por la puerta
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

    // Reinicia la escena (útil si quieres recargar todo)
    public void ReiniciarNivel()
    {
        Time.timeScale = 1f; // asegurar que el tiempo vuelve a normal antes de recargar
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // =====================
    // CONTROL DE ESTADO (switch central)
    // =====================
    public void CambiarEstado(EstadoJuego nuevoEstado)
    {
        // Apagar todos los menús (si existen)
        if (menuInicio) menuInicio.SetActive(false);
        if (menuPausa) menuPausa.SetActive(false);
        if (menuVictoria) menuVictoria.SetActive(false);
        if (menuGameOver) menuGameOver.SetActive(false);

        estadoActual = nuevoEstado;

        switch (nuevoEstado)
        {
            case EstadoJuego.Inicio:
                // Mostrar menú inicio
                if (menuInicio) menuInicio.SetActive(true);
                // dejar valores iniciales visibles (no cambiar internos)
                break;

            case EstadoJuego.Jugando:
                // Al comenzar partida, reestablecer valores iniciales
                tiempoRestante = tiempoInicial;
                vidaActual = vidaInicial;
                puntos = 0;
                tieneLlave = false;
                if (obstaculo != null) obstaculo.SetActive(true);
                if (menuInicio) menuInicio.SetActive(false);
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

    // =====================
    // MÉTODOS PARA BOTONES UI (conectar en OnClick)
    // =====================
    public void IniciarJuego() => CambiarEstado(EstadoJuego.Jugando);
    public void PausarJuego() => CambiarEstado(EstadoJuego.Pausa);
    public void ReanudarJuego() => CambiarEstado(EstadoJuego.Jugando);
    // Reinicia internamente y vuelve a jugar desde cero
    public void ReintentarJuego()
    {
        CambiarEstado(EstadoJuego.Jugando);
    }
    public void SalirJuego()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }

    // =====================
    // MÉTODOS AUXILIARES (mantenidos por compatibilidad con otros scripts)
    // =====================

    // Resetea los valores internos sin cambiar automáticamente el estado visual (útil si quieres limpiar datos desde inspector)
    public void ResetearEstado()
    {
        tiempoRestante = tiempoInicial;
        vidaActual = vidaInicial;
        puntos = 0;
        tieneLlave = false;
        if (obstaculo != null) obstaculo.SetActive(true);
        Debug.Log("GameManager: estado reseteado");
    }

    // =====================
    // GETTERS
    // =====================
    public float GetTiempo() => tiempoRestante;
    public int GetVida() => vidaActual;
    public int GetPuntos() => puntos;
    public bool TieneLlave() => tieneLlave;
}
