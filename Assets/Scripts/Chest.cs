using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Chest : Collectable
{
    public Sprite emptyChest;
    public int pesosAmount = 5;

    protected override void OnCollect()
    {
        if (!collected)
        {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            GameManager.instance.coins += pesosAmount;
            GameManager.instance.ShowText("+" + pesosAmount + "coins!", 25, Color.yellow, transform.position, Vector3.up * 25, 3.0f);
        }
    }
}
