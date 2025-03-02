using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ESortLayer
{
    Ground = 0,
    Default = 1,
    Count
}

public enum WTEventType
{
    SaveDataLoaded = 0,
    MapLoaded = 1,
    AddressableGroupLoadComplete = 2,
    PlayerHPControl = 3,
    PlayerMaxHPControl = 3,
    Timer = 4,
    ChangeGold = 5,
    ChangePoint = 6,
    ChangeStage = 7,
    Count
}

public enum WTGameState
{
    None = 0,
    Lobby,
    Loading,
    Game,
    SelectCharacter
};


public enum WTGameMode
{
    None = 0,
    PlayFromStart = 1,
    ContinueGame = 2
};

public enum WTEffectType
{
    MaxHPIncrease = 0,
    HealHp,
    RangeIncrease,
    AttackSpeedIncrease,
    ProjectileCountIncrease,
    TileGradeChange,
    TilePositionChange,
    GetRandomSoilUnit,
    GetRandomFireUnit,
    GetRandomGoldUnit,
    GetRandomMoonUnit,
    GetRandomWaterUnit,
    GetRandomGhostUnit,
    GetRandomYokaiUnit,
    Count
}

public enum WTEventObjectType
{
    SpawnPC = 0,
    SpawnNPC = 1,
    DestroyCharacter = 2,
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
