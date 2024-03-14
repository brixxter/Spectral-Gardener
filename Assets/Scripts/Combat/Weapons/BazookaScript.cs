using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BazookaScript : MonoBehaviour
{
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public Transform emissionPoint;
    public GameObject player;
    public GameObject projectile;
    public float chargeTime = 1, cooldownTime = 2, projectileSpeed = 5;
    private bool cooldownActive;
    public int bulletID = 1;

    private void Awake()
    {
        skinnedMeshRenderer = gameObject.GetComponent<SkinnedMeshRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !cooldownActive&&!PauseMenuScript.Instance.isPaused) StartCoroutine(ChargeShot());
    }

    public void BoostDamage()
    {
        projectile.GetComponent<LilyProjectileScript>().damage+=20;
    }

    public void BoostSize()
    {
        projectile.GetComponent<LilyProjectileScript>().scale*=1.1f;
    }

    IEnumerator ChargeShot()
    {
        
        if (PlayerStats.Instance.cropAmount[bulletID] <= 0)
        {
            Sounds.Instance.PlaySound(Sounds.Instance.noammo, gameObject.transform, 1f);
            yield break;
        }
        PlayerStats.Instance.cropAmount[bulletID]--;
        ToolSelectionScript.switchLocked = true;
        Sounds.Instance.PlaySound(Sounds.Instance.bazookacharge, gameObject.transform, 1f);
        cooldownActive = true;
        float charged = 0;
        float chargeState = 0;
        while (charged < chargeTime)
        {
            charged += Time.deltaTime;
            chargeState = Mathf.Clamp(charged/chargeTime, 0, 1);
            skinnedMeshRenderer.SetBlendShapeWeight(0, chargeState * 100);
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(DoRecoil());
        ShootingScript.FireBullet(projectile, emissionPoint.transform, projectileSpeed);
        Sounds.Instance.PlaySound(Sounds.Instance.bazookashoot, gameObject.transform, 1f);
        Sounds.Instance.PlaySound(Sounds.Instance.bazookareload, gameObject.transform, 1f);
        while (charged > 0)
        {
            charged -= Time.deltaTime;
            chargeState = Mathf.Clamp(charged/chargeTime, 0, 1);
            skinnedMeshRenderer.SetBlendShapeWeight(0, charged * 100);
            yield return new WaitForEndOfFrame();
        }
        cooldownActive = false;
        ToolSelectionScript.switchLocked = false;
    }

    IEnumerator DoRecoil()
    {
        Animator anim = gameObject.GetComponent<Animator>();
        anim.Play("bazooka recoil");
        yield return new WaitForSeconds(0.9f*cooldownTime);
        anim.Play("New State");
    }


    private void OnDisable()
    {
        StopAllCoroutines();
        skinnedMeshRenderer.SetBlendShapeWeight(0, 0);
        cooldownActive=false;
    }
}


