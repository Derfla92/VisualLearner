using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Unit
{
    private Dictionary<int, string> attacks = new Dictionary<int, string>();

    // Start is called before the first frame update
    public override void Start()
    {
        attacks.Add(0, "Attack");
        attacks.Add(1, "Tail");
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
            int random = Random.Range(0, unitHandler.unitList.Count - 1);
            target = unitHandler.unitList[random];
            base.AquireTarget();
        }
    }

    public override void Attack()
    {
        int random = Random.Range(0, 2);
        GetComponent<Animator>().SetTrigger(attacks[random]);
        base.Attack();




    }
}
