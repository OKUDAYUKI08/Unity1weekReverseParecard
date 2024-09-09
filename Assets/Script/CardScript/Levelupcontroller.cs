using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levelupcontroller : MonoBehaviour
{
    public GameObject[] LevelupCards;
    public Canvas canvas;
    public Dictionary<int,GameObject> LevelupDeckGameobjects = new Dictionary<int, GameObject>();
    public List<int> LevelupDecknumbers = new List<int>(); 
    public GameObject[] popcards = new GameObject[3];


    void Start()
    {
        LevelupDeckGameobjects.Add((int)parameter.LevelupCardType.levelupcard_healhp,LevelupCards[(int)parameter.LevelupCardType.levelupcard_healhp]);
        LevelupDecknumbers.Add((int)parameter.LevelupCardType.levelupcard_healhp);
        LevelupDeckGameobjects.Add((int)parameter.LevelupCardType.levelupcard_attack,LevelupCards[(int)parameter.LevelupCardType.levelupcard_attack]);
        LevelupDecknumbers.Add((int)parameter.LevelupCardType.levelupcard_attack);
        LevelupDeckGameobjects.Add((int)parameter.LevelupCardType.levelupCard_addgardturn,LevelupCards[(int)parameter.LevelupCardType.levelupCard_addgardturn]);
        LevelupDecknumbers.Add((int)parameter.LevelupCardType.levelupCard_addgardturn);
        LevelupDeckGameobjects.Add((int)parameter.LevelupCardType.levelupCard_maxhp,LevelupCards[(int)parameter.LevelupCardType.levelupCard_maxhp]);
        LevelupDecknumbers.Add((int)parameter.LevelupCardType.levelupCard_maxhp);

    }

    public IEnumerator InstantiateLevelupcard()
    {
        parameter.instance.Levelupnow = true;
        for (int i = 0; i < 3; i++)
        {
            int randam = UnityEngine.Random.Range(0, LevelupDecknumbers.Count);
            int cardtypenumber = LevelupDecknumbers[randam];
            GameObject newImageObject = Instantiate(LevelupDeckGameobjects[cardtypenumber]);

            // LevelupCard コンポーネントを取得
            LevelupCard levelupCard = newImageObject.GetComponent<LevelupCard>();

            if (levelupCard != null)
            {
                // id を設定
                levelupCard.id = cardtypenumber;
            }
            else
            {
                Debug.LogError("LevelupCard component not found on the instantiated object.");
            }

            newImageObject.transform.SetParent(canvas.transform, false);
            RectTransform rectTransform = newImageObject.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(204.2647f, 257.6818f); // サイズを設定

            // 任意の位置に配置
            rectTransform.anchoredPosition = new Vector2(-294 + i * 210, -127);
            popcards[i] = newImageObject;
        }

        // クリックを待機するために無限ループを使用
        while (true)
        {
            yield return null; // フレームを待つ
        }
    }

    public IEnumerator DestroyCard()
    {
        foreach(GameObject i in popcards)
        {
            Destroy(i);
        }
        yield return parameter.instance.Levelupnow = false;
    }
}
