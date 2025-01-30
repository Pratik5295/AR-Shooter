using UnityEngine;
using UnityEngine.Events;

public abstract class GameScreen : MonoBehaviour
{
    public UnityEvent OnScreenShownEvent;
    public UnityEvent OnScreenHiddenEvent;
    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
}
