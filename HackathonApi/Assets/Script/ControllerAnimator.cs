
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ControllerAnimator : MonoBehaviour
{
    private string currentAnimaton;


    private Animator animator;



    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeAnimationState(string newAnimation,bool cheat=false)
    {
        if (!cheat && currentAnimaton == newAnimation) return;

        animator.Play(newAnimation);
        currentAnimaton = newAnimation;
    }


    public void AnimationPlay(string nameAnim)
    {
        StartCoroutine(AnimationWait(nameAnim));
    }


    public IEnumerator AnimationWait(string nameAnim)
    {
        animator.Play(nameAnim);
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName(nameAnim))
        {
            yield return null;
        }
        float counter = 0;
        while ((animator.GetCurrentAnimatorStateInfo(0).normalizedTime) % 1 < 0.99f)
        {
            counter += Time.deltaTime;
            yield return null;
        }
    }
}

