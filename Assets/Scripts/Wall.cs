﻿using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {

    public Sprite dmgSprite;
    public int hp = 4;
    public AudioClip wallSound1;
    public AudioClip wallSound2;

    private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start ()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	public void DamageWall(int loss)
    {
        spriteRenderer.sprite = dmgSprite;
        hp -= loss;
        SoundManager.instance.RandomizeSfx(wallSound1, wallSound2);
        if (hp <= 0)
            gameObject.SetActive(false);
    }
}