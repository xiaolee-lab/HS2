using System;
using System.Linq;
using System.Runtime.CompilerServices;
using AIProject;
using AIProject.Animal;
using Illusion.Extensions;
using Manager;
using UnityEngine;

namespace Housing
{
	// Token: 0x02000887 RID: 2183
	public class ItemComponent : MonoBehaviour
	{
		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x06003801 RID: 14337 RVA: 0x0014D737 File Offset: 0x0014BB37
		public Vector3 position
		{
			[CompilerGenerated]
			get
			{
				return base.transform.position;
			}
		}

		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x06003802 RID: 14338 RVA: 0x0014D744 File Offset: 0x0014BB44
		public bool IsColor
		{
			[CompilerGenerated]
			get
			{
				return this.IsColor1 | this.IsColor2 | this.IsColor3 | this.IsEmissionColor;
			}
		}

		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x06003803 RID: 14339 RVA: 0x0014D761 File Offset: 0x0014BB61
		public bool IsColor1
		{
			get
			{
				if (this.renderers.IsNullOrEmpty<ItemComponent.Info>())
				{
					return false;
				}
				return this.renderers.Any(delegate(ItemComponent.Info _info)
				{
					bool result;
					if (!_info.materialInfos.IsNullOrEmpty<ItemComponent.MaterialInfo>())
					{
						result = _info.materialInfos.Any((ItemComponent.MaterialInfo v) => v.isColor1);
					}
					else
					{
						result = false;
					}
					return result;
				});
			}
		}

		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x06003804 RID: 14340 RVA: 0x0014D79D File Offset: 0x0014BB9D
		public bool IsColor2
		{
			get
			{
				if (this.renderers.IsNullOrEmpty<ItemComponent.Info>())
				{
					return false;
				}
				return this.renderers.Any(delegate(ItemComponent.Info _info)
				{
					bool result;
					if (!_info.materialInfos.IsNullOrEmpty<ItemComponent.MaterialInfo>())
					{
						result = _info.materialInfos.Any((ItemComponent.MaterialInfo v) => v.isColor2);
					}
					else
					{
						result = false;
					}
					return result;
				});
			}
		}

		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x06003805 RID: 14341 RVA: 0x0014D7D9 File Offset: 0x0014BBD9
		public bool IsColor3
		{
			get
			{
				if (this.renderers.IsNullOrEmpty<ItemComponent.Info>())
				{
					return false;
				}
				return this.renderers.Any(delegate(ItemComponent.Info _info)
				{
					bool result;
					if (!_info.materialInfos.IsNullOrEmpty<ItemComponent.MaterialInfo>())
					{
						result = _info.materialInfos.Any((ItemComponent.MaterialInfo v) => v.isColor3);
					}
					else
					{
						result = false;
					}
					return result;
				});
			}
		}

		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x06003806 RID: 14342 RVA: 0x0014D815 File Offset: 0x0014BC15
		public bool IsEmissionColor
		{
			get
			{
				if (this.renderers.IsNullOrEmpty<ItemComponent.Info>())
				{
					return false;
				}
				return this.renderers.Any(delegate(ItemComponent.Info _info)
				{
					bool result;
					if (!_info.materialInfos.IsNullOrEmpty<ItemComponent.MaterialInfo>())
					{
						result = _info.materialInfos.Any((ItemComponent.MaterialInfo v) => v.isEmission);
					}
					else
					{
						result = false;
					}
					return result;
				});
			}
		}

		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x06003807 RID: 14343 RVA: 0x0014D854 File Offset: 0x0014BC54
		public bool IsOption
		{
			get
			{
				bool result;
				if (this.objOption.IsNullOrEmpty<GameObject>())
				{
					result = false;
				}
				else
				{
					result = ((from v in this.objOption
					where v != null
					select v).Count<GameObject>() > 0);
				}
				return result;
			}
		}

		// Token: 0x06003808 RID: 14344 RVA: 0x0014D8A8 File Offset: 0x0014BCA8
		public void SetupRendererInfo()
		{
			MeshRenderer[] componentsInChildren = base.GetComponentsInChildren<MeshRenderer>();
			if (componentsInChildren.IsNullOrEmpty<MeshRenderer>())
			{
				return;
			}
			this.renderers = (from _r in componentsInChildren
			select new ItemComponent.Info(_r)).ToArray<ItemComponent.Info>();
		}

