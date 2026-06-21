using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class AutoLoadScene : MonoBehaviour
{
    [Header("Configuração do Tempo")]
    [Tooltip("Tempo em segundos que o ecrã de tutorial vai ficar visível antes do jogo começar.")]
    public float tempoDeEspera = 4f; 

    [Header("Nome da Cena Alvo")]
    [Tooltip("Escreve exatamente o nome da cena do jogo correspondente a esta dificuldade.")]
    public string nomeDaCenaDoJogo;

    void Start()
    {
        // Inicia a contagem decrescente assim que a cena abre
        StartCoroutine(ContagemRegressiva());
    }

    IEnumerator ContagemRegressiva()
    {
        // Espera os segundos que definiste
        yield return new WaitForSeconds(tempoDeEspera);

        // Se tiveres criado o sistema de Fade Out nesta cena, podes tocar a animação aqui.
        // Caso contrário, ele salta diretamente para o jogo de forma limpa:
        if (!string.IsNullOrEmpty(nomeDaCenaDoJogo))
        {
            SceneManager.LoadScene(nomeDaCenaDoJogo);
        }
        else
        {
            Debug.LogError("Esqueceste-te de escrever o nome da cena no script!");
        }
    }
}