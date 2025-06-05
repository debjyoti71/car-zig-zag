using UnityEngine;

public class platfom : MonoBehaviour
{

    public GameObject Star;
    public GameObject Diamond;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Vector3 tempPos = transform.position;
        tempPos.y += 1.2f;
        int randomValue = Random.Range(0, 21);
        if (randomValue < 4)
        {
            Instantiate(Star, tempPos, Star.transform.rotation);
        }
        else if (randomValue == 7)
        {
            Instantiate(Diamond, tempPos, Star.transform.rotation);
        }


    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {   
            Invoke("FallDown", 1f);  
        }

        
    }

   

    void FallDown()
    {
        GetComponent<Rigidbody>().isKinematic = false;
        Destroy(gameObject, 0.6f);
    }

}
