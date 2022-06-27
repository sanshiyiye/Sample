/*
* @classdesc CustomAxisSortCamera
* @author Copyright (c) 2017-2020, w.l.hikaru (xiaoguang.wang@rjoy.com)
* @date
* @description
*/

using UnityEngine;

public class CustomAxisSortCamera : MonoBehaviour 
{
    void Start()
    {
        var camera = GetComponent<Camera>();
        camera.transparencySortMode = TransparencySortMode.CustomAxis;
        camera.transparencySortAxis = new Vector3(0.0f, 0.5f, -0.25f);
    }
}