using System.Collections.Generic;
public class MatchController
{
    private IMatchItem[,] _board;
    private IList<IMatchCondition> _conditions;
    
    public MatchController(IMatchItem[,] board) 
    {
        _board = board;
    }

    public MatchController AddCondition(IMatchCondition condition)
    {
        if (!_conditions.Contains(condition))
        {
            _conditions.Add(condition);
        }
        
        return this;
    }

    public MatchController RemoveCondition(IMatchCondition condition)
    {
        if (_conditions.Contains(condition))
        {
            _conditions.Remove(condition);
        }

        return this;
    }

    public void PerformMatches(IMatchItem movedItem = null)
    {
        HashSet<IMatchItem> matches = new HashSet<IMatchItem>();

        for(int i = 0; i < _conditions.Count; i++)
        {
            matches.UnionWith(_conditions[i].FindMatches(_board, movedItem));
        }

        foreach(IMatchItem matchItem in matches)
        {
            matchItem.Matched();
        }
    }
}
