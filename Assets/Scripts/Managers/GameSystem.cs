using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public List<Screen> screensDisplay = new List<Screen>();
    private int _nbPhase;
    private float _timeScore;
    private int _numbInteraction;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != null)
            Destroy(gameObject);
    }

    private void Start()
    {
        _screens = FindObjectsOfType<Screen>();
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
                GameOver();
            }

//            Debug.Log(messageReceived + "/" + _nbPhase);

            if (Input.GetKeyDown(KeyCode.Escape))
                LevelManager.Instance.LoadMenu();

            if (messageReceived == _numbInteraction)
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
        Debug.Log("Generate");

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
                Debug.Log("Houba" + rand);

                if (!randAlreadyCreate.Contains(rand))
                {
                    if (screensDisplay[rand].screenState == ScreenStates.Display)
                    {
                        Debug.Log(screensDisplay[rand].screenState);

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

    private void GameOver()
    {
        Debug.Log("Game Over");

        // Update Game State
        gameState = GameStates.GameOver;

        // Show GameOver Screen
    }
}