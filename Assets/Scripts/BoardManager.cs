using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField] private List<Shelf> Rows;
    [SerializeField] private int _maxSlotCountInAShelf;
    [SerializeField] private MatchItem _matchItemPrefab;
    public BoardData<IMatchItem> Board { get; private set; }

    public void SwitchItem(Vector3Int from, Vector3Int to)
    {
        if (from.z != 0 || to.z != 0) return;

        IMatchItem temp = Board[to.x, to.y];
        Board[to.x, to.y] = Board[from.x, from.y];
        Board[from.x, from.y] = temp;
        
        CheckRowIsEmpty(from.x);
    }

    public void Clear(int row)
    {
        for (int i = 0; i < Board.Height; i++)
        {
            Board[row, i] = null;
            CheckRowIsEmpty(row);
        }
    }

    public virtual BoardManager Generate(LevelData levelData, List<Sprite> itemSprites)
    {
        Board = new BoardData<IMatchItem>(Rows.Count, _maxSlotCountInAShelf);
        
        for(int i = 0; i < Rows.Count; i++)
        {
            Rows[i].Init(i, levelData.MaxLayerDepth);
        }

        Sprite selectedSprite;
        int selectedDepth = 0;
        ShelfSlot selectedSlot;
        
        IList<ShelfSlot> activeSlots = GetAllSlots();
        List<int> placement;

        
        placement = PickRandomShelfSlot(activeSlots, 3);
        
        placement.Sort((a, b) => b.CompareTo(a));

        for (int k = 0; k < placement.Count; k++)
        {
            activeSlots.RemoveAt(placement[k]);
        }

        placement.Clear();

        int id = levelData.ItemVariety;
        /*
        while (activeSlots.Count > 0 && id > 0)
        {
            selectedSprite = itemSprites[Random.Range(0,itemSprites.Count)];
            itemSprites.Remove(selectedSprite);
            
            if (activeSlots.Count < _maxSlotCountInAShelf)
            {
                for(int i = 0; i < _maxSlotCountInAShelf; i++)
                {
                    int rnd = Random.Range(0, activeSlots.Count);
                    selectedSlot = activeSlots[rnd];
                    ItemPlace(ItemGenerate(selectedSlot, selectedSprite, id, selectedDepth), selectedSlot, selectedDepth);
                    
                    activeSlots.RemoveAt(rnd);
                }
            }
            else
            {
                placement = PickRandomShelfSlot(activeSlots, 3);

                for (int i = 0; i < placement.Count; i++)
                {
                    selectedSlot = activeSlots[placement[i]];
                    ItemPlace(ItemGenerate(selectedSlot, selectedSprite, id, selectedDepth), selectedSlot, selectedDepth);
                }

                for (int i = 0; i < placement.Count; i++)
                {
                    activeSlots.Remove(activeSlots[placement[i]]);
                }

                placement.Clear();
            }

            id--;
        }
        */
        int remaining = 3;
        while(id > 0)
        {
            selectedSprite = itemSprites[Random.Range(0, itemSprites.Count)];
            itemSprites.Remove(selectedSprite);
            
            if(activeSlots.Count < 3)
            {
                remaining = 3 - activeSlots.Count;
                
                for (int i = activeSlots.Count-1; i >= 0; i--)
                {
                    int rnd = Random.Range(0, activeSlots.Count);
                    selectedSlot = activeSlots[rnd];
                    ItemPlace(ItemGenerate(selectedSlot, selectedSprite, id, selectedDepth), selectedSlot, selectedDepth);

                    activeSlots.RemoveAt(rnd);
                }

                selectedDepth++;
                activeSlots = GetAllSlots();
                
                placement = PickRandomShelfSlot(activeSlots, 3);
                placement.Sort((a, b) => b.CompareTo(a));

                for (int k = 0; k < placement.Count; k++)
                {
                    activeSlots.RemoveAt(placement[k]);
                }

                placement.Clear();
            }

            if(remaining > 0)
            {
                placement = PickRandomShelfSlot(activeSlots, remaining);

                for (int i = 0; i < placement.Count; i++)
                {
                    selectedSlot = activeSlots[placement[i]];
                    ItemPlace(ItemGenerate(selectedSlot, selectedSprite, id, selectedDepth), selectedSlot, selectedDepth);  
                }

                placement.Sort((a, b) => b.CompareTo(a));

                for(int i = 0;  i < placement.Count; i++)
                {
                    activeSlots.RemoveAt(placement[i]);
                }

                placement.Clear();
            }

            remaining = 3;
            id--;
        }
        return this;
    }

    private List<int> PickRandomShelfSlot(IList<ShelfSlot> activeSlots, int slotCount = 3)
    {
        List<int> slots = new List<int>(slotCount);
        HashSet<int> shelfIndex = new HashSet<int>(slotCount);
        
        int rnd = 0;
        
        while (slots.Count < slotCount && slots.Count < activeSlots.Count)
        {
            rnd = Random.Range(0, activeSlots.Count);
            if (!shelfIndex.Contains(activeSlots[rnd].GridPos.x))
            {
                slots.Add(rnd);
                shelfIndex.Add(activeSlots[rnd].GridPos.x);
            }
            
        }

        return slots;
    }

    private IList<ShelfSlot> GetAllSlots()
    {
        IList<ShelfSlot> activeSlots = new List<ShelfSlot>();

        for(int i=0; i<Rows.Count; i++)
        {
            for(int j=0; j < Rows[i].Slots.Count; j++)
            {
                activeSlots.Add(Rows[i].Slots[j]);
            }
        }

        return activeSlots;
    }

    private async void CheckRowIsEmpty(int row)
    {
        for(int i = 0; i < Rows[row].Slots.Count; i++)
        {
            if (Board[row, i] != null) return;
        }
        
        await Task.Delay(250);

        IMatchItem[] boardItems = Rows[row].GetRefilledBoard();
        
        for(int i = 0; i < Board.Height && i < boardItems.Length; i++)
        {
            Board[row,i] = boardItems[i];
        }
    }

    private IMatchItem ItemGenerate(ShelfSlot selectedSlot, Sprite selectedSprite, int id, int selectedDepth)
    {
        IMatchItem matchItem = Instantiate(_matchItemPrefab, selectedSlot.transform)
                                            .SetSprite(selectedSprite)
                                            .SetGridPosition(new Vector3Int(selectedSlot.GridPos.x, selectedSlot.GridPos.y, selectedDepth))
                                            .SetId(id)
                                            .SetLock(selectedDepth != 0)
                                            .SetInvisibility(selectedDepth > 1);
        return matchItem;
    }

    private void ItemPlace(IMatchItem matchItem, ShelfSlot selectedSlot, int selectedDepth)
    {
        if (selectedDepth == 0)
            Board[selectedSlot.GridPos.x, selectedSlot.GridPos.y] = matchItem;
        else
            Rows[selectedSlot.GridPos.x].AddItemToQueue(matchItem as MatchItem);
    }
}
