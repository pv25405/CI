using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    public float moveSpeed = 150f; // Velocidade com que o texto sobe
    public float duration = 0.8f;  // Quanto tempo dura no ecrã antes de sumir

    void Start()
    {
        // Destrói automaticamente o texto após a duração definida
        Destroy(gameObject, duration);
    }

    void Update()
    {
        // Faz o texto subir em linha reta na UI do Canvas
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
    }
}