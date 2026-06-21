using UnityEngine;
using System.Collections;

public class FadeInManager : MonoBehaviour
{
    public GameObject painelPreto; // Arrasta o painel preto para aqui

    void Start()
    {
        // Espera 0.6 segundos (o tempo da animação acabar) e desativa o painel completamente
        StartCoroutine(DesativarPaineldepoisDeTempo());
    }

    IEnumerator DesativarPaineldepoisDeTempo()
    {
        yield return new WaitForSeconds(0.6f);
        if (painelPreto != null)
        {
            painelPreto.SetActive(false); // Desativa o painel para não bloquear o rato!
        }
    }
}