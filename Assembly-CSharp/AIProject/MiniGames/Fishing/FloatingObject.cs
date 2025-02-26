using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Manager;
using UniRx;
using UnityEngine;

namespace AIProject.MiniGames.Fishing
{
	// Token: 0x02000F3D RID: 3901
	public class FloatingObject : MonoBehaviour
	{
		// Token: 0x170019E4 RID: 6628
		// (get) Token: 0x060080FB RID: 33019 RVA: 0x0036B789 File Offset: 0x00369B89
		// (set) Token: 0x060080FC RID: 33020 RVA: 0x0036B791 File Offset: 0x00369B91
		public Collider waterCollider { get; set; }

		// Token: 0x170019E5 RID: 6629
		// (get) Token: 0x060080FD RID: 33021 RVA: 0x0036B79A File Offset: 0x00369B9A
		// (set) Token: 0x060080FE RID: 33022 RVA: 0x0036B7A2 File Offset: 0x00369BA2
		public FishingManager fishingSystem { get; set; }

		// Token: 0x170019E6 RID: 6630
		// (get) Token: 0x060080FF RID: 33023 RVA: 0x0036B7AB File Offset: 0x00369BAB
		// (set) Token: 0x06008100 RID: 33024 RVA: 0x0036B7B3 File Offset: 0x00369BB3
		public bool OnWater { get; private set; }

		// Token: 0x170019E7 RID: 6631
		// (get) Token: 0x06008101 RID: 33025 RVA: 0x0036B7BC File Offset: 0x00369BBC
		// (set) Token: 0x06008102 RID: 33026 RVA: 0x0036B7C4 File Offset: 0x00369BC4
		public Func<Collider, bool> WaterEnterChecker { get; set; }

		// Token: 0x170019E8 RID: 6632
		// (get) Token: 0x06008103 RID: 33027 RVA: 0x0036B7CD File Offset: 0x00369BCD
		// (set) Token: 0x06008104 RID: 33028 RVA: 0x0036B7D5 File Offset: 0x00369BD5
		public Func<Collider, bool> WaterStayChecker { get; set; }

		// Token: 0x170019E9 RID: 6633
		// (get) Token: 0x06008105 RID: 33029 RVA: 0x0036B7DE File Offset: 0x00369BDE
		// (set) Token: 0x06008106 RID: 33030 RVA: 0x0036B7E6 File Offset: 0x00369BE6
		public Func<Collider, bool> WaterExitChecker { get; set; }

		// Token: 0x170019EA RID: 6634
		// (get) Token: 0x06008107 RID: 33031 RVA: 0x0036B7EF File Offset: 0x00369BEF
		// (set) Token: 0x06008108 RID: 33032 RVA: 0x0036B7F7 File Offset: 0x00369BF7
		public Action<Collider> WaterEnterEvent { get; set; }

		// Token: 0x170019EB RID: 6635
		// (get) Token: 0x06008109 RID: 33033 RVA: 0x0036B800 File Offset: 0x00369C00
		// (set) Token: 0x0600810A RID: 33034 RVA: 0x0036B808 File Offset: 0x00369C08
		public Action<Collider> WaterStayEvent { get; set; }

		// Token: 0x170019EC RID: 6636
		// (get) Token: 0x0600810B RID: 33035 RVA: 0x0036B811 File Offset: 0x00369C11
		// (set) Token: 0x0600810C RID: 33036 RVA: 0x0036B819 File Offset: 0x00369C19
		public Action<Collider> WaterExitEvent { get; set; }

		// Token: 0x170019ED RID: 6637
		// (get) Token: 0x0600810D RID: 33037 RVA: 0x0036B822 File Offset: 0x00369C22
		private FishingDefinePack.LureParamGroup Param
		{
			[CompilerGenerated]
			get
			{
				return Singleton<Manager.Resources>.Instance.FishingDefinePack.LureParam;
			}
		}

		// Token: 0x170019EE RID: 6638
		// (get) Token: 0x0600810E RID: 33038 RVA: 0x0036B833 File Offset: 0x00369C33
		// (set) Token: 0x0600810F RID: 33039 RVA: 0x0036B83C File Offset: 0x00369C3C
		public bool UseWaterBuoyancy
		{
			get
			{
				return this.useWaterBuoyancy_;
			}
			set
			{
				this.useWaterBuoyancy_ = value;
				if (this.useWaterBuoyancy_)
				{
					this.m_rigidbody = base.gameObject.GetOrAddComponent<Rigidbody>();
					this.m_rigidbody.useGravity = true;
					this.m_rigidbody.constraints = (RigidbodyConstraints)122;
					this.initialDrag = this.m_rigidbody.drag;
					this.initialAngularDrag = this.m_rigidbody.angularDrag;
				}
				else if (this.m_rigidbody != null)
				{
					UnityEngine.Object.Destroy(this.m_rigidbody);
					this.m_rigidbody = null;
				}
				if (this.fishingSystem != null)
				{
					this.fishingSystem.WaterBox.enabled = this.useWaterBuoyancy_;
				}
				if (this.useWaterBuoyancy_)
				{
					if (this.returnPosAngleDisposable != null)
					{
						this.returnPosAngleDisposable.Dispose();
					}
				}
				else if (this.returnPosAngleDisposable == null)
				{
					IEnumerator _coroutine = this.ReturnPosAngle(0.25f);
					this.returnPosAngleDisposable = Observable.FromCoroutine(() => _coroutine, false).Subscribe<Unit>();
				}
			}
		}

