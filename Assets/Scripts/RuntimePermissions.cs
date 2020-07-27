using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class RuntimePermissions : MonoBehaviour
{
    private const string CalendarPermission = "android.permission.WRITE_CALENDAR";
    public Text acess;
    // public Button BtnGrant, BtnProceed;

    // Start is called before the first frame update
    private void Awake()
    {
#if PLATFORM_ANDROID
        Permission.RequestUserPermission(CalendarPermission);
        StartCoroutine(WaitForPermissions());
#else
                   
#endif
        //});
        if (Permission.HasUserAuthorizedPermission(CalendarPermission))
        {
            acess.text = "acccess allowed";
            // ProceedToFirstScreen();
        }

    }
    void Start()
    {


       // BtnGrant.interactable = true;
     //   BtnProceed.interactable = false;

       // BtnGrant.onClick.RemoveAllListeners();
       // BtnGrant.onClick.AddListener(() =>
       // {

    //    });


        //BtnProceed.onClick.RemoveAllListeners();
        //BtnProceed.onClick.AddListener(() =>
        //{
        //    ProceedToFirstScreen();
        //});
    }

    private void ProceedToFirstScreen()
    {
        this.gameObject.SetActive(false);
        //TODO: Launch something else
    }

    IEnumerator WaitForPermissions()
    {
        yield return new WaitForSeconds(5);
      //  BtnGrant.interactable = true;
        if (Permission.HasUserAuthorizedPermission(CalendarPermission))
        {
            acess.text = "acccess allowed";
        }
        else
        {
#if PLATFORM_ANDROID
            Permission.RequestUserPermission(CalendarPermission);
            StartCoroutine(WaitForPermissions());
#else
                   
#endif
        }
    }

}
