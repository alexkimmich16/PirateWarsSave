using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TridentManager : MonoBehaviour
{
    public GameObject CharacterHolder;
    public GameObject Spawned;
    public Transform Spawn;


    public void AddCharacterSlot(int Num)
    {
        CharacterHolder.GetComponent<TextMeshProUGUI>().enabled = false;

        Spawned = Instantiate(AllInfo.instance.GamePirates[Num].pirateBase.Prefab, Spawn.position, Spawn.rotation);
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

        //changescene
    }

    public void UpgradeWithGems()
    {

    }

    public void ReRoll()
    {

    }



    public void AddCharacterButton()
    {
        SceneLoader.instance.LoadSceneWithoutFade("Upgrade");
    }
    void Start()
    {
        if(SceneLoader.instance.DisplayCharacter == true)
        {
            AddCharacterSlot(SceneLoader.instance.DisplayNum);
        }
    }

    public void Back()
    {
        SceneLoader.instance.LoadScene("Main");
    }
}
