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
    public GameObject floatingTextPrefab; 

    [Header("Efeitos Sonoros (SFX)")]
    public AudioClip soundGood; // Som para comida saudável
    public AudioClip soundBad;  // Som para comida má
    private AudioSource audioSource; // O componente que vai reproduzir o som

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        // Obtém ou adiciona automaticamente o componente de áudio no GameManager
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
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
            textColor = Color.green;
            textPrefix = "+";

            // Toca o som bom!
            PlaySound(soundGood);
        }
        else
        {
            score -= 5;
            if (score < 0) score = 0;
            pointsChanged = 5;
            textColor = Color.red;
            textPrefix = "-";

            // Toca o som mau!
            PlaySound(soundBad);
        }

        UpdateScoreText();
        CreateFloatingText(textPrefix + pointsChanged + " PONTOS", spawnPosition, textColor);
    }

    // Função interna para dar o "Play" no som sem cortar o anterior
    void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    void CreateFloatingText(string text, Vector3 position, Color color)
    {
        if (floatingTextPrefab == null) return;

        Canvas canvas = Object.FindFirstObjectByType<Canvas>();
        if (canvas == null) return;

        GameObject textObj = Instantiate(floatingTextPrefab, canvas.transform);
        textObj.transform.position = position;
        textObj.transform.localScale = Vector3.one;

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