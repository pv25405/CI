using UnityEngine;
using UnityEngine.InputSystem; 

public class PlayerController : MonoBehaviour
{
    public float speed = 500f; 
    
    [Header("Limites do Mapa")]
    // Este valor define a distância máxima que ele pode ir para a esquerda e direita
    public float screenLimit = 400f; 

    void Update()
    {
        float horizontalInput = 0f;

        // Lê os controlos (Novo Sistema)
        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
            {
                horizontalInput = -1f;
            }
            else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
            {
                horizontalInput = 1f;
            }
        }

        // Move o boneco
        Vector3 direction = new Vector3(horizontalInput, 0f, 0f);
        transform.Translate(direction * speed * Time.deltaTime);

        // --- SISTEMA DE LIMITES ---
        // Obtém a posição atual do boneco
        Vector3 currentPosition = transform.localPosition;

        // Limita a posição X entre o valor negativo e positivo do teu limite
        currentPosition.x = Mathf.Clamp(currentPosition.x, -screenLimit, screenLimit);

        // Aplica a posição limitada de volta ao boneco
        transform.localPosition = currentPosition;
        // --------------------------

        // Vira o boneco para a esquerda ou direita
        if (horizontalInput > 0.1f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (horizontalInput < -0.1f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}