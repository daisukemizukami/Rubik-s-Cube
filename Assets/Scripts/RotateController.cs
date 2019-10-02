using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Gestures;

public class RotateController : MonoBehaviour
{
    public GameObject gamemanager;
    RubicCube rubikcube;

    private void Start()
    {
        rubikcube = gamemanager.GetComponent<RubicCube>();
        //Debug.unityLogger.logEnabled = false;
    }



    private void OnEnable()
    {
        GetComponent<FlickGesture>().Flicked += OnFlick;
        //GetComponent<TapGesture>().Tapped += tappedHandle;
    }
    private void OnDisable()
    {
        GetComponent<FlickGesture>().Flicked -= OnFlick;
    }

    // フリックジェスチャーが成功すると呼ばれるメソッド
    private void OnFlick(object sender, System.EventArgs e)
    {

        var gesture = sender as FlickGesture;
        string str = "フリック: " + gesture.ScreenFlickVector + " (" + gesture.ScreenFlickTime + "秒)";
        Debug.Log(str);

 

        float distanceFromCamera = Vector3.Distance(transform.position, Camera.main.transform.position);

        Vector3 wp1 = new Vector3(gesture.PreviousScreenPosition.x, gesture.PreviousScreenPosition.y, distanceFromCamera);
        wp1 = Camera.main.ScreenToWorldPoint(wp1);

        Vector3 wp2 = new Vector3(gesture.ScreenPosition.x, gesture.ScreenPosition.y, distanceFromCamera);
        wp2 = Camera.main.ScreenToWorldPoint(wp2);

        Vector3 velocity = (wp2 - wp1) / gesture.FlickTime;

        Debug.Log("velocity:" + velocity);

        if(velocity == Vector3.zero)
        {
            Debug.Log("デルで" + velocity);

            return;
        }

		//velocity = cubepanel.InverseTransformDirection(velocity);

		int distance = 10;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		Debug.DrawLine(ray.origin, ray.direction * distance, Color.blue);

        //Rayが当たったオブジェクトの情報を入れる箱
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, distance))
		{
            Debug.Log("hit.collider.tag: " + hit.collider.tag);

            if(hit.collider.transform.parent.parent.parent.name != null)
            {
                var hitname = hit.collider.transform.parent.parent.parent.name;
                Debug.Log("cube : " + hitname);

                //rotate(velocity, ELayer.B, ELayer.L);

                if (hit.collider.tag == "toppanel")
                {
                    Debug.Log("if開始");

                    //if (hitname == c24) rotate(velocity, ELayer.B, ELayer.L);
                    //else if (hitname == c25) rotate(velocity, ELayer.B, ELayer.M);
                    //else if (hitname == c26) rotate(velocity, ELayer.B, ELayer.R);
                    //else if (hitname == c21) rotate(velocity, ELayer.S, ELayer.L);
                    //else if (hitname == c22) rotate(velocity, ELayer.S, ELayer.M);
                    //else if (hitname == c23) rotate(velocity, ELayer.S, ELayer.R);
                    //else if (hitname == c18) rotate(velocity, ELayer.F, ELayer.L);
                    //else if (hitname == c19) rotate(velocity, ELayer.F, ELayer.M);
                    //else if (hitname == c20) rotate(velocity, ELayer.F, ELayer.R);

                    if (hitname == rubikcube.cubes[24].name) rotate(velocity, ELayer.B, ELayer.L);
                    else if (hitname == rubikcube.cubes[25].name) rotate(velocity, ELayer.B, ELayer.M);
                    else if (hitname == rubikcube.cubes[26].name) rotate(velocity, ELayer.B, ELayer.R);
                    else if (hitname == rubikcube.cubes[21].name) rotate(velocity, ELayer.S, ELayer.L);
                    else if (hitname == rubikcube.cubes[22].name) rotate(velocity, ELayer.S, ELayer.M);
                    else if (hitname == rubikcube.cubes[23].name) rotate(velocity, ELayer.S, ELayer.R);
                    else if (hitname == rubikcube.cubes[18].name) rotate(velocity, ELayer.F, ELayer.L);
                    else if (hitname == rubikcube.cubes[19].name) rotate(velocity, ELayer.F, ELayer.M);
                    else if (hitname == rubikcube.cubes[20].name) rotate(velocity, ELayer.F, ELayer.R);

                    Debug.Log("ifおわり");

                }

                //switch (hit.collider.tag)
                //{
                //    case "toppanel":
                //        Debug.Log("switch");

                //        if (hitname == rubikcube.cubes[24].name) rotate(velocity, ELayer.B, ELayer.L);
                //        else if (hitname == rubikcube.cubes[25].name) rotate(velocity, ELayer.B, ELayer.M);
                //        else if (hitname == rubikcube.cubes[26].name)
                //        {
                //            Debug.Log("26入ったけども");

                //            rotate(velocity, ELayer.B, ELayer.R);
                //        }
                //        else if (hitname == rubikcube.cubes[21].name) rotate(velocity, ELayer.S, ELayer.L);
                //        else if (hitname == rubikcube.cubes[22].name) rotate(velocity, ELayer.S, ELayer.M);
                //        else if (hitname == rubikcube.cubes[23].name) rotate(velocity, ELayer.S, ELayer.R);
                //        else if (hitname == rubikcube.cubes[18].name) rotate(velocity, ELayer.F, ELayer.L);
                //        else if (hitname == rubikcube.cubes[19].name) rotate(velocity, ELayer.F, ELayer.M);
                //        else if (hitname == rubikcube.cubes[20].name) rotate(velocity, ELayer.F, ELayer.R);
                //        Debug.Log("switchおわり");

                //        break;
                //        //case "bottompanel":
                //        //    if (hitname == rubikcube.cubes[].name) rotate(velocity, ELayer., ELayer.);
                //        //    else if (hitname == rubikcube.cubes[].name) rotate(velocity, ELayer., ELayer.);
                //        //    else if (hitname == rubikcube.cubes[].name) rotate(velocity, ELayer., ELayer.);
                //        //    else if (hitname == rubikcube.cubes[].name) rotate(velocity, ELayer., ELayer.);
                //        //    else if (hitname == rubikcube.cubes[].name) rotate(velocity, ELayer., ELayer.);
                //        //    else if (hitname == rubikcube.cubes[].name) rotate(velocity, ELayer., ELayer.);
                //        //    else if (hitname == rubikcube.cubes[].name) rotate(velocity, ELayer., ELayer.);
                //        //    else if (hitname == rubikcube.cubes[].name) rotate(velocity, ELayer., ELayer.);
                //        //    else if (hitname == rubikcube.cubes[].name) rotate(velocity, ELayer., ELayer.);
                //        //    break;
                //        //case "frontpanel":
                //        //    if (hitname == rubikcube.cubes[].name) rotate(velocity, ELayer., ELayer.);
                //        //    else if (hitname == rubikcube.cubes[].name) rotate(velocity, ELayer., ELayer.);
                //        //    else if (hitname == rubikcube.cubes[].name) rotate(velocity, ELayer., ELayer.);
                //        //    else if (hitname == rubikcube.cubes[].name) rotate(velocity, ELayer., ELayer.);
                //        //    else if (hitname == rubikcube.cubes[].name) rotate(velocity, ELayer., ELayer.);
                //        //    else if (hitname == rubikcube.cubes[].name) rotate(velocity, ELayer., ELayer.);
                //        //    else if (hitname == rubikcube.cubes[].name) rotate(velocity, ELayer., ELayer.);
                //        //    else if (hitname == rubikcube.cubes[].name) rotate(velocity, ELayer., ELayer.);
                //        //    else if (hitname == rubikcube.cubes[].name) rotate(velocity, ELayer., ELayer.);
                //        //    break;
                //        //case "backpanel":
                //        //    if (hitname == rubikcube.cubes[].name) rotate(velocity, ELayer., ELayer.);
                //        //    else if (hitname == rubikcube.cubes[].name) rotate(velocity, ELayer., ELayer.);
                //        //    else if (hitname == rubikcube.cubes[].name) rotate(velocity, ELayer., ELayer.);
                //        //    else if (hitname == rubikcube.cubes[].name) rotate(velocity, ELayer., ELayer.);
                //        //    else if (hitname == rubikcube.cubes[].name) rotate(velocity, ELayer., ELayer.);
                //        //    else if (hitname == rubikcube.cubes[].name) rotate(velocity, ELayer., ELayer.);
                //        //    else if (hitname == rubikcube.cubes[].name) rotate(velocity, ELayer., ELayer.);
                //        //    else if (hitname == rubikcube.cubes[].name) rotate(velocity, ELayer., ELayer.);
                //        //    else if (hitname == rubikcube.cubes[].name) rotate(velocity, ELayer., ELayer.);
                //        //    break;
                //        //case "rightpanel":
                //        //    if (hitname == rubikcube.cubes[].name) rotate(velocity, ELayer., ELayer.);
                //        //    else if (hitname == rubikcube.cubes[].name) rotate(velocity, ELayer., ELayer.);
                //        //    else if (hitname == rubikcube.cubes[].name) rotate(velocity, ELayer., ELayer.);
                //        //    else if (hitname == rubikcube.cubes[].name) rotate(velocity, ELayer., ELayer.);
                //        //    else if (hitname == rubikcube.cubes[].name) rotate(velocity, ELayer., ELayer.);
                //        //    else if (hitname == rubikcube.cubes[].name) rotate(velocity, ELayer., ELayer.);
                //        //    else if (hitname == rubikcube.cubes[].name) rotate(velocity, ELayer., ELayer.);
                //        //    else if (hitname == rubikcube.cubes[].name) rotate(velocity, ELayer., ELayer.);
                //        //    else if (hitname == rubikcube.cubes[].name) rotate(velocity, ELayer., ELayer.);
                //        //    break;
                //        //case "leftpanel":
                //        //    if (hitname == rubikcube.cubes[].name) rotate(velocity, ELayer., ELayer.);
                //        //    else if (hitname == rubikcube.cubes[].name) rotate(velocity, ELayer., ELayer.);
                //        //    else if (hitname == rubikcube.cubes[].name) rotate(velocity, ELayer., ELayer.);
                //        //    else if (hitname == rubikcube.cubes[].name) rotate(velocity, ELayer., ELayer.);
                //        //    else if (hitname == rubikcube.cubes[].name) rotate(velocity, ELayer., ELayer.);
                //        //    else if (hitname == rubikcube.cubes[].name) rotate(velocity, ELayer., ELayer.);
                //        //    else if (hitname == rubikcube.cubes[].name) rotate(velocity, ELayer., ELayer.);
                //        //    else if (hitname == rubikcube.cubes[].name) rotate(velocity, ELayer., ELayer.);
                //        //    else if (hitname == rubikcube.cubes[].name) rotate(velocity, ELayer., ELayer.);
                //        //    break;
                //}

            }
            else
            {
                Debug.Log("キューブ名わからん");

                return;

            }



            //         if (hit.collider.tag == "toppanel")
            //{
            //             Debug.Log("名前や" + hit.collider.transform.parent.parent.parent.name);
            //             Debug.Log("rubikcube.cubes[24].name " + rubikcube.cubes[24].name);

            //             if (hit.collider.transform.parent.parent.parent.name == rubikcube.cubes[24].name)
            //             {
            //                 rotate(velocity, ELayer.B, ELayer.L);
            //             }
            //}
        }



     
       

    }

    void tappedHandle(object sender, System.EventArgs e)
    {
        //処理したい内容
        Debug.Log("タップされたな");

    }



    public void rotate( Vector3 velocity,ELayer layerX,ELayer layerY)
    {
        Debug.Log("まわすでrotate");

        if (Mathf.Abs(velocity.x) > Mathf.Abs(velocity.y))
        {
            if (velocity.x > 0)
            {
                rubikcube.RotateCW(layerX,true);
                Debug.Log("x+");
            }
            else
            {
                rubikcube.RotateCW(layerX, true);
                Debug.Log("x-");
            }
        }
        else
        {
            if (velocity.y > 0)
            {
                rubikcube.RotateCW(layerY, true);
                Debug.Log("y+");
            }
            else
            {
                rubikcube.RotateCW(layerY, true);
                Debug.Log("y-");
            }
        }
    } 
}
