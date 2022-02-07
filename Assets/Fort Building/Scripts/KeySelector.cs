using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class KeySelector : MonoBehaviour {

    /// 
    /// UI to set your favorite  keys for building
    /// 
    /// 

    #region variables

    [Header("Default Inputs")] // customizable keys for building and disable the building system 
    public KeyCode StairsKey = KeyCode.Alpha1; 
    public KeyCode FloorKey = KeyCode.Alpha4;
    public KeyCode WallKey = KeyCode.Alpha3;
    public KeyCode BuildToolKey = KeyCode.Alpha2;
    public KeyCode RotateKey = KeyCode.R;


    private int Intbuild;
    public bool cansetbuild=false;
    public Text []keysText;
   
    public Slider [] SensitivemousSliders;
    private vThirdPersonCamera ThirdPCamera;
    Event e;

    #endregion


   void Start()
    {
        ThirdPCamera = FindObjectOfType<vThirdPersonCamera>();
        //SET the saved keybinds
        if (PlayerPrefs.HasKey("StairsKey"))
        {
            StairsKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("StairsKey"));
            print(" StairsKey loaded  " + StairsKey.ToString());
        }
        if (PlayerPrefs.HasKey("FloorKey"))
        {
            FloorKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("FloorKey"));
            print(" FloorKey loaded  " + FloorKey.ToString());
        }
        if (PlayerPrefs.HasKey("WallKey"))
        {
            WallKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("WallKey"));
            print(" WallKey loaded  " + WallKey.ToString());
        }
        if (PlayerPrefs.HasKey(" RotateKey"))
        {
            RotateKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("RotateKey"));
            print("  RotateKey loaded  " + RotateKey.ToString());
        }

       

        keysText[0].text = StairsKey.ToString();
        keysText[1].text = FloorKey.ToString();
        keysText[2].text = WallKey.ToString();
        // keysText[3].text = RotateKey.ToString();


    }

   

    public void Sensitivemouse()
    {

        ThirdPCamera.xMouseSensitivity = SensitivemousSliders[0].value;
      ThirdPCamera.yMouseSensitivity = SensitivemousSliders[1].value;

    }


    private void OnDisable()
    {

        if (keysText[3]!=null)
        {
            keysText[3].gameObject.SetActive(false);

        }

    }

    void OnGUI()
    {
        if (cansetbuild)
        {
            e = Event.current;
            if (e.isKey)
            {
                

                if (Intbuild == 0)//STAIRS
                {
                    Debug.Log("Detected key code: " + e.keyCode);
                    StairsKey = e.keyCode;
                    keysText[0].text = e.keyCode.ToString();
                    PlayerPrefs.SetString("StairsKey", e.keyCode.ToString());//Save the pref
                    cansetbuild = false;
                    this.gameObject.SetActive(false);


                }
                else if (Intbuild == 1)//FLOOR
                {
                    Debug.Log("Detected key code: " + e.keyCode);
                    FloorKey = e.keyCode;
                    keysText[1].text = e.keyCode.ToString();
                    PlayerPrefs.SetString("FloorKey", e.keyCode.ToString());//Save the pref
                    cansetbuild = false;
                   
                    this.gameObject.SetActive(false);


                }
                else if (Intbuild == 2)//WALL
                {
                    Debug.Log("Detected key code: " + e.keyCode);
                    WallKey = e.keyCode;
                    PlayerPrefs.SetString("WallKey", e.keyCode.ToString());//Save the pref
                    cansetbuild = false;
                    keysText[2].text = e.keyCode.ToString();

                    this.gameObject.SetActive(false);


                }
            }
        }
    }

    public void SetBuildInt(int build)
    {
        Intbuild = build;
        cansetbuild = true;
        Time.timeScale = 0;
        this.gameObject.SetActive(true);
        keysText[3].gameObject.SetActive(true);

    }

}
