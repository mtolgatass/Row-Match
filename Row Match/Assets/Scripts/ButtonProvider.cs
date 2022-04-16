using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public sealed class ButtonProvider : MonoBehaviour
{
    public static CustomButton DeliverCustomButton(Transform parentTransform)
    {
        CustomButton button = Instantiate(Resources.Load<CustomButton>("CustomButton"), parentTransform.position, Quaternion.identity);
        Debug.Log("GOT BUTTON: " + button.index);
        return button;
    }
}
