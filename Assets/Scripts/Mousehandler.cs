using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Mousehandler : MonoBehaviour
{

    public UnitHandler unitHandler;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MouseClick();
    }


    //public void MouseClick()
    //{
    //    if (Input.GetMouseButtonUp(0))
    //    {
    //        ray = cam.ScreenPointToRay(Input.mousePosition);
    //        Debug.Log(cam.ScreenPointToRay(Input.mousePosition));
    //        Debug.DrawLine(ray.origin, ray.direction*100,Color.red,10f);
    //        Physics.Raycast(ray, out info, Mathf.Infinity);
    //        Debug.Log(info.transform.name);
    //    }
    //    if (info.transform != null)
    //    {

    //    }

    //}

    private void MouseClick()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;
            int mask = 1 << 3;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawLine(ray.origin, ray.GetPoint(100f),Color.red,10f);
            RaycastHit hitinfo;
            Physics.Raycast(ray, out hitinfo,100f,mask);
            if (hitinfo.collider != null)
            {
                unitHandler.infoPane.transform.parent.gameObject.SetActive(true);
                unitHandler.selectedUnit = hitinfo.transform.GetComponentInParent<Unit>();
                unitHandler.UpdateInfoTab();
                
            }
            else
            {
                unitHandler.infoPane.transform.parent.gameObject.SetActive(false);
            }
        }
    }   


}
