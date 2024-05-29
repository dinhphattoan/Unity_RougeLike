using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public static class Methods
{
    public enum SpriteBottomType
    {
        Grass, StoneBrick, Dirt, Rock, Wood
    }
    public enum SpriteLeftType
    {
        Grass, StoneBrick, Dirt, Rock
    }
    public enum SpriteRightType
    {
        Grass, StoneBrick, Dirt, Rock
    }
    public enum SpriteTopType
    {
        Grass, StoneBrick, Dirt
    }
    public enum SpriteSideType
    {
        SpriteLeftType, SpriteTopType, SpriteRightType, SpriteBottomType
    }
    public enum SpriteCorner
    {
        Start, Middle, End
    }

    /// <summary>
    /// Preload all resources that defined from the texture2d
    /// </summary>
    public static List<Sprite> CreateSprites(Texture2D texture2D, int pixelsPerUnit, Vector2Int spriteSize)
    {
        var mySprites = new List<Sprite>();

        // Replace these values with your actual coordinates and sizes
        int spriteHeight = spriteSize.y;
        int spriteWidth = spriteSize.x;
        for (int y = 0; y < texture2D.height; y += spriteHeight)
        {
            for (int x = 0; x < texture2D.width; x += spriteWidth)
            {
                // Define the rectangle for the current sprite
                Rect spriteRect = new Rect(x, texture2D.height - y - spriteHeight, spriteWidth, spriteHeight);

                // Create a new sprite using Sprite.Create
                Sprite newSprite = Sprite.Create(texture2D, spriteRect, new Vector2(0.5f, 0.5f), pixelsPerUnit);

                // Add the created sprite to the list
                mySprites.Add(newSprite);
            }
        }
        return mySprites;
    }
    public static Sprite GetSpriteFromTexture2DAtPosition(Texture2D texture2D, int pixelsPerUnit, Vector2Int spriteSize, Vector2 postition)
    {
        // Replace these values with actual coordinates and sizes
        int spriteHeight = spriteSize.y;
        int spriteWidth = spriteSize.x;
        int x = (int)postition.y * spriteWidth;
        int y = (int)postition.x * spriteHeight;
        // Define the rectangle for the current sprite
        Rect spriteRect = new Rect(x, texture2D.height - y - spriteHeight, spriteWidth, spriteHeight);

        // Create a new sprite using Sprite.Create
        return Sprite.Create(texture2D, spriteRect, new Vector2(0.5f, 0.5f), pixelsPerUnit);

    }
    public static Sprite BorderSpritePicker(List<Sprite> resource1, List<Sprite> resource2, SpriteCorner spriteCorner, SpriteSideType spriteSideType, int spriteTypeIndex)
    {
        //Grass type
        // if (spriteTypeIndex == 0)
        // {
        //     if (spriteSideType == SpriteSideType.SpriteBottomType)
        //     {
        //         return resource1[9];
        //     }
        //     if(spriteSideType == SpriteSideType.SpriteLeftType)
        //     {
        //         return resource2[23];
        //     }
        //     if(spriteSideType == SpriteSideType.SpriteRightType)
        //     {
        //         return resource1[16];
        //     }
        //     if(spriteSideType==SpriteSideType.SpriteTopType)
        //     {
        //         return resource1[25];
        //     }
        // }
        //StoneBrick type
        // if (spriteTypeIndex == 1)
        // {
        //     if (spriteSideType == SpriteSideType.SpriteBottomType)
        //     {
        //         return resource1[12];
        //     }
        //     if (spriteSideType == SpriteSideType.SpriteLeftType)
        //     {
        //         return resource2[20];
        //     }
        //     if (spriteSideType == SpriteSideType.SpriteRightType)
        //     {
        //         return resource1[19];
        //     }
        //     if (spriteSideType == SpriteSideType.SpriteTopType)
        //     {
        //         return resource1[28];
        //     }
        // }
        // //Dirt type
        // if (spriteTypeIndex == 2)
        // {
        //     if (spriteSideType == SpriteSideType.SpriteBottomType)
        //     {
        //         return resource1[36];
        //     }
        //     if (spriteSideType == SpriteSideType.SpriteLeftType)
        //     {
        //         return resource2[44];
        //     }
        //     if (spriteSideType == SpriteSideType.SpriteRightType)
        //     {
        //         return resource1[43];
        //     }
        //     if (spriteSideType == SpriteSideType.SpriteTopType)
        //     {
        //         return resource1[52];
        //     }
        // }
        // //Rock type
        // if (spriteTypeIndex == 3)
        // {
        //     if (spriteSideType == SpriteSideType.SpriteBottomType)
        //     {
        //         return resource1[33];
        //     }
        //     if (spriteSideType == SpriteSideType.SpriteLeftType)
        //     {
        //         return resource1[42];
        //     }
        //     if (spriteSideType == SpriteSideType.SpriteRightType)
        //     {
        //         return resource2[45];
        //     }
        //     if (spriteSideType == SpriteSideType.SpriteTopType)
        //     {
        //         return resource1[28];
        //     }
        // }
        if (spriteTypeIndex == 0)
        {
            if (spriteSideType == SpriteSideType.SpriteLeftType)
            {
                return resource1[9];
            }
            if (spriteSideType == SpriteSideType.SpriteTopType)
            {
                return resource2[23];
            }
            if (spriteSideType == SpriteSideType.SpriteBottomType)
            {
                return resource1[16];
            }
            if (spriteSideType == SpriteSideType.SpriteRightType)
            {
                return resource1[25];
            }
        }
        //StoneBrick type
        if (spriteTypeIndex == 1)
        {
            if (spriteSideType == SpriteSideType.SpriteLeftType)
            {
                return resource1[12];
            }
            if (spriteSideType == SpriteSideType.SpriteTopType)
            {
                return resource2[20];
            }
            if (spriteSideType == SpriteSideType.SpriteBottomType)
            {
                return resource1[19];
            }
            if (spriteSideType == SpriteSideType.SpriteRightType)
            {
                return resource1[28];
            }
        }
        //Dirt type
        if (spriteTypeIndex == 2)
        {
            if (spriteSideType == SpriteSideType.SpriteLeftType)
            {
                return resource1[36];
            }
            if (spriteSideType == SpriteSideType.SpriteTopType)
            {
                return resource2[44];
            }
            if (spriteSideType == SpriteSideType.SpriteBottomType)
            {
                return resource1[43];
            }
            if (spriteSideType == SpriteSideType.SpriteRightType)
            {
                return resource1[52];
            }
        }
        //Rock type
        if (spriteTypeIndex == 3)
        {
            if (spriteSideType == SpriteSideType.SpriteLeftType)
            {
                return resource1[33];
            }
            if (spriteSideType == SpriteSideType.SpriteTopType)
            {
                return resource1[42];
            }
            if (spriteSideType == SpriteSideType.SpriteBottomType)
            {
                return resource2[45];
            }
            if (spriteSideType == SpriteSideType.SpriteRightType)
            {
                return resource1[28];
            }
        }

        return null;

    }
    public static void DrawSpriteOnTile(ref Tilemap tilemap,Vector2Int pos,Sprite sprite)
    {
        Tile tile =new Tile(){sprite=sprite};
        tilemap.SetTile((Vector3Int)pos,tile);
    }
}
