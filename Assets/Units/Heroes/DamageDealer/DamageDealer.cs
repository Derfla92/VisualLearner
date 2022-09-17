using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : Hero
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        
    }

    public override void Awake()
    {
        role = Role.DamageDealer;
    }

    // Update is called once per frame
    public override void Update()
    {
        if (timeManager.run)
        {
            base.Update();
            if(target == null)
            {
                AquireTarget();
                Debug.Log("Getting target: " + target.name);
            }
            else
            {
                Debug.Log("attacking target: " + target.name);
                Attack();
            }
        }
    }

    public override void AquireTarget()
    {
        if (unitHandler.unitList.Count > 0)
        {
            Debug.Log("List is > 0");
            foreach (Unit unit in unitHandler.unitList)
            {
                target = unit as Boss;
                if(target != null)
                {
                    break;
                }
            }
            
        }
    }

}
