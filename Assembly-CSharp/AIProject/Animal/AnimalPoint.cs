using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Manager;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

namespace AIProject.Animal
{
	// Token: 0x02000BBA RID: 3002
	public class AnimalPoint : Point
	{
		// Token: 0x17001103 RID: 4355
		// (get) Token: 0x06005A94 RID: 23188 RVA: 0x00269914 File Offset: 0x00267D14
		public int ID
		{
			[CompilerGenerated]
			get
			{
				return this._id;
			}
		}

		// Token: 0x06005A95 RID: 23189 RVA: 0x0026991C File Offset: 0x00267D1C
		public void SetID(int id)
		{
			this._id = id;
		}

		// Token: 0x17001104 RID: 4356
		// (get) Token: 0x06005A96 RID: 23190 RVA: 0x00269925 File Offset: 0x00267D25
		public LocateTypes LocateType
		{
			[CompilerGenerated]
			get
			{
				return this._locateType;
			}
		}

		// Token: 0x17001105 RID: 4357
		// (get) Token: 0x06005A97 RID: 23191 RVA: 0x0026992D File Offset: 0x00267D2D
		// (set) Token: 0x06005A98 RID: 23192 RVA: 0x0026993A File Offset: 0x00267D3A
		public Vector3 Position
		{
			get
			{
				return base.transform.position;
			}
			set
			{
				base.transform.position = value;
			}
		}

		// Token: 0x17001106 RID: 4358
		// (get) Token: 0x06005A99 RID: 23193 RVA: 0x00269948 File Offset: 0x00267D48
		// (set) Token: 0x06005A9A RID: 23194 RVA: 0x00269955 File Offset: 0x00267D55
		public Vector3 EulerAngles
		{
			get
			{
				return base.transform.eulerAngles;
			}
			set
			{
				base.transform.eulerAngles = value;
			}
		}

		// Token: 0x17001107 RID: 4359
		// (get) Token: 0x06005A9B RID: 23195 RVA: 0x00269963 File Offset: 0x00267D63
		// (set) Token: 0x06005A9C RID: 23196 RVA: 0x00269970 File Offset: 0x00267D70
		public Quaternion Rotation
		{
			get
			{
				return base.transform.rotation;
			}
			set
			{
				base.transform.rotation = value;
			}
		}

		// Token: 0x17001108 RID: 4360
		// (get) Token: 0x06005A9D RID: 23197 RVA: 0x0026997E File Offset: 0x00267D7E
		// (set) Token: 0x06005A9E RID: 23198 RVA: 0x0026998B File Offset: 0x00267D8B
		public Vector3 LocalPosition
		{
			get
			{
				return base.transform.localPosition;
			}
			set
			{
				base.transform.localPosition = value;
			}
		}

		// Token: 0x17001109 RID: 4361
		// (get) Token: 0x06005A9F RID: 23199 RVA: 0x00269999 File Offset: 0x00267D99
		// (set) Token: 0x06005AA0 RID: 23200 RVA: 0x002699A6 File Offset: 0x00267DA6
		public Vector3 LocalEulerAngles
		{
			get
			{
				return base.transform.localEulerAngles;
			}
			set
			{
				base.transform.localEulerAngles = value;
			}
		}

		// Token: 0x1700110A RID: 4362
		// (get) Token: 0x06005AA1 RID: 23201 RVA: 0x002699B4 File Offset: 0x00267DB4
		// (set) Token: 0x06005AA2 RID: 23202 RVA: 0x002699C1 File Offset: 0x00267DC1
		public Quaternion LocalRotation
		{
			get
			{
				return base.transform.localRotation;
			}
			set
			{
				base.transform.localRotation = value;
			}
		}

		// Token: 0x1700110B RID: 4363
		// (get) Token: 0x06005AA3 RID: 23203 RVA: 0x002699CF File Offset: 0x00267DCF
		public Vector3 Forward
		{
			[CompilerGenerated]
			get
			{
				return base.transform.forward;
			}
		}

		// Token: 0x1700110C RID: 4364
		// (get) Token: 0x06005AA4 RID: 23204 RVA: 0x002699DC File Offset: 0x00267DDC
		public Vector3 Up
		{
			[CompilerGenerated]
			get
			{
				return base.transform.up;
			}
		}

		// Token: 0x1700110D RID: 4365
		// (get) Token: 0x06005AA5 RID: 23205 RVA: 0x002699E9 File Offset: 0x00267DE9
		public Vector3 Right
		{
			[CompilerGenerated]
			get
			{
				return base.transform.right;
			}
		}

		// Token: 0x1700110E RID: 4366
		// (get) Token: 0x06005AA6 RID: 23206 RVA: 0x002699F6 File Offset: 0x00267DF6
		public Color Pink
		{
			[CompilerGenerated]
			get
			{
				return this._pink;
			}
		}

		// Token: 0x1700110F RID: 4367
		// (get) Token: 0x06005AA7 RID: 23207 RVA: 0x002699FE File Offset: 0x00267DFE
		public virtual bool Available
		{
			[CompilerGenerated]
			get
			{
				return true;
			}
		}

