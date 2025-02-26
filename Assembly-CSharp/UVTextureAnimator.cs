using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000467 RID: 1127
internal class UVTextureAnimator : MonoBehaviour
{
	// Token: 0x060014BA RID: 5306 RVA: 0x00081C0A File Offset: 0x0008000A
	private void Start()
	{
		this.InitMaterial();
		this.InitDefaultVariables();
		this.isInizialised = true;
		this.isVisible = true;
		base.StartCoroutine(this.UpdateCorutine());
	}

	// Token: 0x060014BB RID: 5307 RVA: 0x00081C33 File Offset: 0x00080033
	public void SetInstanceMaterial(Material mat, Vector2 offsetMat)
	{
		this.instanceMaterial = mat;
		this.InitDefaultVariables();
	}

	// Token: 0x060014BC RID: 5308 RVA: 0x00081C44 File Offset: 0x00080044
	private void InitDefaultVariables()
	{
		this.allCount = 0;
		this.deltaFps = 1f / this.Fps;
		this.count = this.Rows * this.Columns;
		this.index = this.Columns - 1;
		Vector2 value = new Vector2((float)this.index / (float)this.Columns - (float)(this.index / this.Columns), 1f - (float)(this.index / this.Columns) / (float)this.Rows);
		this.OffsetMat = (this.IsRandomOffsetForInctance ? UnityEngine.Random.Range(0, this.count) : (this.OffsetMat - this.OffsetMat / this.count * this.count));
		Vector2 value2 = (!(this.SelfTiling == Vector2.zero)) ? this.SelfTiling : new Vector2(1f / (float)this.Columns, 1f / (float)this.Rows);
		if (this.AnimatedMaterialsNotInstance.Length > 0)
		{
			foreach (Material material in this.AnimatedMaterialsNotInstance)
			{
				material.SetTextureScale("_MainTex", value2);
				material.SetTextureOffset("_MainTex", Vector2.zero);
				if (this.IsBump)
				{
					material.SetTextureScale("_BumpMap", value2);
					material.SetTextureOffset("_BumpMap", Vector2.zero);
				}
				if (this.IsHeight)
				{
					material.SetTextureScale("_HeightMap", value2);
					material.SetTextureOffset("_HeightMap", Vector2.zero);
				}
				if (this.IsCutOut)
				{
					material.SetTextureScale("_CutOut", value2);
					material.SetTextureOffset("_CutOut", Vector2.zero);
				}
			}
		}
		else if (this.instanceMaterial != null)
		{
			this.instanceMaterial.SetTextureScale("_MainTex", value2);
			this.instanceMaterial.SetTextureOffset("_MainTex", value);
			if (this.IsBump)
			{
				this.instanceMaterial.SetTextureScale("_BumpMap", value2);
				this.instanceMaterial.SetTextureOffset("_BumpMap", value);
			}
			if (this.IsBump)
			{
				this.instanceMaterial.SetTextureScale("_HeightMap", value2);
				this.instanceMaterial.SetTextureOffset("_HeightMap", value);
			}
			if (this.IsCutOut)
			{
				this.instanceMaterial.SetTextureScale("_CutOut", value2);
				this.instanceMaterial.SetTextureOffset("_CutOut", value);
			}
		}
		else if (this.currentRenderer != null)
		{
			this.currentRenderer.material.SetTextureScale("_MainTex", value2);
			this.currentRenderer.material.SetTextureOffset("_MainTex", value);
			if (this.IsBump)
			{
				this.currentRenderer.material.SetTextureScale("_BumpMap", value2);
				this.currentRenderer.material.SetTextureOffset("_BumpMap", value);
			}
			if (this.IsHeight)
			{
				this.currentRenderer.material.SetTextureScale("_HeightMap", value2);
				this.currentRenderer.material.SetTextureOffset("_HeightMap", value);
			}
			if (this.IsCutOut)
			{
				this.currentRenderer.material.SetTextureScale("_CutOut", value2);
				this.currentRenderer.material.SetTextureOffset("_CutOut", value);
			}
		}
	}

	// Token: 0x060014BD RID: 5309 RVA: 0x00081FB4 File Offset: 0x000803B4
	private void InitMaterial()
	{
		if (base.GetComponent<Renderer>() != null)
		{
			this.currentRenderer = base.GetComponent<Renderer>();
		}
		else
		{
			Projector component = base.GetComponent<Projector>();
			if (component != null)
			{
				if (!component.material.name.EndsWith("(Instance)"))
				{
					component.material = new Material(component.material)
					{
						name = component.material.name + " (Instance)"
					};
				}
				this.instanceMaterial = component.material;
			}
		}
	}

	// Token: 0x060014BE RID: 5310 RVA: 0x0008204A File Offset: 0x0008044A
	private void OnEnable()
	{
		if (!this.isInizialised)
		{
			return;
		}
		this.InitDefaultVariables();
		this.isVisible = true;
		if (!this.isCorutineStarted)
		{
			base.StartCoroutine(this.UpdateCorutine());
		}
	}

	// Token: 0x060014BF RID: 5311 RVA: 0x0008207D File Offset: 0x0008047D
	private void OnDisable()
	{
		this.isCorutineStarted = false;
		this.isVisible = false;
		base.StopAllCoroutines();
	}

	// Token: 0x060014C0 RID: 5312 RVA: 0x00082093 File Offset: 0x00080493
	private void OnBecameVisible()
	{
		this.isVisible = true;
		if (!this.isCorutineStarted)
		{
			base.StartCoroutine(this.UpdateCorutine());
		}
	}

