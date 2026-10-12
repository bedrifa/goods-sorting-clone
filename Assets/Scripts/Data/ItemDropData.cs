using UnityEngine;

public class ItemDropData
{
    public IMatchItem Item;
    public Vector3Int OldGridPos;
    public Vector3Int CurrentGridPos;
    public ItemDropData(IMatchItem item, Vector3Int oldGridPos, Vector3Int currentGridPos)
    {
        Item = item;
        OldGridPos = oldGridPos;
        CurrentGridPos = currentGridPos;
    }
}
