using Match3.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;
namespace Match3.InteractionSystem
{
    public class InteractionController : MonoBehaviour
    {
        private Camera _cam;
        private IInteractable _interactableObject;
    
        private void Awake()
        {
            _cam = Camera.main;
        }
        private void Update()
        {
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
                return;

            if (Input.GetMouseButtonDown(0))
            {
                Collider2D hitCollider = Physics2D.OverlapPoint(_cam.ScreenToWorldPoint(Input.mousePosition));
                if (hitCollider != null && hitCollider.TryGetComponent(out _interactableObject))
                {
                    _interactableObject.InteractionStart();
                }
            }
            if (Input.GetMouseButton(0))
            {
                _interactableObject?.InteractionUpdate(_cam.ScreenToWorldPoint(Input.mousePosition));
            }
            if (Input.GetMouseButtonUp(0))
            {
                _interactableObject?.InteractionEnd();
                _interactableObject = null;
            }
        }
    }
}
