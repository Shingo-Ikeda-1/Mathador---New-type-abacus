using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public enum RendererType
{
    Image,
    SpriteRenderer
};

public class DigitIcon : DigitChangeable
{
    [SerializeField]
    protected IconsInfo iconsInfo;

    protected Sprite[] NumIcons;

    public RendererType RenderType;

    [SerializeField]
    [HideInInspector]
    protected internal Image HandleImage;

    [SerializeField]
    [HideInInspector]
    protected internal Image SubHandleImage;

    [SerializeField]
    [HideInInspector]
    protected internal SpriteRenderer HandleRenderer;

    [SerializeField]
    [HideInInspector]
    protected internal SpriteRenderer SubHandleRenderer;

    public override void OnEnable()
    {
        base.OnEnable();
        NumIcons = iconsInfo.Icons;
    }

    public override void OnDisable()
    {
        base.OnDisable();
    }

    protected override void Update04Visual()
    {
        switch (RenderType)
        {
            case RendererType.Image:
                if (Number04 == 0)
                {
                    SubHandleImage.enabled = false;
                    SubHandleImage.sprite = null;
                }
                else
                {
                    SubHandleImage.sprite = NumIcons[Number04];
                    SubHandleImage.enabled = true;
                }
                break;
            case RendererType.SpriteRenderer:
                if (Number04 == 0)
                {
                    SubHandleRenderer.enabled = false;
                    SubHandleRenderer.sprite = null;
                }
                else
                {
                    SubHandleRenderer.sprite = NumIcons[Number04];
                    SubHandleRenderer.enabled = true;
                }
                break;
        }
    }

    protected override void Update05Visual()
    {
        switch (RenderType)
        {
            case RendererType.Image:
                HandleImage.sprite = NumIcons[IsFiveOn ? 5 : 0];
                RectTransform subHandleRect = SubHandleImage.GetComponent<RectTransform>();
                if (IsFiveOn)
                {
                    // position adjustment
                    subHandleRect.anchoredPosition = new Vector2(0, -1.5f);
                }
                else
                {
                    subHandleRect.anchoredPosition = new Vector2(0, 0);
                }
                break;
            case RendererType.SpriteRenderer:
                HandleRenderer.sprite = NumIcons[IsFiveOn ? 5 : 0];
                Transform subHandleTrans = SubHandleRenderer.GetComponent<Transform>();
                if (IsFiveOn)
                {
                    // position adjustment
                    subHandleTrans.localPosition = new Vector3(0, -0.15f, -0.15f);
                }
                else
                {
                    subHandleTrans.localPosition = new Vector3(0, 0, -0.15f);
                }
                break;
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(DigitIcon), true)]
public class DigitIconEditor : Editor
{
    private DigitIcon _target;

    private SerializedProperty _handleImageProperty;
    private SerializedProperty _subHandleImageProperty;

    private SerializedProperty _handleRendererProperty;
    private SerializedProperty _subHandleRendererProperty;


    private void OnEnable()
    {
        _target = target as DigitIcon;

        _handleImageProperty = serializedObject.FindProperty(nameof(_target.HandleImage));
        _subHandleImageProperty = serializedObject.FindProperty(nameof(_target.SubHandleImage));

        _handleRendererProperty = serializedObject.FindProperty(nameof(_target.HandleRenderer));
        _subHandleRendererProperty = serializedObject.FindProperty(nameof(_target.SubHandleRenderer));
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        serializedObject.Update();

        switch (_target.RenderType)
        {
            case RendererType.Image:
                EditorGUILayout.PropertyField(_handleImageProperty);
                EditorGUILayout.PropertyField(_subHandleImageProperty);
                break;
            case RendererType.SpriteRenderer:
                EditorGUILayout.PropertyField(_handleRendererProperty);
                EditorGUILayout.PropertyField(_subHandleRendererProperty);
                break;
            default:
                break;
        }
        serializedObject.ApplyModifiedProperties();
    }
}
#endif
