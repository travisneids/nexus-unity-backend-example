using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SupportCreatorModal : MonoBehaviour
{
    public Button supportCreatorButton;
    public GameObject supportCreatorModal;

    void Start()
    {
        supportCreatorModal.SetActive(false);
        supportCreatorButton.onClick.AddListener(OpenSupportCreatorModal);

    }

    void OpenSupportCreatorModal()
    {
        supportCreatorModal.SetActive(true);
    }

    public void CloseSupportCreatorModal()
    {
        supportCreatorModal.SetActive(false);
    }
}
