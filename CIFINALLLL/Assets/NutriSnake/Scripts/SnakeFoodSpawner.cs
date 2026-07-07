using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Coloca comida saudável e não saudável em células livres da grelha da cobra.
public class SnakeFoodSpawner : MonoBehaviour
{
    [Header("Referências")]
    public SnakeController snake;
    public RectTransform boardRoot;

    [Header("Prefabs Saudáveis")]
    public List<RectTransform> healthyPrefabs;

    [Header("Prefabs Não Saudáveis")]
    public List<RectTransform> unhealthyPrefabs;

    [Header("Configurações")]
    public int simultaneousFood = 3;
    [Range(0f, 1f)]
    public float healthyChance = 0.65f;
    public float respawnDelay = 0.4f;

    private List<SnakeFood> active = new List<SnakeFood>();

    void Start()
    {
        for (int i = 0; i < simultaneousFood; i++)
        {
            SpawnOne();
        }
    }

    public bool TryEat(Vector2Int cell, out bool isHealthy)
    {
        for (int i = 0; i < active.Count; i++)
        {
            if (active[i] != null && active[i].cell == cell)
            {
                isHealthy = active[i].isHealthy;
                Destroy(active[i].gameObject);
                active.RemoveAt(i);
                StartCoroutine(RespawnAfterDelay());
                return true;
            }
        }

        isHealthy = false;
        return false;
    }

    IEnumerator RespawnAfterDelay()
    {
        yield return new WaitForSeconds(respawnDelay);
        SpawnOne();
    }

    void SpawnOne()
    {
        if (snake == null || boardRoot == null) return;

        Vector2Int cell = FindFreeCell();
        if (cell.x < 0) return;

        bool healthy = Random.value <= healthyChance;
        RectTransform prefab = PickPrefab(healthy);
        if (prefab == null) return;

        RectTransform view = Instantiate(prefab, boardRoot);
        view.anchoredPosition = snake.CellToAnchoredPos(cell);
        view.gameObject.SetActive(true);

        SnakeFood food = view.GetComponent<SnakeFood>();
        if (food == null)
        {
            food = view.gameObject.AddComponent<SnakeFood>();
        }
        food.isHealthy = healthy;
        food.cell = cell;

        active.Add(food);
    }

    RectTransform PickPrefab(bool healthy)
    {
        List<RectTransform> list = healthy ? healthyPrefabs : unhealthyPrefabs;
        if (list == null || list.Count == 0) return null;
        return list[Random.Range(0, list.Count)];
    }

    Vector2Int FindFreeCell()
    {
        for (int attempt = 0; attempt < 100; attempt++)
        {
            Vector2Int cell = new Vector2Int(Random.Range(0, snake.columns), Random.Range(0, snake.rows));
            if (snake.IsCellOccupied(cell)) continue;

            bool occupiedByFood = false;
            for (int i = 0; i < active.Count; i++)
            {
                if (active[i] != null && active[i].cell == cell) { occupiedByFood = true; break; }
            }
            if (occupiedByFood) continue;

            return cell;
        }
        return new Vector2Int(-1, -1);
    }
}
