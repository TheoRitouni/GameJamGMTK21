using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchControls : MonoBehaviour
{
    private Toggle toggleControls;

    private void Start()
    {
        toggleControls = GetComponent<Toggle>();
        toggleControls.isOn = GlobalValues.switchControl;
    }

    public void ChangeValue(bool pValue)
    {
        GlobalValues.switchControl = pValue;
    }
}
