using UnityEngine;

public class RotateCube : MonoBehaviour
{
    public bool active;
    public float interpTime;
    public float snapAmount;

    private MouseState clickState;
    private Vector3 rotateTo;

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        rotateTo = transform.eulerAngles;
    }

    private void Awake()
    {
        active = true;
    }

    private void Update()
    {
        if (!active) return;
        if (Input.GetMouseButtonDown(0) && (clickState == MouseState.NONE || clickState == MouseState.WAIT)) { clickState = MouseState.CLICK; }
        else if (clickState == MouseState.CLICK && Input.GetMouseButtonDown(0)) clickState = MouseState.DRAG;
        else if (clickState == MouseState.DRAG) {
            if (!Input.GetMouseButtonDown(0)) { clickState = MouseState.NONE;}
        }
        DragCube();
    }

    private void DragCube() {
        if (clickState == MouseState.DRAG) rotateTo = new Vector3(transform.eulerAngles.x + Input.GetAxis("Mouse Y"), transform.eulerAngles.y + Input.GetAxis("Mouse X"), transform.eulerAngles.z);
        if (Mathf.Abs(transform.eulerAngles.x - rotateTo.x) < snapAmount && Mathf.Abs(transform.eulerAngles.y - rotateTo.y) < snapAmount) {
            clickState = MouseState.WAIT;
            return;
        }
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, rotateTo, interpTime);
    }
}