		// Token: 0x06003809 RID: 14345 RVA: 0x0014D8F6 File Offset: 0x0014BCF6
		public void Setup(bool _force = false)
		{
			this.SetMinMax(_force);
			this.SetActionPoint();
			this.SetFarmPoint();
			this.SetHPoint();
			this.SetPetHomePoint();
			this.SetJukePoint();
			this.SetCraftPoint();
			this.SetLightSwitchPoint();
		}

		// Token: 0x0600380A RID: 14346 RVA: 0x0014D92C File Offset: 0x0014BD2C
		public void SetMinMax(bool _force = false)
		{
			if (!this.autoSize && !_force)
			{
				return;
			}
			MeshRenderer[] componentsInChildren = base.gameObject.GetComponentsInChildren<MeshRenderer>();
			if (componentsInChildren.IsNullOrEmpty<MeshRenderer>())
			{
				return;
			}
			Bounds bounds = componentsInChildren[0].bounds;
			foreach (MeshRenderer meshRenderer in componentsInChildren)
			{
				bounds.Encapsulate(meshRenderer.bounds);
			}
			this.min = bounds.min;
			this.max = bounds.max;
			this.min = new Vector3(this.Ceil(this.min.x), this.Ceil(this.min.y), this.Ceil(this.min.z));
			this.max = new Vector3(this.Ceil(this.max.x), this.Ceil(this.max.y), this.Ceil(this.max.z));
		}

		// Token: 0x0600380B RID: 14347 RVA: 0x0014DA30 File Offset: 0x0014BE30
		public void SetVisibleOption(bool _flag)
		{
			if (this.objOption.IsNullOrEmpty<GameObject>())
			{
				return;
			}
			foreach (GameObject self in this.objOption)
			{
				self.SetActiveIfDifferent(_flag);
			}
		}

		// Token: 0x0600380C RID: 14348 RVA: 0x0014DA75 File Offset: 0x0014BE75
		public void SetActionPoint()
		{
			this.actionPoints = base.gameObject.GetComponentsInChildren<ActionPoint>(true);
		}

		// Token: 0x0600380D RID: 14349 RVA: 0x0014DA89 File Offset: 0x0014BE89
		public void SetFarmPoint()
		{
			this.farmPoints = base.gameObject.GetComponentsInChildren<FarmPoint>(true);
		}

		// Token: 0x0600380E RID: 14350 RVA: 0x0014DAA0 File Offset: 0x0014BEA0
		public void SetHPoint()
		{
			if (this.hPoints.IsNullOrEmpty<HPoint>())
			{
				this.hPoints = base.gameObject.GetComponentsInChildren<HPoint>(true);
				if (this.hPoints.IsNullOrEmpty<HPoint>())
				{
					return;
				}
			}
			if (Singleton<Manager.Resources>.IsInstance())
			{
				foreach (HPoint hpoint in this.hPoints)
				{
					hpoint.Init();
					hpoint.SetEffectActive(false);
				}
			}
		}

		// Token: 0x0600380F RID: 14351 RVA: 0x0014DB16 File Offset: 0x0014BF16
		public void SetPetHomePoint()
		{
			this.petHomePoints = base.gameObject.GetComponentsInChildren<PetHomePoint>(true);
		}

		// Token: 0x06003810 RID: 14352 RVA: 0x0014DB2A File Offset: 0x0014BF2A
		public void SetJukePoint()
		{
			this.jukePoints = base.gameObject.GetComponentsInChildren<JukePoint>(true);
		}

		// Token: 0x06003811 RID: 14353 RVA: 0x0014DB3E File Offset: 0x0014BF3E
		public void SetCraftPoint()
		{
			this.craftPoints = base.gameObject.GetComponentsInChildren<CraftPoint>(true);
		}

		// Token: 0x06003812 RID: 14354 RVA: 0x0014DB52 File Offset: 0x0014BF52
		public void SetLightSwitchPoint()
		{
			this.lightSwitchPoints = base.gameObject.GetComponentsInChildren<LightSwitchPoint>(true);
		}

