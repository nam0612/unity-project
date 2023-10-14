using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Colidable : MonoBehaviour
{
    private ContactFilter2D filter;

    private BoxCollider2D boxCollider;
    private Collider2D[] hits = new Collider2D[10];

    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void Update()
    {
        //collision work
        boxCollider.OverlapCollider(filter, hits);
        for(int i  = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
                continue;
            OnCollide(hits[i]);

            // the array is not cleaned up, so we do it yourself
            hits[i] = null;
        }
    }

    protected virtual void OnCollide(Collider2D coll)
    {
        Debug.Log("oke");
    }
}
