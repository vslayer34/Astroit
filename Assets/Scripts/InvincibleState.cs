using System.Collections;
using UnityEngine;

public class InvincibleState : MonoBehaviour
{
    [SerializeField]
    private float _duration = 2.0f;

    [SerializeField]
    private float _flashingIntervalDuration = 0.25f;

    [SerializeField]
    private Damagable _damagableComponent;

    [SerializeField]
    private SpriteRenderer _shipSprite;

    private bool _isFirstSpawn = true;



    // Game Loop Methods---------------------------------------------------------------------------

    private void OnEnable()
    {
        if (_isFirstSpawn)
        {
            _isFirstSpawn = false;
            return;
        }

        StartCoroutine(StartInvicibleSequence());
    }

    private void OnDisable()
    {
        // make sure the components is turned on even the player is disabled during the coroutine
        _damagableComponent.enabled = true;
        _shipSprite.enabled = true;
    }

    // Member Methods------------------------------------------------------------------------------

    private IEnumerator StartInvicibleSequence()
    {
        _damagableComponent.enabled = false;
        bool isVisible = false;

        float timer = 0.0f;
        float flashingInterval = 0.0f;

        while (timer <= _duration)
        {
            if (flashingInterval > _flashingIntervalDuration)
            {
                _shipSprite.enabled = isVisible;
                isVisible = !isVisible;
                flashingInterval -= _flashingIntervalDuration;
            }

            timer += Time.deltaTime;
            flashingInterval += Time.deltaTime;
            yield return null;
        }

        _damagableComponent.enabled = true;
        _shipSprite.enabled = true;
    }
}
