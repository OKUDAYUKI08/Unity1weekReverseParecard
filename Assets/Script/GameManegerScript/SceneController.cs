
using Unity.Mathematics;
using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using unityroom.Api;
using JetBrains.Annotations;
public class SceneController : MonoBehaviour
{
    public GameObject[] backimages;
    public GameObject nowbackimage;
    public int gridRows = 2;  // グリッドの行数
    public int gridCols = 6;  // グリッドの列数
    public float offsetX = 1.6f;  // カードのX方向の間隔
    public float offsetY = 2.3f;  // カードのY方向の間隔

    [SerializeField] private MemoryCard originalCard;  // 元のカード（プレハブ）
    public Levelupcontroller levelupcontroller;
    public Textshake textshakecontroller;
    public resultactivecontroller resultactivecontroller;
    [SerializeField] private GameObject[] card_images;  // カードの画像

    [SerializeField] private GameObject[] monster_images;
    [SerializeField] private GameObject[] boss_images;
    public MemoryCard[] Cards;
    public List<GameObject> EnemyGameobject;
    public int EnemyCount=0;
    Vector2 monsterposition = new Vector2(0,2.5f);

    public GameObject _firstRevealed; //1枚目にめくったカード
    public GameObject _secondRevealed;//2枚目にめくったカード

    public GameObject firstclearcard; //ペア成立後1枚目にめくったカードがあったところに新しく置くカード
    public GameObject secondclearcard;//ペア成立後２枚目にめくったカードににあったところに新しく置くカード

    public MonsterStatus Enemy; 

    public int monsterid; //モンスターを識別するID
    public int setcount;
    public int boolsetcount;

    [Header("攻撃カードが揃った時のSE")]public AudioClip attackSE;
    [Header("敵へのダメージSE")]public AudioClip enemydamageSE;
    [Header("PlayerへのダメージSE")]public AudioClip playerdamageSE;
    [Header("防御カードが揃った時のSE")]public AudioClip gardSE;
    [Header("ガードターンが減った時のSE")]public AudioClip garddestroySE;
    [Header("能力カードが揃った時のSE")]public AudioClip LevelupSE;
    [Header("カードをめくった時のSE")]public AudioClip cardmekuriSE;
    [Header("シャッフルする時のSE")]public AudioClip cardhuffleSE;
    


    
    
    // ２枚目のカードが選ばれていない時にtrueを返す
    public bool canReveal
    {
        get { return _secondRevealed == null; }
    }
    //

    //カードの種類
    public enum CardType
    {
        card_attack,
        card_gard,
        card_damage,
        card_Levelup,
    }
    //



