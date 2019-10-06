
using UnityEngine;
using TouchScript.Gestures.TransformGestures;

public class CameraController : MonoBehaviour
{
    public ScreenTransformGesture TwoFingerMoveGesture;
    public ScreenTransformGesture OneFingerMoveGesture;
    public float RotationSpeed = 10f;
    public float ZoomSpeed = 1f;

    [SerializeField] Transform targetobj;
    [SerializeField] Transform cam;


    private void OnEnable()
    {
        TwoFingerMoveGesture.Transformed += twoFingerTransformHandler;
        OneFingerMoveGesture.Transformed += OneFingerMoveTransformedHandler;
    }

    private void OnDisable()
    {
        TwoFingerMoveGesture.Transformed -= twoFingerTransformHandler;
        OneFingerMoveGesture.Transformed -= OneFingerMoveTransformedHandler;
    }

    private void OneFingerMoveTransformedHandler(object sender, System.EventArgs e)
    {
        cam.RotateAround(targetobj.position, Vector3.up, OneFingerMoveGesture.DeltaPosition.x * Time.deltaTime * RotationSpeed);
        cam.RotateAround(targetobj.position, cam.right, -OneFingerMoveGesture.DeltaPosition.y * Time.deltaTime * RotationSpeed);

        //Debug.Log("one finger  " );
    }

    private void twoFingerTransformHandler(object sender, System.EventArgs e)
    {

        Debug.Log(cam.transform.position.magnitude);

        //-7 -20
        if(TwoFingerMoveGesture.DeltaPosition.y < 0)
        {
            if (cam.transform.position.magnitude < 20f)
            {
                cam.Translate(Vector3.forward * TwoFingerMoveGesture.DeltaPosition.y * ZoomSpeed);
                Debug.Log("TwoFingerMoveGesture.DeltaPosition.y　上" + TwoFingerMoveGesture.DeltaPosition.y);
            }
        
        }
        else
        {
            if (cam.transform.position.magnitude > 7f)
            {
                cam.Translate(Vector3.forward * TwoFingerMoveGesture.DeltaPosition.y * ZoomSpeed);
                Debug.Log("TwoFingerMoveGesture.DeltaPosition.y 下" + TwoFingerMoveGesture.DeltaPosition.y);

            }
        
        }

        //Debug.Log("two finger  " + TwoFingerMoveGesture.DeltaPosition);
    }
    }
