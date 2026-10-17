using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 10f;
    private Vector3 _targetPos;
    private Rigidbody2D _rb;
    public bool isMoving = false;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10f));
            _targetPos = worldPos;
            isMoving = true;
        }

        if (isMoving && Vector3.Distance(transform.position, _targetPos) < 0.1f)
        {
            isMoving = false;
        }

        if (isMoving)
        {
            Vector3 newPos = Vector2.MoveTowards(_rb.position, _targetPos, _moveSpeed * Time.deltaTime);
            _rb.MovePosition(newPos);
        }
    }
}
