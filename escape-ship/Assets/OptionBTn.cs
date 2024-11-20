using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class OptionBTn : MonoBehaviour
{

    [Header("Btn")]
    [SerializeField] private GameObject ShowKeyBind;

    [Header("Sound")]
    [SerializeField] private AudioSource BTNClick;

    public void ShowKeyOption()
    {
        ShowKeyBind.SetActive(true);
        BTNClick.Play();

        GameManager.Instance.ShowMouse();
    }

    public void HideKeyOPtion()
    {
        ShowKeyBind.SetActive(false);
        BTNClick.Play();

    }
}
