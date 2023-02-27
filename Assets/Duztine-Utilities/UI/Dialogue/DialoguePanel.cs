using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DialoguePanel : Singleton<DialoguePanel>
{
    [SerializeField] private TextMeshProUGUI curSpeechTxt;
    
    [Space]
    
    [SerializeField] private List<Sprite> avatarList;
    [SerializeField] private List<DialogueCharacter> charList;
    
    private int curSide = -1;

    [SerializeField] private DialogueScript sampleScript;
    
    [Button]
    public void PlaySampleScript()
    {
        charList.ForEach(c=>c.gameObject.SetActive(false));
        
        StartCoroutine(IEPlayDialogueScript());
        
        IEnumerator IEPlayDialogueScript()
        {
            var dialogueList = sampleScript.dialogueList;
            var lastSpeaker = "";
            
            foreach (var dialogue in dialogueList)
            {
                var avt = GetAvatarById(dialogue.avatarId);
                if (dialogue.name != lastSpeaker)
                {
                    curSide = (curSide + 1) % 2;
                }

                lastSpeaker = dialogue.name;

                for (var i = 0; i < 2; i++)
                {
                    if (i == curSide)
                    {
                        charList[i].SetIdentity(avt, dialogue.name);
                        charList[i].Speak(true);
                    }
                    else
                    {
                        charList[i].Speak(false);
                    }
                }

                yield return StartCoroutine(IESimulateTypeWriter(curSpeechTxt, dialogue.speech));
                yield return new WaitForSeconds(1f);
            }
        }
        
        IEnumerator IESimulateTypeWriter(TextMeshProUGUI txt, string content, float delay = 0.1f)
        {
            txt.text = "";
            foreach (char c in content)
            {
                txt.text += c;
                yield return new WaitForSeconds(delay);
            }
        }
    }

    private Sprite GetAvatarById(string avatarId)
    {
        var avt = avatarList.Find(a => a.name == avatarId);
        if (avt == null)
        {
            Debug.Log($"NULL: AvatarID '{avatarId}' is not exist");
        }
        return avt;
    }
}

[Serializable]
public class DialogueScript
{
    [FormerlySerializedAs("dialogueQueue")] public List<Dialogue> dialogueList = new();
}

[Serializable]
public class Dialogue
{
    public string name;
    public string avatarId;
    public string speech;
}
