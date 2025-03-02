using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public interface BaseObject
{
    public abstract void Init();
    public abstract void Setup();
    public abstract void OnAttack();
    public abstract void OnTakeDamaged(int damage);
    public abstract void DeathEvt();

}
