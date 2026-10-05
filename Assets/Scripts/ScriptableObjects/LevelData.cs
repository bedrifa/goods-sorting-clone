using UnityEngine;
[CreateAssetMenu(menuName = "LevelData/New Level Data", fileName = "Level")]
public class LevelData : ScriptableObject
{
    public GameObject BoardTemplate;
    public int BlankCountOfFirstLayer;
    public int ItemVariety;
    public int MaxLayerDepth;
}
