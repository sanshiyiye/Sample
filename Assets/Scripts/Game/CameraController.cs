using System;
using UnityEngine;

/**
* @classdesc CameraController
* @author Copyright (c) 2017-2020, w.l.hikaru (xiaoguang.wang@rjoy.com)
* @date
* @description
*/

public class CameraController : MonoBehaviour
{
        // 模型
        public Transform model;
        // 旋转速度
        public float rotateSpeed = 32f;
        public float rotateLerp = 8;    
        // 移动速度
        public float moveSpeed = 1f;
        public float moveLerp = 10f;
        // 镜头拉伸速度
        public float zoomSpeed = 10f;
        public float zoomLerp = 4f;     

        // 计算移动
        private Vector3 position, targetPosition;
        // 计算旋转
        private Quaternion rotation, targetRotation;
        // 计算距离
        private float distance, targetDistance;
        // 默认距离
        private const float default_distance = 5f;
        // y轴旋转范围
        private const float min_angle_y = -89f;
        private const float max_angle_y = 89f;

        public bool useKeyBoard = false;
        
        // Use this for initialization
        void Start ()
        {

                // 旋转归零
                targetRotation = Quaternion.identity; 
                // 初始位置是模型
                targetPosition = model.position;
                // 初始镜头拉伸
                targetDistance = default_distance;
        }

        // Update is called once per frame
        void Update()
        {
                
                if (Application.isMobilePlatform)
                {
                        TouchLogic();
                }
                else
                {
                        if (useKeyBoard)
                        {
                                KeyBoardLogic();
                        }
                        else
                        { 
                                MouseLogic();  
                        }
                       
                }
                
                           
        }

        /// <summary>
        /// 键盘控制相机
        /// </summary>
        private void KeyBoardLogic()
        {
                float d_target_distance = targetDistance;
                if (d_target_distance < 2f)
                {
                        d_target_distance = 2f;
                }
                if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
                {
                        targetPosition -= transform.up * d_target_distance / (2f * default_distance);
                }

                if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
                {
                        targetPosition += transform.up * d_target_distance / (2f * default_distance);
                }

                if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
                {
                        targetPosition += transform.right * d_target_distance / (2f * default_distance);
                }

                if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
                {
                        targetPosition -= transform.right * d_target_distance / (2f * default_distance);
                }     
        }

        Touch oldTouch1,oldTouch2;
        
        private void TouchLogic()
        {
                if (Input.touchCount <= 0)
                {
                        return;
                }
                float d_target_distance = targetDistance;
                if (Input.touchCount == 1)
                {
                        Touch touch = Input.GetTouch(0);
                        Vector2 deltaPos = touch.deltaPosition;
                       // moveSpeed = 0.1f;
                        deltaPos.x *= moveSpeed * d_target_distance /  default_distance;
                        deltaPos.x = deltaPos.x / 100;
                        deltaPos.y *= moveSpeed * d_target_distance / default_distance;
                        deltaPos.y = deltaPos.y / 100;
                        // Debug.Log(String.Format("x:{0:F2},y:{0:F2}",deltaPos.x,deltaPos.y) );
                        targetPosition -= transform.up * deltaPos.y + transform.right * deltaPos.x;
                }
                else
                {
                        Touch newTouch1 = Input.GetTouch(0);
                        Touch newTouch2 = Input.GetTouch(1);
                        if (newTouch2.phase==TouchPhase.Began)
                        {
                                oldTouch1 = newTouch1;
                                oldTouch2 = newTouch2;
                                return;
                        }
                        //计算老的两点距离和新的两点间距离，变大要放大模型，变小要缩放模型  
                        float oldDistance = Vector2.Distance(oldTouch1.position, oldTouch2.position);
                        float newDistance = Vector2.Distance(newTouch1.position, newTouch2.position);
                        //两个距离之差，为正表示放大手势， 为负表示缩小手势  
                        float offset = newDistance - oldDistance;
                        //放大因子， 一个像素按 0.01倍来算(100可调整)  
                        float scaleFactor = offset / 1000;
                        targetDistance -= scaleFactor * zoomSpeed;  
                        // Debug.Log(String.Format("targetDistance:{0:F2},",targetDistance) );
                        oldTouch1 = newTouch1;
                        oldTouch2 = newTouch2;
                }
        }

        // private void CalculateDistance(float dx,float dy,float d_target_distance)
        // {
        //         dx *= moveSpeed * d_target_distance /  default_distance;
        //         dx = dx / 100;
        //         dy *= moveSpeed * d_target_distance / default_distance;
        //         dy = dy / 100;
        //         Debug.Log(String.Format("x:{0:F2},y:{0:F2}",dx,dy) );
        //         targetPosition -= transform.up * dy + transform.right * dx;
        // }
        
        private void MouseLogic()
        {
                float dx = Input.GetAxis("Mouse X");
                float dy = Input.GetAxis("Mouse Y");

                // 异常波动
                if (Mathf.Abs(dx) > 5f || Mathf.Abs(dy) > 5f)
                {
                        return;
                }

                float d_target_distance = targetDistance;
                if (d_target_distance < 2f)
                {
                        d_target_distance = 2f;
                }

                // 鼠标左键移动
                if (Input.GetMouseButton(0))
                {
                        // Debug.Log(String.Format("x:{0:F2},y:{0:F2}",dx,dy) );
                        dx *= moveSpeed * d_target_distance / default_distance;
                        dy *= moveSpeed * d_target_distance / default_distance;
                        targetPosition -= transform.up * dy + transform.right * dx;
                        
                }

                // 鼠标右键旋转
                if (Input.GetMouseButton(1))
                {
                        dx *= rotateSpeed;
                        dy *= rotateSpeed;
                        if (Mathf.Abs(dx) > 0 || Mathf.Abs(dy) > 0)
                        {
                                // 获取摄像机欧拉角
                                Vector3 angles = transform.rotation.eulerAngles;
                                // 欧拉角表示按照坐标顺序旋转，比如angles.x=30，表示按x轴旋转30°，dy改变引起x轴的变化
                                angles.x = Mathf.Repeat(angles.x + 180f, 360f) - 180f;
                                angles.y += dx;
                                angles.x -= dy;
                                angles.x = ClampAngle(angles.x, min_angle_y, max_angle_y);
                                // 计算摄像头旋转
                                targetRotation.eulerAngles = new Vector3(angles.x, angles.y, 0);
                                // 随着旋转，摄像头位置自动恢复
                                Vector3 temp_position =
                                        Vector3.Lerp(targetPosition, model.position, Time.deltaTime * moveLerp);
                                targetPosition = Vector3.Lerp(targetPosition, temp_position, Time.deltaTime * moveLerp);
                        }
                }


                KeyBoardLogic();
                // 鼠标滚轮拉伸
                targetDistance -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;   
        }

        // 控制旋转角度范围：min max
        float ClampAngle(float angle, float min, float max)
        {
                // 控制旋转角度不超过360
                if (angle < -360f) angle += 360f;
                if (angle > 360f) angle -= 360f;
                return Mathf.Clamp(angle, min, max);
        }       

        private void FixedUpdate()
        {
                rotation = Quaternion.Slerp(rotation, targetRotation, Time.deltaTime * rotateLerp);
                position = Vector3.Lerp(position, targetPosition, Time.deltaTime * moveLerp);
                distance = Mathf.Lerp(distance, targetDistance, Time.deltaTime * zoomLerp);
                // 设置摄像头旋转
                transform.rotation = rotation;
                // 设置摄像头位置
                transform.position = position - rotation * new Vector3(0, 0, distance);
        }
    
}
