using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddColliders : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        GetComponent<ParticleSystem>().trigger.SetCollider(0, GameObject.Find("UnitHandler").GetComponent<UnitHandler>().heroes[0].GetComponentInChildren<Collider>());   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
