using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageHoldingHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // ========= COMPONENT "IMAGE" IS REQUIRED ON THIS GAMEOBJECT ===========
    
    [SerializeField] private float delay;
    [SerializeField] private UnityEvent unityEvent;
    private Coroutine coroutine;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        coroutine = StartCoroutine(IEInvokeEventAfterDelay());
        
        IEnumerator IEInvokeEventAfterDelay()
        {
            yield return new WaitForSeconds(delay);
            unityEvent?.Invoke();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
    }
}
