using System;
using System.Runtime.CompilerServices;
using Manager;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

namespace AIProject
{
	// Token: 0x02000C26 RID: 3110
	public abstract class Point : SerializedMonoBehaviour
	{
		// Token: 0x170012C9 RID: 4809
		// (get) Token: 0x06006002 RID: 24578 RVA: 0x0026291A File Offset: 0x00260D1A
		// (set) Token: 0x06006003 RID: 24579 RVA: 0x00262922 File Offset: 0x00260D22
		public virtual int RegisterID { get; set; }

		// Token: 0x170012CA RID: 4810
		// (get) Token: 0x06006004 RID: 24580 RVA: 0x0026292B File Offset: 0x00260D2B
		// (set) Token: 0x06006005 RID: 24581 RVA: 0x00262933 File Offset: 0x00260D33
		public MapArea OwnerArea
		{
			get
			{
				return this._ownerArea;
			}
			set
			{
				this._ownerArea = value;
			}
		}

		// Token: 0x170012CB RID: 4811
		// (get) Token: 0x06006006 RID: 24582 RVA: 0x0026293C File Offset: 0x00260D3C
		// (set) Token: 0x06006007 RID: 24583 RVA: 0x00262944 File Offset: 0x00260D44
		public MapArea.AreaType AreaType
		{
			get
			{
				return this._areaType;
			}
			set
			{
				this._areaType = value;
			}
		}

		// Token: 0x170012CC RID: 4812
		// (get) Token: 0x06006008 RID: 24584 RVA: 0x0026294D File Offset: 0x00260D4D
		public bool Initialized
		{
			[CompilerGenerated]
			get
			{
				return this._initialized;
			}
		}

		// Token: 0x06006009 RID: 24585 RVA: 0x00262955 File Offset: 0x00260D55
		protected virtual void Start()
		{
		}

		// Token: 0x0600600A RID: 24586 RVA: 0x00262957 File Offset: 0x00260D57
		protected virtual void OnEnable()
		{
		}

		// Token: 0x0600600B RID: 24587 RVA: 0x00262959 File Offset: 0x00260D59
		protected virtual void OnDisable()
		{
		}

		// Token: 0x0600600C RID: 24588 RVA: 0x0026295B File Offset: 0x00260D5B
		public virtual void LocateGround()
		{
			Point.LocateGround(base.transform);
		}

		// Token: 0x0600600D RID: 24589 RVA: 0x00262968 File Offset: 0x00260D68
		public virtual void RefreshExistence()
		{
			NavMeshHit navMeshHit;
			if (NavMesh.FindClosestEdge(base.transform.position, out navMeshHit, -1))
			{
				base.gameObject.SetActive(navMeshHit.distance >= Singleton<Manager.Resources>.Instance.LocomotionProfile.PointDistanceMargin);
			}
			else
			{
				base.gameObject.SetActive(false);
			}
		}

		// Token: 0x0600600E RID: 24590 RVA: 0x002629C4 File Offset: 0x00260DC4
		protected static void LocateGround(Transform t)
		{
			Vector3 origin = t.position + Vector3.up * 15f;
			Collider[] componentsInChildren = t.GetComponentsInChildren<Collider>();
			Ray ray = new Ray(origin, Vector3.down);
			int a = Physics.RaycastNonAlloc(ray, Point._raycastHits, 100f, Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.AreaDetectionLayer);
			int num = Mathf.Min(a, Point._raycastHits.Length);
			if (0 < num)
			{
				RaycastHit raycastHit = default(RaycastHit);
				for (int i = 0; i < num; i++)
				{
					RaycastHit raycastHit2 = Point._raycastHits[i];
					bool flag = false;
					foreach (Collider collider in componentsInChildren)
					{
						flag |= (collider.gameObject == raycastHit2.collider.gameObject);
						if (flag)
						{
							break;
						}
					}
					if (!flag)
					{
						raycastHit = raycastHit2;
						break;
					}
				}
				if (raycastHit.collider == null)
				{
					return;
				}
				t.position = raycastHit.point;
			}
		}

		// Token: 0x04005569 RID: 21865
		[SerializeField]
		[HideInEditorMode]
		[DisableInPlayMode]
		private MapArea _ownerArea;

		// Token: 0x0400556A RID: 21866
		[SerializeField]
		[HideInEditorMode]
		[DisableInPlayMode]
		private MapArea.AreaType _areaType;

		// Token: 0x0400556B RID: 21867
		protected bool _initialized;

		// Token: 0x0400556C RID: 21868
		protected static RaycastHit[] _raycastHits = new RaycastHit[3];
	}
}
