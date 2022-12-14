using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{


    public UnitHandler unitHandler;
    public TimeManager timeManager;
    public GameManager gameManager;
    public GameObject healthBar;
    public Unit target;
    public Role role;
    public int maxHitPoints;
    public int hitPoints;
    public float turnSpeed;

    public int attackDamage;
    public float attackSpeed;
    public float swingTimer;

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
        swingTimer = attackSpeed;
    }

    public virtual void Awake()
    {
        swingTimer = attackSpeed;
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
                    if (spellCaster)
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
        healthBar.GetComponentInParent<Canvas>().transform.LookAt(Camera.main.transform.position);
    }

    public virtual void AquireTarget()
    {

    }

    public virtual void TryAttack()
    {
        if (swingTimer <= 0)
        {
            if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                Attack();
            }
        }
        else
        {
            swingTimer -= Time.deltaTime * timeManager.timeMultiplier ;
        }
    }

    public virtual void Attack()
    {
        if (TryGetComponent(out Animator animator))
        {
            animator.Play(GetComponent<MeleeAttack>().attackAnim.name);

        }
        target.TakeDamage(attackDamage);
        swingTimer = attackSpeed;
    }


    public virtual void TakeDamage(int damage)
    {
        hitPoints -= damage;

        if (hitPoints <= 0)
        {
            hitPoints = 0;
            Die();
        }
        UpdateHealthBar();
    }

    public virtual void GetHealed(int healing)
    {
        hitPoints += healing;
        if (hitPoints > maxHitPoints)
        {
            hitPoints = maxHitPoints;
        }
        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        float healthPercentage = (float)hitPoints / (float)maxHitPoints;
        healthBar.GetComponent<Image>().fillAmount = healthPercentage;
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
                transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, turnSpeed * Time.deltaTime * timeManager.timeMultiplier);
            }
        }
    }



    public virtual void Die()
    {

    }
}
