using UnityEngine;
using UnityEngine.SceneManagement; // para reiniciar la escena

public class Character : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private Vector2 direccion;
    [SerializeField] private float fuerza;

    private int puntos = 0;

    // --- VIDA ---
    [Header("Vida")]
    [SerializeField] private int vidaMax = 3;   // vida inicial/máxima
    private int vidaActual;                     // vida en juego

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        vidaActual = vidaMax;
        Debug.Log("Vida inicial: " + vidaActual);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            rb2d.AddForce(direccion * fuerza);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            Vector2 direccionInvertida = new Vector2(-direccion.x, direccion.y);
            rb2d.AddForce(direccionInvertida * fuerza);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 🦴 Hueso → suma puntos
        if (other.CompareTag("Hueso"))
        {
            puntos += 1;
            Destroy(other.gameObject);
            Debug.Log("Huesos: " + puntos);
        }

        // Nota: La VIDA y las TRAMPAS las manejan los otros scripts
        // (OrangeChicken y Trap) llamando a SumarVida / RestarVida.
        // Así evitamos duplicar efectos.
    }

    // ===== Métodos públicos para que otros objetos afecten la vida =====
    public void SumarVida(int cantidad)
    {
        vidaActual += cantidad;
        if (vidaActual > vidaMax) vidaActual = vidaMax;
        Debug.Log($"Vida +{cantidad} → {vidaActual}/{vidaMax}");
    }

    public void RestarVida(int cantidad)
    {
        vidaActual -= cantidad;
        Debug.Log($"Vida -{cantidad} → {vidaActual}/{vidaMax}");

        if (vidaActual <= 0)
        {
            Debug.Log("💀 Has muerto. Reiniciando escena...");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    // (Opcional) getters si luego quieres mostrarlos en UI
    public int GetVidaActual() => vidaActual;
    public int GetVidaMax() => vidaMax;
}
