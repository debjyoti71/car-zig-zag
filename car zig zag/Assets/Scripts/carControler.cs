using UnityEngine;

public class carControler : MonoBehaviour
{

    public float speed = 5f;
    bool faceLeft, firstTab;


    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.position += transform.forward*speed*Time.deltaTime;
    }
}
