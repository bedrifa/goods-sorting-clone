using System.Collections.Generic;
public class MatchController
{
    private IList<IMatchCondition> _conditions;
    public MatchController()
    {
        _conditions = new List<IMatchCondition>();
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

    public bool PerformMatches(BoardData<IMatchItem> board, IMatchItem movedItem = null)
    {
        HashSet<IMatchItem> matches = new HashSet<IMatchItem>();

        for(int i = 0; i < _conditions.Count; i++)
        {
            matches.UnionWith(_conditions[i].FindMatches(board, movedItem));
        }
        if (matches.Count == 0) return false;

        foreach(IMatchItem matchItem in matches)
        {
            matchItem.Matched();
        }
        return true;
    }
}
