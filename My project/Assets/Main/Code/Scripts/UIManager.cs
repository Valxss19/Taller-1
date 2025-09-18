using UnityEngine;
using TMPro;
using UnityEngine.UI;
{

}

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text tituloText;
    [SerializeField]
    private TMP_Text alertasText;

    [SerializeField]
    private TMP_InputField respuestaInput;

    [SerializeField]
    private Button enviarButton;

    [SerializeField]
    private int edad;

    private void Start()
    {
        tituloText.text = "Hola, Introduce tu edad";
        alertasText.text = "";
        enviarButton.onClick.AddListener(FuncionDelBoton);

    }
    public void FuncionDelBoton()
    {
        int edad= int.Parse(respuestaInput.textComponent.text);


}
}
