using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenceCameraControl : MonoBehaviour
{
    private bool lockControl = false;
    private Vector3 recordPosition;
    private float orgDistance = 0f;
    private float curDistance = 0f;
    private Vector2 touch1;
    private Vector2 touch2;
    private float diff;
    private float offsetZ;
    private bool isControlInput;
    public float maxDistance = 20f;
    public float backDistance = 18f;
    public float minDistance = 0f;
    public float scaleFactor = 0.08f;
    public float backSpeed = 10.0f;
    public Vector2 distanceAngle = new Vector2(-20.0f,20.0f);
    public float minoffsetY = 0f;
    public float maxoffsetY = 0f;
    public Vector3 CameraMinPos = Vector3.zero;
    public Vector3 CameraMaxPos = Vector3.zero;

    public Transform backGround;
    private Vector3 backgroudRestPosition;

    public Vector3 bgMinDistance = Vector3.zero;
    public Vector3 bgMaxDistance = Vector3.zero;

    private void Awake()
    {
        recordPosition = transform.localPosition;
        backGround = transform.root.Find("ground");
        backgroudRestPosition = backGround.localPosition;
        LoadProfile();
        UpdateCameraPosition(0);
    }

    public void SetLock()
    {
        lockControl = true;
    }

    public void UnLock()
    {
        lockControl = false;
    }

    private void ResetPosition()
    {
        transform.localPosition = recordPosition;
    }

    private void LoadProfile()
    {
        //transform.GetComponent<PostProcessVolume>().profile = Resources.Load<PostProcessProfile>("hero_Camera Profile");
    }

    private void Update()
    {
        if (!lockControl)
        {
            if (Application.isMobilePlatform)
            {
                TouchLogic();
            }
            else
            {
                MouseLogic();
            }
        }

        if (!IsControlInput())
        {
            UpateToBackPosition();
        }
    }

    private void MouseLogic()
    {
        var scrollValue = Input.GetAxis("Mouse ScrollWheel");

        if (scrollValue != 0)
        {
            isControlInput = true;
            UpdateCameraPosition(scrollValue * 70);
        }
        else
        {
            isControlInput = false;
        }    
    }

    private void TouchLogic()
    {
        if (Input.touchCount != 2)
        {
            isControlInput = false;
        }

        if (Input.touchCount == 2 && !IsControlInput())
        {
            touch1 = Input.GetTouch(0).position;
            touch2 = Input.GetTouch(1).position;
            orgDistance = Vector2.Distance(touch1, touch2);
            isControlInput = true;
        }

        if (Input.touchCount == 2)
        {
            curDistance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
            diff = (curDistance - orgDistance);
            UpdateCameraPosition(diff);
        }
    }

    private void UpdateCameraPosition(float zChange)
    {
        offsetZ += zChange * scaleFactor;
        offsetZ = Mathf.Clamp(offsetZ, minDistance, maxDistance);
        float lerpvalue = Mathf.InverseLerp(minDistance, maxDistance, offsetZ);
        //distanceAngle.y = Mathf.Lerp(minoffsetY, maxoffsetY, lerpvalue);
        //var directionX = GameTools.AngleToDirection(distanceAngle.x);
        //var directionY = GameTools.AngleToDirection(distanceAngle.y);
        //var direction = (directionX + new Vector3(0, directionY.x, directionY.z)).normalized;
        //var offset = direction * offsetZ;
        //transform.localPosition = recordPosition + offset;
        transform.localPosition = Vector3.Lerp(CameraMinPos, CameraMaxPos, lerpvalue);
        backGround.localPosition = Vector3.Lerp(bgMinDistance, bgMaxDistance, lerpvalue);
    }

    private void UpateToBackPosition()
    {
        if (offsetZ > backDistance)
        {
            offsetZ -= Time.deltaTime * backSpeed;

            if (offsetZ < backDistance)
            {
                offsetZ = backDistance;
            }

            UpdateCameraPosition(0.0f);
        }
    }

    public bool IsControlInput()
    {
        return isControlInput;
    }
}
