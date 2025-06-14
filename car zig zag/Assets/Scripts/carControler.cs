using UnityEngine;

public class carControler : MonoBehaviour
{
    public float speed = 5f;
    public bool firstTapDone = false;
    bool faceLeft = true;

    public static carControler instance;  

    void Start()
    {
        instance = this;
        float yRot = transform.eulerAngles.y;
        faceLeft = Mathf.Approximately(yRot, 90f);
    }

    void Update()
    {
        if (GameManager.instance.isgameStarted)
        {
            checkInput();
            Move();
        }

        if (transform.position.y < -2f)
        {
            GameManager.instance.GameOver();
        }
    }

    void Move()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

    }

    void checkInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!firstTapDone)
            {
                firstTapDone = true;  // First tap is just to start the game, ignore it
                return;
            }

            changeDir();
        }
    }



    void changeDir()
    {
        if (faceLeft)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            faceLeft = false;
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
            faceLeft = true;
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
