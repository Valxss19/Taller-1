using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb2d;
    [SerializeField]
    private Vector2 direccion;
    [SerializeField]
    private float fuerza;

    private int puntos = 0;  

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            rb2d.AddForce(direccion * fuerza);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            Vector2 direccionInvertida = new Vector2(-direccion.x, direccion.y);
            rb2d.AddForce(direccionInvertida * fuerza);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hueso"))
        {
            puntos += 1;
            Destroy(other.gameObject);
            Debug.Log("Hueso: " + puntos);
        }
    }
}
