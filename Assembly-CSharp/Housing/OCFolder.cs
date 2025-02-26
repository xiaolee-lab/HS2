using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using AIProject;
using AIProject.Animal;
using UnityEngine;

namespace Housing
{
	// Token: 0x0200088D RID: 2189
	public class OCFolder : ObjectCtrl
	{
		// Token: 0x06003876 RID: 14454 RVA: 0x0014E992 File Offset: 0x0014CD92
		public OCFolder(OIFolder _oiFolder, GameObject _gameObject, CraftInfo _craftInfo) : base(_oiFolder, _gameObject, _craftInfo)
		{
		}

		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x06003877 RID: 14455 RVA: 0x0014E9A8 File Offset: 0x0014CDA8
		public OIFolder OIFolder
		{
			[CompilerGenerated]
			get
			{
				return base.ObjectInfo as OIFolder;
			}
		}

		// Token: 0x17000A0D RID: 2573
		// (get) Token: 0x06003878 RID: 14456 RVA: 0x0014E9B5 File Offset: 0x0014CDB5
		// (set) Token: 0x06003879 RID: 14457 RVA: 0x0014E9BD File Offset: 0x0014CDBD
		public Dictionary<IObjectInfo, ObjectCtrl> Child { get; private set; } = new Dictionary<IObjectInfo, ObjectCtrl>();

		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x0600387A RID: 14458 RVA: 0x0014E9C6 File Offset: 0x0014CDC6
		public override string Name
		{
			[CompilerGenerated]
			get
			{
				return this.OIFolder.Name;
			}
		}

		// Token: 0x17000A0F RID: 2575
		// (get) Token: 0x0600387B RID: 14459 RVA: 0x0014E9D3 File Offset: 0x0014CDD3
		public override bool IsOverlapNow
		{
			[CompilerGenerated]
			get
			{
				return this.Child.Any((KeyValuePair<IObjectInfo, ObjectCtrl> v) => v.Value.IsOverlapNow);
			}
		}

		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x0600387C RID: 14460 RVA: 0x0014E9FD File Offset: 0x0014CDFD
		public List<ObjectCtrl> ChildObjectCtrls
		{
			get
			{
				return (from v in this.Child
				orderby this.OIFolder.Child.FindIndex((IObjectInfo _i) => _i == v.Key)
				select v.Value).ToList<ObjectCtrl>();
			}
		}

		// Token: 0x0600387D RID: 14461 RVA: 0x0014EA40 File Offset: 0x0014CE40
		public override bool OnRemoving()
		{
			bool flag = true;
			foreach (KeyValuePair<IObjectInfo, ObjectCtrl> keyValuePair in this.Child)
			{
				flag &= keyValuePair.Value.OnRemoving();
			}
			return flag;
		}

		// Token: 0x0600387E RID: 14462 RVA: 0x0014EAA8 File Offset: 0x0014CEA8
		public override void OnDelete()
		{
			foreach (KeyValuePair<IObjectInfo, ObjectCtrl> keyValuePair in this.Child)
			{
				keyValuePair.Value.OnDeleteChild();
			}
			base.OnDelete();
		}

		// Token: 0x0600387F RID: 14463 RVA: 0x0014EB10 File Offset: 0x0014CF10
		public override void OnDeleteChild()
		{
			foreach (KeyValuePair<IObjectInfo, ObjectCtrl> keyValuePair in this.Child)
			{
				keyValuePair.Value.OnDeleteChild();
			}
		}

		// Token: 0x06003880 RID: 14464 RVA: 0x0014EB74 File Offset: 0x0014CF74
		public override void OnDestroy()
		{
			foreach (KeyValuePair<IObjectInfo, ObjectCtrl> keyValuePair in this.Child)
			{
				keyValuePair.Value.OnDestroy();
			}
			base.OnDestroy();
		}

		// Token: 0x06003881 RID: 14465 RVA: 0x0014EBDC File Offset: 0x0014CFDC
		public override void GetActionPoint(ref List<ActionPoint> _points)
		{
			foreach (KeyValuePair<IObjectInfo, ObjectCtrl> keyValuePair in this.Child)
			{
				keyValuePair.Value.GetActionPoint(ref _points);
			}
		}

		// Token: 0x06003882 RID: 14466 RVA: 0x0014EC40 File Offset: 0x0014D040
		public override void GetFarmPoint(ref List<FarmPoint> _points)
		{
			foreach (KeyValuePair<IObjectInfo, ObjectCtrl> keyValuePair in this.Child)
			{
				keyValuePair.Value.GetFarmPoint(ref _points);
			}
		}

