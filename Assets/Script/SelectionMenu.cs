using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionMenu : MonoBehaviour
{
    //make character buttons not removeable in inventory mode
    //keep arrows
    
    
    #region Singleton
    public static SelectionMenu instance;
    void Awake() { instance = this; }
    #endregion

    [System.Serializable]
    public class CharacterHolder
    {
        public string Name;
        public bool Active;
        public int NumInList;
        public GameObject Character;
    }
    public List<CharacterHolder> CharacterHolders;
    public List<Transform> Positions;
    public List<GameObject> Characters;

    public List<Image> Images;

    public Image Character;
    public Image Inventory;

    public Sprite Active;
    public Sprite InActive;

    public List<GameObject> CharacterAssets;
    public List<GameObject> InventoryAssets;

    public void ButtonRemovePirate(int Num)
    {
        CharacterHolders[Num].Active = false;
        Destroy(Characters[Num]);
        UpdatePirate();
    }
    public void SetCurrentPirate(int Listnum)
    {
        CharacterHolders[0].NumInList = Listnum;
        CharacterHolders[0].Character = AllInfo.instance.GamePirates[Listnum].pirateBase.Prefab;
        CharacterHolders[0].Active = true;
        UpdatePirate();
    }
    void Start()
    {
        InventoryActive();
        UpdatePirate();
    }
    public void UpdatePirate()
    {
        Selected.instance.HelpAll();
        for (int i = 0; i < CharacterHolders.Count; i++)
        {
            Destroy(Characters[i]);
            Characters[i] = null;
            if (CharacterHolders[i].Active == true)
            {
                int Num = CharacterHolders[i].NumInList;
                GameObject Spawned = Instantiate(AllInfo.instance.GamePirates[Num].pirateBase.Prefab, Positions[i].position, Positions[i].rotation);
                Destroy(Spawned.GetComponent<BattleAI>());
                Destroy(Spawned.GetComponent<Rigidbody>());
                Destroy(Spawned.GetComponent<BattleAI>());
                float Size = 9f;
                Spawned.transform.rotation = Quaternion.Euler(0,180,0);
                Spawned.transform.localScale = new Vector3(Size, Size, Size);
                if (Spawned.GetComponent<KnifeControl>() != null)
                {
                    Destroy(Spawned.GetComponent<KnifeControl>());
                }
                Characters[i] = Spawned;
            }
        }

        if (CharacterHolders[0].Active)
        {
            Selected.instance.SetPirateNum(CharacterHolders[0].NumInList);
        }
        else
        {
            Selected.instance.DisplayNoEquiptment();
        }
        
    }
    public void RightPirate()
    {
        CharacterHolder spare = CharacterHolders[0];
        for (int i = 0; i < CharacterHolders.Count; i++)
        {
            int NextNum = i + 1;
            if (i == CharacterHolders.Count - 1)
            {
                CharacterHolders[i] = spare;
            }
            else
            {
                CharacterHolders[i] = CharacterHolders[NextNum];
            }
        }
        UpdatePirate();
    }
    public void LeftPirate()
    {
        List<CharacterHolder> BeforeStats = new List<CharacterHolder>(CharacterHolders);
        for (int i = 0; i < CharacterHolders.Count; i++)
        {
            int NextNum = i - 1;
            if (i == 0)
            {
                CharacterHolders[i] = BeforeStats[BeforeStats.Count - 1];
            }
            else
            {
                CharacterHolders[i] = BeforeStats[NextNum];
            }
        }
        UpdatePirate();
    }
    public void CharacterActive()
    {
        Character.sprite = Active;
        Inventory.sprite = InActive;
        for (int i = 0; i < CharacterAssets.Count; i++)
        {
            CharacterAssets[i].SetActive(true);
        }
        for (int i = 0; i < InventoryAssets.Count; i++)
        {
            InventoryAssets[i].SetActive(false);
        }

        for (int i = 0; i < Characters.Count; i++)
        {
            //Images[i].gameObject.GetComponent<Button>().interactable = true;
        }
    }
    public void InventoryActive()
    {
        Character.sprite = InActive;
        Inventory.sprite = Active;
        for (int i = 0; i < CharacterAssets.Count; i++)
        {
            CharacterAssets[i].SetActive(false);
        }
        for (int i = 0; i < InventoryAssets.Count; i++)
        {
            InventoryAssets[i].SetActive(true);
        }
        /*
        for (int i = 0; i < Images.Count; i++)
        {
            Images[i].gameObject.GetComponent<Button>().interactable = false;
        }
        */
    }
    public void Back()
    {
        SceneLoader.instance.LoadScene("Main");
    }
    public void Next()
    {
        List<AllInfo.GamePirate> pirates = new List<AllInfo.GamePirate>();
        for (int i = 0; i < CharacterHolders.Count; i++)
        {
            if (CharacterHolders[i].Active == true)
            {
                int num = CharacterHolders[i].NumInList;
                pirates.Add(AllInfo.instance.GamePirates[num]);
            }
        }
        MapLevels.instance.SetActivePirates(pirates);
        SceneLoader.instance.LoadScene("Map");
    }
}
