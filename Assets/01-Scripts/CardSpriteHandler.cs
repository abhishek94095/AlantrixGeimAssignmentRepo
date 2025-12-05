using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardSpriteData
{
    public int id;
    public Sprite sprite;
}

public class CardSpriteHandler : Singleton<CardSpriteHandler>
{
    [Header("Default Fallback Card")]
    public Sprite defaultSprite;

    [Header("Card Sprite Database")]
    public List<CardSpriteData> spriteDatabase = new List<CardSpriteData>();

    private Dictionary<int, Sprite> _lookup;

    public override void Awake()
    {
        base.Awake();

        _lookup = new Dictionary<int, Sprite>();

        foreach (var data in spriteDatabase)
        {
            if (data == null || data.sprite == null) 
                continue;

            if (!_lookup.ContainsKey(data.id))
                _lookup.Add(data.id, data.sprite);
        }
    }

    public Sprite GetSprite(int id)
    {
        if (_lookup != null && _lookup.TryGetValue(id, out var result))
            return result;

        return defaultSprite;
    }
}
