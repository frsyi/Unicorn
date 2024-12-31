using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;
    public int score;

    private void Awake()
    {
        if (Instance != null) 
        {
            Destroy(gameObject);
            return;
        }
        Instance = this; 
        DontDestroyOnLoad(gameObject);
    }

    [Serializable]
    public class ScoreData
    {
        public int scoreX;
    }

    public void SaveScoreData()
    {
        ScoreData data = new ScoreData();
        data.scoreX = score; 

        string json = JsonUtility.ToJson(data); 

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);   
    }

    public void LoadScoreData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path); 

            ScoreData data = JsonUtility.FromJson<ScoreData>(json); 
            score = data.scoreX; 
        }
    }
}
