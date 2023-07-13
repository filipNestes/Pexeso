using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [HideInInspector]public int pairAmount;
    public Sprite[] spriteList;

    float offSet = 1.2f; //OFFSET BETWEEN THE CARDS
    public GameObject cardPrefab;

    List<GameObject> cardDeck = new List<GameObject>();
    public int width;
    public int height;


    // Start is called before the first frame update
    void Start() {
        GameManager.instance.SetPairs(pairAmount);
        CreatePlayField();
    }

    void CreatePlayField() {
        for (int i = 0; i < pairAmount; i++) {
            for (int j = 0; j < 2; j++)
            {
                Vector3 pos = Vector3.zero;
                GameObject newCard = Instantiate(cardPrefab, pos, Quaternion.identity);
                newCard.GetComponent<Card>().SetCard(i,spriteList[i]);
                cardDeck.Add(newCard);
            }
        } 
        // SHUFFLE CARDS
        for (int i = 0; i < cardDeck.Count; i++)
        {
            int index = Random.Range(0, cardDeck.Count);
            var temp = cardDeck[i];
            cardDeck[i] = cardDeck[index];
            cardDeck[index] = temp;
        }  
        // PASS OUT CARDS ON FIELD
        int num = 0;
        for (int x = 0; x < width; x++) {
            for (int z = 0; z < height; z++) {
                Vector3 pos = new Vector3(x * offSet, 0, z * offSet);
                cardDeck[num].transform.position = pos;
                num++;
            }   
        }
    }

    void OnDrawGitmos() {
        if (pairAmount*2 != width * height) {
            Debug.Log("ERROR: width * height should be pairAmount * 2");
        }
        if (pairAmount > spriteList.Length) {
            Debug.Log("to much pairs");
        }
    }

 
}
