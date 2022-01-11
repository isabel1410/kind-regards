using System.Collections;
using UnityEngine;

/// <summary>
/// Handles animations for the room <see cref="GameObject"/>.
/// </summary>
public class RoomAnimation : MonoBehaviour
{
    private Animator animator;
    [SerializeField]
    private AnimationClip[] cloud1Animations;
    [SerializeField]
    private AnimationClip[] cloud2Animations;

    /// <summary>
    /// Called before the end of the first frame
    /// </summary>
    private void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(StartOffset());
    }

    /// <summary>
    /// Initiates all animations with a random offset in a random order.
    /// </summary>
    /// <returns></returns>
    private IEnumerator StartOffset()
    {
        System.Collections.Generic.List<System.Action> animationPlayFunctions = new System.Collections.Generic.List<System.Action>
        {
            delegate{ PlayCloud1Animation(); },
            delegate{ PlayCloud2Animation(); }
        };
        while (animationPlayFunctions.Count != 0)
        {
            System.Action animationPlayFunction = animationPlayFunctions[Random.Range(0, animationPlayFunctions.Count)];
            animationPlayFunctions.Remove(animationPlayFunction);
            animationPlayFunction.Invoke();
            yield return new WaitForSeconds(Random.Range(0, 5));
        }
    }

    /// <summary>
    /// Picks a random animation belonging to the Cloud 1 <see cref="GameObject"/> and plays it with a random offset.
    /// </summary>
    public void PlayCloud1Animation()
    {
        AnimationClip animation = cloud1Animations[Random.Range(0, cloud1Animations.Length)];
        StartCoroutine(StartCloudAnimation(animation));
    }

    /// <summary>
    /// Picks a random animation belonging to the Cloud 2 <see cref="GameObject"/> and plays it with a random offset.
    /// </summary>
    public void PlayCloud2Animation()
    {
        AnimationClip animation = cloud2Animations[Random.Range(0, cloud2Animations.Length)];
        StartCoroutine(StartCloudAnimation(animation));
    }

    /// <summary>
    /// Plays <paramref name="animation"/> with a random offset.
    /// </summary>
    /// <param name="animation">The <see cref="AnimationClip"/> to play.</param>
    /// <returns></returns>
    private IEnumerator StartCloudAnimation(AnimationClip animation)
    {
        yield return new WaitForSeconds(Random.Range(0, 10));
        animator.Play(animation.name);
    }
}
