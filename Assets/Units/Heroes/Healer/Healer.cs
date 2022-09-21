using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Animations;
using UnityEngine;

public class Healer : Hero
{

    private Dictionary<Unit.Role,float> weight = new Dictionary<Unit.Role,float>
    {
        [Unit.Role.Tank] = 0.6f,
        [Unit.Role.Healer] = 0.5f,
        [Unit.Role.DamageDealer] = 0.4f,
    };


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
            if (FindHealingTarget())
            {
                SpellCaster spellCaster = GetComponent<SpellCaster>();
                if (!spellCaster.isCasting)
                {
                    spellCaster.TryCastSpell();
                }
                else
                {
                    spellCaster.UpdateCastTime();
                }
            }
            else
            {
                if (target == null)
                {
                    AquireTarget();
                }
                else
                {
                    if (!target.GetComponent<Hero>())
                    {
                        if (target.hitPoints > 0)
                        {
                            RotateTowardsTarget();
                            TryAttack();
                        }
                    }
                }
            }
        }
    }

    public override void AquireTarget()
    {
        if (!FindHealingTarget())
        {
            base.AquireTarget();
        }
    }



    //public bool FindHealingTarget()
    //{

    //    List<Hero> healableTarget = unitHandler.heroes.FindAll(x => x.hitPoints <= x.maxHitPoints - GetComponent<SpellCaster>().spells.Find(x => x.GetComponent<Heal>()).GetComponent<Heal>().healingPower);
    //    if (healableTarget.Count > 0)
    //    {
    //        Debug.Log("Found healable target");
    //        int rand = Random.Range(0, healableTarget.Count - 1);
    //        target = healableTarget[rand];
    //        return true;
    //    }

    //    return false;
    //}

    public bool FindHealingTarget()
    {

        List<Hero> healableTargets = unitHandler.heroes.FindAll(x => x.hitPoints < x.maxHitPoints);
        if (healableTargets.Count > 0)
        {
            healableTargets = healableTargets.OrderBy(x => ((float)x.hitPoints / (float)x.maxHitPoints) * weight[x.role]).ToList();
            //Debug.Log("--------------------------");
            //foreach (var item in healableTargets) 
            //{
            //    Debug.Log(item.name + ": " + ((float)item.hitPoints / (float)item.maxHitPoints) * weight[item.role]);
            //}
            target = healableTargets.First();
            return true;
        }

        return false;
    }

}
