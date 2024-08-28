using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class MenuManeger : MonoBehaviour
{
    [SerializeField] Text text;
    [SerializeField] GameObject obj;
    bool show=false;
    private void Start()
    {
        DataHolder.coins = PlayerPrefs.GetInt("manys");
    }

    private void Update()
    {
        if (text != null)
        {
            text.text = (DataHolder.coins+DataHolder.Doncoins).ToString();
        }
    }
    public void GoToScene(int num)
    {
        SceneManager.LoadScene(num);
        GameManager.LikeGameOver();
    }
    public void ShowObject()
    {
        if (show==false)
        {
            show = true;
            obj.SetActive(true);
        }
        else
        {
            show = false;
            obj.SetActive(false);
        }
    }
}