	// Token: 0x060014C1 RID: 5313 RVA: 0x000820B4 File Offset: 0x000804B4
	private void OnBecameInvisible()
	{
		this.isVisible = false;
	}

	// Token: 0x060014C2 RID: 5314 RVA: 0x000820C0 File Offset: 0x000804C0
	private IEnumerator UpdateCorutine()
	{
		this.isCorutineStarted = true;
		while (this.isVisible && (this.IsLoop || this.allCount != this.count))
		{
			this.UpdateCorutineFrame();
			if (!this.IsLoop && this.allCount == this.count)
			{
				break;
			}
			yield return new WaitForSeconds(this.deltaFps);
		}
		this.isCorutineStarted = false;
		yield break;
	}

	// Token: 0x060014C3 RID: 5315 RVA: 0x000820DC File Offset: 0x000804DC
	private void UpdateCorutineFrame()
	{
		if (this.currentRenderer == null && this.instanceMaterial == null && this.AnimatedMaterialsNotInstance.Length == 0)
		{
			return;
		}
		this.allCount++;
		if (this.IsReverse)
		{
			this.index--;
		}
		else
		{
			this.index++;
		}
		if (this.index >= this.count)
		{
			this.index = 0;
		}
		if (this.AnimatedMaterialsNotInstance.Length > 0)
		{
			for (int i = 0; i < this.AnimatedMaterialsNotInstance.Length; i++)
			{
				int num = i * this.OffsetMat + this.index + this.OffsetMat;
				num -= num / this.count * this.count;
				Vector2 value = new Vector2((float)num / (float)this.Columns - (float)(num / this.Columns), 1f - (float)(num / this.Columns) / (float)this.Rows);
				this.AnimatedMaterialsNotInstance[i].SetTextureOffset("_MainTex", value);
				if (this.IsBump)
				{
					this.AnimatedMaterialsNotInstance[i].SetTextureOffset("_BumpMap", value);
				}
				if (this.IsHeight)
				{
					this.AnimatedMaterialsNotInstance[i].SetTextureOffset("_HeightMap", value);
				}
				if (this.IsCutOut)
				{
					this.AnimatedMaterialsNotInstance[i].SetTextureOffset("_CutOut", value);
				}
			}
		}
		else
		{
			Vector2 value2;
			if (this.IsRandomOffsetForInctance)
			{
				int num2 = this.index + this.OffsetMat;
				value2 = new Vector2((float)num2 / (float)this.Columns - (float)(num2 / this.Columns), 1f - (float)(num2 / this.Columns) / (float)this.Rows);
			}
			else
			{
				value2 = new Vector2((float)this.index / (float)this.Columns - (float)(this.index / this.Columns), 1f - (float)(this.index / this.Columns) / (float)this.Rows);
			}
			if (this.instanceMaterial != null)
			{
				this.instanceMaterial.SetTextureOffset("_MainTex", value2);
				if (this.IsBump)
				{
					this.instanceMaterial.SetTextureOffset("_BumpMap", value2);
				}
				if (this.IsHeight)
				{
					this.instanceMaterial.SetTextureOffset("_HeightMap", value2);
				}
				if (this.IsCutOut)
				{
					this.instanceMaterial.SetTextureOffset("_CutOut", value2);
				}
			}
			else if (this.currentRenderer != null)
			{
				this.currentRenderer.material.SetTextureOffset("_MainTex", value2);
				if (this.IsBump)
				{
					this.currentRenderer.material.SetTextureOffset("_BumpMap", value2);
				}
				if (this.IsHeight)
				{
					this.currentRenderer.material.SetTextureOffset("_HeightMap", value2);
				}
				if (this.IsCutOut)
				{
					this.currentRenderer.material.SetTextureOffset("_CutOut", value2);
				}
			}
		}
	}

	// Token: 0x040017D8 RID: 6104
	public Material[] AnimatedMaterialsNotInstance;

	// Token: 0x040017D9 RID: 6105
	public int Rows = 4;

	// Token: 0x040017DA RID: 6106
	public int Columns = 4;

	// Token: 0x040017DB RID: 6107
	public float Fps = 20f;

	// Token: 0x040017DC RID: 6108
	public int OffsetMat;

	// Token: 0x040017DD RID: 6109
	public Vector2 SelfTiling = default(Vector2);

	// Token: 0x040017DE RID: 6110
	public bool IsLoop = true;

	// Token: 0x040017DF RID: 6111
	public bool IsReverse;

	// Token: 0x040017E0 RID: 6112
	public bool IsRandomOffsetForInctance;

	// Token: 0x040017E1 RID: 6113
	public bool IsBump;

	// Token: 0x040017E2 RID: 6114
	public bool IsHeight;

	// Token: 0x040017E3 RID: 6115
	public bool IsCutOut;

	// Token: 0x040017E4 RID: 6116
	private bool isInizialised;

	// Token: 0x040017E5 RID: 6117
	private int index;

	// Token: 0x040017E6 RID: 6118
	private int count;

	// Token: 0x040017E7 RID: 6119
	private int allCount;

	// Token: 0x040017E8 RID: 6120
	private float deltaFps;

	// Token: 0x040017E9 RID: 6121
	private bool isVisible;

	// Token: 0x040017EA RID: 6122
	private bool isCorutineStarted;

	// Token: 0x040017EB RID: 6123
	private Renderer currentRenderer;

	// Token: 0x040017EC RID: 6124
	private Material instanceMaterial;
}
