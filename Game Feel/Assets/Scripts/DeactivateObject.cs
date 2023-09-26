using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateObject : MonoBehaviour
{
    [SerializeField] float lifeTime;
    private void OnEnable()
    {
        Invoke(nameof(Deactivate),lifeTime );
    }
    private void Deactivate() => gameObject.SetActive(false);

}
