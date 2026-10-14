using UnityEngine;

public struct ItemDropData
{
    public IMatchItem Item;
    public Vector3Int OldGridPos;
    public Vector3Int CurrentGridPos;
}

public struct ItemMatchData
{
    public Vector3Int MatchedPosition; 
}

public struct PopupShowData
{
    public GameObject GameObject;
}
