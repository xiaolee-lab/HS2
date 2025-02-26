using System;
using UnityEngine;

// Token: 0x020010C1 RID: 4289
public class RenderCrossFade : BaseRenderCrossFade
{
	// Token: 0x17001EFC RID: 7932
	// (get) Token: 0x06008F00 RID: 36608 RVA: 0x003B7617 File Offset: 0x003B5A17
	public bool IsEnd
	{
		get
		{
			return this.state == RenderCrossFade.State.None;
		}
	}

	// Token: 0x17001EFD RID: 7933
	// (get) Token: 0x06008F01 RID: 36609 RVA: 0x003B7622 File Offset: 0x003B5A22
	// (set) Token: 0x06008F02 RID: 36610 RVA: 0x003B762A File Offset: 0x003B5A2A
	public RenderCrossFade.State state { get; set; }

	// Token: 0x17001EFE RID: 7934
	// (get) Token: 0x06008F03 RID: 36611 RVA: 0x003B7633 File Offset: 0x003B5A33
	// (set) Token: 0x06008F04 RID: 36612 RVA: 0x003B763B File Offset: 0x003B5A3B
	public bool isMyUpdateCap { get; set; }

	// Token: 0x06008F05 RID: 36613 RVA: 0x003B7644 File Offset: 0x003B5A44
	public void SubAlphaStart()
	{
		this.isSubAlpha = true;
	}

	// Token: 0x06008F06 RID: 36614 RVA: 0x003B764D File Offset: 0x003B5A4D
	public void Set()
	{
		this.state = RenderCrossFade.State.Ready;
		this.isSubAlpha = false;
	}

	// Token: 0x06008F07 RID: 36615 RVA: 0x003B765D File Offset: 0x003B5A5D
	public override void End()
	{
		base.End();
		this.state = RenderCrossFade.State.None;
	}

	// Token: 0x06008F08 RID: 36616 RVA: 0x003B766C File Offset: 0x003B5A6C
	protected override void Awake()
	{
		base.Awake();
		this.isInitRenderSetting = false;
		this.state = RenderCrossFade.State.None;
		this.isMyUpdateCap = true;
		this.isAlphaAdd = false;
		base.alpha = 0f;
		this.timer = 0f;
	}

	// Token: 0x06008F09 RID: 36617 RVA: 0x003B76A6 File Offset: 0x003B5AA6
	protected override void Update()
	{
		if (!this.isMyUpdateCap)
		{
			return;
		}
		this.UpdateCalc();
	}

	// Token: 0x06008F0A RID: 36618 RVA: 0x003B76BA File Offset: 0x003B5ABA
	public void UpdateDrawer()
	{
		if (this.isMyUpdateCap)
		{
			return;
		}
		this.UpdateCalc();
	}

	// Token: 0x06008F0B RID: 36619 RVA: 0x003B76D0 File Offset: 0x003B5AD0
	private void UpdateCalc()
	{
		if (this.state == RenderCrossFade.State.Ready)
		{
			base.Capture();
			this.state = RenderCrossFade.State.Draw;
		}
		if (this.state == RenderCrossFade.State.Draw && this.isSubAlpha)
		{
			this.timer += Time.deltaTime;
			base.AlphaCalc();
			if (this.timer > this.maxTime)
			{
				this.state = RenderCrossFade.State.None;
			}
		}
	}

	// Token: 0x06008F0C RID: 36620 RVA: 0x003B7740 File Offset: 0x003B5B40
	protected override void OnGUI()
	{
		if (this.state == RenderCrossFade.State.Draw)
		{
			GUI.depth = 10;
			GUI.color = new Color(1f, 1f, 1f, base.alpha);
			GUI.DrawTexture(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), this.texture);
			base.isDrawGUI = true;
		}
	}

	// Token: 0x04007383 RID: 29571
	private bool isSubAlpha;

	// Token: 0x020010C2 RID: 4290
	public enum State
	{
		// Token: 0x04007385 RID: 29573
		None,
		// Token: 0x04007386 RID: 29574
		Ready,
		// Token: 0x04007387 RID: 29575
		Draw
	}
}
