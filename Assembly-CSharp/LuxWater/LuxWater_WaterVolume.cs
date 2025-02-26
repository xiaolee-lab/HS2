using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Rendering;

namespace LuxWater
{
	// Token: 0x020003E6 RID: 998
	public class LuxWater_WaterVolume : MonoBehaviour
	{
		// Token: 0x14000050 RID: 80
		// (add) Token: 0x060011B8 RID: 4536 RVA: 0x00069F0C File Offset: 0x0006830C
		// (remove) Token: 0x060011B9 RID: 4537 RVA: 0x00069F40 File Offset: 0x00068340
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event LuxWater_WaterVolume.TriggerEnter OnEnterWaterVolume;

		// Token: 0x14000051 RID: 81
		// (add) Token: 0x060011BA RID: 4538 RVA: 0x00069F74 File Offset: 0x00068374
		// (remove) Token: 0x060011BB RID: 4539 RVA: 0x00069FA8 File Offset: 0x000683A8
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event LuxWater_WaterVolume.TriggerExit OnExitWaterVolume;

		// Token: 0x060011BC RID: 4540 RVA: 0x00069FDC File Offset: 0x000683DC
		private void OnEnable()
		{
			if (this.WaterVolumeMesh == null)
			{
				return;
			}
			this.ID = base.GetInstanceID();
			base.Invoke("Register", 0f);
			Renderer component = base.GetComponent<Renderer>();
			component.shadowCastingMode = ShadowCastingMode.Off;
			Material sharedMaterial = component.sharedMaterial;
			sharedMaterial.EnableKeyword("USINGWATERVOLUME");
			sharedMaterial.SetFloat("_WaterSurfaceYPos", base.transform.position.y);
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x0006A055 File Offset: 0x00068455
		private void OnDisable()
		{
			if (this.waterrendermanager)
			{
				this.waterrendermanager.DeRegisterWaterVolume(this, this.ID);
			}
			this.readyToGo = false;
			base.GetComponent<Renderer>().sharedMaterial.DisableKeyword("USINGWATERVOLUME");
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x0006A098 File Offset: 0x00068498
		private void Register()
		{
			LuxWater_UnderWaterRendering instance = LuxWater_UnderWaterRendering.instance;
			if (instance != null)
			{
				this.waterrendermanager = LuxWater_UnderWaterRendering.instance;
				bool isVisible = base.GetComponent<Renderer>().isVisible;
				this.waterrendermanager.RegisterWaterVolume(this, this.ID, isVisible, this.SlidingVolume);
				this.readyToGo = true;
			}
			else
			{
				base.Invoke("Register", 0f);
			}
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x0006A103 File Offset: 0x00068503
		private void OnBecameVisible()
		{
			if (this.readyToGo)
			{
				this.waterrendermanager.SetWaterVisible(this.ID);
			}
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x0006A121 File Offset: 0x00068521
		private void OnBecameInvisible()
		{
			if (this.readyToGo)
			{
				this.waterrendermanager.SetWaterInvisible(this.ID);
			}
		}

		// Token: 0x060011C1 RID: 4545 RVA: 0x0006A140 File Offset: 0x00068540
		private void OnTriggerEnter(Collider other)
		{
			LuxWater_WaterVolumeTrigger component = other.GetComponent<LuxWater_WaterVolumeTrigger>();
			if (component != null && this.waterrendermanager != null && this.readyToGo && component.active)
			{
				this.waterrendermanager.EnteredWaterVolume(this, this.ID, component.cam, this.GridSize);
				if (LuxWater_WaterVolume.OnEnterWaterVolume != null)
				{
					LuxWater_WaterVolume.OnEnterWaterVolume();
				}
			}
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x0006A1BC File Offset: 0x000685BC
		private void OnTriggerStay(Collider other)
		{
			LuxWater_WaterVolumeTrigger component = other.GetComponent<LuxWater_WaterVolumeTrigger>();
			if (component != null && this.waterrendermanager != null && this.readyToGo && component.active)
			{
				this.waterrendermanager.EnteredWaterVolume(this, this.ID, component.cam, this.GridSize);
			}
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x0006A224 File Offset: 0x00068624
		private void OnTriggerExit(Collider other)
		{
			LuxWater_WaterVolumeTrigger component = other.GetComponent<LuxWater_WaterVolumeTrigger>();
			if (component != null && this.waterrendermanager != null && this.readyToGo && component.active)
			{
				this.waterrendermanager.LeftWaterVolume(this, this.ID, component.cam);
				if (LuxWater_WaterVolume.OnExitWaterVolume != null)
				{
					LuxWater_WaterVolume.OnExitWaterVolume();
				}
			}
		}

		// Token: 0x040013F9 RID: 5113
		[Space(6f)]
		[LuxWater_HelpBtn("h.86taxuhovssb")]
		public Mesh WaterVolumeMesh;

		// Token: 0x040013FA RID: 5114
		[Space(8f)]
		public bool SlidingVolume;

		// Token: 0x040013FB RID: 5115
		public float GridSize = 10f;

		// Token: 0x040013FC RID: 5116
		private LuxWater_UnderWaterRendering waterrendermanager;

		// Token: 0x040013FD RID: 5117
		private bool readyToGo;

		// Token: 0x040013FE RID: 5118
		private int ID;

		// Token: 0x020003E7 RID: 999
		// (Invoke) Token: 0x060011C5 RID: 4549
		public delegate void TriggerEnter();

		// Token: 0x020003E8 RID: 1000
		// (Invoke) Token: 0x060011C9 RID: 4553
		public delegate void TriggerExit();
	}
}
