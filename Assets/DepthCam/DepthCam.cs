using UnityEngine;

[ExecuteInEditMode, RequireComponent(typeof(Camera))]
public class DepthCam : MonoBehaviour
{
    private enum ChannelState
    {
        depth = 0,
        invertedDepth = 1,
        sourceImage = 2,
        disabled = 3
    }

    [SerializeField]
    private ChannelState redChannel;
    [SerializeField, Range(0f, 0.99f), Tooltip("Clamp minimum distance")]
    private float rMin = 0f;
    [SerializeField, Range(0.01f, 1f), Tooltip("Clamp maximum distance")]
    private float rMax = 1f;

    [SerializeField]
    private ChannelState greenChannel;
    [SerializeField, Range(0f, 0.99f), Tooltip("Clamp minimum distance")]
    private float gMin = 0f;
    [SerializeField, Range(0.01f, 1f), Tooltip("Clamp maximum distance")]
    private float gMax = 1f;

    [SerializeField]
    private ChannelState blueChannel;
    [SerializeField, Range(0f, 0.99f), Tooltip("Clamp minimum distance")]
    private float bMin = 0f;
    [SerializeField, Range(0.01f, 1f), Tooltip("Clamp maximum distance")]
    private float bMax = 1f;

    [Header("Debug Draw")]
    [SerializeField, Range(0, 4), Tooltip("0 disables GUI")]
    private int GUIMagnification = 0;
    [SerializeField]
    private Vector2Int GUIPosition = new Vector2Int(0, 0);
    private bool drawGUI;
    private Rect guiRect;

    [Space, SerializeField]
    private RenderTexture renderTexture;
    [SerializeField] Shader shader;
    private Material material;

    private void ApplySettings()
    {
        drawGUI = GUIMagnification > 0;
        if (drawGUI)
        {
            int w = renderTexture.width;
            int h = renderTexture.height;
            int m = GUIMagnification;
            guiRect = new Rect(GUIPosition.x * w * m, GUIPosition.y * h * m, w * m, h * m);
        }

        if (material == null)
        {
            material = new Material(shader);
            material.hideFlags = HideFlags.DontSave;

            Camera cam = GetComponent<Camera>();
            cam.depthTextureMode = DepthTextureMode.Depth;
            cam.targetTexture = renderTexture;
        }

        material.SetInt("_R_State", (int)redChannel);
        material.SetFloat("_R_Min", rMin);
        material.SetFloat("_R_Max", rMax);
        material.SetInt("_G_State", (int)greenChannel);
        material.SetFloat("_G_Min", gMin);
        material.SetFloat("_G_Max", gMax);
        material.SetInt("_B_State", (int)blueChannel);
        material.SetFloat("_B_Min", bMin);
        material.SetFloat("_B_Max", bMax);
    }

    private void Awake()
    {
        ApplySettings();
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, material, 0);
    }

    private void OnValidate()
    {
        rMax = Mathf.Max(rMin + 0.01f, rMax);
        gMax = Mathf.Max(gMin + 0.01f, gMax);
        bMax = Mathf.Max(bMin + 0.01f, bMax);

        ApplySettings();
    }

    private void OnGUI()
    {
        if (drawGUI)
        {
            GUI.DrawTexture(guiRect, renderTexture);
        }
    }

    private void OnDestroy()
    {
        if (material != null)
        {
            if (Application.isPlaying)
            {
                Destroy(material);
            }
            else
            {
                DestroyImmediate(material);
            }
        }
    }
}
