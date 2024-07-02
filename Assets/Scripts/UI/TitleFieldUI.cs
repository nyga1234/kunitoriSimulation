using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleFieldUI : MonoBehaviour
{
    //シングルトン化（どこからでもアクセスできるようにする）
    public static TitleFieldUI instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public Text titleFieldText;
    public Text titleFieldSubText;

    public void ShowCharacterChoiceText()
    {
        this.gameObject.SetActive(true);
        titleFieldText.text = "      プレイするキャラを選択してください";
    }

    public void ShowInformationText()
    {
        this.gameObject.SetActive(true);
        titleFieldText.text = "      情報の参照";
    }

    public void ShowAppointmentText()
    {
        this.gameObject.SetActive(true);
        titleFieldText.text = "     配下の身分 / 給料を変更";
    }
    
    public void ShowSearchText()
    {
        this.gameObject.SetActive(true);
        titleFieldText.text = "     無所属の人材を探索";
    }

    public void ShowBanishmentText()
    {
        this.gameObject.SetActive(true);
        titleFieldText.text = "     配下を勢力から追放";
    }

    public void ShowAllianceText()
    {
        this.gameObject.SetActive(true);
        titleFieldText.text = "     他勢力と同盟を締結";
    }

    public void ShowLaureateText()
    {
        this.gameObject.SetActive(true);
        titleFieldText.text = "     全てを捨て放浪";
    }

    public void ShowRecruitText()
    {
        this.gameObject.SetActive(true);
        titleFieldText.text = "     兵士を雇用";
    }

    public void ShowTrainingText()
    {
        this.gameObject.SetActive(true);
        titleFieldText.text = "     兵士を訓練";
    }

    public void ShowEnterText()
    {
        this.gameObject.SetActive(true);
        titleFieldText.text = "     任意の勢力に所属";
    }

    public void ShowVagabondText()
    {
        this.gameObject.SetActive(true);
        titleFieldText.text = "     所属の勢力を去る";
    }
    
    public void ShowRebellionText()
    {
        this.gameObject.SetActive(true);
        titleFieldText.text = "     クーデターを起こす";
    }

    public void ShowFunctionText()
    {
        this.gameObject.SetActive(true);
        titleFieldText.text = "     セーブ / 設定 / ゲームの終了";
    }

    public void ShowAttackText()
    {
        this.gameObject.SetActive(true);
        titleFieldText.text = "     隣接の他国へ侵攻";
    }

    public void ShowProvokeText()
    {
        this.gameObject.SetActive(true);
        titleFieldText.text = "     敵将を戦場へ誘います";
    }

    public void ShowSubdueText()
    {
        this.gameObject.SetActive(true);
        titleFieldText.text = "     不忠の逆賊を討伐";
    }

    public void ShowEndText()
    {
        this.gameObject.SetActive(true);
        titleFieldText.text = "     ターンの終了";
    }

    public void ShowPlayerLordPhase()
    {
        this.gameObject.SetActive(true);
        titleFieldText.text = "     プレイヤー領主フェーズ";
    }

    public void ShowPlayerPersonalPhase()
    {
        this.gameObject.SetActive(true);
        titleFieldText.text = "     プレイヤー個人フェーズ";
    }

    public void ShowPlayerBattlePhase()
    {
        this.gameObject.SetActive(true);
        titleFieldText.text = "     プレイヤー戦闘フェーズ";
    }
    
    public void ShowChangeLordTurnText()
    {
        this.gameObject.SetActive(true);
        titleFieldText.text = "     " + GameManager.instance.turnCount + "期";
        titleFieldSubText.text = "国家フェーズ";
    }

    public void ShowChangePersonalTurnText()
    {
        this.gameObject.SetActive(true);
        titleFieldText.text = "     " + GameManager.instance.turnCount + "期";
        titleFieldSubText.text = "個人フェーズ";
    }

    public void ShowChangeBattleTurnText()
    {
        this.gameObject.SetActive(true);
        titleFieldText.text = "     " + GameManager.instance.turnCount + "期";
        titleFieldSubText.text = "戦闘フェーズ";
    }

    public void HideTitleSubText()
    {
        titleFieldSubText.gameObject.SetActive(false);
    }
}
