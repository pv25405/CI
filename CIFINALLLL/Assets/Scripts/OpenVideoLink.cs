using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Attach to a UI GameObject that also has a Button component. Opens a video URL
/// (e.g. a YouTube link) in the user's default browser/app when clicked.
/// Wires itself to the Button's onClick automatically, so no manual Inspector
/// setup of the OnClick list is required.
/// </summary>
[RequireComponent(typeof(Button))]
public class OpenVideoLink : MonoBehaviour
{
    [Tooltip("Full URL of the video to open (e.g. a YouTube link).")]
    public string videoUrl = "https://youtu.be/NVouURGp3vE";

    void Awake()
    {
        Button btn = GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.AddListener(Open);
        }
    }

    public void Open()
    {
        if (!string.IsNullOrEmpty(videoUrl))
        {
            Application.OpenURL(videoUrl);
        }
    }
}
