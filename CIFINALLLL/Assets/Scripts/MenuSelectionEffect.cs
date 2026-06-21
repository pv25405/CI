using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuSelectionEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Alvos da Interação")]
    public RectTransform meuBotao;      // O botão onde o rato entra
    public RectTransform outroBotao;    // O botão rival que vai encolher

    [Header("Definições de Tamanho")]
    public float scaleAumentar = 1.1f;  // Aumenta 10%
    public float scaleDiminuir = 0.85f; // Encolhe 15%

    private Vector3 scaleOriginal = Vector3.one;

    void Start()
    {
        // Se não arrastares o teu próprio botão, ele tenta assumir o objeto atual
        if (meuBotao == null) meuBotao = GetComponent<RectTransform>();
    }

    // Quando o rato entra neste botão
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (meuBotao != null) meuBotao.localScale = scaleOriginal * scaleAumentar;
        if (outroBotao != null) outroBotao.localScale = scaleOriginal * scaleDiminuir;
    }

    // Quando o rato sai deste botão
    public void OnPointerExit(PointerEventData eventData)
    {
        ResetPrecos();
    }

    void OnDisable()
    {
        ResetPrecos();
    }

    // Devolve ambos os botões ao tamanho original (1, 1, 1)
    void ResetPrecos()
    {
        if (meuBotao != null) meuBotao.localScale = scaleOriginal;
        if (outroBotao != null) outroBotao.localScale = scaleOriginal;
    }
}