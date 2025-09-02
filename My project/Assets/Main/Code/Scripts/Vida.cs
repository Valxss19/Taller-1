using UnityEngine;

public class OrangeChicken : MonoBehaviour
{
    public int vidaExtra = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Character ch = collision.GetComponent<Character>();
            if (ch != null) ch.SumarVida(vidaExtra);
            Destroy(gameObject);
        }
    }
}
