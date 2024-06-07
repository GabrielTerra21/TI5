using UnityEngine;

public class SplineLine : MonoBehaviour {
    [Header("Line")]
    public GameObject source, target;

    [Header("Components")]
    public LineRenderer line;

    [Header("data")] 
    public float heightOffset;

    public int halfSegments, verts;


    private void Awake() {
        if (!line) line = GetComponent<LineRenderer>();
    }

    private void FixedUpdate() {
        SetLine();
    }

    private void SetLine() {
        Debug.Log("uai");
        Vector3[] spline = GetPositions(verts);
        line.positionCount = spline.Length;
        line.SetPositions(spline);
    }

    private Vector3[] GetPositions(int segments) {
        //Get 3 key positions to guide the line
        Vector3[] keyPos = new Vector3[3];
        for (int i = 0; i < keyPos.Length; i++) {
            keyPos[i] = Vector3.Lerp(source.transform.position, target.transform.position, 0.5f * i);
        }
        keyPos[1] += Vector3.up * heightOffset;// set the middle point to be at the determined height offset from the other points

        float step = (float) 1 / segments;// Calculate step between vertices
        Vector3[] pos = new Vector3[segments];

        for (int i = 0; i < segments; i++) {
            Vector3 a = Vector3.Lerp(keyPos[0], keyPos[1], step * i);
            Vector3 b = Vector3.Lerp(keyPos[1], keyPos[2], step * i);
            Vector3 c = Vector3.Lerp(a, b, step * i);
            pos[i] = c;
        }

        // Vector3[] pos = new Vector3[ (halfSegments * 2)];
        // // Calculate positions for the first half of the spline
        // for (int i = 0; i < halfSegments; i++) {
        //     pos[i] = Vector3.Lerp(keyPos[0], keyPos[1], step * i);
        // }
        // // Calculate positions for the second half of the spline
        // for (int i = 1; i < halfSegments; i++) {
        //     pos[i + halfSegments] = Vector3.Lerp(keyPos[1], keyPos[2], step * i);
        // }

        return pos;
    }
}
