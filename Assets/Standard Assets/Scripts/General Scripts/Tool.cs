using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tool : MonoBehaviour
{
    Image toolImage;
    Text toolCount;

    // Use this for initialization
    void Start()
    {
        toolImage = GetComponentInChildren<Image>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDrag()
    {
        toolImage.color -= Color.white * Time.deltaTime;
    }
}
