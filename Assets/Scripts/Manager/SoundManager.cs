using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //tuto sound manager pour faire propre avec enum
    public AudioSource music;
    public static SoundManager Instance { get; private set; }
    void Awake()
    {
        Instance = this;
    }
    void Update()
    {
        
    }
}
