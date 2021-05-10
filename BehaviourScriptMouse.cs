using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourScriptMouse : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 MousePosition;
    private Camera cam;
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        MousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("Click");
            Debug.Log("PositionX:" + MousePosition.x);
            Debug.Log("PositionY:" + MousePosition.y);
        }
    }
}