		// Token: 0x06008110 RID: 33040 RVA: 0x0036B960 File Offset: 0x00369D60
		private IEnumerator ReturnPosAngle(float _time)
		{
			if (base.transform.localPosition == Vector3.zero && base.transform.localEulerAngles == Vector3.zero)
			{
				yield return null;
				this.returnPosAngleDisposable = null;
				yield break;
			}
			yield return null;
			Vector3 _startPos = base.transform.localPosition;
			Vector3 _startAngle = base.transform.localEulerAngles;
			for (float _counter = 0f; _counter <= _time; _counter += Time.deltaTime)
			{
				float _t = Mathf.InverseLerp(0f, _time, _counter);
				base.transform.localPosition = Vector3.Lerp(_startPos, Vector3.zero, _t);
				base.transform.localEulerAngles = Vector3.Lerp(_startAngle, Vector3.zero, _t);
				yield return null;
			}
			Transform transform = base.transform;
			Vector3 zero = Vector3.zero;
			base.transform.localEulerAngles = zero;
			transform.localPosition = zero;
			this.returnPosAngleDisposable = null;
			yield break;
		}

		// Token: 0x06008111 RID: 33041 RVA: 0x0036B984 File Offset: 0x00369D84
		private void Awake()
		{
			this.CreateFloatingObject();
			(from _ in Observable.EveryFixedUpdate().TakeUntilDestroy(base.gameObject)
			where base.isActiveAndEnabled
			where this.UseWaterBuoyancy && this.OnWater && !this.voxels.IsNullOrEmpty<Vector3>() && this.waterCollider != null
			select _).Subscribe(delegate(long _)
			{
				this.WaterBuoyancyUpdate();
			});
		}

		// Token: 0x06008112 RID: 33042 RVA: 0x0036B9DB File Offset: 0x00369DDB
		private void OnEnable()
		{
			this.CutIntoVoxels();
		}

		// Token: 0x06008113 RID: 33043 RVA: 0x0036B9E3 File Offset: 0x00369DE3
		private void OnDisable()
		{
			this.UseWaterBuoyancy = false;
		}

		// Token: 0x06008114 RID: 33044 RVA: 0x0036B9EC File Offset: 0x00369DEC
		private void CreateFloatingObject()
		{
			this.m_collider = base.gameObject.GetOrAddComponent<SphereCollider>();
			this.m_collider.isTrigger = true;
			this.UseWaterBuoyancy = false;
			this.CutIntoVoxels();
		}

		// Token: 0x06008115 RID: 33045 RVA: 0x0036BA18 File Offset: 0x00369E18
		private void WaterBuoyancyUpdate()
		{
			Vector3 a = this.CalculateMaxBuoyancyForce() / (float)this.voxels.Count;
			float num = this.m_collider.bounds.size.y * this.Param.NormalizedVoxelSize;
			float num2 = 0f;
			for (int i = 0; i < this.voxels.Count; i++)
			{
				Vector3 vector = base.transform.TransformPoint(this.voxels[i]);
				Vector3 vector2;
				float num3 = (!FishingManager.CheckOnWater(vector, out vector2)) ? this.fishingSystem.MoveArea.transform.position.y : vector2.y;
				float num4 = num3 - vector.y + num / 2f;
				float num5 = Mathf.Clamp(num4 / num, 0f, 1f);
				num2 += num5;
				Vector3 up = Vector3.up;
				Quaternion quaternion = Quaternion.FromToRotation(this.waterCollider.transform.up, up);
				quaternion = Quaternion.Slerp(quaternion, Quaternion.identity, num5);
				Vector3 force = quaternion * (a * num5);
				this.m_rigidbody.AddForceAtPosition(force, vector);
			}
			num2 /= (float)this.voxels.Count;
			this.m_rigidbody.drag = Mathf.Lerp(this.initialDrag, this.Param.DragInWater, num2);
			this.m_rigidbody.angularDrag = Mathf.Lerp(this.initialAngularDrag, this.Param.AngularDragInWater, num2);
		}

