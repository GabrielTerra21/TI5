using TMPro;

public class EnemyDisplay : HealthDisplay
{
    public TMP_Text nameText;
    public Character owner;


    public void OnEnterCharacter(Character agent){
        owner = agent;
        nameText.text = owner.data.CharName;
        SetValues(owner);
    }

    public void UpdateInfo(){
        UpdateValues(owner.curHp);
    }
}
