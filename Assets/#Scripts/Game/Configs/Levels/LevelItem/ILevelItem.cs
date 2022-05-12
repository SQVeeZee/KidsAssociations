using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILevelItem
{
    event Action<ELevelCompleteReason> onLevelCompleted;
}
