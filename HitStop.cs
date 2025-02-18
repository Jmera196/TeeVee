using System.Collections;
using UnityEngine;

public class HitStop : MonoBehaviour
{
    [Range(0,1.5f)]
    public float duration = 1f;
    bool _isFrozen = false;
    float _pendingFreezeDuration = 0f;
    // Update is called once per frame
    void Update()
    {
        if (_pendingFreezeDuration > 0 && !_isFrozen)
        {
            StartCoroutine(DoFreeze());
        }
    }
    public void Freeze()
    {
        _pendingFreezeDuration = duration;
    }

    IEnumerator DoFreeze()
    {
        _isFrozen = true;
        var original = Time.timeScale;
        Time.timeScale = 0f;
        
        yield return new WaitForSecondsRealtime(duration);

        Time.timeScale = original;
        _pendingFreezeDuration = 0;
        _isFrozen = false;
    }
}
