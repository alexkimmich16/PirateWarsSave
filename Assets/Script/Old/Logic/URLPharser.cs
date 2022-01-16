using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJson;

public class URLPharser : MonoBehaviour
{
    static public URLPharser share;

    private void Awake()
    {
        if(share == null)
        {
            share = this;
        }
    }

    public JsonObject GetURLParams()
    {
        JsonObject jobj = new JsonObject();

        string urlString = "https://api.risingsun.finance/session?address=0x884F86Bdb9766D9B30895bc9CF02A918BF267dda&sessionId=tempallow4";
        //string urlString = "https://api.risingsun.finance/session?address=0x7fbade4e8b9f0999407c3a50b11ed142b036b076&sessionId=randomsessionid";

#if UNITY_WEBGL && !UNITY_EDITOR
        urlString = Application.absoluteURL;
#endif

        Debug.Log("url string ---" + urlString);

        int idx = -1;
        idx = urlString.IndexOf("?");

        if(idx < 0)
        {
            return null;
        }
        else
        {
            string paramStr = urlString.Substring(idx + 1);
            paramStr.Replace("?", "");
            string[] paramArray = paramStr.Split('&');
            for (int i = 0; i < paramArray.Length; i++)
            {
                string ssParam = paramArray[i].Replace("&", "");
                ssParam.Replace(" ", "");

                string[] data = ssParam.Split('=');

                if (data.Length == 2)
                {
                    jobj.Add(data[0], data[1]);
                }
            }
        }

        return jobj;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