    //モンスターのステータス
    public struct MonsterStatus
    {
        public int Hp;
        public int MaxHp;
        public int Attack_Strong;
        public int Attack_Weak;
        public int Misstolerance;
        public string Name;
        public MonsterStatus(int hp,int maxhp,int attack_strong,int attack_weak,int misstolerance,string name)
        {
            Hp=hp;
            MaxHp=maxhp;
            Attack_Strong=attack_strong;
            Attack_Weak=attack_weak;
            Misstolerance=misstolerance;
            Name=name;
        }
    }
    //
    //モンスターの種類
    public enum MonsterType
    {
        monster_slime,
        monster_mimic,
        monster_pegadog,
        monster_berserker,
        monster_madousi,
        monster_orc,
    }
    public enum BossType
    {
        boss_golem,
        monster_wyvern,
        boss_reddragon,
        boss_leviathan
    }
    //モンスターの初期ステータス
    public MonsterStatus[] monsters = new MonsterStatus[]
    {
        new MonsterStatus(
            hp: 3,
            maxhp:3,
            attack_strong:2,
            attack_weak:1,
            misstolerance:5,
            name : "スライム"
        ),
        new MonsterStatus(
            hp: 1,
            maxhp:1,
            attack_strong:10,
            attack_weak:10,
            misstolerance:1,
            name : "ミミック"
        ),
        new MonsterStatus(
            hp: 5,
            maxhp:5,
            attack_strong:2,
            attack_weak:2,
            misstolerance:3,
            name : "ペガイヌ"
        ),
        new MonsterStatus(
            hp: 15,
            maxhp:15,
            attack_strong:7,
            attack_weak:5,
            misstolerance:3,
            name : "バーサーカー"
        ),
        new MonsterStatus(
            hp: 15,
            maxhp:15,
            attack_strong:8,
            attack_weak:4,
            misstolerance:10,
            name : "魔導師"
        ),
        new MonsterStatus(
            hp: 10,
            maxhp:10,
            attack_strong:2,
            attack_weak:1,
            misstolerance:10,
            name : "オーク"
        ),
    };
    public MonsterStatus[] bosses = new MonsterStatus[]
    {
        new MonsterStatus(
            hp: 50,
            maxhp:50,
            attack_strong:13,
            attack_weak:10,
            misstolerance:10,
            name : "ゴーレム"
        ),
        new MonsterStatus(
            hp: 60,
            maxhp:60,
            attack_strong:20,
            attack_weak:10,
            misstolerance:10,
            name : "ワイバーン"
        ),
        new MonsterStatus(
            hp: 70,
            maxhp:70,
            attack_strong:20,
            attack_weak:10,
            misstolerance:10,
            name : "レッドドラゴン"
        ),
        new MonsterStatus(
            hp: 80,
            maxhp:80,
            attack_strong:30,
            attack_weak:10,
            misstolerance:10,
            name : "リヴァイアサン"
        ),
    };
    //
    


