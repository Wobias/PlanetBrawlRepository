﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpeedable
{
    //slow method
    void SpeedToSize(HealthState currentHealthState);
    void SpeedEffect(float speedMultiplier, float timeout=0);
}


