using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class RubicCube : MonoBehaviour
{
    public List<Transform> cubes;
    [SerializeField] Transform cubeContainer;
    [SerializeField] Timer timer;

    [SerializeField] public bool isRotating;
    [SerializeField] bool isRandomMoving;
    [SerializeField] int _count = 7;
    [SerializeField] GameObject touchmanager;
    public Func<float, float> easing;

    [SerializeField] int randomcount =3;
    [SerializeField] int _count2 = 1;

    static System.Random random = new System.Random();
    private List<ELayer> randomMove= new List<ELayer>();

    [SerializeField] GameObject startPanel;
    [SerializeField] GameObject endPanel;

    Stack<Command> stack = new Stack<Command>();


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
        isRandomMoving = false;

        endPanel.SetActive(false);
        startPanel.SetActive(false);

        easing = Easings.CubicEaseInOut;
        StartCoroutine(TouchManagerSetActive());

        //var startcount = _count;
        //_count = _count2;

        for (int i = 0; i < randomcount; i++)
        {
            ELayer layer = new ELayer();
            switch (random.Next(9))
            {
                case 0:
                    layer = ELayer.F;
                    break;
                case 1:
                    layer = ELayer.R;
                    break;
                case 2:
                    layer = ELayer.U;
                    break;
                case 3:
                    layer = ELayer.L;
                    break;
                case 4:
                    layer = ELayer.B;
                    break;
                case 5:
                    layer = ELayer.D;
                    break;
                case 6:
                    layer = ELayer.M;
                    break;
                case 7:
                    layer = ELayer.E;
                    break;
                case 8:
                    layer = ELayer.S;
                    break;
            }

            randomMove.Add(layer);
            Command command = new Command(layer, true);
        }

        StartCoroutine(GameStart());


        //Debug.Log(stack.Peek().layer);

        //_count = startcount;
    }

    IEnumerator GameStart()
    {
        isRandomMoving = true;

        yield return new WaitForSeconds(.5f);


        for (int i = 0; i < randomMove.Count; i++)
        {
            Debug.Log("randomMove" + randomMove[i]);
            RotateCW(randomMove[i],true);

            while (isRotating)
                yield return null;
        }

        isRandomMoving = false;

        startPanel.SetActive(true);

        yield return new WaitForSeconds(1f);


        startPanel.SetActive(false);

        timer.StartTimer();
    }

    public void Undo()
    {
        if(stack.Count > 0 && isRotating == false)
        {
            var laststack = stack.Pop();
            RotateCW(laststack.layer, !laststack.isCW, true);

            Debug.Log(" undo finished remain stack.Count" + stack.Count); 
        }
    }


    //TODO check TouchManager behavior
    IEnumerator TouchManagerSetActive()
    {
        yield return new WaitForSeconds(.3f);

        touchmanager.SetActive(true);
    }


