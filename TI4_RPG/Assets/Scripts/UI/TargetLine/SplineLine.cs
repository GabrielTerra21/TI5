using UnityEngine;

public class SplineLine : MonoBehaviour {
    [Header("Line")] 
    
    [SerializeField] private float heightOffset;
    [SerializeField] private int verts;
    public GameObject source, target;
    private float step;
    private Vector3 keyPos;
    private Vector3[] positions;

    [Header("Components")]
    public LineRenderer line;


    private void Awake() {
        if (!line) line = GetComponent<LineRenderer>();
        step = (float) 1 / (verts - 1);// Calculate step between vertices
        positions = new Vector3[verts];
    }

    private void FixedUpdate() {
        SetLine();
    }

    public void Target(GameObject nTarget) {
        target = nTarget;
    }

    public void SetOrigin(GameObject origin) {
        source = origin;
    }
    
    private void SetLine() {
        GetPositions();
        line.positionCount = positions.Length;
        line.SetPositions(positions);
    }

    private void GetPositions() {
        //Get 3 key positions to guide the line
        keyPos = Vector3.Lerp(source.transform.position, target.transform.position, 0.5f); // middle point is halfway point between origin and target
        keyPos += Vector3.up * heightOffset;// set the middle point to be at the determined height offset from the other points

        for (int i = 0; i < verts; i++) {
            Vector3 a = Vector3.Lerp(source.transform.position, keyPos, step * i);
            Vector3 b = Vector3.Lerp(keyPos, target.transform.position, step * i);
            Vector3 c = Vector3.Lerp(a, b, step * i);
            positions[i] = c;
        }
    }
}
