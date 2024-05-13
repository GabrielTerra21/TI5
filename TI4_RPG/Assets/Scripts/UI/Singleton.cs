using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T: MonoBehaviour {
    public static T Insatance;

    private void Awake() {
        if (Insatance != null) {
            Destroy(gameObject);
        }
        Insatance = this as T;
        DontDestroyOnLoad(gameObject);
    }
}
