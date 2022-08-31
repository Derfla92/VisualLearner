using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public UnitHandler unitHandler;
    public Unit target;
    // Start is called before the first frame update
    public virtual void Start() 
    {
        unitHandler = GameObject.Find("UnitHandler").GetComponent<UnitHandler>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    public virtual void AquireTarget()
    {

    }

    public virtual void Attack()
    {
        
    }
}
