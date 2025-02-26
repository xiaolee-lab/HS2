using System;
using UnityEngine;

// Token: 0x020002D9 RID: 729
public class UnluckDistanceDisabler : MonoBehaviour
{
	// Token: 0x06000C34 RID: 3124 RVA: 0x0002EDCC File Offset: 0x0002D1CC
	public void Start()
	{
		if (this._distanceFromMainCam)
		{
			this._distanceFrom = Camera.main.transform;
		}
		base.InvokeRepeating("CheckDisable", this._disableCheckInterval + UnityEngine.Random.value * this._disableCheckInterval, this._disableCheckInterval);
		base.InvokeRepeating("CheckEnable", this._enableCheckInterval + UnityEngine.Random.value * this._enableCheckInterval, this._enableCheckInterval);
		base.Invoke("DisableOnStart", 0.01f);
	}

	// Token: 0x06000C35 RID: 3125 RVA: 0x0002EE4C File Offset: 0x0002D24C
	public void DisableOnStart()
	{
		if (this._disableOnStart)
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x06000C36 RID: 3126 RVA: 0x0002EE68 File Offset: 0x0002D268
	public void CheckDisable()
	{
		if (base.gameObject.activeInHierarchy && (base.transform.position - this._distanceFrom.position).sqrMagnitude > (float)(this._distanceDisable * this._distanceDisable))
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x06000C37 RID: 3127 RVA: 0x0002EEC8 File Offset: 0x0002D2C8
	public void CheckEnable()
	{
		if (!base.gameObject.activeInHierarchy && (base.transform.position - this._distanceFrom.position).sqrMagnitude < (float)(this._distanceDisable * this._distanceDisable))
		{
			base.gameObject.SetActive(true);
		}
	}

	// Token: 0x04000AE0 RID: 2784
	public int _distanceDisable = 1000;

	// Token: 0x04000AE1 RID: 2785
	public Transform _distanceFrom;

	// Token: 0x04000AE2 RID: 2786
	public bool _distanceFromMainCam;

	// Token: 0x04000AE3 RID: 2787
	public float _disableCheckInterval = 10f;

	// Token: 0x04000AE4 RID: 2788
	public float _enableCheckInterval = 1f;

	// Token: 0x04000AE5 RID: 2789
	public bool _disableOnStart;
}
