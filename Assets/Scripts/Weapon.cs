using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class Weapon : Colidable
{
    //Damage struct
    public int[] damagePoint = { 1,2,3,4,5,6,7};
    public float[] pushForce = { 2.0f, 2.2f, 2.5f, 3f, 3.2f, 3.6f, 4f };

    //Upgrade
    public int weaponLevel = 0;
    private SpriteRenderer spriteRenderer;

    //swing 
    private Animator anim;
    private float cooldown = 0.5f;
    private float lastSwing;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(Time.time - lastSwing > cooldown)
            {
                lastSwing = Time.time;
                StartCoroutine(Swing());
            }
        }
    }

    private IEnumerator Swing()
    {
        anim.SetBool("swing", true);
        yield return 0.3f;
        anim.SetBool("swing", false);
    }

    protected override void OnCollide(Collider2D coll)
    {
        if(coll.tag == "Fighter")
        {
            if (coll.name == "Player")
                return;


            Damage dmg = new Damage
            {
                damageAmount = damagePoint[weaponLevel],
                origin = transform.position,
                pushForce = pushForce[weaponLevel]
            };

            coll.SendMessage("ReceiveDamage", dmg);

        }
        Debug.Log(coll);
    }

    public void UpgradeWeapon()
    {
        weaponLevel++;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
    }

    public void SetWeaponLevel(int level)
    {
        weaponLevel = level;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[(int)weaponLevel];
    }
}
