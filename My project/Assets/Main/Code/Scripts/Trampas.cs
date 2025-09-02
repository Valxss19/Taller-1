using UnityEngine;

public class Trap : MonoBehaviour
{
    public int da�o = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Character ch = collision.GetComponent<Character>();
            if (ch != null) ch.RestarVida(da�o);
        }
    }
}
