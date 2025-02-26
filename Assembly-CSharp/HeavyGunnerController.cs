using System;
using System.Collections.Generic;
using SpriteToParticlesAsset;
using UnityEngine;

// Token: 0x020005A6 RID: 1446
public class HeavyGunnerController : MonoBehaviour
{
	// Token: 0x06002181 RID: 8577 RVA: 0x000B90DC File Offset: 0x000B74DC
	private void Start()
	{
		this.rig = base.GetComponent<Rigidbody2D>();
		this.animator = base.GetComponent<Animator>();
	}

	// Token: 0x06002182 RID: 8578 RVA: 0x000B90F8 File Offset: 0x000B74F8
	private void Update()
	{
		float axis = Input.GetAxis("Vertical");
		float axis2 = Input.GetAxis("Horizontal");
		Vector2 vector = new Vector2(axis2, axis);
		if (vector.magnitude > 1f)
		{
			vector.Normalize();
		}
		this.rig.velocity = new Vector3(vector.x * this.Speed, vector.y * this.Speed, 0f);
		this.animator.SetFloat("Speed", this.rig.velocity.magnitude);
		float num = Mathf.Atan2(this.LookAtAim.transform.position.y - base.transform.position.y, this.LookAtAim.transform.position.x - base.transform.position.x);
		this.wantedRotation = 57.295776f * num + this.angleDisplacement;
		Quaternion to = Quaternion.Euler(0f, 0f, this.wantedRotation);
		base.transform.rotation = Quaternion.RotateTowards(base.transform.rotation, to, this.RotationVelocity * Time.deltaTime);
		if (Input.GetMouseButton(0))
		{
			this.ShootPrep();
		}
		if (Input.GetMouseButtonUp(0))
		{
			this.Shoot();
		}
		if (Input.GetKeyDown(KeyCode.Z))
		{
			this.ShadowFXToggle();
		}
		if (Input.GetKeyDown(KeyCode.X))
		{
			this.WeirdFXToggle();
		}
	}

	// Token: 0x06002183 RID: 8579 RVA: 0x000B9294 File Offset: 0x000B7694
	public void ShootPrep()
	{
		this.ShootPrepTime += Time.deltaTime;
		if (this.ShootPrepTime > 0.1f)
		{
			if (!this.GunPrep.IsPlaying())
			{
				this.GunPrep.Play();
			}
			this.GunPrep.EmissionRate = this.ShootPrepTime * 1000f;
			if (this.GunPrep.EmissionRate > 10000f)
			{
				this.GunPrep.EmissionRate = 10000f;
			}
		}
	}

	// Token: 0x06002184 RID: 8580 RVA: 0x000B931C File Offset: 0x000B771C
	public void Shoot()
	{
		this.animator.SetTrigger("Shoot");
		this.ShootPrepTime = 0f;
		this.GunPrep.Stop();
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.BulletPrefab);
		gameObject.transform.position = this.BulletStartPos.position;
		gameObject.GetComponent<Rigidbody2D>().AddForce(base.transform.up * this.bulletSpeed);
		gameObject.GetComponent<Rigidbody2D>().AddTorque(this.bulletRotationSpeed);
		UnityEngine.Object.Destroy(gameObject, 10f);
	}

	// Token: 0x06002185 RID: 8581 RVA: 0x000B93B4 File Offset: 0x000B77B4
	public void ShadowFXToggle()
	{
		if (this.ShadowFxs[0].IsPlaying())
		{
			foreach (SpriteToParticles spriteToParticles in this.ShadowFxs)
			{
				spriteToParticles.Stop();
			}
		}
		else
		{
			foreach (SpriteToParticles spriteToParticles2 in this.ShadowFxs)
			{
				spriteToParticles2.Play();
			}
		}
	}

	// Token: 0x06002186 RID: 8582 RVA: 0x000B9474 File Offset: 0x000B7874
	public void WeirdFXToggle()
	{
		if (this.WeirdFxs[0].IsPlaying())
		{
			foreach (SpriteToParticles spriteToParticles in this.WeirdFxs)
			{
				spriteToParticles.Stop();
			}
		}
		else
		{
			foreach (SpriteToParticles spriteToParticles2 in this.WeirdFxs)
			{
				spriteToParticles2.Play();
			}
		}
	}

	// Token: 0x0400210B RID: 8459
	public List<SpriteToParticles> ShadowFxs;

	// Token: 0x0400210C RID: 8460
	public List<SpriteToParticles> WeirdFxs;

	// Token: 0x0400210D RID: 8461
	public SpriteToParticles GunPrep;

	// Token: 0x0400210E RID: 8462
	public float Speed = 20f;

	// Token: 0x0400210F RID: 8463
	public GameObject LookAtAim;

	// Token: 0x04002110 RID: 8464
	public float RotationVelocity = 5f;

	// Token: 0x04002111 RID: 8465
	private float wantedRotation;

	// Token: 0x04002112 RID: 8466
	public float angleDisplacement = 180f;

	// Token: 0x04002113 RID: 8467
	public Rigidbody2D rig;

	// Token: 0x04002114 RID: 8468
	private Animator animator;

	// Token: 0x04002115 RID: 8469
	private float ShootPrepTime;

	// Token: 0x04002116 RID: 8470
	public GameObject BulletPrefab;

	// Token: 0x04002117 RID: 8471
	public Transform BulletStartPos;

	// Token: 0x04002118 RID: 8472
	public float bulletSpeed = 20f;

	// Token: 0x04002119 RID: 8473
	public float bulletRotationSpeed = 20f;
}
