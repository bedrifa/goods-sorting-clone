using UnityEngine;
namespace Match3.Interfaces
{
    public interface IInteractable
    {
        public bool CanInteractable { get; }
        public void InteractionStart();
        public void InteractionUpdate(Vector3 position);
        public void InteractionEnd();
    }
}
