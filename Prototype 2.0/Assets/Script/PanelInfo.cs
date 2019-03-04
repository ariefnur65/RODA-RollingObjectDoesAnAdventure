using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelInfo : MonoBehaviour {
    
    public void HidePanelInfo()
    {
        Time.timeScale = 1.0f;
        gameObject.GetComponent<Image>().enabled = false;
      gameObject.transform.GetChild(0).GetComponent<Text>().enabled = false;
    }
}
