using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pause_text : MonoBehaviour
{
    bool paused;
    // Start is called before the first frame update
    void Start()
    {
        paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {
                transform.position = new Vector3(8.0f, 12.0f, -2.8f);
                paused = true;
            }
            else
            {
                Debug.Log("Remove");
                transform.position = new Vector3(10.0f, 12.0f, -26.8f);
                paused = false;
            }
        }
    }
}
