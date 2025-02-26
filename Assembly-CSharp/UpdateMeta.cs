using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000AFA RID: 2810
public class UpdateMeta : MonoBehaviour
{
	// Token: 0x060051E8 RID: 20968 RVA: 0x0021D59C File Offset: 0x0021B99C
	private void Update()
	{
		for (int i = 0; i < this.lstShoot.Count; i++)
		{
			this.lstShoot[i].ShootMetaBall();
		}
	}

	// Token: 0x060051E9 RID: 20969 RVA: 0x0021D5D8 File Offset: 0x0021B9D8
	private void LateUpdate()
	{
		this.isCreate = false;
		for (int i = 0; i < this.lstShoot.Count; i++)
		{
			if (this.lstShoot[i].IsCreate())
			{
				this.isCreate = true;
				break;
			}
		}
		if (this.metaseed && this.isCreate)
		{
			this.metaseed.CreateMesh();
		}
	}

	// Token: 0x060051EA RID: 20970 RVA: 0x0021D658 File Offset: 0x0021BA58
	public void ConstMetaMesh()
	{
		bool flag = false;
		for (int i = 0; i < this.lstShoot.Count; i++)
		{
			if (this.lstShoot[i].isConstMetaMesh)
			{
				flag = true;
				break;
			}
		}
		if (flag && !this.isCreate)
		{
			for (int j = 0; j < this.lstShoot.Count; j++)
			{
				if (this.lstShoot[j].isEnable && this.lstShoot[j].isConstMetaMesh)
				{
					if (this.objStaticMesh.transform.parent != this.lstShoot[j].parentTransform)
					{
						this.objStaticMesh.transform.SetParent(this.lstShoot[j].parentTransform);
					}
					break;
				}
			}
		}
		else
		{
			this.StaticMeshParentInit();
		}
	}

	// Token: 0x060051EB RID: 20971 RVA: 0x0021D760 File Offset: 0x0021BB60
	public void MetaBallClear(bool end = false)
	{
		for (int i = 0; i < this.lstShoot.Count; i++)
		{
			this.lstShoot[i].MetaBallClear();
		}
		this.StaticMeshParentInit();
		if (!end)
		{
			base.StartCoroutine(this.CreateMesh());
		}
		else
		{
			this.CreateEndMesh();
		}
	}

	// Token: 0x060051EC RID: 20972 RVA: 0x0021D7C0 File Offset: 0x0021BBC0
	private bool StaticMeshParentInit()
	{
		if (!this.objStaticMesh)
		{
			return false;
		}
		if (this.objStaticMesh.transform.parent == this.metaseed.transform)
		{
			return true;
		}
		this.objStaticMesh.transform.SetParent(this.metaseed.transform, false);
		this.objStaticMesh.transform.localPosition = Vector3.zero;
		this.objStaticMesh.transform.localRotation = Quaternion.identity;
		this.objStaticMesh.transform.localScale = Vector3.one;
		return true;
	}

	// Token: 0x060051ED RID: 20973 RVA: 0x0021D864 File Offset: 0x0021BC64
	private IEnumerator CreateMesh()
	{
		yield return this.wait;
		if (this.metaseed)
		{
			this.metaseed.CreateMesh();
		}
		yield return null;
		yield break;
	}

	// Token: 0x060051EE RID: 20974 RVA: 0x0021D87F File Offset: 0x0021BC7F
	private void CreateEndMesh()
	{
		if (this.metaseed)
		{
			this.metaseed.CreateMesh();
		}
	}

	// Token: 0x04004C9E RID: 19614
	public List<MetaballShoot> lstShoot = new List<MetaballShoot>();

	// Token: 0x04004C9F RID: 19615
	[Tooltip("StaticMetaballSeedScript")]
	public StaticMetaballSeed metaseed;

	// Token: 0x04004CA0 RID: 19616
	[Tooltip("StaticMesh")]
	public GameObject objStaticMesh;

	// Token: 0x04004CA1 RID: 19617
	[SerializeField]
	private bool isCreate;

	// Token: 0x04004CA2 RID: 19618
	private WaitForSeconds wait = new WaitForSeconds(0.001f);
}
