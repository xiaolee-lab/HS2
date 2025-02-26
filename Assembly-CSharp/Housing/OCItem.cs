using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using AIProject;
using AIProject.Animal;
using Housing.Extension;
using Manager;
using UniRx;
using UnityEngine;

namespace Housing
{
	// Token: 0x0200088F RID: 2191
	public class OCItem : ObjectCtrl
	{
		// Token: 0x06003893 RID: 14483 RVA: 0x0014F208 File Offset: 0x0014D608
		public OCItem(OIItem _oiItem, GameObject _gameObject, CraftInfo _craftInfo, Housing.LoadInfo _loadInfo) : base(_oiItem, _gameObject, _craftInfo)
		{
			this.LoadInfo = _loadInfo;
			if (base.GameObject != null)
			{
				this.m_itemComponent = base.GameObject.GetComponent<ItemComponent>();
			}
			else
			{
				this.m_itemComponent = null;
			}
			if (this.m_itemComponent == null)
			{
				this.m_itemComponent = base.GameObject.AddComponent<ItemComponent>();
				this.m_itemComponent.Setup(false);
			}
			if (this.m_itemComponent != null)
			{
				this.m_itemComponent.SetHPoint();
			}
			Observable.Merge<bool>(new IObservable<bool>[]
			{
				this.subjectColor1,
				this.subjectColor2,
				this.subjectColor3,
				this.subjectEmissionColor
			}).BatchFrame(0, FrameCountType.Update).Subscribe(delegate(IList<bool> _)
			{
				this.UpdateColor();
			});
		}

		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x06003894 RID: 14484 RVA: 0x0014F324 File Offset: 0x0014D724
		public OIItem OIItem
		{
			[CompilerGenerated]
			get
			{
				return base.ObjectInfo as OIItem;
			}
		}

		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x06003895 RID: 14485 RVA: 0x0014F331 File Offset: 0x0014D731
		// (set) Token: 0x06003896 RID: 14486 RVA: 0x0014F339 File Offset: 0x0014D739
		public Housing.LoadInfo LoadInfo { get; private set; }

		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x06003897 RID: 14487 RVA: 0x0014F344 File Offset: 0x0014D744
		public ItemComponent ItemComponent
		{
			[CompilerGenerated]
			get
			{
				ItemComponent result;
				if ((result = this.m_itemComponent) == null)
				{
					GameObject gameObject = base.GameObject;
					result = (this.m_itemComponent = ((gameObject != null) ? gameObject.GetComponent<ItemComponent>() : null));
				}
				return result;
			}
		}

		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x06003898 RID: 14488 RVA: 0x0014F37C File Offset: 0x0014D77C
		// (set) Token: 0x06003899 RID: 14489 RVA: 0x0014F384 File Offset: 0x0014D784
		public HashSet<OCItem> HashOverlap { get; private set; } = new HashSet<OCItem>();

		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x0600389A RID: 14490 RVA: 0x0014F38D File Offset: 0x0014D78D
		// (set) Token: 0x0600389B RID: 14491 RVA: 0x0014F395 File Offset: 0x0014D795
		public HashSet<OCItem> CheckedOverlap { get; private set; } = new HashSet<OCItem>();

		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x0600389C RID: 14492 RVA: 0x0014F39E File Offset: 0x0014D79E
		public override string Name
		{
			[CompilerGenerated]
			get
			{
				return this.LoadInfo.name;
			}
		}

		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x0600389D RID: 14493 RVA: 0x0014F3AB File Offset: 0x0014D7AB
		public int Category
		{
			[CompilerGenerated]
			get
			{
				return this.LoadInfo.category;
			}
		}