		// Token: 0x06003813 RID: 14355 RVA: 0x0014DB68 File Offset: 0x0014BF68
		public void SetDefColor()
		{
			if (this.renderers.IsNullOrEmpty<ItemComponent.Info>())
			{
				return;
			}
			ItemComponent.Info info = (from r in this.renderers
			where r.meshRenderer != null && !r.materialInfos.IsNullOrEmpty<ItemComponent.MaterialInfo>()
			select r).FirstOrDefault((ItemComponent.Info _i) => _i.materialInfos.Any((ItemComponent.MaterialInfo _m) => _m.isColor1));
			if (info != null)
			{
				this.defColor1 = info.GetDefColor(0);
			}
			ItemComponent.Info info2 = (from r in this.renderers
			where r.meshRenderer != null && !r.materialInfos.IsNullOrEmpty<ItemComponent.MaterialInfo>()
			select r).FirstOrDefault((ItemComponent.Info _i) => _i.materialInfos.Any((ItemComponent.MaterialInfo _m) => _m.isColor2));
			if (info2 != null)
			{
				this.defColor2 = info2.GetDefColor(1);
			}
			ItemComponent.Info info3 = (from r in this.renderers
			where r.meshRenderer != null && !r.materialInfos.IsNullOrEmpty<ItemComponent.MaterialInfo>()
			select r).FirstOrDefault((ItemComponent.Info _i) => _i.materialInfos.Any((ItemComponent.MaterialInfo _m) => _m.isColor3));
			if (info3 != null)
			{
				this.defColor3 = info3.GetDefColor(2);
			}
			ItemComponent.Info info4 = (from r in this.renderers
			where r.meshRenderer != null && !r.materialInfos.IsNullOrEmpty<ItemComponent.MaterialInfo>()
			select r).FirstOrDefault((ItemComponent.Info _i) => _i.materialInfos.Any((ItemComponent.MaterialInfo _m) => _m.isEmission));
			if (info4 != null)
			{
				this.defEmissionColor = info4.GetDefColor(4);
			}
		}

		// Token: 0x06003814 RID: 14356 RVA: 0x0014DD00 File Offset: 0x0014C100
		public void SetOverlapColliders(bool _flag)
		{
			if (this.overlapColliders.IsNullOrEmpty<Collider>())
			{
				return;
			}
			foreach (Collider collider in from v in this.overlapColliders
			where v != null
			select v)
			{
				collider.enabled = _flag;
				collider.gameObject.SetActiveIfDifferent(_flag);
				collider.gameObject.layer = LayerMask.NameToLayer("Housing/Overlap");
			}
		}

		// Token: 0x06003815 RID: 14357 RVA: 0x0014DDB0 File Offset: 0x0014C1B0
		private float Ceil(float _value)
		{
			bool flag = _value < 0f;
			return Mathf.Ceil(Mathf.Abs(_value)) * (float)((!flag) ? 1 : -1);
		}

		// Token: 0x04003870 RID: 14448
		[Header("色替え関係")]
		public ItemComponent.Info[] renderers;

		// Token: 0x04003871 RID: 14449
		public Color defColor1 = Color.white;

		// Token: 0x04003872 RID: 14450
		public Color defColor2 = Color.white;

		// Token: 0x04003873 RID: 14451
		public Color defColor3 = Color.white;

		// Token: 0x04003874 RID: 14452
		[ColorUsage(false, true)]
		public Color defEmissionColor = Color.clear;

		// Token: 0x04003875 RID: 14453
		[Header("行動関係")]
		public ActionPoint[] actionPoints;

		// Token: 0x04003876 RID: 14454
		public FarmPoint[] farmPoints;

		// Token: 0x04003877 RID: 14455
		public PetHomePoint[] petHomePoints;

		// Token: 0x04003878 RID: 14456
		public JukePoint[] jukePoints;

		// Token: 0x04003879 RID: 14457
		public CraftPoint[] craftPoints;

		// Token: 0x0400387A RID: 14458
		public LightSwitchPoint[] lightSwitchPoints;

		// Token: 0x0400387B RID: 14459
		public HPoint[] hPoints;

		// Token: 0x0400387C RID: 14460
		[Header("オプション関係")]
		public GameObject[] objOption;

		// Token: 0x0400387D RID: 14461
		[Header("遮蔽関係")]
		public ItemComponent.ColInfo[] colInfos;

		// Token: 0x0400387E RID: 14462
		[Header("サイズ関係")]
		public bool autoSize = true;

		// Token: 0x0400387F RID: 14463
		public Vector3 min = Vector3.zero;

		// Token: 0x04003880 RID: 14464
		public Vector3 max = Vector3.zero;

		// Token: 0x04003881 RID: 14465
		[Header("重なり関係")]
		public bool overlap;

		// Token: 0x04003882 RID: 14466
		public Collider[] overlapColliders;

