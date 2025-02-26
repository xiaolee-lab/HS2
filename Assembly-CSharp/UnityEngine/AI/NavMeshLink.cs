using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityEngine.AI
{
	// Token: 0x0200040B RID: 1035
	[ExecuteInEditMode]
	[DefaultExecutionOrder(-101)]
	[AddComponentMenu("Navigation/NavMeshLink", 33)]
	[HelpURL("https://github.com/Unity-Technologies/NavMeshComponents#documentation-draft")]
	public class NavMeshLink : MonoBehaviour
	{
		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06001274 RID: 4724 RVA: 0x00073282 File Offset: 0x00071682
		// (set) Token: 0x06001275 RID: 4725 RVA: 0x0007328A File Offset: 0x0007168A
		public int agentTypeID
		{
			get
			{
				return this.m_AgentTypeID;
			}
			set
			{
				this.m_AgentTypeID = value;
				this.UpdateLink();
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06001276 RID: 4726 RVA: 0x00073299 File Offset: 0x00071699
		// (set) Token: 0x06001277 RID: 4727 RVA: 0x000732A1 File Offset: 0x000716A1
		public Vector3 startPoint
		{
			get
			{
				return this.m_StartPoint;
			}
			set
			{
				this.m_StartPoint = value;
				this.UpdateLink();
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06001278 RID: 4728 RVA: 0x000732B0 File Offset: 0x000716B0
		// (set) Token: 0x06001279 RID: 4729 RVA: 0x000732B8 File Offset: 0x000716B8
		public Vector3 endPoint
		{
			get
			{
				return this.m_EndPoint;
			}
			set
			{
				this.m_EndPoint = value;
				this.UpdateLink();
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600127A RID: 4730 RVA: 0x000732C7 File Offset: 0x000716C7
		// (set) Token: 0x0600127B RID: 4731 RVA: 0x000732CF File Offset: 0x000716CF
		public float width
		{
			get
			{
				return this.m_Width;
			}
			set
			{
				this.m_Width = value;
				this.UpdateLink();
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600127C RID: 4732 RVA: 0x000732DE File Offset: 0x000716DE
		// (set) Token: 0x0600127D RID: 4733 RVA: 0x000732E6 File Offset: 0x000716E6
		public int costModifier
		{
			get
			{
				return this.m_CostModifier;
			}
			set
			{
				this.m_CostModifier = value;
				this.UpdateLink();
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600127E RID: 4734 RVA: 0x000732F5 File Offset: 0x000716F5
		// (set) Token: 0x0600127F RID: 4735 RVA: 0x000732FD File Offset: 0x000716FD
		public bool bidirectional
		{
			get
			{
				return this.m_Bidirectional;
			}
			set
			{
				this.m_Bidirectional = value;
				this.UpdateLink();
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06001280 RID: 4736 RVA: 0x0007330C File Offset: 0x0007170C
		// (set) Token: 0x06001281 RID: 4737 RVA: 0x00073314 File Offset: 0x00071714
		public bool autoUpdate
		{
			get
			{
				return this.m_AutoUpdatePosition;
			}
			set
			{
				this.SetAutoUpdate(value);
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06001282 RID: 4738 RVA: 0x0007331D File Offset: 0x0007171D
		// (set) Token: 0x06001283 RID: 4739 RVA: 0x00073325 File Offset: 0x00071725
		public int area
		{
			get
			{
				return this.m_Area;
			}
			set
			{
				this.m_Area = value;
				this.UpdateLink();
			}
		}

		// Token: 0x06001284 RID: 4740 RVA: 0x00073334 File Offset: 0x00071734
		private void OnEnable()
		{
			this.AddLink();
			if (this.m_AutoUpdatePosition && this.m_LinkInstance.valid)
			{
				NavMeshLink.AddTracking(this);
			}
		}

		// Token: 0x06001285 RID: 4741 RVA: 0x0007335D File Offset: 0x0007175D
		private void OnDisable()
		{
			NavMeshLink.RemoveTracking(this);
			this.m_LinkInstance.Remove();
		}

		// Token: 0x06001286 RID: 4742 RVA: 0x00073370 File Offset: 0x00071770
		public void UpdateLink()
		{
			this.m_LinkInstance.Remove();
			this.AddLink();
		}

		// Token: 0x06001287 RID: 4743 RVA: 0x00073384 File Offset: 0x00071784
		private static void AddTracking(NavMeshLink link)
		{
			if (NavMeshLink.s_Tracked.Count == 0)
			{
				Delegate onPreUpdate = NavMesh.onPreUpdate;
				if (NavMeshLink.<>f__mg$cache0 == null)
				{
					NavMeshLink.<>f__mg$cache0 = new NavMesh.OnNavMeshPreUpdate(NavMeshLink.UpdateTrackedInstances);
				}
				NavMesh.onPreUpdate = (NavMesh.OnNavMeshPreUpdate)Delegate.Combine(onPreUpdate, NavMeshLink.<>f__mg$cache0);
			}
			NavMeshLink.s_Tracked.Add(link);
		}

		// Token: 0x06001288 RID: 4744 RVA: 0x000733DC File Offset: 0x000717DC
		private static void RemoveTracking(NavMeshLink link)
		{
			NavMeshLink.s_Tracked.Remove(link);
			if (NavMeshLink.s_Tracked.Count == 0)
			{
				Delegate onPreUpdate = NavMesh.onPreUpdate;
				if (NavMeshLink.<>f__mg$cache1 == null)
				{
					NavMeshLink.<>f__mg$cache1 = new NavMesh.OnNavMeshPreUpdate(NavMeshLink.UpdateTrackedInstances);
				}
				NavMesh.onPreUpdate = (NavMesh.OnNavMeshPreUpdate)Delegate.Remove(onPreUpdate, NavMeshLink.<>f__mg$cache1);
			}
		}

		// Token: 0x06001289 RID: 4745 RVA: 0x00073435 File Offset: 0x00071835
		private void SetAutoUpdate(bool value)
		{
			if (this.m_AutoUpdatePosition == value)
			{
				return;
			}
			this.m_AutoUpdatePosition = value;
			if (value)
			{
				NavMeshLink.AddTracking(this);
			}
			else
			{
				NavMeshLink.RemoveTracking(this);
			}
		}

		// Token: 0x0600128A RID: 4746 RVA: 0x00073464 File Offset: 0x00071864
		private void AddLink()
		{
			this.m_LinkInstance = NavMesh.AddLink(new NavMeshLinkData
			{
				startPosition = this.m_StartPoint,
				endPosition = this.m_EndPoint,
				width = this.m_Width,
				costModifier = (float)this.m_CostModifier,
				bidirectional = this.m_Bidirectional,
				area = this.m_Area,
				agentTypeID = this.m_AgentTypeID
			}, base.transform.position, base.transform.rotation);
			if (this.m_LinkInstance.valid)
			{
				this.m_LinkInstance.owner = this;
			}
			this.m_LastPosition = base.transform.position;
			this.m_LastRotation = base.transform.rotation;
		}

		// Token: 0x0600128B RID: 4747 RVA: 0x00073535 File Offset: 0x00071935
		private bool HasTransformChanged()
		{
			return this.m_LastPosition != base.transform.position || this.m_LastRotation != base.transform.rotation;
		}

		// Token: 0x0600128C RID: 4748 RVA: 0x00073572 File Offset: 0x00071972
		private void OnDidApplyAnimationProperties()
		{
			this.UpdateLink();
		}

		// Token: 0x0600128D RID: 4749 RVA: 0x0007357C File Offset: 0x0007197C
		private static void UpdateTrackedInstances()
		{
			foreach (NavMeshLink navMeshLink in NavMeshLink.s_Tracked)
			{
				if (navMeshLink.HasTransformChanged())
				{
					navMeshLink.UpdateLink();
				}
			}
		}

		// Token: 0x040014F8 RID: 5368
		[SerializeField]
		private int m_AgentTypeID;

		// Token: 0x040014F9 RID: 5369
		[SerializeField]
		private Vector3 m_StartPoint = new Vector3(0f, 0f, -2.5f);

		// Token: 0x040014FA RID: 5370
		[SerializeField]
		private Vector3 m_EndPoint = new Vector3(0f, 0f, 2.5f);

		// Token: 0x040014FB RID: 5371
		[SerializeField]
		private float m_Width;

		// Token: 0x040014FC RID: 5372
		[SerializeField]
		private int m_CostModifier = -1;

		// Token: 0x040014FD RID: 5373
		[SerializeField]
		private bool m_Bidirectional = true;

		// Token: 0x040014FE RID: 5374
		[SerializeField]
		private bool m_AutoUpdatePosition;

		// Token: 0x040014FF RID: 5375
		[SerializeField]
		private int m_Area;

		// Token: 0x04001500 RID: 5376
		private NavMeshLinkInstance m_LinkInstance = default(NavMeshLinkInstance);

		// Token: 0x04001501 RID: 5377
		private Vector3 m_LastPosition = Vector3.zero;

		// Token: 0x04001502 RID: 5378
		private Quaternion m_LastRotation = Quaternion.identity;

		// Token: 0x04001503 RID: 5379
		private static readonly List<NavMeshLink> s_Tracked = new List<NavMeshLink>();

		// Token: 0x04001504 RID: 5380
		[CompilerGenerated]
		private static NavMesh.OnNavMeshPreUpdate <>f__mg$cache0;

		// Token: 0x04001505 RID: 5381
		[CompilerGenerated]
		private static NavMesh.OnNavMeshPreUpdate <>f__mg$cache1;
	}
}
