using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testcamerafunctionnss : MonoBehaviour
{
    // Start is called before the first frame update
     
    void Start()
    {
        NativeCamera.CheckPermission();
        NativeCamera.TakePicture(PrintOnConsole);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static void PrintOnConsole(string prompt)
    {
        Debug.LogError(prompt);
    }
}
