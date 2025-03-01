using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ESortLayer
{
    Ground = 0,
    Default = 1,
    Count
}

// object ��
public enum WTEventType
{
    MapLoaded = 0,
    AddressableGroupLoadComplete = 1,
    Count
}

// object�� param���� ���� �̺�Ʈ Ÿ�Ե�
public enum WTEventObjectType
{
    SpawnPC = 0,
    SpawnNPC = 1,
    DestroyCharacter = 3,
    Count
}

public enum WTCharacterAnimState
{
    None = -1,
    Locomotion = 0,
    Emotion = 1,
    EventMotion = 2,
    Count
}

public enum WTAnimParam_Int
{
    State,
    SubState,
    Count
}