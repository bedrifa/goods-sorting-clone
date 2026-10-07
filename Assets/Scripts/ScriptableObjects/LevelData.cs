using UnityEngine;
[CreateAssetMenu(menuName = "LevelData/New Level Data", fileName = "Level")]
public class LevelData : ScriptableObject
{
    public BoardManager BoardTemplate;
    public int BlankCountOfFirstLayer;
    public int ItemVariety;
    public int MaxLayerDepth;
}
