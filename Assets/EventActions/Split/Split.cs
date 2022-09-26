using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Split : EventAction
{

    public Vector3 destination;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void ExecuteAction(Hero hero)
    {
        Vector3 currentPos = hero.transform.position;
        Move(hero);
        MoveBack(hero, currentPos);
    }

    public void Move(Hero hero)
    {
        //if(destination == null)
        //{
        //    destination = hero.transform.position + hero.transform.right * 30;
        //}

        if (hero.role != Unit.Role.Tank)
        {
            Debug.Log("Move hero");
            hero.transform.position = Vector3.Lerp(hero.transform.position, hero.transform.position + hero.transform.right*10,Time.deltaTime);
        }
    }
    public void MoveBack(Hero hero, Vector3 originalPos)
    {
        //if(destination == null)
        //{
        //    destination = hero.transform.position + hero.transform.right * 30;
        //}

        if (hero.role != Unit.Role.Tank)
        {
            Debug.Log("Move hero");
            hero.transform.position = Vector3.Lerp(hero.transform.position, originalPos,Time.deltaTime);
        }
    }
}
