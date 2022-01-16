using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;

public enum CARD_TYPE
{
    CT_WATER,
    CT_FIRE,
    CT_EARTH,
    CT_WIND
}

public class Card
{
    public int cardIdx;
    public string cardImgURL;
    public string cardBgURL;
    public string cardBgImgURL;
    public string cardCleanImgURL;

    public string cardName;
    public string kanjiName;
    public int rareCount;
    public int rareTier;
    public int power;
    public CARD_TYPE cardType;

    public bool isUsable;

    public Card(Card cData)
    {
        cardIdx = cData.cardIdx;
        cardImgURL = cData.cardImgURL;
        cardBgURL = cData.cardBgURL;
        cardBgImgURL = cData.cardBgImgURL;
        cardCleanImgURL = cData.cardCleanImgURL;

        cardName = cData.cardName;
        kanjiName = cData.kanjiName;
        rareCount = cData.rareCount;
        rareTier = cData.rareTier;
        power = cData.power;
        cardType = cData.cardType;
        isUsable = cData.isUsable;
    }
    
    public Card()
    {
        cardIdx = -1;
        cardImgURL = "";
        cardBgURL = "";
        cardBgImgURL = "";
        cardCleanImgURL = "";

        cardName = "";
        kanjiName = "";
        rareCount = 0;
        rareTier = 0;
        power = 0;
        cardType = CARD_TYPE.CT_FIRE;

        isUsable = true;
    }

    public Card(JsonObject jData, bool isServer)
    {
        cardIdx = -1;
        cardImgURL = "";
        cardBgURL = "";
        cardBgImgURL = "";
        cardCleanImgURL = "";

        cardName = "";
        kanjiName = "";
        rareCount = 0;
        rareTier = 0;
        power = 0;
        cardType = CARD_TYPE.CT_FIRE;

        if (jData.ContainsKey("id"))
        {
            cardIdx = Convert.ToInt32(jData["id"]);
        }

        if (jData.ContainsKey("image"))
        {
            cardImgURL = Convert.ToString(jData["image"]);
        }

        if(jData.ContainsKey("background_image"))
        {
            cardBgImgURL = Convert.ToString(jData["background_image"]);
        }

        if(jData.ContainsKey("image_clean"))
        {
            cardCleanImgURL = Convert.ToString(jData["image_clean"]);
        }

        if (jData.ContainsKey("background"))
        {
            cardBgURL = Convert.ToString(jData["background"]);
        }

        if (jData.ContainsKey("name"))
        {
            cardName = Convert.ToString(jData["name"]);
        }

        if (jData.ContainsKey("kanji_name"))
        {
            kanjiName = Convert.ToString(jData["kanji_name"]);
        }

        if (jData.ContainsKey("rare_count"))
        {
            rareCount = Convert.ToInt32(jData["rare_count"]);
        }

        if (jData.ContainsKey("rare_tier"))
        {
            rareTier = Convert.ToInt32(jData["rare_tier"]);
        }

        if (jData.ContainsKey("power"))
        {
            power = Convert.ToInt32(jData["power"]);
        }

        if (jData.ContainsKey("element"))
        {
            string strType = Convert.ToString(jData["element"]);

            switch (strType)
            {
                case "earth":
                    cardType = CARD_TYPE.CT_EARTH;
                    break;
                case "water":
                    cardType = CARD_TYPE.CT_WATER;
                    break;
                case "wind":
                    cardType = CARD_TYPE.CT_WIND;
                    break;
                case "fire":
                    cardType = CARD_TYPE.CT_FIRE;
                    break;
                default:
                    cardType = CARD_TYPE.CT_EARTH;
                    break;
            }
        }

        isUsable = true;
    }

