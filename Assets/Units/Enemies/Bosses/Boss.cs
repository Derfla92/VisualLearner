using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    public override void Awake()
    {
        role = Role.Boss;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();


    }



    public override void Attack()
    {
        Animator animator = GetComponent<Animator>();

        animator.Play(GetComponent<MeleeAttack>().attackAnim.name);

        target.GetComponent<Hero>().TakeDamage(attackDamage);
        if (target.hitPoints < 0)
        {
            target = null;
        }
        swingTimer = attackSpeed;

    }



    public override void Die()
    {
        base.Die();
        gameManager.GameOver();
    }

}
