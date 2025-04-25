using UnityEngine;

public class Projectile : MonoBehaviour
{
    [field: SerializeField, Header("Projectile stats")]
    public int PlayerNumber { get; private set; }

    [SerializeField]
    private float _speed = 30.0f;

    [SerializeField]
    private float _damagedAmount = 30.0f;

    [SerializeField]
    private float _lifeSpan = 5.0f;


    [SerializeField, Header("Audio Clips")]
    private AudioClip _fireAudio;

    [SerializeField]
    private AudioClip _impactAudio;

    private float _lifeTimer;
    
    private Collider2D[] _hitArray = new Collider2D[5];



    // Game Loop Methods---------------------------------------------------------------------------

    private void OnEnable()
    {
        AudioManager.Instance.PlaySFX(_fireAudio);
    }

    private void Update()
    {
        // Move the projectile
        transform.position += transform.up * _speed * Time.deltaTime;

        // Determine collisions
        int hitCount = Physics2D.OverlapBox(transform.position, transform.localScale, transform.rotation.z, new ContactFilter2D(), _hitArray);

        if (hitCount > 0)
        {
            for (int i = 0; i < hitCount; i++)
            {
                if (_hitArray[i].TryGetComponent(out Damagable damagable))
                {
                    if (damagable.Damage(_damagedAmount))
                    {
                        // Destroy the asteroid
                        if (_hitArray[i].TryGetComponent<Asteroid>(out Asteroid asteroid))
                        {
                            GameManager.Instance.UpdatePlayersScore(PlayerNumber, asteroid.PointValue);
                        }
                    }

                    AudioManager.Instance.PlaySFX(_impactAudio);
                    gameObject.SetActive(false);
                    break;
                }
            }
        }

        // manage life time
        _lifeTimer += Time.deltaTime;

        // disable the projectile at the end of its life cycle
        if (_lifeTimer >= _lifeSpan)
        {
            gameObject.SetActive(false);
        }
    }

    void OnDisable()
    {
        _lifeTimer = 0.0f;
    }
}