		// Token: 0x17000A18 RID: 2584
		// (get) Token: 0x0600389E RID: 14494 RVA: 0x0014F3B8 File Offset: 0x0014D7B8
		public ActionPoint[] ActionPoints
		{
			[CompilerGenerated]
			get
			{
				ItemComponent itemComponent = this.ItemComponent;
				return (itemComponent != null) ? itemComponent.actionPoints : null;
			}
		}

		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x0600389F RID: 14495 RVA: 0x0014F3CF File Offset: 0x0014D7CF
		public FarmPoint[] FarmPoints
		{
			[CompilerGenerated]
			get
			{
				ItemComponent itemComponent = this.ItemComponent;
				return (itemComponent != null) ? itemComponent.farmPoints : null;
			}
		}

		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x060038A0 RID: 14496 RVA: 0x0014F3E6 File Offset: 0x0014D7E6
		public HPoint[] HPoints
		{
			[CompilerGenerated]
			get
			{
				ItemComponent itemComponent = this.ItemComponent;
				return (itemComponent != null) ? itemComponent.hPoints : null;
			}
		}

		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x060038A1 RID: 14497 RVA: 0x0014F3FD File Offset: 0x0014D7FD
		public ItemComponent.ColInfo[] ColInfos
		{
			[CompilerGenerated]
			get
			{
				ItemComponent itemComponent = this.ItemComponent;
				return (itemComponent != null) ? itemComponent.colInfos : null;
			}
		}

		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x060038A2 RID: 14498 RVA: 0x0014F414 File Offset: 0x0014D814
		public PetHomePoint[] PetHomePoints
		{
			[CompilerGenerated]
			get
			{
				ItemComponent itemComponent = this.ItemComponent;
				return (itemComponent != null) ? itemComponent.petHomePoints : null;
			}
		}

		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x060038A3 RID: 14499 RVA: 0x0014F42B File Offset: 0x0014D82B
		public JukePoint[] JukePoints
		{
			[CompilerGenerated]
			get
			{
				ItemComponent itemComponent = this.ItemComponent;
				return (itemComponent != null) ? itemComponent.jukePoints : null;
			}
		}

		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x060038A4 RID: 14500 RVA: 0x0014F442 File Offset: 0x0014D842
		public CraftPoint[] CraftPoints
		{
			[CompilerGenerated]
			get
			{
				ItemComponent itemComponent = this.ItemComponent;
				return (itemComponent != null) ? itemComponent.craftPoints : null;
			}
		}

		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x060038A5 RID: 14501 RVA: 0x0014F459 File Offset: 0x0014D859
		public LightSwitchPoint[] LightSwitchPoints
		{
			[CompilerGenerated]
			get
			{
				ItemComponent itemComponent = this.ItemComponent;
				return (itemComponent != null) ? itemComponent.lightSwitchPoints : null;
			}
		}

		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x060038A6 RID: 14502 RVA: 0x0014F470 File Offset: 0x0014D870
		public bool IsColor
		{
			[CompilerGenerated]
			get
			{
				return this.ItemComponent && this.ItemComponent.IsColor;
			}
		}

		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x060038A7 RID: 14503 RVA: 0x0014F493 File Offset: 0x0014D893
		public bool IsColor1
		{
			[CompilerGenerated]
			get
			{
				return this.ItemComponent && this.ItemComponent.IsColor1;
			}
		}

		// Token: 0x17000A22 RID: 2594
		// (get) Token: 0x060038A8 RID: 14504 RVA: 0x0014F4B6 File Offset: 0x0014D8B6
		public bool IsColor2
		{
			[CompilerGenerated]
			get
			{
				return this.ItemComponent && this.ItemComponent.IsColor2;
			}
		}

		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x060038A9 RID: 14505 RVA: 0x0014F4D9 File Offset: 0x0014D8D9
		public bool IsColor3
		{
			[CompilerGenerated]
			get
			{
				return this.ItemComponent && this.ItemComponent.IsColor3;
			}
		}

		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x060038AA RID: 14506 RVA: 0x0014F4FC File Offset: 0x0014D8FC
		public bool IsEmissionColor
		{
			[CompilerGenerated]
			get
			{
				return this.ItemComponent && this.ItemComponent.IsEmissionColor;
			}
		}

		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x060038AB RID: 14507 RVA: 0x0014F51F File Offset: 0x0014D91F
		// (set) Token: 0x060038AC RID: 14508 RVA: 0x0014F52C File Offset: 0x0014D92C
		public Color Color1
		{
			get
			{
				return this.OIItem.Color1;
			}
			set
			{
				this.OIItem.Color1 = value;
				this.subjectColor1.OnNext(true);
			}
		}

		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x060038AD RID: 14509 RVA: 0x0014F546 File Offset: 0x0014D946
		// (set) Token: 0x060038AE RID: 14510 RVA: 0x0014F553 File Offset: 0x0014D953
		public Color Color2
		{
			get
			{
				return this.OIItem.Color2;
			}
			set
			{
				this.OIItem.Color2 = value;
				this.subjectColor2.OnNext(true);
			}
		}

		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x060038AF RID: 14511 RVA: 0x0014F56D File Offset: 0x0014D96D
		// (set) Token: 0x060038B0 RID: 14512 RVA: 0x0014F57A File Offset: 0x0014D97A
		public Color Color3
		{
			get
			{
				return this.OIItem.Color1;
			}
			set
			{
				this.OIItem.Color3 = value;
				this.subjectColor3.OnNext(true);
			}
		}

