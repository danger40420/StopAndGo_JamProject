using UnityEngine;
using System.Collections;
using DG.Tweening;

public class fadetransition : TransitionScript
{
    public CanvasGroup Fadetransition;

    public override IEnumerator AnimateTransitionIn()
    {
        var tweener = Fadetransition.DOFade(1f,1f);
        yield return tweener.WaitForCompletion();
    }
    public override IEnumerator AnimateTransitionOut()
    {
        var tweener = Fadetransition.DOFade(0f,1f);
        yield return tweener.WaitForCompletion();
    }
}
