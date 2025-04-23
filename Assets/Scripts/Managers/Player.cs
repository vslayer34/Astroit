using UnityEngine;

public class Player : MonoBehaviour
{
    public static int totalPlayersNumber;

    public int PlayerNumber { get; private set; }

    private int _lives = 3;
    private Damagable _damagableScript;



    // Game Loop Methods---------------------------------------------------------------------------

    private void Awake()
    {
        PlayerNumber = totalPlayersNumber++;

        _damagableScript = GetComponent<Damagable>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Asteroid asteroid))
        {
            if (_damagableScript.Damage(200.0f))
            {
                _lives--;
                GameManager.Instance.ReportPlayerDeath(gameObject, PlayerNumber, _lives);
                // Player dies
            }
        }
    }
}
