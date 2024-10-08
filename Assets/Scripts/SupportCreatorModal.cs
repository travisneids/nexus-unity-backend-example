using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SupportCreatorModal : MonoBehaviour
{
    public Button supportCreatorButton;
    public Button cancelButton;
    public Button confirmButton;
    public Button closeModalButton;
    public GameObject supportCreatorModal;
    public GameObject supportCreatorModalBackground;

    void Start()
    {
        supportCreatorModal.SetActive(false);
        supportCreatorModalBackground.SetActive(false);

        supportCreatorButton.onClick.AddListener(OpenSupportCreatorModal);
        cancelButton.onClick.AddListener(CloseSupportCreatorModal);
        closeModalButton.onClick.AddListener(CloseSupportCreatorModal);

    }

    void OpenSupportCreatorModal()
    {
        supportCreatorModal.SetActive(true);
        supportCreatorModalBackground.SetActive(true);
    }

    public void CloseSupportCreatorModal()
    {
        supportCreatorModal.SetActive(false);
        supportCreatorModalBackground.SetActive(false);
    }
}
