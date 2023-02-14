using System;
using System.Collections.Generic;
using System.Linq;
// using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ExpandableMenu : MonoBehaviour
{
    [SerializeField] private Button toggleBtn;
    [SerializeField] private Transform itmParent;
    [SerializeField] private Vector2 spacing;
    [SerializeField] private Vector2 offset;
    [SerializeField] private float animDuration = 0.25f;
    [SerializeField] private bool expandOnStart = true;
    
    private List<Transform> itmList;
    private CanvasGroup canvasGroup;
    private bool isExpanded = false;
    private bool isAnimating = false;

    private void Start()
    {
        if (itmParent.GetComponent<CanvasGroup>() == null)
        {
            itmParent.AddComponent<CanvasGroup>();
        }

        canvasGroup = itmParent.GetComponent<CanvasGroup>();
        
        toggleBtn.onClick.RemoveAllListeners();
        toggleBtn.onClick.AddListener(Toggle);
        
        itmList = new List<Transform>();
        foreach (Transform child in itmParent)
        {
            itmList.Add(child);
        }

        isExpanded = true;
        Collapse(0.01f, () =>
        {
            if (expandOnStart) Expand(0.25f);
        });
    }
    
    private void Toggle()
    {
        if (isAnimating)
        {
            return;
        }
        
        isExpanded = !isExpanded;
        if (isExpanded)
        {
            Expand(animDuration);    
        }
        else
        {
            Collapse(animDuration);
        }
    }

    private void Expand(float time, Action complete = null)
    {
        // ------------[ DOTWEEN IS REQUIRED ]-----------------
        
        
        // isAnimating = true;
        // canvasGroup.DOFade(1f, time * 0.8f);
        // toggleBtn.transform.DOScale(1.2f, time / 2).SetLoops(2, LoopType.Yoyo);
        //
        // int multiple = 1;
        // itmList.ForEach(itm =>
        // {
        //     itm.DOLocalMove((Vector3)offset + (Vector3) spacing * multiple, time).
        //         OnComplete(() =>
        //         {
        //             if (itm == itmList.Last())
        //             {
        //                 isAnimating = false;
        //                 complete?.Invoke();
        //             }
        //         });
        //     multiple++;
        // });
    }

    private void Collapse(float time, Action complete = null)
    {
        // ------------[ DOTWEEN IS REQUIRED ]-----------------
        
        
        // isAnimating = true;
        // canvasGroup.DOFade(0f, time * 0.8f);
        // toggleBtn.transform.DOScale(1.2f, time / 2).SetLoops(2, LoopType.Yoyo);
        //
        // itmList.ForEach(itm =>
        // {
        //     itm.DOLocalMove(Vector3.zero, time).
        //         OnComplete(() =>
        //         {
        //             if (itm == itmList.Last())
        //             {
        //                 isAnimating = false;
        //                 complete?.Invoke();
        //             }
        //         });
        // });
    }
}
