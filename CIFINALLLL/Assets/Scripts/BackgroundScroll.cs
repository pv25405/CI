using UnityEngine;

public class InfiniteBackground : MonoBehaviour
{
    public RectTransform ceu1;
    public RectTransform ceu2;
    public float velocidade = 50f; // Velocidade em píxeis por segundo

    private float larguraEcrã;

    void Start()
    {
        // Assume que as duas imagens têm o tamanho total do ecrã
        larguraEcrã = ceu1.rect.width;
        
        // Coloca o céu 2 colado logo à direita do céu 1
        ceu2.anchoredPosition = new Vector2(larguraEcrã, ceu1.anchoredPosition.y);
    }

    void Update()
    {
        // Move ambos os céus para a esquerda
        ceu1.anchoredPosition += Vector2.left * velocidade * Time.deltaTime;
        ceu2.anchoredPosition += Vector2.left * velocidade * Time.deltaTime;

        // Se o céu 1 sair todo da tela, vai para trás do céu 2
        if (ceu1.anchoredPosition.x <= -larguraEcrã)
        {
            ceu1.anchoredPosition = new Vector2(ceu2.anchoredPosition.x + larguraEcrã, ceu1.anchoredPosition.y);
        }

        // Se o céu 2 sair todo da tela, vai para trás do céu 1
        if (ceu2.anchoredPosition.x <= -larguraEcrã)
        {
            ceu2.anchoredPosition = new Vector2(ceu1.anchoredPosition.x + larguraEcrã, ceu2.anchoredPosition.y);
        }
    }
}