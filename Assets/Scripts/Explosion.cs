using System.Collections;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    private float _duration = 0.5f;

    [SerializeField]
    private AnimationCurve _changeCurve;

    [SerializeField]
    private AudioClip _explosionAudio;



    // Game Loop Methods---------------------------------------------------------------------------

    private void OnEnable()
    {
        StartCoroutine(ShrinkScale());
    }

    // Member Methods------------------------------------------------------------------------------

    private IEnumerator ShrinkScale()
    {
        AudioManager.Instance.PlaySFX(_explosionAudio);

        // float startScale = transform.localScale.x;
        float startScale = 0.3f;
        float scale = startScale;
        float timer = 0.0f;

        while (timer < _duration)
        {
            float t = timer / _duration;
            scale = Mathf.Lerp(startScale, 0.0f, _changeCurve.Evaluate(t));
            transform.localScale = Vector3.one * scale;

            timer += Time.deltaTime;
            yield return null;
        }

        transform.localScale = Vector3.zero;
        gameObject.SetActive(false);
    }
}
