using UnityEngine;

public class LevelupCard : MonoBehaviour
{
    public int _id;

    public int id
    {
        get { return _id; }
        set { _id = value; }
    }
    public Levelupcontroller controller; // Levelupcontroller の参照を保持

    public void Start()
    {
        controller = GameObject.Find("LevelupController").GetComponent<Levelupcontroller>();
    }
    
    public void Click()
    {
        Debug.Log("Card clicked: " + id);
        switch(id)
        {
            case (int)parameter.LevelupCardType.levelupcard_attack:
                parameter.instance.PlaySE(parameter.instance.leveliaiSE);
                Debug.Log("攻撃力");
                parameter.instance.Player_Attack +=2;
                break;
            case (int)parameter.LevelupCardType.levelupcard_healhp:
                parameter.instance.PlaySE(parameter.instance.levelHealSE);
                Debug.Log("回復");
                parameter.instance.Player_Hp +=10;
                if(parameter.instance.Player_Hp>parameter.instance.Player_MaxHp)
                {
                    parameter.instance.Player_Hp=parameter.instance.Player_MaxHp;
                }
                break;
            case (int)parameter.LevelupCardType.levelupCard_maxhp:
                parameter.instance.PlaySE(parameter.instance.levelHealSE);
                Debug.Log("最大HP");
                parameter.instance.Player_MaxHp+=3;
                break;
            case (int)parameter.LevelupCardType.levelupCard_addgardturn:
                parameter.instance.PlaySE(parameter.instance.leveliaiSE);
                Debug.Log("ガードターン");
                parameter.instance.Player_GardTurn+=3;
                break;
            default:
                break;
        }
        // カードを破壊
        StartCoroutine(controller.DestroyCard());
    }
}
