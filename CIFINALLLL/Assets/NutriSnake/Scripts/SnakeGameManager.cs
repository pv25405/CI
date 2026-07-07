using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

// Gere a pontuação e o fim de jogo do NutriSnake.
// Reutiliza o campo estático GameManager.finalScore para mostrar o resultado
// no ecrã de Score (tal como o jogo Caça-Nutrientes), sem alterar esse script.
public class SnakeGameManager : MonoBehaviour
{
    public static SnakeGameManager instance;

    [Header("Pontuação")]
    public int score = 0;
    public TextMeshProUGUI scoreText;

    [Header("Efeito de Pontos Flutuantes")]
    public GameObject floatingTextPrefab;

    [Header("Efeitos Sonoros (SFX)")]
    public AudioClip soundGood;
    public AudioClip soundBad;
    private AudioSource audioSource;

    [Header("Cena Seguinte")]
    public string scoreSceneName = "NUTRISNAKE Score";

    private bool isGameOver = false;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

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
    }

    public void OnFoodEaten(bool isHealthy, Vector3 worldPosition)
    {
        if (isGameOver) return;

        if (isHealthy)
        {
            score += 10;
            UpdateScoreText();
            CreateFloatingText("+10 PONTOS", worldPosition, Color.green);
            PlaySound(soundGood);
        }
        else
        {
            CreateFloatingText("ALIMENTO PROIBIDO!", worldPosition, Color.red);
            PlaySound(soundBad);
        }
    }

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
        if (scoreText != null) scoreText.text = "SCORE " + score;
    }

    public void FinishGame()
    {
        if (isGameOver) return;
        isGameOver = true;
        GameManager.finalScore = score;
        SceneManager.LoadScene(scoreSceneName);
    }
}