		// Token: 0x06003883 RID: 14467 RVA: 0x0014ECA4 File Offset: 0x0014D0A4
		public override void GetHPoint(ref List<HPoint> _points)
		{
			foreach (KeyValuePair<IObjectInfo, ObjectCtrl> keyValuePair in this.Child)
			{
				keyValuePair.Value.GetHPoint(ref _points);
			}
		}

		// Token: 0x06003884 RID: 14468 RVA: 0x0014ED08 File Offset: 0x0014D108
		public override void GetColInfo(ref List<ItemComponent.ColInfo> _infos)
		{
			foreach (KeyValuePair<IObjectInfo, ObjectCtrl> keyValuePair in this.Child)
			{
				keyValuePair.Value.GetColInfo(ref _infos);
			}
		}

		// Token: 0x06003885 RID: 14469 RVA: 0x0014ED6C File Offset: 0x0014D16C
		public override void GetPetHomePoint(ref List<PetHomePoint> _points)
		{
			foreach (KeyValuePair<IObjectInfo, ObjectCtrl> keyValuePair in this.Child)
			{
				keyValuePair.Value.GetPetHomePoint(ref _points);
			}
		}

		// Token: 0x06003886 RID: 14470 RVA: 0x0014EDD0 File Offset: 0x0014D1D0
		public override void GetJukePoint(ref List<JukePoint> _points)
		{
			foreach (KeyValuePair<IObjectInfo, ObjectCtrl> keyValuePair in this.Child)
			{
				keyValuePair.Value.GetJukePoint(ref _points);
			}
		}

		// Token: 0x06003887 RID: 14471 RVA: 0x0014EE34 File Offset: 0x0014D234
		public override void GetCraftPoint(ref List<CraftPoint> _points)
		{
			foreach (KeyValuePair<IObjectInfo, ObjectCtrl> keyValuePair in this.Child)
			{
				keyValuePair.Value.GetCraftPoint(ref _points);
			}
		}

		// Token: 0x06003888 RID: 14472 RVA: 0x0014EE98 File Offset: 0x0014D298
		public override void GetLightSwitchPoint(ref List<LightSwitchPoint> _points)
		{
			foreach (KeyValuePair<IObjectInfo, ObjectCtrl> keyValuePair in this.Child)
			{
				keyValuePair.Value.GetLightSwitchPoint(ref _points);
			}
		}

		// Token: 0x06003889 RID: 14473 RVA: 0x0014EEFC File Offset: 0x0014D2FC
		public override void GetUsedNum(int _no, ref int _num)
		{
			foreach (KeyValuePair<IObjectInfo, ObjectCtrl> keyValuePair in this.Child)
			{
				keyValuePair.Value.GetUsedNum(_no, ref _num);
			}
		}

		// Token: 0x0600388A RID: 14474 RVA: 0x0014EF60 File Offset: 0x0014D360
		public override bool CheckOverlap(ObjectCtrl _oc, bool _load = false)
		{
			bool flag = false;
			foreach (KeyValuePair<IObjectInfo, ObjectCtrl> keyValuePair in this.Child)
			{
				flag |= keyValuePair.Value.CheckOverlap(_oc, _load);
			}
			return flag;
		}

		// Token: 0x0600388B RID: 14475 RVA: 0x0014EFCC File Offset: 0x0014D3CC
		public override void BeforeCheckOverlap()
		{
			foreach (KeyValuePair<IObjectInfo, ObjectCtrl> keyValuePair in this.Child)
			{
				keyValuePair.Value.BeforeCheckOverlap();
			}
		}

		// Token: 0x0600388C RID: 14476 RVA: 0x0014F030 File Offset: 0x0014D430
		public override void AfterCheckOverlap()
		{
			foreach (KeyValuePair<IObjectInfo, ObjectCtrl> keyValuePair in this.Child)
			{
				keyValuePair.Value.AfterCheckOverlap();
			}
		}

		// Token: 0x0600388D RID: 14477 RVA: 0x0014F094 File Offset: 0x0014D494
		public override void SetOverlapColliders(bool _flag)
		{
			foreach (KeyValuePair<IObjectInfo, ObjectCtrl> keyValuePair in this.Child)
			{
				keyValuePair.Value.SetOverlapColliders(_flag);
			}
		}

		// Token: 0x0600388E RID: 14478 RVA: 0x0014F0F8 File Offset: 0x0014D4F8
		public override void GetOverlapObject(ref List<ObjectCtrl> _lst)
		{
			foreach (KeyValuePair<IObjectInfo, ObjectCtrl> keyValuePair in this.Child)
			{
				keyValuePair.Value.GetOverlapObject(ref _lst);
			}
		}
	}
}
