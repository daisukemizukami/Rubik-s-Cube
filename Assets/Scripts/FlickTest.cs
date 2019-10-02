using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Gestures;
using System;


public class FlickTest : MonoBehaviour
{
    private Vector2 releasedposition;
    private Vector2 pressedposition;



    private void OnEnable()
    {
        GetComponent<FlickGesture>().Flicked += OnFlick;
        GetComponent<TapGesture>().Tapped += tappedHandler;
        GetComponent<ReleaseGesture>().Released += OnReleased;
        GetComponent<PressGesture>().Pressed += OnPressed;


    }

    private void OnDisable()
    {
        GetComponent<FlickGesture>().Flicked -= OnFlick;
        GetComponent<TapGesture>().Tapped -= tappedHandler;
        GetComponent<PressGesture>().Pressed -= OnPressed;
        GetComponent<ReleaseGesture>().Released -= OnReleased;

    }


    private void OnPressed(object sender, EventArgs e)
    {
        Debug.Log("mizukami 押された");
        var gesture = sender as PressGesture;

        Debug.Log("mizukami OnPressed PreviousNormalizedScreenPosition: " + gesture.PreviousNormalizedScreenPosition);
        Debug.Log("mizukami OnPressed PreviousScreenPosition: " + gesture.PreviousScreenPosition);
        Debug.Log("mizukami OnPressed ScreenPosition: " + gesture.ScreenPosition);
        pressedposition = gesture.ScreenPosition;

    }

    private void OnReleased(object sender, EventArgs e)
    {
        Debug.Log("mizukami 離された");
        var gesture = sender as ReleaseGesture;

        Debug.Log("mizukami OnReleased PreviousNormalizedScreenPosition: " + gesture.PreviousNormalizedScreenPosition);
        Debug.Log("mizukami OnReleased PreviousScreenPosition: " + gesture.PreviousScreenPosition);
        Debug.Log("mizukami OnReleased ScreenPosition: " + gesture.ScreenPosition);

        float distanceFromCamera = Vector3.Distance(transform.position, Camera.main.transform.position);

        releasedposition = gesture.ScreenPosition;


        Vector3 wp1b = new Vector3(pressedposition.x, pressedposition.y, distanceFromCamera);
        wp1b = Camera.main.ScreenToWorldPoint(wp1b);

        Vector3 wp2b = new Vector3(releasedposition.x, releasedposition.y, distanceFromCamera);
        wp2b = Camera.main.ScreenToWorldPoint(wp2b);

        Vector3 velocity2 = (wp2b - wp1b) / .06f;
        Debug.Log("mizukami wp1b : " + wp1b);
        Debug.Log("mizukami wp2b : " + wp2b);
        Debug.Log("mizukami velocity2:" + velocity2);

    }

    // フリックジェスチャーが成功すると呼ばれるメソッド
    private void OnFlick(object sender, EventArgs e)
    {
        var gesture = sender as FlickGesture;
        string str = "mizukami Flick: " + gesture.ScreenFlickVector + " (" + gesture.ScreenFlickTime + "秒)";
        Debug.Log(str);

        float distanceFromCamera = Vector3.Distance(transform.position, Camera.main.transform.position);

        Vector3 wp1 = new Vector3(gesture.PreviousScreenPosition.x, gesture.PreviousScreenPosition.y, distanceFromCamera);
        wp1 = Camera.main.ScreenToWorldPoint(wp1);

        Vector3 wp2 = new Vector3(gesture.ScreenPosition.x, gesture.ScreenPosition.y, distanceFromCamera);
        wp2 = Camera.main.ScreenToWorldPoint(wp2);

        Vector3 wp1b = new Vector3(pressedposition.x, pressedposition.y, distanceFromCamera);
        wp1b = Camera.main.ScreenToWorldPoint(wp1b);

        Vector3 wp2b = new Vector3(releasedposition.x, releasedposition.y, distanceFromCamera);
        wp2b = Camera.main.ScreenToWorldPoint(wp2b);




        Debug.Log("mizukami wp1 : " + wp1);
        Debug.Log("mizukami wp2 : " + wp2);
        //Debug.Log("wp3 : " + wp3);
        Vector3 velocity = (wp2 - wp1) / gesture.FlickTime;
        Vector3 velocity2 = (wp2b - wp1b) / gesture.FlickTime;

        Debug.Log("mizukami velocity:" + velocity);
        //Debug.Log("mizukami velocity2:" + velocity2);




    }
    // タップイベントのイベントハンドラ
    private void tappedHandler(object sender, EventArgs e)
    {
        string str = "タップされた: "  + " (" + ")";

        Debug.Log(str);

        var gesture = sender as TapGesture;

        Debug.Log("tapped PreviousNormalizedScreenPosition: " + gesture.PreviousNormalizedScreenPosition);
        Debug.Log("tapped PreviousScreenPosition: " + gesture.PreviousScreenPosition);
        Debug.Log("tapped ScreenPosition: " + gesture.ScreenPosition);
    }




}