using UnityEngine;

public class ItemTiempo : MonoBehaviour
{
    public float segundosExtra = 5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            TimeManager tm = FindObjectOfType<TimeManager>();

            if (tm != null)

                tm.SumarTiempo(segundosExtra);

        }

        Destroy(gameObject);
    }
    }