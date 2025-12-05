using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(SpriteRenderer))]
public class CardView : MonoBehaviour
{
    public int CardId { get; private set; }

    private SpriteRenderer _renderer;
    private Sprite _frontSprite;
    private Sprite _backSprite;

    private bool _isFrontShowing;
    private bool _isAnimating;

    public float flipDuration = 0.3f;
    public Ease flipEase = Ease.OutQuad;
    private Vector3 originalScale;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        originalScale = transform.localScale;
    }

    public void Initialize(int id, Sprite front, Sprite back, bool faceDown = true)
    {
        CardId = id;
        _frontSprite = front;
        _backSprite = back;
        _renderer.sprite = faceDown ? _backSprite : _frontSprite;
        _isFrontShowing = !faceDown;
    }

    public void Flip()
    {
        if (_isAnimating) return;
        _isAnimating = true;

        bool showFront = !_isFrontShowing;

        Sequence seq = DOTween.Sequence();

        seq.Append(transform.DOScaleX(0f, flipDuration * 0.5f).SetEase(flipEase));

        seq.AppendCallback(() =>
        {
            _renderer.sprite = showFront ? _frontSprite : _backSprite;
            _isFrontShowing = showFront;
        });

        seq.Append(transform.DOScaleX(originalScale.x, flipDuration * 0.5f).SetEase(flipEase));

        seq.OnComplete(() => _isAnimating = false);
    }

    private void OnMouseDown()
    {
        Flip();
    }
}
