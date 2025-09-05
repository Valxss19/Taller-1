using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb2d;
    [SerializeField]
    private float fuerzaMovimiento = 5f; 
    

    public bool tieneLlave = false; // ✅ Sencillo: solo dice si el jugador recogió la llave

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Movimiento y salto con A y D
        if (Input.GetKeyDown(KeyCode.A))
        {
            rb2d.AddForce(new Vector2(-1, 1) * fuerzaMovimiento, ForceMode2D.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            rb2d.AddForce(new Vector2(1, 1) * fuerzaMovimiento, ForceMode2D.Impulse);
        }
    }

    // Detectar colisiones con llave y puerta
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Llave"))
        {
            tieneLlave = true; // ✅ Ahora tiene la llave
            Destroy(collision.gameObject); // Desaparece la llave
            Debug.Log("Llave: Sí");
        }

        if (collision.CompareTag("Puerta") && tieneLlave)
        {
            Debug.Log("¡GANASTE!");
            Time.timeScale = 0f; // Pausa el juego
        }
    }
}
