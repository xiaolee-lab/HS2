using System;
using System.Collections.Generic;

namespace UnityEngine.AI
{
	// Token: 0x0200040D RID: 1037
	[ExecuteInEditMode]
	[AddComponentMenu("Navigation/NavMeshModifierVolume", 31)]
	[HelpURL("https://github.com/Unity-Technologies/NavMeshComponents#documentation-draft")]
	public class NavMeshModifierVolume : MonoBehaviour
	{
		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600129C RID: 4764 RVA: 0x00073714 File Offset: 0x00071B14
		// (set) Token: 0x0600129D RID: 4765 RVA: 0x0007371C File Offset: 0x00071B1C
		public Vector3 size
		{
			get
			{
				return this.m_Size;
			}
			set
			{
				this.m_Size = value;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600129E RID: 4766 RVA: 0x00073725 File Offset: 0x00071B25
		// (set) Token: 0x0600129F RID: 4767 RVA: 0x0007372D File Offset: 0x00071B2D
		public Vector3 center
		{
			get
			{
				return this.m_Center;
			}
			set
			{
				this.m_Center = value;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060012A0 RID: 4768 RVA: 0x00073736 File Offset: 0x00071B36
		// (set) Token: 0x060012A1 RID: 4769 RVA: 0x0007373E File Offset: 0x00071B3E
		public int area
		{
			get
			{
				return this.m_Area;
			}
			set
			{
				this.m_Area = value;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060012A2 RID: 4770 RVA: 0x00073747 File Offset: 0x00071B47
		public static List<NavMeshModifierVolume> activeModifiers
		{
			get
			{
				return NavMeshModifierVolume.s_NavMeshModifiers;
			}
		}

		// Token: 0x060012A3 RID: 4771 RVA: 0x0007374E File Offset: 0x00071B4E
		private void OnEnable()
		{
			if (!NavMeshModifierVolume.s_NavMeshModifiers.Contains(this))
			{
				NavMeshModifierVolume.s_NavMeshModifiers.Add(this);
			}
		}

		// Token: 0x060012A4 RID: 4772 RVA: 0x0007376B File Offset: 0x00071B6B
		private void OnDisable()
		{
			NavMeshModifierVolume.s_NavMeshModifiers.Remove(this);
		}

		// Token: 0x060012A5 RID: 4773 RVA: 0x00073779 File Offset: 0x00071B79
		public bool AffectsAgentType(int agentTypeID)
		{
			return this.m_AffectedAgents.Count != 0 && (this.m_AffectedAgents[0] == -1 || this.m_AffectedAgents.IndexOf(agentTypeID) != -1);
		}

		// Token: 0x0400150B RID: 5387
		[SerializeField]
		private Vector3 m_Size = new Vector3(4f, 3f, 4f);

		// Token: 0x0400150C RID: 5388
		[SerializeField]
		private Vector3 m_Center = new Vector3(0f, 1f, 0f);

		// Token: 0x0400150D RID: 5389
		[SerializeField]
		private int m_Area;

		// Token: 0x0400150E RID: 5390
		[SerializeField]
		private List<int> m_AffectedAgents = new List<int>(new int[]
		{
			-1
		});

		// Token: 0x0400150F RID: 5391
		private static readonly List<NavMeshModifierVolume> s_NavMeshModifiers = new List<NavMeshModifierVolume>();
	}
}