    private void Start()
    {
        parameter.instance.Player_Attack = parameter.instance.InitPlayer_Attack;
        parameter.instance.Player_Hp = parameter.instance.InitPlayer_Hp;
        parameter.instance.Player_MaxHp = parameter.instance.InitPlayer_MaxHp;
        parameter.instance.Player_GardTurn = parameter.instance.InitPlayer_GardTurn;
        Enit();
    }
    public int[] numbers = {0, 0, 1, 1, 2, 2, 3, 3,0,0,1,1};
    private void Enit()
    {
        int randam = 0;
        Vector3 startPos = originalCard.transform.position;
        Cards = new MemoryCard[12];

        randam = (int)UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(MonsterType)).Length);
        monsterid = randam;
        Enemy = new MonsterStatus(hp:monsters[monsterid].Hp , maxhp:monsters[monsterid].MaxHp , attack_strong:monsters[monsterid].Attack_Strong , attack_weak:monsters[monsterid].Attack_Weak , misstolerance:monsters[monsterid].Misstolerance , name:monsters[monsterid].Name);
        GameObject  enemy = Instantiate(monster_images[monsterid],monsterposition,quaternion.identity);

        // フェードインスクリプトを新しく生成したモンスターにアタッチ
        enemy.AddComponent<Fadeinobject>();
        enemy.GetComponent<Fadeinobject>().FadeinobjectStart();
        EnemyGameobject.Add(enemy);
        // 背景生成
        nowbackimage = Instantiate(backimages[UnityEngine.Random.Range(0,backimages.Length)]);

        numbers = ShuffleArray(numbers);

        for (int i = 0; i < gridCols; i++)
        {
            for (int j = 0; j < gridRows; j++)
            {
                MemoryCard card;
                if (i == 0 && j == 0)
                {
                    card = Instantiate(originalCard,new Vector3(-4.30000019f,-1.46000004f,0),quaternion.identity);
                }
                else
                {
                    card = Instantiate(originalCard) as MemoryCard;
                }

                int index = j * gridCols + i;
                int id = numbers[index];
                card.SetCard(id);
                card.index = index;
                Cards[index] = card;

                float posX = (offsetX * i) + startPos.x;
                float posY = -(offsetY * j) + startPos.y;
                card.transform.position = new Vector3(posX, posY, startPos.z);
            }
        }
    }


    // 配列の順番をシャッフルする関数
    private int[] ShuffleArray(int[] numbers)
    {
        int[] newArray = numbers.Clone() as int[];
        for (int i = 0; i < newArray.Length; i++)
        {
            int tmp = newArray[i];
            int r = UnityEngine.Random.Range(i, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = tmp;
        }
        return newArray;
    }

    // カードを選択した時に呼び出す関数
    public void CardRevealed(GameObject card)
    {
        MemoryCard cardscript = card.GetComponent<MemoryCard>();
        parameter.instance.PlaySE(cardmekuriSE);
        if (_firstRevealed == null)
        {

            _firstRevealed = card;
            if(cardscript.id==(int)CardType.card_attack)
            {
                firstclearcard=Instantiate(card_images[cardscript.id],card.transform.position,Quaternion.identity);
            }
            else if(cardscript.id==(int)CardType.card_gard)
            {
                firstclearcard=Instantiate(card_images[cardscript.id],card.transform.position,Quaternion.identity);
            }
            else if(cardscript.id==(int)CardType.card_damage)
            {
                firstclearcard=Instantiate(card_images[cardscript.id],card.transform.position,Quaternion.identity);
            }
            else if(cardscript.id==(int)CardType.card_Levelup)
            {
                firstclearcard=Instantiate(card_images[cardscript.id],card.transform.position,Quaternion.identity);
            }
        }
        else
        {
            _secondRevealed = card;
            if(cardscript.id==(int)CardType.card_attack)
            {
                secondclearcard=Instantiate(card_images[cardscript.id],card.transform.position,Quaternion.identity);
            }
            else if(cardscript.id==(int)CardType.card_gard)
            {
                secondclearcard=Instantiate(card_images[cardscript.id],card.transform.position,Quaternion.identity);
            }
            else if(cardscript.id==(int)CardType.card_damage)
            {
                secondclearcard=Instantiate(card_images[cardscript.id],card.transform.position,Quaternion.identity);
            }
            else if(cardscript.id==(int)CardType.card_Levelup)
            {
                secondclearcard=Instantiate(card_images[cardscript.id],card.transform.position,Quaternion.identity);
            }
            StartCoroutine(CheckMatch());

        }
    }
    //点滅
    public float blinkInterval = 0.1f; // 点滅の間隔
    public float blinkDuration = 0.6f; // 点滅させる合計時間

    private bool isBlinking = false;

    public void StartBlinking()
    {
        if (!isBlinking)
        {
            StartCoroutine(Blink(EnemyGameobject[EnemyCount]));
            parameter.instance.PlaySE(enemydamageSE);
        }
    }
    public void StartBlinkingdeth()
    {
        if (!isBlinking)
        {
            StartCoroutine(Blinkdeth(EnemyGameobject[EnemyCount]));
            parameter.instance.PlaySE(enemydamageSE);
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
    public IEnumerator Blinkdeth(GameObject obj)
    {
        isBlinking = true;
        float elapsedTime = 0f;

        while (elapsedTime < blinkDuration)
        {
            obj.SetActive(!obj.activeSelf); // オブジェクトの表示・非表示を切り替える
            yield return new WaitForSeconds(blinkInterval); // 設定した間隔待機する
            elapsedTime += blinkInterval;
        }
        obj.SetActive(false);
        isBlinking = false;
    }
    
    private System.Collections.IEnumerator CheckMatch()
    {

        //カードが揃った時の処理
        if (_firstRevealed.GetComponent<MemoryCard>().id == _secondRevealed.GetComponent<MemoryCard>().id)
        {
            MemoryCard firstcard;
            MemoryCard secondcard;
            int cardid=_firstRevealed.GetComponent<MemoryCard>().id;
            yield return new WaitForSeconds(1.0f);
            switch(cardid)
            {
                // 攻撃カードが揃った時
                case (int)CardType.card_attack:
                    // 敵にダメージを与える
                    parameter.instance.PlaySE(attackSE);
                    yield return new WaitForSeconds(0.7f);
                    Enemy.Hp -=parameter.instance.Player_Attack;
                    if(Enemy.Hp<=0)
                    {
                        Enemy.Hp=0;
                        StartBlinkingdeth();
                    }
                    else
                    {
                        StartBlinking();
                    }
                    break;

                //　防御カードが揃った時
                case (int)CardType.card_gard:
                    parameter.instance.Player_GardTurn += 2;
                    parameter.instance.PlaySE(gardSE);
                    break;
                
                // ダメージカードが揃った時
                case (int)CardType.card_damage:
                    // ガードターンがあればダメージを受けない
                    if(parameter.instance.Player_GardTurn>0)
                    {
                        parameter.instance.PlaySE(garddestroySE);
                        break;
                    }
                    else
                    {
                        parameter.instance.PlaySE(playerdamageSE);
                        textshakecontroller.TriggerShake();
                        // ダメージを受ける
                        if(Enemy.Misstolerance==0)
                        {
                            // 強攻撃
                            parameter.instance.Player_Hp -= Enemy.Attack_Strong;
                        }
                        else
                        {
                            parameter.instance.Player_Hp -= Enemy.Attack_Weak;
                        }
                        if(parameter.instance.Player_Hp<0)
                        {
                            parameter.instance.Player_Hp=0;
                        }
                        break;
                    }
                // 能力アップカードが揃った時
                case (int)CardType.card_Levelup:
                    parameter.instance.PlaySE(LevelupSE);
                    StartCoroutine(levelupcontroller.InstantiateLevelupcard());
                    break;
            }
            
            // // 防御カード以外が揃った時ガードターンを減らす
            // if(cardid!=(int)CardType.card_gard && GardTurn >0)
            // {
            //     GardTurn--;
            // }

            parameter.instance.score++;


            // 新しいカードの生成
            Vector3 firstcardposition =_firstRevealed.transform.position;
            Vector3 secondcardposition = _secondRevealed.transform.position;
            int firstindex = _firstRevealed.GetComponent<MemoryCard>().index;
            int secondindex = _secondRevealed.GetComponent<MemoryCard>().index;
            firstcard = Instantiate(originalCard);
            secondcard = Instantiate(originalCard);
            firstcard.index = firstindex;
            secondcard.index = secondindex;
            int firstid = UnityEngine.Random.Range(0,4);
            int secondid = UnityEngine.Random.Range(0,4);
            firstcard.SetCard(firstid);
            secondcard.SetCard(secondid);
            firstcard.transform.position = firstcardposition;
            secondcard.transform.position = secondcardposition;
            Cards[firstindex] = firstcard;
            Cards[secondindex] = secondcard;

            if (Enemy.Hp == 0)
            {
                Destroy(firstclearcard);
                Destroy(secondclearcard);   
                parameter.instance.EnemyDefeats++;
                numbers = ShuffleArray(numbers);
                for (int i = 0; i < 12; i++)
                {
                    Cards[i]._id = numbers[i];
                }
                yield return new WaitForSeconds(1.5f);
                
                // 現在の敵キャラクターを破壊
                EnemyGameobject[EnemyCount].GetComponent<MonsterImage>().Destroyme();
                EnemyGameobject[EnemyCount] = null;
                
                // 新しいモンスターを設定
                boolsetcount = parameter.instance.EnemyDefeats/setcount;
                GameObject newEnemy;
                if(boolsetcount==0)
                {
                    monsterid = (int)UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(MonsterType)).Length);
                    Enemy = new MonsterStatus(hp:monsters[monsterid].Hp , maxhp:monsters[monsterid].MaxHp , attack_strong:monsters[monsterid].Attack_Strong , attack_weak:monsters[monsterid].Attack_Weak , misstolerance:monsters[monsterid].Misstolerance , name:monsters[monsterid].Name);
                    newEnemy = Instantiate(monster_images[monsterid], monsterposition, quaternion.identity);
                    Debug.Log("setcount"+setcount);
                    Debug.Log("EnemyDefeats"+parameter.instance.EnemyDefeats);
                    Debug.Log("Boolsetcount"+boolsetcount);
                }
                else
                {
                    monsterid = (int)UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(BossType)).Length);
                    Enemy = new MonsterStatus(hp:bosses[monsterid].Hp , maxhp:bosses[monsterid].MaxHp , attack_strong:bosses[monsterid].Attack_Strong , attack_weak:bosses[monsterid].Attack_Weak , misstolerance:bosses[monsterid].Misstolerance , name:bosses[monsterid].Name);
                    newEnemy = Instantiate(boss_images[monsterid], monsterposition, quaternion.identity);
                    setcount++;
                    boolsetcount=0;
                    Debug.Log("setcount"+setcount);
                    Debug.Log("EnemyDefeats"+parameter.instance.EnemyDefeats);
                    Debug.Log("Boolsetcount"+boolsetcount);
                }

                // 新しいモンスターを設定
                // monsterid = (int)UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(MonsterType)).Length);
                // Enemy = new MonsterStatus(
                //     hp: monsters[monsterid].Hp,
                //     maxhp: monsters[monsterid].MaxHp,
                //     attack_strong: monsters[monsterid].Attack_Strong,
                //     attack_weak: monsters[monsterid].Attack_Weak,
                //     misstolerance: monsters[monsterid].Misstolerance,
                //     name: monsters[monsterid].Name
                // );
                

                
                // フェードインスクリプトを新しく生成したモンスターにアタッチ
                newEnemy.AddComponent<Fadeinobject>();

                // 背景をフェードアウトして破壊
                nowbackimage.GetComponent<Fadebackimage>().FadeOutStart();
                parameter.instance.PlaySE(cardhuffleSE);

                yield return new WaitForSeconds(1.0f);
                // 背景を生成してフェードイン
                GameObject backimage = Instantiate(backimages[UnityEngine.Random.Range(0,backimages.Length)]);
                Destroy(nowbackimage);
                backimage.GetComponent<Fadebackimage>().FadeinimageStart();
                nowbackimage = backimage;
                yield return new WaitForSeconds(1.0f);
                newEnemy.GetComponent<Fadeinobject>().FadeinobjectStart();
                
                EnemyGameobject.Add(newEnemy);
                EnemyCount++;
            }

            Destroy(_firstRevealed);
            Destroy(_secondRevealed);

        }
        // カードが揃わなかった時の処理
        else
        {
            yield return new WaitForSeconds(1.0f);

            _firstRevealed.GetComponent<MemoryCard>().Unreveal();
            _secondRevealed.GetComponent<MemoryCard>().Unreveal();

            // モンスターごとのミス許容値を超えた時
            if(Enemy.Misstolerance==0)
            {

                if(parameter.instance.Player_GardTurn ==0)
                {
                    parameter.instance.PlaySE(playerdamageSE);
                    textshakecontroller.TriggerShake();
                    if(_firstRevealed.GetComponent<MemoryCard>().id == (int)CardType.card_damage || _secondRevealed.GetComponent<MemoryCard>().id == (int)CardType.card_damage)
                    {
                        // 弱攻撃
                        //　片方だけ傷カード
                        parameter.instance.Player_Hp -= Enemy.Attack_Weak;
                    }
                    else
                    {
                        // 通常攻撃
                        parameter.instance.Player_Hp--;
                    }
                }
                // ガードターンがある時にミスした時にガードターンを減らす
                else
                {
                    parameter.instance.Player_GardTurn--;
                    parameter.instance.PlaySE(garddestroySE);
                }
            }

            //ミスしたパラメータ回数
            parameter.instance.misscount++;
            Enemy.Misstolerance--;
            if(Enemy.Misstolerance<0)
            {
                Enemy.Misstolerance =0;
            }


        }
        _firstRevealed = null;
        _secondRevealed = null;
        Destroy(firstclearcard);
        Destroy(secondclearcard);
        if(parameter.instance.Player_Hp<=0)
        {
            parameter.instance.Player_Hp = 0;
        }
        if(parameter.instance.Player_Hp==0)
        {
            parameter.instance.gameover = true;
            resultactivecontroller.ResultActive();
            UnityroomApiClient.Instance.SendScore(1, parameter.instance.EnemyDefeats, ScoreboardWriteMode.HighScoreDesc);
        }
    }
}
