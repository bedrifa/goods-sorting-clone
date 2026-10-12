using UnityEngine;
public interface IMatchItem : IItem
{
    public Vector3Int GridPos { get; }
    public void Matched();
}
