using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class DragAndDrop : MonoBehaviour
{

    public List<EventAction> actions;
    public EventManager eventManager;
    private GameObject heldItem;

    // Start is called before the first frame update
    void Start()
    {
        actions = GetComponentsInChildren<EventAction>().ToList();
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
                foreach (EventAction action in actions)
                {


                    if (RectTransformUtility.RectangleContainsScreenPoint(action.GetComponent<RectTransform>(), Input.mousePosition))
                    {
                        heldItem = Instantiate(action.gameObject, GetComponentInParent<Canvas>().transform);
                        RectTransform rectTransform = heldItem.GetComponent<RectTransform>();

                        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                        rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                        rectTransform.offsetMax = new Vector2(60, 40);
                        rectTransform.offsetMin = new Vector2(0, 0);

                        rectTransform = rectTransform.GetComponentInChildren<Text>().GetComponent<RectTransform>();
                        rectTransform.anchorMin = new Vector2(0, 0);
                        rectTransform.anchorMax = new Vector2(1, 1);
                        rectTransform.offsetMax = new Vector2(0, 0);
                        rectTransform.offsetMin = new Vector2(0, 0);
                        break;

                    }
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

                foreach (UiEvent uiEvent in uiEvents)
                {
                    if(FindDropInEventChain(uiEvent))
                    {
                        break;
                    }
                    
                }

                Destroy(heldItem);
                heldItem = null;
            }
        }
    }

    private bool FindDropInEventChain(UiEvent rootEvent)
    {
        UiEvent current = rootEvent;
        while (current.nextEvent != null)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(current.GetComponent<RectTransform>(), Input.mousePosition))
            {

                current.AddAction(heldItem.GetComponent<EventAction>());
                return true;
            }
            current = current.nextEvent;
        }
        return false;
    }
}
