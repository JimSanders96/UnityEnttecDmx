﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Showtec;

public class DmxController : MonoBehaviour
{
    public bool active = false;
    [Range(0, 255)]
    public int strobeVal = 210;
    public bool masterFaderControlActive = false;
    [Range(0, 255)]
    public int masterFaderVal = 255;

    private float dmxSignalIntervalSeconds = 0;
    private float count;
    private float regularIntervalSeconds = 0.05f;
    private bool strobeActive = false;
    private float strobeIntervalSeconds = 0.05f;

    private void Awake()
    {

        ShowtecLLB8.Init();
        dmxSignalIntervalSeconds = regularIntervalSeconds;
        AllBlue(0);
        AllRed(1);
    }

    private void Update()
    {
        count += Time.deltaTime;
        if (count >= dmxSignalIntervalSeconds && active)
        {
            ResetCounter();

            if (masterFaderControlActive)
                SetMasterFader((byte)masterFaderVal);
            else
                ShowtecLLB8.SendData();
        }
    }

    private void ResetCounter()
    {
        count -= dmxSignalIntervalSeconds;
        if (count < 0)
            count = 0;
    }

    public void ToggleStrobe()
    {
        strobeActive = !strobeActive;
        SetStroboscope(strobeActive);
    }

    #region LedBar functions
    public void AllOff()
    {
        ShowtecLLB8.SetAllOff(true);
        ResetCounter();
    }

    public void SetMasterFader(byte value)
    {
        ResetCounter();
        ShowtecLLB8.SetMasterFader(value, true);
    }

    public void SetStroboscope(bool active)
    {
        dmxSignalIntervalSeconds = active ? strobeIntervalSeconds : regularIntervalSeconds;
        ResetCounter();

        byte value = active ? (byte)strobeVal : (byte)0;
        ShowtecLLB8.SetStroboscope(value, true);
    }



    public void AllBlue(int ledbarIndex = 0)
    {
        ShowtecLLB8.SetAllSingleColor(ShowtecLLB8.RGB.BLUE, 255, false, ledbarIndex);
        ShowtecLLB8.SetMasterFader(255, true, ledbarIndex);
        ResetCounter();
    }

    public void AllRed(int ledbarIndex = 0)
    {
        ShowtecLLB8.SetAllSingleColor(ShowtecLLB8.RGB.RED, 255, false, ledbarIndex);
        ShowtecLLB8.SetMasterFader(255, true, ledbarIndex);
        ResetCounter();
    }

    public void AllGreen(int ledbarIndex = 0)
    {
        ShowtecLLB8.SetAllSingleColor(ShowtecLLB8.RGB.GREEN, 255, false, ledbarIndex);
        ShowtecLLB8.SetMasterFader(255, true, ledbarIndex);
        ResetCounter();
    }

    #endregion

}
