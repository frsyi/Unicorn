using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelector : MonoBehaviour
{
    public int currentCharacterIndex;
    public GameObject[] characters;

    void Start()
    {
        currentCharacterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);
        SetActiveCharacter();
    }

    public void SetActiveCharacter()
    {
        foreach (GameObject character in characters)
        {
            character.SetActive(false);
        }
        characters[currentCharacterIndex].SetActive(true);
    }
}
