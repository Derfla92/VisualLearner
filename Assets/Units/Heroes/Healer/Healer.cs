using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : Hero
{

    public int healingPower;
    public float coolDown;
    public float lastHeal;
    

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        
    }

    public override void Awake()
    {
        role = Role.Healer;
    }

    // Update is called once per frame
    public override void Update()
    {
        if (timeManager.run)
        {
            base.Update();
            if (lastHeal + coolDown < timeManager.currentTime)
            {
                if (target == null)
                {
                    FindHealingTarget();
                    Heal();
                    target = null;
                }
            }
        }
    }

    public void Heal()
    {
        if (target != null)
        {
            target.hitPoints += healingPower;
            target.GetComponent<Hero>().UpdateHealthBar();
            lastHeal = timeManager.currentTime;
        }

    }

    public void FindHealingTarget()
    {
        List<Unit> healableTarget = unitHandler.unitList.FindAll(x => x.hitPoints < 70);
        if (healableTarget.Count > 0)
        {
            int rand = Random.Range(0, healableTarget.Count - 1);
            target = healableTarget[rand];
        }
    }
}