		// Token: 0x06008116 RID: 33046 RVA: 0x0036BBB8 File Offset: 0x00369FB8
		private void CutIntoVoxels()
		{
			Quaternion rotation = base.transform.rotation;
			base.transform.rotation = Quaternion.identity;
			Bounds bounds = this.m_collider.bounds;
			this.voxelSize = bounds.size * this.Param.NormalizedVoxelSize;
			int num = Mathf.RoundToInt(1f / this.Param.NormalizedVoxelSize);
			this.voxels = new List<Vector3>(num * num * num);
			int mask = LayerMask.GetMask(new string[]
			{
				LayerMask.LayerToName(base.gameObject.layer)
			});
			for (int i = 0; i < num; i++)
			{
				for (int j = 0; j < num; j++)
				{
					for (int k = 0; k < num; k++)
					{
						float x = bounds.min.x + this.voxelSize.x * (0.5f + (float)i);
						float y = bounds.min.y + this.voxelSize.y * (0.5f + (float)j);
						float z = bounds.min.z + this.voxelSize.z * (0.5f + (float)k);
						Vector3 vector = new Vector3(x, y, z);
						if (this.IsPointInsideCollider(vector, this.m_collider, ref bounds, mask))
						{
							this.voxels.Add(base.transform.InverseTransformPoint(vector));
						}
					}
				}
			}
			base.transform.rotation = rotation;
		}

		// Token: 0x06008117 RID: 33047 RVA: 0x0036BD54 File Offset: 0x0036A154
		private Vector3 CalculateMaxBuoyancyForce()
		{
			float num = this.m_rigidbody.mass / this.Param.Density;
			return this.Param.WaterDensity * num * -Physics.gravity;
		}

		// Token: 0x06008118 RID: 33048 RVA: 0x0036BD98 File Offset: 0x0036A198
		private bool IsPointInsideCollider(Vector3 _point, Collider _collider, ref Bounds _colliderBounds, int _layerMask)
		{
			float magnitude = _colliderBounds.size.magnitude;
			Ray ray = new Ray(_point, _collider.transform.position - _point);
			RaycastHit[] array = new RaycastHit[3];
			int num = Physics.RaycastNonAlloc(ray, array, magnitude, _layerMask);
			if (0 < num)
			{
				for (int i = 0; i < num; i++)
				{
					RaycastHit raycastHit = array[i];
					if (raycastHit.collider == _collider)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06008119 RID: 33049 RVA: 0x0036BE24 File Offset: 0x0036A224
		private void OnTriggerEnter(Collider other)
		{
			Func<Collider, bool> waterEnterChecker = this.WaterEnterChecker;
			bool? flag = (waterEnterChecker != null) ? new bool?(waterEnterChecker(other)) : null;
			if (flag != null && flag.Value)
			{
				Action<Collider> waterEnterEvent = this.WaterEnterEvent;
				if (waterEnterEvent != null)
				{
					waterEnterEvent(other);
				}
				this.OnWater = true;
			}
		}

		// Token: 0x0600811A RID: 33050 RVA: 0x0036BE90 File Offset: 0x0036A290
		private void OnTriggerStay(Collider other)
		{
			if (!this.OnWater)
			{
				Func<Collider, bool> waterStayChecker = this.WaterStayChecker;
				bool? flag = (waterStayChecker != null) ? new bool?(waterStayChecker(other)) : null;
				if (flag != null && flag.Value)
				{
					Action<Collider> waterStayEvent = this.WaterStayEvent;
					if (waterStayEvent != null)
					{
						waterStayEvent(other);
					}
					this.OnWater = true;
				}
			}
		}

		// Token: 0x0600811B RID: 33051 RVA: 0x0036BF08 File Offset: 0x0036A308
		private void OnTriggerExit(Collider other)
		{
			Func<Collider, bool> waterExitChecker = this.WaterExitChecker;
			bool? flag = (waterExitChecker != null) ? new bool?(waterExitChecker(other)) : null;
			if (flag != null && flag.Value)
			{
				Action<Collider> waterExitEvent = this.WaterExitEvent;
				if (waterExitEvent != null)
				{
					waterExitEvent(other);
				}
				this.OnWater = false;
			}
		}

		// Token: 0x040067AF RID: 26543
		private Collider m_collider;

		// Token: 0x040067B0 RID: 26544
		private Rigidbody m_rigidbody;

		// Token: 0x040067B2 RID: 26546
		private Vector3 voxelSize = Vector3.zero;

		// Token: 0x040067B3 RID: 26547
		private List<Vector3> voxels = new List<Vector3>();

		// Token: 0x040067B4 RID: 26548
		private float initialDrag;

		// Token: 0x040067B5 RID: 26549
		private float initialAngularDrag;

		// Token: 0x040067BE RID: 26558
		private bool useWaterBuoyancy_;

		// Token: 0x040067BF RID: 26559
		private IDisposable returnPosAngleDisposable;
	}
}
