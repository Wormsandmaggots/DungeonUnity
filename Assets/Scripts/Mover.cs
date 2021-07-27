using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : Fighter
{
    protected  BoxCollider2D boxCollider;
    protected Vector3 moveDelta;
    protected RaycastHit2D hit;
    protected float ySpeed = 0.75f;
    protected float xSpeed = 1f;
    protected virtual void Start()
    {
        //getting components
        boxCollider = GetComponent<BoxCollider2D>();
    }
    
    protected virtual void UpdateMover(Vector3 input)
    {
        moveDelta = new Vector3(input.x*xSpeed,input.y*ySpeed,0);

        //changing direction to left or right
        //also can use Vector2(1,180,1) for example
        if (moveDelta.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (moveDelta.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        
        //add push vector
        moveDelta += pushDirection;

        //reduce push force every frame
        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed);

        // if hit in something in y axis
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            //moving forward
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }

        //same but in x axis
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            //moving forward
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }
    }
}
