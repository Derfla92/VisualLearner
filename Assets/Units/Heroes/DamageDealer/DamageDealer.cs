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
        base.Update();
    }

    

}
