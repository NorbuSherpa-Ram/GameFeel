using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Discoball : MonoBehaviour,IHitable 
{
    [SerializeField] EntityFX myFx;
    public void OnHit()
    {
        myFx.Flash();
    }


}
