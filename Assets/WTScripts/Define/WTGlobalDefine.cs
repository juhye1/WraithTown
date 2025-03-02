using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class WTConstants
{
    public const float CameraRotationEuler_X = 35.3f;
    public const float CameraRotationEuler_Y = 45f;
    public const ushort StartStageID = 10001;
    public const ushort TotalStageTime = 300;
    public const ushort MaxHP = 100;
    public const string StrDay = "Day ";
    public const ushort UnitIDMiho = 11001;
    public const ushort UnitIDKebi = 11002;
}

public delegate void WTEventProcDelegate(int param1);
public delegate void WTEventProcDelegateObj(object param1);


static public class WTGlobal
{
    static WTEventProcDelegate[] _eventProcDelegates = new WTEventProcDelegate[(int)WTEventType.Count];
    static WTEventProcDelegateObj[] _eventProcDelegatesObj = new WTEventProcDelegateObj[(int)WTEventObjectType.Count];

    static public int[] animParamID_IntType = new int[(int)WTAnimParam_Int.Count];

    public static void RegisterEventDelegate(WTEventType evtType, WTEventProcDelegate dele)
    {
        bool bExist = false;
        if (_eventProcDelegates[(int)evtType] != null)
        {
            Delegate[] arrDele = _eventProcDelegates[(int)evtType].GetInvocationList();

            for (int i = 0; i < arrDele.Length; ++i)
            {
                if (arrDele[i].Target == dele.Target && arrDele[i].Method == dele.Method)
                {
                    bExist = true;
                    break;
                }
            }
        }

        if (bExist == false)
        {
            _eventProcDelegates[(int)evtType] += dele;
        }
    }

    public static void UnregisterEventDelegate(WTEventType evtType, WTEventProcDelegate dele)
    {
        _eventProcDelegates[(int)evtType] -= dele;
    }

    public static void CallEventDelegate(WTEventType evtType, int param)
    {
        if (_eventProcDelegates[(int)evtType] != null)
        {
            _eventProcDelegates[(int)evtType](param);
        }
    }

    public static void RegisterEventDelegateObj(WTEventObjectType evtType, WTEventProcDelegateObj dele)
    {
        bool bExist = false;
        if (_eventProcDelegatesObj[(int)evtType] != null)
        {
            Delegate[] arrDele = _eventProcDelegatesObj[(int)evtType].GetInvocationList();

            for (int i = 0; i < arrDele.Length; ++i)
            {
                if (arrDele[i].Target == dele.Target && arrDele[i].Method == dele.Method)
                {
                    bExist = true;
                    break;
                }
            }
        }

        if (bExist == false)
        {
            _eventProcDelegatesObj[(int)evtType] += dele;
        }
    }

    public static void UnregisterEventDelegateObj(WTEventObjectType evtType, WTEventProcDelegateObj dele)
    {
        _eventProcDelegatesObj[(int)evtType] -= dele;
    }

    public static void CallEventDelegateObj(WTEventObjectType evtType, object param)
    {
        if (_eventProcDelegatesObj[(int)evtType] != null)
        {
            _eventProcDelegatesObj[(int)evtType](param);
        }
        else
        {
        }
    }

}

