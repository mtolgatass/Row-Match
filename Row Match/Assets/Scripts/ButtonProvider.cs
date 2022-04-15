using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonProvider : MonoBehaviour
{

    void Start()
    {
    }

    public GameObject DeliverButton(Transform parentTransform)
    {
        GameObject go = new GameObject();

        go.AddComponent<Button>();
        go.AddComponent<Image>();

        go.GetComponent<Button>().transform.SetParent(parentTransform);
        go.GetComponent<Button>().transform.localPosition = Vector3.zero;
        go.GetComponent<Button>().transform.localScale = Vector3.one;


        return go;
    }
}
