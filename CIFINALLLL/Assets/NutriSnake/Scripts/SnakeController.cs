using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

// Controla o movimento da cobra numa grelha (estilo Snake clássico).
// Usa as setas do teclado (ou WASD) para mudar de direção.
public class SnakeController : MonoBehaviour
{
    [Header("Grade")]
    public int columns = 20;
    public int rows = 11;
    public float cellSize = 80f;

    [Header("Velocidade")]
    public float moveInterval = 0.2f;

    [Header("Referências")]
    public RectTransform segmentPrefab;
    public RectTransform boardRoot;
    public SnakeFoodSpawner spawner;

    private List<Vector2Int> segments = new List<Vector2Int>();
    private List<RectTransform> segmentViews = new List<RectTransform>();
    private Vector2Int direction = Vector2Int.right;
    private Vector2Int pendingDirection = Vector2Int.right;
    private float timer = 0f;
    private bool alive = true;

    void Start()
    {
        segments.Clear();
        int startX = columns / 2;
        int startY = rows / 2;
        segments.Add(new Vector2Int(startX - 1, startY));
        segments.Add(new Vector2Int(startX - 2, startY));
        segments.Add(new Vector2Int(startX - 3, startY));
        direction = Vector2Int.right;
        pendingDirection = Vector2Int.right;

        for (int i = 0; i < segments.Count; i++)
        {
            CreateSegmentView();
        }
        UpdateViews();
    }

    void CreateSegmentView()
    {
        if (segmentPrefab == null || boardRoot == null) return;
        RectTransform view = Instantiate(segmentPrefab, boardRoot);
        view.gameObject.SetActive(true);
        segmentViews.Add(view);
    }

    void Update()
    {
        if (!alive) return;

        ReadInput();

        timer += Time.deltaTime;
        if (timer >= moveInterval)
        {
            timer = 0f;
            Step();
        }
    }

    void ReadInput()
    {
        if (Keyboard.current == null) return;

        if ((Keyboard.current.upArrowKey.wasPressedThisFrame || Keyboard.current.wKey.wasPressedThisFrame) && direction != Vector2Int.down)
        {
            pendingDirection = Vector2Int.up;
        }
        else if ((Keyboard.current.downArrowKey.wasPressedThisFrame || Keyboard.current.sKey.wasPressedThisFrame) && direction != Vector2Int.up)
        {
            pendingDirection = Vector2Int.down;
        }
        else if ((Keyboard.current.leftArrowKey.wasPressedThisFrame || Keyboard.current.aKey.wasPressedThisFrame) && direction != Vector2Int.right)
        {
            pendingDirection = Vector2Int.left;
        }
        else if ((Keyboard.current.rightArrowKey.wasPressedThisFrame || Keyboard.current.dKey.wasPressedThisFrame) && direction != Vector2Int.left)
        {
            pendingDirection = Vector2Int.right;
        }
    }

    void Step()
    {
        direction = pendingDirection;
        Vector2Int newHead = segments[0] + direction;

        // Colisão com a parede
        if (newHead.x < 0 || newHead.x >= columns || newHead.y < 0 || newHead.y >= rows)
        {
            GameOver();
            return;
        }

        // Colisão com o próprio corpo
        if (segments.Contains(newHead))
        {
            GameOver();
            return;
        }

        bool grow = false;

        if (spawner != null)
        {
            bool isHealthy;
            if (spawner.TryEat(newHead, out isHealthy))
            {
                Vector3 worldPos = CellToWorldPos(newHead);
                if (SnakeGameManager.instance != null)
                {
                    SnakeGameManager.instance.OnFoodEaten(isHealthy, worldPos);
                }

                if (isHealthy)
                {
                    grow = true;
                }
                else
                {
                    GameOver();
                    return;
                }
            }
        }

        segments.Insert(0, newHead);

        if (grow)
        {
            CreateSegmentView();
        }
        else
        {
            segments.RemoveAt(segments.Count - 1);
        }

        UpdateViews();
    }

    void UpdateViews()
    {
        for (int i = 0; i < segments.Count; i++)
        {
            if (i < segmentViews.Count)
            {
                segmentViews[i].anchoredPosition = CellToAnchoredPos(segments[i]);
                segmentViews[i].gameObject.SetActive(true);
            }
        }
    }

    public Vector2 CellToAnchoredPos(Vector2Int cell)
    {
        float originX = -(columns - 1) * cellSize / 2f;
        float originY = -(rows - 1) * cellSize / 2f;
        return new Vector2(originX + cell.x * cellSize, originY + cell.y * cellSize);
    }

    public Vector3 CellToWorldPos(Vector2Int cell)
    {
        if (boardRoot == null) return Vector3.zero;
        Vector2 local = CellToAnchoredPos(cell);
        return boardRoot.TransformPoint(new Vector3(local.x, local.y, 0f));
    }

    public bool IsCellOccupied(Vector2Int cell)
    {
        return segments.Contains(cell);
    }

    void GameOver()
    {
        if (!alive) return;
        alive = false;
        if (SnakeGameManager.instance != null)
        {
            SnakeGameManager.instance.FinishGame();
        }
    }
}
