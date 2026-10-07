using System.Collections.Generic;

public interface IMatchCondition
{
    public HashSet<IMatchItem> FindMatches(BoardData<IMatchItem> board, IMatchItem movedItem = null);
}
