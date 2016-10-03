using UnityEngine;
using System.Collections;

public class BasicEnemy_WallCheck : MonoBehaviour {

    private BasicEnemy_Movement _enemyMovement;
    private BasicEnemy_AI _enemyAI;
    private Transform _transform;

    private void Start()
    {
        _enemyMovement = GetComponentInParent<BasicEnemy_Movement>();
        _enemyAI = GetComponentInParent<BasicEnemy_AI>();
        _transform = GetComponent<Transform>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag == "Ground")
        {

            _enemyMovement.ChangeDirection();
        }
    }

    public bool CheckRight()
    {
        var allButIgnoreLinecast = ~(1 << 8);
        var blocked = Physics2D.Raycast(_transform.position, _transform.right, 2, allButIgnoreLinecast);
        return blocked;
    }

    public bool CheckLeft()
    {
        var allButIgnoreLinecast = ~(1 << 8);
        var blocked = Physics2D.Linecast(_transform.position, -_transform.right, 2, allButIgnoreLinecast);
        return blocked;
    }
}
