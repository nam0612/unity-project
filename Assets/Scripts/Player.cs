using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Move
{
    private SpriteRenderer spriteRenderer;
    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        UpdateMotor(new Vector3 (x, y, 0));
    }

    public void SwapSprite(int skinId)
    {
        spriteRenderer.sprite = GameManager.instance.playerSprites[skinId];
    }

    public void OnLevelUP()
    {
        maxHitpoint++;
        hitpoint = maxHitpoint;
        transform.position = GameObject.Find("SpawPoint").transform.position;
    }

    public void SetLevel(int level)
    {
        for(int i = 0; i<  level; i++)
        {
            OnLevelUP();
        }

    }

    public void Heal(int healingAmount)
    {
        if (hitpoint == maxHitpoint)
            return;
        hitpoint += healingAmount;
        if(hitpoint > maxHitpoint) 
            hitpoint = maxHitpoint;
        GameManager.instance.ShowText("+" + healingAmount.ToString() + "hp", 25, Color.green, transform.position, Vector3.up * 30, 1.0f) ;
        GameManager.instance.OnHitpointChange();
    }

    protected override void ReceiveDamage(Damage dmg)
    {
        base.ReceiveDamage(dmg);
        GameManager.instance.OnHitpointChange();
    } 

}
