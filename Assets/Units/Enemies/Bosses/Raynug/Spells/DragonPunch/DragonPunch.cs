using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonPunch : Spell
{
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
        GameObject spellObject = instantiatedObject.Find(x => !x.GetComponent<ParticleSystem>().isPlaying);
        instantiatedObject.Remove(spellObject);
        Destroy(spellObject);
    }
}
