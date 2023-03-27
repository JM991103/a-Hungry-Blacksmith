using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreNPC : MonoBehaviour
{
    Animator anim;
    Dialogue dialogue;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        dialogue = FindObjectOfType<Dialogue>();
    }
    
    /// <summary>
    /// 애니메이션 이벤트 시스템(Dialogue 출력)
    /// </summary>
    void StartAnim()
    {
        dialogue.ShowOnOff(true);
        dialogue.NextDialogue();
    }

     
}
