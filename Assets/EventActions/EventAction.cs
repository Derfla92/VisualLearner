using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EventAction : MonoBehaviour
{
    public int id;
    public GameObject deleteButton;

    public List<Unit> relevantHeroes;
    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    public virtual void Awake()
    {
        relevantHeroes = new List<Unit>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if(GameObject.FindObjectOfType<Boss>().GetComponent<SpellCaster>().currentSpell)
        {
            relevantHeroes = GameObject.FindObjectOfType<Boss>().GetComponent<SpellCaster>().currentSpell.GetComponent<DragonPunch>().heroesInArea.ToList();
        }
    }


    public void RemoveSelf()
    {
        UiEvent uiEventParent = GetComponentInParent<UiEvent>();
        uiEventParent.RemoveAction(this);
    }
    
    public virtual void ExecuteAction(Hero hero)
    {

    }
}
