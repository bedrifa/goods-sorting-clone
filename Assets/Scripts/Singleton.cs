using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    public static T Instance { get; private set; }
    public abstract bool Persist { get; }

    protected virtual void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning($"{this.GetType()} singleton object already exist");
            DestroyImmediate(gameObject);
            return;
        }

        Instance = this as T;

        if (Persist)
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    protected virtual void OnDestroy()
    {
        if (Instance == this as T) Instance = null;
    }
}