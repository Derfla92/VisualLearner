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
        base.Update();
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
        }
    }

    public override void AquireTarget()
    {
        base.AquireTarget();

        if (unitHandler.unitList.Count > 0)
        {
            int random = Random.Range(0, unitHandler.unitList.Count - 1);
            target = unitHandler.unitList[random];
        }
    }

    public override void Attack()
    {
        base.Attack();
        
        

    }
}
