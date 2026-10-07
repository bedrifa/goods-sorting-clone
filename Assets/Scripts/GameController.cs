using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour, IEventListener
{
    [SerializeField] private List<LevelData> _levelDatas;
    [SerializeField] private List<Sprite> _itemSprites;
    private MatchController _matchController;
    private BoardManager _boardManager;
    private void Awake()
    {
        GenerateLevel();
        _matchController = new MatchController();
        _matchController.AddCondition(new RowMatchCondition(3));
    }
    
    private void OnEnable()
    {
        EventManager.Instance.AddEventListener(EventType.ItemDropped, this);
    }
    private void OnDisable()
    {
        EventManager.Instance?.RemoveEventListener(EventType.ItemDropped, this);
    }

    private void GenerateLevel() 
    {
        _boardManager = Instantiate(_levelDatas[0].BoardTemplate, Vector3.zero, Quaternion.identity).Generate(_levelDatas[0], _itemSprites);
    }

    private void OnItemDropped(ItemDropData data)
    {
        MatchItem item = data.Item as MatchItem;
        _boardManager.SwitchItem(data.OldGridPos, data.CurrentGridPos);

        if (_matchController.PerformMatches(_boardManager.Board, data.Item))
        {
            _boardManager.Clear(data.Item.GridPos.x);
            EventManager.Instance.TriggerEvent(EventType.ItemMatched, data.CurrentGridPos.x);
        }
        else
            item.Drop();

    }

    public void OnEvent(EventType eventType, object eventData)
    {
        switch (eventType)
        {
            case EventType.ItemDropped: OnItemDropped(eventData as ItemDropData); break;
            default: break;
        }
    }
}
