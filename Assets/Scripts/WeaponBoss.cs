using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBoss : MonoBehaviour
{
    //Arma do chefe final

    public Vector3 attackOffset;
    public float attackRange = 1f;
    public LayerMask attackMask;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Attack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;
        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);

        if (colInfo == null && player.GetComponent<Player>().isVisible == false)
        {
            player.GetComponent<Player>().isVisible = true;
            StartCoroutine(player.GetComponent<Player>().PlayerDemage(0.05f));
            Controller.current.RemoveLife(1);
        }
    }


    void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Gizmos.DrawWireSphere(pos, attackRange);
    }
}
