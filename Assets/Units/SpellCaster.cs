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

    }

    // Update is called once per frame
    void Update()
    {

    }
    public bool TryCastSpell()
    {
        if (!isCasting)
        {
            foreach (GameObject spell in spells)
            {
                Spell spellInfo = spell.GetComponent<Spell>();
                if (spellInfo.cooldownTimer <= 0)
                {

                    StartCastSpell(spell);
                    return true;
                }
                else
                {
                    spellInfo.cooldownTimer -= Time.deltaTime;
                }
            }
        }
        return false;
    }

    public void StartCastSpell(GameObject spell)
    {
        currentSpell = spell;
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
            castTimer += Time.deltaTime;
        }
    }

    public void CastSpell(GameObject spell)
    {
        Animator animator = GetComponent<Animator>();

            animator.Play(spell.name);

            GameObject newSpell = Instantiate(spell);
            Spell spellInfo = spell.GetComponent<Spell>();
            spellInfo.cooldownTimer = spellInfo.cooldown;
            if (spellInfo.spellType == SpellType.Projectile)
            {
                newSpell.GetComponent<Spell>().AimSpell(transform.position, transform.forward);

            }
            else if (spellInfo.spellType == SpellType.Direct)
            {
                newSpell.GetComponent<Spell>().AimSpell(GetComponent<Unit>().target.transform.position);
            }
            newSpell.GetComponent<Spell>().ApplySpellEffect(GetComponent<Unit>().target);
            GetComponent<Unit>().target = null;
        


    }
}
