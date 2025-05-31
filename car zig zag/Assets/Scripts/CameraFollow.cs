using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    public Transform target; // The target to follow
    Vector3 distance;
    public float followspeed; // Speed of the camera following
    void Start()
    {
        distance = target.position - transform.position; // Calculate the initial distance from the target
    }

    // Update is called once per frame
    void Update()
    {
        if (target.position.y >-1f)
        {
            follow();
        }
       
    }

    void follow()
    {
        Vector3 currentpos = transform.position; 
        Vector3 targetpos = target.position - distance; 
        Vector3 newpos = Vector3.Lerp(currentpos, targetpos, followspeed * Time.deltaTime);

        transform.position = newpos; 
    }
}
