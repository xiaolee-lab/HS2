using System;
using UnityEngine;

// Token: 0x02001169 RID: 4457
public class ChangePtnAnime : MonoBehaviour
{
	// Token: 0x06009329 RID: 37673 RVA: 0x003D0665 File Offset: 0x003CEA65
	private void Start()
	{
	}

	// Token: 0x0600932A RID: 37674 RVA: 0x003D0668 File Offset: 0x003CEA68
	public void Init(Texture[] tex)
	{
		this.TexChange = new Texture[tex.Length];
		for (int i = 0; i < tex.Length; i++)
		{
			this.TexChange[i] = tex[i];
		}
	}

	// Token: 0x0600932B RID: 37675 RVA: 0x003D06A4 File Offset: 0x003CEAA4
	private void Update()
	{
		if (base.GetComponent<Renderer>())
		{
			int num = (int)((long)((double)Time.timeSinceLevelLoad * 1000.0) % 1000000L) / this.ChangeTime;
			if (this.MatChange != null && this.MatChange.Length != 0)
			{
				int num2 = num % this.MatChange.Length;
				if (this.indexM != num2)
				{
					this.indexM = num2;
					base.GetComponent<Renderer>().sharedMaterial = this.MatChange[this.indexM];
				}
			}
			if (this.TexChange != null && this.TexChange.Length != 0)
			{
				int num3 = num % this.TexChange.Length;
				if (this.indexT != num3)
				{
					this.indexT = num3;
					base.GetComponent<Renderer>().material.mainTexture = this.TexChange[this.indexT];
				}
			}
		}
	}

	// Token: 0x04007701 RID: 30465
	public Material[] MatChange;

	// Token: 0x04007702 RID: 30466
	public Texture[] TexChange;

	// Token: 0x04007703 RID: 30467
	public int ChangeTime = 200;

	// Token: 0x04007704 RID: 30468
	private int indexT;

	// Token: 0x04007705 RID: 30469
	private int indexM;
}
