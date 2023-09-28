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
    [SerializeField] private GameObject deathRemaining;
    [SerializeField] private GameObject deathParticle;

    public SpriteRenderer mySR;
    [SerializeField] Color defaultColor;

    void Start()
    {

        pooler = ObjectPooler.instance;
        defaultMaterial = mySR.material;
    }

    public  void SplatterEffect()
    {
        Quaternion rotate = new Quaternion(0, 0, Random.Range(0, 180),1);
        GameObject newSplatter = Instantiate(deathRemaining, transform.position,rotate);
        newSplatter.GetComponent<SpriteRenderer>().color = mySR.color;
        newSplatter.transform.SetParent(Splatter_Holder.instance.transform);
    }
    public  void SplatParticle()
    {
        GameObject newSplat = pooler?.GetObjectFormPool(deathParticle, transform.position);
        if (newSplat != null)
        {
            ParticleSystem.MainModule ps = newSplat.GetComponent<ParticleSystem>().main;
            ps.startColor = defaultColor;
        }
    }

    public void Flash() => StartCoroutine(FlashEffect());

    private IEnumerator FlashEffect()
    {
        //   if(defaultColor!=null ) 
        defaultColor = mySR.color;
        mySR.color = Color.white;
        mySR.material = flashMaterial;
        yield return new WaitForSeconds(.12f);
        mySR.color = defaultColor;
        mySR.material = defaultMaterial;
    }
}
