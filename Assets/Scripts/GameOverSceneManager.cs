using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverSceneManager : MonoBehaviour
{
    public TextMeshProUGUI coinText;

    void Start()
    {
        MainSceneManager.Instance.LoadCoinData();
        coinText.text = "Total Coins: " + MainSceneManager.Instance.coin.ToString();
    }

    void Update()
    {
        
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
    }
}
