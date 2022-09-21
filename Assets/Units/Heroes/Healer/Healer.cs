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

        List<Hero> healableTarget = unitHandler.heroes.FindAll(x => x.hitPoints < x.maxHitPoints);
        if (healableTarget.Count > 0)
        {
            healableTarget.OrderBy(x => x.hitPoints);
            Debug.Log("Found healable target");
            target = healableTarget.First();
            return true;
        }

        return false;
    }

}