/// <summary>
/// rotate test method 
/// </summary>
    public void PressButtonF()
    {
        RotateCW(ELayer.F, true);
    }
    public void PressButtonR()
    {
        RotateCW(ELayer.R, true);
    }
    public void PressButtonU()
    {       
        RotateCW(ELayer.U, true);
    }
    public void PressButtonL()
    {
        RotateCW(ELayer.L, true);
    }
    public void PressButtonB()
    {      
         RotateCW(ELayer.B, true);        
    }
    public void PressButtonD()
    {
         RotateCW(ELayer.D, true);
    }
    public void PressButtonM()
    {
        RotateCW(ELayer.M, true);   
    }
    public void PressButtonE()
    {
        RotateCW(ELayer.E, true);
    }
    public void PressButtonS()
    {
         RotateCW(ELayer.S,true);
    }


    public void RotateCW(ELayer layer,bool isInverse ,bool isUndo = false)
    {
        if (!isRotating)
        {
            isRotating = true;

            switch (layer)
            {
                case ELayer.F:
                    Debug.Log("F");
                    StartCoroutine(RotateCube(F, _count, easing, Vector3.back, isInverse));
                    F = F.RotateCW(isInverse);
                    if (!isUndo)
                    {
                        stack.Push(new Command(ELayer.F,isInverse));
                    }
                    break;
                case ELayer.R:
                    Debug.Log("R");
                    StartCoroutine(RotateCube(R, _count, easing, Vector3.right, isInverse));
                    R = R.RotateCW(isInverse);
                    if (!isUndo)
                    {
                        stack.Push(new Command(ELayer.R, isInverse));
                    }
                    break;
                case ELayer.U:
                    Debug.Log("U");
                    StartCoroutine(RotateCube(U, _count, easing, Vector3.up, isInverse));
                    U = U.RotateCW(isInverse);
                    if (!isUndo)
                    {
                        stack.Push(new Command(ELayer.U, isInverse));
                    }
                    break;
                case ELayer.L:
                    Debug.Log("L");
                    StartCoroutine(RotateCube(L, _count, easing, Vector3.right, isInverse));
                    L = L.RotateCW(isInverse);
                    if (!isUndo)
                    {
                        stack.Push(new Command(ELayer.L, isInverse));
                    }
                    break;
                case ELayer.B:
                    Debug.Log("B");
                    StartCoroutine(RotateCube(B, _count, easing, Vector3.back, isInverse));
                    B = B.RotateCW(isInverse);
                    if (!isUndo)
                    {
                        stack.Push(new Command(ELayer.B, isInverse));
                    }
                    break;
                case ELayer.D:
                    Debug.Log("D");
                    StartCoroutine(RotateCube(D, _count, easing, Vector3.up, isInverse));
                    D = D.RotateCW(isInverse);
                    if (!isUndo)
                    {
                        stack.Push(new Command(ELayer.D, isInverse));
                    }
                    break;
                case ELayer.M:
                    Debug.Log("M");
                    StartCoroutine(RotateCube(M, _count, easing, Vector3.right, isInverse));
                    M = M.RotateCW(isInverse);
                    if (!isUndo)
                    {
                        stack.Push(new Command(ELayer.M, isInverse));
                    }
                    break;
                case ELayer.E:
                    Debug.Log("E");
                    StartCoroutine(RotateCube(E, _count, easing, Vector3.up, isInverse));
                    E = E.RotateCW(isInverse);
                    if (!isUndo)
                    {
                        stack.Push(new Command(ELayer.E, isInverse));
                    }
                    break;
                case ELayer.S:
                    Debug.Log("S");
                    StartCoroutine(RotateCube(S, _count, easing, Vector3.back, isInverse));
                    S = S.RotateCW(isInverse);
                    if (!isUndo)
                    {
                        stack.Push(new Command(ELayer.S, isInverse));
                    }
                    break;
            }

            if (!isRandomMoving)
            {
                CheckStatus();
            }


            Debug.Log("stack.Count :" + stack.Count);

        }

       

        IEnumerator RotateCube(CubeLayer cubelayer, int _count, Func<float, float> curve,Vector3 axis, bool isCW)
        {
            var aDegree = isCW ? 90f : -90f;

            cubelayer.SetParentCenter();

            var from = cubelayer.lc.rotation;
            var to = Quaternion.AngleAxis(aDegree, axis);

            float count;

            if (isRandomMoving)
            {
                count = _count / 3;
            }
            else
            {
                count = _count;
            }

            var quaternions = Enumerable.Range(1, _count)
                .Select(i => (float)i / (float)count)
                .Select(curve)
                .Select(f => Quaternion.Slerp(from, to * from, f));

            foreach (var quaternion in quaternions)
            {
                cubelayer.lc.localRotation = quaternion;
                //after rendering
                yield return new WaitForEndOfFrame();
            }

            cubelayer.SetParentRootobj(cubeContainer);

            Debug.Log("LotateEND");

            isRotating = false;

        }
    }

    public void GameEnd()
    {
        endPanel.SetActive(true);
        timer.StopTimer();
        //Debug.LogWarning("Congratulations!!");
    }

    public void CheckStatus()
    {
        Debug.Log("Check");

        if (cubes[0].name == "Cube"     &&
            cubes[1].name == "Cube (1)" &&
            cubes[2].name == "Cube (2)" &&
            cubes[3].name == "Cube (3)" &&
            cubes[4].name == "Cube (4)" &&
            cubes[5].name == "Cube (5)" &&
            cubes[6].name == "Cube (6)" &&
            cubes[7].name == "Cube (7)" &&
            cubes[8].name == "Cube (8)" &&
            cubes[9].name == "Cube (9)" &&
            cubes[10].name == "Cube (10)" &&
            cubes[11].name == "Cube (11)" &&
            cubes[12].name == "Cube (12)" &&
            cubes[13].name == "Cube (13)" &&
            cubes[14].name == "Cube (14)" &&
            cubes[15].name == "Cube (15)" &&
            cubes[16].name == "Cube (16)" &&
            cubes[17].name == "Cube (17)" &&
            cubes[18].name == "Cube (18)" &&
            cubes[19].name == "Cube (19)" &&
            cubes[20].name == "Cube (20)" &&
            cubes[21].name == "Cube (21)" &&
            cubes[22].name == "Cube (22)" &&
            cubes[23].name == "Cube (23)" &&
            cubes[24].name == "Cube (24)" &&
            cubes[25].name == "Cube (25)" &&
            cubes[26].name == "Cube (26)" 
        ) GameEnd();
    }

    // l0 l1 l2 --> l6 l7 l0
    // l7 lc l3 --> l5 lc l1
    // l6 l5 l4 --> l4 l3 l2

    public CubeLayer F
    {
        get { return new CubeLayer(cubes[10], cubes[18], cubes[19], cubes[20], cubes[11], cubes[2], cubes[1], cubes[0],cubes[9]); }
        set
        {
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

    public CubeLayer U // upper face (around y axis)
    {
        get
        {
            return new CubeLayer(cubes[22], cubes[24], cubes[25], cubes[26], cubes[23], cubes[20], cubes[19], cubes[18], cubes[21]);
        }
        set
        {
            cubes[24] = value.l0; cubes[25] = value.l1; cubes[26] = value.l2;
            cubes[21] = value.l7; cubes[22] = value.lc; cubes[23] = value.l3;
            cubes[18] = value.l6; cubes[19] = value.l5; cubes[20] = value.l4;
        }
    }
    public CubeLayer L // left face (around x axis)
    {
        get
        {
            return new CubeLayer(cubes[12], cubes[18], cubes[21], cubes[24], cubes[15], cubes[6], cubes[3], cubes[0], cubes[9]);
        }
        set
        {
            cubes[18] = value.l0; cubes[21] = value.l1; cubes[24] = value.l2;
            cubes[ 9] = value.l7; cubes[12] = value.lc; cubes[15] = value.l3;
            cubes[ 0] = value.l6; cubes[ 3] = value.l5; cubes[ 6] = value.l4;
        }
    }
    public CubeLayer B // back face (around z axis)
    {
        get
        {
            return new CubeLayer(cubes[16], cubes[24], cubes[25], cubes[26], cubes[17], cubes[8], cubes[7], cubes[6], cubes[15]);
        }
        set
        {
            cubes[24] = value.l0; cubes[25] = value.l1; cubes[26] = value.l2;
            cubes[15] = value.l7; cubes[16] = value.lc; cubes[17] = value.l3;
            cubes[ 6] = value.l6; cubes[ 7] = value.l5; cubes[ 8] = value.l4;
        }
    }
    public CubeLayer D // down face (around y axis)
    {
        get
        {
            return new CubeLayer(cubes[4], cubes[6], cubes[7], cubes[8], cubes[5], cubes[2], cubes[1], cubes[0], cubes[3]);
        }
        set
        {
            cubes[ 6] = value.l0; cubes[ 7] = value.l1; cubes[ 8] = value.l2;
            cubes[ 3] = value.l7; cubes[ 4] = value.lc; cubes[ 5] = value.l3;
            cubes[ 0] = value.l6; cubes[ 1] = value.l5; cubes[ 2] = value.l4;
        }
    }
    public CubeLayer M // middle layer around x-axis
    {
        get
        {
            return new CubeLayer(cubes[13], cubes[19], cubes[22], cubes[25], cubes[16], cubes[7], cubes[4], cubes[1], cubes[10]);
        }
        set
        {
            cubes[19] = value.l0; cubes[22] = value.l1; cubes[25] = value.l2;
            cubes[10] = value.l7; cubes[13] = value.lc; cubes[16] = value.l3;
            cubes[1] = value.l6; cubes[4] = value.l5; cubes[7] = value.l4;
        }
    }
    public CubeLayer E // middle layer around y-axis
    {
        get
        {
            return new CubeLayer(cubes[13], cubes[15], cubes[16], cubes[17], cubes[14], cubes[11], cubes[10], cubes[9], cubes[12]);
        }
        set
        {
            cubes[15] = value.l0; cubes[16] = value.l1; cubes[17] = value.l2;
            cubes[12] = value.l7; cubes[13] = value.lc; cubes[14] = value.l3;
            cubes[ 9] = value.l6; cubes[10] = value.l5; cubes[11] = value.l4;
        }
    }
    public CubeLayer S // middle layer around z-axis
    {
        get
        {
            return new CubeLayer(cubes[13], cubes[21], cubes[22], cubes[23], cubes[14], cubes[5], cubes[4], cubes[3], cubes[12]);
        }
        set
        {
            cubes[21] = value.l0; cubes[22] = value.l1; cubes[23] = value.l2;
            cubes[12] = value.l7; cubes[13] = value.lc; cubes[14] = value.l3;
            cubes[ 3] = value.l6; cubes[ 4] = value.l5; cubes[ 5] = value.l4;
        }
    }


}

public class CubeLayer
{

    public Transform lc,l0,l1, l2, l3, l4, l5, l6, l7;
    public CubeLayer(Transform cc, Transform c0, Transform c1, Transform c2, Transform c3, Transform c4, Transform c5, Transform c6, Transform c7)
    {
        l0 = c0; l1 = c1; l2 = c2;
        l7 = c7; lc = cc; l3 = c3;
        l6 = c6; l5 = c5; l4 = c4;
    }

    //TODO use Matrix to extend
    // l0 l1 l2 --> l6 l7 l0
    // l7 lc l3 --> l5 lc l1
    // l6 l5 l4 --> l4 l3 l2

    public CubeLayer RotateCW(bool isCW)
    {
        if (isCW)
        {
            return new CubeLayer(lc, l6, l7, l0, l1, l2, l3, l4, l5);

        }else
        {
    // c0 c1 c2 --> c2 c3 c4
    // c7 C  c3 --> c1 C  c5
    // c6 c5 c4 --> c0 c7 c6
            return new CubeLayer(lc, l2, l3, l4, l5, l6, l7, l0, l1);
        }
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

        //Debug.Log(l0.gameObject.name + l1.gameObject.name + l2.gameObject.name);
        //Debug.Log(l7.gameObject.name + lc.gameObject.name + l3.gameObject.name);
        //Debug.Log(l6.gameObject.name + l5.gameObject.name + l4.gameObject.name);

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
    F = 0, // front  face (around z axis)
    R = 1, // right  face (around x axis)
    U = 2, // upper  face (around y axis)
    L = 3, // left   face (around x axis)
    B = 4, // back   face (around z axis)
    D = 5, // down   face (around y axis)
    M = 6, // middle layer(around x-axis)
    E = 7, // middle layer(around y-axis)
    S = 8  // middle layer(around z-axis)
}

public class Command
{
    public ELayer layer { get; private set; }
    public bool isCW { get; private set; }

    public Command(ELayer layer, bool isCW)
    {
        this.layer = layer;
        this.isCW = isCW;
    }
}
