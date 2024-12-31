using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    void Start()
    {
        scoreText.text = "Score : " + MainManager.Instance.score.ToString();
    }

    void Update()
    {
        
    }

    public void addScore()
    {
        scoreText.text = MainManager.Instance.score.ToString(); 
    }

    public void SaveData()
    {
        MainManager.Instance.SaveScoreData(); 
    }

    public void LoadData()
    {
        MainManager.Instance.LoadScoreData(); //memanggil metode LoadScoreData() dari MainManager untuk memuat skor dari file JSON
        scoreText.text = MainManager.Instance.score.ToString();
    }
}
