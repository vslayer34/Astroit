using UnityEngine;

public class Damagable : MonoBehaviour
{
    private const float MAX_HEALTH = 100.0f;
    private float _health;



    // Game Loop Methods---------------------------------------------------------------------------

    private void OnEnable()
    {
        _health = MAX_HEALTH;
    }

    // Member Methods------------------------------------------------------------------------------

    public bool Damage(float damage)
    {
        if (!isActiveAndEnabled)
        {
            return false;
        }

        _health -= damage;

        if (_health <= 0.0f)
        {
            var explosion = GameManager.Instance.ExplosionPool.GetPoolObject();
            explosion.transform.position = transform.position;
            explosion.transform.localScale = transform.localScale;

            explosion.SetActive(true);
            gameObject.SetActive(false);

            return true;
        }

        return false;
    }
}
