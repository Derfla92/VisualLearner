using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hero : Unit
{
    // Start is called before the first frame update

    public GameObject healthBar;
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (role != Unit.Role.Healer)
        {
            if (target == null)
            {
                AquireTarget();
            }
            else
            {
                if (target.hitPoints > 0)
                {
                    Attack();
                }
                else
                {
                    target = null;
                }
            }
        }
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        UpdateHealthBar();

    }

    public override void AquireTarget()
    {
        if (unitHandler.enemies.Count > 0)
        {
            target = unitHandler.enemies.Find(x => x.role == Unit.Role.Boss);
        }
    }

    public void UpdateHealthBar()
    {
        healthBar.GetComponent<RectTransform>().offsetMax = new Vector2(-1 * 124 * (1 - ((float)hitPoints / (float)maxHitPoints)), 0);
    }

    public override void Die()
    {

        unitHandler.heroes.Remove(this);

        if (TryGetComponent<Animator>(out Animator animator))
        {
            animator.Play("Die");
        }
        Destroy(GetComponent<Hero>());
        if(unitHandler.heroes.Count <= 0)
        {
            gameManager.GameOver();
        }

    }
}
