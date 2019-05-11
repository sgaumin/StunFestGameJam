using UnityEngine;

public class GameSystem : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LevelManager.Instance.LoadMenu();
        }
    }
}