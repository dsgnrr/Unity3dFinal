using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMessage
{
    private static int _identity = 1;
    public int Key { get; set; } = _identity++;
    public string Text { get; set; }
    public DateTime Moment { get; set; } = DateTime.Now;
    public object Sender { get; set; } // nullable
    public int Lifetime { get; set; } = 5; // in seconds
}
