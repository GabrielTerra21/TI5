using System.Collections;
using Cinemachine;
using UnityEngine;

public class LimaCameraBehaviour : MonoBehaviour {
    [SerializeField] private CinemachineVirtualCamera cam;
    [SerializeField] private float defaultSize;
    [SerializeField] private float zoomedSize;
    [SerializeField] private float duration;

    private void Start() {
        Debug.Log(GameObject.FindWithTag("Player"));
        cam = GetComponent<CinemachineVirtualCamera>();
        defaultSize = cam.m_Lens.OrthographicSize;
        GameManager.Instance.enterCombat.AddListener(ZoomIn);
        GameManager.Instance.enterExploration.AddListener(ZoomOut);
    }

    private void ZoomIn() {
        StartCoroutine(Zoom(defaultSize, zoomedSize));
    }

    private void ZoomOut() {
        StartCoroutine(Zoom(zoomedSize, defaultSize));
    }

    IEnumerator Zoom(float oSize, float fSize) {
        float val;
        float interpolator = 0;
        while (interpolator < 1) {
            val = Mathf.Lerp(oSize, fSize, interpolator);
            cam.m_Lens.OrthographicSize = val;
            interpolator += Time.fixedDeltaTime / duration;
            yield return null;
        }
        cam.m_Lens.OrthographicSize = fSize;
    }
}
