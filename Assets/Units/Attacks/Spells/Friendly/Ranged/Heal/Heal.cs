using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : FriendlySpell
{

    public int healingPower;

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

    public override void ApplySpellEffect(Unit target)
    { 
        target.GetHealed(healingPower);  
    }

    public override void PlayAnimation()
    {
        Animator animator = GetComponent<Animator>();
        animator.Play("Heal");
    }
}
