using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class CameraShake : MonoBehaviour
{

    [SerializeField] float shakeDuration = 1f;
    [SerializeField] float shakeMagnitude = 0.1f;

    [SerializeField] int traumaDuration = 5000;

    Vector3 initialPosition;

    Stopwatch traumaTimer;

    void Start()
    {
        initialPosition = transform.position;
        traumaTimer = new Stopwatch();
        traumaTimer.Start();
    }

    public void Play(float damage)
    {
        StartCoroutine(Shake(damage));
    }

    IEnumerator Shake(float damage)
    {
        float elapsedTime = 0f;

        if (traumaTimer.ElapsedMilliseconds >= traumaDuration)
        {
            traumaTimer.Restart();
            shakeMagnitude = 0.1f;
        }
        else
        {
            shakeMagnitude += 0.1f;

        }


        while (elapsedTime < shakeDuration)
        {
            transform.position = initialPosition + (Vector3)Random.insideUnitCircle * (CalculateShake(damage));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        transform.position = initialPosition;
    }

    private float CalculateShake(float damage)
    {
        var magnitude = shakeMagnitude + (1 / damage);
        UnityEngine.Debug.Log(magnitude.ToString());
        return magnitude;
    }
}
