using UnityEngine;

public class ItemDropData
{
    public IMatchItem Item;
    public Vector2Int OldGridPos;
    public Vector2Int CurrentGridPos;
    public ItemDropData(IMatchItem item, Vector2Int oldGridPos, Vector2Int currentGridPos)
    {
        Item = item;
        OldGridPos = oldGridPos;
        CurrentGridPos = currentGridPos;
    }
}
