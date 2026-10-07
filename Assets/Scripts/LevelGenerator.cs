using UnityEngine;

public class LevelGenerator: MonoBehaviour
{
    
    private void Awake()
    {

    }
    private IMatchItem[,] _board;
    public IMatchItem[,] GetBoard()
    {
        return _board;
    }
    
    public void Generate() 
    {
    
    }
}
