using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Gestures;



public class InputController : MonoBehaviour
{
    [SerializeField] float z = -10f;
    [SerializeField] Transform targetobj;
    [SerializeField] Rigidbody rigidbody;


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
        var cameraworld = Camera.main.ScreenToWorldPoint(new Vector3(gesture.ScreenFlickVector.x, gesture.ScreenFlickVector.y, z));



        float distanceFromCamera = Vector3.Distance(transform.position, Camera.main.transform.position);

        Vector3 wp1 = new Vector3(gesture.PreviousScreenPosition.x, gesture.PreviousScreenPosition.y, distanceFromCamera);
        wp1 = Camera.main.ScreenToWorldPoint(wp1);

        Vector3 wp2 = new Vector3(gesture.ScreenPosition.x, gesture.ScreenPosition.y, distanceFromCamera);
        wp2 = Camera.main.ScreenToWorldPoint(wp2);

        Vector3 velocity = (wp2 - wp1) / gesture.FlickTime;

        Debug.Log("velocity:" + velocity);

        velocity = rigidbody.transform.InverseTransformDirection(velocity);


        if (Mathf.Abs(velocity.x) > Mathf.Abs(velocity.y))
        {
            velocity = new Vector3 (velocity.x, 0f, 0f);
            Debug.Log("x:");

            if(velocity.x>0)
            {
                rigidbody.transform.localPosition = new Vector3(rigidbody.transform.localPosition.x + .1f, rigidbody.transform.localPosition.y, rigidbody.transform.localPosition.z);
            }
            else
            {
                rigidbody.transform.localPosition = new Vector3(rigidbody.transform.localPosition.x - .1f, rigidbody.transform.localPosition.y, rigidbody.transform.localPosition.z);

            }

        }
        else
        {
            velocity = new Vector3 (0f,velocity.y, 0f);
            Debug.Log("y:");

            if (velocity.y > 0)
            {
                rigidbody.transform.localPosition = new Vector3(rigidbody.transform.localPosition.x , rigidbody.transform.localPosition.y + .1f, rigidbody.transform.localPosition.z);
            }
            else
            {
                rigidbody.transform.localPosition = new Vector3(rigidbody.transform.localPosition.x, rigidbody.transform.localPosition.y - .1f, rigidbody.transform.localPosition.z);

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
