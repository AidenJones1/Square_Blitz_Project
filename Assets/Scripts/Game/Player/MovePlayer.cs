using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class MovePlayer : MonoBehaviour
{
    private UnityEvent OnTouchStart = new UnityEvent();
    private UnityEvent OnTouchEnd = new UnityEvent();

    private Collider2D _playerCollider;
    private Rigidbody2D _playerBody;

    private bool _isDragging = false;
    private Vector2 _playerPos;
    private Vector2 _touchPosOffset;

    private void Awake()
    {
        _playerCollider = GetComponent<Collider2D>();
        _playerBody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        OnTouchStart.AddListener(UIManager.Manage.HideTipDisplay);
        OnTouchStart.AddListener(GameTime.Timer.StartTimer);

        OnTouchEnd.AddListener(GameplayManager.Manage.EndGame);
    }

    private void Update()
    {
        if (_isDragging)
        {
            MovePlayerToMouse();
        }
    }

    // Invokes if the user clicked (or tapped) and continues to hold on to the object's collider. Prevents the object from snapping the mouse position by calculating
    // the needed offsetting position.
    private void OnMouseDown()
    {
        OnTouchStart.Invoke();

        _playerPos = transform.position;
        Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _touchPosOffset = touchPos - new Vector2(_playerPos.x, _playerPos.y);

        _isDragging = true;
    }

    // Invokes if the user releases the mouse button (or finger from the screen) while holding the object.
    private void OnMouseUp()
    {
        DeletePlayerObject();
    }

    public void DeletePlayerObject()
    {
        _isDragging = false;
        Destroy(this.gameObject);
        AudioManager.Instance.Play("PlayerDestroyed_1");
        OnTouchEnd.Invoke();
    }

    private void MovePlayerToMouse()
    {
        transform.position = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y) - _touchPosOffset;
    }
}
