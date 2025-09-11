using UnityEngine;
using UnityEngine.EventSystems;

public class SfxButton : MonoBehaviour
{
    public void PlayBubbleHover()
    {
        AudioManager.Instance.PlaySfx(AudioManager.SfxCode.hover);
    }

    // Quand on clique sur le bouton
    public void PlayBubbleClick()
    {
        AudioManager.Instance.PlaySfx(AudioManager.SfxCode.click);
    }
}
