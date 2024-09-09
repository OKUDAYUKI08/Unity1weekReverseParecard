
using UnityEngine;
using TMPro;

public class CanvasTextController : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI MissText;
    public TextMeshProUGUI MonsterText;
    public TextMeshProUGUI GardTurnText;
    public TextMeshProUGUI PlayerStatusText;
    public TextMeshProUGUI MissToleranceText;
    public TextMeshProUGUI MonsterMissText;

    public TextMeshProUGUI tuyoText;
    public SceneController sceneController;


    // Update is called once per frame
    void Update()
    {
        ScoreText.text = $"paredcount:{parameter.instance.score}";
        MissText.text = $"misscount:{parameter.instance.misscount}";
        MonsterText.text =$"{sceneController.Enemy.Name} Hp:{sceneController.Enemy.Hp}/{sceneController.Enemy.MaxHp}";
        MonsterMissText.text=$"リミット:{sceneController.Enemy.Misstolerance}";
        GardTurnText.text =$"gardTurn:{parameter.instance.Player_GardTurn}";
        MissToleranceText.text =$"misstolerance:{sceneController.Enemy.Misstolerance}";
        PlayerStatusText.text=$"Hp/MaxHp:{parameter.instance.Player_Hp}/{parameter.instance.Player_MaxHp}\n";
        PlayerStatusText.text+=$"Attack:{parameter.instance.Player_Attack}\n";
        PlayerStatusText.text+=$"Gard:{parameter.instance.Player_GardTurn}";
        tuyoText.text =$"強:{sceneController.Enemy.Attack_Strong}\n";
        tuyoText.text +=$"弱:{sceneController.Enemy.Attack_Weak}\n";
    }
}
