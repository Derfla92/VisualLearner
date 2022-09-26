using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Spell;

public class SpellCaster : MonoBehaviour
{

    public List<GameObject> spells = new List<GameObject>();
    public bool isCasting = false;
    public GameObject currentSpell;
    public float castTimer;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject spell in spells)
        {
            spell.GetComponent<Spell>().cooldownTimer = spell.GetComponent<Spell>().cooldown;
        }
    }
    private void Awake()
    {
        foreach (GameObject spell in spells)
        {
            spell.GetComponent<Spell>().cooldownTimer = spell.GetComponent<Spell>().cooldown;
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
    public bool TryCastSpell()
    {
        foreach (GameObject spell in spells)
        {
            Spell spellInfo = spell.GetComponent<Spell>();
            if (spellInfo.cooldownTimer <= 0)
            {
                if (!isCasting)
                {

                    StartCastSpell(spell);
                    return true;
                }
            }
            else
            {
                spellInfo.cooldownTimer -= Time.deltaTime * GameObject.FindObjectOfType<TimeManager>().timeMultiplier;
            }
        }
        return false;
    }

    public void StartCastSpell(GameObject spell)
    {
        currentSpell = Instantiate(spell);
        currentSpell.GetComponent<ParticleSystem>().Stop(true);
        Spell spellInfo = spell.GetComponent<Spell>();
        if (spellInfo.spellType == SpellType.Projectile)
        {
            currentSpell.GetComponent<Spell>().AimSpell(transform.position, transform.forward);

        }
        else if (spellInfo.spellType == SpellType.Direct)
        {
            currentSpell.GetComponent<Spell>().AimSpell(GetComponent<Unit>().target.transform.position);
        }
        isCasting = true;
    }

    public void UpdateCastTime()
    {
        if (castTimer >= currentSpell.GetComponent<Spell>().castTime)
        {
            CastSpell(currentSpell);
            castTimer = 0;
            isCasting = false;
        }
        else
        {
            castTimer += Time.deltaTime * GameObject.FindObjectOfType<TimeManager>().timeMultiplier;
        }
    }

    public void CastSpell(GameObject spell)
    {
        Animator animator = GetComponent<Animator>();
        animator.Play(spell.name);
        currentSpell.GetComponent<ParticleSystem>().Play(true);
        currentSpell.GetComponent<Spell>().isAnimating = true;

        Spell spellInfo = spells[0].GetComponent<Spell>();
        spellInfo.cooldownTimer = spellInfo.cooldown;
        
        currentSpell.GetComponent<Spell>().ApplySpellEffect(GetComponent<Unit>().target);
        GetComponent<Unit>().target = null;



    }
}
