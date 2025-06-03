using UnityEngine;

public class carControler : MonoBehaviour
{

    public float speed = 5f;
    bool faceLeft, firstTab;


    // Update is called once per frame
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

    void checkInput() { 
    if (Input.GetMouseButtonDown(0))
        {
            changeDir();
        }
    }

    void changeDir()
    {
        if (faceLeft)
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
            faceLeft = false;
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            faceLeft = true;
        }
    }


}