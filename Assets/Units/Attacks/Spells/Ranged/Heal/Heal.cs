using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : Spell
{


    // Start is called before the first frame update
    public override void Start()
    {
        spellName = "Heal";
    }

    public override void Awake()
    {

    }

    // Update is called once per frame
    public override void Update()
    {

        GameObject spellObject = instantiatedObject.Find(x => !x.GetComponent<ParticleSystem>().isPlaying);
        instantiatedObject.Remove(spellObject);
        Destroy(spellObject);
    }
}
