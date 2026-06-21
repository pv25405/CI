using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // Necessário para detetar o rato

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Image buttonImage;
    private Vector3 originalScale;
    private Color originalColor;

    [Header("Configurações do Rato Por Cima")]
    public float scaleMultiplier = 1.05f; // Aumenta 5%
    public Color hoverColor = new Color(0.75f, 0.75f, 0.75f, 1f); // Fica mais escuro

    void Awake()
    {
        buttonImage = GetComponent<Image>();
        originalScale = transform.localScale;
        
        if (buttonImage != null)
        {
            originalColor = buttonImage.color;
        }
    }

    // Quando o rato entra no botão
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = originalScale * scaleMultiplier;
        if (buttonImage != null) buttonImage.color = hoverColor;
    }

    // Quando o rato sai do botão
    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = originalScale;
        if (buttonImage != null) buttonImage.color = originalColor;
    }

    void OnDisable()
    {
        // Garante que o botão volta ao normal se a cena mudar ou sumir
        transform.localScale = originalScale;
        if (buttonImage != null) buttonImage.color = originalColor;
    }
}