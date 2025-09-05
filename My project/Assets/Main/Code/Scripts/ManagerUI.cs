using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Referencias TextMeshPro")]
    [SerializeField] private TextMeshProUGUI textoTiempo;
    [SerializeField] private TextMeshProUGUI textoVida;
    [SerializeField] private TextMeshProUGUI textoPuntos;
    [SerializeField] private TextMeshProUGUI textoLlave;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    private void Update()
    {
        // Tiempo
        textoTiempo.text = "Tiempo: " + Mathf.CeilToInt(gameManager.GetTiempo());

        // Vida
        textoVida.text = "Vida: " + gameManager.GetVida();

        // Puntos
        textoPuntos.text = "Puntos: " + gameManager.GetPuntos();

        // Llave
        textoLlave.text = gameManager.TieneLlave() ? "Llave: ✔" : "Llave: ✘";
    }
}
