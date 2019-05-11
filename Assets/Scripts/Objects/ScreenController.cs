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

    public static int characterHeight = 241;
    public static int characterWidth = 213;

    public static List<string> existingCharacters = new List<string>();
    static System.Random random = new System.Random();

    public float timerSpeed = 1F;
    float originalSize;
    float totalTime = 0F;

    public Image timerFill;

    private Screen _screen;
        
    // Start is called before the first frame update
    void Start()
    {
        _screen = GetComponent<Screen>();
        
        originalSize = timerFill.rectTransform.rect.height;
<<<<<<< HEAD
=======
        mire.SetActive(false);

>>>>>>> parent of 188937a... Ajusted border + timer in bubble
        CreateCharacter();
    }

    // Update is called once per frame
    void Update()
    {
        totalTime += Time.deltaTime;
        if(timerFill.rectTransform.rect.height > 0)
        {
            // resize the timer fill
            float newSize = originalSize * (1 - totalTime * timerSpeed);
            timerFill.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, newSize);
        } else
        {
            Debug.Log("timer over");
            // show the mire when time is over
        }
    }

    public void ResetTimer()
    {
        totalTime = 0;
    }

    void CreateCharacter()
    {
        int bodyIndex, earsIndex, headIndex, noseIndex, mouthIndex, eyesIndex, hatIndex;
        bodyIndex = earsIndex = headIndex = noseIndex = mouthIndex = eyesIndex = hatIndex = 0;

        bool characterIsUnique = false;
        string characterFeatures = "";
        while (!characterIsUnique)
        {
            bodyIndex = random.Next(nBody);
            earsIndex = random.Next(nEars);
            headIndex = random.Next(nHead);
            noseIndex = random.Next(nNose);
            mouthIndex = random.Next(nMouth);
            eyesIndex = random.Next(nEyes);
            hatIndex = random.Next(nHat);

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
        byte[] bytes = File.ReadAllBytes(Application.dataPath + "/Art/Characters/corps" + bodyIndex + ".png");
        Texture2D bodyTexture = new Texture2D(characterWidth, characterHeight);
        bodyTexture.LoadImage(bytes);
        characterBody.sprite = Sprite.Create(bodyTexture, new Rect(0, 0, characterWidth, characterHeight),
            new Vector2(0.5f, 0.0f), 1.0f);

        // load ears
        bytes = File.ReadAllBytes(Application.dataPath + "/Art/Characters/oreilles/oreille" + earsIndex + ".png");
        Texture2D earsTexture = new Texture2D(characterWidth, characterHeight);
        earsTexture.LoadImage(bytes);
        characterEars.sprite = Sprite.Create(earsTexture, new Rect(0, 0, characterWidth, characterHeight),
            new Vector2(0.5f, 0.0f), 1.0f);

        // load head
        bytes = File.ReadAllBytes(Application.dataPath + "/Art/Characters/base tete/base_tete" + headIndex + ".png");
        Texture2D headTexture = new Texture2D(characterWidth, characterHeight);
        headTexture.LoadImage(bytes);
        characterHead.sprite = Sprite.Create(headTexture, new Rect(0, 0, characterWidth, characterHeight),
            new Vector2(0.5f, 0.0f), 1.0f);

        // load nose
        bytes = File.ReadAllBytes(Application.dataPath + "/Art/Characters/nez/nez" + noseIndex + ".png");
        Texture2D noseTexture = new Texture2D(characterWidth, characterHeight);
        noseTexture.LoadImage(bytes);
        characterNose.sprite = Sprite.Create(noseTexture, new Rect(0, 0, characterWidth, characterHeight),
            new Vector2(0.5f, 0.0f), 1.0f);

        // load mouth
        bytes = File.ReadAllBytes(Application.dataPath + "/Art/Characters/bouches/bouche" + mouthIndex + ".png");
        Texture2D mouthTexture = new Texture2D(characterWidth, characterHeight);
        mouthTexture.LoadImage(bytes);
        characterMouth.sprite = Sprite.Create(mouthTexture, new Rect(0, 0, characterWidth, characterHeight),
            new Vector2(0.5f, 0.0f), 1.0f);

        // load eyes
        bytes = File.ReadAllBytes(Application.dataPath + "/Art/Characters/oeil/oeil" + eyesIndex + ".png");
        Texture2D eyesTexture = new Texture2D(characterWidth, characterHeight);
        eyesTexture.LoadImage(bytes);
        characterEyes.sprite = Sprite.Create(eyesTexture, new Rect(0, 0, characterWidth, characterHeight),
            new Vector2(0.5f, 0.0f), 1.0f);

        // load hat
        bytes = File.ReadAllBytes(Application.dataPath + "/Art/Characters/tophead/tophead" + hatIndex + ".png");
        Texture2D hatTexture = new Texture2D(characterWidth, characterHeight);
        hatTexture.LoadImage(bytes);
        characterHat.sprite = Sprite.Create(hatTexture, new Rect(0, 0, characterWidth, characterHeight),
            new Vector2(0.5f, 0.0f), 1.0f);
    }
}