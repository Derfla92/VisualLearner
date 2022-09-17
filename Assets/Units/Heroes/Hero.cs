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
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        UpdateHealthBar();

    }
    public void UpdateHealthBar()
    {
        healthBar.GetComponent<RectTransform>().offsetMax = new Vector2(-1*124*(1-((float)hitPoints/(float)maxHitPoints)), 0) ;
    }
}
