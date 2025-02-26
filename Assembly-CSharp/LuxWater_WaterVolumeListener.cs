using System;
using LuxWater;
using UnityEngine;

// Token: 0x020003D6 RID: 982
public class LuxWater_WaterVolumeListener : MonoBehaviour
{
	// Token: 0x0600116C RID: 4460 RVA: 0x0006696D File Offset: 0x00064D6D
	private void OnEnable()
	{
		LuxWater_WaterVolume.OnEnterWaterVolume += this.Enter;
		LuxWater_WaterVolume.OnExitWaterVolume += this.Exit;
	}

	// Token: 0x0600116D RID: 4461 RVA: 0x00066991 File Offset: 0x00064D91
	private void OnDisable()
	{
		LuxWater_WaterVolume.OnEnterWaterVolume -= this.Enter;
		LuxWater_WaterVolume.OnExitWaterVolume -= this.Exit;
	}

	// Token: 0x0600116E RID: 4462 RVA: 0x000669B5 File Offset: 0x00064DB5
	private void Enter()
	{
	}

	// Token: 0x0600116F RID: 4463 RVA: 0x000669B7 File Offset: 0x00064DB7
	private void Exit()
	{
	}
}
