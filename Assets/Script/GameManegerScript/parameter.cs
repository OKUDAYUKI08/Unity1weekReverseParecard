using UnityEngine;
public class parameter : MonoBehaviour
{
    public static parameter instance=null;
    public AudioClip levelHealSE;
    public AudioClip leveliaiSE;

    // Scoreパラメータ
    [Header("揃えたペアの数")]public int score;
    [Header("神経衰弱でミスした数")]public int misscount;
    [Header("的を倒した数")]public int EnemyDefeats;
    [Header("SEを鳴らすスピーカー")]private AudioSource audioSource = null;
    [Header("Seの音の大きさ")]public float SeVolume;
    void Update()
    {
        audioSource.volume= SeVolume;
    }

    // Script


    // 判別
    [Header("能力アップ選択中か")]public bool Levelupnow = false;
    [Header("ゲームオーバーか")]public bool gameover = false;
    [Header("サウンド設定中か")]public bool soundsetting = false;
    // Playerのステータス
    [Header("プレイヤーの名前")]public string Player_Name;
    [Header("プレイヤーのHP")]public int Player_Hp;
    [Header("プレイヤーの最大HP")]public int Player_MaxHp;
    [Header("プレイヤーの攻撃力")]public int Player_Attack;
    [Header("プレイヤーの残りガードターン数")]public int Player_GardTurn;
    [Header("プレイヤーの初期HP")]public int InitPlayer_Hp;
    [Header("プレイヤーの初期最大HP")]public int InitPlayer_MaxHp;
    [Header("プレイヤーの初期攻撃力")]public int InitPlayer_Attack;
    [Header("プレイヤーの初期残りガードターン数")]public int InitPlayer_GardTurn;
    [Header("出現したモンスターの識別ID")]public int monsterid;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    // このカードの順番とLevelupcontrollerのLevelupCardsに入っているオブジェクトの順番は一致させる
    public enum LevelupCardType
    {
        levelupcard_attack,
        levelupCard_addgardturn,
        levelupCard_maxhp,
        levelupcard_healhp,
    }

    private void Awake(){
        if(instance==null){
            instance=this;
            DontDestroyOnLoad(this.gameObject);
        }
        else{
            Destroy(this.gameObject);
        }
    }
    public void PlaySE(AudioClip audioClip)
    {
        if(audioSource!=null)
        {
            audioSource.PlayOneShot(audioClip);
        }
        else
        {
            Debug.Log("オーディオソースが設定されていません");
        }
    }
}
