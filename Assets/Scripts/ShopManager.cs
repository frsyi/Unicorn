using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public int currentCharacterIndex;
    public GameObject[] characterModels;
    public TextMeshProUGUI coinText;
    public CharacterData[] characters;
    public Button buyButton;

    void Start()
    {
        GameManager.Instance.LoadCoinData();
        coinText.text = "Coins: " + GameManager.Instance.coin.ToString();

        foreach (CharacterData character in characters)
        {
            if (character.price == 0)
            {
                character.isUnlocked = true;
            }
            else
            {
                character.isUnlocked = PlayerPrefs.GetInt(character.name, 0) == 0 ? false : true;
            }
        }

        currentCharacterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);
        ActivateCharacterModel(currentCharacterIndex);
        UpdateUI();
    }

    private void ActivateCharacterModel(int index)
    {
        foreach (GameObject character in characterModels)
        {
            character.SetActive(false);
        }
        characterModels[index].SetActive(true);
    }

    public void ChangeNext()
    {
        currentCharacterIndex = (currentCharacterIndex + 1) % characterModels.Length;
        ActivateCharacterModel(currentCharacterIndex);
        PlayerPrefs.SetInt("SelectedCharacter", currentCharacterIndex);
        PlayerPrefs.Save();
        UpdateUI();
    }

    public void ChangePrevious()
    {
        currentCharacterIndex--;
        if (currentCharacterIndex < 0)
        {
            currentCharacterIndex = characterModels.Length - 1;
        }
        ActivateCharacterModel(currentCharacterIndex);
        PlayerPrefs.SetInt("SelectedCharacter", currentCharacterIndex);
        PlayerPrefs.Save();
        UpdateUI();
    }

    public void UnlockCharacter()
    {
        CharacterData c = characters[currentCharacterIndex];
        int totalCoins = GameManager.Instance.coin;

        if (totalCoins >= c.price && !c.isUnlocked)
        {
            c.isUnlocked = true;
            PlayerPrefs.SetInt(c.name, 1);
            GameManager.Instance.coin -= c.price;
            GameManager.Instance.SaveCoinData();
            PlayerPrefs.SetInt("SelectedCharacter", currentCharacterIndex);
            UpdateUI();
        }
        else
        {
            Debug.Log("Not enough coins or character already unlocked!");
        }
    }

    private void UpdateUI()
    {
        CharacterData c = characters[currentCharacterIndex];
        coinText.text = "Coins: " + GameManager.Instance.coin.ToString();

        PlayerPrefs.SetInt("SelectedCharacter", currentCharacterIndex);
        PlayerPrefs.Save();

        if (c.isUnlocked)
        {
            buyButton.gameObject.SetActive(false);
        }
        else
        {
            buyButton.gameObject.SetActive(true);
            buyButton.GetComponentInChildren<TextMeshProUGUI>().text = "Buy - " + c.price;
            buyButton.interactable = (GameManager.Instance.coin >= c.price);
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
}
