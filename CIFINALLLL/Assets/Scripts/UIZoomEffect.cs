using UnityEngine;
using System.Collections;

public class UIZoomEffect : MonoBehaviour
{
    [Header("Configurações do Zoom")]
    public float velocidadeZoom = 5f;    // Velocidade da aproximação
    public float tamanhoMaximoZoom = 3f; // Quantas vezes o botão vai esticar

    private RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Função que inicia o efeito de crescer
    public void AtivarZoom()
    {
        StartCoroutine(ExecutarZoom());
    }

    IEnumerator ExecutarZoom()
    {
        float tempo = 0f;
        Vector3 escalaIncial = rectTransform.localScale;
        Vector3 escalaFinal = Vector3.one * tamanhoMaximoZoom;

        while (tempo < 0.5f)
        {
            tempo += Time.deltaTime;
            rectTransform.localScale = Vector3.Lerp(escalaIncial, escalaFinal, tempo * velocidadeZoom);
            yield return null;
        }

        rectTransform.localScale = escalaFinal;
    }
}