		// Token: 0x04003883 RID: 14467
		[Header("初期関係")]
		public Vector3 initPos = Vector3.zero;

		// Token: 0x02000888 RID: 2184
		[Serializable]
		public class MaterialInfo
		{
			// Token: 0x0400389B RID: 14491
			public bool isColor1;

			// Token: 0x0400389C RID: 14492
			public bool isColor2;

			// Token: 0x0400389D RID: 14493
			public bool isColor3;

			// Token: 0x0400389E RID: 14494
			public bool isEmission;
		}

		// Token: 0x02000889 RID: 2185
		[Serializable]
		public class ColInfo
		{
			// Token: 0x0600382F RID: 14383 RVA: 0x0014E080 File Offset: 0x0014C480
			public void CheckCollision(Collider _collider)
			{
				Vector3 position = _collider.transform.position;
				Quaternion rotation = _collider.transform.rotation;
				bool flag = false;
				foreach (Collider collider in from v in this.colliders
				where v != null
				select v)
				{
					Vector3 vector;
					float num;
					flag |= Physics.ComputePenetration(_collider, position, rotation, collider, collider.transform.position, collider.transform.rotation, out vector, out num);
					MeshCollider meshCollider = collider as MeshCollider;
					if (meshCollider != null)
					{
						CapsuleCollider capsuleCollider = _collider as CapsuleCollider;
						RaycastHit raycastHit;
						flag |= meshCollider.Raycast(new Ray(position, _collider.transform.TransformDirection(Vector3.forward)), out raycastHit, Mathf.Abs(capsuleCollider.height));
					}
				}
				if (this.visible != !flag)
				{
					this.Visible = !flag;
				}
			}

			// Token: 0x170009F6 RID: 2550
			// (set) Token: 0x06003830 RID: 14384 RVA: 0x0014E1AC File Offset: 0x0014C5AC
			public bool Visible
			{
				set
				{
					if (this.renderers.IsNullOrEmpty<Renderer>())
					{
						return;
					}
					this.visible = value;
					foreach (Renderer renderer in from v in this.renderers
					where v != null
					select v)
					{
						renderer.enabled = value;
					}
				}
			}

			// Token: 0x0400389F RID: 14495
			public Collider[] colliders;

			// Token: 0x040038A0 RID: 14496
			public Renderer[] renderers;

			// Token: 0x040038A1 RID: 14497
			private bool visible = true;
		}

		// Token: 0x0200088A RID: 2186
		[Serializable]
		public class Info
		{
			// Token: 0x06003833 RID: 14387 RVA: 0x0014E252 File Offset: 0x0014C652
			public Info()
			{
			}

			// Token: 0x06003834 RID: 14388 RVA: 0x0014E25C File Offset: 0x0014C65C
			public Info(MeshRenderer _renderer)
			{
				this.meshRenderer = _renderer;
				if (this.meshRenderer == null)
				{
					return;
				}
				Material[] sharedMaterials = this.meshRenderer.sharedMaterials;
				this.materialInfos = new ItemComponent.MaterialInfo[sharedMaterials.Length];
				for (int i = 0; i < sharedMaterials.Length; i++)
				{
					this.materialInfos[i] = new ItemComponent.MaterialInfo();
				}
			}

			// Token: 0x06003835 RID: 14389 RVA: 0x0014E2C4 File Offset: 0x0014C6C4
			public Color GetDefColor(int _id)
			{
				Material[] sharedMaterials = this.meshRenderer.sharedMaterials;
				for (int i = 0; i < sharedMaterials.Length; i++)
				{
					ItemComponent.MaterialInfo materialInfo = this.materialInfos.SafeGet(i);
					switch (_id)
					{
					case 0:
						if (materialInfo.isColor1)
						{
							return sharedMaterials[i].GetColor("_Color");
						}
						break;
					case 1:
						if (materialInfo.isColor2)
						{
							return sharedMaterials[i].GetColor("_Color2");
						}
						break;
					case 2:
						if (materialInfo.isColor3)
						{
							return sharedMaterials[i].GetColor("_Color3");
						}
						break;
					case 4:
						if (materialInfo.isEmission)
						{
							return sharedMaterials[i].GetColor("_EmissionColor");
						}
						break;
					}
				}
				return Color.white;
			}

			// Token: 0x040038A4 RID: 14500
			public MeshRenderer meshRenderer;

			// Token: 0x040038A5 RID: 14501
			public ItemComponent.MaterialInfo[] materialInfos;
		}
	}
}
