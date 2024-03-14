using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoeScript : MonoBehaviour
{
    public Camera cam;
    public GameObject plot, preview;
    private GameObject previewInstance;
    private MeshRenderer previewMR;
    public GameObject infoPanel;



    private void OnEnable()
    {
        previewInstance = Instantiate(preview);
        previewMR = previewInstance.GetComponent<MeshRenderer>();
        infoPanel.SetActive(true);
    }

    void Update()
    {
        RaycastHit hit;
        var ray = Camera.main.ScreenPointToRay(WorldScript.screenCenter);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.point.y < 0.005 && (hit.point - cam.transform.position).magnitude < 4)
            {
                previewMR.enabled = true;
                previewInstance.transform.position = hit.point;
                if (Input.GetKeyDown(KeyCode.Mouse0) && PlotPreviewScript.validPlacement && !PauseMenuScript.Instance.isPaused)
                {
                    if(PlayerStats.Instance.availablePlots <= 0) return;
                    StartCoroutine(MoveHoe());
                    GameObject plotInstance = Instantiate(plot);
                    plotInstance.transform.position = hit.point;
                    DirtEffect.Instance.SpawnDirt(hit.point, 30);
                    Sounds.Instance.PlaySound(Sounds.Instance.dig, transform, 0.4f);
                    PlayerStats.Instance.plantedPlots++;
                    PlayerStats.Instance.availablePlots--;
                }
            }
            else
            {
                previewMR.enabled = false;
            }

        }
    }

    IEnumerator MoveHoe()
    {
        ToolSelectionScript.switchLocked = true;
        Animator anim = gameObject.GetComponent<Animator>();
        anim.Play("hoe placement");
        yield return new WaitForSeconds(0.4f);
        anim.Play("New State");
        ToolSelectionScript.switchLocked = false;
    }

    private void OnDisable()
    {
        Destroy(previewInstance);
        if(infoPanel!=null) infoPanel.SetActive(false);
    }
}
