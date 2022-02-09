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
        public Slider sliderAdd;
    }

    public Transform Spawn;

    public Image iconDisplay;
    private void Start()
    {
        int Num = SceneLoader.instance.TypeNumInList;
        if (SceneLoader.instance.IsPirate == true)
        {
            UpdateSliders(SceneLoader.instance.Before, SceneLoader.instance.Added);
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
    public void UpdateSliders(AllInfo.StatMultiplier Before, AllInfo.StatMultiplier Add)
    {
        //calculate min/max
        
        sliderInfos[0].slider.value = GetSliderValue(sliderInfos[0].MaxValue, Before.Health);
        sliderInfos[1].slider.value = GetSliderValue(sliderInfos[1].MaxValue, Before.Damage);
        sliderInfos[2].slider.value = GetSliderValue(sliderInfos[2].MaxValue, Before.Armour);
        sliderInfos[3].slider.value = GetSliderValue(sliderInfos[3].MaxValue, Before.CritPercent);
        sliderInfos[4].slider.value = GetSliderValue(sliderInfos[4].MaxValue, Before.CritDamage);
        sliderInfos[5].slider.value = GetSliderValue(sliderInfos[5].MaxValue, Before.Intellect);
        sliderInfos[6].slider.value = GetSliderValue(sliderInfos[6].MaxValue, Before.Dexterity);

        sliderInfos[0].sliderAdd.value = GetSliderValue(sliderInfos[0].MaxValue, Before.Health) + GetSliderValue(sliderInfos[0].MaxValue, Add.Health);
        sliderInfos[1].sliderAdd.value = GetSliderValue(sliderInfos[1].MaxValue, Before.Damage) + GetSliderValue(sliderInfos[1].MaxValue, Add.Damage);
        sliderInfos[2].sliderAdd.value = GetSliderValue(sliderInfos[2].MaxValue, Before.Armour) + GetSliderValue(sliderInfos[2].MaxValue, Add.Armour);
        sliderInfos[3].sliderAdd.value = GetSliderValue(sliderInfos[3].MaxValue, Before.CritPercent) + GetSliderValue(sliderInfos[3].MaxValue, Add.CritPercent);
        sliderInfos[4].sliderAdd.value = GetSliderValue(sliderInfos[4].MaxValue, Before.CritDamage) + GetSliderValue(sliderInfos[4].MaxValue, Add.CritDamage);
        sliderInfos[5].sliderAdd.value = GetSliderValue(sliderInfos[5].MaxValue, Before.Intellect) + GetSliderValue(sliderInfos[5].MaxValue, Add.Intellect);
        sliderInfos[6].sliderAdd.value = GetSliderValue(sliderInfos[6].MaxValue, Before.Dexterity) + GetSliderValue(sliderInfos[6].MaxValue, Add.Dexterity);
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
