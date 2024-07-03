using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class TextToTextMeshProConverter : MonoBehaviour
{
    [MenuItem("Tools/Convert Text to TextMeshPro")]
    public static void ConvertTextToTextMeshPro()
    {
        Text[] textObjects = FindObjectsOfType<Text>();

        foreach (Text textObject in textObjects)
        {
            GameObject go = textObject.gameObject;
            string textContent = textObject.text;
            Font font = textObject.font;
            int fontSize = textObject.fontSize;
            Color color = textObject.color;
            TextAnchor alignment = textObject.alignment;
            bool supportRichText = textObject.supportRichText;

            DestroyImmediate(textObject);

            TextMeshProUGUI tmp = go.AddComponent<TextMeshProUGUI>();
            tmp.text = textContent;
            tmp.fontSize = fontSize;
            tmp.color = color;
            tmp.alignment = ConvertAlignment(alignment);
            tmp.richText = supportRichText;

            // Optional: Assign a default TMP Font Asset
            if (font != null)
            {
                // Load your custom TMP Font Asset
                TMP_FontAsset customFont = Resources.Load<TMP_FontAsset>("MyFont");

                // Assign the TMP Font Asset to the TextMeshPro component
                if (customFont != null)
                {
                    tmp.font = customFont;
                }
                else
                {
                    Debug.LogError("TMP Font Asset 'MyFont' not found in Resources folder.");
                }
            }
        }
    }

    private static TextAlignmentOptions ConvertAlignment(TextAnchor alignment)
    {
        switch (alignment)
        {
            case TextAnchor.UpperLeft: return TextAlignmentOptions.TopLeft;
            case TextAnchor.UpperCenter: return TextAlignmentOptions.Top;
            case TextAnchor.UpperRight: return TextAlignmentOptions.TopRight;
            case TextAnchor.MiddleLeft: return TextAlignmentOptions.Left;
            case TextAnchor.MiddleCenter: return TextAlignmentOptions.Center;
            case TextAnchor.MiddleRight: return TextAlignmentOptions.Right;
            case TextAnchor.LowerLeft: return TextAlignmentOptions.BottomLeft;
            case TextAnchor.LowerCenter: return TextAlignmentOptions.Bottom;
            case TextAnchor.LowerRight: return TextAlignmentOptions.BottomRight;
            default: return TextAlignmentOptions.Center;
        }
    }
}
