using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Gestures;
using System;

public class TouchController : MonoBehaviour
{
    RubicCube rubikcube;
    [SerializeField] ELayer layerX;
    [SerializeField] ELayer layerY;
    [SerializeField] bool inverseX = false;
    [SerializeField] bool inverseY = false;

    private bool isCW_X = true;
    private bool isCW_Y = true;

    private Vector2 pressedposition;

    // Start is called before the first frame update
    void Start()
    {
        rubikcube = GameObject.Find("GameManager").GetComponent<RubicCube>();

        if (inverseX)
        {
            isCW_X = false;
        }
        if(inverseY)
        {
            isCW_Y = false;
        }
    }

    private void OnEnable()
    {
        GetComponent<FlickGesture>().Flicked += OnFlick;
        GetComponent<ReleaseGesture>().Released += OnReleased;
        GetComponent<PressGesture>().Pressed += OnPressed;
    }

    private void OnDisable()
    {
        GetComponent<FlickGesture>().Flicked -= OnFlick;
        GetComponent<PressGesture>().Pressed -= OnPressed;
        GetComponent<ReleaseGesture>().Released -= OnReleased;
    }

    private void OnPressed(object sender, EventArgs e)
    {

        var gesture = sender as PressGesture;
        pressedposition = gesture.ScreenPosition;

    }
    private void OnReleased(object sender, EventArgs e)
    {
        var gesture = sender as ReleaseGesture;


        float distanceFromCamera = Vector3.Distance(transform.position, Camera.main.transform.position);

        Vector3 wp1b = new Vector3(pressedposition.x, pressedposition.y, distanceFromCamera);
        wp1b = Camera.main.ScreenToWorldPoint(wp1b);

        Vector3 wp2b = new Vector3(gesture.ScreenPosition.x, gesture.ScreenPosition.y, distanceFromCamera);
        wp2b = Camera.main.ScreenToWorldPoint(wp2b);

        Vector3 velocity2 = (wp2b - wp1b) / .06f;

        if (velocity2 == Vector3.zero)
        {
            Debug.Log("zero :" + velocity2);

            return;
        }

        rotate(velocity2, layerX, layerY);


    }

    // フリックジェスチャーが成功すると呼ばれるメソッド
    private void OnFlick(object sender, System.EventArgs e)
    {

        var gesture = sender as FlickGesture;
        string str = "Flick: " + gesture.ScreenFlickVector + " (" + gesture.ScreenFlickTime + "秒)";
        Debug.Log(str);

        float distanceFromCamera = Vector3.Distance(transform.position, Camera.main.transform.position);

        Vector3 wp1 = new Vector3(gesture.PreviousScreenPosition.x, gesture.PreviousScreenPosition.y, distanceFromCamera);
        wp1 = Camera.main.ScreenToWorldPoint(wp1);

        Vector3 wp2 = new Vector3(gesture.ScreenPosition.x, gesture.ScreenPosition.y, distanceFromCamera);
        wp2 = Camera.main.ScreenToWorldPoint(wp2);

        //Vector3 wp3 = new Vector3(gesture.PreviousScreenPosition.x + gesture.ScreenFlickVector.x, gesture.PreviousScreenPosition.y + gesture.ScreenFlickVector.y, distanceFromCamera);
        //wp2 = Camera.main.ScreenToWorldPoint(wp2);

        Vector3 velocity = (wp2 - wp1) / gesture.FlickTime;
        //Vector3 velocity = (wp3 - wp1) / gesture.FlickTime;


        //Debug.Log("gesture.PreviousScreenPosition:　" + gesture.PreviousScreenPosition);
        //Debug.Log("gesture.ScreenPosition:　" + gesture.ScreenPosition);
        Debug.Log("wp1 : " + wp1);
        Debug.Log("wp2 : " + wp2);
        //Debug.Log("wp3 : " + wp3);






        //Debug.Log("gesture.FlickTime:　" + gesture.FlickTime);

        Debug.Log("velocity:" + velocity);

        if (velocity == Vector3.zero)
        {
            Debug.Log("zero :" + velocity);

            return;
        }

        //rotate(velocity, layerX, layerY);


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
