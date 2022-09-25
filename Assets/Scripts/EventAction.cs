using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventAction : MonoBehaviour
{
    public int id;
    public GameObject deleteButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void RemoveSelf()
    {
        UiEvent uiEventParent = GetComponentInParent<UiEvent>();
        uiEventParent.RemoveAction(this);
    }

}
