using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.UI;

public class ColliderUI : MonoBehaviour
{
    public GameObject uiElement;
    public GameObject nextUI;
    public Button switchButton;

    private void Start()
    {
        uiElement.SetActive(false);
        nextUI.SetActive(false);
        //switchButton.onClick.AddListener(SwitchUI);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("player in");
            uiElement.SetActive(true);
        }
    }
    /*
    void SwitchUI()
    {

        uiElement.SetActive(false);
        Debug.Log("Activating nextUI");
        nextUI.SetActive(true);
    }
    */
}
