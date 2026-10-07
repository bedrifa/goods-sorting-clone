using Match3.Interfaces;
using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Holdable : MonoBehaviour, IInteractable
{
    public Action OnInteractionStart;
    public Action OnInteractionEnd;

    public bool CanInteractable{ get; private set; }

    public void InteractionStart()
    {
        OnInteractionStart?.Invoke();
    }

    public void InteractionUpdate(Vector3 positon)
    {
        transform.position = new Vector3(positon.x, positon.y, transform.position.z);
    }

    public void InteractionEnd()
    {
        OnInteractionEnd?.Invoke();
    }

}
