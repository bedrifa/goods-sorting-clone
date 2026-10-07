using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public override bool Persist => false;
    protected override void Awake()
    {
        base.Awake();
        Input.multiTouchEnabled = false;
        Application.targetFrameRate = 90;
    }
}
