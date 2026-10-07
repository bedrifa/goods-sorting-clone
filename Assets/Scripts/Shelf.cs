using System.Collections.Generic;
using UnityEngine;

public class Shelf : MonoBehaviour
{
    [SerializeField] private int boardSizeX = 1;
    [SerializeField] private int boardSizeY = 3;
    [SerializeField] private List<ShelfSlot> _slots;
    public List<ShelfSlot> Slots { get => _slots; }
    public void Init(int boardPosX)
    {
        for(int i = 0; i < _slots.Count; i++)
        {
            Slots[i].Assign(new Vector2Int (boardPosX, i));
        }
    }

    public void Refill()
    {
        
    }
}
