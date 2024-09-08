using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int gridWidth = 10;
    public int gridHeight = 10;

    // Inicializa la cuadrícula con los parámetros necesarios
    public void InitializeGrid()
    {
        // Aquí puedes implementar la lógica para inicializar tu cuadrícula
        Debug.Log($"Grid initialized with width: {gridWidth} and height: {gridHeight}");
    }
}
