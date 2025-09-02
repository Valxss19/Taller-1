using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public float tiempoRestante = 60f;
    [SerializeField]
    private TMP_Text textoTiempo;

   
    void Update()
    {

        tiempoRestante -= Time.deltaTime;


        if (tiempoRestante < 0)
            tiempoRestante = 0;

        if (textoTiempo != null)
            textoTiempo.text = "Tiempo: " + Mathf.Ceil(tiempoRestante). ToString();

        
    }
   

    public void SumarTiempo (float segundos)
    {
        tiempoRestante += segundos;
    }
}
