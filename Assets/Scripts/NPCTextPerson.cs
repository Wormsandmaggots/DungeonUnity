using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTextPerson : Collidable
{
    public string Message;
    private float Cooldown = 4f;
    private float LastShout;

    protected override void Start()
    {
        base.Start();
        LastShout = -Cooldown;
    }
    protected override void OnCollide(Collider2D coll)
    {
        if (Time.time - LastShout > Cooldown)
        {
            LastShout = Time.time;
            GameManager.instance.ShowText(Message, 20, Color.white, transform.position+new Vector3(0,0.16f,0), Vector3.zero, Cooldown);
        }

    }
}

