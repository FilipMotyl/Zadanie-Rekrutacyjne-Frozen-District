using UnityEngine;

public class DragonAnimator : MonoBehaviour 
{
    [SerializeField] private Animator animator;
    private string currentAnimationName;

    private void OnEnable()
    {
        currentAnimationName = string.Empty;
    }

    public void PlayAttackAnimation() 
    {
        StopWalkingAndRunningAnimations();
        TryChangingAnimationTo("Attack_1");
    }

    public void PlayDeadAnimation() 
    {
        StopWalkingAndRunningAnimations();
        TryChangingAnimationTo("Dead");
    }

    void TryChangingAnimationTo(string clipName) 
    {
        if (currentAnimationName == clipName)
        {
            return;
        }
        animator.SetTrigger(clipName);
        currentAnimationName = clipName;
    }

    public void PlayWalkAnimation() 
    {
        GetComponent<Animator>().SetBool("IsWalk", true);
    }

    public void StopWalkingAndRunningAnimations() 
    {
        GetComponent<Animator>().SetBool("IsWalk", false);
        GetComponent<Animator>().SetBool("IsRun", false);
    }

    public void PlayRunAnimation() 
    {
        GetComponent<Animator>().SetBool("IsWalk", true);
        GetComponent<Animator>().SetBool("IsRun", true);
    }

    public string GetCurrentAnimationName()
    {
        return currentAnimationName;
    }
}