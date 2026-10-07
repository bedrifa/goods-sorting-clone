using System.Collections.Generic;

public class RowMatchCondition : IMatchCondition
{
    private int _minRequirementForMatching;
    
    public RowMatchCondition( int minRequirementForMatching = 3)
    { 
        _minRequirementForMatching = minRequirementForMatching;
    }

    public HashSet<IMatchItem> FindMatches(BoardData<IMatchItem> board, IMatchItem movedItem = null)
    {
        HashSet<IMatchItem> matched = new HashSet<IMatchItem>();
        HashSet<IMatchItem> current = new HashSet<IMatchItem>();
        
        if (movedItem != null &&  board != null)
        {
            current.Add(movedItem);

            int startWidth = movedItem.GridPos.x;
            int startHeight = movedItem.GridPos.y;

            if (startHeight < board.Height)
            {
                for (int i = startHeight + 1; i < board.Height; i++)
                {
                    if (board[startWidth, i] == null || board[startWidth, i].Id != movedItem.Id)
                        break;

                    current.Add(board[startWidth, i]);
                }
            }
            
            if(startHeight > 0)
            {
                for (int i = startHeight - 1; i >= 0; i--)
                {
                    if (board[startWidth, i] == null || board[startWidth, i].Id != movedItem.Id)
                        break;

                    current.Add(board[startWidth, i]);
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
