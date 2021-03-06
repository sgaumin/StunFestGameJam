﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameSystem : MonoBehaviour
{
    public static GameSystem Instance;

    public GameStates gameState = GameStates.Play;

    [HideInInspector] public int messageReceived;
    [HideInInspector] public int screenMire = 0;
    [SerializeField] private float minTime = 2f;
    [SerializeField] private float maxTime = 5f;
    [SerializeField] private int limitScreenMire = 4;

    [HideInInspector] public List<Screen> screensDisplay = new List<Screen>();
    private int _nbPhase;
    private float _timeScore;
    private int _numbInteraction;

    [SerializeField] private SpriteRenderer spriteRendererHeart;
    [SerializeField] private Sprite[] spriteHearts;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != null)
            Destroy(gameObject);
    }

    private void Start()
    {
        screensDisplay = FindObjectsOfType<Screen>().ToList();

        _nbPhase = 1;
        _numbInteraction = 1;

        StartCoroutine(GenerateWave());
    }

    private void Update()
    {
        if (gameState == GameStates.Play)
        {
            if (screenMire == limitScreenMire)
            {
                // Game Over
                StartCoroutine(GameOver());
            }

//            Debug.Log(messageReceived + "/" + _nbPhase);

            if (Input.GetKeyDown(KeyCode.Escape))
                LevelManager.Instance.LoadMenu();

            if (messageReceived == _numbInteraction)
            {
                messageReceived = 0;
                _nbPhase++;
                StartCoroutine(GenerateWave());
            }

            // Update Time Score
            _timeScore += Time.deltaTime;
        }
    }

    private IEnumerator GenerateWave()
    {
        yield return new WaitForSeconds(2f);

        _numbInteraction = Mathf.Min(_nbPhase, screensDisplay.Count);

        List<int> randAlreadyCreate = new List<int>();

        for (int i = 0; i < _numbInteraction; i++)
        {
            yield return new WaitForSeconds(Random.Range(minTime, maxTime));

            int rand = 0;
            bool check = false;
            while (!check)
            {
                rand = Random.Range(0, screensDisplay.Count);

                if (!randAlreadyCreate.Contains(rand))
                {
                    if (screensDisplay[rand].screenState == ScreenStates.Display)
                    {
                        if (!screensDisplay[rand].demandGenerated)
                        {
                            check = true;
                        }
                    }
                }
            }

            randAlreadyCreate.Add(rand);

            // Spawn message
            screensDisplay[rand].GenerateDemand();

            yield return null;
        }
    }

    private IEnumerator GameOver()
    {
        // Update Game State
        gameState = GameStates.GameOver;

        yield return new WaitForSeconds(3f);

        // Show GameOver Screen

        // Load Menu
        LevelManager.Instance.LoadMenu();
    }

    public void UpdateLife()
    {
        screenMire++;
        if (screenMire < limitScreenMire)
            spriteRendererHeart.sprite = spriteHearts[screenMire];
        else if (screenMire == limitScreenMire)
            spriteRendererHeart.sprite = null;
    }
}