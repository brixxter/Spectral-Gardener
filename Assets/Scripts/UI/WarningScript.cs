using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningScript : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(WarningDelete());
    }

    IEnumerator WarningDelete()
    {
        yield return new WaitForSeconds(10);
        gameObject.SetActive(false);
    }
}
