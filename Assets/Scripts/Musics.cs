using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using UnityEngine;

public class Musics : MonoBehaviour
{
    public KeyBinds keyBinds;
    public List<char> defaultKey = new List<char> { 'A', 'S', 'D', 'J', 'K', 'L' };
    string chosenBeats;
    List<string> MusicBeats = new List<string> { "SSDADS####LLKJ###KJKJDS####KKJK###KKLJLK####DDSDJK###LKLKJD#######LLL##JKLKJK####DDSDJK##JKLKJD############SSDADS####LLKJ###KJKJDS####KKJK###KKLJLK####DDSDJK###LKLKJD#######LLL##JKLKJK####DDSDJK##JKLKJD############SSDADS####LLKJ###KJKJDS####KKJK###KKLJLK####DDSDJK###LKLKJD#######LLL##JKLKJK####DDSDJK##JKLKJD############", "SSSSS", "DDDDD" };
    //List<string> MusicBeats = new List<string> { "(5AS)DDJKDL(5AS)DDJKL(5AS)DDJKL", "SSSSS", "DDDDD" };
    
    public char[] Beats;

    public int MusicNumber = 0;
    int musicFlag;
    void Start()
    {
        musicFlag = MusicNumber;
        ProceedMusicBeat(MusicNumber);
    }

    // Update is called once per frame
    void Update()
    {
        if(MusicNumber != musicFlag)
        {
            musicFlag = MusicNumber;
            ProceedMusicBeat(musicFlag);
        }
    }

    public void ProceedMusicBeat(int musicNumber)
    {
        char[] beatKeys = MusicBeats[musicNumber].ToCharArray();
        for(int i = 0; i < beatKeys.Length; i++)
        {
            if (beatKeys[i] == 'A')
            {
                beatKeys[i] = keyBinds.Keys[0];
            }
            else if (beatKeys[i] == 'S')
            {
                beatKeys[i] = keyBinds.Keys[1];
            }
            else if (beatKeys[i] == 'D')
            {
                beatKeys[i] = keyBinds.Keys[2];
            }
            else if (beatKeys[i] == 'G')
            {
                beatKeys[i] = keyBinds.Keys[3];
            }
            else if (beatKeys[i] == 'J')
            {
                beatKeys[i] = keyBinds.Keys[4];
            }
            else if (beatKeys[i] == 'K')
            {
                beatKeys[i] = keyBinds.Keys[5];
            }
            else if (beatKeys[i] == 'L')
            {
                beatKeys[i] = keyBinds.Keys[6];
            }
            else{}

        }
        Beats = beatKeys;
        print(new string(Beats));
        
    }


}
