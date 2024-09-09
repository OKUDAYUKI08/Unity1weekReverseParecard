using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fadebackimage : MonoBehaviour
{
    public float fadeDuration = 1.0f; // フェードインの時間

    private SpriteRenderer spriteRenderer;


    public void FadeinimageStart()
    {
        // スプライトレンダラーを取得
        spriteRenderer = GetComponent<SpriteRenderer>();

        // 初期アルファを0にして完全に透明にする
        Color color = spriteRenderer.color;
        color.a = 0;
        spriteRenderer.color = color;
        // フェードインを開始
        StartCoroutine(FadeIn());
    }

    public void FadeOutStart()
    {
        // スプライトレンダラーを取得
        spriteRenderer = GetComponent<SpriteRenderer>();

        Color color = spriteRenderer.color;
        spriteRenderer.color = color;

        // フェードインを開始
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        Color color = spriteRenderer.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration); // アルファ値を0から1まで変化
            spriteRenderer.color = color;

            yield return null; // 次のフレームまで待機
        }

        // 最後にアルファ値を1に設定（完全に表示）
        color.a = 1;
        spriteRenderer.color = color;
    }
    IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        Color color = spriteRenderer.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(1f - (elapsedTime / fadeDuration)); // アルファ値を1から0まで変化
            spriteRenderer.color = color;

            yield return null; // 次のフレームまで待機
        }

        // 最後にアルファ値を0に設定（完全に透明）
        color.a = 0.2f;
        spriteRenderer.color = color;
    }
}
