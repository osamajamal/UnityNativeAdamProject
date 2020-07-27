using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneCamera : MonoBehaviour
{
    // Start is called before the first frame update
    public bool CamAvailable;
    private WebCamTexture FrontCam;
    private Texture DeafultBackground;
    public RawImage Background;
    public AspectRatioFitter Fit;
    void Start()
    {
        DeafultBackground = Background.texture;
        WebCamDevice[] devices = WebCamTexture.devices;
        if(devices.Length == 0)
        {
            Debug.LogError("No Camera access");
            return;

        }

        for (int i = 0; i < devices.Length; i++)
        {
            if (devices[i].isFrontFacing)
            {
                FrontCam = new WebCamTexture(devices[i].name,Screen.width,Screen.height);
            }

                  
        }

        if(FrontCam == null)
        {
            Debug.LogError("Unable to find front camera");
        }
        FrontCam.Play();
        Background.texture = FrontCam;
        CamAvailable = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(CamAvailable == false)
        {
            WebCamDevice[] devices = WebCamTexture.devices;
            for (int i = 0; i < devices.Length; i++)
            {
                if (devices[i].isFrontFacing)
                {
                    FrontCam = new WebCamTexture(devices[i].name, Screen.width, Screen.height);
                }


            }

            if (FrontCam == null)
            {
                Debug.LogError("Unable to find front camera");
            }
            FrontCam.Play();
            Background.texture = FrontCam;
            CamAvailable = true;
            return;
        }

        float ratio = (float)FrontCam.width / (float)FrontCam.height;
        Fit.aspectRatio = ratio;
        float ScaleY = FrontCam.videoVerticallyMirrored ? -1f : 1f;
        Background.rectTransform.localScale = new Vector3(1f,ScaleY,1f);
        int orient = -FrontCam.videoRotationAngle;
        Background.rectTransform.localEulerAngles = new Vector3(0,0,orient);
    }
}
