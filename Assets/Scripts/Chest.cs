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
            GameManager.instance.ShowText("+" + pesosAmount + "coins!", 25, Color.yellow, transform.position, Vector3.up * 50, 3.0f);
        }
    }


    //    protected void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        //collected = true;
    //        GetComponent<SpriteRenderer>().sprite = emptyChest;
    //        pesosAmount += 5;
    //        // GetComponent<Animator>().enabled = true;
    //        Debug.Log(pesosAmount.ToString());
    //    }

    //}
}
