using System;
using System.Collections.Generic;

namespace UnityEngine.AI
{
	// Token: 0x0200040C RID: 1036
	[ExecuteInEditMode]
	[AddComponentMenu("Navigation/NavMeshModifier", 32)]
	[HelpURL("https://github.com/Unity-Technologies/NavMeshComponents#documentation-draft")]
	public class NavMeshModifier : MonoBehaviour
	{
		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06001290 RID: 4752 RVA: 0x0007360D File Offset: 0x00071A0D
		// (set) Token: 0x06001291 RID: 4753 RVA: 0x00073615 File Offset: 0x00071A15
		public bool overrideArea
		{
			get
			{
				return this.m_OverrideArea;
			}
			set
			{
				this.m_OverrideArea = value;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06001292 RID: 4754 RVA: 0x0007361E File Offset: 0x00071A1E
		// (set) Token: 0x06001293 RID: 4755 RVA: 0x00073626 File Offset: 0x00071A26
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

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06001294 RID: 4756 RVA: 0x0007362F File Offset: 0x00071A2F
		// (set) Token: 0x06001295 RID: 4757 RVA: 0x00073637 File Offset: 0x00071A37
		public bool ignoreFromBuild
		{
			get
			{
				return this.m_IgnoreFromBuild;
			}
			set
			{
				this.m_IgnoreFromBuild = value;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06001296 RID: 4758 RVA: 0x00073640 File Offset: 0x00071A40
		public static List<NavMeshModifier> activeModifiers
		{
			get
			{
				return NavMeshModifier.s_NavMeshModifiers;
			}
		}

		// Token: 0x06001297 RID: 4759 RVA: 0x00073647 File Offset: 0x00071A47
		private void OnEnable()
		{
			if (!NavMeshModifier.s_NavMeshModifiers.Contains(this))
			{
				NavMeshModifier.s_NavMeshModifiers.Add(this);
			}
		}

		// Token: 0x06001298 RID: 4760 RVA: 0x00073664 File Offset: 0x00071A64
		private void OnDisable()
		{
			NavMeshModifier.s_NavMeshModifiers.Remove(this);
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x00073672 File Offset: 0x00071A72
		public bool AffectsAgentType(int agentTypeID)
		{
			return this.m_AffectedAgents.Count != 0 && (this.m_AffectedAgents[0] == -1 || this.m_AffectedAgents.IndexOf(agentTypeID) != -1);
		}

		// Token: 0x04001506 RID: 5382
		[SerializeField]
		private bool m_OverrideArea;

		// Token: 0x04001507 RID: 5383
		[SerializeField]
		private int m_Area;

		// Token: 0x04001508 RID: 5384
		[SerializeField]
		private bool m_IgnoreFromBuild;

		// Token: 0x04001509 RID: 5385
		[SerializeField]
		private List<int> m_AffectedAgents = new List<int>(new int[]
		{
			-1
		});

		// Token: 0x0400150A RID: 5386
		private static readonly List<NavMeshModifier> s_NavMeshModifiers = new List<NavMeshModifier>();
	}
}
