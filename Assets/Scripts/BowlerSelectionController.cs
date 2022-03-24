using System;
using UnityEngine;

public class BowlerSelectionController : MonoBehaviour
{

    private Constants.BowlerType selectedBowler;
    private Action<Constants.BowlerType> selectionCompletionCallBack;

    public void EnableBowlerSelection(Action<Constants.BowlerType> callBack) 
    {
        selectionCompletionCallBack = callBack;
    }
    public void OnClickOfBowlerType(string bowlerType) 
    {
        setBowlerType(bowlerType);
        selectionCompletionCallBack(selectedBowler);
    }

    private void setBowlerType(string bowlerType) 
    { 
        selectedBowler = bowlerType.ToEnum<Constants.BowlerType>(); 
    }
    public Constants.BowlerType getBowlerType() 
    {
        return selectedBowler;
    }
}
