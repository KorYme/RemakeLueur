using UnityEngine;

public class AnimatorFunctions : MonoBehaviour
{
    public enum AnimatorBehaviours
    {
        None,
        Destroy,
    }

    [SerializeField] private AnimatorBehaviours animatorBehaviours;
    [DrawIf("animatorBehaviours", AnimatorBehaviours.Destroy)][SerializeField] private GameObject objectToDestroy;

    public void AnimationFunction()
    {
        switch (animatorBehaviours)
        {
            case AnimatorBehaviours.None:
                break;
            case AnimatorBehaviours.Destroy:
                Destroy(objectToDestroy);
                break;
            default:
                break;
        }
    }
}
