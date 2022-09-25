using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hero : Unit
{
    // Start is called before the first frame update
    public List<EventAction> nextActions;
    
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        nextActions = gameManager.GetComponent<EventManager>().nextEvent.eventActions;
        base.Update();
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        //Debug.Log(this.name + " was hit by spell");
    }

    public override void AquireTarget()
    {
        if (unitHandler.enemies.Count > 0)
        {
            target = unitHandler.enemies.Find(x => x.role == Unit.Role.Boss);
        }
    }

    

    public override void Die()
    {

        unitHandler.heroes.Remove(this);

        if (TryGetComponent<Animator>(out Animator animator))
        {
            animator.Play("Die");
        }
        Destroy(GetComponent<Hero>());
        if(unitHandler.heroes.Count <= 0)
        {
            gameManager.GameOver();
        }

    }
}
