using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class child : MonoBehaviour
{

    [SerializeField] Transform parent;
    // Start is called before the first frame update
    void Start()
    {
        var a = transform.InverseTransformPoint(parent.position);
        var b = transform.TransformPoint(parent.position);
        var c = parent.InverseTransformPoint(new Vector3(2,0,2)) ;
        var d = parent.TransformPoint(transform.position);


        Debug.Log("child.InverseTransformPoint(parent.position):" + a);
        Debug.Log("child.TransformPoint(parent.position)" + b);
        Debug.Log("parent.TransformPoint(child.position)" + c);
        Debug.Log("parent.TransformPoint(transform.position)" + d);



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
