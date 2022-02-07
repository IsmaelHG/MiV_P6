using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.PostProcessing;



public class uicontroller : MonoBehaviour {

    ///
    /// UI controller 
    /// 

    public PostProcessProfile PP;
    public Button MenuButtton;
    public Button ExitMenuButtton;
    bool change;

    private void Start()
    {
        SetBlur(false);
        LockCursor();
        
    }
    // Use this for initialization
    public void SetTimeScale (int val) {
        Time.timeScale = val;

    }

    public void SetBlur(bool val)
    {
       
        //PP.depthOfField.enabled= val;
    }

  

    private void Update()
    {

       

        if (Input.GetKeyDown(KeyCode.P))
        {
         
            if (!change)
            {
                MenuButtton.onClick.Invoke();
               // Cursor.visible = true;
               Cursor.lockState= CursorLockMode.None;
                Cursor.lockState = CursorLockMode.Confined;
                change = !change;
               
                
              
            }
            else
            {

                
                ExitMenuButtton.onClick.Invoke();
                change = !change;
                
                LockCursor();

            }
        }
    }

    private void OnDisable()
    {
       SetBlur( false);
    }

   

    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
      
       
    }



    

}
