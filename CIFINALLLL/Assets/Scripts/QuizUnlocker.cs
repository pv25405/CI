using UnityEngine;
using UnityEngine.UI;

public class QuizUnlocker : MonoBehaviour
{
    [Header("Botão do Quiz")]
    public Button botaoQuiz; 

    void Start()
    {
        // Ao iniciar a cena, verifica se o Quiz já deve estar desbloqueado
        VerificarDesbloqueio();
    }

    // Estas funções guardam na memória permanente que o botão foi clicado (1 = Clicado)
    public void ClicouEmAlimentacaoSaudavel()
    {
        PlayerPrefs.SetInt("ClicouAlimentacao", 1);
        PlayerPrefs.Save();
        VerificarDesbloqueio();
    }

    public void ClicouEmRodaAlimentos()
    {
        PlayerPrefs.SetInt("ClicouRoda", 1);
        PlayerPrefs.Save();
        VerificarDesbloqueio();
    }

    public void ClicouEmMaAlimentacao()
    {
        PlayerPrefs.SetInt("ClicouMaAlimentacao", 1);
        PlayerPrefs.Save();
        VerificarDesbloqueio();
    }

    private void VerificarDesbloqueio()
    {
        // Vai buscar à memória o estado de cada botão (se não encontrar, assume 0)
        int clicouAlimentacao = PlayerPrefs.GetInt("ClicouAlimentacao", 0);
        int clicouRoda = PlayerPrefs.GetInt("ClicouRoda", 0);
        int clicouMaAlimentacao = PlayerPrefs.GetInt("ClicouMaAlimentacao", 0);

        // Se os três tiverem o valor 1, significa que o jogador já passou por todos!
        if (clicouAlimentacao == 1 && clicouRoda == 1 && clicouMaAlimentacao == 1)
        {
            if (botaoQuiz != null)
            {
                botaoQuiz.interactable = true; // Desbloqueia!
            }
        }
        else
        {
            if (botaoQuiz != null)
            {
                botaoQuiz.interactable = false; // Continua trancado
            }
        }
    }

    // FUNÇÃO BÓNUS: Se quiseres resetar o jogo mais tarde, podes chamar isto para trancar o quiz outra vez
    public void ResetarProgresso()
    {
        PlayerPrefs.DeleteKey("ClicouAlimentacao");
        PlayerPrefs.DeleteKey("ClicouRoda");
        PlayerPrefs.DeleteKey("ClicouMaAlimentacao");
        VerificarDesbloqueio();
    }
}