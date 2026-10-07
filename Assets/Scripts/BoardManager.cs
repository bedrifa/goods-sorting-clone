using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField] private List<Shelf> Rows;
    [SerializeField] private int _maxSlotCountInAShelf;
    [SerializeField] private MatchItem _matchItemPrefab;
    public BoardData<IMatchItem> Board { get; private set; }

    public void SwitchItem(Vector2Int from, Vector2Int to)
    {
        IMatchItem temp = Board[to.x, to.y];
        Board[to.x, to.y] = Board[from.x, from.y];
        Board[from.x, from.y] = temp;
        
        CheckRowIsEmpty(from.x);
        
        /*
        string test = "";
        for (int i = 0; i < Board.Width; i++)
        {
            for (int j = 0; j < Board.Width; j++)
            {
                if (Board[i, j] == null)
                {
                    test += "X";
                }
                else
                {
                    test += Board[i, j].Id;
                }

            }
            test += " / ";
        }
        Debug.Log(test);
        */
    }

    public void Clear(int row)
    {
        for (int i = 0; i < Board.Height; i++)
        {
            Board[row, i] = null;
            CheckRowIsEmpty(row);
        }
    }

    public BoardManager Generate(LevelData levelData, List<Sprite> itemSprites)
    {
        Board = new BoardData<IMatchItem>(Rows.Count, _maxSlotCountInAShelf);
        
        for(int i = 0; i < Rows.Count; i++)
        {
            Rows[i].Init(i);
        }

        int totalSlot = Board.Width * Board.Height;
        Sprite selectedSprite;
        int rndX, rndY;

        for (int i = 1; i <= (totalSlot - levelData.BlankCountOfFirstLayer)/3; i++)
        {
            selectedSprite = itemSprites[Random.Range(0,itemSprites.Count)];
            itemSprites.Remove(selectedSprite);

            for(int j = 0; j < 3; j++)
            {
                do
                {
                    rndX = Random.Range(0, Rows.Count);
                    rndY = Random.Range(0, _maxSlotCountInAShelf);
                } while (Board[rndX, rndY] != null);

                Board[rndX,rndY] = Instantiate(_matchItemPrefab, Rows[rndX].Slots[rndY].transform).SetSprite(selectedSprite).SetGridPosition(new Vector2Int(rndX,rndY)).SetId(i);
            }
        }
        
        return this;
    }

    private void CheckRowIsEmpty(int row)
    {
        for(int i =0; i < Rows[row].Slots.Count; i++)
        {
            if (Rows[row].Slots[i] != null) return;
        }

        Rows[row].Refill();
    }
}
