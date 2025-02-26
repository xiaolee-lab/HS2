using System;
using UnityEngine;

// Token: 0x020010C5 RID: 4293
public class SmartTouch : MonoBehaviour
{
	// Token: 0x17001F01 RID: 7937
	// (get) Token: 0x06008F13 RID: 36627 RVA: 0x003B7918 File Offset: 0x003B5D18
	public Vector2 InPos
	{
		get
		{
			return this.inPos;
		}
	}

	// Token: 0x17001F02 RID: 7938
	// (get) Token: 0x06008F14 RID: 36628 RVA: 0x003B7920 File Offset: 0x003B5D20
	public Vector2 UpPos
	{
		get
		{
			return this.upPos;
		}
	}

	// Token: 0x17001F03 RID: 7939
	// (get) Token: 0x06008F15 RID: 36629 RVA: 0x003B7928 File Offset: 0x003B5D28
	public float Distance
	{
		get
		{
			return (!this.Tapping) ? (this.upPos - this.inPos).magnitude : 0f;
		}
	}

	// Token: 0x17001F04 RID: 7940
	// (get) Token: 0x06008F16 RID: 36630 RVA: 0x003B7963 File Offset: 0x003B5D63
	public float TapTime
	{
		get
		{
			return this.inTimer;
		}
	}

	// Token: 0x17001F05 RID: 7941
	// (get) Token: 0x06008F17 RID: 36631 RVA: 0x003B796B File Offset: 0x003B5D6B
	public bool Tapping
	{
		get
		{
			return this.inFlg;
		}
	}

	// Token: 0x17001F06 RID: 7942
	// (get) Token: 0x06008F18 RID: 36632 RVA: 0x003B7973 File Offset: 0x003B5D73
	public int TapCount
	{
		get
		{
			return this.tapCount;
		}
	}

	// Token: 0x06008F19 RID: 36633 RVA: 0x003B797B File Offset: 0x003B5D7B
	private void Start()
	{
		this.inPos = Vector2.zero;
		this.upPos = Vector2.zero;
		this.inTimer = 0f;
		this.outTimer = 0f;
		this.tapCount = 0;
		this.inFlg = false;
	}

	// Token: 0x06008F1A RID: 36634 RVA: 0x003B79B8 File Offset: 0x003B5DB8
	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			this.inPos = Input.mousePosition;
			this.inFlg = true;
			this.inTimer = 0f;
			if (this.outTimer < this.countTime)
			{
				this.tapCount++;
			}
			else
			{
				this.tapCount = 1;
			}
		}
		else if (Input.GetMouseButtonUp(0))
		{
			this.upPos = Input.mousePosition;
			this.inFlg = false;
		}
		if (this.inFlg)
		{
			this.inTimer += Time.deltaTime;
			this.outTimer = 0f;
		}
		else
		{
			this.outTimer += Time.deltaTime;
			this.outTimer = Mathf.Min(this.outTimer, this.countTime);
			if (this.outTimer == this.countTime)
			{
				this.tapCount = 0;
			}
		}
	}

	// Token: 0x04007390 RID: 29584
	public float countTime = 0.2f;

	// Token: 0x04007391 RID: 29585
	private Vector2 inPos;

	// Token: 0x04007392 RID: 29586
	private Vector2 upPos;

	// Token: 0x04007393 RID: 29587
	private float inTimer;

	// Token: 0x04007394 RID: 29588
	private float outTimer;

	// Token: 0x04007395 RID: 29589
	private int tapCount;

	// Token: 0x04007396 RID: 29590
	private bool inFlg;
}
