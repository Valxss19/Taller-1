using UnityEngine;

public class Trampa : MonoBehaviour
{
    [SerializeField] private int daño = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.RestarVida(daño);
            Debug.Log("El jugador cayó en una trampa");
        }
    }
}
