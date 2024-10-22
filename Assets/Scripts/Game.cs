using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Game : MonoBehaviour
{
    public GameObject Player_CardPrefab;

    //Arrays of cards and values of the Player;
    public List<Sprite> cardSprites;
    public List<int> cardValues;

    //Arrays of cards and values of the Dealer;
    public List<Sprite> cardSpritesDealer;
    public List<int> cardValuesDealer;

    //Score of the player
    public int score = 0;
    public TextMeshProUGUI scoreText;

    //Score of the dealer
    public int dealerScore = 0;
    public TextMeshProUGUI dealerText;

    //Number of cards played
    public int numCards = 0;
    public int numCardsDealer = 0;

    public GameObject playerWonPanel;
    public GameObject playerLostPanel;
    public GameObject cardPanel;

    public int cardBack;

    // Start is called before the first frame update
    void Start()
    {
        cardBack = PlayerPrefs.GetInt("cardBack", 0);
        //Spawn 2 cards in the beginning.
        SpawnCard();
        SpawnCard();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "" + score;
        dealerText.text = "" + dealerScore;
        Debug.Log(cardBack);
    }

    public void changeCardPanel()
    {
        cardPanel.SetActive(true);
    }
    public void changeCardPanelDeactivate()
    {
        cardPanel.SetActive(false);
    }
    //3 Buttons to change the cards' backs.
    public void changeCardBack0()
    {
        cardBack = 0;
        PlayerPrefs.SetInt("cardBack", cardBack);
        PlayerPrefs.Save();
        SceneManager.LoadScene(1);
    }
    public void changeCardBack1()
    {
        cardBack = 1;
        PlayerPrefs.SetInt("cardBack", cardBack);
        PlayerPrefs.Save();
        SceneManager.LoadScene(1);
    }
    public void changeCardBack2()
    {
        cardBack = 2;
        PlayerPrefs.SetInt("cardBack", cardBack);
        PlayerPrefs.Save();
        SceneManager.LoadScene(1);
    }

    //If clicked hit
    public void Hit()
    {
        if (score < 21)
        {
            SpawnCard();
        }
    }
    //If Player stays, he makes dealer move.
    public void Stay()
    {
        DealersMove();
    }

    public void SpawnCard()
    {
        if (numCards <= 3)
        {
            Vector3 startDeckPosition = new Vector3(807f, -2f, 0f);
            Vector3 spawnPosition = new Vector3(-519 + numCards * 270f, -388f, 0f);

            GameObject cardObject = Instantiate(Player_CardPrefab, startDeckPosition, Quaternion.identity);

            StartCoroutine(MoveCard(cardObject, startDeckPosition, spawnPosition, 1f));

            int randomIndex = cardBack * 13 + Random.Range(0, 12);

            Card card = cardObject.GetComponent<Card>();
            card.SetCard(cardSprites[randomIndex], cardValues[randomIndex]);

            if (randomIndex == 12 | randomIndex == 25 || randomIndex == 38)
            {
                if (score < 11)
                {
                    score += 11;
                }
                else
                {
                    score += 1;
                }
            }
            else
            {
                score += cardValues[randomIndex];
            }

            cardSprites.RemoveAt(randomIndex);
            cardValues.RemoveAt(randomIndex);

            numCards++;
            if (score > 21)
            {
                playerLose();
            }
            if (score == 21)
            {
                playerWin();
            }
        }
    }

    public void playerWin()
    {
        playerWonPanel.SetActive(true);
    }

    public void playerLose()
    {
        playerLostPanel.SetActive(true);
    }

    //Button to go back to menu
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void DealersMove()
    {
        while (dealerScore <= score && dealerScore <= 21)
        {
            spawnEnemyCard();
        }
        if (dealerScore > 21 || dealerScore < score)
        {
            playerWin();
        }
        else
        {
            playerLose();
        }
    }

    public void spawnEnemyCard()
    {
        if (numCardsDealer <= 3)
        {
            Vector3 startDeckPosition = new Vector3(807f, -2f, 0f);
            Vector3 spawnPosition = new Vector3(-519 + numCardsDealer * 270f, 376f, 0f);

            GameObject cardObject = Instantiate(Player_CardPrefab, startDeckPosition, Quaternion.identity);

            StartCoroutine(MoveCard(cardObject, startDeckPosition, spawnPosition, 1f));

            int randomIndex = Random.Range(0, cardSpritesDealer.Count);

            Card card = cardObject.GetComponent<Card>();
            card.SetCard(cardSpritesDealer[randomIndex], cardValuesDealer[randomIndex]);

            dealerScore += cardValuesDealer[randomIndex];

            cardSpritesDealer.RemoveAt(randomIndex);
            cardValuesDealer.RemoveAt(randomIndex);

            numCardsDealer++;
        }
    }

    public IEnumerator MoveCard(GameObject cardObject, Vector3 start, Vector3 end, float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            cardObject.transform.position = Vector3.Lerp(start, end, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        cardObject.transform.position = end;
    }
}
