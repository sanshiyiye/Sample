/**
* @classdesc SerializablePoolTypes
* @author Copyright (c) 2017-2020, w.l.hikaru (xiaoguang.wang@rjoy.com)
* @date
* @description
*/

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Pool
{
    [Serializable]
    public sealed class TransformPool : SerializablePool<Transform>
    {
    }
    
    [Serializable]
    public sealed class SpriteRendererPool : SerializablePool<SpriteRenderer>
    {
    }
    [Serializable]
    public sealed class RectTransformPool : SerializablePool<RectTransform>
    {
    }
    [Serializable]
    public sealed class ImagePool : SerializablePool<Image>
    {
    }

    [Serializable]
    public sealed class TMP_TextPool : SerializablePool<TextMeshProUGUI>
    {
        
    }
    
    
    
}