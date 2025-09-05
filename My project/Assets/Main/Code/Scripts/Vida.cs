using UnityEngine;

public class Vida : MonoBehaviour
{
    public int vidaExtra = 1; // cuánto suma

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FindFirstObjectByType<GameManager>().SumarVida(vidaExtra);
            Destroy(gameObject);
        }
    }
}
