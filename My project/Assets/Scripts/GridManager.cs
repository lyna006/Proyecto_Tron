using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int gridWidth = 10;
    public int gridHeight = 10;

    // Inicializa la cuadr�cula con los par�metros necesarios
    public void InitializeGrid()
    {
        // Aqu� puedes implementar la l�gica para inicializar tu cuadr�cula
        Debug.Log($"Grid initialized with width: {gridWidth} and height: {gridHeight}");
    }
}
