using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public static UI Instance;

    /// <summary>
    /// UI에 표시되는 HP 텍스트
    /// </summary>
    TextMeshProUGUI hpText;

    /// <summary>
    /// UI에 표시되는 골드 텍스트
    /// </summary>
    TextMeshProUGUI goldText;

    /// <summary>
    /// UI에 표시되는 명성치 텍스트
    /// </summary>
    TextMeshProUGUI fameText;

    /// <summary>
    /// UI에 표시되는 날짜 텍스트
    /// </summary>
    TextMeshProUGUI dayText;

    /// <summary>
    /// 다음날로 넘어가는 버튼
    /// </summary>
    Button nextDayButton;

    /// <summary>
    /// 다음날로 넘어가는 버튼을 누르면 나타나는 검은 화면
    /// </summary>
    Image nextDayImage;

    /// <summary>
    /// 검은 화면에 나타나는 텍스트
    /// </summary>
    TextMeshProUGUI nextDayText;


    /// <summary>
    /// 최대 체력
    /// </summary>
    int maxhp = 5;

    /// <summary>
    /// 현재 체력
    /// </summary>
    int hp = 5;

    /// <summary>
    /// 사용 가능한 골드
    /// </summary>
    int gold;

    /// <summary>
    /// 명성치
    /// </summary>
    int fame;

    /// <summary>
    /// 플레이한 날짜
    /// </summary>
    int day = 1;

    public int HP
    {
        get => hp;
        set
        {
            hp = value;
            hpText.text = $"{hp} / {maxhp}";
            if (hp <= 0)
            {
                if (SceneManager.GetActiveScene().name == "main")
                {
                    nextDayButton.gameObject.SetActive(false);
                }
                else
                {
                    nextDayButton.gameObject.SetActive(true);
                }
            }
        }
    }

    public int Gold
    {
        get => gold;
        set
        {
            if (gold != value)
            {
                gold = value;
                goldText.text = $"{gold}";
            }
        }
    }

    public int Fame
    {
        get => fame;
        set
        {
            if (fame != value)
            {
                fame = value;
                fameText.text = $"{fame}";
            }
        }
    }

    public int Day
    {
        get => day;
        set
        {
            if (day != value)
            {
                day = value;
                dayText.text = $"{day}";
            }
        }
    }

    public Transform DayTextParent => dayText.transform.parent.GetComponent<Transform>();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(transform.parent);        

        hpText = transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        goldText = transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        fameText = transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        dayText = transform.GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>();

        nextDayButton = transform.GetChild(4).GetComponent<Button>();
        nextDayButton.gameObject.SetActive(false);
        nextDayButton.onClick.AddListener(NextDay);

        nextDayImage = transform.GetComponentInChildren<Image>();
        nextDayImage.gameObject.SetActive(false);
        nextDayText = nextDayImage.GetComponentInChildren<TextMeshProUGUI>();
        nextDayText.gameObject.SetActive(false);
    }

    void NextDay()
    {
        nextDayText.gameObject.SetActive(true);
        nextDayImage.gameObject.SetActive(true);
        
        StartCoroutine(FadeInOut());        
        nextDayButton.gameObject.SetActive(false);        
    }

    IEnumerator FadeInOut()
    {
        for (float i = 0.0f; i <= 1.0f;)
        {
            i += 0.2f;
            nextDayImage.color = new Color(0, 0, 0, i);
            nextDayText.color = new Color(1, 1, 1, i);
            yield return new WaitForSeconds(0.05f);
        }

        Day++;
        HP = maxhp;
        yield return new WaitForSeconds(1.3f);

        for (float i = 1.0f; i >= 0.0f;)
        {
            i -= 0.1f;
            nextDayImage.color = new Color(0, 0, 0, i);
            nextDayText.color = new Color(1, 1, 1, i);
            yield return new WaitForSeconds(0.05f);
        }

        nextDayText.gameObject.SetActive(false);
        nextDayImage.gameObject.SetActive(false);
    }
}
