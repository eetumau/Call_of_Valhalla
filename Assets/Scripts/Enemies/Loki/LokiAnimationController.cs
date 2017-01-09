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
        _animator.SetInteger("animState", 0);
        Debug.Log("RESETANIMATION");
    }

    public void SetAttackAnimation()
    {
        StopAllCoroutines();
        Debug.Log("ATTACK");
        StartCoroutine(ResetToIdleAnimation(0.5f));
        _animator.SetInteger("animState", 1);
    }

    public IEnumerator ResetToIdleAnimation(float howLong)
    {
        yield return new WaitForSeconds(howLong);
        SetIdleAnimation();
    }

    public void StopFight()
    {
        StartCoroutine(ResetToIdleAnimation(0));
    }

    public void SetTeleportAnimation()
    {
        StopAllCoroutines();
        _animator.SetInteger("animState", 2);
    }

    public void SetTeleportAttackAnimation()
    {
        _animator.SetInteger("animState", 3);
    }

    public void SetStunAnimation()
    {
        StopAllCoroutines();
        Debug.Log("STUNNED");
        _animator.SetInteger("animState", 4);
    }

    public void SetDeathAnimation()
    {
        StopAllCoroutines();
        Debug.Log("deathanimation");
        _animator.SetInteger("animState", 5);
    }
}
