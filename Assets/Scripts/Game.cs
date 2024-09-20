using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Game : MonoBehaviour
{
    public GameObject Player_CardPrefab;

    public TextMeshProUGUI scoreText;

    //Arrays of cards and values;
    public List<Sprite> cardSprites;
    public List<int> cardValues;

    public int score = 0;
    public int numCards = 0;

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
        scoreText.text = ""+score;
    }

    //If clicked hit
    public void Hit()
    {
        if (score < 21)
        {
            SpawnCard();
        }
    }

    public void Stay()
    {

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
        }
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

}
