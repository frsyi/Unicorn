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
        GameManager.Instance.LoadCoinData();
        coinText.text = "Total Coins: " + GameManager.Instance.coin.ToString();
    }

    void Update()
    {
        
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
