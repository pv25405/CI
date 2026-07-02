using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement; // <--- ESTA LINHA TEM DE ESTAR AQUI!

public class QuizManager : MonoBehaviour
{
    [System.Serializable]
    public class Pergunta 
    {
        public string pergunta;
        [Tooltip("Escreve exatamente 3 opções para condizer com os teus 3 botões.")]
        public string[] opcoes = new string[3];
        [Tooltip("Índice da resposta certa: 0 para o botão da Esquerda, 1 para o do Meio, 2 para o da Direita.")]
        public int resposta_correta;
    }

    [System.Serializable]
    public class Pagina 
    {
        public string titulo;
        public List<Pergunta> perguntas;
    }

    [Header("Configuração do Quiz (Preencher no Inspector)")]
    public List<Pagina> paginas;

    [Header("Elementos de Interface (UI)")]
    public TextMeshProUGUI txtTituloPagina; 
    public TextMeshProUGUI txtTextoPergunta;
    public Button[] botoesOpcoes; // Mantém os teus 3 botões aqui

    private int paginaAtual = 0;
    private int perguntaNaPaginaAtual = 0;

    void Start()
    {
        // Garante que o jogo não crasha se te esqueceres de preencher as perguntas
        if (paginas == null || paginas.Count == 0 || paginas[0].perguntas.Count == 0)
        {
            Debug.LogError("Por favor, cria as páginas e perguntas diretamente no Inspector do script!");
            return;
        }

        ConfigurarBotoes();
        MostrarPergunta();
    }

    void ConfigurarBotoes()
    {
        for (int i = 0; i < botoesOpcoes.Length; i++)
        {
            int indiceBotao = i; 
            botoesOpcoes[i].onClick.AddListener(() => CarregarNoBotaoResposta(indiceBotao));
        }
    }

    void MostrarPergunta()
    {
        // Se passarmos da última página, o quiz termina
        if (paginaAtual >= paginas.Count)
        {
            TerminarQuiz();
            return;
        }

        Pagina pagina = paginas[paginaAtual];
        Pergunta pergunta = pagina.perguntas[perguntaNaPaginaAtual];

        // Atualiza os textos do Quadro
        if (txtTituloPagina != null) txtTituloPagina.text = pagina.titulo;
        if (txtTextoPergunta != null) txtTextoPergunta.text = pergunta.pergunta;

        // Distribui os textos pelos teus 3 botões
        for (int i = 0; i < botoesOpcoes.Length; i++)
        {
            TextMeshProUGUI txtBotao = botoesOpcoes[i].GetComponentInChildren<TextMeshProUGUI>();
            if (txtBotao != null && i < pergunta.opcoes.Length)
            {
                txtBotao.text = pergunta.opcoes[i];
            }
        }
    }
public void CarregarNoBotaoResposta(int indiceSelecionado)
    {
        Pergunta perguntaAtualObj = paginas[paginaAtual].perguntas[perguntaNaPaginaAtual];

        if (indiceSelecionado == perguntaAtualObj.resposta_correta)
        {
            Debug.Log("Resposta Certa! A mudar de cena...");
            SceneManager.LoadScene("Escola Quiz 1"); // Salta logo para a cena
        }
        else
        {
            Debug.Log("Resposta Errada! Tenta outra vez.");
        }
    }

    void AvancarNoQuiz()
    {
        perguntaNaPaginaAtual++;

        // Se terminar as perguntas desta página, avança para a página seguinte
        if (perguntaNaPaginaAtual >= paginas[paginaAtual].perguntas.Count)
        {
            perguntaNaPaginaAtual = 0;
            paginaAtual++;
        }

        MostrarPergunta();
    }

    void TerminarQuiz()
    {
        if (txtTituloPagina != null) txtTituloPagina.text = "Parabéns!";
        if (txtTextoPergunta != null) txtTextoPergunta.text = "Completaste o quiz de alimentação saudável com sucesso!";
        
        foreach (Button b in botoesOpcoes)
        {
            b.gameObject.SetActive(false);
        }
    }
}