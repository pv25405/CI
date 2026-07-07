using UnityEngine;

// Marca um item de comida colocado na grelha da cobra.
// isHealthy = true para alimentos saudáveis (maçã, banana, cenoura...)
// isHealthy = false para alimentos não saudáveis (hambúrguer...)
public class SnakeFood : MonoBehaviour
{
    public bool isHealthy;
    public Vector2Int cell;
}
