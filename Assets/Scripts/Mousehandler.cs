using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mousehandler : MonoBehaviour
{

    public Camera cam;
    private Ray ray;
    private RaycastHit info;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MouseClick();
    }


    public void MouseClick()
    {
        if (Input.GetMouseButtonUp(0))
        {
            ray = cam.ScreenPointToRay(Input.mousePosition);
            Debug.Log(cam.ScreenPointToRay(Input.mousePosition));
            Debug.DrawLine(ray.origin, ray.direction*100,Color.red,10f);
            Physics.Raycast(ray, out info, Mathf.Infinity);
            Debug.Log(info.transform.name);
        }
        if (info.transform != null)
        {

        }

    }


}
