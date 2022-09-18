using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    public string spellName;
    public float castTime;
    public int cooldown;
    public float cooldownTimer = 0;
    public GameObject prefab;
    public List<GameObject> instantiatedObject;

    
    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    public virtual void Awake()
    {
        instantiatedObject = new List<GameObject>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }
}
