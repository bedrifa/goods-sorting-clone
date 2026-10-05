using System.Collections.Generic;

public interface IMatchCondition
{
    public HashSet<IMatchItem> FindMatches(IMatchItem[,] board, IMatchItem movedItem = null);
}
