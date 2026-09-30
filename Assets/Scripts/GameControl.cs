using UnityEngine;

public class GameControl : MonoBehaviour
{
    private Camera cam;
    private IHoldable holdableObject;
    
    private void Awake()
    {
        cam = Camera.main;
    }
    private void Update()
    { 
        if (Input.GetMouseButtonDown(0))
        {
            Collider2D hitCollider = Physics2D.OverlapPoint(cam.ScreenToWorldPoint(Input.mousePosition));
            if (hitCollider != null && hitCollider.TryGetComponent(out holdableObject))
            {
                holdableObject.OnHold();
            }
        }
        if (Input.GetMouseButton(0))
        {
            holdableObject?.OnDrag(cam.ScreenToWorldPoint(Input.mousePosition));
        }
        if (Input.GetMouseButtonUp(0))
        {
            holdableObject?.OnDrop();
            holdableObject = null;
        }
    }
}
