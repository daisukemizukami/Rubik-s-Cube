using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    private float countTime = 0;
    private bool isPlaying;

    private void Start()
    {
        isPlaying = false;
    }

    public void StartTimer()
    {
        isPlaying = true;
    }

    public void StopTimer()
    {
        isPlaying = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (isPlaying) { 
            countTime += Time.deltaTime; //スタートしてからの秒数を格納
        }
        GetComponent<Text>().text = countTime.ToString("F2"); //小数2桁にして表示
    }
}