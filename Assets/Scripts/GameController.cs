using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    #region References
    public GameObject homeScreenGO;
    public GameObject bowlerSelectionGO;
    public GameObject pitchRegionSelectionGO;
    public GameObject delivaryTypeDisplayGO;
    public GameObject runsSelectionGO;
    public GameObject endScreenGO;

    [SerializeField]
    private TextMeshProUGUI resultText;
    [SerializeField]
    private GameObject mainCanvasGO;

    private BowlerSelectionController bowlerSelectionController;
    private PitchRegionSelectionController pitchRegionSelectionController;
    private DelivaryTypeDisplayController delivaryTypeDisplayController;
    private RunsSelectionController runsSelectionController;

    [SerializeField]
    private BallDelivaryController ballDelivaryController;
    #endregion

    #region Variables
    private Constants.BowlerType selectedBowlerType;
    private int selectedRegion;
    private int selectedRuns;
    private int result;
    private int score = 0;
    private int wickets = 0;
    private int ballsRemianing = Constants.TOTAL_BALLS;

    private List<ItemAndWeights> scoreItems = new List<ItemAndWeights>();
    private List<string> HitMissCases = new List<string>() { Constants.OUT, Constants.MISSED};
    #endregion

    private void Start()
    {
        reset();
        init();
        EnableHomeScreen();
    }

    #region INITIALIZATION
    private void init()
    {
        #region SCORE ITEMS
        scoreItems.Add(new ItemAndWeights(0,90));
        scoreItems.Add(new ItemAndWeights(1, 85));
        scoreItems.Add(new ItemAndWeights(2, 60));
        scoreItems.Add(new ItemAndWeights(4, 35));
        scoreItems.Add(new ItemAndWeights(6, 20));
        #endregion

        #region Controllers
        bowlerSelectionController = bowlerSelectionGO.GetComponent<BowlerSelectionController>();
        pitchRegionSelectionController = pitchRegionSelectionGO.GetComponent<PitchRegionSelectionController>();
        delivaryTypeDisplayController = delivaryTypeDisplayGO.GetComponent<DelivaryTypeDisplayController>();
        runsSelectionController = runsSelectionGO.GetComponent<RunsSelectionController>();
        #endregion
    }
    #endregion

    #region ON CLICK EVENTS 
    public void OnClickOfStart()
    {
        DisableHomeScreen();
        EnableBowlerSelection();
    }

    public void OnClickOfPlayAgain()
    {
        DisableEndScreen();
        reset();
        EnableBowlerSelection();
    }

    public void OnClickOfHome()
    {
        DisableEndScreen();
        reset();
        EnableHomeScreen();
    }
    #endregion

    #region CALL BACKS
    private void setSelectedBowler(Constants.BowlerType bowlerType)
    {
        selectedBowlerType = bowlerType;
        DisableBowlerSelection();
        EnablePitchSelection();
    }
    private void setSelectedPitchRegion(int regionNumber)
    {
        selectedRegion = regionNumber;
        DisablePitchSelection();
        EnableDelivaryTypeDisplay();
    }
    #endregion

    #region ENABLE HOME SCREEN
    private void EnableHomeScreen() 
    {
        homeScreenGO.SetActive(true);
    }
    private void DisableHomeScreen() 
    {
        homeScreenGO.SetActive(false);
    }
    #endregion

    #region ENABLE OR DISABLE BOWLER SELECTION
    private void EnableBowlerSelection() 
    {
        bowlerSelectionGO.SetActive(true);
        bowlerSelectionController.EnableBowlerSelection(setSelectedBowler);
    }
    private void DisableBowlerSelection()
    {
        bowlerSelectionGO.SetActive(false);
    }
    #endregion

    #region ENABLE OR DISABLE PITCH REGION SELECTION
    private void EnablePitchSelection()
    {
        pitchRegionSelectionGO.SetActive(true);
        pitchRegionSelectionController.EnableGrid(setSelectedPitchRegion);
    }
    private void DisablePitchSelection()
    {
        pitchRegionSelectionGO.SetActive(false);
    }
    #endregion

    #region ENABLE OR DISABLE DELIVARY TYPE
    private void EnableDelivaryTypeDisplay()
    {
        delivaryTypeDisplayGO.SetActive(true);
        delivaryTypeDisplayController.EnableDelivaryDisplay(DisableDelivaryTypeDisplay, selectedBowlerType,selectedRegion);
    }

    private void DisableDelivaryTypeDisplay()
    {
        delivaryTypeDisplayGO.SetActive(false);
        EnableRunsSelection();
    }
    #endregion

    #region ENABLE OR DISABLE RUNS SELECTION
    private void EnableRunsSelection()
    {
        runsSelectionGO.SetActive(true);
        runsSelectionController.EnableRunsSelection(DisableRunsSelection);
    }

    private void DisableRunsSelection(int runs)
    {
        selectedRuns = runs;
        runsSelectionGO.SetActive(false);
        DeliverBall();
    }
    #endregion

    #region ENABLE OR DISABLE END SCREEN
    private void EnableEndScreen(string message) 
    {
        resultText.text = message;
        endScreenGO.SetActive(true);
    }

    private void DisableEndScreen() {
        endScreenGO.SetActive(false);
    }
    #endregion

    #region RESULT
    private void EnableResult()
    {
        result = Utilities.RandomElementByWeight(scoreItems);
        delivaryTypeDisplayGO.SetActive(true);

        string message = "";
        if (result == selectedRuns)
        {
            message = selectedRuns.ToString();
            score += selectedRuns;
        }
        else {
            int numberofCases = HitMissCases.Count;
            int randomNumber = Random.Range(0, numberofCases);
            message = HitMissCases[randomNumber];
            if (message == Constants.OUT) wickets++;
        }

        ballsRemianing--;

        delivaryTypeDisplayController.EnableScoreDisaplay(DisableResult, message, score,wickets, ballsRemianing);
    }

    private void DisableResult()
    {
        delivaryTypeDisplayGO.SetActive(false);

        //Check if target reached
        if (score >= Constants.TARGET_SCORE)
        {
            EnableEndScreen(Constants.BATSMAN_WIN);
        }
        else
        {
            if (ballsRemianing != 0 && wickets< Constants.WICKETS)
                EnableRunsSelection();
            else
                EnableEndScreen(Constants.BOWLERS_WIN);
        }
    }
    #endregion

    #region BALL DELIVARY SIMULATION
    private void DeliverBall() 
    {
        mainCanvasGO.SetActive(false);
        ballDelivaryController.DeliverBall(OnCompletionOfBallDelivery);
    }
    private void OnCompletionOfBallDelivery()
    {
        mainCanvasGO.SetActive(true);
        EnableResult();
    }
    #endregion

    #region RESET
        private void reset() 
    {
        score = 0;
        ballsRemianing = Constants.TOTAL_BALLS;
        wickets = 0;
    }
    #endregion
}
