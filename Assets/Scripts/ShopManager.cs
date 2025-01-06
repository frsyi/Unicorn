using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public int currentCharacterIndex;
    public GameObject[] characterModels;

    public CharacterData[] characters;
    public Button buyButton;
    
    void Start()
    {
        foreach (CharacterData character in characters)
        {
            if (character.price == 0)
            {
                character.isUnlocked = true;
            } else
            {
                character.isUnlocked = PlayerPrefs.GetInt(character.name, 0) == 0 ? false: true;
            }
        }
        
        currentCharacterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);
        foreach (GameObject character in characterModels)
        {
            character.SetActive(false);
        }
        characterModels[currentCharacterIndex].SetActive(true);
    }

    void Update()
    {
        UpdateUI();
    }

    public void ChangeNext()
    {
        characterModels[currentCharacterIndex].SetActive(false);
        currentCharacterIndex++;
        if (currentCharacterIndex == characterModels.Length)
        {
            currentCharacterIndex = 0;
        }
        characterModels[currentCharacterIndex].SetActive(true);
        PlayerPrefs.SetInt("SelectedCharacter", currentCharacterIndex);
    }

    public void ChangePrevious()
    {
        characterModels[currentCharacterIndex].SetActive(false);
        currentCharacterIndex--;
        if (currentCharacterIndex == -1)
        {
            currentCharacterIndex = characterModels.Length - 1;
        }
        characterModels[currentCharacterIndex].SetActive(true);
        PlayerPrefs.SetInt("SelectedCharacter", currentCharacterIndex);
    }
    
    private void UpdateUI()
    {
        CharacterData c = characters[currentCharacterIndex];
        if (c.isUnlocked)
        {
            buyButton.gameObject.SetActive(false);
        } 
        else 
        {
            buyButton.gameObject.SetActive(true);
            buyButton.GetComponentInChildren<TextMeshProUGUI>().text = "Buy-" + c.price;
            
            if (c.price < PlayerPrefs.GetInt("TotalCoins", 0))
            {
                buyButton.interactable = true;
            }
            else
            {
                buyButton.interactable = false;
            }
        }
    }
}
