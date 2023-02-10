using System.Collections;
using UnityEngine;

public class RecyclableObject : MonoBehaviour
{
    [SerializeField] private bool isAutoRecycle;
    [SerializeField] private float lifeTime;

    private bool isAvailable;
    public bool IsAvailable
    {
        get => isAvailable;
    }

    public string originalName;
    
    public void OnSpawn()
    {
        isAvailable = true;
        if (isAutoRecycle)
        {
            StartCoroutine(IEAutoRecycle());
        }
    }

    IEnumerator IEAutoRecycle()
    {
        yield return new WaitForSeconds(lifeTime);
        Recycle();
    }

    public virtual void Recycle()
    {
        if (!isAvailable) return;
        isAvailable = false;

        if (ObjectPool.Instance)
        {
            ObjectPool.Instance.DestroyObject(this);
        }
    }
}
