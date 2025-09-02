using UnityEngine;

public class Trampa : MonoBehaviour
{
    public int da�o = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            LifeManager.instance.QuitarVida(da�o);
        }
    }
}
