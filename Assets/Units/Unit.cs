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
                SpellCaster spellCaster = GetComponent<SpellCaster>();
                
                if (target.hitPoints > 0)
                {
                    if(spellCaster)
                    {
                        if (!spellCaster.isCasting)
                        {
                            if (!spellCaster.TryCastSpell())
                            {
                                TryAttack();
                            }
                        }
                        else
                        {
                            spellCaster.UpdateCastTime();
                        }
                    }
                    else
                    {
                        TryAttack();
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
    }


    public virtual void TakeDamage(int damage)
    {
        hitPoints -= damage;

        if (hitPoints <= 0)
        {
            Die();
        }
    }

    public void RotateTowardsTarget()
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
