using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class KeyMIrror : MonoBehaviour {

    public Text TextOri;
    public Text TextDest;

    private void Start()
    {
        TextDest.text = TextOri.text;
    }

    private void OnEnable()
    {
        TextDest.text = TextOri.text;
    }
}
