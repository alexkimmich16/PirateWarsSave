using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using TMPro;

public class CharacterRevealControl : MonoBehaviour
{
    public AllInfo.GamePirate pirate;
    
    public List<SliderInfo> sliderInfos;
    [System.Serializable]
    public class SliderInfo
    {
        public string Name;
        public int MaxValue;
        public Slider slider;
    }

    public Transform Spawn;

    public Image iconDisplay;
    private void Start()
    {
        int Num = SceneLoader.instance.TypeNumInList;
        if (SceneLoader.instance.IsPirate == true)
        {
            UpdateSliders(pirate);
            GameObject Spawned = Instantiate(AllInfo.instance.GamePirates[Num].pirateBase.Prefab, Spawn.position, Spawn.rotation);
            Destroy(Spawned.GetComponent<BattleAI>());
            Destroy(Spawned.GetComponent<Rigidbody>());
            Destroy(Spawned.GetComponent<BattleAI>());
            float Size = 9f;
            Spawned.transform.rotation = Quaternion.Euler(0, 156f, 0);
            Spawned.transform.localScale = new Vector3(Size, Size, Size);
            if (Spawned.GetComponent<KnifeControl>() != null)
            {
                Destroy(Spawned.GetComponent<KnifeControl>());
            }
        }
        else if (SceneLoader.instance.IsPirate == false)
        {
            iconDisplay.enabled = true;
            iconDisplay.sprite = AllInfo.instance.GameEquipments[Num].equipmentInfo.icon;
        }
        
        
    }
    public void UpdateSliders(AllInfo.GamePirate pirateInfo)
    {
        //calculate min/max
        
        sliderInfos[0].slider.value = GetSliderValue(sliderInfos[0].MaxValue, pirateInfo.Health);
        sliderInfos[1].slider.value = GetSliderValue(sliderInfos[1].MaxValue, pirateInfo.Damage);
        sliderInfos[2].slider.value = GetSliderValue(sliderInfos[2].MaxValue, pirateInfo.Armour);
        sliderInfos[3].slider.value = GetSliderValue(sliderInfos[3].MaxValue, pirateInfo.CritPercent);
        sliderInfos[4].slider.value = GetSliderValue(sliderInfos[4].MaxValue, pirateInfo.CritDamage);
        sliderInfos[5].slider.value = GetSliderValue(sliderInfos[5].MaxValue, pirateInfo.Intellect);
        sliderInfos[6].slider.value = GetSliderValue(sliderInfos[6].MaxValue, pirateInfo.Dexterity);
    }

    public void BackButton()
    {
        SceneLoader.instance.LoadScene("Main");
    }
    public float GetSliderValue(int MaxValue, int MyValue)
    {
        return MyValue / MaxValue;
    }
}
