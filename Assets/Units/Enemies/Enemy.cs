using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    public override void Awake()
    {
        base.Awake();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void AquireTarget()
    {
        if (unitHandler.heroes.Count > 0)
        {
            if (unitHandler.heroes.Find(x => x.role == Unit.Role.Tank))
            {
                target = unitHandler.heroes.Find(x => x.role == Unit.Role.Tank);
            }
            else
            {
                int random = Random.Range(0, unitHandler.heroes.Count - 1);
                target = unitHandler.heroes[random];

            }
        }
    }

    public override void Die()
    {
        unitHandler.enemies.Remove(this);
        if (TryGetComponent<Animator>(out Animator animator))
        {
            Debug.Log("playing anim");
            animator.Play("Die");
        }
    }
}
