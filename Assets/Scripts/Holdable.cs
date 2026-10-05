using Match3.Interfaces;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Holdable : MonoBehaviour, IInteractable
{
    public bool CanInteractable{ get; private set; }

    public void InteractionStart()
    {
        EventManager.Instance.TriggerEvent(EventType.ItemPicked, gameObject);
    }

    public void InteractionUpdate(Vector3 positon)
    {
        transform.position = new Vector3(positon.x, positon.y, transform.position.z);
    }

    public void InteractionEnd()
    {
        EventManager.Instance.TriggerEvent(EventType.ItemDropped, gameObject);
    }

}
