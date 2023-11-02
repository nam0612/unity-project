using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealingFoutain : Colidable
{
    public int healingAmount = 1;

    private float healoCooldown = 1.0f;

    private float lastHeal;


    protected override void OnCollide(Collider2D coll)
    {
        if(coll.name != "Player")
        {
            return;
        }

        if(Time.time - lastHeal > healoCooldown)
        {
            lastHeal = Time.time;
            GameManager.instance.player.Heal(healingAmount);
        }
    }
}
