using UnityEngine;

public class FoodItem : MonoBehaviour
{
    [Header("Configurações do Alimento")]
    public float fallSpeed = 200f; // Velocidade de queda (ajustada para a UI do Canvas)
    
    // MARCA esta caixa no Inspector se for saudável (Banana/Brócolo)
    // DESMARCA se for comida que faz mal (Piza/Donut)
    public bool isHealthy; 

    void Update()
    {
        // Faz o item cair em linha reta (Eixo Y negativo)
        Vector3 movement = new Vector3(0f, -1f, 0f) * fallSpeed * Time.deltaTime;
        transform.Translate(movement);

        // Se o item passar do fundo do ecrã preto, é destruído para não pesar no jogo
        // Se o teu ecrã for muito comprido e eles sumirem antes do fim, aumenta o -500f para -600f
        if (transform.localPosition.y < -680f) 
        {
            Destroy(gameObject);
        }
    }

    // Deteta a colisão com o herói (Player)
   void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.instance != null)
            {
                // Passamos a posição atual do item (transform.position) para o GameManager saber onde criar o texto!
                GameManager.instance.OnItemCaught(isHealthy, transform.position);
            }
            Destroy(gameObject);
        }
    }
}