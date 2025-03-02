using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class MapSettings : MonoBehaviour
{
    [SerializeField]
    Collider2D coll;
    [SerializeField]
    CinemachineConfiner2D Confiner2D;
    [SerializeField]
    CinemachineVirtualCamera VirtualCamera;
    // Start is called before the first frame update
    void Start()
    {
        Confiner2D.m_BoundingShape2D = coll;
        VirtualCamera.Follow = BasePlayer.Instance.transform;
    }

}
