using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Toolbox : MonoBehaviour
{
    public GameObject panel1;
    public GameObject panel2;
    public GameObject panel3;
    public GameObject panel4;
    public GameObject panel5;
    public GameObject panel6;
    private GameObject[] panels;

    // Use this for initialization
    void Start()
    {
        panels = new GameObject[] {
			panel1,
			panel2,
			panel3,
			panel4,
			panel5,
			panel6
		};

        MapMaker map = GetComponent<MapMaker>();
        if (map != null)
        {
            for (int i = 0; i < map.tools.Length; i++)
            {
                var tool = map.tools[i];
                var panel = panels[i];
                if (panel != null)
                {
                    Text count = panel.GetComponentInChildren<Text>();
                    count.text = tool.count.ToString();
                }
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (panel1 != null)
        {
        }
    }
}
