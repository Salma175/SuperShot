using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PitchRegionSelectionController : MonoBehaviour
{
    private Action<int> selectedRegionCallback;

    public void EnableGrid(Action<int> callback) {
        selectedRegionCallback = callback;
    }

    public void OnClickOfGrid(int gridIId) 
    {
        selectedRegionCallback(gridIId);
    }
}
