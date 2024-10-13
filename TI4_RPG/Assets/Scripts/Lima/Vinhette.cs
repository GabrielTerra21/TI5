using UnityEngine;
using UnityEngine.Serialization;

public class Vinhette : MonoBehaviour {
    [SerializeField] private AnimationClip fadeIn, fadeOut, cover, uncover;
    [FormerlySerializedAs("animation")] [SerializeField] private Animation anim;

    private void Awake() {
        anim = GetComponent<Animation>();
    }

    public float FadeIn() {
        anim.clip = fadeIn;
        anim.Play();
        return fadeIn.length;
    }

    public float FadeOut() {
        anim.clip = fadeOut;
        anim.Play();
        return fadeOut.length;
    }

    public float Cover() {
        anim.clip = cover;
        anim.Play();
        return cover.length;
    }

    public float Uncover() {
        anim.clip = uncover;
        anim.Play();
        return uncover.length;
    }
    
}
