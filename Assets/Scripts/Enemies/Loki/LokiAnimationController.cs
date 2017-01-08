using UnityEngine;
using System.Collections;

public class LokiAnimationController : MonoBehaviour {

    private Animator _animator;

	// Use this for initialization
	void Awake () {
        _animator = GetComponent<Animator>();
	}

    public void SetIdleAnimation()
    {
        Debug.Log("RESET");
        _animator.SetInteger("animState", 0);
    }

    public void SetAttackAnimation()
    {
        StopAllCoroutines();
        StartCoroutine(ResetToIdleAnimation(0.5f));
        Debug.Log("STEP1");
        _animator.SetInteger("animState", 1);
    }


    public IEnumerator ResetToIdleAnimation(float howLong)
    {
        yield return new WaitForSeconds(howLong);
        Debug.Log("STEP2");
        SetIdleAnimation();
    }

    public void SetTeleportAnimation()
    {
        _animator.SetInteger("animState", 2);
    }

    public void SetTeleportAttackAnimation()
    {
        _animator.SetInteger("animState", 3);
    }
}
