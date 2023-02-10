using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class Testing : MonoBehaviour
{
    [SerializeField] private List<int> rateList;

    [Button]
    public void Random()
    {
        Common.GetRandomResultWithRate(rateList);
    }

    public Text txt;

    [Button]
    public void TestTypeWriter(string content)
    {
        StartCoroutine(Common.IESimulateTypeWriter(txt, content));
    }

    [Button]
    public void TestChangeNum(int from, int to, int times, float timeStep)
    {
        StartCoroutine(Common.IEChangeNumberGradually(txt, from, to, times,timeStep));
    }

    [Button]
    public void TestChangeText(string content)
    {
        StartCoroutine(Common.IEChangeTextRandomly(txt,true,true,true,10,0.1f, () =>
        {
            txt.text = content;
        }));
    }
}
