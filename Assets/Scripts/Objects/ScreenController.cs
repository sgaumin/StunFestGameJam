using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ScreenController : MonoBehaviour
{
    const int nBody = 1;
    const int nEars = 3;
    const int nHead = 5;
    const int nNose = 7;
    const int nMouth = 4;
    const int nEyes = 6;
    const int nHat = 4;

    [SerializeField] private Sprite[] bodies;
    [SerializeField] private Sprite[] heads;
    [SerializeField] private Sprite[] ears;
    [SerializeField] private Sprite[] noses;
    [SerializeField] private Sprite[] mouths;
    [SerializeField] private Sprite[] eyes;
    [SerializeField] private Sprite[] hats;

    [SerializeField] private Sprite blueBackground;
    [SerializeField] private Sprite greenBackground;
    [SerializeField] private Sprite redBackground;

    public static int characterHeight = 241;
    public static int characterWidth = 213;

    public static List<string> existingCharacters = new List<string>();
    static System.Random random = new System.Random();

    public float timerTime = 10F;
    float totalTime = 0F;

    public Image timerFill;

    private Screen _screen;

    GameObject bubble;


    // Start is called before the first frame update
    void Start()
    {
        _screen = GetComponent<Screen>();
        bubble = transform.Find("Bubble").gameObject;
        bubble.SetActive(false);
        timerFill.fillAmount = 1F;

        CreateCharacter();
    }

    // Update is called once per frame
    void Update()
    {
        if (bubble.activeSelf)
        {
            if (totalTime <= timerTime)
            {
                totalTime += Time.deltaTime;
                // resize the timer fill
                timerFill.fillAmount = 1 - totalTime / timerTime;
            }
            else
            {
                if (_screen.screenState == ScreenStates.Display)
                {
                    HideBubble();
                    _screen.ScreenOver();
                }
            }
        }
    }

    public void SetBackground(MessageColors color)
    {
        Image background = transform.Find("ScreenContent").GetComponent<Image>();
        switch (color)
        {
            case MessageColors.Red:
                background.sprite = redBackground;
                break;
            case MessageColors.Green:
                background.sprite = greenBackground;
                break;
            case MessageColors.Blue:
                background.sprite = blueBackground;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void ResetTimer()
    {
        totalTime = 0;
        timerFill.fillAmount = 1;
    }

    public void HideBubble()
    {
        bubble.SetActive(false);
    }

    public void ShowBubble()
    {
        bubble.SetActive(true);
    }

    void CreateCharacter()
    {
        int bodyIndex, earsIndex, headIndex, noseIndex, mouthIndex, eyesIndex, hatIndex;
        bodyIndex = earsIndex = headIndex = noseIndex = mouthIndex = eyesIndex = hatIndex = 0;

        bool characterIsUnique = false;
        string characterFeatures = "";
        while (!characterIsUnique)
        {
            bodyIndex = random.Next(bodies.Length);
            earsIndex = random.Next(ears.Length);
            headIndex = random.Next(heads.Length);
            noseIndex = random.Next(noses.Length);
            mouthIndex = random.Next(mouths.Length);
            eyesIndex = random.Next(eyes.Length);
            hatIndex = random.Next(hats.Length);

            characterFeatures = bodyIndex + "" + earsIndex + "" + headIndex + "" + noseIndex + mouthIndex + eyesIndex +
                                "" + hatIndex;
            characterIsUnique = true;

            foreach (string otherCharacter in existingCharacters)
            {
                if (characterFeatures.Equals(otherCharacter))
                {
                    characterIsUnique = false;
                    break;
                }
            }
        }

        existingCharacters.Add(characterFeatures);

        Image characterBody = transform.Find("ScreenContent/Character_body").GetComponent<Image>();
        Image characterEars = transform.Find("ScreenContent/Character_body/Character_ears").GetComponent<Image>();
        Image characterHead = transform.Find("ScreenContent/Character_body/Character_head").GetComponent<Image>();
        Image characterNose = transform.Find("ScreenContent/Character_body/Character_head/Character_nose")
            .GetComponent<Image>();
        Image characterMouth = transform.Find("ScreenContent/Character_body/Character_head/Character_mouth")
            .GetComponent<Image>();
        Image characterEyes = transform.Find("ScreenContent/Character_body/Character_head/Character_eyes")
            .GetComponent<Image>();
        Image characterHat = transform.Find("ScreenContent/Character_body/Character_hat").GetComponent<Image>();

        // load body
        characterBody.sprite = bodies[bodyIndex];

        // load ears
        characterEars.sprite = ears[earsIndex];

        // load head
        characterHead.sprite = heads[headIndex];

        // load nose
        characterNose.sprite = noses[noseIndex];

        // load mouth
        characterMouth.sprite = mouths[mouthIndex];

        // load eyes
        characterEyes.sprite = eyes[eyesIndex];

        // load hat
        characterHat.sprite = hats[hatIndex];
    }
}