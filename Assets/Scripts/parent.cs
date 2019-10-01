using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript;
using TouchScript.Gestures;
using System;

public class parent : MonoBehaviour
{

    [SerializeField] Transform child;

    // Start is called before the first frame update
    void Start()
    {

        //座標変換
       //var a = transform.InverseTransformPoint(child.position);
       //var b = transform.TransformPoint(child.position);
       //Debug.Log("transform.InverseTransformPoint(child.position):" + a);
       //Debug.Log("transform.TransformPoint(child.position)" + b);


       // var c = child.transform.TransformPoint(child.position);
       // Debug.Log("child.position)" + child.position);
       // Debug.Log("child.transform.TransformPoint(child.position)" + c);
       // Debug.Log("world child" + transform.TransformPoint(new Vector3(2,0,2)));






    }

    // Update is called once per frame
    void Update()
    {
        
    }




    //public FlickGesture flickGesture;

    //private void OnEnable()
    //{
    //    flickGesture.Flicked += OnFlicked;
    //}

    //private void OnDisable()
    //{
    //    flickGesture.Flicked -= OnFlicked;
    //}

    //private void OnFlicked(object sender, EventArgs e)
    //{
    //    Debug.Log("フリックされた: " + flickGesture.ScreenFlickVector);
    //}
}


