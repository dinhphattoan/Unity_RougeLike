using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static Methods;
public class RoomPrefabGenerator : MonoBehaviour
{

    [Space]
    [Header("Preload resources")]
    [Space]
    [Header("Wall attribute")]
    public SpriteLeftType leftWallType;
    public SpriteTopType topWallType;
    public SpriteRightType rightWallType;
    public SpriteBottomType bottomWallType;

    [Space]
    [Header("Resource attribute")]
    [Space]
    [SerializeField] Texture2D texture2D;
    [SerializeField] Texture2D texture2D_reverse;
    [SerializeField] List<Sprite> resourcesSprite = new List<Sprite>();
    [SerializeField] List<Sprite> resourcesSprite_reverse = new List<Sprite>();
    [Space]
    [Header("Room attribute")]
    public Vector2Int roomSize;
    public GameObject spriteExample;
    public Tilemap tilemap;
    public void GenerateMap()
    {
        tilemap.ClearAllTiles();
        resourcesSprite.Clear();
        resourcesSprite_reverse.Clear();
        resourcesSprite = Methods.CreateSprites(texture2D, 1, new Vector2Int(8, 8));
        resourcesSprite_reverse = Methods.CreateSprites(texture2D_reverse, 1, new Vector2Int(8, 8));
        Sprite[,] matrix = new Sprite[roomSize.x, roomSize.y];
        //Fill default tile
        //Border tiles
        for (int i = 0; i < roomSize.x; i++)
        {
            for (int j = 0; j < roomSize.y; j++)
            {

                if (i == 0)
                {
                    matrix[i, j] = Methods.BorderSpritePicker(resourcesSprite, resourcesSprite_reverse
                                    , SpriteCorner.Middle, SpriteSideType.SpriteTopType, (int)leftWallType);
                    continue;
                }
                if (j == 0)
                {
                    matrix[i, j] = Methods.BorderSpritePicker(resourcesSprite, resourcesSprite_reverse
                    , SpriteCorner.Middle, SpriteSideType.SpriteLeftType, (int)bottomWallType);
                    continue;
                }
                if (i == roomSize.x - 1)
                {
                    matrix[i, j] = Methods.BorderSpritePicker(resourcesSprite, resourcesSprite_reverse
                    , SpriteCorner.Middle, SpriteSideType.SpriteBottomType, (int)rightWallType);
                    continue;
                }
                if (j == roomSize.y - 1)
                {
                    matrix[i, j] = Methods.BorderSpritePicker(resourcesSprite, resourcesSprite_reverse
                    , SpriteCorner.Middle, SpriteSideType.SpriteRightType, (int)topWallType);
                    continue;
                }
                matrix[i, j] = null;

            }

        }
        //Four corner fill with default
        matrix[0, 0] = null;
        matrix[0, roomSize.y - 1] = null;
        matrix[roomSize.x - 1, 0] = null;
        matrix[roomSize.x - 1, roomSize.y - 1] = null;
        //Draw tile map
        for (int i = 0; i < roomSize.x; i++)
        {
            for (int j = 0; j < roomSize.y; j++)
            {
                var tile = ScriptableObject.CreateInstance<Tile>();
                tile.sprite = matrix[i, j];

                tilemap.SetTile(new Vector3Int(i, j, 0), tile);

            }
        }
    }

}

