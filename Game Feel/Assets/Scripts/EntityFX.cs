using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    private Entity myEntity; 
    private ObjectPooler pooler;

    [Header("Effects")]
    private Material defaultMaterial;

    [SerializeField] private Material flashMaterial;
    [SerializeField] private GameObject splatter;
    [SerializeField] private GameObject splatParticle;

    public SpriteRenderer mySR;
    Color defaultColor;
    void Start()
    {
        defaultColor = mySR.color;
        pooler = ObjectPooler.instance;
        defaultMaterial = mySR.material;
    }

    public  void SplatterEffect()
    {
        GameObject newSplatter = Instantiate(splatter, transform.position, Quaternion.identity);
        newSplatter.GetComponent<SpriteRenderer>().color = mySR.color;
    }
    public  void SplatParticle()
    {
        GameObject newSplat = pooler?.GetObjectFormPool(splatParticle, transform.position);
        if (newSplat != null)
        {
            ParticleSystem.MainModule ps = newSplat.GetComponent<ParticleSystem>().main;
            ps.startColor = defaultColor;
        }
    }

    public void Flash() => StartCoroutine(FlashEffect());

    private IEnumerator FlashEffect()
    {
       
        mySR.color = Color.white;
        mySR.material = flashMaterial;
        yield return new WaitForSeconds(.15f);
        mySR.color = defaultColor;
        mySR.material = defaultMaterial;
    }
}
