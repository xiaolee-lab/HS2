using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using AIProject.Animal;
using Manager;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000BF3 RID: 3059
	[Serializable]
	public class Chunk : MonoBehaviour
	{
		// Token: 0x170011DE RID: 4574
		// (get) Token: 0x06005D85 RID: 23941 RVA: 0x002780F6 File Offset: 0x002764F6
		public int ID
		{
			[CompilerGenerated]
			get
			{
				return this._id;
			}
		}

		// Token: 0x170011DF RID: 4575
		// (get) Token: 0x06005D86 RID: 23942 RVA: 0x002780FE File Offset: 0x002764FE
		public Chunk[] ConnectedChunks
		{
			[CompilerGenerated]
			get
			{
				return this._connectedChunks;
			}
		}

		// Token: 0x170011E0 RID: 4576
		// (get) Token: 0x06005D87 RID: 23943 RVA: 0x00278106 File Offset: 0x00276506
		public MapArea[] MapAreas
		{
			[CompilerGenerated]
			get
			{
				return this._mapAreas;
			}
		}

		// Token: 0x170011E1 RID: 4577
		// (get) Token: 0x06005D88 RID: 23944 RVA: 0x0027810E File Offset: 0x0027650E
		// (set) Token: 0x06005D89 RID: 23945 RVA: 0x00278116 File Offset: 0x00276516
		public List<Waypoint> Waypoints { get; private set; } = new List<Waypoint>();

		// Token: 0x170011E2 RID: 4578
		// (get) Token: 0x06005D8A RID: 23946 RVA: 0x0027811F File Offset: 0x0027651F
		// (set) Token: 0x06005D8B RID: 23947 RVA: 0x00278127 File Offset: 0x00276527
		public List<ActionPoint> ActionPoints { get; private set; } = new List<ActionPoint>();

		// Token: 0x170011E3 RID: 4579
		// (get) Token: 0x06005D8C RID: 23948 RVA: 0x00278130 File Offset: 0x00276530
		// (set) Token: 0x06005D8D RID: 23949 RVA: 0x00278138 File Offset: 0x00276538
		public List<BasePoint> BasePoints { get; private set; } = new List<BasePoint>();

		// Token: 0x170011E4 RID: 4580
		// (get) Token: 0x06005D8E RID: 23950 RVA: 0x00278141 File Offset: 0x00276541
		// (set) Token: 0x06005D8F RID: 23951 RVA: 0x00278149 File Offset: 0x00276549
		public List<DevicePoint> DevicePoints { get; private set; } = new List<DevicePoint>();

		// Token: 0x170011E5 RID: 4581
		// (get) Token: 0x06005D90 RID: 23952 RVA: 0x00278152 File Offset: 0x00276552
		// (set) Token: 0x06005D91 RID: 23953 RVA: 0x0027815A File Offset: 0x0027655A
		public List<FarmPoint> FarmPoints { get; private set; } = new List<FarmPoint>();

		// Token: 0x170011E6 RID: 4582
		// (get) Token: 0x06005D92 RID: 23954 RVA: 0x00278163 File Offset: 0x00276563
		// (set) Token: 0x06005D93 RID: 23955 RVA: 0x0027816B File Offset: 0x0027656B
		public List<ShipPoint> ShipPoints { get; private set; } = new List<ShipPoint>();

		// Token: 0x170011E7 RID: 4583
		// (get) Token: 0x06005D94 RID: 23956 RVA: 0x00278174 File Offset: 0x00276574
		// (set) Token: 0x06005D95 RID: 23957 RVA: 0x0027817C File Offset: 0x0027657C
		public List<MerchantPoint> MerchantPoints { get; private set; } = new List<MerchantPoint>();

		// Token: 0x170011E8 RID: 4584
		// (get) Token: 0x06005D96 RID: 23958 RVA: 0x00278185 File Offset: 0x00276585
		// (set) Token: 0x06005D97 RID: 23959 RVA: 0x0027818D File Offset: 0x0027658D
		public List<EventPoint> EventPoints { get; private set; } = new List<EventPoint>();

		// Token: 0x170011E9 RID: 4585
		// (get) Token: 0x06005D98 RID: 23960 RVA: 0x00278196 File Offset: 0x00276596
		// (set) Token: 0x06005D99 RID: 23961 RVA: 0x0027819E File Offset: 0x0027659E
		public List<StoryPoint> StoryPoints { get; private set; } = new List<StoryPoint>();

		// Token: 0x170011EA RID: 4586
		// (get) Token: 0x06005D9A RID: 23962 RVA: 0x002781A7 File Offset: 0x002765A7
		// (set) Token: 0x06005D9B RID: 23963 RVA: 0x002781AF File Offset: 0x002765AF
		public List<AnimalPoint> AnimalPoints { get; private set; } = new List<AnimalPoint>();

		// Token: 0x170011EB RID: 4587
		// (get) Token: 0x06005D9C RID: 23964 RVA: 0x002781B8 File Offset: 0x002765B8
		// (set) Token: 0x06005D9D RID: 23965 RVA: 0x002781C0 File Offset: 0x002765C0
		public List<ActionPoint> AppendActionPoints { get; private set; } = new List<ActionPoint>();

		// Token: 0x170011EC RID: 4588
		// (get) Token: 0x06005D9E RID: 23966 RVA: 0x002781C9 File Offset: 0x002765C9
		// (set) Token: 0x06005D9F RID: 23967 RVA: 0x002781D1 File Offset: 0x002765D1
		public List<FarmPoint> AppendFarmPoints { get; private set; } = new List<FarmPoint>();

		// Token: 0x170011ED RID: 4589
		// (get) Token: 0x06005DA0 RID: 23968 RVA: 0x002781DA File Offset: 0x002765DA
		// (set) Token: 0x06005DA1 RID: 23969 RVA: 0x002781E2 File Offset: 0x002765E2
		public List<PetHomePoint> AppendPetHomePoints { get; private set; } = new List<PetHomePoint>();

		// Token: 0x170011EE RID: 4590
		// (get) Token: 0x06005DA2 RID: 23970 RVA: 0x002781EB File Offset: 0x002765EB
		// (set) Token: 0x06005DA3 RID: 23971 RVA: 0x002781F3 File Offset: 0x002765F3
		public List<JukePoint> AppendJukePoints { get; private set; } = new List<JukePoint>();

		// Token: 0x170011EF RID: 4591
		// (get) Token: 0x06005DA4 RID: 23972 RVA: 0x002781FC File Offset: 0x002765FC
		// (set) Token: 0x06005DA5 RID: 23973 RVA: 0x00278204 File Offset: 0x00276604
		public List<CraftPoint> AppendCraftPoints { get; private set; } = new List<CraftPoint>();

		// Token: 0x170011F0 RID: 4592
		// (get) Token: 0x06005DA6 RID: 23974 RVA: 0x0027820D File Offset: 0x0027660D
		// (set) Token: 0x06005DA7 RID: 23975 RVA: 0x00278215 File Offset: 0x00276615
		public List<LightSwitchPoint> AppendLightSwitchPoints { get; private set; } = new List<LightSwitchPoint>();

		// Token: 0x170011F1 RID: 4593
		// (get) Token: 0x06005DA8 RID: 23976 RVA: 0x0027821E File Offset: 0x0027661E
		// (set) Token: 0x06005DA9 RID: 23977 RVA: 0x00278226 File Offset: 0x00276626
		public List<HPoint> AppendHPoints { get; private set; } = new List<HPoint>();

		// Token: 0x06005DAA RID: 23978 RVA: 0x00278230 File Offset: 0x00276630
		private void Awake()
		{
			if (Singleton<Map>.IsInstance())
			{
				Singleton<Map>.Instance.ChunkTable[this._id] = this;
			}
			this._mapAreas = base.GetComponentsInChildren<MapArea>(true);
			foreach (MapArea mapArea in this._mapAreas)
			{
				mapArea.ChunkID = this._id;
			}
		}

		// Token: 0x06005DAB RID: 23979 RVA: 0x00278298 File Offset: 0x00276698
		public IEnumerator Load(Waypoint[] wp, BasePoint[] bp, DevicePoint[] dp, FarmPoint[] fp, ShipPoint[] shipPt, ActionPoint[] ap, MerchantPoint[] mp, EventPoint[] ep, StoryPoint[] sp, AnimalPoint[] pap)
		{
			List<IConnectableObservable<Unit>> streams = ListPool<IConnectableObservable<Unit>>.Get();
			foreach (MapArea mapArea in this._mapAreas)
			{
				foreach (Waypoint waypoint in mapArea.Waypoints)
				{
					waypoint.OwnerArea = null;
				}
				mapArea.Waypoints.Clear();
				foreach (BasePoint basePoint in mapArea.BasePoints)
				{
					basePoint.OwnerArea = null;
				}
				mapArea.BasePoints.Clear();
				foreach (DevicePoint devicePoint in mapArea.DevicePoints)
				{
					devicePoint.OwnerArea = null;
				}
				mapArea.DevicePoints.Clear();
				foreach (FarmPoint farmPoint in mapArea.FarmPoints)
				{
					farmPoint.OwnerArea = null;
				}
				mapArea.FarmPoints.Clear();
				foreach (ShipPoint shipPoint in mapArea.ShipPoints)
				{
					shipPoint.OwnerArea = null;
				}
				mapArea.ShipPoints.Clear();
				foreach (ActionPoint actionPoint in mapArea.ActionPoints)
				{
					actionPoint.OwnerArea = null;
				}
				mapArea.ActionPoints.Clear();
				foreach (MerchantPoint merchantPoint in mapArea.MerchantPoints)
				{
					merchantPoint.OwnerArea = null;
				}
				mapArea.MerchantPoints.Clear();
				foreach (EventPoint eventPoint in mapArea.EventPoints)
				{
					eventPoint.OwnerArea = null;
				}
				mapArea.EventPoints.Clear();
				foreach (StoryPoint storyPoint in mapArea.StoryPoints)
				{
					storyPoint.OwnerArea = null;
				}
				mapArea.StoryPoints.Clear();
				foreach (AnimalPoint animalPoint in mapArea.AnimalPoints)
				{
					animalPoint.OwnerArea = null;
				}
				mapArea.AnimalPoints.Clear();
			}
			MapArea[] mapAreas2 = this._mapAreas;
			for (int j = 0; j < mapAreas2.Length; j++)
			{
				MapArea area = mapAreas2[j];
				IConnectableObservable<Unit> connectableObservable = Observable.FromCoroutine(() => area.Load(wp, bp, dp, fp, shipPt, ap, mp, ep, sp, pap), false).Publish<Unit>();
				connectableObservable.Connect();
				streams.Add(connectableObservable);
			}
			yield return streams.WhenAll().ToYieldInstruction<Unit>();
			ListPool<IConnectableObservable<Unit>>.Release(streams);
			foreach (MapArea mapArea2 in this._mapAreas)
			{
				foreach (Waypoint item in mapArea2.Waypoints)
				{
					this.Waypoints.Add(item);
				}
				foreach (BasePoint item2 in mapArea2.BasePoints)
				{
					this.BasePoints.Add(item2);
				}
				foreach (DevicePoint item3 in mapArea2.DevicePoints)
				{
					this.DevicePoints.Add(item3);
				}
				foreach (FarmPoint item4 in mapArea2.FarmPoints)
				{
					this.FarmPoints.Add(item4);
				}
				foreach (ShipPoint item5 in mapArea2.ShipPoints)
				{
					this.ShipPoints.Add(item5);
				}
				foreach (ActionPoint item6 in mapArea2.ActionPoints)
				{
					this.ActionPoints.Add(item6);
				}
				foreach (MerchantPoint item7 in mapArea2.MerchantPoints)
				{
					this.MerchantPoints.Add(item7);
				}
				foreach (EventPoint item8 in mapArea2.EventPoints)
				{
					this.EventPoints.Add(item8);
				}
				foreach (StoryPoint item9 in mapArea2.StoryPoints)
				{
					this.StoryPoints.Add(item9);
				}
				foreach (AnimalPoint item10 in mapArea2.AnimalPoints)
				{
					this.AnimalPoints.Add(item10);
				}
			}
			int id = 0;
			foreach (Waypoint waypoint2 in this.Waypoints)
			{
				Waypoint waypoint3 = waypoint2;
				int id2;
				id = (id2 = id) + 1;
				waypoint3.ID = id2;
			}
			yield break;
		}

		// Token: 0x06005DAC RID: 23980 RVA: 0x00278300 File Offset: 0x00276700
		public void LoadAppendActionPoints(ActionPoint[] pts)
		{
			LayerMask areaDetectionLayer = Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.AreaDetectionLayer;
			LayerMask roofLayer = Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.RoofLayer;
			foreach (MapArea mapArea in this._mapAreas)
			{
				foreach (ActionPoint actionPoint in mapArea.AppendActionPoints)
				{
					actionPoint.OwnerArea = null;
				}
				mapArea.AppendActionPoints.Clear();
			}
			foreach (MapArea mapArea2 in this._mapAreas)
			{
				mapArea2.AddPoints<ActionPoint>(pts, mapArea2.AppendActionPoints, areaDetectionLayer, roofLayer);
			}
			this.AppendActionPoints.Clear();
			foreach (MapArea mapArea3 in this._mapAreas)
			{
				foreach (ActionPoint item in mapArea3.AppendActionPoints)
				{
					this.AppendActionPoints.Add(item);
				}
			}
		}

		// Token: 0x06005DAD RID: 23981 RVA: 0x00278478 File Offset: 0x00276878
		public void LoadAppendFarmPoints(FarmPoint[] pts)
		{
			LayerMask areaDetectionLayer = Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.AreaDetectionLayer;
			LayerMask roofLayer = Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.RoofLayer;
			foreach (MapArea mapArea in this._mapAreas)
			{
				foreach (FarmPoint farmPoint in mapArea.AppendFarmPoints)
				{
					farmPoint.OwnerArea = null;
				}
				mapArea.AppendFarmPoints.Clear();
			}
			foreach (MapArea mapArea2 in this._mapAreas)
			{
				mapArea2.AddPoints<FarmPoint>(pts, mapArea2.AppendFarmPoints, areaDetectionLayer, roofLayer);
			}
			this.AppendFarmPoints.Clear();
			foreach (MapArea mapArea3 in this._mapAreas)
			{
				foreach (FarmPoint item in mapArea3.AppendFarmPoints)
				{
					this.AppendFarmPoints.Add(item);
				}
			}
		}

		// Token: 0x06005DAE RID: 23982 RVA: 0x002785F0 File Offset: 0x002769F0
		public void LoadAppendPetHomePoints(PetHomePoint[] pts)
		{
			LayerMask areaDetectionLayer = Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.AreaDetectionLayer;
			LayerMask roofLayer = Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.RoofLayer;
			foreach (MapArea mapArea in this._mapAreas)
			{
				foreach (PetHomePoint petHomePoint in mapArea.AppendPetHomePoints)
				{
					petHomePoint.OwnerArea = null;
				}
				mapArea.AppendPetHomePoints.Clear();
			}
			foreach (MapArea mapArea2 in this._mapAreas)
			{
				mapArea2.AddPoints<PetHomePoint>(pts, mapArea2.AppendPetHomePoints, areaDetectionLayer, roofLayer);
			}
			this.AppendPetHomePoints.Clear();
			foreach (MapArea mapArea3 in this._mapAreas)
			{
				foreach (PetHomePoint item in mapArea3.AppendPetHomePoints)
				{
					this.AppendPetHomePoints.Add(item);
				}
			}
		}

		// Token: 0x06005DAF RID: 23983 RVA: 0x00278768 File Offset: 0x00276B68
		public void LoadAppendJukePoints(JukePoint[] pts)
		{
			LayerMask areaDetectionLayer = Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.AreaDetectionLayer;
			LayerMask roofLayer = Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.RoofLayer;
			foreach (MapArea mapArea in this._mapAreas)
			{
				foreach (JukePoint jukePoint in mapArea.AppendJukePoints)
				{
					jukePoint.OwnerArea = null;
				}
				mapArea.AppendJukePoints.Clear();
			}
			foreach (MapArea mapArea2 in this._mapAreas)
			{
				mapArea2.AddPoints<JukePoint>(pts, mapArea2.AppendJukePoints, areaDetectionLayer, roofLayer);
			}
			this.AppendJukePoints.Clear();
			foreach (MapArea mapArea3 in this._mapAreas)
			{
				foreach (JukePoint item in mapArea3.AppendJukePoints)
				{
					this.AppendJukePoints.Add(item);
				}
			}
		}

		// Token: 0x06005DB0 RID: 23984 RVA: 0x002788E0 File Offset: 0x00276CE0
		public void LoadAppendCraftPoints(CraftPoint[] pts)
		{
			LayerMask areaDetectionLayer = Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.AreaDetectionLayer;
			LayerMask roofLayer = Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.RoofLayer;
			foreach (MapArea mapArea in this._mapAreas)
			{
				foreach (CraftPoint craftPoint in mapArea.AppendCraftPoints)
				{
					craftPoint.OwnerArea = null;
				}
				mapArea.AppendCraftPoints.Clear();
			}
			foreach (MapArea mapArea2 in this._mapAreas)
			{
				mapArea2.AddPoints<CraftPoint>(pts, mapArea2.AppendCraftPoints, areaDetectionLayer, roofLayer);
			}
			this.AppendCraftPoints.Clear();
			foreach (MapArea mapArea3 in this._mapAreas)
			{
				foreach (CraftPoint item in mapArea3.AppendCraftPoints)
				{
					this.AppendCraftPoints.Add(item);
				}
			}
		}

		// Token: 0x06005DB1 RID: 23985 RVA: 0x00278A58 File Offset: 0x00276E58
		public void LoadAppendLightSwitchPoints(LightSwitchPoint[] pts)
		{
			LayerMask areaDetectionLayer = Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.AreaDetectionLayer;
			LayerMask roofLayer = Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.RoofLayer;
			foreach (MapArea mapArea in this._mapAreas)
			{
				foreach (LightSwitchPoint lightSwitchPoint in mapArea.AppendLightSwitchPoints)
				{
					lightSwitchPoint.OwnerArea = null;
				}
				mapArea.AppendLightSwitchPoints.Clear();
			}
			if (!pts.IsNullOrEmpty<LightSwitchPoint>())
			{
				foreach (MapArea mapArea2 in this._mapAreas)
				{
					mapArea2.AddPoints<LightSwitchPoint>(pts, mapArea2.AppendLightSwitchPoints, areaDetectionLayer, roofLayer);
				}
			}
			this.AppendLightSwitchPoints.Clear();
			foreach (MapArea mapArea3 in this._mapAreas)
			{
				foreach (LightSwitchPoint item in mapArea3.AppendLightSwitchPoints)
				{
					this.AppendLightSwitchPoints.Add(item);
				}
			}
		}

		// Token: 0x06005DB2 RID: 23986 RVA: 0x00278BDC File Offset: 0x00276FDC
		public bool CheckPointOnTheArea<T>(T point, LayerMask layer, LayerMask roofLayer, float offsetY = 10f) where T : Point
		{
			if (this._mapAreas.IsNullOrEmpty<MapArea>())
			{
				return false;
			}
			foreach (MapArea mapArea in this._mapAreas)
			{
				if (mapArea.CheckPointOnTheArea<T>(point, layer, roofLayer, offsetY))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06005DB3 RID: 23987 RVA: 0x00278C30 File Offset: 0x00277030
		public void LoadAppendHPoints(HPoint[] pts)
		{
			LayerMask areaDetectionLayer = Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.AreaDetectionLayer;
			foreach (MapArea mapArea in this._mapAreas)
			{
				mapArea.AppendHPoints.Clear();
			}
			if (!pts.IsNullOrEmpty<HPoint>())
			{
				foreach (MapArea mapArea2 in this._mapAreas)
				{
					mapArea2.AddHPoints(pts, mapArea2.AppendHPoints, areaDetectionLayer);
				}
			}
			this.AppendHPoints.Clear();
			foreach (MapArea mapArea3 in this._mapAreas)
			{
				foreach (HPoint item in mapArea3.AppendHPoints)
				{
					this.AppendHPoints.Add(item);
				}
			}
		}

		// Token: 0x040053B7 RID: 21431
		[SerializeField]
		private int _id;

		// Token: 0x040053B8 RID: 21432
		[SerializeField]
		private Chunk[] _connectedChunks;

		// Token: 0x040053B9 RID: 21433
		[Space(8f)]
		[SerializeField]
		[HideInEditorMode]
		[DisableInPlayMode]
		private MapArea[] _mapAreas;
	}
}