		// Token: 0x17000A28 RID: 2600
		// (get) Token: 0x060038B1 RID: 14513 RVA: 0x0014F594 File Offset: 0x0014D994
		// (set) Token: 0x060038B2 RID: 14514 RVA: 0x0014F5A1 File Offset: 0x0014D9A1
		public Color EmissionColor
		{
			get
			{
				return this.OIItem.EmissionColor;
			}
			set
			{
				this.OIItem.EmissionColor = value;
				this.subjectEmissionColor.OnNext(true);
			}
		}

		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x060038B3 RID: 14515 RVA: 0x0014F5BB File Offset: 0x0014D9BB
		public bool IsOption
		{
			get
			{
				return this.LoadInfo.useOption & (this.ItemComponent && this.ItemComponent.IsOption);
			}
		}

		// Token: 0x17000A2A RID: 2602
		// (get) Token: 0x060038B4 RID: 14516 RVA: 0x0014F5EA File Offset: 0x0014D9EA
		// (set) Token: 0x060038B5 RID: 14517 RVA: 0x0014F5F7 File Offset: 0x0014D9F7
		public bool VisibleOption
		{
			get
			{
				return this.OIItem.VisibleOption;
			}
			set
			{
				this.OIItem.VisibleOption = value;
				ItemComponent itemComponent = this.ItemComponent;
				if (itemComponent != null)
				{
					itemComponent.SetVisibleOption(value);
				}
			}
		}

		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x060038B6 RID: 14518 RVA: 0x0014F61A File Offset: 0x0014DA1A
		public override bool IsOverlapNow
		{
			[CompilerGenerated]
			get
			{
				return this.HashOverlap.Count<OCItem>() != 0;
			}
		}

		// Token: 0x17000A2C RID: 2604
		// (get) Token: 0x060038B7 RID: 14519 RVA: 0x0014F630 File Offset: 0x0014DA30
		public IEnumerable<Collider> OverlapColliders
		{
			get
			{
				IEnumerable<Collider> result;
				if (this.ItemComponent)
				{
					if (this.ItemComponent.overlapColliders.IsNullOrEmpty<Collider>())
					{
						result = null;
					}
					else
					{
						result = from v in this.ItemComponent.overlapColliders
						where v != null
						select v;
					}
				}
				else
				{
					result = null;
				}
				return result;
			}
		}

		// Token: 0x060038B8 RID: 14520 RVA: 0x0014F69C File Offset: 0x0014DA9C
		public override bool OnRemoving()
		{
			bool flag = true;
			if (this.ItemComponent != null)
			{
				if (!this.ItemComponent.petHomePoints.IsNullOrEmpty<PetHomePoint>())
				{
					foreach (PetHomePoint petHomePoint in this.ItemComponent.petHomePoints)
					{
						flag &= petHomePoint.CanDelete();
					}
				}
				if (!this.ItemComponent.craftPoints.IsNullOrEmpty<CraftPoint>())
				{
					foreach (CraftPoint craftPoint in this.ItemComponent.craftPoints)
					{
						flag &= craftPoint.CanDelete();
					}
				}
			}
			return flag;
		}

		// Token: 0x060038B9 RID: 14521 RVA: 0x0014F74C File Offset: 0x0014DB4C
		public override void OnDelete()
		{
			foreach (OCItem ocitem in this.HashOverlap)
			{
				ocitem.HashOverlap.Remove(this);
			}
			this.HashOverlap.Clear();
			base.OnDelete();
		}

