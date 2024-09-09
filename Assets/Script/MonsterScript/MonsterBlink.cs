using System.Collections;

using UnityEngine;


public class MonsterBlink : MonoBehaviour
{
    public float blinkInterval = 0.1f; // 点滅の間隔
    public float blinkDuration = 0.6f; // 点滅させる合計時間

    private bool isBlinking = false;

    public void StartBlinking()
    {
        if (!isBlinking)
        {
            StartCoroutine(Blink(this.gameObject));
        }
    }

    public IEnumerator Blink(GameObject obj)
    {
        isBlinking = true;
        float elapsedTime = 0f;

        while (elapsedTime < blinkDuration)
        {
            obj.SetActive(!obj.activeSelf); // オブジェクトの表示・非表示を切り替える
            yield return new WaitForSeconds(blinkInterval); // 設定した間隔待機する
            elapsedTime += blinkInterval;
        }

        // 点滅終了後にオブジェクトを表示状態に戻す
        obj.SetActive(true);
        isBlinking = false;
    }
}
