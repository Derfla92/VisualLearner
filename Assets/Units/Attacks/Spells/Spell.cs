using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Spell : MonoBehaviour
{

    public Sprite icon;
    public SpellType spellType;
    public float castTime;
    public float cooldown;
    public float cooldownTimer;
    public bool isAnimating = false;

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

    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (isAnimating)
        {
            if (!GetComponent<ParticleSystem>().isPlaying)
            {
                Destroy(gameObject);
            }
        }
        
    }



    public virtual void AimSpell(Vector3 from, Vector3 direction)
    {
        transform.position = from;
        transform.forward = direction;
    }

    public virtual void AimSpell(Vector3 position)
    {
        transform.position = position;
    }

    public virtual void ApplySpellEffect(Unit target)
    {

    }
    public virtual void ApplySpellEffect()
    {

    }

    public virtual void PlayAnimation()
    {

    }
}
