using UnityEngine;

public class Vinhette : MonoBehaviour {
    [SerializeField] private AnimationClip fadeIn, fadeOut;
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
}
