using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UI;
using UnityEngine;

public class Unit : MonoBehaviour
{


    public UnitHandler unitHandler;
    public TimeManager timeManager;
    public GameManager gameManager;
    public Unit target;
    public Role role;
    public int maxHitPoints;
    public int hitPoints;
    public int attackDamage;
    public float attackSpeed;
    public float lastAttack = 0;
    public float turnSpeed;
    public bool isCasting = false;
    public Spell currentSpell;
    public float castTimer;

    public enum Role
    {
        Tank,
        Healer,
        DamageDealer,
        Boss
    };


    // Start is called before the first frame update
    public virtual void Start()
    {
        unitHandler = GameObject.Find("UnitHandler").GetComponent<UnitHandler>();
        timeManager = GameObject.Find("TimeManager").GetComponent<TimeManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public virtual void Awake()
    {

    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (timeManager.run)
        {
            if (target == null)
            {
                AquireTarget();
            }
            else
            {
                RotateTowardsTarget();
                if (target.hitPoints > 0)
                {
                    if (!isCasting)
                    {
                        if (!TryCastSpell())
                        {
                            TryAttack();
                        }
                    }
                    else
                    {
                        UpdateCastTime();
                    }
                }
                else
                {
                    target = null;
                }
            }
        }
    }

    public virtual void AquireTarget()
    {

    }

    public virtual void TryAttack()
    {
        if (timeManager.currentTime > lastAttack + attackSpeed)
        {
            if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                Attack();
            }
        }
    }

    public virtual void Attack()
    {
        if (TryGetComponent(out Animator animator))
        {
            animator.Play(GetComponent<MeleeAttack>().attackAnim.name);

        }
        target.TakeDamage(attackDamage);
        lastAttack = timeManager.currentTime;
        if (target.hitPoints < 0)
        {
            target = null;
        }
    }



    public virtual bool TryCastSpell()
    {
        Spell[] spells = GetComponents<Spell>();

        foreach (Spell spell in spells)
        {
            if (spell.cooldownTimer <= 0)
            {

                StartCastSpell(spell);
                return true;
            }
            else
            {
                spell.cooldownTimer -= Time.deltaTime;
            }
        }
        return false;
    }

    public virtual void StartCastSpell(Spell spell)
    {
        currentSpell = spell;
        isCasting = true;
    }

    public virtual void UpdateCastTime()
    {
        if (castTimer >= currentSpell.castTime)
        {
            currentSpell.CastSpell(this);
            castTimer = 0;
            isCasting = false;
        }
        else
        {
            castTimer += Time.deltaTime;
        }
    }

    public virtual void TakeDamage(int damage)
    {
        hitPoints -= damage;

        if (hitPoints <= 0)
        {
            Die();
        }
    }

    private void RotateTowardsTarget()
    {
        if (target != this)
        {
            Vector3 direction = target.transform.position - transform.position;
            if (role == Role.Boss)

                Debug.DrawRay(transform.position, direction, Color.red);
            //Quaternion toRotation = Quaternion.FromToRotation(transform.position, direction);
            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);

            if (transform.rotation != toRotation && GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.name != GetComponent<MeleeAttack>().attackAnim.name)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, turnSpeed * Time.deltaTime);
            }
        }
    }



    public virtual void Die()
    {

    }
}
