using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // (Resto del código se mantiene igual, excepto las nuevas funciones añadidas)

    public void UnlockGun()
    {
        playerHasGun = true;
        Debug.Log("Gun unlocked!");
    }

    public void IncreaseJumpForce(float additionalForce)
    {
        jumpForce += additionalForce;
        Debug.Log($"Jump force increased by {additionalForce}!");
    }
}