using System.Collections;
using UnityEngine;

public class Fadeinobject : MonoBehaviour
{
    public float fadeDuration = 1.0f; // フェードインの時間

    private SpriteRenderer spriteRenderer;
    void Awake()
    {
        // スプライトレンダラーを取得
        spriteRenderer = GetComponent<SpriteRenderer>();

        // 初期アルファを0にして完全に透明にする
        Color color = spriteRenderer.color;
        color.a = 0;
        spriteRenderer.color = color;
    }
    public void FadeinobjectStart()
    {

        // フェードインを開始
        StartCoroutine(FadeIn());
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
}
