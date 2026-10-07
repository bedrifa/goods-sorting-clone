using DG.Tweening;
using Match3.Interfaces;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Collider2D))]
public class MatchItem : MonoBehaviour, IMatchItem, IInteractable
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private ParticleSystem _blastParticle;
    [SerializeField] private LayerMask _slotLayer;
    public bool CanInteractable => !_isLocked;
    public int Id { get; private set; }

    public Vector2Int GridPos { get; private set; }

    private Sequence _onDropTween;
    private bool _isLocked, _isPicked;
    private Vector3 _lastPosition;
    private void Awake()
    {
        float y = transform.localScale.y;
        _onDropTween = DOTween.Sequence();
        _onDropTween
            .Append(transform.DOScaleY(y - .05f, .15f))
            .Append(transform.DOScaleY(y, .1f))
            .SetLink(gameObject)
            .SetAutoKill(false)
            .Pause();
    }

    public MatchItem SetLock(bool isLocked)
    {
        _isLocked = isLocked;
        _spriteRenderer.color = Color.gray3;
        return this;
    }

    public MatchItem SetInvisibility(bool isInvisible)
    {
        gameObject.SetActive(isInvisible);
        return this;
    }

    public MatchItem SetSprite(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
        return this;
    }

    public MatchItem SetId(int id)
    {
        Id = id;
        return this;
    }

    public MatchItem SetGridPosition(Vector2Int gridPos)
    {
        GridPos = gridPos;
        return this;
    }

    public async void Matched()
    {
        GetComponent<Collider2D>().enabled = false;
        _isLocked = true;
        _onDropTween.Restart();

        await _onDropTween.AsyncWaitForCompletion();
        
        _spriteRenderer.enabled = false;
        
        _blastParticle.Play();
        
        Destroy(gameObject, 1);
    }

    public void Drop()
    {
        _onDropTween.Restart();
    }

    public void InteractionStart()
    {
        _lastPosition = transform.position;
    }

    public void InteractionUpdate(Vector3 position)
    {
        transform.position = new Vector3(position.x, position.y, transform.position.z);
    }

    public void InteractionEnd()
    {
        Collider2D hitSlot = Physics2D.OverlapPoint(transform.position, _slotLayer);
        if (hitSlot != null && hitSlot.TryGetComponent(out ShelfSlot slot))
        {
            transform.parent = hitSlot.gameObject.transform;
            transform.localPosition = Vector3.zero;
            Vector2Int temp = GridPos;
            SetGridPosition(slot.GridPos);
            EventManager.Instance.TriggerEvent(EventType.ItemDropped, new ItemDropData(this, temp, GridPos));
        }
        else
        {
            transform.position = _lastPosition;
        }
    }
}
