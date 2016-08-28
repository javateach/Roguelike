using UnityEngine;
using System.Collections;
using System;
//This class is created from the YouTube tutorial

public class Enemy : MovingObject {


    public int playerDamage;
    public AudioClip enemyAttack1;
    public AudioClip enemyAttack2;

    private Animator animator;
    private Transform target;
    private bool skipMove;


	// Use this for initialization
	protected override void Start ()
    {
        GameManager.instance.AddEnemyToList(this);
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        base.Start();
	}

    protected override void AttemptMove<T>(int xDir, int yDir)
    {
        if(skipMove) //allows enemy to only move every other turn
        {
            skipMove = false;
            return;
        }
        base.AttemptMove<T>(xDir, yDir);
        skipMove = true;
    }

    public void MoveEnemy()
    {
        int xDir = 0;
        int yDir = 0;
        if (Mathf.Abs(target.position.x - transform.position.x) < float.Epsilon) //enemy and player are in same column
            yDir = target.position.y > transform.position.y ? 1 : -1;//Move up or down
        else
            xDir = target.position.x > transform.position.x ? 1 : -1;

        AttemptMove<Player>(xDir, yDir);

    }

    protected override void OnCantMove<T>(T component)
    {
        Player hitPlayer = component as Player;//cast the component as a player object
        if (hitPlayer != null)
        {
            animator.SetTrigger("enemyAttack");
            SoundManager.instance.RandomizeSfx(enemyAttack1, enemyAttack2);
            hitPlayer.LoseFood(playerDamage);
        }
    }
}
