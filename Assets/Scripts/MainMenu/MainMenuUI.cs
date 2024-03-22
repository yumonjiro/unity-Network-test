using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class MainMenuUI : MonoBehaviour
{
    public GameObject StartButton;
    public GameObject StartHostButton;
    public GameObject StartClientButton;
    public void OnStartHostClicked()
    {
        NetworkManager.Singleton.StartHost();
        StartHostButton.SetActive(false);
        StartClientButton.SetActive(false);

        StartButton.SetActive(true);
    }
    public void OnClientStartClicked()
    {
        NetworkManager.Singleton.StartClient();
        StartClientButton.SetActive(false);
        StartHostButton.SetActive(false);

        StartButton.SetActive(true);
    }
    public void OnStartClicked()
    {
        Debug.Log("Start Button Clicled");
        NetworkManager.Singleton.SceneManager.LoadScene("Col", UnityEngine.SceneManagement.LoadSceneMode.Single);
    }

}
