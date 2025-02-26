using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02001365 RID: 4965
public class DrawArrow : BaseLoader
{
	// Token: 0x0600A661 RID: 42593 RVA: 0x0043A5E4 File Offset: 0x004389E4
	public void Setup()
	{
		if (null == this.trfRef || null == this.tmpArrow)
		{
			return;
		}
		FindAssist findAssist = new FindAssist();
		findAssist.Initialize(this.trfRef);
		this.lstArrow.Clear();
		Transform transform = base.transform.Find("top");
		if (transform)
		{
			transform.name = "delete";
			UnityEngine.Object.Destroy(transform.gameObject);
		}
		GameObject gameObject = new GameObject("top");
		gameObject.transform.SetParent(base.transform, false);
		for (int i = 0; i < this.bonenames.Length; i++)
		{
			GameObject gameObject2;
			if (findAssist.dictObjName.TryGetValue(this.bonenames[i], out gameObject2))
			{
				GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(this.tmpArrow, gameObject.transform, false);
				gameObject3.name = this.bonenames[i];
				this.lstArrow.Add(new DrawArrow.ArrowData
				{
					trfBone = gameObject2.transform,
					trfArrow = gameObject3.transform
				});
			}
		}
	}

	// Token: 0x0600A662 RID: 42594 RVA: 0x0043A706 File Offset: 0x00438B06
	private void Reset()
	{
		this.Setup();
	}

	// Token: 0x0600A663 RID: 42595 RVA: 0x0043A710 File Offset: 0x00438B10
	private void LateUpdate()
	{
		if (this.lstArrow == null)
		{
			return;
		}
		int count = this.lstArrow.Count;
		for (int i = 0; i < count; i++)
		{
			this.lstArrow[i].trfArrow.position = this.lstArrow[i].trfBone.position;
			this.lstArrow[i].trfArrow.rotation = this.lstArrow[i].trfBone.rotation;
		}
	}

	// Token: 0x040082AF RID: 33455
	public Transform trfRef;

	// Token: 0x040082B0 RID: 33456
	public GameObject tmpArrow;

	// Token: 0x040082B1 RID: 33457
	private List<DrawArrow.ArrowData> lstArrow = new List<DrawArrow.ArrowData>();

	// Token: 0x040082B2 RID: 33458
	[Button("Setup", "初期化", new object[]
	{

	})]
	public int setup;

	// Token: 0x040082B3 RID: 33459
	private string[] bonenames = new string[]
	{
		"cf_J_ArmUp01_s_R",
		"cf_J_Mune_Nip01_s_L",
		"cf_J_Mune_Nip01_s_R",
		"cf_J_Mune_Nip02_s_L",
		"cf_J_Mune_Nip02_s_R",
		"cf_J_Mune_Nipacs01_L",
		"cf_J_Mune_Nipacs01_R",
		"cf_J_Mune00_d_L",
		"cf_J_Mune00_d_R",
		"cf_J_Mune00_s_L",
		"cf_J_Mune00_s_R",
		"cf_J_Mune00_t_L",
		"cf_J_Mune00_t_R",
		"cf_J_Mune01_s_L",
		"cf_J_Mune01_s_R",
		"cf_J_Mune01_t_L",
		"cf_J_Mune01_t_R",
		"cf_J_Mune02_s_L",
		"cf_J_Mune02_s_R",
		"cf_J_Mune02_t_L",
		"cf_J_Mune02_t_R",
		"cf_J_Mune03_s_L",
		"cf_J_Mune03_s_R",
		"cf_J_Mune04_s_L",
		"cf_J_Mune04_s_R",
		"cf_J_Shoulder02_s_L",
		"cf_J_Shoulder02_s_R",
		"cf_J_sk_siri_dam",
		"cf_J_Spine01_s",
		"cf_J_Spine02_s",
		"cf_J_Spine03_s",
		"cf_N_height"
	};

	// Token: 0x02001366 RID: 4966
	public class ArrowData
	{
		// Token: 0x040082B4 RID: 33460
		public Transform trfBone;

		// Token: 0x040082B5 RID: 33461
		public Transform trfArrow;
	}
}