		// Token: 0x060038BA RID: 14522 RVA: 0x0014F7C0 File Offset: 0x0014DBC0
		public override void OnDeleteChild()
		{
			foreach (OCItem ocitem in this.HashOverlap)
			{
				ocitem.HashOverlap.Remove(this);
			}
			this.HashOverlap.Clear();
		}

		// Token: 0x060038BB RID: 14523 RVA: 0x0014F830 File Offset: 0x0014DC30
		public override void OnDestroy()
		{
			foreach (ActionPoint actionPoint in from v in this.ActionPoints
			where v != null
			select v)
			{
				Singleton<Map>.Instance.RemoveAppendActionPoint(actionPoint);
				Singleton<Map>.Instance.RemoveAppendCommandable(actionPoint);
				Singleton<Map>.Instance.RemoveAgentSearchActionPoint(actionPoint);
				Singleton<Map>.Instance.UnregisterRuntimePoint(actionPoint, false);
			}
			foreach (FarmPoint farmPoint in from v in this.FarmPoints
			where v != null
			select v)
			{
				Singleton<Map>.Instance.RemoveRuntimeFarmPoint(farmPoint);
				Singleton<Map>.Instance.RemoveAppendCommandable(farmPoint);
				Singleton<Map>.Instance.UnregisterRuntimePoint(farmPoint, false);
			}
			foreach (PetHomePoint petHomePoint in from v in this.PetHomePoints
			where v != null
			select v)
			{
				Singleton<Map>.Instance.RemovePetHomePoint(petHomePoint);
				Singleton<Map>.Instance.RemoveAppendCommandable(petHomePoint);
				Singleton<Map>.Instance.UnregisterRuntimePoint(petHomePoint, false);
			}
			foreach (JukePoint jukePoint in from v in this.JukePoints
			where v != null
			select v)
			{
				Singleton<Map>.Instance.RemoveJukePoint(jukePoint);
				Singleton<Map>.Instance.RemoveAppendCommandable(jukePoint);
				Singleton<Map>.Instance.UnregisterRuntimePoint(jukePoint, false);
			}
			foreach (CraftPoint craftPoint in from v in this.CraftPoints
			where v != null
			select v)
			{
				Singleton<Map>.Instance.RemoveCraftPoint(craftPoint);
				Singleton<Map>.Instance.RemoveAppendCommandable(craftPoint);
				Singleton<Map>.Instance.UnregisterRuntimePoint(craftPoint, false);
			}
			foreach (LightSwitchPoint lightSwitchPoint in from v in this.LightSwitchPoints
			where v != null
			select v)
			{
				Singleton<Map>.Instance.RemoveRuntimeLightSwitchPoint(lightSwitchPoint);
				Singleton<Map>.Instance.RemoveAppendCommandable(lightSwitchPoint);
				Singleton<Map>.Instance.UnregisterRuntimePoint(lightSwitchPoint, false);
			}
			this.m_itemComponent = null;
			base.OnDestroy();
		}

		// Token: 0x060038BC RID: 14524 RVA: 0x0014FB9C File Offset: 0x0014DF9C
		public override void RestoreObject(GameObject _gameObject)
		{
			base.RestoreObject(_gameObject);
			this.m_itemComponent = base.GameObject.GetComponent<ItemComponent>();
			if (this.m_itemComponent == null)
			{
				this.m_itemComponent = base.GameObject.AddComponent<ItemComponent>();
				this.m_itemComponent.Setup(false);
			}
			if (this.m_itemComponent != null)
			{
				this.m_itemComponent.SetHPoint();
			}
		}

