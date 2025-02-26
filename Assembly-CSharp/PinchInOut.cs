using System;
using UnityEngine;

// Token: 0x020010C3 RID: 4291
public class PinchInOut : MonoBehaviour
{
	// Token: 0x17001EFF RID: 7935
	// (get) Token: 0x06008F0E RID: 36622 RVA: 0x003B77BF File Offset: 0x003B5BBF
	public PinchInOut.State NowState
	{
		get
		{
			return this.nowState;
		}
	}

	// Token: 0x17001F00 RID: 7936
	// (get) Token: 0x06008F0F RID: 36623 RVA: 0x003B77C7 File Offset: 0x003B5BC7
	public float Rate
	{
		get
		{
			return this.scaleRate;
		}
	}

	// Token: 0x06008F10 RID: 36624 RVA: 0x003B77CF File Offset: 0x003B5BCF
	private void Start()
	{
		this.nowState = PinchInOut.State.NONE;
		this.scaleRate = 0f;
		this.prevDist = 0f;
	}

	// Token: 0x06008F11 RID: 36625 RVA: 0x003B77F0 File Offset: 0x003B5BF0
	private void Update()
	{
		if (Input.touchCount != 2)
		{
			this.nowState = PinchInOut.State.NONE;
			return;
		}
		TouchPhase phase = Input.GetTouch(1).phase;
		if (phase != TouchPhase.Began)
		{
			if (phase != TouchPhase.Moved)
			{
				if (phase == TouchPhase.Stationary)
				{
					this.scaleRate = 0f;
					this.nowState = PinchInOut.State.NONE;
				}
			}
			else
			{
				float magnitude = (Input.GetTouch(1).position - Input.GetTouch(0).position).magnitude;
				float num = Input.GetTouch(1).deltaPosition.magnitude + Input.GetTouch(0).deltaPosition.magnitude;
				if (this.prevDist > magnitude)
				{
					this.nowState = PinchInOut.State.ScalDown;
				}
				else if (this.prevDist < magnitude)
				{
					this.nowState = PinchInOut.State.ScalUp;
				}
				else
				{
					this.nowState = PinchInOut.State.NONE;
				}
				this.scaleRate = num * this.moveSpeed;
				this.prevDist = magnitude;
			}
		}
	}

	// Token: 0x04007388 RID: 29576
	public float moveSpeed = 0.01f;

	// Token: 0x04007389 RID: 29577
	private PinchInOut.State nowState;

	// Token: 0x0400738A RID: 29578
	private float scaleRate;

	// Token: 0x0400738B RID: 29579
	private float prevDist;

	// Token: 0x020010C4 RID: 4292
	public enum State
	{
		// Token: 0x0400738D RID: 29581
		NONE,
		// Token: 0x0400738E RID: 29582
		ScalUp,
		// Token: 0x0400738F RID: 29583
		ScalDown
	}
}
