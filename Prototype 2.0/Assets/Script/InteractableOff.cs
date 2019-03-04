using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableOff : MonoBehaviour {
    public Image[] InfoPanel;
    private LandMarkGenerator theLandMarkGenerator;
    private Button pauseBtn;
    public Button resetBtn;
    private Image InfoPanelON;

    private void Start()
    {
        theLandMarkGenerator = FindObjectOfType<LandMarkGenerator>();
        pauseBtn = GameObject.Find("Pause Button").GetComponent<Button>();
        InfoPanelON = InfoPanel[0];
    }

    private void Update()
    {
        if (InfoPanelON.enabled)
        {
            pauseBtn.enabled = false;
            resetBtn.enabled = false;
        }
        else
        {
            pauseBtn.enabled = true;
            resetBtn.enabled = true;
        }
    }


    public void InfoPanelKeluar(string namaLandmark, int urutanLandMark)
    {
        InfoPanelON = InfoPanel[urutanLandMark];
        Debug.Log("Coboa");
            InfoPanel[urutanLandMark].enabled = true; // Panelkeluar
        InfoPanel[urutanLandMark].gameObject.transform.GetChild(0).GetComponent<Text>().enabled = true;
        Time.timeScale = 0.0f;
        PlayerPrefs.SetInt(namaLandmark, 1); //Diset Sudah dilihat

    }

    public void setInfoPanelKeluar(Image infoPanelStateON)
    {
        InfoPanelON = infoPanelStateON;
    }

    public bool getInfoPanelStatus()
    {
        return InfoPanelON.enabled;
    }

}
