using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class dalogue1 : MonoBehaviour
{
    int level;
    public List<GameObject> enemies;
    public List<GameObject> characters;

    private List<float> charstopat = new List<float> { -285f, 300f};
    public List<GameObject> dialogueBoxs;
    public TextMeshProUGUI dialogText;
    public TextMeshProUGUI EnemyName;

    int DialogPart;

    Dictionary<int, Dictionary<int,List<string>>> dialogues = new Dictionary <int, Dictionary<int, List<string>>>()
    {
        {0,  new Dictionary<int, List<string>>()
            {
                {
                    0, new List<string>()
                    {
                        "Ah.. there you go...",
                        "Hihihi, trying to be a hero huh?",
                        "Didn't mean it like that... but it looks like someone has to leave here soon or...",
                        "Huh? Or what? ah, you're trying to play games with me...",
                        "**Cihh, let see what's going to happen now...",
                        "Look who's going to leave here...",
                        "Now!! Let's start the show....",
                    }
                },
                {
                    1, new List<string>(){
                        "Kuhh..!!",
                        "You’re quite strong young man.",
                        "Huff.. Huff",
                        "You too Pocong",
                        "Well said… but I must warn you ab–",
                        "Ugh! Uhuk!",
                        "Pocong!",
                        "I must warn you about the danger ahe–"
                    }
                }
            }
        },
        {1,  new Dictionary<int, List<string>>()
            {
                {
                    0, new List<string>()
                    {
                        "Freeze!",
                        "State your name and reason here!",
                        "MC sir, and… Wait…",
                        "Ha! Gotcha!",
                        "Wait… you are…",
                        "Tuyul!",
                        "Haha,that’s me.",
                        "Now, if you can excuse me for a moment",
                        "Not in my watch!"
                    }
                },
                {
                    1, new List<string>(){
                        "Now I catch you!",
                        "Uncle, what are you going to do uncle?",
                        "Enough of your game!",
                        "You will answer my question from now on!",
                        "Tuyul:”Well, that was fun MC",
                        "Tuyul:”But I suggest you move quickly, before it’s too late",
                        "What do you mean? too late for what?",
                        "Well, thank you for playing with me for the last time MC"
                    }
                }
            }
        },
        {2,  new Dictionary<int, List<string>>()
            {
                {
                    0, new List<string>()
                    {
                        "Hi hi hi, what a cute man you are~",
                        "What? Who’s there?",
                        "Show yourself",
                        "Hi hi hi, since you are cute, I will show myself to you~",
                        "Try not to pass out, Darling~",
                        "How is my appearance Darling?",
                        "Ekhem! Can I get your number please, I mean your number, I mean your number",
                        "Well of course Darling~",
                        "But first, can you do me a favor please?",
                        "Of course m’lady, anything for you!",
                        "Hi hi hi, I knew you would say that",
                        "So… I want you to turn around and walked away from this village and pretend that nothing happened",
                        "If you do that I’ll give you my number as well as my protection from your boss",
                        "That sound like a really nice deal!",
                        "Riight~",
                        "I can get out of this place, got your protection",
                        "And most importantly I got your number!",
                        "Soo…",
                        "But I REFUSE!",
                        "W-wha…",
                        "One thing that I like is saying NO to the person who think he is stronger than me",
                        "...",
                        "I understand.",
                        "And that leave us with one option left",
                        "COME!"
                    }
                },
                {
                    1, new List<string>(){
                        "For a cute man like you you are quite strong",
                        "Well the same goes for you Kunti",
                        "Hi hi hi, you making me blush~",
                        "And as for your reward for coming this far I’ll tell you my number~",
                        "Really? wait, no!",
                        "Kunti, you start to…",
                        "Disappear…",
                        "It’s fine darling~",
                        "Anyway, are you ready to hear my number?",
                        "It’s zero eight eight…",
                        "Zero eight eight, and then",
                        "And the rest is someday~"
                    }
                }
            }
        },
        {3,  new Dictionary<int, List<string>>()
            {
                {
                    0, new List<string>()
                    {
                        "This place look very scary.",
                        "Is this even a part of the village?",
                        "Who’s there?! Show yourself!",
                        "Heh heh heh… A little human who dare to enter my territory, who are you?",
                        "My name is MC. And I come here to  cleanse the curse from the village.",
                        "Hoo… a little human want to lift a curse?",
                        "Come back in 100 years and maybe you can start to try to lift the curse",
                        "Ha ha ha, you have a good sense of humor Genderuwo!",
                        "But I won’t back down either",
                        "So , prepare yourself Genderuwo!",
                        "Ha ha ha! then show me your capabilities!"
                    }
                },
                {
                    1, new List<string>(){
                        "Ha ha ha! You are much stronger than I expected little human!",
                        "What?!?",
                        "My attacks are ineffective?",
                        "Hahaha! What’s wrong little human, can’t go on longer?",
                        "kuhh!",
                        "Now… its the time you admit your defeat little human",
                        "Huh?",
                        "..."
                    }
                }
            }
        },
        {4,  new Dictionary<int, List<string>>()
            {
                {
                    0, new List<string>()
                    {
                        "Hey… You awake…",
                        "*Ughh. Where am I?",
                        "Relax, young man.",
                        "We are in Security POS",
                        "You are safe here",
                        "Thank you for your help Grandpa",
                        "Ha ha ha, don’t mention it.",
                        "Anyway, how is your wound?",
                        "Its better now",
                        "Speaking of which, how did you safe me?",
                        "Oh that…",
                        "I was patrolling when I hear a loud noises nearby",
                        "I rushed to that place",
                        "And when I got there, I found you laying in the ground.",
                        "Oh, and I was about to ask, what are you doing here in this village?",
                        "Do you know this village is already abandoned?",
                        "Umm… it’s a bit complicated to explain",
                        "So that’s why I got here",
                        "Wait, you said that this village is abandoned?",
                        "Then why you also here?",
                        "Ha ha ha! So you realized that!",
                        "As a gift I will tell you about myself",
                        "I am Dukun the one who curse this village",
                        "!!!",
                        "So you are the culprit for why this village is abandoned!",
                        "Ha ha ha! Well then, are you scared?",
                        "Scared? ",
                        "I’ve been through many things to come here",
                        "There is nothing to be afraid anymore",
                        "Lift this curse in an instant! or else…",
                        "Or else What!",
                        "You are the one who came here and defeat my summons!",
                        "Do you know how important they are?!",
                        "!!!",
                        "Enough of this nonsense!",
                        "I will stop you and make you restart again and again MC!",
                        "Bring it ON!"
                    }
                },
                {
                    1, new List<string>(){
                        "Finally it’s all over"
                    }
                }
            }
        },
    };
    int dialognum = 0;
    Dictionary<int, Dictionary<int, List<int>>> dialogRoutes = new Dictionary<int, Dictionary<int, List<int>>>()
    {
        {0,  new Dictionary<int, List<int>>()
            {
                {
                    0, new List<int>()
                    {
                        0,1,0,1,0,1,1
                    }
                },
                {
                    1, new List<int>(){
                        1,1,0,0,1,1,0,1
                    }
                }
            }
        },
        {1,  new Dictionary<int, List<int>>()
            {
                {
                    0, new List<int>()
                    {
                        0,1,0,1,0,0,1,1,0
                    }
                },
                {
                    1, new List<int>(){
                        0,1,0,0,1,1,0,1
                    }
                }
            }
        },
        {2,  new Dictionary<int, List<int>>()
            {
                {
                    0, new List<int>()
                    {
                        1,0,0,1,1,1,0,1,1,0,1,1,1,0,1,0,0,1,0,1,0,1,1,1,0
                    }
                },
                {
                    1, new List<int>(){
                        1,0,1,1,0,0,0,1,1,1,0,1
                    }
                }
            }
        },
        {3,  new Dictionary<int, List<int>>()
            {
                {
                    0, new List<int>()
                    {
                        0,0,0,1,0,1,1,0,0,0,1
                    }
                },
                {
                    1, new List<int>(){
                        1,0,0,1,0,1,0,1
                    }
                }
            }
        },
        {4,  new Dictionary<int, List<int>>()
            {
                {
                    0, new List<int>()
                    {
                        1,0,1,1,1,0,1,1,0,0,1,1,1,1,1,1,0,0,0,0,1,1,1,0,0,1,0,0,0,0,1,1,1,0,1,1,0
                    }
                },
                {
                    1, new List<int>(){
                        0
                    }
                }
            }
        },
    };


    public CanvasGroup screenFade;
    void Start()
    {
        level = PlayerPrefs.GetInt($"LevelGame{PlayerPrefs.GetString("PlayingAs")}");
        DialogPart = PlayerPrefs.GetInt($"DialogPart{PlayerPrefs.GetString("PlayingAs")}");
        foreach(GameObject enm in enemies) {
            enm.SetActive(false);
        }
        characters.Add(enemies.ElementAt(level));
        characters.Last().SetActive(true);

        EnemyName.text = enemies.ElementAt(level).name;
        StartCoroutine(startFade());
    }

    IEnumerator playerSetMove()
    {
        if(DialogPart == 0)
        {
            float elapsedTime = 0f;
            Vector3 playerstartpos = characters[0].transform.localPosition;
            Vector3 enemystartpos = characters[1].transform.localPosition;

            while (elapsedTime < 3.0f)
            {
                characters[0].transform.localPosition = Vector3.Lerp(playerstartpos, new Vector3(charstopat[0], playerstartpos.y, playerstartpos.z), elapsedTime / 3.0f);
                characters[1].transform.localPosition = Vector3.Lerp(enemystartpos, new Vector3(charstopat[1], enemystartpos.y, enemystartpos.z), elapsedTime / 3.0f);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            characters[0].transform.localPosition = new Vector3(charstopat[0], playerstartpos.y, playerstartpos.z);
            dialogueBoxs[dialogRoutes[level][DialogPart][dialognum]].gameObject.SetActive(true);
            dialogText.text = dialogues[level][DialogPart][dialognum];
        }

        if(DialogPart == 1)
        {
            characters[0].transform.localPosition -= new Vector3(charstopat[0], 0f, 0f);
            characters[1].transform.localPosition -= new Vector3(charstopat[1], 0f, 0f);
            dialogueBoxs[dialogRoutes[level][DialogPart][dialognum]].gameObject.SetActive(true);
            dialogText.text = dialogues[level][DialogPart][dialognum];
        }
    }

    public void nextDialog()
    {
        if(dialognum < dialogues[level][DialogPart].Count)
        {
            dialognum++;
        }
        if(dialognum == dialogues[level][DialogPart].Count)
        {
            if(DialogPart == 0)
            {
                StartCoroutine(goToScene("GamePlayScene"));
            }
            if(DialogPart == 1)
            {
                if (level == 4)
                {
                    PlayerPrefs.SetString("GameFinished", PlayerPrefs.GetString("PlayingAs"));
                    StartCoroutine(goToScene("MainMenu"));
                }
                else
                {
                    StartCoroutine(goToScene("OpeningScene"));
                }
            }
        }
        changeDialog(dialognum);
    }
    void changeDialog(int num)
    {
        
        if(dialogRoutes[level][DialogPart][dialognum] == 0)
        {
            dialogueBoxs[0].gameObject.SetActive(true);
            dialogueBoxs[1].gameObject.SetActive(false);
        }
        
        if(dialogRoutes[level][DialogPart][dialognum] == 1)
        {
            dialogueBoxs[1].gameObject.SetActive(true);
            dialogueBoxs[0].gameObject.SetActive(false);
        }

        dialogText.text = dialogues[level][DialogPart][dialognum];
    }
    IEnumerator goToScene(string scene)
    {
        yield return new WaitForSeconds(1f); // Menunggu selama 2 detik
        float elapsedTime = 0f;
        float startAlpha = screenFade.alpha; // Alpha awal
        float targetAlpha = 1f; // Alpha tujuan

        while (elapsedTime < 2f) // Durasi perubahan alpha
        {
            // Menggunakan fungsi Lerp untuk mengubah alpha secara perlahan
            screenFade.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / 2f);
            elapsedTime += Time.deltaTime; // Menambah waktu yang sudah berlalu
            yield return null; // Menunggu frame berikutnya
        }

        // Pastikan alpha mencapai targetAlpha
        screenFade.alpha = targetAlpha;
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(scene);
    }

    IEnumerator startFade()
    {
        
        float elapsedTime = 0f;
        float startAlpha = screenFade.alpha; // Alpha awal
        float targetAlpha = 0f; // Alpha tujuan

        while (elapsedTime < 2f) // Durasi perubahan alpha
        {
            // Menggunakan fungsi Lerp untuk mengubah alpha secara perlahan
            screenFade.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / 2f);
            elapsedTime += Time.deltaTime; // Menambah waktu yang sudah berlalu
            yield return null; // Menunggu frame berikutnya
        }

        // Pastikan alpha mencapai targetAlpha
        screenFade.alpha = targetAlpha;
        StartCoroutine(playerSetMove());
    }

}
