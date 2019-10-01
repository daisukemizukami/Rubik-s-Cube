using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Gestures;

public class RotateController : MonoBehaviour
{
    [SerializeField] Transform cubepanel;
    public GameObject gamemanager;
       


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

        velocity = cubepanel.InverseTransformDirection(velocity);


        if (Mathf.Abs(velocity.x) > Mathf.Abs(velocity.y))
        {
            velocity = new Vector3(velocity.x, 0f, 0f);

            if (velocity.x > 0)
            {
                gamemanager.GetComponent<RubicCube>().RotateCW(ELayer.U);
                Debug.Log("x+");

            }
            else
            {
                gamemanager.GetComponent<RubicCube>().RotateCW(ELayer.U);
                Debug.Log("x-");

            }

        }
        else
        {
            velocity = new Vector3(0f, velocity.y, 0f);

            if (velocity.y > 0)
            {
                gamemanager.GetComponent<RubicCube>().RotateCW(ELayer.L);
                Debug.Log("y+");

            }
            else
            {
                gamemanager.GetComponent<RubicCube>().RotateCW(ELayer.L);
                Debug.Log("y-");


            }


        }



        //rigidbody.AddForce(velocity, ForceMode.VelocityChange);


        //Debug.Log("        gesture.PreviousScreenPosition.x:" + gesture.PreviousScreenPosition.x);
        //Debug.Log("        gesture.ScreenPosition.x:" + gesture.ScreenPosition.x);




        Debug.Log("velocity:" + velocity);




    }

    void tappedHandle(object sender, System.EventArgs e)
    {
        //処理したい内容
        Debug.Log("タップされたな");

    }
}
