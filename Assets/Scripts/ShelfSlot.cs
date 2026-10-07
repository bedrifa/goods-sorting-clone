using UnityEngine;

public class ShelfSlot : MonoBehaviour
{
    public Vector2Int GridPos { get; private set; }

    public void Assign(Vector2Int gridPos)
    {
        GridPos = gridPos;
    }
}
