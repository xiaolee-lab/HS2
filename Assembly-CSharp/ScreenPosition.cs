using System;
using UnityEngine;

// Token: 0x02000797 RID: 1943
[ExecuteInEditMode]
public class ScreenPosition : MonoBehaviour
{
	// Token: 0x170007DB RID: 2011
	// (get) Token: 0x06002DF9 RID: 11769 RVA: 0x00104738 File Offset: 0x00102B38
	// (set) Token: 0x06002DFA RID: 11770 RVA: 0x00104740 File Offset: 0x00102B40
	public Vector3 position
	{
		get
		{
			return this._position;
		}
		set
		{
			this.isChange = true;
			this._position = value;
			base.transform.position = Camera.main.ScreenToWorldPoint(this._position);
		}
	}

	// Token: 0x06002DFB RID: 11771 RVA: 0x0010476C File Offset: 0x00102B6C
	private void Update()
	{
		if (base.transform.hasChanged && !this.isChange)
		{
			this._position = Camera.main.WorldToScreenPoint(base.transform.position);
		}
		else
		{
			this.isChange = false;
		}
	}

	// Token: 0x06002DFC RID: 11772 RVA: 0x001047BB File Offset: 0x00102BBB
	private void OnValidate()
	{
		this.position = this._position;
	}

	// Token: 0x04002CFD RID: 11517
	[SerializeField]
	private Vector3 _position = new Vector3(0f, 0f, 10f);

	// Token: 0x04002CFE RID: 11518
	private bool isChange;
}
