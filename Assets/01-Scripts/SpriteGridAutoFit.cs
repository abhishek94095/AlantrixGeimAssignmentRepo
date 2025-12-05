using UnityEngine;

public class SpriteGridAutoFit : MonoBehaviour
{
    [Header("Grid Size")]
    [Min(1)] public int rows = 2;
    [Min(1)] public int columns = 2;

    [Header("Card Prefab")]
    public GameObject cardPrefab;

    [Header("Camera")]
    public Camera targetCamera;

    [Header("Card Layout")]
    public float cardSize = 1f;
    public float spacingX = 0.1f;
    public float spacingY = 0.1f;

    [Header("Margin")]
    public float marginX = 0.5f;
    public float marginY = 0.5f;

    [Header("Back Sprite")]
    public Sprite backSprite;

    private void Start()
    {
        if (targetCamera == null) targetCamera = Camera.main;
        RebuildGrid();
    }

    [ContextMenu("Rebuild Grid")]
    public void RebuildGrid()
    {
        if (cardPrefab == null)
        {
            Debug.LogError("Card prefab missing.");
            return;
        }

        foreach (Transform child in transform)
            DestroyImmediate(child.gameObject);

        float totalWidth = columns * cardSize + (columns - 1) * spacingX;
        float totalHeight = rows * cardSize + (rows - 1) * spacingY;

        float requiredH = totalHeight / 2f + marginY;
        float requiredHFromW = (totalWidth / (2f * targetCamera.aspect)) + marginX;
        targetCamera.orthographicSize = Mathf.Max(requiredH, requiredHFromW);

        Vector3 center = targetCamera.transform.position;
        center.z = 0;

        float startX = center.x - totalWidth / 2f + cardSize / 2f;
        float startY = center.y + totalHeight / 2f - cardSize / 2f;

        SpriteRenderer sr = cardPrefab.GetComponent<SpriteRenderer>() ?? cardPrefab.GetComponentInChildren<SpriteRenderer>();
        float scaleFactor = cardSize / Mathf.Max(sr.sprite.bounds.size.x, sr.sprite.bounds.size.y);

        int id = 0;

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {
                Vector3 pos = new(startX + c * (cardSize + spacingX), startY - r * (cardSize + spacingY), 0);
                GameObject go = Instantiate(cardPrefab, pos, Quaternion.identity, transform);
                go.transform.localScale = new Vector3(scaleFactor, scaleFactor, 1);

                var card = go.GetComponent<CardView>();
                card.Initialize(id, CardSpriteHandler.Instance.GetSprite(id), backSprite, true);

                id++;
            }
        }
    }
}
