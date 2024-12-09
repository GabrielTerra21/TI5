public class Seller : WaitingTrigger
{
    public NewStore shop;
    public Dialogue[] dialogue;
    protected int counter = 0;

    protected override void Start() {
        base.Start();
        action.AddListener(() => {
            if(GameManager.Instance.shopUnlocked){ 
                shop.OpenStore();
                return;
            }
            GameManager.Instance.shopUnlocked = true;
            GameManager.Instance.DialogueManager.StartDialogue(dialogue[counter]);
            if (counter < dialogue.Length - 1) counter++;
        });
    }
}
