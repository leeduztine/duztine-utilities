using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // ==================== DOTWEEN IS REQUIRED ======================
    
    [SerializeField] private Image mainBar;
    [SerializeField] private Image subBar;
    
    [Space]
    
    [SerializeField] private Color healingColor;
    [SerializeField] private Color takingDmgColor;
    [SerializeField] private float shortPhase = 0.5f;
    [SerializeField] private float intervalPhase = 0f;
    [SerializeField] private float longPhase = 1f;
    
    [Space]
    
    [SerializeField] private int maxHp = 100;
    [SerializeField] private int damageAmount = 25;
    [SerializeField] private int healAmount = 25;
    
    private int currentHp = 0;

    private Sequence seq;
    private float curHpRate;
    private float mainDelta;
    private float subDelta;

    private void Start()
    {
        currentHp = maxHp;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow)) Heal();
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) TakeDamage();
    }

    private void CalculateCurHp(bool isTakenDmg)
    {
        if (isTakenDmg)
            currentHp -= damageAmount;
        else
            currentHp += healAmount;

        currentHp = Mathf.Clamp(currentHp, 0, maxHp);
        curHpRate = (float)currentHp / maxHp;
        mainDelta = Mathf.Abs(curHpRate - mainBar.fillAmount);
        subDelta = Mathf.Abs(curHpRate - subBar.fillAmount);
    }

    [Button]
    public void TakeDamage()
    {
        CalculateCurHp(true);
        
        seq.Kill();
        seq = DOTween.Sequence();
        subBar.color = takingDmgColor;
        seq.Append(mainBar.DOFillAmount(curHpRate, shortPhase * mainDelta))
            .AppendInterval(intervalPhase)
            .Append(subBar.DOFillAmount(curHpRate, longPhase * subDelta));
        
    }

    [Button]
    public void Heal()
    {
        CalculateCurHp(false);
        
        seq.Kill();
        seq = DOTween.Sequence();
        subBar.color = healingColor;
        seq.Append(subBar.DOFillAmount(curHpRate, longPhase * subDelta))
            .AppendInterval(intervalPhase)
            .Append(mainBar.DOFillAmount(curHpRate, shortPhase * mainDelta));
    }
}
