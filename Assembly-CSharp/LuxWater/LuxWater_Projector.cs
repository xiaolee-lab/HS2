using System;
using System.Collections.Generic;
using UnityEngine;

namespace LuxWater
{
	// Token: 0x020003DE RID: 990
	public class LuxWater_Projector : MonoBehaviour
	{
		// Token: 0x0600118D RID: 4493 RVA: 0x00067828 File Offset: 0x00065C28
		private void Update()
		{
			base.transform.position.y = this.origPos.y;
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x00067854 File Offset: 0x00065C54
		private void OnEnable()
		{
			this.origPos = base.transform.position;
			Renderer component = base.GetComponent<Renderer>();
			if (component != null)
			{
				this.m_Rend = base.GetComponent<Renderer>();
				this.m_Mat = this.m_Rend.sharedMaterials[0];
				this.m_Rend.enabled = false;
				if (this.Type == LuxWater_Projector.ProjectorType.FoamProjector)
				{
					LuxWater_Projector.FoamProjectors.Add(this);
				}
				else
				{
					LuxWater_Projector.NormalProjectors.Add(this);
				}
				this.added = true;
			}
		}

		// Token: 0x0600118F RID: 4495 RVA: 0x000678E0 File Offset: 0x00065CE0
		private void OnDisable()
		{
			if (this.added)
			{
				if (this.Type == LuxWater_Projector.ProjectorType.FoamProjector)
				{
					LuxWater_Projector.FoamProjectors.Remove(this);
				}
				else
				{
					LuxWater_Projector.NormalProjectors.Remove(this);
				}
				this.m_Rend.enabled = true;
			}
		}

		// Token: 0x0400136D RID: 4973
		[Space(8f)]
		public LuxWater_Projector.ProjectorType Type;

		// Token: 0x0400136E RID: 4974
		[NonSerialized]
		public static List<LuxWater_Projector> FoamProjectors = new List<LuxWater_Projector>();

		// Token: 0x0400136F RID: 4975
		[NonSerialized]
		public static List<LuxWater_Projector> NormalProjectors = new List<LuxWater_Projector>();

		// Token: 0x04001370 RID: 4976
		[NonSerialized]
		public Renderer m_Rend;

		// Token: 0x04001371 RID: 4977
		[NonSerialized]
		public Material m_Mat;

		// Token: 0x04001372 RID: 4978
		private bool added;

		// Token: 0x04001373 RID: 4979
		private Vector3 origPos;

		// Token: 0x020003DF RID: 991
		public enum ProjectorType
		{
			// Token: 0x04001375 RID: 4981
			FoamProjector,
			// Token: 0x04001376 RID: 4982
			NormalProjector
		}
	}
}
