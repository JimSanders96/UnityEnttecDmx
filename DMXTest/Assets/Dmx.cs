using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Showtec;

public class Dmx : MonoBehaviour
{

    public float intervalSeconds = 0.25f;
    public bool active = false;

    private float count;

    private void Awake()
    {
        OpenDMX.start();
        ShowtecLLB8.AllOff();
    }

    private void Update()
    {
        count += Time.deltaTime;
        if (count >= intervalSeconds && active)
        {
            count -= intervalSeconds;
            OpenDMX.writeData();
        }
    }

    private void OnApplicationQuit()
    {

    }

    public void AllBlue()
    {
        ShowtecLLB8.AllSingleColor(ShowtecLLB8.RGB.BLUE, 255, 255);
    }

    public void AllRed()
    {
        ShowtecLLB8.AllSingleColor(ShowtecLLB8.RGB.RED, 255, 255);
    }

    public void AllGreen()
    {
        ShowtecLLB8.AllSingleColor(ShowtecLLB8.RGB.GREEN, 255, 255);
    }

}
