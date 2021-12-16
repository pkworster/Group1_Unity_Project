using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject playerObj;
    public Vector3 offset = new Vector3(10.0f, 8f, -16.0f);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerObj == null)
            return;
        // should remove error when player gets killed - 11/29 Peter Worster
        transform.position = playerObj.transform.position + offset;
    }
}
