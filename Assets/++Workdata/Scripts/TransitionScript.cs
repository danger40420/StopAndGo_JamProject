using UnityEngine;
using System.Collections;

public abstract class TransitionScript : MonoBehaviour
{
    public abstract IEnumerator AnimateTransitionIn();
    public abstract IEnumerator AnimateTransitionOut();
}
