using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Hero : Unit
{
    // Start is called before the first frame update
    public List<EventAction> actionQueue;
    public EventAction nextAction;
    public Vector3 destination;

    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        UiEvent nextEvent = gameManager.GetComponent<EventManager>().nextEvent;

        if (nextEvent != null)
        {

            if (nextAction == null)
            {
                if (actionQueue.Count > 0)
                {
                    if (actionQueue.First().GetComponent<EventAction>().relevantHeroes.Find(x => x == this))
                    {
                        nextAction = actionQueue.First();
                    }
                    actionQueue.Remove(nextAction);
                }
                else
                {
                    if (nextEvent.eventActions.Count > 0)
                    {
                        actionQueue = nextEvent.eventActions.ToList();
                        if (actionQueue.First().GetComponent<EventAction>().relevantHeroes.Find(x => x == this))
                        {
                            nextAction = actionQueue.First();
                        }
                        actionQueue.Remove(nextAction);
                    }
                }
                if (nextAction != null)
                {
                    if (nextEvent.eventTime < timeManager.currentTime + 2)
                    {
                        //nextAction.ExecuteAction(this);
                        nextAction = null;
                    }
                }
            }
            else
            {
                if (nextEvent.eventTime < timeManager.currentTime + 2)
                {
                    //nextAction.ExecuteAction(this);
                    nextAction = null;
                }
            }

        }



        base.Update();
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
        if (unitHandler.heroes.Count <= 0)
        {
            gameManager.GameOver();
        }

    }
}
