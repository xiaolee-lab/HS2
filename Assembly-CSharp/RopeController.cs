using System;
using PicoGames.QuickRopes;
using UnityEngine;

// Token: 0x02000A63 RID: 2659
[RequireComponent(typeof(QuickRope))]
public class RopeController : MonoBehaviour
{
	// Token: 0x06004EC4 RID: 20164 RVA: 0x001E351C File Offset: 0x001E191C
	private void Awake()
	{
		this.rope = base.GetComponent<QuickRope>();
		if (this.rope.Spline.IsLooped)
		{
			base.enabled = false;
			return;
		}
		this.rope.minLinkCount = this.minJointCount;
		if (!this.rope.canResize)
		{
			this.rope.maxLinkCount = this.rope.Links.Length + 1;
			this.rope.canResize = true;
			this.rope.Generate();
		}
		this.rope.Links[this.rope.Links.Length - 1].Rigidbody.isKinematic = true;
	}

	// Token: 0x06004EC5 RID: 20165 RVA: 0x001E35CC File Offset: 0x001E19CC
	private void Update()
	{
		this.rope.velocityAccel = this.acceleration;
		this.rope.velocityDampen = this.dampening;
		if (Input.GetKey(KeyCode.UpArrow))
		{
			this.rope.Velocity = this.maxSpeed;
		}
		else if (Input.GetKey(KeyCode.DownArrow))
		{
			this.rope.Velocity = -this.maxSpeed;
		}
		else
		{
			this.rope.Velocity = 0f;
		}
	}

	// Token: 0x040047B6 RID: 18358
	[Min(1f)]
	public int minJointCount = 3;

	// Token: 0x040047B7 RID: 18359
	[Min(0.001f)]
	public float maxSpeed = 5f;

	// Token: 0x040047B8 RID: 18360
	[Range(0f, 1f)]
	public float acceleration = 1f;

	// Token: 0x040047B9 RID: 18361
	[Range(0.001f, 1f)]
	public float dampening = 1f;

	// Token: 0x040047BA RID: 18362
	private QuickRope rope;
}
