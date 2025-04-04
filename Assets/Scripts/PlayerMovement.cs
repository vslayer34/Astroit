using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    
    [SerializeField, Header("Required Components")]
    private SpriteRenderer _exhaustSpriteRenderer;

    [SerializeField, Space(15)]
    private float _movementSpeed = 1.0f;

    [SerializeField]
    private float _turnSpeed = 10.0f;

    private float _moveForce;
    private float _turnForce;



    // Game loop Methods---------------------------------------------------------------------------

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rb.AddForce(_moveForce * transform.up);
        _rb.AddTorque(_turnForce);
    }

    private void OnDisable()
    {
        // Reset the player when its gameobject is disabled
        _moveForce = 0.0f;
        _turnForce = 0.0f;
        _rb.linearVelocity = Vector2.zero;
        _exhaustSpriteRenderer.color = new Color(_exhaustSpriteRenderer.color.r, _exhaustSpriteRenderer.color.g, _exhaustSpriteRenderer.color.b, 0.0f);
    }

    // Signal Methods------------------------------------------------------------------------------

    private void OnMove(InputValue value)
    {
        _moveForce = value.Get<float>() * _movementSpeed;
        Debug.Log(value.Get<float>());
        _exhaustSpriteRenderer.color = new Color(_exhaustSpriteRenderer.color.r, _exhaustSpriteRenderer.color.g, _exhaustSpriteRenderer.color.b, value.Get<float>());
    }

    private void OnTurn(InputValue value)
    {
        _turnForce = value.Get<float>() * _turnSpeed;        
    }
}
