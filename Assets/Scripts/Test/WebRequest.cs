/*
* @classdesc WebRequest
* @author Copyright (c) 2017-2020, w.l.hikaru (xiaoguang.wang@rjoy.com)
* @date
* @description
*/

using System;
using System.Collections;
using System.IO;
using System.IO.Compression;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Test
{
    public class WebRequest : MonoBehaviour
    {
        IEnumerator Start()
        {
            var request = UnityWebRequest.Get("{your app engine url hare}");
            request.SetRequestHeader("accept-encoding", "gzip");
            request.SetRequestHeader("user-agent", "gzip");

            yield return request.SendWebRequest();

            if (request.isHttpError || request.isNetworkError)
            {
                Debug.LogError(request.error);
                yield break;
            }

            var data = request.downloadHandler.data;

            var text = ""; 

            if (data[0] == 0x1f && data[1] == 0x8b)
            { // gzip

                text = System.Text.Encoding.UTF8.GetString(Zlib.GZipStream.UncompressBuffer(data));
            }
            else
            { // plane data
                text = request.downloadHandler.text;
            }
        }

    }
}