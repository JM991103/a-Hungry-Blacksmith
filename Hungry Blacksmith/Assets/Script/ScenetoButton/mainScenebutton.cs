using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainScenebutton : MonoBehaviour
{
    public string loadScene;
    public int hpDown = 0;

    Button mainSceneButton;
    TextMeshProUGUI mainSceneText;

    private void Awake()
    {
        mainSceneButton = GetComponent<Button>();
        mainSceneButton.onClick.AddListener(ChangeButton);
        mainSceneText = mainSceneButton.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        if (gameObject.name == "black market")
        {
            mainSceneText.gameObject.SetActive(UI.Instance.blackMarket);
        }
    }

    void ChangeButton()
    {
        if ((GameManager.Inst.UI.HP - hpDown) > -1)
        {
            if (loadScene == "black market")
            {
                if (UI.Instance.blackMarket)
                {
                    SceneManager.LoadScene(loadScene);
                    Debug.Log($"HP가{hpDown}만큼 감소되었습니다.  현재 HP:{GameManager.Inst.UI.HP}");
                    if (SceneManager.GetActiveScene().name == "main")
                    {
                        GameManager.Inst.UI.DayTextParent.gameObject.SetActive(false);
                    }
                    else
                    {
                        GameManager.Inst.UI.DayTextParent.gameObject.SetActive(true);
                    }

                    UI.Instance.blackMarket = false;
                }
                else
                {
                    Debug.Log("암시장 입장 불가능");                    
                }
            }
            else
            {
                SceneManager.LoadScene(loadScene);
                Debug.Log($"HP가{hpDown}만큼 감소되었습니다.  현재 HP:{GameManager.Inst.UI.HP}");
                if (SceneManager.GetActiveScene().name == "main")
                {
                    GameManager.Inst.UI.DayTextParent.gameObject.SetActive(false);
                }
                else
                {
                    GameManager.Inst.UI.DayTextParent.gameObject.SetActive(true);
                }
            }
        }
    }
}
