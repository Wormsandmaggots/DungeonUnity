using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover
{
    //exp
    public int exp = 1;

    public float triggerLength = 1;
    public float chaselength = 5;
    private bool chasing;
    private bool collidinWithPlayer;
    private Transform playerTransform;
    private Vector3 startingPosition;

    //hitbox
    public ContactFilter2D filter;
    private BoxCollider2D hitbox;
    private Collider2D[] hits = new Collider2D[10];
    protected override void Start()
    {
        base.Start();
        playerTransform = GameManager.instance.player.transform;
        startingPosition = transform.position;
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }
    private void FixedUpdate()
    {
        //is the player in range
        if (Vector3.Distance(playerTransform.position, startingPosition) < chaselength)
        {
            if (Vector3.Distance(playerTransform.position, startingPosition) < triggerLength)
                chasing = true;
            if (chasing)
            {
                if (!collidinWithPlayer)
                {
                    UpdateMover((playerTransform.position - transform.position).normalized);
                }
            }
            else
            {
                UpdateMover(startingPosition - transform.position);
            }
        }
        else
        {
            UpdateMover(startingPosition - transform.position);
            chasing = false;
        }
        //check fo overlapse
        collidinWithPlayer = false;
        boxCollider.OverlapCollider(filter, hits);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
                continue;
            if(hits[i].tag=="Fighter"&&hits[i].name=="Player")
                collidinWithPlayer = true;

            hits[i] = null;
              
            
        }
    }
    protected override void Death()
    {
        Destroy(gameObject);
        GameManager.instance.GrantXp(exp);
        GameManager.instance.ShowText("+" + exp + "xp",30,Color.blue,transform.position,Vector3.up*40,1f);

    }
}
