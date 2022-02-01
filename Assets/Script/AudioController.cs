using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public enum TypeOfAudio
{
    
    Music = 0,
    Effect = 1,
}
public class AudioController : MonoBehaviour
{
    #region Singleton + classes
    public static AudioController instance;
    void Awake()
    {
        instance = this;
    }
    [System.Serializable]
    public class Audio
    {
        public string Name;
        public AudioClip Sound;
        public TypeOfAudio type;

        [Range(0.0f, 1f)]
        public float Volume = 1;
    }
    [System.Serializable]
    public class AudioType
    {
        public string Name;
        public AudioMixerGroup audioMixer;
        public List<Audio> AudioClips = new List<Audio>();
    }
    #endregion
    public bool MakeSound;
    public List<AudioType> AudioTypes;
    
    private void Start()
    {
        SpawnAudio(FindAudio("Anchored", TypeOfAudio.Music));
    }

    public Audio FindAudio(string Name, TypeOfAudio type)
    {
        int TypeNum = (int)type;
        for (int i = 0; i < AudioTypes[TypeNum].AudioClips.Count; i++)
        {
            //Debug.Log("PT3");
            if (Name == AudioTypes[TypeNum].AudioClips[i].Name)
            {
                return AudioTypes[TypeNum].AudioClips[i];
            }
        }
        Debug.LogError("could not find audio of name:" + Name + "  and type: " + type.ToString());
        return null;
        
    }
    public void SpawnAudio(Audio AudioInfo)
    {
        
        GameObject Sound = new GameObject();
        Sound.AddComponent<AudioSource>();
        Sound.GetComponent<AudioSource>().outputAudioMixerGroup = AudioTypes[(int)AudioInfo.type].audioMixer;
        Sound.GetComponent<AudioSource>().clip = AudioInfo.Sound;
        if (MakeSound == true)
        {
            Sound.GetComponent<AudioSource>().volume = AudioInfo.Volume;
        }
        else
        {
            Debug.LogError("Remember! MakeSound is disabled in the audiocontroller script right now!");
            Sound.GetComponent<AudioSource>().volume = 0;
        }
        
        Sound.GetComponent<AudioSource>().Play();
    }
}
