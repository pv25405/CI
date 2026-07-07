using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TempoCronometroTMP : MonoBehaviour
{
    public float tempo = 30f;
    public TextMeshProUGUI textoTempo;
    public string cenaResultado;

    void Update()
    {
        tempo -= Time.deltaTime;

        if (tempo <= 0)
        {
            tempo = 0;
            IrParaResultado();
        }

        textoTempo.text = "TEMPO: " + Mathf.CeilToInt(tempo).ToString();
    }

    void IrParaResultado()
    {
        SceneManager.LoadScene(cenaResultado);
    }
}
