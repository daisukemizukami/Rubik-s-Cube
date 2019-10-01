
using UnityEngine;
using TouchScript.Gestures.TransformGestures;

    public class CameraController : MonoBehaviour
    {
        public ScreenTransformGesture TwoFingerMoveGesture;
        public ScreenTransformGesture ManipulationGesture;
        public float PanSpeed = 200f;
        public float RotationSpeed = 200f;
        public float ZoomSpeed = 10f;

        [SerializeField] Transform targetobj;
        [SerializeField] Transform cam;

    private void Awake()
        {
            
        }

        private void OnEnable()
        {
            TwoFingerMoveGesture.Transformed += twoFingerTransformHandler;
            ManipulationGesture.Transformed += manipulationTransformedHandler;
        }

        private void OnDisable()
        {
            TwoFingerMoveGesture.Transformed -= twoFingerTransformHandler;
            ManipulationGesture.Transformed -= manipulationTransformedHandler;
        }

        private void manipulationTransformedHandler(object sender, System.EventArgs e)
        {

        //Debug.Log(ManipulationGesture.DeltaPosition.x +" " + ManipulationGesture.DeltaPosition.y );

        // targetの位置のY軸を中心に、回転（公転）する
        cam.RotateAround(targetobj.position, Vector3.up, ManipulationGesture.DeltaPosition.x * Time.deltaTime * RotationSpeed);
        // カメラの垂直移動（※角度制限なし、必要が無ければコメントアウト）
        cam.RotateAround(targetobj.position, cam.right, -ManipulationGesture.DeltaPosition.y * Time.deltaTime * RotationSpeed);

        Debug.Log("one finger  " );

        //var rotation = Quaternion.Euler(ManipulationGesture.DeltaPosition.y / Screen.height * RotationSpeed,
        //    -ManipulationGesture.DeltaPosition.x / Screen.width * RotationSpeed,
        //    ManipulationGesture.DeltaRotation);
        //targetobj.localRotation *= rotation;

        //cam.transform.localPosition += Vector3.forward * (ManipulationGesture.DeltaScale - 1f) * ZoomSpeed;
    }

    private void twoFingerTransformHandler(object sender, System.EventArgs e)
        {
        //cam.localPosition += targetobj.rotation * TwoFingerMoveGesture.DeltaPosition * PanSpeed;
         cam.Translate(Vector3.forward * TwoFingerMoveGesture.DeltaPosition.y * PanSpeed);
        //cam.localPosition =
        //    new Vector3
        //    (
        //        0,
        //        0,
        //        cam.localPosition.z  +TwoFingerMoveGesture.DeltaPosition.x * PanSpeed
            //);
        Debug.Log("two finger  " + TwoFingerMoveGesture.DeltaPosition);
    }
    }
