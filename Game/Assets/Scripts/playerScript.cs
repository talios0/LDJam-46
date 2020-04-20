using UnityEngine;

public class playerScript: MonoBehaviour
{
    public LevelManager levelManager;
    private bool lose;

    private static bool slowUnlock = false;
    private static float gravity;

    public bool ballDropped;

    private void Start()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        gravity = Physics.gravity.y;
        if (levelManager.GetLevel() + 1 >= 5)
        {
            slowUnlock = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Laser")
        {
            // You Lose
            lose = levelManager.GameLoss();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (lose) return;
        if (collision.gameObject.tag == "WinPlat")
        {
            // You Win
            levelManager.LevelCompleteStart();
            if (levelManager.GetLevel() + 1 >= 5) {
                slowUnlock = true;
            }
        }
    }

    private void Update()
    {
        if (!slowUnlock) return;
        if (!ballDropped) return;
        if (Input.GetAxisRaw("Slow") != 0)
        {
            GetComponent<Rigidbody>().isKinematic = true;
        }
        else {
            if (GetComponent<Rigidbody>().isKinematic)
            {
                GetComponent<Rigidbody>().isKinematic = false;
                GetComponent<Rigidbody>().detectCollisions = true;
            }
        }
    }


    public void SetUnlock(bool state) {
        slowUnlock = state;
    }
}
