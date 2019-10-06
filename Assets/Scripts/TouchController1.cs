using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Gestures;
using System;

public class TouchController1 : MonoBehaviour
{
    RubicCube rubikcube;

    //TODO if add cubesize layerX parameter selecion from editor change to script (use tag and cubename)
    [SerializeField] ELayer layerX;
    [SerializeField] ELayer layerY;

    private bool isCW_X = true;
    private bool isCW_Y = true;

    private Vector2 pressedposition;

    public bool isObject;

    private string panelname = "" ;
    private string cubenum = "" ;

    public LayerMask LayerMask;

    // Start is called before the first frame update
    void Start()
    {
        rubikcube = GameObject.Find("GameManager").GetComponent<RubicCube>();
        //panelname = gameObject.tag;
       isObject = false;

}

private void Update()
    {
        isObject = false;

        //メインカメラ上のマウスカーソルのある位置からRayを飛ばす
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, 10f, LayerMask))
        {

            //Rayが当たるオブジェクトがあった場合はそのオブジェクト名をログに表示
            Debug.Log(hit.collider.gameObject.name);
            Debug.Log(hit.collider.gameObject.transform.parent.parent.parent.name);

            panelname  = hit.collider.gameObject.name;
            cubenum = hit.collider.gameObject.transform.parent.parent.parent.name;
            isObject = true;


    
        }


    }

    private void OnEnable()
    {
        GetComponent<ReleaseGesture>().Released += OnReleased;
        GetComponent<PressGesture>().Pressed += OnPressed;
    }

    private void OnDisable()
    {
        GetComponent<PressGesture>().Pressed -= OnPressed;
        GetComponent<ReleaseGesture>().Released -= OnReleased;
    }

    private void OnPressed(object sender, EventArgs e)
    {
        if (isObject)
        {
            var gesture = sender as PressGesture;
            pressedposition = gesture.ScreenPosition;

            Debug.Log("pressed");

        }


    }
    private void OnReleased(object sender, EventArgs e)
    {

        if (isObject)
        {
            Debug.Log("released");

        var gesture = sender as ReleaseGesture;


        float distanceFromCamera = Vector3.Distance(transform.position, Camera.main.transform.position);

        Vector3 wp1b = new Vector3(pressedposition.x, pressedposition.y, distanceFromCamera);
        wp1b = Camera.main.ScreenToWorldPoint(wp1b);

        Vector3 wp2b = new Vector3(gesture.ScreenPosition.x, gesture.ScreenPosition.y, distanceFromCamera);
        wp2b = Camera.main.ScreenToWorldPoint(wp2b);

        Vector3 velocity = (wp2b - wp1b) / .06f;
        Vector3 velocity2 = Vector3.zero;

        //  velocity's cordinate change to  panel coordinate from world coordinate
        switch (panelname)
        {
            case "frontpanel":
                velocity2 = GameObject.Find("transform_frontpanel").transform.InverseTransformVector(velocity);
                switch(cubenum)
                {
                    case "Cube":
                        layerX = ELayer.D;
                        layerY = ELayer.L;
                        break;
                    case "Cube (1)":
                        layerX = ELayer.D;
                        layerY = ELayer.M;
                        break;
                    case "Cube (2)":
                        layerX = ELayer.D;
                        layerY = ELayer.R;
                        break;
                    case "Cube (9)":
                        layerX = ELayer.E;
                        layerY = ELayer.L;
                        break;
                    case "Cube (10)":
                        layerX = ELayer.E;
                        layerY = ELayer.M;
                        break;
                    case "Cube (11)":
                        layerX = ELayer.E;
                        layerY = ELayer.R;
                        break;
                    case "Cube (18)":
                        layerX = ELayer.U;
                        layerY = ELayer.L;
                        break;
                    case "Cube (19)":
                        layerX = ELayer.U;
                        layerY = ELayer.M;
                        break;
                    case "Cube (20)":
                        layerX = ELayer.U;
                        layerY = ELayer.R;
                        break;


                }
                break;

            //case "backpanel":
            //    velocity2 = GameObject.Find("transform_backpanel").transform.InverseTransformVector(velocity);
            //    break;
            //case "toppanel":
            //    velocity2 = GameObject.Find("transform_toppanel").transform.InverseTransformVector(velocity);
            //    break;
            //case "bottompanel":
            //    velocity2 = GameObject.Find("transform_bottompanel").transform.InverseTransformVector(velocity);
            //    break;
            //case "rightpanel":
            //    velocity2 = GameObject.Find("transform_rightpanel").transform.InverseTransformVector(velocity);
            //    break;
            //case "leftpanel":
            //    velocity2 = GameObject.Find("transform_leftpanel").transform.InverseTransformVector(velocity);
            //    break;
        }

        if (velocity2 == Vector3.zero)
        {
            Debug.Log("zero :" + velocity2);

            return;
        }

        rotate(velocity2, layerX, layerY);
        Debug.LogWarning("layerX"+ layerX);
        Debug.LogWarning("layerX"+ layerY);

        }

    }

    public void rotate(Vector3 velocity, ELayer layerX, ELayer layerY)
    {

        if (Mathf.Abs(velocity.x) > Mathf.Abs(velocity.y))
        {
            if (velocity.x > 0)
            {
                rubikcube.RotateCW(layerX, isCW_X);
                Debug.Log("x+");
            }
            else
            {
                rubikcube.RotateCW(layerX, !isCW_X);
                Debug.Log("x-");
            }
        }
        else
        {
            if (velocity.y > 0)
            {
                rubikcube.RotateCW(layerY, isCW_Y);
                //Debug.Log("y+");
            }
            else
            {
                rubikcube.RotateCW(layerY, !isCW_Y);
                //Debug.Log("y-");
            }
        }
    }
}




