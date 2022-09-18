using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Animations;
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
        base.Update();
        if (timeManager.run)
        {
            if (lastHeal + coolDown < timeManager.currentTime)
            {
                FindHealingTarget();
                if (target != null)
                {
                    if (target.role != Unit.Role.Boss)
                    {
                        Heal();
                        target = null;
                    }
                }
            }
        }
    }

    public void Heal()
    {
        if (target != null)
        {
            Animator animator = GetComponent<Animator>();
            animator.Play("Heal");
            Debug.Log("Healing: " + target.name);

            List<Spell> spells = GetComponents<Spell>().ToList();

            Spell spell = spells.Find(x => x.spellName == "Heal");

            Transform heal = Instantiate(spell.prefab).transform;

            spell.instantiatedObject.Add(heal.gameObject);
            heal.position = target.transform.position;
            
            

            target.hitPoints += healingPower;
            target.GetComponent<Hero>().UpdateHealthBar();
            lastHeal = timeManager.currentTime;
        }

    }

    public void FindHealingTarget()
    {
        List<Hero> healableTarget = unitHandler.heroes.FindAll(x => x.hitPoints < x.maxHitPoints - healingPower);
        if (healableTarget.Count > 0)
        {
            Debug.Log("Found healable target");
            int rand = Random.Range(0, healableTarget.Count - 1);
            target = healableTarget[rand];
        }
    }
}
