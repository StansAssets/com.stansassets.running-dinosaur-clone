using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class SG_GiphyUploader
{
    static readonly string k_UploadUrl = "https://upload.giphy.com/v1/gifs";
    readonly string m_APIKey;

    public SG_GiphyUploader(string apiKey)
    {
        m_APIKey = apiKey;
    }

    public void Upload(string filPath, Action<SG_GiphyUploadResult> callback)
    {
        var imageData = File.ReadAllBytes(filPath);
        var data = new List<IMultipartFormSection>
        {
            new MultipartFormDataSection("api_key", m_APIKey),
            new MultipartFormFileSection("file", imageData, "test.png", "image/png")
        };

        //init handshake
        var handshake = UnityWebRequest.Post(k_UploadUrl, data);
        var request = handshake.SendWebRequest();

        SG_GiphyUploadResult result;
        request.completed += (action) =>
        {
#if UNITY_2020_1_OR_NEWER
            if (handshake.result == UnityWebRequest.Result.Success)
#else
            if (!handshake.isHttpError && !handshake.isNetworkError)
#endif
            {
                result = JsonUtility.FromJson<SG_GiphyUploadResult>(handshake.downloadHandler.text);
            }
            else
            {
                result = new SG_GiphyUploadResult();
                result.meta.msg = handshake.error;
                result.meta.status = 0;
            }

            callback.Invoke(result);
        };
    }
}
