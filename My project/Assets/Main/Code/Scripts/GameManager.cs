using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Singleton para acceder fácil desde otros scripts (GameManager.Instance)
    public static GameManager Instance { get; private set; }

    [Header("Referencias")]
    [SerializeField] private GameObject obstaculo; // el objeto que bloquea la llave (asignar en Inspector)

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
        // Singleton simple (evita duplicados)
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // Asegurarse que el tiempo no quede en 0 (si se pausó antes)
        Time.timeScale = 1f;
    }

    private void Start()
    {
        tiempoRestante = tiempoInicial;
        vidaActual = vidaInicial;
    }

    private void Update()
    {
        // Si el juego está en pausa (timeScale == 0) no descontamos tiempo
        if (Time.timeScale == 0f) return;

        // Actualizar tiempo
        tiempoRestante -= Time.deltaTime;

        if (tiempoRestante <= 0f)
        {
            ReiniciarNivel();
        }
    }

    // -------------------------
    // PUNTOS
    // -------------------------
    // Llamado por Hueso (sin parámetros)
    public void SumarPunto()
    {
        SumarPuntos(1);
    }

    // Versión con cantidad (por compatibilidad)
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

    // -------------------------
    // TIEMPO
    // -------------------------
    // Sumar tiempo extra (Muslito Azul)
    public void SumarTiempo(float extra)
    {
        tiempoRestante += extra;
        Debug.Log("Tiempo extra: +" + extra + "s (restante: " + Mathf.CeilToInt(tiempoRestante) + "s)");
    }

    // -------------------------
    // VIDA
    // -------------------------
    public void SumarVida(int cantidad)
    {
        vidaActual += cantidad;
        Debug.Log("Vida aumentada: " + vidaActual);
    }

    public void RestarVida(int cantidad)
    {
        vidaActual -= cantidad;
        Debug.Log("Vida actual: " + vidaActual);

        if (vidaActual <= 0)
        {
            Debug.Log("Vida llegó a 0 -> Reiniciando nivel");
            ReiniciarNivel();
        }
    }

    // -------------------------
    // LLAVE / PUERTA
    // -------------------------
    // Llamado por el script de la llave
    public void RecogerLlave()
    {
        tieneLlave = true;
        Debug.Log("Llave recogida");
    }

    // Llamado cuando el jugador llega a la puerta
    public void LlegarPuerta()
    {
        if (tieneLlave)
        {
            Debug.Log("¡GANASTE!");
            Time.timeScale = 0f; // pausa el juego
        }
        else
        {
            Debug.Log("La puerta está cerrada. Necesitas la llave.");
        }
    }

    // -------------------------
    // REINICIO
    // -------------------------
    private void ReiniciarNivel()
    {
        // Asegura que el timeScale vuelve a 1 para que la escena recargada funcione bien
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // -------------------------
    // GETTERS (usados por la UI u otros scripts)
    // -------------------------
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

    // -------------------------
    // MÉTODOS AUXILIARES (opcional)
    // -------------------------
    // Reinicia valores (útil para testing)
    public void ResetearEstado()
    {
        tiempoRestante = tiempoInicial;
        vidaActual = vidaInicial;
        puntos = 0;
        tieneLlave = false;
        if (obstaculo != null) obstaculo.SetActive(true);
        Time.timeScale = 1f;
        Debug.Log("GameManager: estado reseteado");
    }
}
