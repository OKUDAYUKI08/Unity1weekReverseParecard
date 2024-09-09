
using UnityEngine;

public class MemoryCard : MonoBehaviour
{
    public SceneController controller;  // ゲームのコントローラー
    public int _id;  // カードのID（ペアを識別）
    public int index;

    void Start()
    {
        controller = GameObject.Find("SceneController").GetComponent<SceneController>();
    }
    public void SetCard(int id)
    {
        _id = id;
    }

    public int id
    {
        get { return _id; }
    }

    void OnMouseDown()
    {
        if (this.gameObject.activeSelf && controller.canReveal && !parameter.instance.Levelupnow && !parameter.instance.gameover && !parameter.instance.soundsetting)
        {
            this.gameObject.SetActive(false);  // 裏面を非表示にして表面を表示
            controller.CardRevealed(this.gameObject);
        }
    }

    public void Unreveal()
    {
        this.gameObject.SetActive(true);  // 裏面を再び表示
    }
    public void Destroyme()
    {
        Destroy(this.gameObject);
    }
}
