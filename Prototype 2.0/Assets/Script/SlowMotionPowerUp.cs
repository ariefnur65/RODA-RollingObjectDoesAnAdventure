using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotionPowerUp : MonoBehaviour {

    #region slowmotion func variable
    public bool slowMotion;
    #endregion



    private void Start()
    {
        slowMotion = false;
    }

    #region Slow Motion Effect
    //Method penentu aktif tidaknya efek slow motion
    public void setSlowMo(bool Status)
    {
        slowMotion = Status;
        if (slowMotion == true)
        {
            if (Time.timeScale == 1.0f)
            {
                Time.timeScale = 0.2f;
            }
        }
        else
        {
            Time.timeScale = 1.0f;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }
    }
    #endregion
}