		// Token: 0x060038BD RID: 14525 RVA: 0x0014FC08 File Offset: 0x0014E008
		public override void GetLocalMinMax(Vector3 _pos, Quaternion _rot, Transform _root, ref Vector3 _min, ref Vector3 _max)
		{
			Matrix4x4 matrix4x = Matrix4x4.TRS(_root.position, _root.rotation, Vector3.one);
			Matrix4x4 rhs = Matrix4x4.TRS(_pos, _rot, Vector3.one);
			rhs = matrix4x.inverse * rhs;
			Vector3 min = this.ItemComponent.min;
			Vector3 max = this.ItemComponent.max;
			Vector3[] source = new Vector3[]
			{
				rhs.MultiplyPoint3x4(new Vector3(min.x, min.y, min.z)),
				rhs.MultiplyPoint3x4(new Vector3(min.x, min.y, max.z)),
				rhs.MultiplyPoint3x4(new Vector3(max.x, min.y, min.z)),
				rhs.MultiplyPoint3x4(new Vector3(max.x, min.y, max.z)),
				rhs.MultiplyPoint3x4(new Vector3(min.x, max.y, min.z)),
				rhs.MultiplyPoint3x4(new Vector3(min.x, max.y, max.z)),
				rhs.MultiplyPoint3x4(new Vector3(max.x, max.y, min.z)),
				rhs.MultiplyPoint3x4(new Vector3(max.x, max.y, max.z))
			};
			_min = new Vector3(source.Min((Vector3 _v) => _v.x), source.Min((Vector3 _v) => _v.y), source.Min((Vector3 _v) => _v.z));
			_max = new Vector3(source.Max((Vector3 _v) => _v.x), source.Max((Vector3 _v) => _v.y), source.Max((Vector3 _v) => _v.z));
		}

		// Token: 0x060038BE RID: 14526 RVA: 0x0014FEB8 File Offset: 0x0014E2B8
		public override void GetActionPoint(ref List<ActionPoint> _points)
		{
			ActionPoint[] actionPoints = this.ActionPoints;
			if (!actionPoints.IsNullOrEmpty<ActionPoint>())
			{
				_points.AddRange(from v in actionPoints
				where v != null
				select v);
			}
		}

		// Token: 0x060038BF RID: 14527 RVA: 0x0014FF04 File Offset: 0x0014E304
		public override void GetFarmPoint(ref List<FarmPoint> _points)
		{
			FarmPoint[] farmPoints = this.FarmPoints;
			if (!farmPoints.IsNullOrEmpty<FarmPoint>())
			{
				_points.AddRange(from v in farmPoints
				where v != null
				select v);
			}
		}

		// Token: 0x060038C0 RID: 14528 RVA: 0x0014FF50 File Offset: 0x0014E350
		public override void GetHPoint(ref List<HPoint> _points)
		{
			HPoint[] hpoints = this.HPoints;
			if (!hpoints.IsNullOrEmpty<HPoint>())
			{
				_points.AddRange(from v in hpoints
				where v != null
				select v);
			}
		}

		// Token: 0x060038C1 RID: 14529 RVA: 0x0014FF9C File Offset: 0x0014E39C
		public override void GetColInfo(ref List<ItemComponent.ColInfo> _infos)
		{
			ItemComponent.ColInfo[] colInfos = this.ColInfos;
			if (!colInfos.IsNullOrEmpty<ItemComponent.ColInfo>())
			{
				_infos.AddRange(from v in colInfos
				where v != null
				select v);
			}
		}

		// Token: 0x060038C2 RID: 14530 RVA: 0x0014FFE8 File Offset: 0x0014E3E8
		public override void GetPetHomePoint(ref List<PetHomePoint> _points)
		{
			PetHomePoint[] petHomePoints = this.PetHomePoints;
			if (!petHomePoints.IsNullOrEmpty<PetHomePoint>())
			{
				_points.AddRange(from v in petHomePoints
				where v != null
				select v);
			}
		}

		// Token: 0x060038C3 RID: 14531 RVA: 0x00150034 File Offset: 0x0014E434
		public override void GetJukePoint(ref List<JukePoint> _points)
		{
			JukePoint[] jukePoints = this.JukePoints;
			if (!jukePoints.IsNullOrEmpty<JukePoint>())
			{
				_points.AddRange(from v in jukePoints
				where v != null
				select v);
			}
		}

		// Token: 0x060038C4 RID: 14532 RVA: 0x00150080 File Offset: 0x0014E480
		public override void GetCraftPoint(ref List<CraftPoint> _points)
		{
			CraftPoint[] craftPoints = this.CraftPoints;
			if (!craftPoints.IsNullOrEmpty<CraftPoint>())
			{
				_points.AddRange(from v in craftPoints
				where v != null
				select v);
			}
		}

		// Token: 0x060038C5 RID: 14533 RVA: 0x001500CC File Offset: 0x0014E4CC
		public override void GetLightSwitchPoint(ref List<LightSwitchPoint> _points)
		{
			LightSwitchPoint[] lightSwitchPoints = this.LightSwitchPoints;
			if (!lightSwitchPoints.IsNullOrEmpty<LightSwitchPoint>())
			{
				_points.AddRange(from v in lightSwitchPoints
				where v != null
				select v);
			}
		}

