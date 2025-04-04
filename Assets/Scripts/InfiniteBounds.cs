using UnityEngine;

public class InfiniteBounds : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Camera _mainCamera;



    // Game Loop Methods---------------------------------------------------------------------------

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _mainCamera = Camera.main;
    }


    private void FixedUpdate()
    {
        float sizeBuffer = transform.localScale.x + 0.5f;
        Vector2 topRightCorner = _mainCamera.ViewportToWorldPoint(new Vector2 (1.0f, 1.0f));
        Vector2 bottomLeftCorner = _mainCamera.ViewportToWorldPoint(new Vector2 (0.0f, 0.0f));

        bool _isObjectNeedMoving = false;
        Vector2 newPosition = _rb.position;


        if (newPosition.x > topRightCorner.x + sizeBuffer)
        {
            newPosition.x = bottomLeftCorner.x - sizeBuffer;
            _isObjectNeedMoving = true;
        }

        if (newPosition.x < bottomLeftCorner.x - sizeBuffer)
        {
            newPosition.x = topRightCorner.x + sizeBuffer;
            _isObjectNeedMoving = true;
        }

        if (newPosition.y > topRightCorner.y + sizeBuffer)
        {
            newPosition.y = bottomLeftCorner.y - sizeBuffer;
            _isObjectNeedMoving = true;
        }

        if (newPosition.y < bottomLeftCorner.y - sizeBuffer)
        {
            newPosition.y = topRightCorner.y + sizeBuffer;
            _isObjectNeedMoving = true;
        }

        if (_isObjectNeedMoving)
        {
            _rb.position = newPosition;
        }
    }
}
