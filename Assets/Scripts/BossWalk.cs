using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWalk : StateMachineBehaviour
{
    [SerializeField]
    public float speed;

    Transform player;
    Rigidbody2D rig;
    
    FinalBoss boss;
    float attackRange = 4f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rig = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<FinalBoss>();

        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        boss.LookAtPlayer();
        Vector2 target = new Vector2(player.position.x, rig.position.y);
        Vector2 newPos = Vector2.MoveTowards(rig.position, target, speed * Time.deltaTime);
        rig.MovePosition(newPos);
        

        if (player.GetComponent<Player>().isAlive == true)
        {
            
            if (Vector2.Distance(player.position, rig.position) <= attackRange)
            {
                animator.SetTrigger("Attack");
               
            }
        }
        if (player.GetComponent<Player>().isAlive == false)
        {
            
            animator.SetTrigger("idle");
        }


    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }


}
