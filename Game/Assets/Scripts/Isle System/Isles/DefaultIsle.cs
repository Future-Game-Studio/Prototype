﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultIsle : MonoBehaviour
{
    public IsleType Type { get; protected set; } = IsleType.Default;
    public Vector3 StartScale { get; protected set; }

    
}
