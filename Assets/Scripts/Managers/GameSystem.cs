using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameSystem : MonoBehaviour
{
    public int messageReceived;

    private Screen[] _screens;
    private int _nbPhase;

    [SerializeField] private float minTime = 2f;
    [SerializeField] private float maxTime = 5f;

    private void Start()
    {
        _nbPhase = 1;
        _screens = FindObjectsOfType<Screen>();

        StartCoroutine(GenerateWave());
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
            StartCoroutine(GenerateWave());
        }
    }

    private IEnumerator GenerateWave()
    {
        for (int i = 0; i < _nbPhase; i++)
        {
            yield return new WaitForSeconds(Random.Range(minTime, maxTime));
            _screens[Random.Range(0, _screens.Length)].GenerateDemand();
        }

        _nbPhase++;
    }
}