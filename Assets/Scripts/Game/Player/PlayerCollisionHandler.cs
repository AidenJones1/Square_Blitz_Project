using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCollisionHandler : MonoBehaviour
{
    public UnityEvent OnObjectiveCollect = new UnityEvent();
    public UnityEvent OnMuliplierCollect = new UnityEvent();
    public UnityEvent OnObstacleCollect = new UnityEvent();

    private void Start()
    {
        OnObjectiveCollect.AddListener(ScoreManager.Manage.AddScore);
        OnObjectiveCollect.AddListener(SpawnManager.Manage.RespawnObjects);

        OnMuliplierCollect.AddListener(ScoreManager.Manage.StartMultiplierRoutine);

        OnObstacleCollect.AddListener(GameplayManager.Manage.EndGame);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject obj = collision.gameObject;

        if (obj.CompareTag("Objective"))
        {
            Destroy(obj);
            AudioManager.Instance.Play("ObjectiveCollect_" + Random.Range(1, 4));
            SpawnManager.ObjectivesList.Remove(obj);
            OnObjectiveCollect.Invoke();
        }

        else if (obj.CompareTag("Obstacle"))
        {
            Destroy(this.gameObject);
            AudioManager.Instance.Play("PlayerDestroyed_1");
            OnObstacleCollect.Invoke();
        }

        else if (obj.CompareTag("Multiplier"))
        {
            Destroy(obj);
            AudioManager.Instance.Play("MultiplierCollect_1");
            OnMuliplierCollect.Invoke();
        }
    }
}
