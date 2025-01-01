using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GManager : MonoBehaviour
{
    public TextMeshProUGUI coinText;

    void Start()
    {
        coinText.text = "Coins : " + MainSceneManager.Instance.coin.ToString();
    }

    void Update()
    {
        
    }

    public void addCoin()
    {
        MainSceneManager.Instance.coin += 1;
        coinText.text = "Coins : " + MainSceneManager.Instance.coin.ToString(); 
    }

    public void SaveData()
    {
        MainSceneManager.Instance.SaveCoinData(); 
    }

    public void LoadData()
    {
        MainSceneManager.Instance.LoadCoinData(); 
        coinText.text = MainSceneManager.Instance.coin.ToString();
    }
}
