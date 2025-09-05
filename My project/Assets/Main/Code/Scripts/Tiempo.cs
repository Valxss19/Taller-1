using UnityEngine;

public class Tiempo : MonoBehaviour
{
    public float tiempoExtra = 5f; // 👈 segundos que añade

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FindFirstObjectByType<GameManager>().SumarTiempo(tiempoExtra);
            Destroy(gameObject); // 👈 desaparece solo este muslito
        }
    }
}
