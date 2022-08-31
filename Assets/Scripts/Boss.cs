using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Unit
{


    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        AquireTarget();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void AquireTarget()
    {
        base.AquireTarget();
        int random = Random.Range(0, unitHandler.unitList.Count-1);
        target = unitHandler.unitList[random];
    }
}
