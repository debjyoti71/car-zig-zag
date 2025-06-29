using UnityEngine;

public class carControler : MonoBehaviour
{
    [Header("Car Settings")]
    public float speed = 5f;
    public bool isAutoMode = false;
    public bool firstTapDone = false;

    public static carControler instance;

    bool isMoving = false;
    int currentIndex = 0;
    Vector3 targetPosition;
    bool reachedEnd = false;

    void Start()
    {
        instance = this;

        // Round initial Y rotation to determine facing direction
        float yRot = transform.eulerAngles.y;
    }

    void Update()
    {
        if (GameManager.instance.isgameStarted)
        {
            if (isAutoMode)
            {
                if (!reachedEnd)
                    AutoFollowPath();
            }
            else
            {
                checkInput();
            }

            Move();
        }

        if (transform.position.y < -2f)
        {
            GameManager.instance.GameOver();
        }
    }

    void checkInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!firstTapDone)
            {
                firstTapDone = true;
                return;
            }

            changeDir();
        }
    }

    void changeDir()
    {
        transform.Rotate(0, 90, 0); // turn 90° right
    }

    void AutoFollowPath()
    {
        var path = PlatformSpawner.instance.platformLoc;

        if (!isMoving && path.ContainsKey(currentIndex))
        {
            targetPosition = path[currentIndex];
            Vector3 direction = (targetPosition - transform.position).normalized;

            // Rotate towards direction
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.z))
                transform.rotation = Quaternion.Euler(0, direction.x > 0 ? 90 : -90, 0);
            else
                transform.rotation = Quaternion.Euler(0, direction.z > 0 ? 0 : 180, 0);

            isMoving = true;
        }

        if (isMoving)
        {
            float dist = Vector3.Distance(transform.position, targetPosition);
            if (dist < 0.1f)
            {
                transform.position = targetPosition; // snap to center
                currentIndex++;
                isMoving = false;

                if (!path.ContainsKey(currentIndex))
                {
                    reachedEnd = true;
                    Debug.Log("Auto Path Complete!");
                }
            }
        }
    }

    void Move()
    {
        if (isAutoMode && isMoving)
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        else if (!isAutoMode)
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("diamond"))
        {
            Destroy(collision.gameObject);
            GameManager.instance.AddDiamond();
        }

        if (collision.gameObject.CompareTag("star"))
        {
            Destroy(collision.gameObject);
            GameManager.instance.AddStar();
        }
    }
}
