using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainScenebutton : MonoBehaviour
{
    public string loadScene;

    Button mainSceneButton;

    private void Awake()
    {
        mainSceneButton = GetComponent<Button>();
        mainSceneButton.onClick.AddListener(ShopButton);
    }

    void ShopButton()
    {
        SceneManager.LoadScene(loadScene);
    }

}
