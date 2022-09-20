using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Spell : MonoBehaviour
{

    public Sprite icon;
    public string spellName;
    public SpellType spellType;
    public float castTime;
    public float cooldown;
    public float cooldownTimer;
    public GameObject spellPrefab;
    public List<GameObject> instantiatedObject;

    public enum SpellType
    {
        Projectile,
        Direct
    };

    
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
        GameObject spellObject = instantiatedObject.Find(x => !x.GetComponent<ParticleSystem>().isPlaying);
        instantiatedObject.Remove(spellObject);
        Destroy(spellObject);
    }

    public virtual void CastSpell(Unit caster)
    {
        PlayAnimation();
        instantiatedObject.Add(Instantiate(spellPrefab));
        cooldownTimer = cooldown;
        if(spellType == SpellType.Projectile)
        {
            AimSpell(caster.transform.position,caster.transform.forward);
            
        }
        else if(spellType == SpellType.Direct)
        {
            AimSpell(caster.target.transform.position);
        }
        ApplySpellEffect(caster.target);
    }

    public virtual void AimSpell(Vector3 from, Vector3 direction)
    {
        instantiatedObject[instantiatedObject.Count - 1].transform.position = from;
        instantiatedObject[instantiatedObject.Count - 1].transform.forward = direction;
    }

    public virtual void AimSpell(Vector3 position)
    {
        instantiatedObject[instantiatedObject.Count - 1].transform.position = position;
    }

    public virtual void ApplySpellEffect(Unit target)
    {
        
    }

    public virtual void PlayAnimation()
    {

    }
}
