using UnityEngine;


public class PlayerSave : MonoBehaviour
{
    public PlayerController playerController;

    //private void Awake()
    //{
    //    playerController = GetComponent<PlayerController>();
    //}

    public void SavePlayerState()
    {
        Vector3 position = playerController.GetPlayerPosition();
        PlayerPrefs.SetFloat("PlayerPosX", position.x);
        PlayerPrefs.SetFloat("PlayerPosY", position.y);
        PlayerPrefs.SetFloat("PlayerPosZ", position.z);
        PlayerPrefs.Save();
        Debug.Log("Player position saved!");
    }

    public void LoadPlayerState()
    {
        if (PlayerPrefs.HasKey("PlayerPosX"))
        {
            float x = PlayerPrefs.GetFloat("PlayerPosX");
            float y = PlayerPrefs.GetFloat("PlayerPosY");
            float z = PlayerPrefs.GetFloat("PlayerPosZ");

            playerController.SetPlayerPosition(new Vector3(x, y, z));
            Debug.Log("Player position loaded!");
        }
        else
        {
            Debug.LogWarning("No saved player position found!");
        }
    }

    private void OnApplicationQuit()
    {
        SavePlayerState();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            SavePlayerState();
        }

        if (Input.GetKeyDown(KeyCode.F9))
        {
            LoadPlayerState();
        }
    }
}
