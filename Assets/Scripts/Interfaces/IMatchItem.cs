using UnityEngine;

public interface IMatchItem : IItem
{
    public Vector2 GridPosition { get; }
    public void Matched();
}
