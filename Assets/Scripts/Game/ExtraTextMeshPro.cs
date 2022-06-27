using System;
using UnityEngine;
using TMPro;
/**
* @classdesc TestTextMeshPro
* @author Copyright (c) 2017-2020, w.l.hikaru (xiaoguang.wang@rjoy.com)
* @date
* @description
*/

public class ExtraTextMeshPro : MonoBehaviour
{
    public TextMeshProUGUI text1;
    public float faceDilate;
    // [Range(0,1)]
    public float outlineWidth;
    public float underlayOffsetX;
    public float underlayOffsetY;
    public float underlayDilate;
    public Color32 effectColor = Color.black;
    public Color32 underLayColor = Color.black;

    private Boolean isRefresh = false;
    
    // private void Update()
    // {
    //     if (!isRefresh)
    //     {
    //         Refresh();
    //         isRefresh = true;
    //     }
    // }

    public void Start()
    {
        Refresh();
    }

    private void OnValidate()
    {
        Refresh();
    }

    private void Refresh()
    {
        text1 = this.gameObject.GetComponent<TextMeshProUGUI>();
        text1.outlineWidth = outlineWidth;
        text1.underlayOffsetX = underlayOffsetX;
        text1.underlayOffsetY = underlayOffsetY;
        // text1.faceDilate = faceDilate;
        // text1.underlayDilate = underlayDilate;
        // text1.effectColorFloat = new Vector4(effectColor.r / 255f, effectColor.g / 255f, effectColor.b / 255f, effectColor.a / 255f);
        //
        // var a = 255 * (1 - (underLayColor.a / 255f));
        // // Debug.Log(underLayColor.a);
        // text1.underLayColorFloat = new Vector3(underLayColor.r / 255f, underLayColor.g / 255f, underLayColor.b / 255f);
        // //text1.underLayColorFloat = new Vector3(underLayColor.r / 255f+ a/255f,underLayColor.g / 255f+a/255f,underLayColor.b / 255f+ a/255f);
        Debug.Log(text1.underLayColorFloat);
    }
}