		// Token: 0x060038C6 RID: 14534 RVA: 0x00150115 File Offset: 0x0014E515
		public override void GetUsedNum(int _no, ref int _num)
		{
			if (this.OIItem.ID == _no)
			{
				_num++;
			}
		}

		// Token: 0x060038C7 RID: 14535 RVA: 0x00150130 File Offset: 0x0014E530
		public void ResetColor()
		{
			if (this.IsColor1)
			{
				this.Color1 = this.ItemComponent.defColor1;
			}
			if (this.IsColor2)
			{
				this.Color2 = this.ItemComponent.defColor2;
			}
			if (this.IsColor3)
			{
				this.Color3 = this.ItemComponent.defColor3;
			}
			if (this.IsEmissionColor)
			{
				this.EmissionColor = this.ItemComponent.defEmissionColor;
			}
		}

		// Token: 0x060038C8 RID: 14536 RVA: 0x001501B0 File Offset: 0x0014E5B0
		public void UpdateColor()
		{
			if (this.ItemComponent == null || !this.ItemComponent.IsColor)
			{
				return;
			}
			OIItem info = this.OIItem;
			foreach (ItemComponent.Info info2 in this.ItemComponent.renderers)
			{
				Material[] materials = info2.meshRenderer.materials;
				for (int j = 0; j < materials.Length; j++)
				{
					Material m = materials[j];
					if (!(m == null))
					{
						info2.materialInfos.SafeProc(j, delegate(ItemComponent.MaterialInfo _info)
						{
							if (_info.isColor1)
							{
								m.SetColor(ItemShader._Color, info.Color1);
							}
							if (_info.isColor2)
							{
								m.SetColor(ItemShader._Color2, info.Color2);
							}
							if (_info.isColor3)
							{
								m.SetColor(ItemShader._Color3, info.Color3);
							}
							if (_info.isEmission)
							{
								m.SetColor(ItemShader._EmissionColor, info.EmissionColor);
							}
						});
					}
				}
				info2.meshRenderer.materials = materials;
			}
		}

		// Token: 0x060038C9 RID: 14537 RVA: 0x00150298 File Offset: 0x0014E698
		public override bool CheckOverlap(ObjectCtrl _oc, bool _load = false)
		{
			if (this.ItemComponent == null)
			{
				return false;
			}
			OCItem ocitem = _oc as OCItem;
			if (ocitem == null || this == ocitem)
			{
				return false;
			}
			if (_load && this.CheckedOverlap.Contains(ocitem))
			{
				return this.HashOverlap.Contains(ocitem);
			}
			if (!ocitem.ItemComponent.overlap || !this.ItemComponent.overlap)
			{
				if (_load)
				{
					this.CheckedOverlap.Add(ocitem);
					ocitem.CheckedOverlap.Add(this);
				}
				return false;
			}
			bool flag = false;
			IEnumerable<Collider> overlapColliders = ocitem.OverlapColliders;
			if (!overlapColliders.IsNullOrEmpty<Collider>())
			{
				Vector3 zero = Vector3.zero;
				float num = 0f;
				IEnumerable<Collider> overlapColliders2 = this.OverlapColliders;
				if (overlapColliders2.IsNullOrEmpty<Collider>())
				{
					Bounds bounds = default(Bounds);
					bounds.SetMinMax(this.ItemComponent.min, this.ItemComponent.max);
					BoxCollider boxCollider = Singleton<CraftScene>.Instance.TestColliders.SafeGet(0);
					boxCollider.center = bounds.center;
					boxCollider.size = bounds.size - OCItem.correctionSize;
					foreach (Collider collider in overlapColliders)
					{
						flag |= Physics.ComputePenetration(boxCollider, this.Position, this.Rotation, collider, collider.transform.position, collider.transform.rotation, out zero, out num);
					}
				}
				else
				{
					foreach (Collider collider2 in overlapColliders)
					{
						foreach (Collider collider3 in overlapColliders2)
						{
							flag |= Physics.ComputePenetration(collider2, collider2.transform.position, collider2.transform.rotation, collider3, collider3.transform.position, collider3.transform.rotation, out zero, out num);
						}
					}
				}
			}
			else
			{
				flag = this.CheckOverlapSize(ocitem);
			}
			if (flag)
			{
				this.HashOverlap.Add(ocitem);
				ocitem.HashOverlap.Add(this);
			}
			else
			{
				this.HashOverlap.Remove(ocitem);
				ocitem.HashOverlap.Remove(this);
			}
			if (_load)
			{
				this.CheckedOverlap.Add(ocitem);
				ocitem.CheckedOverlap.Add(this);
			}
			return flag;
		}

