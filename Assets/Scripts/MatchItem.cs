using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class MatchItem : MonoBehaviour, IMatchItem, IEventListener
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private ParticleSystem _blastParticle;
    public bool CanInteractable => !_isLocked;
    public int Id { get; private set; }

    public Vector2 GridPosition { get; private set; }

    private Sequence _onDropTween;
    private bool _isLocked;
    
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

    private void OnEnable()
    {
        EventManager.Instance.AddEventListener(EventType.ItemDropped, this);
    }

    private void OnDisable()
    {
        EventManager.Instance.RemoveEventListener(EventType.ItemDropped, this);
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

    public MatchItem SetGridPosition(Vector2 current)
    {
        GridPosition = current;
        return this;
    }

    public async void Matched()
    {
        GetComponent<Collider2D>().enabled = false;
        _onDropTween.Restart();

        await _onDropTween.AsyncWaitForCompletion();
        
        _spriteRenderer.enabled = false;
        
        _blastParticle.Play();
        
        Destroy(gameObject, 1);
    }


    public void OnEvent(EventType eventType, object eventData)
    {
        switch (eventType)
        {
            case EventType.ItemDropped: if(eventData == (object) gameObject) ItemDropped(); break;
            default: break;
        }
    }

    private void ItemDropped()
    {
        _onDropTween.Restart();
    }
}
