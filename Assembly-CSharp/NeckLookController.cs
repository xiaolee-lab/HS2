using System;
using System.IO;
using UnityEngine;

// Token: 0x020010EC RID: 4332
public class NeckLookController : MonoBehaviour
{
	// Token: 0x06008FC7 RID: 36807 RVA: 0x003BECB8 File Offset: 0x003BD0B8
	private void Start()
	{
		if (!this.target && Camera.main)
		{
			this.target = Camera.main.transform;
		}
	}

	// Token: 0x06008FC8 RID: 36808 RVA: 0x003BECE9 File Offset: 0x003BD0E9
	private void Update()
	{
	}

	// Token: 0x06008FC9 RID: 36809 RVA: 0x003BECEC File Offset: 0x003BD0EC
	private void LateUpdate()
	{
		if (this.neckLookScript == null)
		{
			return;
		}
		this.neckLookScript.UpdateCall(this.ptnNo);
		if (this.target != null)
		{
			if (null != this.neckLookScript)
			{
				Vector3 position = base.transform.position;
				Vector3 position2 = this.target.position;
				for (int i = 0; i < 2; i++)
				{
					position2[i] = Mathf.Lerp(position[i], position2[i], this.rate);
				}
				this.neckLookScript.NeckUpdateCalc(position2, this.ptnNo);
			}
		}
		else
		{
			this.neckLookScript.NeckUpdateCalc(this.neckLookScript.backupPos, this.ptnNo);
		}
	}

	// Token: 0x06008FCA RID: 36810 RVA: 0x003BEDC0 File Offset: 0x003BD1C0
	public void SaveNeckLookCtrl(BinaryWriter writer)
	{
		writer.Write(this.ptnNo);
		Quaternion fixAngle = this.neckLookScript.fixAngle;
		writer.Write(fixAngle.x);
		writer.Write(fixAngle.y);
		writer.Write(fixAngle.z);
		writer.Write(fixAngle.w);
	}

	// Token: 0x06008FCB RID: 36811 RVA: 0x003BEE1C File Offset: 0x003BD21C
	public void LoadNeckLookCtrl(BinaryReader reader)
	{
		this.ptnNo = reader.ReadInt32();
		Quaternion fixAngle = new Quaternion(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
		this.neckLookScript.SetFixAngle(fixAngle);
	}

	// Token: 0x0400749D RID: 29853
	public NeckLookCalc neckLookScript;

	// Token: 0x0400749E RID: 29854
	public int ptnNo;

	// Token: 0x0400749F RID: 29855
	public Transform target;

	// Token: 0x040074A0 RID: 29856
	public float rate = 1f;
}
