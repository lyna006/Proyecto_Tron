using UnityEngine;

public class GridNode
{
    public GridNode Up { get; set; }
    public GridNode Down { get; set; }
    public GridNode Left { get; set; }
    public GridNode Right { get; set; }
    public Vector2Int Position { get; private set; }

    public GridNode(Vector2Int position)
    {
        Position = position;
    }
}


