using UnityEngine;
using System.IO.Ports;


public class ArduinoButton : MonoBehaviour
{
    SerialPort sp = new SerialPort("COM3", 9600);  // Change COM port as needed

    bool moveForward = false;

    void Start()
    {
        try
        {
            sp.Open();
            sp.ReadTimeout = 50;
        }
        catch (System.Exception e)
        {
            Debug.Log("Serial Port Error: " + e.Message);
        }
    }

    void Update()
    {
        // Check for button press or release from Arduino
        if (sp.IsOpen)
        {
            try
            {
                string value = sp.ReadLine();
                if (value.Contains("PRESSED"))
                {
                    moveForward = true;
                }
                else if (value.Contains("RELEASED"))
                {
                    moveForward = false;
                }
            }
            catch (System.Exception) { }
        }

        // Move the sphere forward if button is pressed
        if (moveForward)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 2f);  // Move on Z-axis
        }
    }

    void OnApplicationQuit()
    {
        if (sp.IsOpen)
        {
            sp.Close();
        }
    }
}
