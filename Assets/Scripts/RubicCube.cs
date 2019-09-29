using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class RubicCube : MonoBehaviour
{
    public List<Transform> cubes;
	[SerializeField] Transform cubeContainer;
	[SerializeField] public bool isRotating;
    [SerializeField] int _count = 25;
    public Func<float, float> easing;

    /*  cube map layout
    *       c24  c25  c26
    *     c21  c22  c23|
    *   c18  c19  c20  |
    *   |              |
    *   |   c15  c16  c17
    *   | c12  c13  c14|
    *   c9   c10  c11  |
    *   |              |
    *   |   c6   c7   c8
    *   | c3   c4   c5
    *   c0---c1---c2
    *
    * */

  
    void Start()
	{
		isRotating = false;
        easing = Easings.CubicEaseInOut;
    }

    public void RotateFrontFace()
    {
        if (!isRotating)
        {
            isRotating = true;
            RotateCW(ELayer.F);
        }
    }
    public void RotateRightFace()
    {
        if (!isRotating)
        {
            isRotating = true;

            RotateCW(ELayer.R);
        }
    }

    public void RotateCW(ELayer layer)
    {
        switch(layer)
        {
            case ELayer.F:
                Debug.Log("F");
                StartCoroutine( RotateCube(F,_count,easing,Vector3.back));
                F = F.RotateCW();
                break;
            case ELayer.R:
                Debug.Log("R");
                StartCoroutine(RotateCube(R, _count, easing, Vector3.right));
                R = R.RotateCW();
                break;
        }

        IEnumerator RotateCube(CubeLayer cubelayer, int _count, Func<float, float> curve,Vector3 axis)
        {

            cubelayer.SetParentCenter();
            var from = cubelayer.lc.rotation;

            var to = Quaternion.AngleAxis(90f, axis);

            var quaternions = Enumerable.Range(1, _count)
                .Select(i => (float)i / (float)_count)
                .Select(curve)
                .Select(f => Quaternion.Slerp(from, to * from, f));

            foreach (var quaternion in quaternions)
            {
                cubelayer.lc.rotation = quaternion;
                //after rendering
                yield return new WaitForEndOfFrame();
            }


     



            cubelayer.SetParentRootobj(cubeContainer);

            Debug.Log("LotateEND");

            isRotating = false;


        }
    }

    // l0 l1 l2 --> l6 l7 l0
    // l7 lc l3 --> l5 lc l1
    // l6 l5 l4 --> l4 l3 l2

    public CubeLayer F
    {
        get { return new CubeLayer(cubes[10], cubes[18], cubes[19], cubes[20], cubes[11], cubes[2], cubes[1], cubes[0],cubes[9]); }
        set
        {
            // l0 l1 l2 
            // l7 lc l3 
            // l6 l5 l4 
            cubes[18] = value.l0; cubes[19] = value.l1; cubes[20] = value.l2;
            cubes[ 9] = value.l7; cubes[10] = value.lc; cubes[11] = value.l3;
            cubes[ 0] = value.l6; cubes[ 1] = value.l5; cubes[ 2] = value.l4;
        }
    } // front face (around z axis)

    public CubeLayer R// right face (around x axis)
    {
        get
        {
            return new CubeLayer(cubes[14], cubes[20], cubes[23], cubes[26], cubes[17], cubes[8], cubes[5], cubes[2], cubes[11]);
        }
        set
        {
            cubes[20] = value.l0; cubes[23] = value.l1; cubes[26] = value.l2;
            cubes[11] = value.l7; cubes[14] = value.lc; cubes[17] = value.l3;
            cubes[ 2] = value.l6; cubes[ 5] = value.l5; cubes[ 8] = value.l4;
        }
    }

    public CubeLayer U; // upper face (around y axis)
    public CubeLayer L; // left face (around x axis)

    public CubeLayer B; // back face (around z axis)
    public CubeLayer D; // down face (around y axis)
    public CubeLayer M; // middle layer around x-axis
    public CubeLayer E; // middle layer around y-axis
    public CubeLayer S; // middle layer around z-axis


}

public class CubeLayer
{
    // l0 l1 l2 --> l6 l7 l0
    // l7 lc l3 --> l5 lc l1
    // l6 l5 l4 --> l4 l3 l2
    public Transform lc,l0,l1, l2, l3, l4, l5, l6, l7;
    public CubeLayer(Transform cc, Transform c0, Transform c1, Transform c2, Transform c3, Transform c4, Transform c5, Transform c6, Transform c7)
    {
        l0 = c0; l1 = c1; l2 = c2;
        l7 = c7; lc = cc; l3 = c3;
        l6 = c6; l5 = c5; l4 = c4;
    }

    // l0 l1 l2 --> l6 l7 l0
    // l7 lc l3 --> l5 lc l1
    // l6 l5 l4 --> l4 l3 l2

    public CubeLayer RotateCW()
    {
        return new CubeLayer(lc, l6, l7, l0, l1, l2, l3, l4, l5);
    }

    public void SetParentCenter()
    {
        l0.SetParent(lc, true);
        l1.SetParent(lc, true);
        l2.SetParent(lc, true);
        l3.SetParent(lc, true);
        l4.SetParent(lc, true);
        l5.SetParent(lc, true);
        l6.SetParent(lc, true);
        l7.SetParent(lc, true);

        Debug.Log(l0.gameObject.name + l1.gameObject.name + l2.gameObject.name);
        Debug.Log(l7.gameObject.name + lc.gameObject.name + l3.gameObject.name);
        Debug.Log(l6.gameObject.name + l5.gameObject.name + l4.gameObject.name);

    }

    public void SetParentRootobj(Transform parentTransform)
    {

        l0.SetParent(parentTransform, true);
        l1.SetParent(parentTransform, true);
        l2.SetParent(parentTransform, true);
        l3.SetParent(parentTransform, true);
        l4.SetParent(parentTransform, true);
        l5.SetParent(parentTransform, true);
        l6.SetParent(parentTransform, true);
        l7.SetParent(parentTransform, true);
    }
	
}

public enum ELayer
{
    F, // front face (around z axis)
    R, // right face (around x axis)
    U, // upper face (around y axis)
    L, // left face (around x axis)
    B, // back face (around z axis)
    D, // down face (around y axis)
    M, // middle layer around x-axis
    E, // middle layer around y-axis
    S // middle layer around z-axis
}
