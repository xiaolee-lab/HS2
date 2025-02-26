using System;
using UnityEngine;

// Token: 0x020003D5 RID: 981
public class LuxWater_SetToGerstnerHeight : MonoBehaviour
{
	// Token: 0x06001167 RID: 4455 RVA: 0x00066762 File Offset: 0x00064B62
	private void Start()
	{
		this.trans = base.transform;
		LuxWaterUtils.GetGersterWavesDescription(ref this.Description, this.WaterMaterial);
	}

	// Token: 0x06001168 RID: 4456 RVA: 0x00066781 File Offset: 0x00064B81
	private void OnBecameVisible()
	{
		this.ObjectIsVisible = true;
	}

	// Token: 0x06001169 RID: 4457 RVA: 0x0006678A File Offset: 0x00064B8A
	private void OnBecameInvisible()
	{
		this.ObjectIsVisible = false;
	}

	// Token: 0x0600116A RID: 4458 RVA: 0x00066794 File Offset: 0x00064B94
	private void LateUpdate()
	{
		if (this.ObjectIsVisible || this.AddCircleAnim)
		{
			if (this.WaterMaterial == null)
			{
				return;
			}
			if (this.UpdateWaterMaterialPerFrame)
			{
				LuxWaterUtils.GetGersterWavesDescription(ref this.Description, this.WaterMaterial);
			}
			Vector3 vector = this.trans.position;
			vector -= this.Offset;
			if (this.AddCircleAnim)
			{
				vector.x += Mathf.Sin(Time.time * this.Speed) * Time.deltaTime * this.Radius;
				vector.z += Mathf.Cos(Time.time * this.Speed) * Time.deltaTime * this.Radius;
			}
			int num = this.ManagedWaterProjectors.Length;
			if (num > 0)
			{
				for (int num2 = 0; num2 != num; num2++)
				{
					Vector3 position = this.ManagedWaterProjectors[num2].position;
					position.x = vector.x;
					position.z = vector.z;
					this.ManagedWaterProjectors[num2].position = position;
				}
			}
			this.Offset = LuxWaterUtils.GetGestnerDisplacement(vector, this.Description, this.TimeOffset);
			this.Offset.x = this.Offset.x + this.Offset.x * this.Damping.x;
			this.Offset.y = this.Offset.y + this.Offset.y * this.Damping.y;
			this.Offset.z = this.Offset.z + this.Offset.z * this.Damping.z;
			this.trans.position = vector + this.Offset;
		}
	}

	// Token: 0x04001337 RID: 4919
	public Material WaterMaterial;

	// Token: 0x04001338 RID: 4920
	public Vector3 Damping = new Vector3(0.3f, 1f, 0.3f);

	// Token: 0x04001339 RID: 4921
	public float TimeOffset;

	// Token: 0x0400133A RID: 4922
	public bool UpdateWaterMaterialPerFrame;

	// Token: 0x0400133B RID: 4923
	[Space(8f)]
	public bool AddCircleAnim;

	// Token: 0x0400133C RID: 4924
	public float Radius = 6f;

	// Token: 0x0400133D RID: 4925
	public float Speed = 1f;

	// Token: 0x0400133E RID: 4926
	[Space(8f)]
	public Transform[] ManagedWaterProjectors;

	// Token: 0x0400133F RID: 4927
	[Header("Debug")]
	public float MaxDisp;

	// Token: 0x04001340 RID: 4928
	private Transform trans;

	// Token: 0x04001341 RID: 4929
	private LuxWaterUtils.GersterWavesDescription Description;

	// Token: 0x04001342 RID: 4930
	private bool ObjectIsVisible;

	// Token: 0x04001343 RID: 4931
	private Vector3 Offset = Vector3.zero;
}
