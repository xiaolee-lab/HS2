using System;
using System.Collections.Generic;
using DeepSky.Haze;
using UnityEngine;

// Token: 0x020002E7 RID: 743
public class ContextSwitcher : MonoBehaviour
{
	// Token: 0x06000C94 RID: 3220 RVA: 0x00033542 File Offset: 0x00031942
	private void Start()
	{
		this._view = base.GetComponent<DS_HazeView>();
	}

	// Token: 0x06000C95 RID: 3221 RVA: 0x00033550 File Offset: 0x00031950
	private void Update()
	{
		if (this.contexts.Count > 0 && this._view != null && Input.GetKeyUp(KeyCode.C))
		{
			this._contextIndex++;
			if (this._contextIndex == this.contexts.Count)
			{
				this._contextIndex = 0;
			}
			this._view.ContextAsset = this.contexts[this._contextIndex];
			this._view.OverrideContextAsset = true;
		}
	}

	// Token: 0x04000BA3 RID: 2979
	public List<DS_HazeContextAsset> contexts = new List<DS_HazeContextAsset>();

	// Token: 0x04000BA4 RID: 2980
	private DS_HazeView _view;

	// Token: 0x04000BA5 RID: 2981
	private int _contextIndex;
}
