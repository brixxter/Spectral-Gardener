using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleScript : MonoBehaviour
{
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public Transform emissionPoint;
    public GameObject player;
    public GameObject projectile;
    public float fireRate = 0.1f, projectileSpeed = 5, reloadTime = 2.5f;
    private bool reloading, firing;
    public int cropID = 0, clipState = 0, clipSize = 35, damage;
    private IEnumerator reloadProcess, fireProcess;

    private void Start()
    {
        skinnedMeshRenderer = gameObject.GetComponent<SkinnedMeshRenderer>();
        skinnedMeshRenderer.SetBlendShapeWeight(0, 100 - 100 * clipState / clipSize);
        clipState = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)&&!PauseMenuScript.Instance.isPaused)
        {
            fireProcess = Firing();
            StartCoroutine(fireProcess);
        }
        if (Input.GetKeyUp(KeyCode.Mouse0) && firing) StopCoroutine(fireProcess);
        if (Input.GetKeyDown(KeyCode.R) && !reloading &&!PauseMenuScript.Instance.isPaused) StartCoroutine(Reload());
    }

    public void BoostClipSize()
    {
        clipSize+=10;
    }

    public void BoostDamage()
    {
        projectile.GetComponent<BeanProjectileScript>().damage+=5;
    }

    IEnumerator Firing()
    {
        if (reloading)
        {
            yield break;
        }
        if (ClipEmpty())
        {
            StartCoroutine(Reload());
            yield break;
        }
        firing = true;
        while (Input.GetKey(KeyCode.Mouse0) && !ClipEmpty() && firing)
        {
            clipState--;
            skinnedMeshRenderer.SetBlendShapeWeight(0, 100 - 100 * clipState / clipSize);
            StartCoroutine(RecoilAnim());
            ShootingScript.FireBullet(projectile, emissionPoint.transform, projectileSpeed);
            Sounds.Instance.PlaySound(Sounds.Instance.rifleshoot, transform,1f);
            yield return new WaitForSeconds(fireRate);
        }
        firing = false;
        if (ClipEmpty()) StartCoroutine(Reload());
    }



    IEnumerator Reload()
    {
        if (reloading)
        {
            //Debug.Log("Reload already in progress");

            yield break;
        }

        int ammoNeededInClip = clipSize - clipState;//
        if (ammoNeededInClip <= 0)
        {
           // Debug.Log("Clip still full");
            yield break;
        }

        
        int ammoAmount = Mathf.Min(PlayerStats.Instance.cropAmount[cropID], ammoNeededInClip);
        if (ammoAmount <= 0)
        {
            //play out of ammo sound
            Sounds.Instance.PlaySound(Sounds.Instance.noammo, gameObject.transform, 1f);
            yield break;
        }

        reloading = true;
        firing = false;
        ToolSelectionScript.switchLocked = true;
        //Play reload sound
        Sounds.Instance.PlaySound(Sounds.Instance.riflereload, gameObject.transform, 1f);
        StartCoroutine(ReloadAnim());
        yield return new WaitForSeconds(reloadTime / 2);
        clipState += ammoAmount;
        PlayerStats.Instance.cropAmount[cropID] -= ammoAmount;
        skinnedMeshRenderer.SetBlendShapeWeight(0, 100 - 100 * clipState / clipSize);
        yield return new WaitForSeconds(reloadTime / 2);
        reloading = false;
        ToolSelectionScript.switchLocked = false;
        if (Input.GetKey(KeyCode.Mouse0))
        {
            fireProcess = Firing();
            StartCoroutine(fireProcess);
        }
    }

    IEnumerator RecoilAnim()
    {
        Animator anim = gameObject.GetComponent<Animator>();
        anim.Play("rifle recoil");
        yield return new WaitForSeconds(0.9f * fireRate);
        if(!reloading) anim.Play("New State");
    }

    IEnumerator ReloadAnim()
    {
        Animator anim = gameObject.GetComponent<Animator>();
        anim.Play("rifle reload");
        yield return new WaitForSeconds(0.95f * reloadTime);
        anim.Play("New State");

    }

    private bool ClipEmpty()
    {
        return clipState <= 0;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        reloading = false;
        firing = false;
    }
}
