using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameSystem : MonoBehaviour
{
    public static GameSystem Instance;

    private Screen[] _screens;
    private List<Screen> _screensDisplay = new List<Screen>();
    private int _nbPhase;

    [HideInInspector] public int messageReceived;
    [SerializeField] private float minTime = 2f;
    [SerializeField] private float maxTime = 5f;

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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LevelManager.Instance.LoadMenu();
        }

        if (messageReceived == _nbPhase)
        {
            messageReceived = 0;
            _nbPhase++;
            StartCoroutine(GenerateWave());
        }
    }

    private IEnumerator GenerateWave()
    {
        InitScreenArray();

        int nubIteraction = Mathf.Min(_nbPhase, _screensDisplay.Count);
        
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(Random.Range(minTime, maxTime));

            // Verify if Screens already spawns a message
            int rand = Random.Range(0, _screens.Length);
//            while (_screensDisplay[rand].demandGenerated)
//                rand = Random.Range(0, _screens.Length);

            // Spawn message
            _screens[rand].GenerateDemand();
        }
    }
}