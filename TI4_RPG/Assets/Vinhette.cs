using UnityEngine;

public class Vinhette : MonoBehaviour {
    [SerializeField] private AnimationClip fadeIn, fadeOut, cover, uncover;
    [SerializeField] private Animation animation;

    private void Awake() {
        animation = GetComponent<Animation>();
    }

    public void FadeIn() {
        animation.clip = fadeIn;
        animation.Play();
    }

    public void FadeOut() {
        animation.clip = fadeOut;
        animation.Play();
    }

    public void Cover() {
        animation.clip = cover;
        animation.Play();
    }

    public void Uncover() {
        animation.clip = uncover;
        animation.Play();
    }
    
}
