using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DelivaryTypeDisplayController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI titleText;
    [SerializeField]
    private TextMeshProUGUI dellivaryTypeText;
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private TextMeshProUGUI ballsText;

    private Action displayCompletionCallback;

    public void EnableDelivaryDisplay(Action callback, Constants.BowlerType bowlerType, int grid)
    {
        Reset();

        displayCompletionCallback = callback;
        string message = String.Format("{0} BALL {1}", bowlerType, grid);
        dellivaryTypeText.SetText(message);

        titleText.text = Constants.DELIVARY_TYPE;
        
        //StartCoroutine(DisplayScreen());
    }

    public void EnableScoreDisaplay(Action callback, string result, int score, int wickets, int ballsRemaining)
    {
        Reset();

        displayCompletionCallback = callback;
        dellivaryTypeText.SetText(result);

        scoreText.text = String.Format("Score : {0}/{1}", score, wickets);
        ballsText.text = String.Format("Balls Left: {0}", ballsRemaining);

        scoreText.gameObject.SetActive(true);
        ballsText.gameObject.SetActive(true);

        titleText.text = "";

        //StartCoroutine(DisplayScreen());
    }

    public void OnClickOfOK() {
        Reset();
        displayCompletionCallback();
    }

   /* IEnumerator DisplayScreen()
    {
        yield return new WaitForSeconds(3f);
        
    }
   */

    private void Reset()
    {
        scoreText.gameObject.SetActive(false);
        ballsText.gameObject.SetActive(false);
        titleText.text = Constants.DELIVARY_TYPE;
    }
}
