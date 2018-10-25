﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventType {
    public const string ASTEROID_HIT = "asteroidHit";
    public const string SCORE_UPDATED = "scoreUpdated";
    public const string SHOWING_INTERACTIVE_CONTENT = "showingInteractiveContent";
    public const string CLOSED_INTERACTIVE_CONTENT = "closedInteractiveContent";
    public const string SHIP_LIFE_CHANGED = "shipLifeChanged";
    public const string LOSE_CONDITION_ACHIEVED = "loseConditionAchieved";
    public const string WIN_CONDITION_ACHIEVED = "winConditionAchieved";
}