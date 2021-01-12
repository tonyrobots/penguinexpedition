using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Zoomable : MonoBehaviour, IPointerClickHandler
{

    bool mainCamActive = true;
    public GameObject zoomCam;
    Camera mainCam;

    // Start is called before the first frame update
    void Start()
    {

        mainCam = Camera.main;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!mainCamActive) 
        {
            if (Input.GetMouseButton(0) || Input.GetKeyDown("escape"))
            {
                SwapCameras();
            }
        }
        
    }

    public void OnPointerClick(PointerEventData e)
    {
        SwapCameras();
    }

    void SwapCameras()
    {
        mainCamActive = !mainCamActive;
        mainCam.enabled = mainCamActive;
        zoomCam.SetActive(!mainCamActive);
        Debug.Log("mousedown on image");
    }
}
