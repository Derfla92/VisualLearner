using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonPunch : HostileSpell
{

    public List<Unit> heroesInArea;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    public override void Awake()
    {
        heroesInArea = new List<Unit>();
        base.Awake();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<Hero>())
        {
            heroesInArea.Add(collider.GetComponent<Hero>());
        }
        else if(collider.GetComponentInParent<Hero>())
        {
            heroesInArea.Add(collider.GetComponentInParent<Hero>());
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.GetComponent<Hero>())
        {
            heroesInArea.Remove(collider.GetComponent<Hero>());
        }
        else if (collider.GetComponentInParent<Hero>())
        {
            heroesInArea.Remove(collider.GetComponentInParent<Hero>());
        }
    }

    public override void ApplySpellEffect()
    {
        foreach (Hero hero in heroesInArea)
        {
            hero.TakeDamage(damage);
        }
    }

    public override void ApplySpellEffect(Unit target)
    {
        ApplySpellEffect();
    }
}
