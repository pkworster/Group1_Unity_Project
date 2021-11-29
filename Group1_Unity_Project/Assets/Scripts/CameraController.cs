using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject playerObj;
    private Vector3 offset = new Vector3(0, 0, -15);

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