		// Token: 0x06005AA8 RID: 23208 RVA: 0x00269A01 File Offset: 0x00267E01
		public virtual void LoadObject()
		{
		}

		// Token: 0x06005AA9 RID: 23209 RVA: 0x00269A03 File Offset: 0x00267E03
		protected float DistanceXZ(Vector3 _p1, Vector3 _p2)
		{
			_p1.y = _p2.y;
			return Vector3.Distance(_p1, _p2);
		}

		// Token: 0x06005AAA RID: 23210 RVA: 0x00269A1A File Offset: 0x00267E1A
		protected float DistanceY(Vector3 _p1, Vector3 _p2)
		{
			return Mathf.Abs(_p2.y - _p1.y);
		}

		// Token: 0x06005AAB RID: 23211 RVA: 0x00269A30 File Offset: 0x00267E30
		public static void RelocationOnCollider(Transform t, float heightOnRaycast)
		{
			Vector3 origin = t.position + Vector3.up * heightOnRaycast;
			Collider[] componentsInChildren = t.GetComponentsInChildren<Collider>();
			Ray ray = new Ray(origin, Vector3.down);
			int num = Mathf.Min(Physics.RaycastNonAlloc(ray, Point._raycastHits, 100f, Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.MapLayer), Point._raycastHits.Length);
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

		// Token: 0x06005AAC RID: 23212 RVA: 0x00269B58 File Offset: 0x00267F58
		public static void RelocationOnCollider(Transform t, float heightOnRaycast, LayerMask layerMask)
		{
			Vector3 origin = t.position + Vector3.up * heightOnRaycast;
			Collider[] componentsInChildren = t.GetComponentsInChildren<Collider>();
			Ray ray = new Ray(origin, Vector3.down);
			int num = Mathf.Min(Physics.RaycastNonAlloc(ray, Point._raycastHits, 100f, layerMask), Point._raycastHits.Length);
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

		// Token: 0x06005AAD RID: 23213 RVA: 0x00269C6C File Offset: 0x0026806C
		public static void RelocationOnNavMesh(Transform t, float searchDistance)
		{
			Vector3 position = t.position;
			NavMeshHit navMeshHit;
			if (NavMesh.SamplePosition(position, out navMeshHit, searchDistance, -1))
			{
				t.position = navMeshHit.position;
			}
		}

		// Token: 0x04005265 RID: 21093
		[SerializeField]
		[Tooltip("このポイントのID")]
		protected int _id;

		// Token: 0x04005266 RID: 21094
		[SerializeField]
		private LocateTypes _locateType;

		// Token: 0x04005267 RID: 21095
		private Color _pink = new Color(1f, 0f, 1f);

		// Token: 0x02000BBB RID: 3003
		[Serializable]
		public class LocateInfo
		{
			// Token: 0x06005AAF RID: 23215 RVA: 0x00269CDC File Offset: 0x002680DC
			[Sirenix.OdinInspector.Button("コライダーの高さに合わせる", ButtonHeight = 20)]
			public void LocateCollider()
			{
				if (this._colliderTarget.IsNullOrEmpty<Transform>())
				{
					return;
				}
				foreach (Transform t in this._colliderTarget)
				{
					AnimalPoint.RelocationOnCollider(t, this._raycastUpOffset, this._checkLayer);
				}
			}

			// Token: 0x06005AB0 RID: 23216 RVA: 0x00269D54 File Offset: 0x00268154
			[Sirenix.OdinInspector.Button("一番近いNavMeshの位置に合わせる", ButtonHeight = 20)]
			public void LocateNavMesh()
			{
				if (this._navMeshTarget.IsNullOrEmpty<Transform>())
				{
					return;
				}
				foreach (Transform t in this._navMeshTarget)
				{
					AnimalPoint.RelocationOnNavMesh(t, this._checkNavMeshDistance);
				}
			}

			// Token: 0x06005AB1 RID: 23217 RVA: 0x00269DC8 File Offset: 0x002681C8
			[Sirenix.OdinInspector.Button("両方合わせる", ButtonHeight = 20)]
			public void LocateAll()
			{
				this.LocateCollider();
				this.LocateNavMesh();
			}

			// Token: 0x04005268 RID: 21096
			[SerializeField]
			private float _raycastUpOffset = 3f;

			// Token: 0x04005269 RID: 21097
			[SerializeField]
			private LayerMask _checkLayer = 0;

			// Token: 0x0400526A RID: 21098
			[SerializeField]
			private float _checkNavMeshDistance = 10f;

			// Token: 0x0400526B RID: 21099
			[SerializeField]
			private List<Transform> _colliderTarget = new List<Transform>();

			// Token: 0x0400526C RID: 21100
			[SerializeField]
			private List<Transform> _navMeshTarget = new List<Transform>();

			// Token: 0x0400526D RID: 21101
			private const int ButtonHeight = 20;

			// Token: 0x0400526E RID: 21102
			private const int FontSize = 15;
		}
	}
}
