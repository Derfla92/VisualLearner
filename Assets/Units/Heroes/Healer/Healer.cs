using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Animations;
using UnityEngine;

public class Healer : Hero
{


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
        base.Update();
    }

    public override void AquireTarget()
    {
        if (!FindHealingTarget())
        {
            base.AquireTarget();
        }
    }



    public bool FindHealingTarget()
    {
        Debug.Log("Finding healing target");
        List<Hero> healableTarget = unitHandler.heroes.FindAll(x => x.hitPoints <= x.maxHitPoints - GetComponent<Heal>().healingPower);
        if (healableTarget.Count > 0)
        {
            Debug.Log("Found healable target");
            int rand = Random.Range(0, healableTarget.Count - 1);
            target = healableTarget[rand];
            return true;
        }
        return false;
    }

    public override void StartCastSpell(Spell spell)
    {
        if (spell is FriendlySpell)
        {
            if (target is Hero)
            {
                currentSpell = spell;
                isCasting = true;
            }
            else
            {
                target = null;
            }
        }
    }


}
