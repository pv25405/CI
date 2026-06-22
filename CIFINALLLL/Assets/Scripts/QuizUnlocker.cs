using UnityEngine;
using UnityEngine.UI; // Obrigatório para interagir com os Botões

public class QuizUnlocker : MonoBehaviour
{
    [Header("Botão do Quiz")]
    public Button botaoQuiz; // Arrasta o teu botão QUIZ para aqui

    // Variáveis para guardar se o jogador já clicou em cada aba
    private bool clicouAlimentacaoSaudavel = false;
    private bool clicouRodaAlimentos = false;
    private bool clicouMaAlimentacao = false;

    void Start()
    {
        // Começa o jogo com o botão do Quiz bloqueado (não clicável)
        if (botaoQuiz != null)
        {
            botaoQuiz.interactable = false;
        }
    }

    // Funções que os teus botões vão chamar ao serem clicados:
    
    public void ClicouEmAlimentacaoSaudavel()
    {
        clicouAlimentacaoSaudavel = true;
        VerificarDesbloqueio();
    }

    public void ClicouEmRodaAlimentos()
    {
        clicouRodaAlimentos = true;
        VerificarDesbloqueio();
    }

    public void ClicouEmMaAlimentacao()
    {
        clicouMaAlimentacao = true;
        VerificarDesbloqueio();
    }

    // Função que valida se as 3 condições foram cumpridas
    private void VerificarDesbloqueio()
    {
        if (clicouAlimentacaoSaudavel && clicouRodaAlimentos && clicouMaAlimentacao)
        {
            if (botaoQuiz != null)
            {
                botaoQuiz.interactable = true; // Desbloqueia o botão do Quiz!
            }
        }
    }
}