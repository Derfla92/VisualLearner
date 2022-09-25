using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class DragAndDrop : MonoBehaviour
{

    public List<EventAction> actions = new List<EventAction>();
    public GameObject selectedItem;
    public EventManager eventManager;
    private GameObject heldItem;
    private int currentIdNumber = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PickupAction();
    }

    public void PickupAction()
    {
        if (heldItem == null)
        {
            if (Input.GetMouseButtonDown(0))
            {

                if (RectTransformUtility.RectangleContainsScreenPoint(actions[0].GetComponent<RectTransform>(), Input.mousePosition))
                {
                    heldItem = Instantiate(actions[0].gameObject,GetComponentInParent<Canvas>().transform);
                    heldItem.GetComponent<EventAction>().id = currentIdNumber++;
                    heldItem.GetComponent<EventAction>().deleteButton.SetActive(true);
                    heldItem.GetComponentInChildren<Text>().alignment = TextAnchor.MiddleLeft;
                    Debug.Log("Click");
                }
            }
        }
        else if (heldItem != null)
        {
            if (Input.GetMouseButton(0))
            {
                heldItem.transform.position = Input.mousePosition;
            }
            if (Input.GetMouseButtonUp(0))
            {
                UiEvent[] uiEvents = eventManager.uiEvents.ToArray();
                bool destroy = true;

                foreach (UiEvent uiEvent in uiEvents)
                {
                    if (RectTransformUtility.RectangleContainsScreenPoint(uiEvent.GetComponent<RectTransform>(), Input.mousePosition))
                    {

                        uiEvent.AddAction(heldItem.GetComponent<EventAction>());
                        
                        Debug.Log("Drop action");
                        destroy = false;
                        break;
                    }
                }

                if (destroy)
                {
                    Destroy(heldItem);
                }
                heldItem = null;
            }
        }
    }
}
