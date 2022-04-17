using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public sealed class ButtonProvider : MonoBehaviour
{
    public static CustomButton DeliverCustomButton(Vector2 position)
    {
        CustomButton button = Instantiate(Resources.Load<CustomButton>("CustomButton"), position, Quaternion.identity);
        Debug.Log("GOT BUTTON: " + button.index);
        return button;
    }

    public static MenuCustomButton DeliverCustomButtonForMenu(Vector2 position)
    {
        MenuCustomButton button = Instantiate(Resources.Load<MenuCustomButton>("MenuCustomButton"), position, Quaternion.identity);
        return button;
    }
}
