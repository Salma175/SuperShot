using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants
{ 
    public enum BowlerType
    {
        FAST,
        SPIN
    }

    public const int TOTAL_BALLS = 30;
    public const int TARGET_SCORE = 60;
    public const int WICKETS = 5;

    public const string BATSMAN_WIN = "BATSMAN WIN!";
    public const string BOWLERS_WIN = "BOWLERS WIN";

    public const string DELIVARY_TYPE = "DELIVARY TYPE";
    public const string SCORE = "SCORE";

    public const string OUT = "OUT";
    public const string MISSED = "MISSED";

}
