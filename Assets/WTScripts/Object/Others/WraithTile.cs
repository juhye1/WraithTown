using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WraithTile : MonoBehaviour
{
    public GameObject wraith;
    public SpriteRenderer sr;
    public bool isSpecial;
    public int index;

    public void SetTileColor()
    {
        sr.color = Color.cyan;
    }
}
