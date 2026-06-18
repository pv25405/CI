using UnityEngine;
using System.Collections.Generic; // IMPORTANTE: Sem isto, o List<> dá erro!

public class ItemSpawner : MonoBehaviour
{
    [Header("Lista de Comidas (Prefabs)")]
    public List<GameObject> foodPrefabs; 

    [Header("Configurações de Tempo")]
    public float spawnInterval = 1.5f; 
    private float timer;

    [Header("Limites de Spawn (Eixo X)")]
    public float xLimit = 350f; 

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnFood();
            timer = 0f; 
        }
    }

    void SpawnFood()
    {
        if (foodPrefabs == null || foodPrefabs.Count == 0) return;

        int randomIndex = Random.Range(0, foodPrefabs.Count);
        GameObject selectedFood = foodPrefabs[randomIndex];

        float randomX = Random.Range(-xLimit, xLimit);
        Vector3 spawnPosition = new Vector3(randomX, transform.localPosition.y, 0f);

        GameObject newFood = Instantiate(selectedFood, transform);
        newFood.transform.localPosition = spawnPosition;
    }
}