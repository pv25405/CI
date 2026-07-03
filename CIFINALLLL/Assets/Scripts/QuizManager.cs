using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    [System.Serializable]
    public class Pergunta
    {
        public string pergunta;
        public string[] opcoes = new string[3];
        public int resposta_correta; // 0, 1, 2
    }

    [Header("Perguntas do Quiz")]
    public List<Pergunta> perguntas;

    [Header("UI")]
    public TextMeshProUGUI txtPergunta;
    public Button[] botoes;

    private int perguntaAtual = 0;

    void Start()
    {
        ConfigurarBotoes();
        MostrarPergunta();
    }

    void ConfigurarBotoes()
    {
        for (int i = 0; i < botoes.Length; i++)
        {
            int indice = i;
            botoes[i].onClick.AddListener(() => EscolherResposta(indice));
        }
    }

    void MostrarPergunta()
    {
        Pergunta p = perguntas[perguntaAtual];

        txtPergunta.text = p.pergunta;

        for (int i = 0; i < botoes.Length; i++)
        {
            TextMeshProUGUI txt = botoes[i].GetComponentInChildren<TextMeshProUGUI>();
            txt.text = p.opcoes[i];
        }
    }

    void EscolherResposta(int indice)
    {
        Pergunta p = perguntas[perguntaAtual];

        if (indice == p.resposta_correta)
        {
            Debug.Log("Resposta certa!");

            perguntaAtual++;

            if (perguntaAtual >= perguntas.Count)
            {
                Debug.Log("Quiz terminado! A mudar de cena...");
                SceneManager.LoadScene("EscolaQ2"); // <-- TROCA PARA A CENA QUE QUERES
                return;
            }

            MostrarPergunta();
        }
        else
        {
            Debug.Log("Resposta errada!");
        }
    }
}
