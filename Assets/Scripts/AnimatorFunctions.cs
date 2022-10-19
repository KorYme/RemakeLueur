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


    public void DestroyGameObject()
    {
        Destroy(objectToDestroy);
    }
}
