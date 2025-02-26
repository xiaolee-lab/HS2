using System;
using System.Collections.Generic;
using Illusion.Extensions;
using Manager;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000C6F RID: 3183
	public class VanishControl : MonoBehaviour
	{
		// Token: 0x170014F2 RID: 5362
		// (get) Token: 0x0600683F RID: 26687 RVA: 0x002C7FDF File Offset: 0x002C63DF
		// (set) Token: 0x06006840 RID: 26688 RVA: 0x002C7FE7 File Offset: 0x002C63E7
		public Vector3 LookAtPosition { get; set; }

		// Token: 0x170014F3 RID: 5363
		// (get) Token: 0x06006841 RID: 26689 RVA: 0x002C7FF0 File Offset: 0x002C63F0
		// (set) Token: 0x06006842 RID: 26690 RVA: 0x002C7FF8 File Offset: 0x002C63F8
		public bool ConfigVanish
		{
			get
			{
				return this._isConfigVanish;
			}
			set
			{
				if (this._isConfigVanish != value)
				{
					this._isConfigVanish = value;
					this.VisibleForceVanish(true);
				}
			}
		}

		// Token: 0x06006843 RID: 26691 RVA: 0x002C8014 File Offset: 0x002C6414
		public void Load()
		{
			this._mapVanishList.Clear();
			if (Singleton<Map>.IsInstance())
			{
				List<Map.VisibleObject> lstMapVanish = Singleton<Map>.Instance.LstMapVanish;
				for (int i = 0; i < lstMapVanish.Count; i++)
				{
					int index = i;
					if (!(lstMapVanish[index].collider == null) && lstMapVanish[index].collider.gameObject.activeSelf)
					{
						VirtualCameraController.VisibleObjectH visibleObjectH = new VirtualCameraController.VisibleObjectH();
						visibleObjectH.nameCollider = lstMapVanish[index].nameCollider;
						visibleObjectH.collider = lstMapVanish[index].collider;
						visibleObjectH.vanishObj = lstMapVanish[index].vanishObj;
						visibleObjectH.initEnable = lstMapVanish[index].collider;
						this._mapVanishList.Add(visibleObjectH);
						visibleObjectH.collider.enabled = true;
					}
				}
			}
		}

		// Token: 0x06006844 RID: 26692 RVA: 0x002C80FC File Offset: 0x002C64FC
		public void LoadHousingVanish(PlayerActor player)
		{
			if (Singleton<Map>.Instance.PointAgent != null)
			{
				BasePoint[] basePoints = Singleton<Map>.Instance.PointAgent.BasePoints;
				if (basePoints != null)
				{
					int mapID = Singleton<Map>.Instance.MapID;
					int num = -1;
					Dictionary<int, Dictionary<int, List<int>>> vanishHousingAreaGroup = Singleton<Manager.Resources>.Instance.Map.VanishHousingAreaGroup;
					if (vanishHousingAreaGroup == null || !vanishHousingAreaGroup.ContainsKey(mapID))
					{
						return;
					}
					foreach (KeyValuePair<int, List<int>> keyValuePair in vanishHousingAreaGroup[mapID])
					{
						if (keyValuePair.Value.Contains(player.AreaID))
						{
							num = keyValuePair.Key;
							break;
						}
					}
					if (num < 0)
					{
						return;
					}
					for (int i = 0; i < basePoints.Length; i++)
					{
						if (!(basePoints[i].OwnerArea == null))
						{
							if (vanishHousingAreaGroup[mapID][num].Contains(basePoints[i].OwnerArea.AreaID))
							{
								if (basePoints[i].ID >= 0)
								{
									Singleton<Housing>.Instance.StartShield(basePoints[i].ID);
									break;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06006845 RID: 26693 RVA: 0x002C8264 File Offset: 0x002C6664
		public void ResetVanish()
		{
			for (int i = 0; i < this._mapVanishList.Count; i++)
			{
				if (!(this._mapVanishList[i].collider == null))
				{
					this._mapVanishList[i].collider.enabled = this._mapVanishList[i].initEnable;
				}
			}
		}

		// Token: 0x06006846 RID: 26694 RVA: 0x002C82D8 File Offset: 0x002C66D8
		public void VisibleForceVanish(bool v)
		{
			foreach (VirtualCameraController.VisibleObjectH visibleObjectH in this._mapVanishList)
			{
				if (visibleObjectH.vanishObj != null)
				{
					visibleObjectH.vanishObj.SetActiveIfDifferent(v);
				}
				visibleObjectH.isVisible = v;
				visibleObjectH.delay = ((!v) ? 0f : 0.3f);
			}
		}

		// Token: 0x06006847 RID: 26695 RVA: 0x002C8370 File Offset: 0x002C6770
		private void VisibleForceVanish(VirtualCameraController.VisibleObjectH obj, bool v)
		{
			if (obj == null)
			{
				return;
			}
			if (obj.vanishObj == null)
			{
				return;
			}
			obj.vanishObj.SetActiveIfDifferent(v);
			obj.delay = ((!v) ? 0f : 0.3f);
			obj.isVisible = v;
		}

		// Token: 0x06006848 RID: 26696 RVA: 0x002C83C8 File Offset: 0x002C67C8
		private bool VanishProc()
		{
			if (!this._isConfigVanish)
			{
				return false;
			}
			int i;
			for (i = 0; i < this._mapVanishList.Count; i++)
			{
				List<Collider> list = this._colliderList.FindAll((Collider x) => this._mapVanishList[i].nameCollider == x.name);
				if (list == null || list.Count == 0)
				{
					this.VanishDelayVisible(this._mapVanishList[i]);
				}
				else if (this._mapVanishList[i].isVisible)
				{
					this.VisibleForceVanish(this._mapVanishList[i], false);
				}
			}
			if (this._viewCollider != null && Singleton<Housing>.IsInstance())
			{
				Singleton<Housing>.Instance.ShieldProc(this._viewCollider);
			}
			return true;
		}

		// Token: 0x06006849 RID: 26697 RVA: 0x002C84C9 File Offset: 0x002C68C9
		private bool VanishDelayVisible(VirtualCameraController.VisibleObjectH obj)
		{
			if (obj.isVisible)
			{
				return false;
			}
			obj.delay += Time.deltaTime;
			if (obj.delay >= 0.3f)
			{
				this.VisibleForceVanish(obj, true);
			}
			return true;
		}

		// Token: 0x0600684A RID: 26698 RVA: 0x002C8504 File Offset: 0x002C6904
		private void Start()
		{
			this._viewCollider = base.gameObject.AddComponent<CapsuleCollider>();
			this._viewCollider.radius = 0.05f;
			this._viewCollider.isTrigger = true;
			this._viewCollider.direction = 2;
			Rigidbody rigidbody = base.gameObject.AddComponent<Rigidbody>();
			rigidbody.useGravity = false;
			rigidbody.isKinematic = true;
		}

		// Token: 0x0600684B RID: 26699 RVA: 0x002C8564 File Offset: 0x002C6964
		private void OnDisable()
		{
			this.VisibleForceVanish(true);
		}

		// Token: 0x0600684C RID: 26700 RVA: 0x002C856D File Offset: 0x002C696D
		private void Update()
		{
			this.ConfigVanish = Config.GraphicData.Shield;
		}

		// Token: 0x0600684D RID: 26701 RVA: 0x002C8580 File Offset: 0x002C6980
		private void LateUpdate()
		{
			if (this._viewCollider != null)
			{
				this._viewCollider.height = Vector3.Distance(base.transform.position, this.LookAtPosition);
				this._viewCollider.center = Vector3.forward * this._viewCollider.height * 0.5f;
			}
			this.VanishProc();
		}

		// Token: 0x0600684E RID: 26702 RVA: 0x002C85F0 File Offset: 0x002C69F0
		private void OnTriggerEnter(Collider other)
		{
			if (this._colliderList.FindAll((Collider x) => x.name == other.name) == null)
			{
				this._colliderList.Add(other);
			}
		}

		// Token: 0x0600684F RID: 26703 RVA: 0x002C863C File Offset: 0x002C6A3C
		private void OnTriggerStay(Collider other)
		{
			Collider x2 = this._colliderList.Find((Collider x) => x.name == other.name);
			if (x2 == null)
			{
				this._colliderList.Add(other);
			}
		}

		// Token: 0x06006850 RID: 26704 RVA: 0x002C868B File Offset: 0x002C6A8B
		private void OnTriggerExit(Collider other)
		{
			this._colliderList.Clear();
		}

		// Token: 0x04005923 RID: 22819
		[SerializeField]
		private CapsuleCollider _viewCollider;

		// Token: 0x04005925 RID: 22821
		protected List<VirtualCameraController.VisibleObjectH> _mapVanishList = new List<VirtualCameraController.VisibleObjectH>();

		// Token: 0x04005926 RID: 22822
		protected List<Collider> _colliderList = new List<Collider>();

		// Token: 0x04005927 RID: 22823
		private bool _isConfigVanish = true;
	}
}
