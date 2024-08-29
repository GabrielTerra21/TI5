using UnityEngine;
using UnityEngine.Serialization;

public class Vinhette : MonoBehaviour {
    [SerializeField] private AnimationClip fadeIn, fadeOut, cover, uncover;
    [FormerlySerializedAs("animation")] [SerializeField] private Animation anim;

    private void Awake() {
        anim = GetComponent<Animation>();
    }

    public void FadeIn() {
        anim.clip = fadeIn;
        anim.Play();
    }

    public void FadeOut() {
        anim.clip = fadeOut;
        anim.Play();
    }

    public void Cover() {
        anim.clip = cover;
        anim.Play();
    }

    public void Uncover() {
        anim.clip = uncover;
        anim.Play();
    }
    
}
