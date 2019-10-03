using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Gestures;
using System;

public class TouchController : MonoBehaviour
{
    RubicCube rubikcube;

    //TODO if add cubesize layerX parameter selecion from editor change to script (use tag and cubename)
    [SerializeField] ELayer layerX;
    [SerializeField] ELayer layerY;

    private bool isCW_X = true;
    private bool isCW_Y = true;

    private Vector2 pressedposition;
    private string panelname;

    // Start is called before the first frame update
    void Start()
    {
 

        rubikcube = GameObject.Find("GameManager").GetComponent<RubicCube>();
        panelname = gameObject.tag;
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

        var gesture = sender as PressGesture;
        pressedposition = gesture.ScreenPosition;

        //Debug.Log("tag: " + tag );
        //Debug.Log("cubename: " + transform.parent.parent.parent.name );

    }
    private void OnReleased(object sender, EventArgs e)
    {
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
                break;
            case "backpanel":
                velocity2 = GameObject.Find("transform_backpanel").transform.InverseTransformVector(velocity);
                break;
            case "toppanel":
                velocity2 = GameObject.Find("transform_toppanel").transform.InverseTransformVector(velocity);
                break;
            case "bottompanel":
                velocity2 = GameObject.Find("transform_bottompanel").transform.InverseTransformVector(velocity);
                break;
            case "rightpanel":
                velocity2 = GameObject.Find("transform_rightpanel").transform.InverseTransformVector(velocity);
                break;
            case "leftpanel":
                velocity2 = GameObject.Find("transform_leftpanel").transform.InverseTransformVector(velocity);
                break;
        }

        if (velocity2 == Vector3.zero)
        {
            Debug.Log("zero :" + velocity2);

            return;
        }

        rotate(velocity2, layerX, layerY);


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
                Debug.Log("y+");
            }
            else
            {
                rubikcube.RotateCW(layerY, !isCW_Y);
                Debug.Log("y-");
            }
        }
    }
}




