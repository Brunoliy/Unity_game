
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
	{
	public float Vida { set; get; }
	public void OnHit(float damage, Vector2 knockback);
	}
	