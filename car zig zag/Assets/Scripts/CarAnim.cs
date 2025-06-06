using UnityEngine;
using UnityEngine.UI; // Required for Button class

public class CarAnim : MonoBehaviour
{
    [SerializeField] Vector3 FinalPos;
    Vector3 initialPos;

    private void Awake()
    {
        initialPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(initialPos, FinalPos, 0.2f);
        transform.Rotate(new Vector3(0, 10f, 0) * Time.deltaTime);
    }

    private void OnDisable()
    {
        // Reset position when the script is disabled
        transform.position = initialPos;
        transform.rotation = Quaternion.Euler(0f, 180f, 0f); // Reset rotation to initial state
    }
}
