using UnityEngine;

public class Hueso : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<GameManager>().SumarPunto(); // Suma +1
            Destroy(gameObject); // El hueso desaparece
        }
    }
}
