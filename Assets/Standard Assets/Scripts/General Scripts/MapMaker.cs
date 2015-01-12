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
							obj.tag = "_PLAYER";
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

