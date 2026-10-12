using System.Collections.Generic;
using UnityEngine;

public class Shelf : MonoBehaviour
{
    [SerializeField] private List<ShelfSlot> _slots;
    public List<ShelfSlot> Slots { get => _slots; }
    private BoardData<MatchItem> _nextItems;
    private int boardPos;
    public void Init(int boardPosX, int maxDepth)
    {
        _nextItems = new BoardData<MatchItem>(maxDepth, _slots.Count);

        for(int i = 0; i < _slots.Count; i++)
        {
            Slots[i].Assign(new Vector2Int (boardPosX, i));
        }
        
        boardPos = boardPosX;
    }

    public IMatchItem[] GetRefilledBoard()
    {
        MatchItem[] items = new MatchItem[Slots.Count];
        for(int z = 0; z < _nextItems.Width; z++)
        {
            for(int y = 0; y < _nextItems.Height; y++)
            {
                if(z == 0)
                {
                    items[y] = _nextItems[z, y];
                }
                else
                {
                    _nextItems[z - 1, y] = _nextItems[z, y];
                }
            
                if (_nextItems[z, y] != null)
                {
                    _nextItems[z, y].SetGridPosition(new Vector3Int(boardPos, y, z))
                        .SetLock(z != 0)
                        .SetInvisibility(z > 1);
                }

                _nextItems[z, y] = null;
            }
        }

        return items;
    }

    public void AddItemToQueue(MatchItem item)
    {
        if (item.GridPos.z - 1 > _nextItems.Width)
        {
            Debug.Log($"given depth is more higher than given max depth. depth: {item.GridPos.z}, max depth {_nextItems.Height}");
            return;
        }

        if(_nextItems[item.GridPos.z - 1, item.GridPos.y] != null)
        {
            Debug.Log($"x: {item.GridPos.x}, y:{item.GridPos.y}, z:{item.GridPos.z} is already assigned");
            return;
        }

        _nextItems[item.GridPos.z - 1, item.GridPos.y] = item;
    }
}
