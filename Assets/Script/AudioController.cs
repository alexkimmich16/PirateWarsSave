using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TypeOfAudio
{
    Effect = 0,
    Music = 1,
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
        public List<Audio> AudioClips = new List<Audio>();

        [Range(0.0f, 1f)]
        public float Volume = 1;
    }
    #endregion
    public List<AudioType> AudioTypes;
    

    public void FindAudio(string Name, TypeOfAudio type)
    {
        int TypeNum = (int)type;
        for (int i = 0; i < AudioTypes[TypeNum].AudioClips.Count; i++)
        {
            //Debug.Log("PT3");
            if (Name == AudioTypes[TypeNum].AudioClips[i].Name)
            {
                SpawnAudio(AudioTypes[TypeNum].AudioClips[i]);
                return;
            }
        }
        Debug.LogError("could not find audio of name:" + Name + "  and type: " + type.ToString());

    }
    public void SpawnAudio(Audio AudioInfo)
    {
        GameObject Sound = new GameObject();
        Sound.AddComponent<AudioSource>();
        Sound.GetComponent<AudioSource>().clip = AudioInfo.Sound;
        Sound.GetComponent<AudioSource>().volume = AudioInfo.Volume;
        Sound.GetComponent<AudioSource>().Play();
    }
}
