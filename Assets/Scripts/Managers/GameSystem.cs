using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameSystem : MonoBehaviour
{
    public static GameSystem Instance;

    public GameStates gameState = GameStates.Play;

    [HideInInspector] public int messageReceived;
    [HideInInspector] public int screenMire;
    [SerializeField] private float minTime = 2f;
    [SerializeField] private float maxTime = 5f;
    [SerializeField] private int limitScreenMire = 4;
    
    private Screen[] _screens;
    private List<Screen> _screensDisplay = new List<Screen>();
    private int _nbPhase;
    private float _timeScore;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != null)
            Destroy(gameObject);
    }

    private void Start()
    {
        _nbPhase = 1;
        _screens = FindObjectsOfType<Screen>();

        InitScreenArray();

        StartCoroutine(GenerateWave());
    }

    private void InitScreenArray()
    {
        _screensDisplay.Clear();
        foreach (var screen in _screens)
        {
            if (screen.screenState == ScreenStates.Display)
            {
                _screensDisplay.Add(screen);
            }
        }
    }

    private void Update()
    {
        if (gameState == GameStates.Play)
        {
            if (screenMire == limitScreenMire)
            {
                // Game Over
                GameOver();
            }

            Debug.Log(messageReceived + "/" + _nbPhase);

            if (Input.GetKeyDown(KeyCode.Escape))
                LevelManager.Instance.LoadMenu();

            if (messageReceived == _nbPhase)
            {
                Debug.Log("End Phase");
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
        InitScreenArray();
        int numbInteraction = Mathf.Min(_nbPhase, _screensDisplay.Count);

        for (int i = 0; i < numbInteraction; i++)
        {
            yield return new WaitForSeconds(Random.Range(minTime, maxTime));

            // Verify if Screens already spawns a message
//            InitScreenArray();
//            numbInteraction = Mathf.Min(_nbPhase, _screensDisplay.Count);

            int rand = Random.Range(0, _screensDisplay.Count);

            Debug.Log("Houba");
            
            while (_screensDisplay[rand].demandGenerated || _screensDisplay[rand].screenState == ScreenStates.Mire)
                rand = Random.Range(0, _screensDisplay.Count);

            // Spawn message
            Debug.Log(_screens[rand].gameObject.name);
            _screens[rand].GenerateDemand();
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over");
        
        // Update Game State
        gameState = GameStates.GameOver;

        // Show GameOver Screen

    }
}