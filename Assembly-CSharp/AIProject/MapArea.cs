using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using AIProject.Animal;
using Manager;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000C13 RID: 3091
	public class MapArea : MonoBehaviour
	{
		// Token: 0x170012A5 RID: 4773
		// (get) Token: 0x06005F9F RID: 24479 RVA: 0x00285506 File Offset: 0x00283906
		public MapArea.AreaType Type
		{
			[CompilerGenerated]
			get
			{
				return this._type;
			}
		}

		// Token: 0x170012A6 RID: 4774
		// (get) Token: 0x06005FA0 RID: 24480 RVA: 0x0028550E File Offset: 0x0028390E
		// (set) Token: 0x06005FA1 RID: 24481 RVA: 0x00285516 File Offset: 0x00283916
		public int ChunkID { get; set; }

		// Token: 0x170012A7 RID: 4775
		// (get) Token: 0x06005FA2 RID: 24482 RVA: 0x0028551F File Offset: 0x0028391F
		public int AreaID
		{
			[CompilerGenerated]
			get
			{
				return this._areaID;
			}
		}

		// Token: 0x170012A8 RID: 4776
		// (get) Token: 0x06005FA3 RID: 24483 RVA: 0x00285527 File Offset: 0x00283927
		public List<Waypoint> Waypoints
		{
			[CompilerGenerated]
			get
			{
				return this._waypoints;
			}
		}

		// Token: 0x170012A9 RID: 4777
		// (get) Token: 0x06005FA4 RID: 24484 RVA: 0x0028552F File Offset: 0x0028392F
		public List<BasePoint> BasePoints
		{
			[CompilerGenerated]
			get
			{
				return this._basePoints;
			}
		}

		// Token: 0x170012AA RID: 4778
		// (get) Token: 0x06005FA5 RID: 24485 RVA: 0x00285537 File Offset: 0x00283937
		public List<DevicePoint> DevicePoints
		{
			[CompilerGenerated]
			get
			{
				return this._devicePoints;
			}
		}

		// Token: 0x170012AB RID: 4779
		// (get) Token: 0x06005FA6 RID: 24486 RVA: 0x0028553F File Offset: 0x0028393F
		public List<FarmPoint> FarmPoints
		{
			[CompilerGenerated]
			get
			{
				return this._farmPoints;
			}
		}

		// Token: 0x170012AC RID: 4780
		// (get) Token: 0x06005FA7 RID: 24487 RVA: 0x00285547 File Offset: 0x00283947
		public List<ShipPoint> ShipPoints
		{
			[CompilerGenerated]
			get
			{
				return this._shipPoints;
			}
		}

		// Token: 0x170012AD RID: 4781
		// (get) Token: 0x06005FA8 RID: 24488 RVA: 0x0028554F File Offset: 0x0028394F
		public List<ActionPoint> ActionPoints
		{
			[CompilerGenerated]
			get
			{
				return this._actionPoints;
			}
		}

		// Token: 0x170012AE RID: 4782
		// (get) Token: 0x06005FA9 RID: 24489 RVA: 0x00285557 File Offset: 0x00283957
		public List<MerchantPoint> MerchantPoints
		{
			[CompilerGenerated]
			get
			{
				return this._merchantPoints;
			}
		}

		// Token: 0x170012AF RID: 4783
		// (get) Token: 0x06005FAA RID: 24490 RVA: 0x0028555F File Offset: 0x0028395F
		public List<EventPoint> EventPoints
		{
			[CompilerGenerated]
			get
			{
				return this._eventPoints;
			}
		}

		// Token: 0x170012B0 RID: 4784
		// (get) Token: 0x06005FAB RID: 24491 RVA: 0x00285567 File Offset: 0x00283967
		public List<StoryPoint> StoryPoints
		{
			[CompilerGenerated]
			get
			{
				return this._storyPoints;
			}
		}

		// Token: 0x170012B1 RID: 4785
		// (get) Token: 0x06005FAC RID: 24492 RVA: 0x0028556F File Offset: 0x0028396F
		public List<AnimalPoint> AnimalPoints
		{
			[CompilerGenerated]
			get
			{
				return this._animalPoints;
			}
		}

		// Token: 0x170012B2 RID: 4786
		// (get) Token: 0x06005FAD RID: 24493 RVA: 0x00285577 File Offset: 0x00283977
		public List<ActionPoint> AppendActionPoints
		{
			[CompilerGenerated]
			get
			{
				return this._appendActionPoints;
			}
		}

		// Token: 0x170012B3 RID: 4787
		// (get) Token: 0x06005FAE RID: 24494 RVA: 0x0028557F File Offset: 0x0028397F
		public List<FarmPoint> AppendFarmPoints
		{
			[CompilerGenerated]
			get
			{
				return this._appendFarmPoints;
			}
		}

		// Token: 0x170012B4 RID: 4788
		// (get) Token: 0x06005FAF RID: 24495 RVA: 0x00285587 File Offset: 0x00283987
		public List<PetHomePoint> AppendPetHomePoints
		{
			[CompilerGenerated]
			get
			{
				return this._appendPetHomePoints;
			}
		}

		// Token: 0x170012B5 RID: 4789
		// (get) Token: 0x06005FB0 RID: 24496 RVA: 0x0028558F File Offset: 0x0028398F
		public List<JukePoint> AppendJukePoints
		{
			[CompilerGenerated]
			get
			{
				return this._appendJukePoints;
			}
		}

		// Token: 0x170012B6 RID: 4790
		// (get) Token: 0x06005FB1 RID: 24497 RVA: 0x00285597 File Offset: 0x00283997
		public List<CraftPoint> AppendCraftPoints
		{
			[CompilerGenerated]
			get
			{
				return this._appendCraftPoints;
			}
		}

		// Token: 0x170012B7 RID: 4791
		// (get) Token: 0x06005FB2 RID: 24498 RVA: 0x0028559F File Offset: 0x0028399F
		public List<LightSwitchPoint> AppendLightSwitchPoints
		{
			[CompilerGenerated]
			get
			{
				return this._appendLightSwitchPoints;
			}
		}

		// Token: 0x170012B8 RID: 4792
		// (get) Token: 0x06005FB3 RID: 24499 RVA: 0x002855A7 File Offset: 0x002839A7
		public List<HPoint> AppendHPoints
		{
			[CompilerGenerated]
			get
			{
				return this._appendHPoints;
			}
		}

		// Token: 0x170012B9 RID: 4793
		// (set) Token: 0x06005FB4 RID: 24500 RVA: 0x002855AF File Offset: 0x002839AF
		public Collider[] hColliders
		{
			set
			{
				this._hColliders = value;
			}
		}

		// Token: 0x06005FB5 RID: 24501 RVA: 0x002855B8 File Offset: 0x002839B8
		public IEnumerator Load(Waypoint[] wp, BasePoint[] bp, DevicePoint[] dp, FarmPoint[] fp, ShipPoint[] shipPt, ActionPoint[] ap, MerchantPoint[] mp, EventPoint[] ep, StoryPoint[] sp, AnimalPoint[] pap)
		{
			int loadInFrame = wp.Length / 60;
			int loadCount = 0;
			LayerMask layer = Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.AreaDetectionLayer;
			LayerMask roofLayer = Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.RoofLayer;
			foreach (Waypoint pt in wp)
			{
				int num;
				loadCount = (num = loadCount) + 1;
				if (num == loadInFrame)
				{
					loadCount = 0;
					yield return null;
				}
				this.CheckPointOnTheArea<Waypoint>(pt, this._waypoints, layer, roofLayer);
			}
			this.AddPoints<BasePoint>(bp, this._basePoints, layer, roofLayer);
			this.AddPoints<DevicePoint>(dp, this._devicePoints, layer, roofLayer);
			this.AddPoints<FarmPoint>(fp, this._farmPoints, layer, roofLayer);
			this.AddPoints<ShipPoint>(shipPt, this._shipPoints, layer, roofLayer);
			foreach (ActionPoint pt2 in ap)
			{
				int num;
				loadCount = (num = loadCount) + 1;
				if (num == loadInFrame)
				{
					loadCount = 0;
					yield return null;
				}
				this.CheckPointOnTheArea<ActionPoint>(pt2, this._actionPoints, layer, roofLayer);
			}
			foreach (MerchantPoint merchantPoint in mp)
			{
				this.CheckPointOnTheArea<MerchantPoint>(merchantPoint, this._merchantPoints, layer, roofLayer);
				if (merchantPoint.OwnerArea == this)
				{
					merchantPoint.Refresh();
				}
			}
			this.AddPoints<EventPoint>(ep, this._eventPoints, layer, roofLayer);
			this.AddPoints<StoryPoint>(sp, this._storyPoints, layer, roofLayer);
			this.AddPoints<AnimalPoint>(pap, this._animalPoints, layer, roofLayer);
			yield break;
		}

		// Token: 0x06005FB6 RID: 24502 RVA: 0x00285620 File Offset: 0x00283A20
		public void AddPoints<T>(T[] pts, List<T> list, LayerMask layer, LayerMask roofLayer) where T : Point
		{
			foreach (T point in pts)
			{
				this.CheckPointOnTheArea<T>(point, list, layer, roofLayer);
			}
		}

		// Token: 0x06005FB7 RID: 24503 RVA: 0x00285658 File Offset: 0x00283A58
		public void CheckPointOnTheArea<T>(T point, List<T> pointList, LayerMask layer, LayerMask roofLayer) where T : Point
		{
			Vector3 origin = point.transform.position + Vector3.up * 10f;
			RaycastHit raycastHit;
			if (Physics.Raycast(origin, Vector3.down, out raycastHit, 1000f, layer) && this.ContainsCollider(raycastHit.collider))
			{
				pointList.Add(point);
				point.OwnerArea = this;
				point.AreaType = ((!Physics.Raycast(origin, Vector3.up, out raycastHit, 1000f, roofLayer)) ? MapArea.AreaType.Normal : MapArea.AreaType.Indoor);
			}
		}

		// Token: 0x06005FB8 RID: 24504 RVA: 0x00285704 File Offset: 0x00283B04
		public bool CheckPointOnTheArea<T>(T point, LayerMask layer, LayerMask roofLayer, float offsetY = 10f) where T : Point
		{
			Vector3 origin = point.transform.position + Vector3.up * offsetY;
			RaycastHit raycastHit;
			if (Physics.Raycast(origin, Vector3.down, out raycastHit, 1000f, layer) && this.ContainsCollider(raycastHit.collider))
			{
				point.OwnerArea = this;
				point.AreaType = ((!Physics.Raycast(origin, Vector3.up, out raycastHit, 1000f, roofLayer)) ? MapArea.AreaType.Normal : MapArea.AreaType.Indoor);
				return true;
			}
			return false;
		}

		// Token: 0x06005FB9 RID: 24505 RVA: 0x002857A8 File Offset: 0x00283BA8
		public void AddHPoints(HPoint[] pts, List<HPoint> list, LayerMask layer)
		{
			foreach (HPoint point in pts)
			{
				this.CheckHPointOnTheArea(point, list, layer);
			}
		}

		// Token: 0x06005FBA RID: 24506 RVA: 0x002857D8 File Offset: 0x00283BD8
		private void CheckHPointOnTheArea(HPoint point, List<HPoint> pointList, LayerMask layer)
		{
			Vector3 origin = point.transform.position + Vector3.up * 10f;
			RaycastHit raycastHit;
			if (Physics.Raycast(origin, Vector3.down, out raycastHit, 1000f, layer) && this.ContainsCollider(raycastHit.collider))
			{
				pointList.Add(point);
			}
		}

		// Token: 0x06005FBB RID: 24507 RVA: 0x0028583C File Offset: 0x00283C3C
		public bool ContainsCollider(Collider source)
		{
			foreach (Collider x in this._colliders)
			{
				if (x == source)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06005FBC RID: 24508 RVA: 0x00285878 File Offset: 0x00283C78
		public void Clear()
		{
			this._waypoints.Clear();
			this._actionPoints.Clear();
			this._basePoints.Clear();
			this._devicePoints.Clear();
			this._farmPoints.Clear();
			this._shipPoints.Clear();
			this._animalPoints.Clear();
		}

		// Token: 0x040054DA RID: 21722
		[SerializeField]
		private MapArea.AreaType _type;

		// Token: 0x040054DC RID: 21724
		[SerializeField]
		private int _areaID;

		// Token: 0x040054DD RID: 21725
		[SerializeField]
		private List<Waypoint> _waypoints = new List<Waypoint>();

		// Token: 0x040054DE RID: 21726
		[SerializeField]
		private List<BasePoint> _basePoints = new List<BasePoint>();

		// Token: 0x040054DF RID: 21727
		[SerializeField]
		private List<DevicePoint> _devicePoints = new List<DevicePoint>();

		// Token: 0x040054E0 RID: 21728
		[SerializeField]
		private List<FarmPoint> _farmPoints = new List<FarmPoint>();

		// Token: 0x040054E1 RID: 21729
		[SerializeField]
		private List<ShipPoint> _shipPoints = new List<ShipPoint>();

		// Token: 0x040054E2 RID: 21730
		[SerializeField]
		private List<ActionPoint> _actionPoints = new List<ActionPoint>();

		// Token: 0x040054E3 RID: 21731
		[SerializeField]
		private List<MerchantPoint> _merchantPoints = new List<MerchantPoint>();

		// Token: 0x040054E4 RID: 21732
		[SerializeField]
		private List<EventPoint> _eventPoints = new List<EventPoint>();

		// Token: 0x040054E5 RID: 21733
		[SerializeField]
		private List<StoryPoint> _storyPoints = new List<StoryPoint>();

		// Token: 0x040054E6 RID: 21734
		[SerializeField]
		private List<AnimalPoint> _animalPoints = new List<AnimalPoint>();

		// Token: 0x040054E7 RID: 21735
		[SerializeField]
		private List<ActionPoint> _appendActionPoints = new List<ActionPoint>();

		// Token: 0x040054E8 RID: 21736
		[SerializeField]
		private List<FarmPoint> _appendFarmPoints = new List<FarmPoint>();

		// Token: 0x040054E9 RID: 21737
		private List<PetHomePoint> _appendPetHomePoints = new List<PetHomePoint>();

		// Token: 0x040054EA RID: 21738
		private List<JukePoint> _appendJukePoints = new List<JukePoint>();

		// Token: 0x040054EB RID: 21739
		private List<CraftPoint> _appendCraftPoints = new List<CraftPoint>();

		// Token: 0x040054EC RID: 21740
		private List<LightSwitchPoint> _appendLightSwitchPoints = new List<LightSwitchPoint>();

		// Token: 0x040054ED RID: 21741
		[SerializeField]
		private List<HPoint> _appendHPoints = new List<HPoint>();

		// Token: 0x040054EE RID: 21742
		[SerializeField]
		private Collider[] _colliders;

		// Token: 0x040054EF RID: 21743
		[SerializeField]
		private Collider[] _hColliders;

		// Token: 0x02000C14 RID: 3092
		public enum AreaType
		{
			// Token: 0x040054F1 RID: 21745
			Normal,
			// Token: 0x040054F2 RID: 21746
			Indoor,
			// Token: 0x040054F3 RID: 21747
			Private
		}
	}
}
