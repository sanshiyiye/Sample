using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public static class DOTweenUtil
{
	public static void MoveGameObjectPosition(Transform transform, Transform endtransform, float time)
    {
        Vector3 endVector3 = endtransform.localPosition;
        Tweener tweener = transform.DOLocalMove(endVector3, time);
        //tweener.onPlay();
    }
		public static Tweener DoGameObjectMoveAnchorPos(Transform transform, Vector2 endPos, float time,UnityAction cb = null  )
		{
			if(transform is RectTransform rectTrans){
				Tweener tweener = rectTrans.DOAnchorPos(endPos,time );
        tweener.onComplete = () =>
        {
            if (cb != null)
            {
                cb();
            }
        };
				return tweener;
			}
			return null;
		}

    public static void MoveGameObjectRotation(Transform transform, Transform endtransform, float time)
    {
        Quaternion endValue = endtransform.localRotation;

        Tweener tweener = transform.DOLocalRotateQuaternion(endValue, time);
        //tweener.onPlay();
    }

    public static void SetCameraFov(Transform transform)
    {
        DOTweenAnimation doTweenAnimation = transform.GetComponent<DOTweenAnimation>();
        if (doTweenAnimation)
        {
            doTweenAnimation.DORestart();
        }
    }

    public static void SetSliderValue(Slider slider, float endValue, float duration)
    {
        Tweener tweener = slider.DOValue(endValue, duration);
    }
    
    //设置缩放
    public static void DoGameObjectScale(Transform transform, float scale, float time,UnityAction cb = null )
    {
        Tweener tweener = transform.DOScale(scale, time);
        tweener.onComplete = () =>
        {
            if (cb != null)
            {
                cb();
            }
        };
    }
    
    public static void DoGameObjectMove(Transform transform, Transform endtransform, float time,UnityAction cb = null )
    {
        Vector3 endVector3 = endtransform.position;
        Tweener tweener = transform.DOMove(endVector3, time);
        tweener.onComplete = () =>
        {
            if (cb != null)
            {
                cb();
            }
        };
    }
    
}
