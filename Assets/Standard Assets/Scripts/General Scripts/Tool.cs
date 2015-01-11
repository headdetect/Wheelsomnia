using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tool : MonoBehaviour
{
    Image toolImage;
    Image badgeBackground;
    Text toolCount;

    private int valToolCount;
    private GameObjectTypes objectType;

    private static readonly Rect SPRITE_SIZE = new Rect(0, 0, 500, 500);

    private bool _spriteChanged = false;


    // Use this for initialization
    void Start()
    {
        toolImage = GetComponentsInChildren<Image>()[0];
        badgeBackground = GetComponentsInChildren<Image>()[1];
		toolCount = GetComponentInChildren<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!_spriteChanged) return;

        toolCount.text = valToolCount.ToString();

        Texture2D texture = null;
        switch (objectType)
        {
            case GameObjectTypes.MagnetPull:
                texture = Resources.Load<Texture2D>(GetImage("Magnet_Attract"));
                break;
            case GameObjectTypes.MagnetPush:
                texture = Resources.Load<Texture2D>(GetImage("Magnet_Repel"));
                break;
        }
        toolImage.sprite = Sprite.Create(texture, SPRITE_SIZE, Vector2.zero);

        if (valToolCount <= 0)
        {
            // Make it a bit dimmer //
            Color opaqueColor = new Color(1, 1, 1, 0.3f);
            toolImage.color = opaqueColor;
            badgeBackground.color = opaqueColor;
            toolCount.color = opaqueColor;
        }

        _spriteChanged = false;
    }

    internal void ChangeSprite(ToolSet toolSet)
    {
        _spriteChanged = true;
        valToolCount = toolSet.count;
        objectType = toolSet.type;
    }

	void OnMouseDown() 
	{
		if (toolCount != null) 
		{
			Debug.Log ("toolCount: " + toolCount.text);
            if (valToolCount > 0) 
			{
                valToolCount--;
				GameObject[] playerBlocks = GameObject.FindGameObjectsWithTag("_PLAYERBLOCK");
				PlayerBlock playerBlock;
				foreach (GameObject b in playerBlocks)
				{
					playerBlock = b.GetComponent<PlayerBlock>();
					if (playerBlock != null)
					{
						playerBlock.SetMode(false);					
                        Debug.Log ("Disabled: " + b.gameObject.name);
                    }
                }

                //TODO: check for block type
                GameObject block = (GameObject)Instantiate (Resources.Load ("Prefabs/MagnetPush"));
				block.tag = "_PLAYERBLOCK";
				block.name = "MagnetPush-"+Time.time;
				block.AddComponent ("PlayerBlock");
				block.transform.position = new Vector3 (44, 0, 0);
				Debug.Log ("Created block");
                _spriteChanged = true;
			}
		}
		Debug.Log ("toolbox click done");
	}

    private string GetImage(string texture)
    {
        return "Graphics/Textures/" + texture;
    }
}

    