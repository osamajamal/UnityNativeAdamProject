using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using VoxelBusters;
using VoxelBusters.NativePlugins;

public class Slide1Functions : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject PhoneCmaeraBackground;
    public Sounds TheSounds;
    public GameObject CameraRecordPanel;
    public GameObject SelectTakeVidepicPanel;
    public GameObject CalenderEventsPopup;
    public InputField CalenderEventName;
    public GameObject SharePanel;
    bool isSharing;
    public GameObject CalenderGameobject;
    public DatePicker TheDatePicker;
   bool IsSharingPanel = false;




    void Start()
    {
        if (!UnityEngine.Android.Permission.HasUserAuthorizedPermission("android.permission.READ_CALENDAR"))
        {
            UnityEngine.Android.Permission.RequestUserPermission("android.permission.READ_CALENDAR");
        }
    }

    // Update is called once per frame
    void Update()
    {
     
    }
    private void LateUpdate()
    {
        if (isSharing == true)
        {
            isSharing = false;

            StartCoroutine(CaptureScreenShoot());
        }
    }
    public void OnOpenPhoneCamera()
    {
        TheSounds.ButtonPlaysounds();
        PhoneCmaeraBackground.SetActive(true);
        CameraRecordPanel.SetActive(true);
       
    }

    public void CameraBackBtn()
    {
        
        CameraRecordPanel.SetActive(false);
        PhoneCmaeraBackground.SetActive(false);
    }
    public void SelectPictureVideoPanel()
    {
        TheSounds.ButtonPlaysounds();
        SelectTakeVidepicPanel.SetActive(true);
        CalenderEventsPopup.SetActive(false);
        CalenderGameobject.SetActive(false);
        SharePanel.SetActive(false);
    }

    public void TakePicture()
    {
        SelectTakeVidepicPanel.SetActive(false);
        NativeCamera.TakePicture(TakePictureResult);
    }

    static void TakePictureResult(string Imageurl)
    {
        Debug.LogError(Imageurl);
    }

    public void VideoRecord()
    {
        SelectTakeVidepicPanel.SetActive(false);
        NativeCamera.RecordVideo(VideoRecordResult);
    }

    static void VideoRecordResult(string VideoRecordurl)
    {
        Debug.LogError(VideoRecordurl);
    }
    public void CalnedereventsPanel()
    {
        TheSounds.ButtonPlaysounds();
        CalenderEventsPopup.SetActive(true);
        SelectTakeVidepicPanel.SetActive(false);
        CalenderGameobject.SetActive(false);
        SharePanel.SetActive(false);
    }
    public void GetCalenderEventsName() //Inputfeild name
    {
        
        //Debug.LogError(CalenderEventName.text);
    }
    public void AddEventsCalender()
    {

        
        //Debug.LogError(TheDatePicker.Selecttime);
       // Debug.LogError(TheDatePicker.Endtime);
        CalendarEvent.AddEvent(CalenderEventName.text, TheDatePicker.Selecttime, TheDatePicker.Endtime, true);
        
        CalenderEventsPopup.SetActive(false);
        CalenderEventName.text = "";
    }
    public void OpenCalenderUI()
    {
        CalenderGameobject.SetActive(true);
       
    }

    public void OpenSharePanel()
    {
        if (IsSharingPanel == false)
        {
            IsSharingPanel = true;
            TheSounds.ButtonPlaysounds();
            SharePanel.SetActive(true);
            SelectTakeVidepicPanel.SetActive(false);
            CalenderEventsPopup.SetActive(false);
            CalenderGameobject.SetActive(false);
            
        }
        else
        {
            IsSharingPanel = false;
            SharePanel.SetActive(false);
        }
       
    }
    public void ClosedSharePanel()
    {
        SharePanel.SetActive(false);
    }
    public void ShareSocialMedia()
    {
       
        isSharing = true;
        SharePanel.SetActive(false);
    }

    IEnumerator CaptureScreenShoot()
    {
        yield return new WaitForEndOfFrame();

        Texture2D texture = ScreenCapture.CaptureScreenshotAsTexture();

        ShareSheet(texture);

       UnityEngine.Object.Destroy(texture);

    }

    private void ShareSheet(Texture2D texture)
    {
        ShareSheet _shareSheet = new ShareSheet();

        _shareSheet.Text = "This is my picture";
        _shareSheet.AttachImage(texture);
        //_shareSheet.URL = "https://twitter.com/RoixoGames";


        NPBinding.Sharing.ShowView(_shareSheet, FinishSharing);
    }

    private void FinishSharing(eShareResult _result)
    {
        Debug.Log(_result);
    }


}
