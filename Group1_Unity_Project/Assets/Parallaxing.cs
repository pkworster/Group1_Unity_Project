using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour
{
    public Transform[] backgrounds;         // background list for effect
    private float[] parallaxScales;         // amount of effect
    public float smoothing;                 // hopefully smooths the effect... set above 0... I think

    private Transform cam;                  // ref for the main camera
    private Vector3 previousCamPos;         // stores prev fram cam pos

    // Before start
    private void Awake()
    {
        cam = Camera.main.transform;
    }
    


    // Start is called before the first frame update
    void Start()
    {
        previousCamPos = cam.position;
        parallaxScales = new float [backgrounds.Length];
        for(int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z * -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];
            float backgroundTargetPosX = backgrounds[i].position.x + parallax;
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }
        previousCamPos = cam.position;
    }
}
// second attempt at parallax - Peter Worster
