using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public enum BG_SOUND
{
    BGS_MAIN,
    BGS_BATTLE,
}

public enum EFX_SOUND
{
    EFXS_START_MENU_OPENING,
    EFXS_BATTLE,
    EFXS_BATTLE_CRITICAL,
    EFXS_CARD_PICK,
    EFXS_DECK_APPEAR,
    EFXS_BATLLE_LOSE,
    EFXS_BATTLE_WIN,
    EFXS_BATTLE_OPENING,
    EFXS_BATTLE_ROUND_LOSE,
    EFXS_BATTLE_ROUND_WIN,
    EFXS_MOUSE_CLICK,
    EFXS_MOUSE_BATTLE_START,
    EFXS_MOUSE_HOVER
}

public class SoundManager : MonoBehaviour
{
    static public SoundManager share;

    public AudioClip[] bgSounds;
    public AudioClip fx_startMenuOpening;
    public AudioClip[] fx_battle;
    public AudioClip fx_battle_critical;
    public AudioClip[] fx_card_pick;
    public AudioClip battle_deck_appear;
    public AudioClip battle_win;
    public AudioClip battle_lose;
    public AudioClip battle_opening;
    public AudioClip[] battle_round_lose;
    public AudioClip[] battle_round_win;
    public AudioClip[] mouse_click;
    public AudioClip[] mouse_hover;
    public AudioClip battle_start_click;

    public AudioSource sourceBG;
    public AudioSource sourceEffect;

    private void Awake()
    {
        if(share == null)
        {
            share = this;
        }

        SetBackGroundSound(BG_SOUND.BGS_MAIN);
    }

    public void SetBackGroundSound(BG_SOUND bgSound)
    {
        switch(bgSound)
        {
            case BG_SOUND.BGS_MAIN:
                sourceBG.clip = bgSounds[(int)BG_SOUND.BGS_MAIN];
                break;
            case BG_SOUND.BGS_BATTLE:
                sourceBG.clip = bgSounds[(int)BG_SOUND.BGS_BATTLE];
                break;
            default:
                break;
        }

        sourceBG.Play();
    }


    public AudioClip GetSoundEffect(EFX_SOUND efxSound)
    {
        int rdNumber = 0;
        AudioClip aClip = null;
        switch (efxSound)
        {
            case EFX_SOUND.EFXS_START_MENU_OPENING:
                aClip = fx_startMenuOpening;
                break;
            case EFX_SOUND.EFXS_BATTLE:
                rdNumber = UnityEngine.Random.RandomRange(0, fx_battle.Length);
                aClip = fx_battle[rdNumber];
                break;
            case EFX_SOUND.EFXS_BATTLE_CRITICAL:
                aClip = fx_battle_critical;
                break;
            case EFX_SOUND.EFXS_CARD_PICK:
                rdNumber = UnityEngine.Random.RandomRange(0, fx_card_pick.Length);
                aClip = fx_card_pick[rdNumber];
                break;
            case EFX_SOUND.EFXS_DECK_APPEAR:
                aClip = battle_deck_appear;
                break;
            case EFX_SOUND.EFXS_BATLLE_LOSE:
                aClip = battle_lose;
                break;
            case EFX_SOUND.EFXS_BATTLE_WIN:
                aClip = battle_win;
                break;
            case EFX_SOUND.EFXS_BATTLE_OPENING:
                aClip = battle_opening;
                break;
            case EFX_SOUND.EFXS_BATTLE_ROUND_LOSE:
                rdNumber = UnityEngine.Random.RandomRange(0, battle_round_lose.Length);
                aClip = battle_round_lose[rdNumber];
                break;
            case EFX_SOUND.EFXS_BATTLE_ROUND_WIN:
                rdNumber = UnityEngine.Random.RandomRange(0, battle_round_win.Length);
                aClip = battle_round_win[rdNumber];
                break;
            case EFX_SOUND.EFXS_MOUSE_CLICK:
                rdNumber = UnityEngine.Random.RandomRange(0, mouse_click.Length);
                aClip = mouse_click[rdNumber];
                break;
            case EFX_SOUND.EFXS_MOUSE_BATTLE_START:
                aClip = battle_start_click;
                break;
            case EFX_SOUND.EFXS_MOUSE_HOVER:
                rdNumber = UnityEngine.Random.RandomRange(0, mouse_hover.Length);
                aClip = mouse_hover[rdNumber];
                break;
        }

        return aClip;
    }

    public void SetEffectSound(EFX_SOUND efxSound)
    {
        int rdNumber = 0;
        switch (efxSound)
        {
            case EFX_SOUND.EFXS_START_MENU_OPENING:
                sourceEffect.clip = fx_startMenuOpening;
                break;
            case EFX_SOUND.EFXS_BATTLE:
                rdNumber = UnityEngine.Random.RandomRange(0, fx_battle.Length);
                sourceEffect.clip = fx_battle[rdNumber];
                break;
            case EFX_SOUND.EFXS_BATTLE_CRITICAL:
                sourceEffect.clip = fx_battle_critical;
                break;
            case EFX_SOUND.EFXS_CARD_PICK:
                rdNumber = UnityEngine.Random.RandomRange(0, fx_card_pick.Length);
                sourceEffect.clip = fx_card_pick[rdNumber];
                break;
            case EFX_SOUND.EFXS_DECK_APPEAR:
                sourceEffect.clip = battle_deck_appear;
                break;
            case EFX_SOUND.EFXS_BATLLE_LOSE:
                sourceEffect.clip = battle_lose;
                break;
            case EFX_SOUND.EFXS_BATTLE_WIN:
                sourceEffect.clip = battle_win;
                break;
            case EFX_SOUND.EFXS_BATTLE_OPENING:
                sourceEffect.clip = battle_opening;
                break;
            case EFX_SOUND.EFXS_BATTLE_ROUND_LOSE:
                rdNumber = UnityEngine.Random.RandomRange(0, battle_round_lose.Length);
                sourceEffect.clip = battle_round_lose[rdNumber];
                break;
            case EFX_SOUND.EFXS_BATTLE_ROUND_WIN:
                rdNumber = UnityEngine.Random.RandomRange(0, battle_round_win.Length);
                sourceEffect.clip = battle_round_win[rdNumber];
                break;
            case EFX_SOUND.EFXS_MOUSE_CLICK:
                rdNumber = UnityEngine.Random.RandomRange(0, mouse_click.Length);
                sourceEffect.clip = mouse_click[rdNumber];
                break;
            case EFX_SOUND.EFXS_MOUSE_BATTLE_START:
                sourceEffect.clip = battle_start_click;
                break;
            case EFX_SOUND.EFXS_MOUSE_HOVER:
                rdNumber = UnityEngine.Random.RandomRange(0, mouse_hover.Length);
                sourceEffect.clip = mouse_hover[rdNumber];
                break;
        }

        sourceEffect.Play();
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
