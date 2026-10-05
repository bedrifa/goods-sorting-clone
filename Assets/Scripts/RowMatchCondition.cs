using System.Collections.Generic;

public class RowMatchCondition : IMatchCondition
{
    private int _minRequirementForMatching;
    
    public RowMatchCondition( int minRequirementForMatching = 3)
    { 
        _minRequirementForMatching = minRequirementForMatching;
    }

    public HashSet<IMatchItem> FindMatches(IMatchItem[,] board, IMatchItem movedItem = null)
    {
        HashSet<IMatchItem> matched = new HashSet<IMatchItem>();
        HashSet<IMatchItem> current = new HashSet<IMatchItem>();

        if (movedItem != null &&  board != null)
        {
            current.Add(movedItem);

            int startColumn = (int)movedItem.GridPosition.y;
            int startRow = (int)movedItem.GridPosition.x;
            int maxColumn = board.GetLength(1);

            if (startColumn < maxColumn)
            {
                for (int i = startColumn + 1; i < maxColumn; i++)
                {
                    if (board[startRow, i] == null || board[startRow, i].Id != movedItem.Id)
                        break;

                    current.Add(board[startRow, i]);
                }
            }
            
            if(startColumn > 0)
            {
                for (int i = startColumn - 1; i >= 0; i--)
                {
                    if (board[startRow, i] == null || board[startRow, i].Id != movedItem.Id)
                        break;

                    current.Add(board[startRow, i]);
                }
            }

            if (current.Count >= _minRequirementForMatching)
            {
                matched.UnionWith(current);
            }
            current.Clear();
        }

        return matched;
    }
}
