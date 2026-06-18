using UnityEngine;
using TMPro;

public class DisplayFinalScore : MonoBehaviour
{
    public TextMeshProUGUI endScoreText;

    void Start()
    {
        // Se encontramos o componente de texto, escrevemos a pontuação guardada no GameManager
        if (endScoreText != null)
        {
            endScoreText.text = "Pontuação Final: " + GameManager.finalScore + " pontos!";
        }
    }
}