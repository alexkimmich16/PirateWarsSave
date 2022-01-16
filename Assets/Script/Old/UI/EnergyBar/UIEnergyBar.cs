using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using SimpleJson;
using UnityEngine.Networking;

public class UIEnergyBar : MonoBehaviour
{
    static public UIEnergyBar share;

    public Text energyText;
    public int curEnergyVal;
    public int maxEnergyVal;

    public Text utcEarning;
    public Text totalEarning;

    private void Awake()
    {
        if(share == null)
        {
            share = this;
        }
    }

    public void InitializeEnergy()
    {
        StartCoroutine(LoadEnergyVal());
        StartCoroutine(IEGetEarning());
    }

    public void OnEnable()
    {
        Debug.Log("enable energy");
        if(Engine.share != null && Engine.share.mePlayer != null)
        {
            InitializeEnergy();
        }
    }

    public IEnumerator IEGetEarning()
    {
        string earningURL = GameManager.share.metaServerURL + "earnings/" + Engine.share.mePlayer.address;
        //Debug.Log("earning url ---" + earningURL);
        UnityWebRequest uri = UnityWebRequest.Get(earningURL);

        yield return uri.SendWebRequest();
        if (uri.isNetworkError)
        {

        }
        else
        {
            string downloadData = uri.downloadHandler.text;
            JsonObject jData = (JsonObject)SimpleJson.SimpleJson.DeserializeObject(downloadData);
            //Debug.Log("user earning data ----" + downloadData);

            if(jData.ContainsKey("today_earnings"))
            {
                utcEarning.text = Convert.ToString(jData["today_earnings"]);
            }

            if(jData.ContainsKey("total_earnings"))
            {
                totalEarning.text = Convert.ToString(jData["total_earnings"]);
            }
        }

        yield return new WaitForSeconds(2f);
        StartCoroutine(IEGetEarning());
    }

    public IEnumerator LoadEnergyVal()
    {
        string addUrl = GameManager.share.metaServerURL + "energy/" + Engine.share.mePlayer.address;
        //Debug.Log("add url ----" + addUrl);
        UnityWebRequest uri = UnityWebRequest.Get(addUrl);

        yield return uri.SendWebRequest();
        if (uri.isNetworkError)
        {

        }
        else
        {
            string downloadData = uri.downloadHandler.text;
            JsonObject jData = (JsonObject)SimpleJson.SimpleJson.DeserializeObject(downloadData);

            //Debug.Log("user energy data --- " + downloadData);

            if(jData.ContainsKey("current_energy"))
            {
                curEnergyVal = Convert.ToInt32(jData["current_energy"]);
            }

            if(jData.ContainsKey("max_energy"))
            {
                maxEnergyVal = Convert.ToInt32(jData["max_energy"]);
            }

            energyText.text = curEnergyVal.ToString() + "/" + maxEnergyVal.ToString();
        }

        yield return new WaitForSeconds(2f);
        StartCoroutine(LoadEnergyVal());
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
