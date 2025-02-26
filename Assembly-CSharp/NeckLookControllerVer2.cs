using System;
using System.IO;
using UnityEngine;

// Token: 0x020010ED RID: 4333
[RequireComponent(typeof(NeckLookCalcVer2))]
public class NeckLookControllerVer2 : MonoBehaviour
{
	// Token: 0x06008FCD RID: 36813 RVA: 0x003BEE73 File Offset: 0x003BD273
	private void Start()
	{
		if (!this.target && Camera.main)
		{
			this.target = Camera.main.transform;
		}
	}

	// Token: 0x06008FCE RID: 36814 RVA: 0x003BEEA4 File Offset: 0x003BD2A4
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
				this.neckLookScript.NeckUpdateCalc(position2, this.ptnNo, false);
			}
		}
		else
		{
			this.neckLookScript.NeckUpdateCalc(Vector3.zero, this.ptnNo, true);
		}
	}

	// Token: 0x06008FCF RID: 36815 RVA: 0x003BEF72 File Offset: 0x003BD372
	public void ForceLateUpdate()
	{
		this.LateUpdate();
	}

	// Token: 0x06008FD0 RID: 36816 RVA: 0x003BEF7C File Offset: 0x003BD37C
	public void SaveNeckLookCtrl(BinaryWriter writer)
	{
		writer.Write(this.ptnNo);
		int num = this.neckLookScript.aBones.Length;
		writer.Write(num);
		for (int i = 0; i < num; i++)
		{
			Quaternion fixAngle = this.neckLookScript.aBones[i].fixAngle;
			writer.Write(fixAngle.x);
			writer.Write(fixAngle.y);
			writer.Write(fixAngle.z);
			writer.Write(fixAngle.w);
		}
	}

	// Token: 0x06008FD1 RID: 36817 RVA: 0x003BF004 File Offset: 0x003BD404
	public void LoadNeckLookCtrl(BinaryReader reader)
	{
		this.ptnNo = reader.ReadInt32();
		int num = reader.ReadInt32();
		for (int i = 0; i < num; i++)
		{
			Quaternion quaternion = new Quaternion(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
			this.neckLookScript.aBones[i].fixAngleBackup = (this.neckLookScript.aBones[i].fixAngle = quaternion);
			if (this.neckLookScript.aBones[i].neckBone != null)
			{
				this.neckLookScript.aBones[i].neckBone.localRotation = quaternion;
			}
			if (this.neckLookScript.aBones[i].controlBone != null)
			{
				this.neckLookScript.aBones[i].controlBone.localRotation = quaternion;
			}
		}
	}

	// Token: 0x040074A1 RID: 29857
	public NeckLookCalcVer2 neckLookScript;

	// Token: 0x040074A2 RID: 29858
	public int ptnNo;

	// Token: 0x040074A3 RID: 29859
	public Transform target;

	// Token: 0x040074A4 RID: 29860
	public float rate = 1f;
}
