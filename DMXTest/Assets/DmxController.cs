using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Showtec;

public class DmxController : MonoBehaviour
{
    public bool active = false;
    [Range(0,255)]
    public int strobeVal = 210;

    private float dmxSignalIntervalSeconds = 0;
    private float count;
    private float regularIntervalSeconds = 0.25f;
    private bool strobeActive = false;
    private float strobeIntervalSeconds = 0.1f;

    private void Awake()
    {
        ShowtecLLB8.Init();
        dmxSignalIntervalSeconds = regularIntervalSeconds;
    }

    private void Update()
    {
        count += Time.deltaTime;
        if (count >= dmxSignalIntervalSeconds && active)
        {
            ResetCounter();
            ShowtecLLB8.SendData();
        }
    }

    private void ResetCounter()
    {
        count -= dmxSignalIntervalSeconds;
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

    public void AllBlue()
    {
        ShowtecLLB8.SetAllSingleColor(ShowtecLLB8.RGB.BLUE, 255, false);
        ShowtecLLB8.SetMasterFader(255, true);
        ResetCounter();
    }

    public void AllRed()
    {
        ShowtecLLB8.SetAllSingleColor(ShowtecLLB8.RGB.RED, 255, false);
        ShowtecLLB8.SetMasterFader(255, true);
        ResetCounter();
    }

    public void AllGreen()
    {
        ShowtecLLB8.SetAllSingleColor(ShowtecLLB8.RGB.GREEN, 255, false);
        ShowtecLLB8.SetMasterFader(255, true);
        ResetCounter();
    }
    #endregion

}
