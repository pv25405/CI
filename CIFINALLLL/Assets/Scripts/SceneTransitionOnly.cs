using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransitionOnly : MonoBehaviour
{
    [Header("Componentes da Transição")]
    public GameObject painelTransicao; // Arrastas o PainelTransicao para aqui
    public Animator transicaoAnimator;   // Arrastas o PainelTransicao para aqui também

    // Função que a Escola ou a Cidade vão chamar
    public void MudarCenaComFade(string nomeDaCena)
    {
        StartCoroutine(ExecutarFade(nomeDaCena));
    }

    IEnumerator ExecutarFade(string nomeDaCena)
    {
        if (painelTransicao != null && transicaoAnimator != null)
        {
            painelTransicao.SetActive(true); // Ativa o painel preto
            transicaoAnimator.Play("FadeOut"); // Toca o Fade Out
            yield return new WaitForSeconds(0.5f); // Espera a animação acabar
        }

        SceneManager.LoadScene(nomeDaCena); // Muda de cena com o ecrã preto
    }
}