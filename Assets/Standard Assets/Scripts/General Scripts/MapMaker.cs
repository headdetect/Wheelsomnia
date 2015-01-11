using UnityEngine;
using System.Collections;

internal enum GameObjectTypes
{
    Nothing = 0,
    Wall = 1,
    Ball = 2,
    Lava = 5,
    Flag = 6,
    MagnetPull = 7,
    MagnetPush = 8
}
internal struct ToolSet
{
    public GameObjectTypes type;
    public int count;
}

public class MapMaker : MonoBehaviour
{

    public GameObject flag;
    public GameObject ball;
    public GameObject wall;
    public GameObject magnetPull;
    public GameObject magnetPush;

    internal ToolSet[] tools = new ToolSet[6];

    void OnGUI()
    {
        int tools = GameObject.FindGameObjectsWithTag("_PLAYERBLOCK").Length;
        int total = GameObject.FindObjectsOfType(typeof(MonoBehaviour)).Length;

        Color temp = GUI.backgroundColor;
        GUI.backgroundColor = new Color(0f, 1f / 188f, 1f / 212f, 0.7f);

        GUI.Box(new Rect(0, 0, 150, 100), "Debug Tools");
        GUI.Label(new Rect(5, 30, 150, 50), "FPS: " + (1f / Time.deltaTime));
        GUI.Label(new Rect(5, 45, 150, 50), "# of GameObjects: " + total);
        GUI.Label(new Rect(5, 60, 150, 50), "# of Tools: " + tools);

        GUI.backgroundColor = temp;
    }

    // Use this for initialization
    void Start()
    {
        var testFile = Resources.Load<TextAsset>("test");

        var mapInfo = SimpleJSON.JSON.Parse(testFile.text);

        int x = 0;
        int y = 0;

        foreach (SimpleJSON.JSONNode t in mapInfo["layers"].AsArray)
        {
            SimpleJSON.JSONArray data = t["data"].AsArray;
            int height = t["height"].AsInt;
            int width = t["width"].AsInt;

            SimpleJSON.JSONNode properties = t["properties"];
            for (int i = 0; i < properties.Count; i++)
            {
                var toolData = properties[i];
                var toolType = toolData[0].AsInt;
                var toolCount = toolData[1].AsInt;
                ToolSet tool = new ToolSet()
                {
                    type = (GameObjectTypes)toolType,
                    count = toolCount
                };
                tools[i] = tool;
            }

            for (int h = 0; h < height; h++)
            {
                for (int w = 0; w < width; w++)
                {
                    int index = h * width + w;
                    GameObjectTypes type = (GameObjectTypes)data[index].AsInt;
                    GameObject obj = null;
                    switch (type)
                    {
                        case GameObjectTypes.Ball:
                            obj = Instantiate(ball, new Vector3(x, y, 0), Quaternion.identity) as GameObject;
                            obj.transform.localScale = new Vector3(1, 1, 1);
                            obj.transform.position = new Vector3(x, y, 0);
                            break;
                        case GameObjectTypes.Flag:
                            obj = Instantiate(flag, new Vector3(x, y, 0), Quaternion.identity) as GameObject;
                            obj.transform.localScale = new Vector3(1, 1, 1);
                            obj.transform.position = new Vector3(x, y, 0);
                            obj.tag = "_LEVELBLOCK";
                            break;
                        case GameObjectTypes.Wall:
                            obj = Instantiate(wall, new Vector3(x, y, 0), Quaternion.identity) as GameObject;
                            obj.transform.localScale = new Vector3(1, 1, 1);
                            obj.transform.position = new Vector3(x, y, 0);
                            obj.tag = "_LEVELBLOCK";
                            break;
                        case GameObjectTypes.MagnetPull:
                            obj = Instantiate(magnetPull, new Vector3(x, y, 0), Quaternion.identity) as GameObject;
                            obj.transform.localScale = new Vector3(1, 1, 1);
                            obj.transform.position = new Vector3(x, y, 0);
                            obj.tag = "_LEVELBLOCK";
                            break;
                        case GameObjectTypes.MagnetPush:
                            obj = Instantiate(magnetPush, new Vector3(x, y, 0), Quaternion.identity) as GameObject;
                            obj.transform.localScale = new Vector3(1, 1, 1);
                            obj.transform.position = new Vector3(x, y, 0);
                            obj.tag = "_LEVELBLOCK";
                            break;
                    }
                    x += 2;
                }
                x = 0;
                y -= 2;
            }
        }
    }
}

