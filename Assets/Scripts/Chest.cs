using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    public Sprite emptyChest;
    public int money = 5;

    protected override void OnCollect()
    {

        if(!collected)
        {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            GameManager.instance.money += money;
            GameManager.instance.ShowText("+" + money + " coins",20,Color.white,transform.position,Vector3.up * 25,1.5f);

        }
    }

}
