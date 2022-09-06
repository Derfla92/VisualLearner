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

    // Update is called once per frame
    public override void Update()
    {

        if (target == null)
        {
            AquireTarget();
        }
        else
        {
            if (timeManager.currentTime > lastAttack + attackSpeed)
            {
                Attack();
            }
            base.Update();
        }
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
            base.AquireTarget();
        }
    }

    public override void Attack()
    {
        base.Attack();
    }
}