using UnityEngine;
using TMPro; 
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; 
    public static int finalScore = 0; 

    [Header("Configurações de Pontos")]
    public int score = 0;
    public TextMeshProUGUI scoreText; 

    [Header("Configurações de Tempo")]
    public float timeRemaining = 20f; 
    public TextMeshProUGUI timerText; 
    private bool isGameOver = false;

    [Header("Efeito de Pontos Flutuantes")]
    public GameObject floatingTextPrefab; // Arrastas o teu Prefab azul para aqui

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        score = 0;
        UpdateScoreText();
        UpdateTimerText();
    }

    void Update()
    {
        if (isGameOver) return;

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerText();
        }
        else
        {
            timeRemaining = 0;
            UpdateTimerText();
            FinishGame(); 
        }
    }

    // Atualizámos esta função para aceitar a posição onde a comida foi apanhada!
    public void OnItemCaught(bool isHealthy, Vector3 spawnPosition)
    {
        if (isGameOver) return; 

        int pointsChanged = 0;
        Color textColor;
        string textPrefix = "";

        if (isHealthy)
        {
            score += 10;
            pointsChanged = 10;
            textColor = Color.green; // Verde para saudável
            textPrefix = "+";
        }
        else
        {
            score -= 5;
            if (score < 0) score = 0;
            pointsChanged = 5;
            textColor = Color.red; // Vermelho para não saudável
            textPrefix = "-";
        }

        UpdateScoreText();
        
        // Cria o texto de efeito no ecrã
        CreateFloatingText(textPrefix + pointsChanged + " PONTOS", spawnPosition, textColor);
    }

    void CreateFloatingText(string text, Vector3 position, Color color)
    {
        if (floatingTextPrefab == null) return;

        // Cria o texto dentro do Canvas como filho do GameManager ou da cena
        GameObject textObj = Instantiate(floatingTextPrefab, transform.parent);
        textObj.transform.position = position;

        // Modifica o texto e a cor do componente TextMeshPro
        TextMeshProUGUI tmp = textObj.GetComponent<TextMeshProUGUI>();
        if (tmp != null)
        {
            tmp.text = text;
            tmp.color = color;
        }
    }

    void UpdateScoreText()
    {
        if (scoreText != null) scoreText.text = "PONTOS: " + score;
    }

    void UpdateTimerText()
    {
        if (timerText != null)
        {
            timerText.text = "TEMPO: " + Mathf.CeilToInt(timeRemaining).ToString();
        }
    }

    void FinishGame()
    {
        isGameOver = true;
        finalScore = score; 
        SceneManager.LoadScene("CAÇA-NUTRIENTES Score");
    }
}