using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splatter_Holder : MonoBehaviour
{
    public static Splatter_Holder instance;
    private void Awake()
    {
        instance = this;
    }
}
