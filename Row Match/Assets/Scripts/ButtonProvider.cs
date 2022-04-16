using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

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

    public GameObject DeliverBetterButton(Transform parentTransform)
    {
        GameObject go = new GameObject();
        GameObject goTMPro = new GameObject();

        go.AddComponent<Button>();
        go.AddComponent<Image>();
        goTMPro.AddComponent<TextMeshPro>();

        Transform buttonTransform = go.GetComponent<Button>().transform;
        goTMPro.GetComponent<TextMeshPro>().transform.SetParent(buttonTransform);
        goTMPro.GetComponent<TextMeshPro>().transform.localScale = new Vector3(20, 20, 20);

        go.GetComponent<Button>().transform.SetParent(parentTransform);
        go.GetComponent<Button>().transform.localPosition = Vector3.zero;
        go.GetComponent<Button>().transform.localScale = Vector3.one;

        go.GetComponent<Image>().transform.SetParent(parentTransform);
        go.GetComponent<Image>().transform.localPosition = Vector3.zero;
        go.GetComponent<Image>().transform.localScale = Vector3.one;

        return go;
    }

    public GameObject OneMore(Transform parentTransform)
    {
        GameObject go = new GameObject();

        go.AddComponent<Button>();
        go.AddComponent<TextMeshPro>();

        go.GetComponent<TextMeshPro>().transform.SetParent(parentTransform);
        go.GetComponent<TextMeshPro>().transform.localPosition = Vector3.zero;
        go.GetComponent<TextMeshPro>().transform.localScale = new Vector3(20, 20, 20);

        go.GetComponent<Button>().transform.SetParent(parentTransform);
        go.GetComponent<Button>().transform.localPosition = Vector3.zero;
        go.GetComponent<Button>().transform.localScale = new Vector3(20, 20, 20);

        return go;
    }
}
