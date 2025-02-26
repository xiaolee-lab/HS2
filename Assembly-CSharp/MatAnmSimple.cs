using System;
using UnityEngine;

// Token: 0x0200110D RID: 4365
public class MatAnmSimple : MonoBehaviour
{
	// Token: 0x060090D6 RID: 37078 RVA: 0x003C4ABE File Offset: 0x003C2EBE
	private void Awake()
	{
		this._Color = Shader.PropertyToID("_Color");
	}

	// Token: 0x060090D7 RID: 37079 RVA: 0x003C4AD0 File Offset: 0x003C2ED0
	private void Start()
	{
	}

	// Token: 0x060090D8 RID: 37080 RVA: 0x003C4AD4 File Offset: 0x003C2ED4
	private void Update()
	{
		foreach (MatAnmSimpleInfo matAnmSimpleInfo in this.ptn)
		{
			if (!(null == matAnmSimpleInfo.mr))
			{
				matAnmSimpleInfo.Update(this._Color);
			}
		}
	}

	// Token: 0x04007568 RID: 30056
	public MatAnmSimpleInfo[] ptn;

	// Token: 0x04007569 RID: 30057
	private int _Color;
}