    public Card(JsonObject jData)
    {
        cardIdx = -1;
        cardImgURL = "";
        cardBgURL = "";
        cardCleanImgURL = "";

        cardName = "";
        kanjiName = "";
        rareCount = 0;
        rareTier = 0;
        power = 0;
        cardType = CARD_TYPE.CT_FIRE;

        if(jData.ContainsKey("id"))
        {
            cardIdx = Convert.ToInt32(jData["id"]);
        }

        if(jData.ContainsKey("image"))
        {
            cardImgURL = Convert.ToString(jData["image"]);
        }

        if(jData.ContainsKey("image_clean"))
        {
            cardCleanImgURL = Convert.ToString(jData["image_clean"]);
        }

        if (jData.ContainsKey("background_image"))
        {
            cardBgImgURL = Convert.ToString(jData["background_image"]);
        }

        if (jData.ContainsKey("background"))
        {
            cardBgURL = Convert.ToString(jData["background"]);
        }

        if (jData.ContainsKey("image_properties"))
        {
            JsonObject jDetails = (JsonObject)jData["image_properties"];

            if (jDetails.ContainsKey("name"))
            {
                cardName = Convert.ToString(jDetails["name"]);
            }

            if (jDetails.ContainsKey("kanji_name"))
            {
                kanjiName = Convert.ToString(jDetails["kanji_name"]);
            }

            if (jDetails.ContainsKey("rare_count"))
            {
                rareCount = Convert.ToInt32(jDetails["rare_count"]);
            }

            if (jDetails.ContainsKey("rare_tier"))
            {
                rareTier = Convert.ToInt32(jDetails["rare_tier"]);
            }

            if (jDetails.ContainsKey("power"))
            {
                power = Convert.ToInt32(jDetails["power"]);
            }

            if (jDetails.ContainsKey("element"))
            {
                string strType = Convert.ToString(jDetails["element"]);

                switch(strType)
                {
                    case "earth":
                        cardType = CARD_TYPE.CT_EARTH;
                        break;
                    case "water":
                        cardType = CARD_TYPE.CT_WATER;
                        break;
                    case "wind":
                        cardType = CARD_TYPE.CT_WIND;
                        break;
                    case "fire":
                        cardType = CARD_TYPE.CT_FIRE;
                        break;
                    default:
                        cardType = CARD_TYPE.CT_EARTH;
                        break;
                }
            }
        }
        else
        {
            if(jData.ContainsKey("name"))
            {
                cardName = Convert.ToString(jData["name"]);
            }

            if (jData.ContainsKey("kanji_name"))
            {
                kanjiName = Convert.ToString(jData["kanji_name"]);
            }

            if (jData.ContainsKey("rare_count"))
            {
                rareCount = Convert.ToInt32(jData["rare_count"]);
            }

            if (jData.ContainsKey("rare_tier"))
            {
                rareTier = Convert.ToInt32(jData["rare_tier"]);
            }

            if (jData.ContainsKey("power"))
            {
                power = Convert.ToInt32(jData["power"]);
            }

            if (jData.ContainsKey("element"))
            {
                string strType = Convert.ToString(jData["element"]);

                switch (strType)
                {
                    case "earth":
                        cardType = CARD_TYPE.CT_EARTH;
                        break;
                    case "water":
                        cardType = CARD_TYPE.CT_WATER;
                        break;
                    case "wind":
                        cardType = CARD_TYPE.CT_WIND;
                        break;
                    case "fire":
                        cardType = CARD_TYPE.CT_FIRE;
                        break;
                    default:
                        cardType = CARD_TYPE.CT_EARTH;
                        break;
                }
            }
        }

        isUsable = true;
    }

    public JsonObject GetCardJson()
    {
        JsonObject jData = new JsonObject();
        jData.Add("id", cardIdx);
        jData.Add("image", cardImgURL);
        jData.Add("background", cardBgURL);
        jData.Add("background_image", cardBgImgURL);
        jData.Add("image_clean", cardCleanImgURL);
        jData.Add("name", cardName);
        jData.Add("kanji_name", kanjiName);
        jData.Add("rare_count", rareCount);
        jData.Add("rare_tier", rareTier);
        jData.Add("power", power);
        jData.Add("element", GetCardTypeString());

        return jData;
    }

    public string GetCardTypeString()
    {
        string cType = "";

        switch(cardType)
        {
            case CARD_TYPE.CT_WATER:
                cType = "water";
                break;
            case CARD_TYPE.CT_WIND:
                cType = "wind";
                break;
            case CARD_TYPE.CT_FIRE:
                cType = "fire";
                break;
            case CARD_TYPE.CT_EARTH:
                cType = "earth";
                break;
            default:
                cType = "earth";
                break;
        }

        return cType;
    }
}
