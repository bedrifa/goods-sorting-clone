using DG.Tweening;
using UnityEngine;
public class MatchItem : MonoBehaviour, IHoldable
{
    public bool CanInteractable => true;
    public Sequence OnDropTween;

    private void Awake()
    {
        float y = transform.localScale.y;
        OnDropTween = DOTween.Sequence();

        OnDropTween
            .Append(transform.DOScaleY(y - .05f, .15f))
            .Append(transform.DOScaleY(y, .1f))
            .SetLink(gameObject)
            .SetAutoKill(false)
            .Pause();
    }

    public void OnDrag(Vector2 pos)
    {
        transform.position = new Vector3(pos.x, pos.y, transform.position.z);
    }

    public void OnDrop()
    {
        OnDropTween.Restart();
    }

    public void OnHold()
    {
    
    }
}
