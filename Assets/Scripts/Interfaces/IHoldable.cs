using UnityEngine;

public interface IHoldable
{
    public bool CanInteractable { get; }
    public void OnHold();
    public void OnDrag(Vector2 pos);
    public void OnDrop();
}