		// Token: 0x060038CA RID: 14538 RVA: 0x00150578 File Offset: 0x0014E978
		private bool CheckOverlapSize(OCItem _oc)
		{
			bool flag = false;
			IEnumerable<Collider> overlapColliders = this.OverlapColliders;
			if (overlapColliders.IsNullOrEmpty<Collider>())
			{
				Vector3 zero = Vector3.zero;
				Vector3 zero2 = Vector3.zero;
				_oc.GetLocalMinMax(_oc.Position, _oc.Rotation, base.CraftInfo.ObjRoot.transform, ref zero, ref zero2);
				Bounds bounds = default(Bounds);
				bounds.SetMinMax(zero, zero2);
				bounds.size -= OCItem.correctionSize;
				Vector3 zero3 = Vector3.zero;
				Vector3 zero4 = Vector3.zero;
				this.GetLocalMinMax(this.Position, this.Rotation, base.CraftInfo.ObjRoot.transform, ref zero3, ref zero4);
				Bounds bounds2 = default(Bounds);
				bounds2.SetMinMax(zero3, zero4);
				bounds2.size -= OCItem.correctionSize;
				return bounds.Intersects(bounds2);
			}
			Bounds bounds3 = default(Bounds);
			bounds3.SetMinMax(_oc.ItemComponent.min, _oc.ItemComponent.max);
			BoxCollider boxCollider = Singleton<CraftScene>.Instance.TestColliders.SafeGet(0);
			boxCollider.center = bounds3.center;
			boxCollider.size = bounds3.size - OCItem.correctionSize;
			Vector3 zero5 = Vector3.zero;
			float num = 0f;
			foreach (Collider collider in overlapColliders)
			{
				flag |= Physics.ComputePenetration(boxCollider, _oc.Position, _oc.Rotation, collider, collider.transform.position, collider.transform.rotation, out zero5, out num);
			}
			return flag;
		}

		// Token: 0x060038CB RID: 14539 RVA: 0x00150744 File Offset: 0x0014EB44
		public override void BeforeCheckOverlap()
		{
			this.CheckedOverlap.Clear();
			this.SetOverlapColliders(true);
		}

		// Token: 0x060038CC RID: 14540 RVA: 0x00150758 File Offset: 0x0014EB58
		public override void AfterCheckOverlap()
		{
			this.CheckedOverlap.Clear();
		}

		// Token: 0x060038CD RID: 14541 RVA: 0x00150765 File Offset: 0x0014EB65
		public override void SetOverlapColliders(bool _flag)
		{
			ItemComponent itemComponent = this.ItemComponent;
			if (itemComponent != null)
			{
				itemComponent.SetOverlapColliders(_flag);
			}
		}

		// Token: 0x060038CE RID: 14542 RVA: 0x0015077C File Offset: 0x0014EB7C
		public override void GetOverlapObject(ref List<ObjectCtrl> _lst)
		{
			if (this.IsOverlapNow)
			{
				_lst.Add(this);
			}
		}

		// Token: 0x040038B5 RID: 14517
		private ItemComponent m_itemComponent;

		// Token: 0x040038B8 RID: 14520
		private Subject<bool> subjectColor1 = new Subject<bool>();

		// Token: 0x040038B9 RID: 14521
		private Subject<bool> subjectColor2 = new Subject<bool>();

		// Token: 0x040038BA RID: 14522
		private Subject<bool> subjectColor3 = new Subject<bool>();

		// Token: 0x040038BB RID: 14523
		private Subject<bool> subjectEmissionColor = new Subject<bool>();

		// Token: 0x040038BC RID: 14524
		private static Vector3 correctionSize = new Vector3(0.2f, 0.2f, 0.2f);
	}
}
