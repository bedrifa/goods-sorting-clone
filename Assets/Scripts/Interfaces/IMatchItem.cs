using UnityEngine;
public interface IMatchItem : IItem
{
    public Vector2Int GridPos { get; }
    public void Matched();
}
