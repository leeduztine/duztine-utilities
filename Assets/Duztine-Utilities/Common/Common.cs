using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public static class Common
{
    public static float GetRandomFloat(float min, float max, float step = 0.1f)
    {
        if (min > max)
        {
            min += max;
            max = min - max;
            min -= max;
        }

        float factor = 1f / step;

        return Random.Range((int)(min * factor), (int)(max * factor) + 1) / factor;
    }

    public static bool GetRandomBool(float rate, float maxRate = 100f)
    {
        float rand = GetRandomFloat(0f, maxRate);
        return rand <= rate;
    }

    public static int GetRandomResultWithRate(List<int> rateList)
    {
        List<int> prefixSumList = new List<int>(rateList);

        for (int i = 0; i < prefixSumList.Count; i++)
        {
            if (i > 0)
            {
                prefixSumList[i] += prefixSumList[i - 1];
            }
        }
        
        if (prefixSumList[^1] != 100)
        {
            Debug.Log("Error: invalid rate list");
            return 0;
        }

        int rand = Random.Range(1, 101);
        for (int i = 0; i < prefixSumList.Count; i++)
        {
            if (rand <= prefixSumList[i])
            {
                return i;
            }
        }

        return 0;
    }

    public static IEnumerator IESimulateTypeWriter(Text txt, string content, float delay = 0.1f)
    {
        foreach (char c in content)
        {
            txt.text += c;
            yield return new WaitForSeconds(delay);
        }
    }

    public static IEnumerator IEChangeNumberGradually(Text txt, int originalNum, int expectedNum, float delay = 1f, Action complete = null)
    {
        float timeSinceStarted = 0f;
        int delta = expectedNum - originalNum;
        while (true)
        {
            timeSinceStarted += Time.deltaTime;
            var progress = Mathf.Min(timeSinceStarted / delay, 1f);
            var curNum = originalNum + (int)(delta * progress);
            txt.text = curNum.ToString();

            if (curNum == expectedNum)
            {
                complete?.Invoke();
                yield break;
            }

            yield return null;
        }
    }
    
    public static IEnumerator IEChangeNumberGradually(Text txt, int originalNum, int expectedNum, int times = 10, float timeStep = 0.1f, 
                                                        Action complete = null)
    {
        int timeCount = 0;
        int delta = expectedNum - originalNum;
        while (true)
        {
            timeCount++;
            
            var curNum = originalNum + (int)(delta * (timeCount / (float)times));
            txt.text = curNum.ToString();

            if (timeCount >= times)
            {
                complete?.Invoke();
                txt.text = expectedNum.ToString();
                yield break;
            }

            yield return new WaitForSeconds(timeStep);
        }
    }

    public static IEnumerator IEChangeTextRandomly(Text txt, bool hasLowerCase = true, bool hasUpperCase = true, bool hasNumber = true, 
                                                    float delay = 1f, Action complete = null)
    {
        if (!hasLowerCase && !hasUpperCase && !hasNumber)
        {
            Debug.Log("Error: you must select at least 1 type of character");
            yield break;
        }

        List<char> charPool = new List<char>();
        if (hasLowerCase)
        {
            for (int i = 97; i <= 122; i++)
            {
                charPool.Add((char)i);
            }
        }
        
        if (hasUpperCase)
        {
            for (int i = 65; i <= 90; i++)
            {
                charPool.Add((char)i);
            }
        }
        
        if (hasNumber)
        {
            for (int i = 48; i <= 57; i++)
            {
                charPool.Add((char)i);
            }
        }

        float timeSinceStarted = 0f;
        while (true)
        {
            timeSinceStarted += Time.deltaTime;
            bool isDone = timeSinceStarted >= delay;

            if (isDone)
            {
                complete?.Invoke();
                yield break;
            }

            char[] charArr = txt.text.ToCharArray();
            for (int i = 0; i < charArr.Length; i++)
            {
                charArr[i] = charPool[Random.Range(0, charPool.Count)];
            }

            txt.text = new string(charArr);

            yield return null;
        }
    }
    
    public static IEnumerator IEChangeTextRandomly(Text txt, bool hasLowerCase = true, bool hasUpperCase = true, bool hasNumber = true, 
                                                    int times = 10, float timeStep = 0.1f, Action complete = null)
    {
        if (!hasLowerCase && !hasUpperCase && !hasNumber)
        {
            Debug.Log("Error: you must select at least 1 type of character");
            yield break;
        }

        List<char> charPool = new List<char>();
        if (hasLowerCase)
        {
            for (int i = 97; i <= 122; i++)
            {
                charPool.Add((char)i);
            }
        }
        
        if (hasUpperCase)
        {
            for (int i = 65; i <= 90; i++)
            {
                charPool.Add((char)i);
            }
        }
        
        if (hasNumber)
        {
            for (int i = 48; i <= 57; i++)
            {
                charPool.Add((char)i);
            }
        }

        int timesCount = 0;
        while (true)
        {
            timesCount++;
            
            char[] charArr = txt.text.ToCharArray();
            for (int i = 0; i < charArr.Length; i++)
            {
                charArr[i] = charPool[Random.Range(0, charPool.Count)];
            }

            txt.text = new string(charArr);

            if (timesCount >= times)
            {
                complete?.Invoke();
                yield break;
            }

            yield return new WaitForSeconds(timeStep);
        }
    }
}
