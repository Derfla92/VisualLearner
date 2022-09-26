using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raynug : Boss
{


    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        if (timeManager.run)
        {
            if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Sleep"))
            {
                GetComponent<Animator>().Play("Idle");
            }
        }
        base.Update();
    }
}