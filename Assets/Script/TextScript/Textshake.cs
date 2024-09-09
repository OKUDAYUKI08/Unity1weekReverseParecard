using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Textshake : MonoBehaviour
{
    public float shakeDuration = 0.3f;  // 揺れる時間
    public float shakeMagnitude = 5.0f;  // 揺れの大きさ
    private Vector3 originalPos;
    public TextMeshProUGUI playertext;
    void Start()
    {
        originalPos = playertext.transform.localPosition;
    }
    public void TriggerShake()
    {
        StartCoroutine(Shake());
    }
    IEnumerator Shake()
    {
        float elapsed = 0.0f;
        int direction = 1;

        while (elapsed < shakeDuration)
        {
            float x = direction * shakeMagnitude;
            float y = 1;

            playertext.transform.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);

            elapsed += Time.deltaTime;
            direction *= -1;

            yield return null;
        }

        playertext.transform.localPosition = originalPos;
    }
}
