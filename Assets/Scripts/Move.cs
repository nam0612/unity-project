using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public abstract class Move : Fighter
{
    protected BoxCollider2D boxCollider;
    private Vector3 moveDelta;
    private RaycastHit2D hit;
    public float ySpeed = 0.75f;
    public float xSpeed = 1.0f;


    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    //private void FixedUpdate()
    //{
    //    float x = Input.GetAxisRaw("Horizontal");
    //    float y = Input.GetAxisRaw("Vertical");
    //}

    protected virtual void UpdateMotor(Vector3 input)
    {
        //reset movedelta
        moveDelta = new Vector3(input.x * xSpeed, input.y * ySpeed, 0);

        //swap sprite direction, wether you're going right or left
        if (moveDelta.x > 0)
            transform.localScale = Vector3.one;
        else if (moveDelta.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask());
        if(hit.collider == null)
        {
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.x), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask());
        if (hit.collider == null)
        {
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }
    }
}
