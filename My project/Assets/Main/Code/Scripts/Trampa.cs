using UnityEngine;

public class Trampa : MonoBehaviour
{
    public int daño = 1; // cuánto resta

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FindFirstObjectByType<GameManager>().RestarVida(daño);
        }
    }
}
