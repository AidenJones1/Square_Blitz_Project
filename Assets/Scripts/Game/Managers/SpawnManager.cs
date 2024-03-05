using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Manage { get; private set; }

    [Header("Prefabs")]
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _obstacle;
    [SerializeField] private GameObject _objective;
    [SerializeField] private GameObject _multiplier;

    [Header("Spawn Rules")]
    [SerializeField] private int _stagesToObjectiveLevel = 5;
    [SerializeField] private int _stagesToObstacleLevel = 7;
    [SerializeField] private int _chanceToSpawnMultiplayer = 1;

    private int _currentStage = 1;
    private int _currentObjectiveLevel = 1;

    private Vector3 _screenBounds;

    public static ArrayList ObjectivesList = new ArrayList();
    public static ArrayList ObstaclesList = new ArrayList();

    private void Awake()
    {
        SetupSingleton();
    }

    private void Start()
    {
        _screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    public void ResetLevels()
    {
        _stagesToObjectiveLevel = 5;
        _stagesToObstacleLevel = 7;

        _currentObjectiveLevel = 1;
        _currentStage = 1;

        foreach (GameObject obj in ObjectivesList)
        {
            Destroy(obj);
        }
        ObjectivesList.Clear();

        foreach (GameObject obj in ObstaclesList)
        {
            Destroy(obj);
        }
        ObstaclesList.Clear();

        SpawnPlayer();
        SpawnObjectives();
    }

    public void SpawnPlayer()
    {
        if (Physics2D.CircleCast(Vector2.zero, 0.5f, Vector2.right))
        {
            foreach (RaycastHit2D hit in Physics2D.CircleCastAll(Vector2.zero, 0.5f, Vector2.right))
            {
                if (ObstaclesList.Contains(hit.transform.gameObject))
                {
                    ObstaclesList.Remove(hit);
                    Destroy(hit.transform.gameObject);
                }
            }
        }

        GameObject newObj = Instantiate(_player, Vector2.zero, Quaternion.identity);
        newObj.name = "Player";
    }

    private void SpawnObjectives()
    {
        for (int i = 0; i < _currentObjectiveLevel; i++)
            ObjectivesList.Add(Instantiate(_objective, FindPlaceToSpawn(), Quaternion.identity));
    }

    private void SpawnObstacle()
    {
        ObstaclesList.Add(Instantiate(_obstacle, FindPlaceToSpawn(), Quaternion.identity));
    }

    private void SpawnMultiplier()
    {
        Instantiate(_multiplier, FindPlaceToSpawn(), Quaternion.identity);
    }

    private Vector2 FindPlaceToSpawn()
    {
        float randomXPos = Random.Range(-_screenBounds.x, _screenBounds.x);
        float randomYPos = Random.Range(-_screenBounds.y, _screenBounds.y);

        Vector2 position = new Vector2(randomXPos, randomYPos);
        if (Physics2D.CircleCast(position, 0.5f, Vector2.zero))
        {
            position = FindPlaceToSpawn();
        }

        return position;
    }

    public void RespawnObjects()
    {
        //Everytime the user cleared all current objective objects
        if (ObjectivesList.Count <= 0)
        {
            if (_currentStage % _stagesToObjectiveLevel == 0)
            {
                _currentObjectiveLevel++;
            }
            
            if (_currentStage % _stagesToObstacleLevel == 0)
            {
                SpawnObstacle(); 
            }

            //Every 10 stages, the number of stages needed to level up both levels increases by 2
            if (_currentStage % 10 == 0)
            {
                _stagesToObstacleLevel += 2;
                _stagesToObjectiveLevel += 2;
            }
            _currentStage++;
            SpawnObjectives();

            //Rolls for a chance to spawn a multiplier
            if (Mathf.FloorToInt(Random.Range(1, 101)) <= _chanceToSpawnMultiplayer)
                SpawnMultiplier();
            
            GameTime.Timer.ResetTimer();            
        }
    }

    private void SetupSingleton()
    {
        if (Manage == null)
            Manage = this;
        else
            Destroy(this);
    }
}
