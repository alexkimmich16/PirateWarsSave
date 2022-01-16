using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJson;

public class UIWRMyCardSelection : MonoBehaviour
{
    public UIWRUserCardObject[] userBattleCards;

    public void NextButtonClicked()
    {
        UIRoomManager.share.NextToWaitingRoom();
        SoundManager.share.SetEffectSound(EFX_SOUND.EFXS_MOUSE_BATTLE_START);
    }

    public void InitializeBattleCards()
    {
        Debug.Log(": initialize called");
        //for(int i = 0; i < userBattleCards.Length; i ++)
        //{
        //    userBattleCards[i].InitCardInfo(Engine.share.mePlayer.battleCards[i]);
        //}

        for(int i = 0; i < userBattleCards.Length; i ++)
        {
            userBattleCards[i].InitCardInfo(Engine.share.mePlayer.selectedCards[i]);
        }
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
