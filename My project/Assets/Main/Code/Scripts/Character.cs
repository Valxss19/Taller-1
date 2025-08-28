using UnityEngine;

public class Character : MonoBehaviour
{


    [SerializeField]
    private Rigidbody2D rb2d;
    [SerializeField]
    private Vector2 direccion;
    [SerializeField]
    private float fuerza;
    [SerializeField]
    private Vector2 direccion2;
    [SerializeField]
    private float fuerza2;



    private void Awake()
    {

        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKey(KeyCode.D))
        {

            rb2d.AddForce(direccion * fuerza);

        }
        else if (Input.GetKey(KeyCode.A))
        {

            rb2d. AddForce(-direccion2 * -fuerza2);
        }
    }

}
        

