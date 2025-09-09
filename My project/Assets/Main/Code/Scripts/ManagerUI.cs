using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Referencias UI")]
    [SerializeField] private TMP_Text textoTiempo;
    [SerializeField] private TMP_Text textoVida;
    [SerializeField] private TMP_Text textoPuntos;
    [SerializeField] private TMP_Text textoLlave;

    private void Update()
    {
        if (GameManager.Instance == null) return;

        // Tiempo
        if (textoTiempo != null)
            textoTiempo.text = "Tiempo: " + Mathf.CeilToInt(GameManager.Instance.GetTiempo());

        // Vida
        if (textoVida != null)
            textoVida.text = "Vida: " + GameManager.Instance.GetVida();

        // Puntos
        if (textoPuntos != null)
            textoPuntos.text = "Puntos: " + GameManager.Instance.GetPuntos();

        // Llave
        if (textoLlave != null)
            textoLlave.text = GameManager.Instance.TieneLlave() ? "Llave:" : "Llave:";
    }
}
