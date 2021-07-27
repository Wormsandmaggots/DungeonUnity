using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    public int [] damagePoint = {1,2,3,4,5,6};
    public float[] pushForce = {2f,2.2f,2.3f,2.5f,2.6f,2.7f };

    public int weaponLevel=0;
    public SpriteRenderer spriteRenderer;

    
    private float lastSwing;
    public AnimationClip anim;
    private Animator animator;
    private float cooldown;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    protected override void Update()
    {
        base.Update();
        cooldown = anim.length;
        if (Input.GetMouseButtonDown(0))
        {
            if(Time.time-lastSwing>cooldown)
            {
                lastSwing = Time.time;
                Swing();
            }
        }
    }
    protected override void OnCollide(Collider2D coll)
    {
        if(coll.tag=="Fighter")
        {
            if(coll.name=="Player")
                return;
            Damage dmg = new Damage
            {
                damageAmount = damagePoint[weaponLevel],
                origin = transform.position,
                pushForce = pushForce[weaponLevel],
            };

            coll.SendMessage("ReceiveDamage", dmg);
            Debug.Log(coll.name);
        }
        
    }
    private void Swing()
    {
        animator.SetTrigger("Swing");
    }

    public void upgradeWeapon()
    {
        weaponLevel++;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
        //change stats

    }

    public void setWeaponLevel(int level)
    {
        weaponLevel = level;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
    }
}
