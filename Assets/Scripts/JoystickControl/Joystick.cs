using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum AxisOptions { Both, Horizontal, Vertical }

public class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] protected RectTransform _backgroundRect = null;
    [SerializeField] private RectTransform _handleRect = null;

    [SerializeField] private float handleRange = 1;
    [SerializeField] private float deadZone = 0;

    private Quaternion rotation = Quaternion.identity;
    private Vector2 input = Vector2.zero;

    private Canvas _canvas;
    private Camera _camera;

    private Vector3 movement = Vector3.zero;

    public float Horizontal { get { return input.x; } }
    public float Vertical { get { return input.y; } }

    public Vector2 Direction { get { return new Vector2(Horizontal, Vertical); } }
    public Quaternion Rotation { get => rotation; }

    public float HandleRange
    {
        get { return handleRange; }
        set { handleRange = Mathf.Abs(value); }
    }

    public float DeadZone
    {
        get { return deadZone; }
        set { deadZone = Mathf.Abs(value); }
    }

    public Vector3 Movement { get => movement; }

    protected virtual void Start()
    {
        Initialization();
        SetCenterPosition();
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        movement = Vector3.one;
        OnDrag(eventData);
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        movement = Vector3.zero;
        input = Vector2.zero;
        _handleRect.anchoredPosition = Vector2.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position = RectTransformUtility.WorldToScreenPoint(_camera, _backgroundRect.position);
        Vector2 radius = _backgroundRect.sizeDelta / 2;
        
        input = (eventData.position - position) / (radius * _canvas.scaleFactor);
        HandleInput(input.magnitude, input.normalized, radius);
        _handleRect.anchoredPosition = input * radius * handleRange;

        rotation = Quaternion.Euler(0, Vector2.SignedAngle(Direction, Vector2.up), 0);
    }

    protected virtual void HandleInput(float magnitude, Vector2 normalised, Vector2 radius)
    {
        if (magnitude > deadZone)
        {
            if (magnitude > 1)
                input = normalised;
        }
        else
            input = Vector2.zero;
    }

    private void SetCenterPosition()
    {
        Vector2 center = new Vector2(0.5f, 0.5f);
        _backgroundRect.pivot = center;
        _handleRect.anchorMin = center;
        _handleRect.anchorMax = center;
        _handleRect.pivot = center;
        _handleRect.anchoredPosition = Vector2.zero;
    }

    private void Initialization()
    {
        HandleRange = handleRange;
        DeadZone = deadZone;

        _canvas = GetComponentInParent<Canvas>();

        if (_canvas == null)
            Debug.LogError("The Joystick is not placed inside a canvas");

        _camera = null;

        if (_canvas.renderMode == RenderMode.ScreenSpaceCamera)
            _camera = _canvas.worldCamera;
    }
}