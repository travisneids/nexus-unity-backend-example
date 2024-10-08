using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginController : MonoBehaviour
{
    public Button loginButton;
    public Button cancelButton;
    public Button closeModalButton;
    public GameObject loginModal;
    public GameObject loginModalBackground;

    void Start()
    {
        Debug.Log("Starting Login Controller");
        loginModal.SetActive(false);
        loginModalBackground.SetActive(false);

        loginButton.onClick.AddListener(OpenLoginModal);
        cancelButton.onClick.AddListener(CloseLoginModal);
        closeModalButton.onClick.AddListener(CloseLoginModal);

    }

    void OpenLoginModal()
    {
        loginModal.SetActive(true);
        loginModalBackground.SetActive(true);
    }

    public void CloseLoginModal()
    {
        loginModal.SetActive(false);
        loginModalBackground.SetActive(false);
    }
}
