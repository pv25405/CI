using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ResultadoNivel : MonoBehaviour
{
    public TextMeshProUGUI textoAcertos;
    public TextMeshProUGUI textoErros;
    public string proximaCena;

    void Start()
    {
        textoAcertos.text = "Acertos: " + ScoreManager.acertos;
        textoErros.text = "Erros: " + ScoreManager.erros;

        ScoreManager.Resetar();
    }

    public void IrParaProximaCena()
    {
        SceneManager.LoadScene(proximaCena);
    }
}
