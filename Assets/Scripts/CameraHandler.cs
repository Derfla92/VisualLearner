using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxisRaw("Mouse ScrollWheel") > 0)
        {
            Camera.main.transform.localPosition += Camera.main.transform.forward;
        }
        else if(Input.GetAxisRaw("Mouse ScrollWheel") < 0)
        {
            Camera.main.transform.localPosition -= Camera.main.transform.forward;
        }
    }
}
