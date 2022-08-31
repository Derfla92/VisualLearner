using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public UnitHandler unitHandler;
    public TimeManager timeManager;
    public GameManager gameManager;
    public Unit target;
    [SerializeField]
    private int hitPoints;
    public int attackDamage;
    public float attackSpeed;
    public float lastAttack = 0;

    // Start is called before the first frame update
    public virtual void Start() 
    {
        unitHandler = GameObject.Find("UnitHandler").GetComponent<UnitHandler>();
        timeManager = GameObject.Find("TimeManager").GetComponent<TimeManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    public virtual void AquireTarget()
    {

    }

    public virtual void Attack()
    {
        target.TakeDamage(attackDamage);
        lastAttack = timeManager.currentTime;
        if(target.hitPoints < 0)
        {
            target = null;
        }
    }

    public void TakeDamage(int damage)
    {
        hitPoints -= damage;
        if(hitPoints < 0)
        {
            Die();
        }
    }

    public void Die()
    {
        unitHandler.unitList.Remove(this);

        GetComponent<Renderer>().material.color = Color.red;
        transform.Translate(Vector3.up * 4, Space.World);
        transform.Rotate(new Vector3(0, 0, 90));
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

        gameManager.GameOver();
    }
}
