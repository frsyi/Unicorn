using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainSceneManager : MonoBehaviour
{
    public TextMeshProUGUI coinText;

    void Start()
    {
        GameManager.Instance.LoadCoinData();
        coinText.text = "Coins : " + GameManager.Instance.coin.ToString();
    }

    void Update()
    {

    }

    public void addCoin()
    {
        GameManager.Instance.coin += 1;
        coinText.text = "Coins : " + GameManager.Instance.coin.ToString();
    }

    public void SaveData()
    {
        GameManager.Instance.SaveCoinData();
    }

    public void LoadData()
    {
        GameManager.Instance.LoadCoinData();
        coinText.text = GameManager.Instance.coin.ToString();
    }
}
