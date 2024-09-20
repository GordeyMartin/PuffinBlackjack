using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int value;
    public SpriteRenderer spriteRenderer;

    public void SetCard(Sprite image, int value)
    {
        this.spriteRenderer.sprite = image; 
        this.value = value;
    }
}
