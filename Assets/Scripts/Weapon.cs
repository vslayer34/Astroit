using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private float _offset = 0.75f;

    private int _playerNumber;



    // Game Loop Methods---------------------------------------------------------------------------

    private void Start()
    {
        _playerNumber = GetComponent<Player>().PlayerNumber;
    }

    // Signal Methods------------------------------------------------------------------------------

    public void OnFire(InputValue value)
    {
        GameObject projectile = GameManager.Instance.ProjectilePools[_playerNumber].GetPoolObject();

        projectile.transform.position = transform.position + (_offset * transform.up);
        projectile.transform.rotation = transform.rotation;
    }
}
