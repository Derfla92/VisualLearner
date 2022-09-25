using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonPunch : HostileSpell
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

    void OnTriggerEnter(Collider collider)
    {
        if(collider.GetComponent<Unit>())
        {
            collider.GetComponent<Unit>().TakeDamage(damage);
        }
        else if(collider.GetComponentInParent<Unit>())
        {
            collider.GetComponentInParent<Unit>().TakeDamage(damage);
        }
    }
}
