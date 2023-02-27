using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

public class DialogueCharacter : MonoBehaviour
{
    [SerializeField] private Image avtImg;
    [SerializeField] private TextMeshProUGUI nameTxt;
    [SerializeField] private CanvasGroup nameCG;
    [SerializeField] private Image blackImg;

    private bool hasIdentity = false;
    
    public void SetIdentity(Sprite avt, string charName)
    {
        if (!hasIdentity)
        {
            hasIdentity = true;
            gameObject.SetActive(true);
        }
        
        avtImg.sprite = avt;
        nameTxt.text = charName;
    }

    public void Speak(bool isActive)
    {
        avtImg.transform.DOScale(isActive ? 1.1f : 1f, 0.5f);
        nameCG.DOFade(isActive ? 1f : 0f, 0.5f);
        blackImg.DOFade(isActive ? 0f : 0.8f, 0.5f);
    }
}
