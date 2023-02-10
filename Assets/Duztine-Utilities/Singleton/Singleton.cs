using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance;

    public static T Instance
    {
        get => instance;
    }

    [SerializeField] private bool needDontDestroy = false;

    protected void Awake()
    {
        instance = (T) this;
        if (needDontDestroy) DontDestroyOnLoad(gameObject);
        SetUp();
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

    protected virtual void SetUp()
    {
        
    }
}
