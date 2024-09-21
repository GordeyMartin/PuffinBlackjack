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

    // Start is called before the first frame update
    void Start()
    {
        //Spawn 2 cards in the beginning.
        SpawnCard();
        SpawnCard();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "" + score;
        dealerText.text = "" + dealerScore;
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
            Vector3 spawnPosition = new Vector3(500 + numCards * 250f, 200f, 0f);

            GameObject cardObject = Instantiate(Player_CardPrefab, spawnPosition, Quaternion.identity);

            int randomIndex = Random.Range(0, cardSprites.Count);

            Card card = cardObject.GetComponent<Card>();
            card.SetCard(cardSprites[randomIndex], cardValues[randomIndex]);

            score += cardValues[randomIndex];
            Debug.Log(score);

            cardSprites.RemoveAt(randomIndex);
            cardValues.RemoveAt(randomIndex);

            numCards++;
            Debug.Log(numCards);
        }
    }
    //Button to go back to menu
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void DealersMove()
    {
        while (dealerScore < score && dealerScore <= 21)
        {
            spawnEnemyCard();
        }
    }

    public void spawnEnemyCard()
    {
        if (numCardsDealer <= 3)
        {
            Vector3 spawnPosition = new Vector3(500 + numCardsDealer * 250f, 800f, 0f);

            GameObject cardObject = Instantiate(Player_CardPrefab, spawnPosition, Quaternion.identity);

            int randomIndex = Random.Range(0, cardSpritesDealer.Count);

            Card card = cardObject.GetComponent<Card>();
            card.SetCard(cardSpritesDealer[randomIndex], cardValuesDealer[randomIndex]);

            dealerScore += cardValuesDealer[randomIndex];
            Debug.Log(dealerScore);

            cardSpritesDealer.RemoveAt(randomIndex);
            cardValuesDealer.RemoveAt(randomIndex);

            numCardsDealer++;
            Debug.Log(numCardsDealer);
        }
    }

}
