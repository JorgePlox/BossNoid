using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : MonoBehaviour
{
    HealthBarManager sharedInstance;
    public GameObject boss;
    public Slider healthBar;
    public Canvas healthCanvas;

    bool isActive = false;

    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        healthBar.maxValue = boss.GetComponent<Block>().maxBlockDuration;
        healthCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.sharedInstance.blockCount == 1 && !isActive)
        {
            healthCanvas.enabled = true;
            isActive = true;
        }

        if (isActive)
        {
            healthBar.value = boss.GetComponent<Block>().blockDuration;

            if (healthBar.value == 0)
            {
                healthCanvas.enabled = false;
            }
        }
    }
}
