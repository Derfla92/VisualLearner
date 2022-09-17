using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Unit
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
        if (timeManager.run)
        {
            if (target == null)
            {
                AquireTarget();
            }
            else
            {

                Attack();
            }
        }
        base.Update();
    }

    public override void AquireTarget()
    {


        if (unitHandler.unitList.Count > 0)
        {
            if (unitHandler.unitList.Find(x => x.GetType() == typeof(Tank)) != null)
            {
                target = unitHandler.unitList.Find(x => x.GetType() == typeof(Tank));
            }
            else
            {
                int random = Random.Range(0, unitHandler.unitList.Count - 1);
                target = unitHandler.unitList[random];

            }
        }
    }

    public override void Attack()
    {
        if (timeManager.currentTime > lastAttack + attackSpeed)
        {
            Animator animator = GetComponent<Animator>();

            animator.Play(GetComponent<MeleeAttack>().attackAnim.name);

            target.GetComponent<Hero>().TakeDamage(attackDamage);
            lastAttack = timeManager.currentTime;
            if (target.hitPoints < 0)
            {
                target = null;
            }
        }
    }
}
