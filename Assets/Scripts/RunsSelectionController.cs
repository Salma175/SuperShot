using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunsSelectionController : MonoBehaviour
{
    private Action<int> selectedRunCallback;

    public void EnableRunsSelection(Action<int> callback)
    {
        selectedRunCallback = callback;
    }

    public void OnClickOfRun(int runs)
    {
        selectedRunCallback(runs);
    }
}
