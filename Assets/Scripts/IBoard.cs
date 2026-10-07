using UnityEngine;

public interface IBoard
{
    public void Refill();
    public void AddItem(IMatchItem item, int positionX, int positionY);
    public void RemoveItem(int positionX, int positionY);
}
