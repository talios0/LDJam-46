using UnityEngine;

public class RotateCube : MonoBehaviour
{
    public bool active;
    public float interpTime;
    public float snapAmount;
    public float sensitivity;

    private MouseState clickState;
    private Vector3 rotateTo;
    private Vector3 previousRotation;

    private void Start() {
        Cursor.lockState = CursorLockMode.Confined;
        rotateTo = transform.eulerAngles;
    }

    private void Awake()
    {
        active = true;
    }

    private void Update()
    {
        if (LevelManager.disableRotation) return;
        if (!active) return;
        if (Input.GetMouseButtonDown(0) && (clickState == MouseState.NONE || clickState == MouseState.WAIT)) { clickState = MouseState.CLICK; }
        else if (clickState == MouseState.CLICK && Input.GetMouseButton(0)) { clickState = MouseState.DRAG; }
        else if (clickState == MouseState.DRAG)
        {
            if (!Input.GetMouseButton(0)) { clickState = MouseState.NONE; }
        }
        DragCube();
    }

    private void DragCube() {
        if (clickState == MouseState.DRAG)
        {
            rotateTo = new Vector3(transform.eulerAngles.x + Input.GetAxis("Mouse Y") * -sensitivity, transform.eulerAngles.y + Input.GetAxis("Mouse X") * -sensitivity, transform.eulerAngles.z);
            if (rotateTo.x < 175 && rotateTo.x > 85) rotateTo.x = 85;
            else if (rotateTo.x > 185 && rotateTo.x < 275) rotateTo.x = 275;
        }
        else if (clickState == MouseState.WAIT) return;
        else if (clickState == MouseState.NONE && Mathf.Abs(transform.eulerAngles.x - rotateTo.x) < snapAmount && Mathf.Abs(transform.eulerAngles.y - rotateTo.y) < snapAmount) 
        {
            clickState = MouseState.WAIT;
            transform.eulerAngles = rotateTo;
        }
        previousRotation = transform.eulerAngles;
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, rotateTo, interpTime);
    }
}
