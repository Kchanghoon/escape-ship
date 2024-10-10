using UnityEngine;

[CreateAssetMenu(fileName = "NewKeypadData", menuName = "Keypad/KeypadData")]
public class KeypadData : ScriptableObject
{
    public string correctPassword;  // 정답 비밀번호
}
