using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AIProject;
using AIProject.Animal;
using AIProject.Animal.Resources;
using AIProject.SaveData;
using AIProject.Scene;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEx;

namespace Manager
{
	// Token: 0x020008DB RID: 2267
	public sealed class AnimalManager : Singleton<AnimalManager>
	{
		// Token: 0x06003B9E RID: 15262 RVA: 0x0015D7E5 File Offset: 0x0015BBE5
		public void StartAllAnimalCreate()
		{
			this.HabitatPointsFirstSetting();
			this.SettingAnimalPointBehavior();
		}

		// Token: 0x06003B9F RID: 15263 RVA: 0x0015D7F3 File Offset: 0x0015BBF3
		public void SettingAnimalPointBehavior()
		{
			this.ClearAnimalPointBehavior();
			this.ActivateHabitatPoints();
		}

		// Token: 0x06003BA0 RID: 15264 RVA: 0x0015D801 File Offset: 0x0015BC01
		private void HabitatPointsFirstSetting()
		{
			if (!Singleton<Resources>.IsInstance())
			{
				return;
			}
			this.WildCatHabitatPointSetting();
			this.WildChickenHabitatPointSetting();
			this.WildCatAndChickenHabitatPointSetting();
			this.WildMechaHabitatPointSetting();
			this.WildFrogHabitatPointSetting();
			this.WildBirdFlockHabitatPointSetting();
			this.WildButterflyHabitatPointSetting();
		}

		// Token: 0x06003BA1 RID: 15265 RVA: 0x0015D838 File Offset: 0x0015BC38
		private void WildCatHabitatPointSetting()
		{
			AnimalManager.<WildCatHabitatPointSetting>c__AnonStorey2 <WildCatHabitatPointSetting>c__AnonStorey = new AnimalManager.<WildCatHabitatPointSetting>c__AnonStorey2();
			<WildCatHabitatPointSetting>c__AnonStorey.$this = this;
			if (this.WildCatPopPoints.IsNullOrEmpty<GroundAnimalHabitatPoint>())
			{
				return;
			}
			WorldData worldData = (!Singleton<Game>.IsInstance()) ? null : Singleton<Game>.Instance.WorldData;
			Dictionary<AnimalTypes, Dictionary<int, WildAnimalData>> dictionary = (worldData == null) ? null : worldData.WildAnimalTable;
			Dictionary<int, WildAnimalData> dictionary2 = null;
			if (dictionary != null)
			{
				dictionary.TryGetValue(AnimalTypes.Cat, out dictionary2);
			}
			AnimalDefinePack animalDefinePack = Singleton<Resources>.Instance.AnimalDefinePack;
			<WildCatHabitatPointSetting>c__AnonStorey._systemInfo = animalDefinePack.SystemInfo;
			Vector2 wildCatCreateCoolTime = <WildCatHabitatPointSetting>c__AnonStorey._systemInfo.WildCatCreateCoolTime;
			Vector2 popPointCoolTimeRange = <WildCatHabitatPointSetting>c__AnonStorey._systemInfo.PopPointCoolTimeRange;
			foreach (GroundAnimalHabitatPoint groundAnimalHabitatPoint in this.WildCatPopPoints)
			{
				if (!(groundAnimalHabitatPoint == null))
				{
					WildAnimalData wildAnimalData = null;
					if (dictionary2 != null)
					{
						dictionary2.TryGetValue(groundAnimalHabitatPoint.ID, out wildAnimalData);
					}
					groundAnimalHabitatPoint.SetCoolTime((wildAnimalData == null) ? 0f : wildAnimalData.CoolTime);
					groundAnimalHabitatPoint.CoolTime = popPointCoolTimeRange;
					groundAnimalHabitatPoint.IsActive = !this.IsRain();
					groundAnimalHabitatPoint.IsCountStop = true;
					groundAnimalHabitatPoint.AddCheck = ((Vector3 pos) => !<WildCatHabitatPointSetting>c__AnonStorey.$this.IsRain() && <WildCatHabitatPointSetting>c__AnonStorey.$this.WildCats.Count < <WildCatHabitatPointSetting>c__AnonStorey._systemInfo.WildCatMaxNum);
					groundAnimalHabitatPoint.AddAnimalAction = delegate(GroundAnimalHabitatPoint _basePoint)
					{
						if (_basePoint == null)
						{
							return null;
						}
						WildGround _cat = <WildCatHabitatPointSetting>c__AnonStorey.$this.Create<WildGround>(0, 0);
						if (_cat == null)
						{
							return null;
						}
						_cat.OnDestroyAsObservable().Subscribe(delegate(Unit _)
						{
							<WildCatHabitatPointSetting>c__AnonStorey.RemoveAnimal(_cat, false);
						});
						_cat.Initialize(_basePoint);
						_cat.Refresh();
						return _cat;
					};
				}
			}
		}

		// Token: 0x06003BA2 RID: 15266 RVA: 0x0015D9B4 File Offset: 0x0015BDB4
		private void WildChickenHabitatPointSetting()
		{
			AnimalManager.<WildChickenHabitatPointSetting>c__AnonStorey4 <WildChickenHabitatPointSetting>c__AnonStorey = new AnimalManager.<WildChickenHabitatPointSetting>c__AnonStorey4();
			<WildChickenHabitatPointSetting>c__AnonStorey.$this = this;
			if (this.WildChickenPopPoints.IsNullOrEmpty<GroundAnimalHabitatPoint>())
			{
				return;
			}
			WorldData worldData = (!Singleton<Game>.IsInstance()) ? null : Singleton<Game>.Instance.WorldData;
			Dictionary<AnimalTypes, Dictionary<int, WildAnimalData>> dictionary = (worldData == null) ? null : worldData.WildAnimalTable;
			Dictionary<int, WildAnimalData> dictionary2 = null;
			if (dictionary != null)
			{
				dictionary.TryGetValue(AnimalTypes.Chicken, out dictionary2);
			}
			AnimalDefinePack animalDefinePack = Singleton<Resources>.Instance.AnimalDefinePack;
			<WildChickenHabitatPointSetting>c__AnonStorey._systemInfo = animalDefinePack.SystemInfo;
			Vector2 wildChickenCreateCoolTime = <WildChickenHabitatPointSetting>c__AnonStorey._systemInfo.WildChickenCreateCoolTime;
			Vector2 popPointCoolTimeRange = <WildChickenHabitatPointSetting>c__AnonStorey._systemInfo.PopPointCoolTimeRange;
			foreach (GroundAnimalHabitatPoint groundAnimalHabitatPoint in this.WildChickenPopPoints)
			{
				if (!(groundAnimalHabitatPoint == null))
				{
					WildAnimalData wildAnimalData = null;
					if (dictionary2 != null)
					{
						dictionary2.TryGetValue(groundAnimalHabitatPoint.ID, out wildAnimalData);
					}
					groundAnimalHabitatPoint.SetCoolTime((wildAnimalData == null) ? 0f : wildAnimalData.CoolTime);
					groundAnimalHabitatPoint.CoolTime = popPointCoolTimeRange;
					groundAnimalHabitatPoint.IsActive = !this.IsRain();
					groundAnimalHabitatPoint.IsCountStop = true;
					groundAnimalHabitatPoint.AddCheck = ((Vector3 pos) => !<WildChickenHabitatPointSetting>c__AnonStorey.$this.IsRain() && <WildChickenHabitatPointSetting>c__AnonStorey.$this.WildChickens.Count < <WildChickenHabitatPointSetting>c__AnonStorey._systemInfo.WildChickenMaxNum);
					groundAnimalHabitatPoint.AddAnimalAction = delegate(GroundAnimalHabitatPoint _basePoint)
					{
						if (_basePoint == null)
						{
							return null;
						}
						WildGround _chicken = <WildChickenHabitatPointSetting>c__AnonStorey.$this.Create<WildGround>(1, 0);
						if (_chicken == null)
						{
							return null;
						}
						_chicken.OnDestroyAsObservable().Subscribe(delegate(Unit _)
						{
							<WildChickenHabitatPointSetting>c__AnonStorey.RemoveAnimal(_chicken, false);
						});
						_chicken.Initialize(_basePoint);
						_chicken.Refresh();
						return _chicken;
					};
				}
			}
		}

		// Token: 0x06003BA3 RID: 15267 RVA: 0x0015DB30 File Offset: 0x0015BF30
		private void WildCatAndChickenHabitatPointSetting()
		{
			AnimalManager.<WildCatAndChickenHabitatPointSetting>c__AnonStorey6 <WildCatAndChickenHabitatPointSetting>c__AnonStorey = new AnimalManager.<WildCatAndChickenHabitatPointSetting>c__AnonStorey6();
			<WildCatAndChickenHabitatPointSetting>c__AnonStorey.$this = this;
			if (this.WildCatAndChickenPopPoints.IsNullOrEmpty<GroundAnimalHabitatPoint>())
			{
				return;
			}
			WorldData worldData = (!Singleton<Game>.IsInstance()) ? null : Singleton<Game>.Instance.WorldData;
			Dictionary<AnimalTypes, Dictionary<int, WildAnimalData>> dictionary = (worldData == null) ? null : worldData.WildAnimalTable;
			Dictionary<int, WildAnimalData> dictionary2 = null;
			if (dictionary != null)
			{
				dictionary.TryGetValue(AnimalTypes.Cat | AnimalTypes.Chicken, out dictionary2);
			}
			AnimalDefinePack animalDefinePack = Singleton<Resources>.Instance.AnimalDefinePack;
			<WildCatAndChickenHabitatPointSetting>c__AnonStorey._systemInfo = animalDefinePack.SystemInfo;
			Vector2 wildCatCreateCoolTime = <WildCatAndChickenHabitatPointSetting>c__AnonStorey._systemInfo.WildCatCreateCoolTime;
			Vector2 wildChickenCreateCoolTime = <WildCatAndChickenHabitatPointSetting>c__AnonStorey._systemInfo.WildChickenCreateCoolTime;
			Vector2 popPointCoolTimeRange = <WildCatAndChickenHabitatPointSetting>c__AnonStorey._systemInfo.PopPointCoolTimeRange;
			foreach (GroundAnimalHabitatPoint groundAnimalHabitatPoint in this.WildCatAndChickenPopPoints)
			{
				if (!(groundAnimalHabitatPoint == null))
				{
					WildAnimalData wildAnimalData = null;
					if (dictionary2 != null)
					{
						dictionary2.TryGetValue(groundAnimalHabitatPoint.ID, out wildAnimalData);
					}
					groundAnimalHabitatPoint.SetCoolTime((wildAnimalData == null) ? 0f : wildAnimalData.CoolTime);
					groundAnimalHabitatPoint.CoolTime = popPointCoolTimeRange;
					groundAnimalHabitatPoint.IsActive = !this.IsRain();
					groundAnimalHabitatPoint.IsCountStop = true;
					groundAnimalHabitatPoint.AddCheck = ((Vector3 pos) => !<WildCatAndChickenHabitatPointSetting>c__AnonStorey.$this.IsRain() && <WildCatAndChickenHabitatPointSetting>c__AnonStorey.$this.WildChickens.Count < <WildCatAndChickenHabitatPointSetting>c__AnonStorey._systemInfo.WildChickenMaxNum && <WildCatAndChickenHabitatPointSetting>c__AnonStorey.$this.WildCats.Count < <WildCatAndChickenHabitatPointSetting>c__AnonStorey._systemInfo.WildCatMaxNum);
					groundAnimalHabitatPoint.AddAnimalAction = delegate(GroundAnimalHabitatPoint _basePoint)
					{
						if (_basePoint == null)
						{
							return null;
						}
						WildGround _animal = <WildCatAndChickenHabitatPointSetting>c__AnonStorey.$this.Create<WildGround>((UnityEngine.Random.Range(0, 100) >= 50) ? 1 : 0, 0);
						if (_animal == null)
						{
							return null;
						}
						_animal.OnDestroyAsObservable().Subscribe(delegate(Unit _)
						{
							<WildCatAndChickenHabitatPointSetting>c__AnonStorey.RemoveAnimal(_animal, false);
						});
						_animal.Initialize(_basePoint);
						_animal.Refresh();
						return _animal;
					};
				}
			}
		}

		// Token: 0x06003BA4 RID: 15268 RVA: 0x0015DCB8 File Offset: 0x0015C0B8
		private void WildMechaHabitatPointSetting()
		{
			AnimalManager.<WildMechaHabitatPointSetting>c__AnonStorey8 <WildMechaHabitatPointSetting>c__AnonStorey = new AnimalManager.<WildMechaHabitatPointSetting>c__AnonStorey8();
			<WildMechaHabitatPointSetting>c__AnonStorey.$this = this;
			if (this.MechaHabitatPoints.IsNullOrEmpty<MechaHabitatPoint>())
			{
				return;
			}
			WorldData worldData = (!Singleton<Game>.IsInstance()) ? null : Singleton<Game>.Instance.WorldData;
			Dictionary<AnimalTypes, Dictionary<int, WildAnimalData>> dictionary = (worldData == null) ? null : worldData.WildAnimalTable;
			Dictionary<int, WildAnimalData> dictionary2 = null;
			if (dictionary != null)
			{
				dictionary.TryGetValue(AnimalTypes.Mecha, out dictionary2);
			}
			AnimalDefinePack animalDefinePack = Singleton<Resources>.Instance.AnimalDefinePack;
			<WildMechaHabitatPointSetting>c__AnonStorey._systemInfo = animalDefinePack.SystemInfo;
			foreach (MechaHabitatPoint mechaHabitatPoint in this.MechaHabitatPoints)
			{
				if (!(mechaHabitatPoint == null))
				{
					WildAnimalData wildAnimalData = null;
					if (dictionary2 != null)
					{
						dictionary2.TryGetValue(mechaHabitatPoint.ID, out wildAnimalData);
					}
					mechaHabitatPoint.SetCoolTime((wildAnimalData == null) ? 0f : wildAnimalData.CoolTime);
					mechaHabitatPoint.ForcedAdd = (wildAnimalData == null || wildAnimalData.IsAdded);
					mechaHabitatPoint.IsActive = true;
					mechaHabitatPoint.IsCountStop = true;
					mechaHabitatPoint.AddCheck = ((MechaHabitatPoint _basePoint) => <WildMechaHabitatPointSetting>c__AnonStorey.$this.WildMechas.Count < <WildMechaHabitatPointSetting>c__AnonStorey._systemInfo.WildMechaMaxNum && (<WildMechaHabitatPointSetting>c__AnonStorey.$this.CheckAvailablePoint(_basePoint.Position, true, true) || _basePoint.ForcedAdd));
					mechaHabitatPoint.AddAnimalAction = delegate(MechaHabitatPoint _basePoint)
					{
						if (_basePoint == null)
						{
							return null;
						}
						_basePoint.ForcedAdd = false;
						WildMecha _mecha = <WildMechaHabitatPointSetting>c__AnonStorey.$this.Create<WildMecha>(4, 0);
						if (_mecha == null)
						{
							return null;
						}
						_mecha.OnDestroyAsObservable().Subscribe(delegate(Unit _)
						{
							<WildMechaHabitatPointSetting>c__AnonStorey.RemoveAnimal(_mecha, false);
						});
						_mecha.Initialize(_basePoint);
						_mecha.Refresh();
						return _mecha;
					};
				}
			}
		}

		// Token: 0x06003BA5 RID: 15269 RVA: 0x0015DE24 File Offset: 0x0015C224
		private void WildFrogHabitatPointSetting()
		{
			AnimalManager.<WildFrogHabitatPointSetting>c__AnonStoreyA <WildFrogHabitatPointSetting>c__AnonStoreyA = new AnimalManager.<WildFrogHabitatPointSetting>c__AnonStoreyA();
			<WildFrogHabitatPointSetting>c__AnonStoreyA.$this = this;
			if (this.FrogHabitatPoints.IsNullOrEmpty<FrogHabitatPoint>())
			{
				return;
			}
			WorldData worldData = (!Singleton<Game>.IsInstance()) ? null : Singleton<Game>.Instance.WorldData;
			Dictionary<AnimalTypes, Dictionary<int, WildAnimalData>> dictionary = (worldData == null) ? null : worldData.WildAnimalTable;
			Dictionary<int, WildAnimalData> dictionary2 = null;
			if (dictionary != null)
			{
				dictionary.TryGetValue(AnimalTypes.Frog, out dictionary2);
			}
			AnimalDefinePack animalDefinePack = Singleton<Resources>.Instance.AnimalDefinePack;
			<WildFrogHabitatPointSetting>c__AnonStoreyA._systemInfo = animalDefinePack.SystemInfo;
			foreach (FrogHabitatPoint frogHabitatPoint in this.FrogHabitatPoints)
			{
				if (!(frogHabitatPoint == null))
				{
					frogHabitatPoint.CoolTimeRange = <WildFrogHabitatPointSetting>c__AnonStoreyA._systemInfo.FrogCoolTimeRange;
					WildAnimalData wildAnimalData = null;
					if (dictionary2 != null)
					{
						dictionary2.TryGetValue(frogHabitatPoint.ID, out wildAnimalData);
					}
					frogHabitatPoint.SetCoolTime((wildAnimalData == null) ? 0f : wildAnimalData.CoolTime);
					frogHabitatPoint.ForcedAdd = (wildAnimalData == null || wildAnimalData.IsAdded);
					frogHabitatPoint.IsActive = true;
					frogHabitatPoint.IsCountStop = true;
					frogHabitatPoint.AddCheck = ((FrogHabitatPoint _basePoint) => <WildFrogHabitatPointSetting>c__AnonStoreyA.$this.WildFrogs.Count < <WildFrogHabitatPointSetting>c__AnonStoreyA._systemInfo.WildFrogMaxNum && (<WildFrogHabitatPointSetting>c__AnonStoreyA.$this.CheckAvailablePoint(_basePoint.Position, true, true) || _basePoint.ForcedAdd));
					frogHabitatPoint.AddAnimalAction = delegate(FrogHabitatPoint _basePoint)
					{
						if (_basePoint == null)
						{
							return null;
						}
						_basePoint.ForcedAdd = false;
						WildFrog _frog = <WildFrogHabitatPointSetting>c__AnonStoreyA.$this.Create<WildFrog>(5, 0);
						if (_frog == null)
						{
							return null;
						}
						_frog.OnDestroyAsObservable().Subscribe(delegate(Unit _)
						{
							<WildFrogHabitatPointSetting>c__AnonStoreyA.RemoveAnimal(_frog, false);
						});
						_frog.Initialize(_basePoint);
						_frog.Refresh();
						return _frog;
					};
				}
			}
		}

		// Token: 0x06003BA6 RID: 15270 RVA: 0x0015DFA0 File Offset: 0x0015C3A0
		private void WildBirdFlockHabitatPointSetting()
		{
			AnimalManager.<WildBirdFlockHabitatPointSetting>c__AnonStoreyC <WildBirdFlockHabitatPointSetting>c__AnonStoreyC = new AnimalManager.<WildBirdFlockHabitatPointSetting>c__AnonStoreyC();
			<WildBirdFlockHabitatPointSetting>c__AnonStoreyC.$this = this;
			if (this.BirdFlockHabitatPoints.IsNullOrEmpty<BirdFlockHabitatPoint>())
			{
				return;
			}
			AnimalDefinePack animalDefinePack = Singleton<Resources>.Instance.AnimalDefinePack;
			<WildBirdFlockHabitatPointSetting>c__AnonStoreyC._systemInfo = animalDefinePack.SystemInfo;
			foreach (BirdFlockHabitatPoint birdFlockHabitatPoint in this.BirdFlockHabitatPoints)
			{
				if (!(birdFlockHabitatPoint == null))
				{
					birdFlockHabitatPoint.SetCoolTime();
					birdFlockHabitatPoint.IsActive = (!this.IsRain() && !this.IsNight());
					birdFlockHabitatPoint.IsCountStop = true;
					birdFlockHabitatPoint.AddCheck = ((Vector3 pos) => <WildBirdFlockHabitatPointSetting>c__AnonStoreyC.$this.WildBirdFlocks.Count < <WildBirdFlockHabitatPointSetting>c__AnonStoreyC._systemInfo.WildBirdFlockMaxNum && !<WildBirdFlockHabitatPointSetting>c__AnonStoreyC.$this.IsRain() && !<WildBirdFlockHabitatPointSetting>c__AnonStoreyC.$this.IsNight());
					birdFlockHabitatPoint.AddAnimalAction = delegate(BirdFlockHabitatPoint _basePoint)
					{
						if (_basePoint == null)
						{
							return null;
						}
						WildBirdFlock _bird = <WildBirdFlockHabitatPointSetting>c__AnonStoreyC.$this.Create<WildBirdFlock>(6, 0);
						if (_bird == null)
						{
							return null;
						}
						_bird.OnDestroyAsObservable().Subscribe(delegate(Unit _)
						{
							<WildBirdFlockHabitatPointSetting>c__AnonStoreyC.RemoveAnimal(_bird, false);
						});
						_bird.Initialize(_basePoint);
						return _bird;
					};
				}
			}
		}

		// Token: 0x06003BA7 RID: 15271 RVA: 0x0015E08C File Offset: 0x0015C48C
		private void WildButterflyHabitatPointSetting()
		{
			if (this.ButterflyHabitatPoints.IsNullOrEmpty<ButterflyHabitatPoint>())
			{
				return;
			}
			AnimalDefinePack animalDefinePack = Singleton<Resources>.Instance.AnimalDefinePack;
			AnimalDefinePack.SystemInfoGroup systemInfo = animalDefinePack.SystemInfo;
			foreach (ButterflyHabitatPoint butterflyHabitatPoint in this.ButterflyHabitatPoints)
			{
				if (!(butterflyHabitatPoint == null))
				{
					butterflyHabitatPoint.IsStop = true;
					butterflyHabitatPoint.IsActive = (!this.IsRain() && !this.IsNight());
					butterflyHabitatPoint.IsCreate = false;
					butterflyHabitatPoint.AddCheck = ((Vector3 pos) => !this.IsRain() && !this.IsNight());
					butterflyHabitatPoint.AddAnimalAction = delegate(ButterflyHabitatPoint _basePoint)
					{
						if (_basePoint == null)
						{
							return null;
						}
						WildButterfly _butterfly = this.Create<WildButterfly>(3, 0);
						if (_butterfly == null)
						{
							return null;
						}
						_butterfly.OnDestroyAsObservable().Subscribe(delegate(Unit _)
						{
							this.RemoveAnimal(_butterfly, false);
						});
						_butterfly.Initialize(_basePoint);
						return _butterfly;
					};
				}
			}
		}

		// Token: 0x06003BA8 RID: 15272 RVA: 0x0015E164 File Offset: 0x0015C564
		private void ActivateHabitatPoints()
		{
			if (!this.WildGroundHabitatPoints.IsNullOrEmpty<GroundAnimalHabitatPoint>())
			{
				foreach (GroundAnimalHabitatPoint groundAnimalHabitatPoint in this.WildGroundHabitatPoints)
				{
					if (groundAnimalHabitatPoint != null && groundAnimalHabitatPoint.IsPopPoint)
					{
						groundAnimalHabitatPoint.IsCountStop = false;
					}
				}
			}
			if (!this.MechaHabitatPoints.IsNullOrEmpty<MechaHabitatPoint>())
			{
				foreach (MechaHabitatPoint mechaHabitatPoint in this.MechaHabitatPoints)
				{
					if (mechaHabitatPoint != null)
					{
						mechaHabitatPoint.IsCountStop = false;
					}
				}
			}
			if (!this.FrogHabitatPoints.IsNullOrEmpty<FrogHabitatPoint>())
			{
				foreach (FrogHabitatPoint frogHabitatPoint in this.FrogHabitatPoints)
				{
					if (frogHabitatPoint != null)
					{
						frogHabitatPoint.IsCountStop = false;
					}
				}
			}
			if (!this.BirdFlockHabitatPoints.IsNullOrEmpty<BirdFlockHabitatPoint>())
			{
				foreach (BirdFlockHabitatPoint birdFlockHabitatPoint in this.BirdFlockHabitatPoints)
				{
					if (birdFlockHabitatPoint != null)
					{
						birdFlockHabitatPoint.IsCountStop = false;
					}
				}
			}
			if (!this.ButterflyHabitatPoints.IsNullOrEmpty<ButterflyHabitatPoint>())
			{
				foreach (ButterflyHabitatPoint butterflyHabitatPoint in this.ButterflyHabitatPoints)
				{
					if (butterflyHabitatPoint != null)
					{
						butterflyHabitatPoint.IsStop = false;
					}
				}
			}
		}

		// Token: 0x06003BA9 RID: 15273 RVA: 0x0015E390 File Offset: 0x0015C790
		private void DeactivateHabitatPoints()
		{
			if (!this.WildGroundHabitatPoints.IsNullOrEmpty<GroundAnimalHabitatPoint>())
			{
				foreach (GroundAnimalHabitatPoint groundAnimalHabitatPoint in this.WildGroundHabitatPoints)
				{
					if (groundAnimalHabitatPoint != null && groundAnimalHabitatPoint.IsPopPoint)
					{
						groundAnimalHabitatPoint.IsCountStop = true;
					}
				}
			}
			if (!this.MechaHabitatPoints.IsNullOrEmpty<MechaHabitatPoint>())
			{
				foreach (MechaHabitatPoint mechaHabitatPoint in this.MechaHabitatPoints)
				{
					if (mechaHabitatPoint != null)
					{
						mechaHabitatPoint.IsCountStop = true;
					}
				}
			}
			if (!this.FrogHabitatPoints.IsNullOrEmpty<FrogHabitatPoint>())
			{
				foreach (FrogHabitatPoint frogHabitatPoint in this.FrogHabitatPoints)
				{
					if (frogHabitatPoint != null)
					{
						frogHabitatPoint.IsCountStop = true;
					}
				}
			}
			if (!this.BirdFlockHabitatPoints.IsNullOrEmpty<BirdFlockHabitatPoint>())
			{
				foreach (BirdFlockHabitatPoint birdFlockHabitatPoint in this.BirdFlockHabitatPoints)
				{
					if (birdFlockHabitatPoint != null)
					{
						birdFlockHabitatPoint.IsCountStop = true;
					}
				}
			}
			if (!this.ButterflyHabitatPoints.IsNullOrEmpty<ButterflyHabitatPoint>())
			{
				foreach (ButterflyHabitatPoint butterflyHabitatPoint in this.ButterflyHabitatPoints)
				{
					if (butterflyHabitatPoint != null)
					{
						butterflyHabitatPoint.IsStop = true;
					}
				}
			}
		}

		// Token: 0x06003BAA RID: 15274 RVA: 0x0015E5BC File Offset: 0x0015C9BC
		public void ClearAnimalPointBehavior()
		{
			this.DeactivateHabitatPoints();
		}

		// Token: 0x06003BAB RID: 15275 RVA: 0x0015E5C4 File Offset: 0x0015C9C4
		public bool IsRain(Weather _weather)
		{
			return _weather == Weather.Rain || _weather == Weather.Storm;
		}

		// Token: 0x06003BAC RID: 15276 RVA: 0x0015E5DC File Offset: 0x0015C9DC
		public bool IsRain(EnvironmentSimulator _simulator)
		{
			return !(_simulator == null) && this.IsRain(_simulator.Weather);
		}

		// Token: 0x06003BAD RID: 15277 RVA: 0x0015E5F8 File Offset: 0x0015C9F8
		public bool IsRain()
		{
			if (!Singleton<Map>.IsInstance())
			{
				return false;
			}
			EnvironmentSimulator simulator = Singleton<Map>.Instance.Simulator;
			return !(simulator == null) && this.IsRain(simulator.Weather);
		}

		// Token: 0x06003BAE RID: 15278 RVA: 0x0015E636 File Offset: 0x0015CA36
		public bool IsNight(AIProject.TimeZone _timeZone)
		{
			return _timeZone == AIProject.TimeZone.Night;
		}

		// Token: 0x06003BAF RID: 15279 RVA: 0x0015E63C File Offset: 0x0015CA3C
		public bool IsNight(EnvironmentSimulator _simulator)
		{
			return !(_simulator == null) && this.IsNight(_simulator.TimeZone);
		}

		// Token: 0x06003BB0 RID: 15280 RVA: 0x0015E658 File Offset: 0x0015CA58
		public bool IsNight()
		{
			if (!Singleton<Map>.IsInstance())
			{
				return false;
			}
			EnvironmentSimulator simulator = Singleton<Map>.Instance.Simulator;
			return !(simulator == null) && this.IsNight(simulator.TimeZone);
		}

		// Token: 0x17000ACF RID: 2767
		// (get) Token: 0x06003BB1 RID: 15281 RVA: 0x0015E696 File Offset: 0x0015CA96
		// (set) Token: 0x06003BB2 RID: 15282 RVA: 0x0015E69E File Offset: 0x0015CA9E
		public int AnimalCount { get; private set; }

		// Token: 0x17000AD0 RID: 2768
		// (get) Token: 0x06003BB3 RID: 15283 RVA: 0x0015E6A7 File Offset: 0x0015CAA7
		// (set) Token: 0x06003BB4 RID: 15284 RVA: 0x0015E6AF File Offset: 0x0015CAAF
		private Dictionary<AnimalTypes, Dictionary<BreedingTypes, GameObject>> AnimalBaseTable { get; set; } = new Dictionary<AnimalTypes, Dictionary<BreedingTypes, GameObject>>();

		// Token: 0x17000AD1 RID: 2769
		// (get) Token: 0x06003BB5 RID: 15285 RVA: 0x0015E6B8 File Offset: 0x0015CAB8
		// (set) Token: 0x06003BB6 RID: 15286 RVA: 0x0015E6C0 File Offset: 0x0015CAC0
		private Dictionary<int, Dictionary<int, GameObject>> AnimalBaseObjectTable { get; set; } = new Dictionary<int, Dictionary<int, GameObject>>();

		// Token: 0x17000AD2 RID: 2770
		// (get) Token: 0x06003BB7 RID: 15287 RVA: 0x0015E6CC File Offset: 0x0015CACC
		public Transform AnimalRoot
		{
			get
			{
				if (this._animalRoot != null)
				{
					return this._animalRoot;
				}
				if (!Singleton<Map>.IsInstance())
				{
					return null;
				}
				Transform actorRoot = Singleton<Map>.Instance.ActorRoot;
				this._animalRoot = new GameObject("AnimalRoot").transform;
				this._animalRoot.SetParent(actorRoot, false);
				return this._animalRoot;
			}
		}

		// Token: 0x17000AD3 RID: 2771
		// (get) Token: 0x06003BB8 RID: 15288 RVA: 0x0015E730 File Offset: 0x0015CB30
		// (set) Token: 0x06003BB9 RID: 15289 RVA: 0x0015E738 File Offset: 0x0015CB38
		public bool ActiveMapScene { get; set; }

		// Token: 0x17000AD4 RID: 2772
		// (get) Token: 0x06003BBA RID: 15290 RVA: 0x0015E741 File Offset: 0x0015CB41
		// (set) Token: 0x06003BBB RID: 15291 RVA: 0x0015E749 File Offset: 0x0015CB49
		public List<AnimalBase> Animals { get; private set; } = new List<AnimalBase>();

		// Token: 0x17000AD5 RID: 2773
		// (get) Token: 0x06003BBC RID: 15292 RVA: 0x0015E752 File Offset: 0x0015CB52
		// (set) Token: 0x06003BBD RID: 15293 RVA: 0x0015E75A File Offset: 0x0015CB5A
		public List<AnimalBase> WildAnimals { get; private set; } = new List<AnimalBase>();

		// Token: 0x17000AD6 RID: 2774
		// (get) Token: 0x06003BBE RID: 15294 RVA: 0x0015E763 File Offset: 0x0015CB63
		// (set) Token: 0x06003BBF RID: 15295 RVA: 0x0015E76B File Offset: 0x0015CB6B
		public List<AnimalBase> PetAnimals { get; private set; } = new List<AnimalBase>();

		// Token: 0x17000AD7 RID: 2775
		// (get) Token: 0x06003BC0 RID: 15296 RVA: 0x0015E774 File Offset: 0x0015CB74
		// (set) Token: 0x06003BC1 RID: 15297 RVA: 0x0015E77C File Offset: 0x0015CB7C
		public List<WildGround> WildCats { get; private set; } = new List<WildGround>();

		// Token: 0x17000AD8 RID: 2776
		// (get) Token: 0x06003BC2 RID: 15298 RVA: 0x0015E785 File Offset: 0x0015CB85
		// (set) Token: 0x06003BC3 RID: 15299 RVA: 0x0015E78D File Offset: 0x0015CB8D
		public List<WildGround> WildChickens { get; private set; } = new List<WildGround>();

		// Token: 0x17000AD9 RID: 2777
		// (get) Token: 0x06003BC4 RID: 15300 RVA: 0x0015E796 File Offset: 0x0015CB96
		// (set) Token: 0x06003BC5 RID: 15301 RVA: 0x0015E79E File Offset: 0x0015CB9E
		public List<WildMecha> WildMechas { get; private set; } = new List<WildMecha>();

		// Token: 0x17000ADA RID: 2778
		// (get) Token: 0x06003BC6 RID: 15302 RVA: 0x0015E7A7 File Offset: 0x0015CBA7
		// (set) Token: 0x06003BC7 RID: 15303 RVA: 0x0015E7AF File Offset: 0x0015CBAF
		public List<WildFrog> WildFrogs { get; private set; } = new List<WildFrog>();

		// Token: 0x17000ADB RID: 2779
		// (get) Token: 0x06003BC8 RID: 15304 RVA: 0x0015E7B8 File Offset: 0x0015CBB8
		// (set) Token: 0x06003BC9 RID: 15305 RVA: 0x0015E7C0 File Offset: 0x0015CBC0
		public List<WildButterfly> WildButterflies { get; private set; } = new List<WildButterfly>();

		// Token: 0x17000ADC RID: 2780
		// (get) Token: 0x06003BCA RID: 15306 RVA: 0x0015E7C9 File Offset: 0x0015CBC9
		// (set) Token: 0x06003BCB RID: 15307 RVA: 0x0015E7D1 File Offset: 0x0015CBD1
		public List<WildBirdFlock> WildBirdFlocks { get; private set; } = new List<WildBirdFlock>();

		// Token: 0x17000ADD RID: 2781
		// (get) Token: 0x06003BCC RID: 15308 RVA: 0x0015E7DA File Offset: 0x0015CBDA
		// (set) Token: 0x06003BCD RID: 15309 RVA: 0x0015E7E2 File Offset: 0x0015CBE2
		public List<WalkingPetAnimal> PetCats { get; private set; } = new List<WalkingPetAnimal>();

		// Token: 0x17000ADE RID: 2782
		// (get) Token: 0x06003BCE RID: 15310 RVA: 0x0015E7EB File Offset: 0x0015CBEB
		// (set) Token: 0x06003BCF RID: 15311 RVA: 0x0015E7F3 File Offset: 0x0015CBF3
		public List<PetChicken> PetChickens { get; private set; } = new List<PetChicken>();

		// Token: 0x17000ADF RID: 2783
		// (get) Token: 0x06003BD0 RID: 15312 RVA: 0x0015E7FC File Offset: 0x0015CBFC
		// (set) Token: 0x06003BD1 RID: 15313 RVA: 0x0015E804 File Offset: 0x0015CC04
		public List<PetFish> PetFishes { get; private set; } = new List<PetFish>();

		// Token: 0x17000AE0 RID: 2784
		// (get) Token: 0x06003BD2 RID: 15314 RVA: 0x0015E80D File Offset: 0x0015CC0D
		// (set) Token: 0x06003BD3 RID: 15315 RVA: 0x0015E815 File Offset: 0x0015CC15
		public List<FlyingPetAnimal> PetButterflies { get; private set; } = new List<FlyingPetAnimal>();

		// Token: 0x17000AE1 RID: 2785
		// (get) Token: 0x06003BD4 RID: 15316 RVA: 0x0015E81E File Offset: 0x0015CC1E
		// (set) Token: 0x06003BD5 RID: 15317 RVA: 0x0015E826 File Offset: 0x0015CC26
		public List<WalkingPetAnimal> PetMechas { get; private set; } = new List<WalkingPetAnimal>();

		// Token: 0x17000AE2 RID: 2786
		// (get) Token: 0x06003BD6 RID: 15318 RVA: 0x0015E82F File Offset: 0x0015CC2F
		// (set) Token: 0x06003BD7 RID: 15319 RVA: 0x0015E837 File Offset: 0x0015CC37
		public List<MovingPetAnimal> MovingPets { get; private set; } = new List<MovingPetAnimal>();

		// Token: 0x17000AE3 RID: 2787
		// (get) Token: 0x06003BD8 RID: 15320 RVA: 0x0015E840 File Offset: 0x0015CC40
		// (set) Token: 0x06003BD9 RID: 15321 RVA: 0x0015E848 File Offset: 0x0015CC48
		public List<WalkingPetAnimal> WalkingPets { get; private set; } = new List<WalkingPetAnimal>();

		// Token: 0x17000AE4 RID: 2788
		// (get) Token: 0x06003BDA RID: 15322 RVA: 0x0015E851 File Offset: 0x0015CC51
		// (set) Token: 0x06003BDB RID: 15323 RVA: 0x0015E859 File Offset: 0x0015CC59
		public List<FlyingPetAnimal> FlyingPets { get; private set; } = new List<FlyingPetAnimal>();

		// Token: 0x17000AE5 RID: 2789
		// (get) Token: 0x06003BDC RID: 15324 RVA: 0x0015E862 File Offset: 0x0015CC62
		// (set) Token: 0x06003BDD RID: 15325 RVA: 0x0015E86A File Offset: 0x0015CC6A
		public ReadOnlyDictionary<int, AnimalBase> AnimalTable { get; private set; }

		// Token: 0x17000AE6 RID: 2790
		// (get) Token: 0x06003BDE RID: 15326 RVA: 0x0015E873 File Offset: 0x0015CC73
		private Dictionary<AnimalTypes, Dictionary<BreedingTypes, int>> AnimalCountTable { get; } = new Dictionary<AnimalTypes, Dictionary<BreedingTypes, int>>();

		// Token: 0x17000AE7 RID: 2791
		// (get) Token: 0x06003BDF RID: 15327 RVA: 0x0015E87B File Offset: 0x0015CC7B
		// (set) Token: 0x06003BE0 RID: 15328 RVA: 0x0015E883 File Offset: 0x0015CC83
		public List<AnimalActionPoint> ActionPoints { get; private set; } = new List<AnimalActionPoint>();

		// Token: 0x17000AE8 RID: 2792
		// (get) Token: 0x06003BE1 RID: 15329 RVA: 0x0015E88C File Offset: 0x0015CC8C
		// (set) Token: 0x06003BE2 RID: 15330 RVA: 0x0015E894 File Offset: 0x0015CC94
		public List<GroundAnimalHabitatPoint> WildGroundHabitatPoints { get; private set; } = new List<GroundAnimalHabitatPoint>();

		// Token: 0x17000AE9 RID: 2793
		// (get) Token: 0x06003BE3 RID: 15331 RVA: 0x0015E89D File Offset: 0x0015CC9D
		// (set) Token: 0x06003BE4 RID: 15332 RVA: 0x0015E8A5 File Offset: 0x0015CCA5
		public List<GroundAnimalHabitatPoint> WildCatPopPoints { get; private set; } = new List<GroundAnimalHabitatPoint>();

		// Token: 0x17000AEA RID: 2794
		// (get) Token: 0x06003BE5 RID: 15333 RVA: 0x0015E8AE File Offset: 0x0015CCAE
		// (set) Token: 0x06003BE6 RID: 15334 RVA: 0x0015E8B6 File Offset: 0x0015CCB6
		public List<GroundAnimalHabitatPoint> WildChickenPopPoints { get; private set; } = new List<GroundAnimalHabitatPoint>();

		// Token: 0x17000AEB RID: 2795
		// (get) Token: 0x06003BE7 RID: 15335 RVA: 0x0015E8BF File Offset: 0x0015CCBF
		// (set) Token: 0x06003BE8 RID: 15336 RVA: 0x0015E8C7 File Offset: 0x0015CCC7
		public List<GroundAnimalHabitatPoint> WildCatAndChickenPopPoints { get; private set; } = new List<GroundAnimalHabitatPoint>();

		// Token: 0x17000AEC RID: 2796
		// (get) Token: 0x06003BE9 RID: 15337 RVA: 0x0015E8D0 File Offset: 0x0015CCD0
		// (set) Token: 0x06003BEA RID: 15338 RVA: 0x0015E8D8 File Offset: 0x0015CCD8
		public List<GroundAnimalHabitatPoint> WildCatDepopPoints { get; private set; } = new List<GroundAnimalHabitatPoint>();

		// Token: 0x17000AED RID: 2797
		// (get) Token: 0x06003BEB RID: 15339 RVA: 0x0015E8E1 File Offset: 0x0015CCE1
		// (set) Token: 0x06003BEC RID: 15340 RVA: 0x0015E8E9 File Offset: 0x0015CCE9
		public List<GroundAnimalHabitatPoint> WildChickenDepopPoints { get; private set; } = new List<GroundAnimalHabitatPoint>();

		// Token: 0x17000AEE RID: 2798
		// (get) Token: 0x06003BED RID: 15341 RVA: 0x0015E8F2 File Offset: 0x0015CCF2
		// (set) Token: 0x06003BEE RID: 15342 RVA: 0x0015E8FA File Offset: 0x0015CCFA
		public List<GroundAnimalHabitatPoint> WildCatAndChickenDepopPoints { get; private set; } = new List<GroundAnimalHabitatPoint>();

		// Token: 0x17000AEF RID: 2799
		// (get) Token: 0x06003BEF RID: 15343 RVA: 0x0015E903 File Offset: 0x0015CD03
		// (set) Token: 0x06003BF0 RID: 15344 RVA: 0x0015E90B File Offset: 0x0015CD0B
		public List<MechaHabitatPoint> MechaHabitatPoints { get; private set; } = new List<MechaHabitatPoint>();

		// Token: 0x17000AF0 RID: 2800
		// (get) Token: 0x06003BF1 RID: 15345 RVA: 0x0015E914 File Offset: 0x0015CD14
		// (set) Token: 0x06003BF2 RID: 15346 RVA: 0x0015E91C File Offset: 0x0015CD1C
		public List<FrogHabitatPoint> FrogHabitatPoints { get; private set; } = new List<FrogHabitatPoint>();

		// Token: 0x17000AF1 RID: 2801
		// (get) Token: 0x06003BF3 RID: 15347 RVA: 0x0015E925 File Offset: 0x0015CD25
		// (set) Token: 0x06003BF4 RID: 15348 RVA: 0x0015E92D File Offset: 0x0015CD2D
		public List<ButterflyHabitatPoint> ButterflyHabitatPoints { get; private set; } = new List<ButterflyHabitatPoint>();

		// Token: 0x17000AF2 RID: 2802
		// (get) Token: 0x06003BF5 RID: 15349 RVA: 0x0015E936 File Offset: 0x0015CD36
		// (set) Token: 0x06003BF6 RID: 15350 RVA: 0x0015E93E File Offset: 0x0015CD3E
		public List<BirdFlockHabitatPoint> BirdFlockHabitatPoints { get; private set; } = new List<BirdFlockHabitatPoint>();

		// Token: 0x06003BF7 RID: 15351 RVA: 0x0015E948 File Offset: 0x0015CD48
		public void ClearAnimalPoints()
		{
			this.ActionPoints.Clear();
			this.WildGroundHabitatPoints.Clear();
			this.WildCatPopPoints.Clear();
			this.WildChickenPopPoints.Clear();
			this.WildCatDepopPoints.Clear();
			this.WildCatAndChickenPopPoints.Clear();
			this.WildChickenDepopPoints.Clear();
			this.WildCatAndChickenDepopPoints.Clear();
			this.MechaHabitatPoints.Clear();
			this.FrogHabitatPoints.Clear();
			this.ButterflyHabitatPoints.Clear();
			this.BirdFlockHabitatPoints.Clear();
		}

		// Token: 0x06003BF8 RID: 15352 RVA: 0x0015E9D9 File Offset: 0x0015CDD9
		public void ClearAllAnimals()
		{
			if (this._animalRoot != null)
			{
				if (this._animalRoot.gameObject != null)
				{
					UnityEngine.Object.Destroy(this._animalRoot.gameObject);
				}
				this._animalRoot = null;
			}
		}

		// Token: 0x06003BF9 RID: 15353 RVA: 0x0015EA19 File Offset: 0x0015CE19
		public void ReleaseAnimal()
		{
			this.ClearAnimalPointBehavior();
			this.ClearAnimalPoints();
			this.ClearAllAnimals();
		}

		// Token: 0x06003BFA RID: 15354 RVA: 0x0015EA30 File Offset: 0x0015CE30
		protected override void Awake()
		{
			if (!base.CheckInstance())
			{
				return;
			}
			this.AnimalTable = new ReadOnlyDictionary<int, AnimalBase>(this.animalTable);
			if (AnimalManager._commandRefreshEvent == null)
			{
				AnimalManager._commandRefreshEvent = new Subject<Unit>();
				(from _ in AnimalManager._commandRefreshEvent.Buffer(AnimalManager._commandRefreshEvent.ThrottleFrame(1, FrameCountType.Update)).TakeUntilDestroy(base.gameObject)
				where !_.IsNullOrEmpty<Unit>()
				select _).Subscribe(delegate(IList<Unit> _)
				{
					CommandArea commandArea = Map.GetCommandArea();
					if (commandArea == null)
					{
						return;
					}
					commandArea.RefreshCommands();
				});
			}
		}

		// Token: 0x06003BFB RID: 15355 RVA: 0x0015EAD4 File Offset: 0x0015CED4
		protected override void OnDestroy()
		{
			if (!Singleton<AnimalManager>.IsInstance())
			{
				return;
			}
			if (Singleton<AnimalManager>.Instance != this)
			{
				return;
			}
			base.OnDestroy();
		}

		// Token: 0x06003BFC RID: 15356 RVA: 0x0015EAF8 File Offset: 0x0015CEF8
		private void AddCommandableObject(AnimalBase animal)
		{
			if (animal == null || !animal.IsCommandable)
			{
				return;
			}
			CommandArea commandArea = Map.GetCommandArea();
			if (commandArea == null)
			{
				return;
			}
			commandArea.AddCommandableObject(animal);
		}

		// Token: 0x06003BFD RID: 15357 RVA: 0x0015EB38 File Offset: 0x0015CF38
		private void RemoveCommandableObject(AnimalBase animal)
		{
			if (animal == null || !animal.IsCommandable)
			{
				return;
			}
			CommandArea commandArea = Map.GetCommandArea();
			if (commandArea == null)
			{
				return;
			}
			commandArea.RemoveCommandableObject(animal);
		}

		// Token: 0x06003BFE RID: 15358 RVA: 0x0015EB78 File Offset: 0x0015CF78
		public void StartSubscribe()
		{
			if (!Singleton<Map>.IsInstance())
			{
				return;
			}
			if (this.enviromentSubscribeDisposable != null)
			{
				this.enviromentSubscribeDisposable.Clear();
			}
			this.enviromentSubscribeDisposable = new CompositeDisposable();
			EnvironmentSimulator _simulator = Singleton<Map>.Instance.Simulator;
			(from _ in _simulator.OnWeatherChangedAsObservable().TakeUntilDestroy(base.gameObject)
			where this.isActiveAndEnabled
			select _).OnErrorRetry(delegate(Exception _ex)
			{
			}).Subscribe(delegate(Weather _weather)
			{
				this.RefreshWeather(_simulator);
			}).AddTo(this.enviromentSubscribeDisposable);
			(from _ in _simulator.OnTimeZoneChangedAsObservable().TakeUntilDestroy(base.gameObject)
			where this.isActiveAndEnabled
			select _).OnErrorRetry(delegate(Exception _ex)
			{
			}).Subscribe(delegate(AIProject.TimeZone _)
			{
				this.RefreshTimeZone(_simulator);
			}).AddTo(this.enviromentSubscribeDisposable);
			(from _ in _simulator.OnEnvironmentChangedAsObservable().TakeUntilDestroy(base.gameObject)
			where this.isActiveAndEnabled
			select _).OnErrorRetry(delegate(Exception _ex)
			{
			}).Subscribe(delegate(Unit _)
			{
				this.RefreshEnvironment();
			}).AddTo(this.enviromentSubscribeDisposable);
			(from _ in _simulator.OnMinuteAsObservable().TakeUntilDestroy(base.gameObject)
			where this.isActiveAndEnabled
			select _).OnErrorRetry(delegate(Exception _ex)
			{
			}).Subscribe(delegate(TimeSpan timeSpan)
			{
				this.OnElapsedMinute(timeSpan);
			}, delegate(Exception _ex)
			{
			}, delegate()
			{
			}).AddTo(this.enviromentSubscribeDisposable);
			(from _ in _simulator.OnSecondAsObservable().TakeUntilDestroy(base.gameObject)
			where this.isActiveAndEnabled
			select _).OnErrorRetry(delegate(Exception _ex)
			{
			}).Subscribe(delegate(TimeSpan timeSpan)
			{
				this.OnElapsedSecond(timeSpan);
			}, delegate(Exception _ex)
			{
			}, delegate()
			{
			}).AddTo(this.enviromentSubscribeDisposable);
			LayerMask groundCheckLayer = Singleton<Resources>.Instance.DefinePack.MapDefines.AreaDetectionLayer;
			if (this.mapAreaCheckDisposable != null)
			{
				this.mapAreaCheckDisposable.Dispose();
			}
			this.mapAreaCheckDisposable = (from _ in Singleton<Map>.Instance.FixedUpdateAsObservable()
			where !this.Animals.IsNullOrEmpty<AnimalBase>()
			select _).Subscribe(delegate(Unit _)
			{
				foreach (AnimalBase animalBase in this.Animals)
				{
					if (!(animalBase == null) && animalBase.OnGroundCheck && animalBase.Active)
					{
						animalBase.UpdateCurrentMapArea(groundCheckLayer);
					}
				}
			});
		}

		// Token: 0x06003BFF RID: 15359 RVA: 0x0015EE51 File Offset: 0x0015D251
		public void StopSubscribe()
		{
			if (this.enviromentSubscribeDisposable != null)
			{
				this.enviromentSubscribeDisposable.Clear();
				this.enviromentSubscribeDisposable = null;
			}
		}

		// Token: 0x06003C00 RID: 15360 RVA: 0x0015EE70 File Offset: 0x0015D270
		private void RefreshWeather(EnvironmentSimulator _simulator)
		{
			foreach (AnimalBase animalBase in this.Animals)
			{
				if (animalBase != null)
				{
					animalBase.OnWeatherChanged(_simulator);
				}
			}
			if (!this.WildGroundHabitatPoints.IsNullOrEmpty<GroundAnimalHabitatPoint>())
			{
				foreach (GroundAnimalHabitatPoint groundAnimalHabitatPoint in this.WildGroundHabitatPoints)
				{
					if (!(groundAnimalHabitatPoint == null))
					{
						bool isActive = groundAnimalHabitatPoint.IsActive;
						groundAnimalHabitatPoint.IsActive = !this.IsRain(_simulator.Weather);
						if (groundAnimalHabitatPoint.IsActive && !isActive)
						{
							groundAnimalHabitatPoint.SetCoolTime();
						}
					}
				}
			}
			if (!this.BirdFlockHabitatPoints.IsNullOrEmpty<BirdFlockHabitatPoint>())
			{
				foreach (BirdFlockHabitatPoint birdFlockHabitatPoint in this.BirdFlockHabitatPoints)
				{
					if (!(birdFlockHabitatPoint == null))
					{
						birdFlockHabitatPoint.IsActive = (!this.IsRain(_simulator.Weather) && !this.IsNight(_simulator.TimeZone));
					}
				}
			}
			if (!this.ButterflyHabitatPoints.IsNullOrEmpty<ButterflyHabitatPoint>())
			{
				foreach (ButterflyHabitatPoint butterflyHabitatPoint in this.ButterflyHabitatPoints)
				{
					if (!(butterflyHabitatPoint == null))
					{
						bool isActive2 = butterflyHabitatPoint.IsActive;
						butterflyHabitatPoint.IsActive = (!this.IsRain(_simulator.Weather) && !this.IsNight(_simulator.TimeZone));
						if (butterflyHabitatPoint.IsActive && !isActive2)
						{
							butterflyHabitatPoint.IsCreate = false;
						}
					}
				}
			}
		}

		// Token: 0x06003C01 RID: 15361 RVA: 0x0015F0BC File Offset: 0x0015D4BC
		private void RefreshTimeZone(EnvironmentSimulator _simulator)
		{
			foreach (AnimalBase animalBase in this.Animals)
			{
				if (animalBase != null)
				{
					animalBase.OnTimeZoneChanged(_simulator);
				}
			}
			if (!this.BirdFlockHabitatPoints.IsNullOrEmpty<BirdFlockHabitatPoint>())
			{
				foreach (BirdFlockHabitatPoint birdFlockHabitatPoint in this.BirdFlockHabitatPoints)
				{
					if (!(birdFlockHabitatPoint == null))
					{
						birdFlockHabitatPoint.IsActive = (!this.IsRain(_simulator.Weather) && !this.IsNight(_simulator.TimeZone));
					}
				}
			}
			if (!this.ButterflyHabitatPoints.IsNullOrEmpty<ButterflyHabitatPoint>())
			{
				foreach (ButterflyHabitatPoint butterflyHabitatPoint in this.ButterflyHabitatPoints)
				{
					if (!(butterflyHabitatPoint == null))
					{
						bool isActive = butterflyHabitatPoint.IsActive;
						butterflyHabitatPoint.IsActive = (!this.IsRain(_simulator.Weather) && !this.IsNight(_simulator.TimeZone));
						if (butterflyHabitatPoint.IsActive && !isActive)
						{
							butterflyHabitatPoint.IsCreate = false;
						}
					}
				}
			}
		}

		// Token: 0x06003C02 RID: 15362 RVA: 0x0015F268 File Offset: 0x0015D668
		private void RefreshEnvironment()
		{
			if (!Singleton<Map>.IsInstance())
			{
				return;
			}
			EnvironmentSimulator simulator = Singleton<Map>.Instance.Simulator;
			foreach (AnimalBase animalBase in this.Animals)
			{
				if (animalBase != null)
				{
					animalBase.OnEnvironmentChanged(simulator);
				}
			}
		}

		// Token: 0x06003C03 RID: 15363 RVA: 0x0015F2E8 File Offset: 0x0015D6E8
		private void OnElapsedMinute(TimeSpan _deltaTime)
		{
			foreach (AnimalBase animalBase in this.Animals)
			{
				if (animalBase != null)
				{
					animalBase.OnMinuteUpdate(_deltaTime);
				}
			}
		}

		// Token: 0x06003C04 RID: 15364 RVA: 0x0015F350 File Offset: 0x0015D750
		private void OnElapsedSecond(TimeSpan _deltaTime)
		{
			foreach (AnimalBase animalBase in this.Animals)
			{
				if (animalBase != null)
				{
					animalBase.OnSecondUpdate(_deltaTime);
				}
			}
		}

		// Token: 0x06003C05 RID: 15365 RVA: 0x0015F3B8 File Offset: 0x0015D7B8
		public void RefreshStates(Map _map)
		{
			foreach (AnimalBase animalBase in this.Animals)
			{
				if (!(animalBase == null))
				{
					animalBase.RefreshSearchTarget();
					animalBase.OnEnvironmentChanged(_map.Simulator);
				}
			}
		}

		// Token: 0x06003C06 RID: 15366 RVA: 0x0015F430 File Offset: 0x0015D830
		private void AddPoint(AnimalPoint _point)
		{
			if (_point == null)
			{
				return;
			}
			if (_point is AnimalActionPoint)
			{
				this.ActionPoints.Add(_point as AnimalActionPoint);
			}
			else if (_point is GroundAnimalHabitatPoint)
			{
				GroundAnimalHabitatPoint groundAnimalHabitatPoint = _point as GroundAnimalHabitatPoint;
				this.WildGroundHabitatPoints.Add(groundAnimalHabitatPoint);
				if (groundAnimalHabitatPoint.IsCatOnly)
				{
					if (groundAnimalHabitatPoint.IsPopPoint)
					{
						this.WildCatPopPoints.Add(groundAnimalHabitatPoint);
					}
					if (groundAnimalHabitatPoint.IsDepopPoint)
					{
						this.WildCatDepopPoints.Add(groundAnimalHabitatPoint);
					}
				}
				if (groundAnimalHabitatPoint.IsChickenOnly)
				{
					if (groundAnimalHabitatPoint.IsPopPoint)
					{
						this.WildChickenPopPoints.Add(groundAnimalHabitatPoint);
					}
					if (groundAnimalHabitatPoint.IsDepopPoint)
					{
						this.WildChickenDepopPoints.Add(groundAnimalHabitatPoint);
					}
				}
				if (groundAnimalHabitatPoint.IsBoth)
				{
					if (groundAnimalHabitatPoint.IsPopPoint)
					{
						this.WildCatAndChickenPopPoints.Add(groundAnimalHabitatPoint);
					}
					if (groundAnimalHabitatPoint.IsDepopPoint)
					{
						this.WildCatAndChickenPopPoints.Add(groundAnimalHabitatPoint);
					}
				}
			}
			else if (_point is MechaHabitatPoint)
			{
				this.MechaHabitatPoints.Add(_point as MechaHabitatPoint);
			}
			else if (_point is FrogHabitatPoint)
			{
				this.FrogHabitatPoints.Add(_point as FrogHabitatPoint);
			}
			else if (_point is ButterflyHabitatPoint)
			{
				this.ButterflyHabitatPoints.Add(_point as ButterflyHabitatPoint);
			}
			else if (_point is BirdFlockHabitatPoint)
			{
				this.BirdFlockHabitatPoints.Add(_point as BirdFlockHabitatPoint);
			}
		}

		// Token: 0x06003C07 RID: 15367 RVA: 0x0015F5B8 File Offset: 0x0015D9B8
		private void LogAnimalPoint()
		{
		}

		// Token: 0x06003C08 RID: 15368 RVA: 0x0015F5BC File Offset: 0x0015D9BC
		private IEnumerator SetupGroundAnimalHabitatPointsAsync(Waypoint[] _waypoints, List<GroundAnimalHabitatPoint> _popPoints, List<GroundAnimalHabitatPoint> _depopPoints, int _wait)
		{
			if (_popPoints.IsNullOrEmpty<GroundAnimalHabitatPoint>())
			{
				yield break;
			}
			GroundAnimalHabitatPoint[] _depopPointArray = _depopPoints.ToArray();
			foreach (GroundAnimalHabitatPoint _popPoint in _popPoints)
			{
				yield return _popPoint.SetPointsAsync(_waypoints, _depopPointArray, _wait);
			}
			int _count = 0;
			for (int i = 0; i < _popPoints.Count; i++)
			{
				int num;
				_count = (num = _count) + 1;
				if (num % _wait == 0)
				{
					yield return null;
				}
				GroundAnimalHabitatPoint _point = _popPoints[i];
				if (_point.Waypoints.IsNullOrEmpty<Waypoint>() || _point.DepopPoints.IsNullOrEmpty<GroundAnimalHabitatPoint>())
				{
					_popPoints.RemoveAt(i);
					i--;
				}
			}
			yield break;
		}

		// Token: 0x06003C09 RID: 15369 RVA: 0x0015F5F0 File Offset: 0x0015D9F0
		public IEnumerator SetupPointsAsync(PointManager _pointManager)
		{
			this.ClearAnimalPoints();
			int _count = 0;
			int _wait = 32;
			AnimalPoint[] _animalPoints = _pointManager.AnimalPoints;
			if (!_animalPoints.IsNullOrEmpty<AnimalPoint>())
			{
				foreach (AnimalPoint _point in _animalPoints)
				{
					this.AddPoint(_point);
					if (++_count % _wait == 0)
					{
						yield return null;
					}
				}
			}
			if (this.WildCatPopPoints.IsNullOrEmpty<GroundAnimalHabitatPoint>() && this.WildChickenPopPoints.IsNullOrEmpty<GroundAnimalHabitatPoint>() && this.WildCatAndChickenPopPoints.IsNullOrEmpty<GroundAnimalHabitatPoint>())
			{
				this.LogAnimalPoint();
				yield break;
			}
			Waypoint[] _waypoints = _pointManager.Waypoints;
			yield return this.SetupGroundAnimalHabitatPointsAsync(_waypoints, this.WildCatPopPoints, this.WildCatDepopPoints, _wait);
			yield return this.SetupGroundAnimalHabitatPointsAsync(_waypoints, this.WildChickenPopPoints, this.WildChickenDepopPoints, _wait);
			yield return this.SetupGroundAnimalHabitatPointsAsync(_waypoints, this.WildCatAndChickenPopPoints, this.WildCatAndChickenDepopPoints, _wait);
			this.LogAnimalPoint();
			yield break;
		}

		// Token: 0x06003C0A RID: 15370 RVA: 0x0015F614 File Offset: 0x0015DA14
		public static int GetAnimalCount(AnimalTypes _animalType, BreedingTypes _breedingType)
		{
			if (!Singleton<AnimalManager>.IsInstance())
			{
				return -1;
			}
			Dictionary<AnimalTypes, Dictionary<BreedingTypes, int>> animalCountTable = Singleton<AnimalManager>.Instance.AnimalCountTable;
			if (!animalCountTable.ContainsKey(_animalType))
			{
				animalCountTable[_animalType] = new Dictionary<BreedingTypes, int>();
			}
			int result = 0;
			if (!animalCountTable[_animalType].TryGetValue(_breedingType, out result))
			{
				int num = 0;
				animalCountTable[_animalType][_breedingType] = num;
				return num;
			}
			return result;
		}

		// Token: 0x06003C0B RID: 15371 RVA: 0x0015F67C File Offset: 0x0015DA7C
		public static bool AnimalCountUp(AnimalTypes _animalType, BreedingTypes _breedingType)
		{
			if (!Singleton<AnimalManager>.IsInstance())
			{
				return false;
			}
			Dictionary<AnimalTypes, Dictionary<BreedingTypes, int>> animalCountTable = Singleton<AnimalManager>.Instance.AnimalCountTable;
			if (!animalCountTable.ContainsKey(_animalType))
			{
				animalCountTable[_animalType] = new Dictionary<BreedingTypes, int>();
			}
			int num = 0;
			animalCountTable[_animalType].TryGetValue(_breedingType, out num);
			animalCountTable[_animalType][_breedingType] = num + 1;
			return true;
		}

		// Token: 0x06003C0C RID: 15372 RVA: 0x0015F6DB File Offset: 0x0015DADB
		public static int GetAnimalCount(AnimalBase _animal)
		{
			if (_animal == null)
			{
				return -1;
			}
			return AnimalManager.GetAnimalCount(_animal.AnimalType, _animal.BreedingType);
		}

		// Token: 0x06003C0D RID: 15373 RVA: 0x0015F6FC File Offset: 0x0015DAFC
		public static bool AnimalCountUp(AnimalBase _animal)
		{
			return !(_animal == null) && AnimalManager.AnimalCountUp(_animal.AnimalType, _animal.BreedingType);
		}

		// Token: 0x06003C0E RID: 15374 RVA: 0x0015F71D File Offset: 0x0015DB1D
		public bool ContainsID(int _id)
		{
			return this.animalKeyList.Contains(_id) || this.AnimalTable.ContainsKey(_id);
		}

		// Token: 0x06003C0F RID: 15375 RVA: 0x0015F73F File Offset: 0x0015DB3F
		public bool CheckAnimalType(AnimalBase _animal, AnimalTypes _animalType, BreedingTypes _breedingType)
		{
			return !(_animal == null) && (_animal.AnimalType & _animalType) != (AnimalTypes)0 && _animal.BreedingType == _breedingType;
		}

		// Token: 0x06003C10 RID: 15376 RVA: 0x0015F768 File Offset: 0x0015DB68
		public bool AddAnimal(AnimalBase _animal)
		{
			if (_animal == null)
			{
				return false;
			}
			while (this.ContainsID(this.AnimalCount))
			{
				this.AnimalCount++;
			}
			_animal.SetID(this.AnimalCount, 0);
			this.AddAnimalTable(_animal);
			this.AnimalCount++;
			return true;
		}

		// Token: 0x06003C11 RID: 15377 RVA: 0x0015F7CC File Offset: 0x0015DBCC
		public bool RemoveAnimal(int _id, bool _destroy = true)
		{
			AnimalBase animalBase;
			if (!this.animalTable.TryGetValue(_id, out animalBase))
			{
				return false;
			}
			this.RemoveAnimalTable(animalBase);
			if (_destroy)
			{
				UnityEngine.Object.Destroy(animalBase.gameObject);
			}
			return true;
		}

		// Token: 0x06003C12 RID: 15378 RVA: 0x0015F808 File Offset: 0x0015DC08
		public bool RemoveAnimal(AnimalBase _animal, bool _destroy = true)
		{
			return !(_animal == null) && this.RemoveAnimal(_animal.AnimalID, _destroy);
		}

		// Token: 0x06003C13 RID: 15379 RVA: 0x0015F828 File Offset: 0x0015DC28
		private void AddList<T1, T2>(T1 _obj, List<T2> _list) where T1 : UnityEngine.Object where T2 : UnityEngine.Object
		{
			if (_obj == null || !(_obj is T2))
			{
				return;
			}
			T2 item = _obj as T2;
			if (!_list.Contains(item))
			{
				_list.Add(item);
			}
		}

		// Token: 0x06003C14 RID: 15380 RVA: 0x0015F87C File Offset: 0x0015DC7C
		private void AddList<T>(AnimalBase _animal, AnimalTypes _animalType, List<T> _list) where T : AnimalBase
		{
			if (_animal == null || !(_animal is T))
			{
				return;
			}
			if ((_animal.AnimalType & _animalType) != (AnimalTypes)0 && !_list.Contains(_animal))
			{
				_list.Add(_animal as T);
			}
		}

		// Token: 0x06003C15 RID: 15381 RVA: 0x0015F8CC File Offset: 0x0015DCCC
		private void AddList<T>(AnimalBase _animal, BreedingTypes _breedingType, List<T> _list) where T : AnimalBase
		{
			if (_animal == null || !(_animal is T))
			{
				return;
			}
			if (_animal.BreedingType == _breedingType && !_list.Contains(_animal))
			{
				_list.Add(_animal as T);
			}
		}

		// Token: 0x06003C16 RID: 15382 RVA: 0x0015F91C File Offset: 0x0015DD1C
		private void AddList<T>(AnimalBase _animal, AnimalTypes _animalType, BreedingTypes _breedingType, List<T> _list) where T : AnimalBase
		{
			if (_animal == null || !(_animal is T))
			{
				return;
			}
			if (this.CheckAnimalType(_animal, _animalType, _breedingType) && !_list.Contains(_animal))
			{
				_list.Add(_animal as T);
			}
		}

		// Token: 0x06003C17 RID: 15383 RVA: 0x0015F96E File Offset: 0x0015DD6E
		private void RemoveList<T>(AnimalBase _animal, List<T> _list) where T : AnimalBase
		{
			if (_animal == null || !(_animal is T))
			{
				return;
			}
			if (_list.Contains(_animal))
			{
				_list.Remove(_animal as T);
			}
		}

		// Token: 0x06003C18 RID: 15384 RVA: 0x0015F9A8 File Offset: 0x0015DDA8
		private void AddTable<T1, T2>(T1 _obj, int _id, Dictionary<int, T2> _table, List<int> _keys) where T1 : UnityEngine.Object where T2 : UnityEngine.Object
		{
			if (!(_obj is T2))
			{
				return;
			}
			T2 value = _obj as T2;
			if (!_table.ContainsKey(_id))
			{
				_table[_id] = value;
				if (!_keys.Contains(_id))
				{
					_keys.Add(_id);
				}
			}
		}

		// Token: 0x06003C19 RID: 15385 RVA: 0x0015FA00 File Offset: 0x0015DE00
		private void RemoveTable<T1, T2>(T1 _obj, int _id, Dictionary<int, T2> _table, List<int> _keys) where T1 : UnityEngine.Object where T2 : UnityEngine.Object
		{
			if (!(_obj is T2))
			{
				return;
			}
			T2 t = _obj as T2;
			T2 t2;
			if (_table.TryGetValue(_id, out t2) && t2 == t)
			{
				_table.Remove(_id);
				if (_keys.Contains(_id))
				{
					_keys.Remove(_id);
				}
			}
		}

		// Token: 0x06003C1A RID: 15386 RVA: 0x0015FA74 File Offset: 0x0015DE74
		public void AddTargetAnimals(AgentActor _agent)
		{
			if (_agent == null)
			{
				return;
			}
			_agent.TargetAnimals.Clear();
			if (this.Animals.IsNullOrEmpty<AnimalBase>())
			{
				return;
			}
			foreach (AnimalBase animalBase in this.Animals)
			{
				if (!(animalBase == null) && animalBase.AgentInsight)
				{
					_agent.AddAnimal(animalBase);
				}
			}
			this.Animals.RemoveAll((AnimalBase x) => x == null || x.gameObject == null);
		}

		// Token: 0x06003C1B RID: 15387 RVA: 0x0015FB40 File Offset: 0x0015DF40
		private bool AddAnimalTable(AnimalBase _animal)
		{
			if (_animal == null)
			{
				return false;
			}
			if (this.ContainsID(_animal.GetAnimalInfo().AnimalID))
			{
				return false;
			}
			BreedingTypes breedingType = BreedingTypes.Pet;
			BreedingTypes breedingType2 = BreedingTypes.Wild;
			this.AddTable<AnimalBase, AnimalBase>(_animal, _animal.AnimalID, this.animalTable, this.animalKeyList);
			this.AddList<AnimalBase, AnimalBase>(_animal, this.Animals);
			this.AddList<AnimalBase>(_animal, breedingType2, this.WildAnimals);
			this.AddList<AnimalBase>(_animal, breedingType, this.PetAnimals);
			this.AddList<WildGround>(_animal, AnimalTypes.Cat, breedingType2, this.WildCats);
			this.AddList<WildGround>(_animal, AnimalTypes.Chicken, breedingType2, this.WildChickens);
			this.AddList<WildMecha>(_animal, AnimalTypes.Mecha, breedingType2, this.WildMechas);
			this.AddList<WildFrog>(_animal, AnimalTypes.Frog, breedingType2, this.WildFrogs);
			this.AddList<WildButterfly>(_animal, AnimalTypes.Butterfly, breedingType2, this.WildButterflies);
			this.AddList<WildBirdFlock>(_animal, AnimalTypes.BirdFlock, breedingType2, this.WildBirdFlocks);
			this.AddList<WalkingPetAnimal>(_animal, AnimalTypes.Cat, breedingType, this.PetCats);
			this.AddList<PetChicken>(_animal, AnimalTypes.Chicken, breedingType, this.PetChickens);
			this.AddList<PetFish>(_animal, AnimalTypes.Fish, breedingType, this.PetFishes);
			this.AddList<FlyingPetAnimal>(_animal, AnimalTypes.Butterfly, breedingType, this.PetButterflies);
			this.AddList<WalkingPetAnimal>(_animal, AnimalTypes.Mecha, breedingType, this.PetMechas);
			this.AddList<MovingPetAnimal>(_animal, AnimalTypes.Cat | AnimalTypes.Chicken | AnimalTypes.Butterfly | AnimalTypes.Mecha | AnimalTypes.BirdFlock | AnimalTypes.CatTank | AnimalTypes.Chick | AnimalTypes.Fairy | AnimalTypes.DarkSpirit, breedingType, this.MovingPets);
			this.AddList<WalkingPetAnimal>(_animal, AnimalTypes.Ground, breedingType, this.WalkingPets);
			this.AddList<FlyingPetAnimal>(_animal, AnimalTypes.Flying, breedingType, this.FlyingPets);
			int animalTypeID = _animal.AnimalTypeID;
			BreedingTypes breedingType3 = _animal.BreedingType;
			bool flag = animalTypeID == 0 && breedingType3 == BreedingTypes.Pet;
			if (this.ActiveMapScene && flag)
			{
				_animal.UpdateAsObservable().SkipWhile((Unit _) => !_animal.Active).Take(1).Subscribe(delegate(Unit _)
				{
					if (Singleton<MapUIContainer>.IsInstance() && Singleton<AnimalManager>.IsInstance())
					{
						MiniMapControler minimapUI = Singleton<MapUIContainer>.Instance.MinimapUI;
						if (minimapUI != null)
						{
							minimapUI.PetIconSet();
						}
					}
				});
			}
			if (Singleton<Map>.IsInstance())
			{
				Map instance = Singleton<Map>.Instance;
				if (instance != null && instance.AgentTable != null && _animal.AgentInsight)
				{
					foreach (KeyValuePair<int, AgentActor> keyValuePair in instance.AgentTable)
					{
						AgentActor value = keyValuePair.Value;
						if (!(value == null))
						{
							value.AddAnimal(_animal);
						}
					}
				}
			}
			this.AddCommandableObject(_animal);
			return true;
		}

		// Token: 0x06003C1C RID: 15388 RVA: 0x0015FE54 File Offset: 0x0015E254
		private bool RemoveAnimalTable(AnimalBase _animal)
		{
			if (_animal == null)
			{
				return false;
			}
			if (!this.ContainsID(_animal.GetAnimalInfo().AnimalID))
			{
				return false;
			}
			this.RemoveTable<AnimalBase, AnimalBase>(_animal, _animal.AnimalID, this.animalTable, this.animalKeyList);
			this.RemoveList<AnimalBase>(_animal, this.Animals);
			this.RemoveList<AnimalBase>(_animal, this.WildAnimals);
			this.RemoveList<AnimalBase>(_animal, this.PetAnimals);
			this.RemoveList<WildGround>(_animal, this.WildCats);
			this.RemoveList<WildGround>(_animal, this.WildChickens);
			this.RemoveList<WildMecha>(_animal, this.WildMechas);
			this.RemoveList<WildFrog>(_animal, this.WildFrogs);
			this.RemoveList<WildButterfly>(_animal, this.WildButterflies);
			this.RemoveList<WildBirdFlock>(_animal, this.WildBirdFlocks);
			this.RemoveList<WalkingPetAnimal>(_animal, this.PetCats);
			this.RemoveList<PetChicken>(_animal, this.PetChickens);
			this.RemoveList<PetFish>(_animal, this.PetFishes);
			this.RemoveList<FlyingPetAnimal>(_animal, this.PetButterflies);
			this.RemoveList<WalkingPetAnimal>(_animal, this.PetMechas);
			this.RemoveList<MovingPetAnimal>(_animal, this.MovingPets);
			this.RemoveList<WalkingPetAnimal>(_animal, this.WalkingPets);
			this.RemoveList<FlyingPetAnimal>(_animal, this.FlyingPets);
			if (Singleton<MapUIContainer>.IsInstance() && Singleton<AnimalManager>.IsInstance())
			{
				int animalTypeID = _animal.AnimalTypeID;
				BreedingTypes breedingType = _animal.BreedingType;
				bool flag = animalTypeID == 0 && breedingType == BreedingTypes.Pet;
				if (flag)
				{
					MiniMapControler minimapUI = Singleton<MapUIContainer>.Instance.MinimapUI;
					if (minimapUI != null)
					{
						minimapUI.PetIconSet();
					}
				}
			}
			if (Singleton<Map>.IsInstance())
			{
				Map instance = Singleton<Map>.Instance;
				if (instance != null && instance.AgentTable != null && _animal.AgentInsight)
				{
					foreach (KeyValuePair<int, AgentActor> keyValuePair in instance.AgentTable)
					{
						AgentActor value = keyValuePair.Value;
						if (!(value == null))
						{
							value.RemoveAnimal(_animal);
						}
					}
				}
			}
			this.RemoveCommandableObject(_animal);
			return true;
		}

		// Token: 0x06003C1D RID: 15389 RVA: 0x00160084 File Offset: 0x0015E484
		public GameObject GetAnimalPrefab(int _animalTypeID, int _breedingTypeID)
		{
			GameObject gameObject = null;
			Dictionary<int, GameObject> dictionary;
			if (this.AnimalBaseObjectTable.TryGetValue(_animalTypeID, out dictionary) && dictionary != null)
			{
				if (dictionary.TryGetValue(_breedingTypeID, out gameObject))
				{
					return gameObject;
				}
			}
			else
			{
				Dictionary<int, GameObject> dictionary2 = new Dictionary<int, GameObject>();
				this.AnimalBaseObjectTable[_animalTypeID] = dictionary2;
				dictionary = dictionary2;
			}
			Resources instance = Singleton<Resources>.Instance;
			Dictionary<int, Dictionary<int, AssetBundleInfo>> animalBaseObjInfoTable = instance.AnimalTable.AnimalBaseObjInfoTable;
			Dictionary<int, AssetBundleInfo> dictionary3;
			if (!animalBaseObjInfoTable.TryGetValue(_animalTypeID, out dictionary3) || dictionary3.IsNullOrEmpty<int, AssetBundleInfo>())
			{
				GameObject gameObject2 = null;
				dictionary[_breedingTypeID] = gameObject2;
				return gameObject2;
			}
			AssetBundleInfo assetBundleInfo;
			if (!dictionary3.TryGetValue(_breedingTypeID, out assetBundleInfo))
			{
				GameObject gameObject2 = null;
				dictionary[_breedingTypeID] = gameObject2;
				return gameObject2;
			}
			gameObject = CommonLib.LoadAsset<GameObject>(assetBundleInfo.assetbundle, assetBundleInfo.asset, false, assetBundleInfo.manifest);
			if (gameObject != null)
			{
				MapScene.AddAssetBundlePath(assetBundleInfo.assetbundle, assetBundleInfo.manifest);
			}
			return gameObject;
		}

		// Token: 0x06003C1E RID: 15390 RVA: 0x00160170 File Offset: 0x0015E570
		public T Create<T>(int _animalTypeID, int _breedingTypeID) where T : AnimalBase
		{
			if (!Singleton<Resources>.IsInstance() || !Singleton<Map>.IsInstance())
			{
				return (T)((object)null);
			}
			Resources instance = Singleton<Resources>.Instance;
			GameObject animalPrefab = this.GetAnimalPrefab(_animalTypeID, _breedingTypeID);
			if (animalPrefab == null)
			{
				return (T)((object)null);
			}
			Dictionary<int, AnimalModelInfo> source;
			if (!instance.AnimalTable.ModelInfoTable.TryGetValue(_animalTypeID, out source) || source.IsNullOrEmpty<int, AnimalModelInfo>())
			{
				return (T)((object)null);
			}
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(animalPrefab, Vector3.zero, Quaternion.identity, this.AnimalRoot);
			T _animal = gameObject.GetComponent<T>();
			if (_animal == null)
			{
				UnityEngine.Object.Destroy(gameObject);
				return (T)((object)null);
			}
			this.AddAnimal(_animal);
			_animal.OnDestroyAsObservable().Subscribe(delegate(Unit _)
			{
				this.RemoveAnimal(_animal, false);
			});
			int animalCount = AnimalManager.GetAnimalCount(_animal);
			AnimalManager.AnimalCountUp(_animal);
			_animal.ObjName = string.Format("{0}_{1}", animalPrefab.name, animalCount.ToString("00"));
			_animal.SetModelInfo(source.Rand<int, AnimalModelInfo>().Value);
			return _animal;
		}

		// Token: 0x06003C1F RID: 15391 RVA: 0x001602E4 File Offset: 0x0015E6E4
		public AnimalBase CreateBase(int _animalTypeID, int _breedingTypeID)
		{
			if (!Singleton<Resources>.IsInstance() || !Singleton<Map>.IsInstance())
			{
				return null;
			}
			Resources instance = Singleton<Resources>.Instance;
			GameObject animalPrefab = this.GetAnimalPrefab(_animalTypeID, _breedingTypeID);
			if (animalPrefab == null)
			{
				return null;
			}
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(animalPrefab, Vector3.zero, Quaternion.identity, this.AnimalRoot);
			AnimalBase _animal = gameObject.GetComponent<AnimalBase>();
			if (_animal == null)
			{
				UnityEngine.Object.Destroy(gameObject);
				return null;
			}
			this.AddAnimal(_animal);
			_animal.OnDestroyAsObservable().Subscribe(delegate(Unit _)
			{
				this.RemoveAnimal(_animal, false);
			});
			int animalCount = AnimalManager.GetAnimalCount(_animal);
			AnimalManager.AnimalCountUp(_animal);
			_animal.ObjName = string.Format("{0}_{1}", animalPrefab.name, animalCount.ToString("00"));
			return _animal;
		}

		// Token: 0x06003C20 RID: 15392 RVA: 0x001603E0 File Offset: 0x0015E7E0
		public bool CheckAvailablePoint(Vector3 _position, bool _checkDistance, bool _checkVisible)
		{
			PlayerActor player = Map.GetPlayer();
			Camera cameraComponent = Map.GetCameraComponent(player);
			bool flag = false;
			bool flag2 = false;
			Resources resources = (!Singleton<Resources>.IsInstance()) ? null : Singleton<Resources>.Instance;
			if (_checkDistance && player != null)
			{
				flag = (Vector3.Distance(player.Position, _position) <= resources.AnimalDefinePack.SystemInfo.PopDistance);
			}
			if (_checkVisible && cameraComponent != null)
			{
				Vector3 vector = cameraComponent.WorldToViewportPoint(_position);
				if (0f <= vector.z)
				{
					Rect rect = new Rect(0f, 0f, 1f, 1f);
					float num = (!(resources != null)) ? 1f : resources.AnimalDefinePack.SystemInfo.ViewportPointScale;
					Vector3 b = new Vector3(0.5f, 0.5f, 0f);
					Vector3 scale = new Vector3(num, num, 1f);
					vector -= b;
					vector.Scale(scale);
					vector += b;
					flag2 = rect.Contains(vector);
				}
				else
				{
					flag2 = false;
				}
			}
			return !flag && !flag2;
		}

		// Token: 0x06003C21 RID: 15393 RVA: 0x00160520 File Offset: 0x0015E920
		public bool CheckAvailablePointPreferDistance(Vector3 _position)
		{
			AnimalDefinePack animalDefinePack = (!Singleton<Resources>.IsInstance()) ? null : Singleton<Resources>.Instance.AnimalDefinePack;
			if (animalDefinePack == null)
			{
				return false;
			}
			PlayerActor player = Map.GetPlayer();
			Camera cameraComponent = Map.GetCameraComponent(player);
			bool flag = false;
			bool flag2 = false;
			if (player != null)
			{
				flag = (Vector3.Distance(player.Position, _position) <= animalDefinePack.SystemInfo.PopDistance);
			}
			if (!flag)
			{
				return true;
			}
			if (cameraComponent != null)
			{
				Vector3 vector = cameraComponent.WorldToViewportPoint(_position);
				if (0f <= vector.z)
				{
					Rect rect = new Rect(0f, 0f, 1f, 1f);
					float viewportPointScale = animalDefinePack.SystemInfo.ViewportPointScale;
					Vector3 b = new Vector3(0.5f, 0.5f, 0f);
					Vector3 scale = new Vector3(viewportPointScale, viewportPointScale, 1f);
					vector -= b;
					vector.Scale(scale);
					vector += b;
					flag2 = rect.Contains(vector);
				}
				else
				{
					flag2 = false;
				}
			}
			return !flag && !flag2;
		}

		// Token: 0x06003C22 RID: 15394 RVA: 0x00160650 File Offset: 0x0015EA50
		public void SetupSaveDataWildAnimals()
		{
			WorldData worldData = (!Singleton<Game>.IsInstance()) ? null : Singleton<Game>.Instance.WorldData;
			if (worldData == null)
			{
				return;
			}
			Dictionary<AnimalTypes, Dictionary<int, WildAnimalData>> dictionary = worldData.WildAnimalTable;
			if (dictionary == null)
			{
				Dictionary<AnimalTypes, Dictionary<int, WildAnimalData>> dictionary2 = new Dictionary<AnimalTypes, Dictionary<int, WildAnimalData>>();
				worldData.WildAnimalTable = dictionary2;
				dictionary = dictionary2;
			}
			if (!this.WildChickenPopPoints.IsNullOrEmpty<GroundAnimalHabitatPoint>())
			{
				Dictionary<int, WildAnimalData> dictionary3;
				if (!dictionary.TryGetValue(AnimalTypes.Chicken, out dictionary3) || dictionary3 == null)
				{
					Dictionary<int, WildAnimalData> dictionary4 = new Dictionary<int, WildAnimalData>();
					dictionary[AnimalTypes.Chicken] = dictionary4;
					dictionary3 = dictionary4;
				}
				List<int> list = ListPool<int>.Get();
				foreach (GroundAnimalHabitatPoint groundAnimalHabitatPoint in this.WildChickenPopPoints)
				{
					if (!(groundAnimalHabitatPoint == null))
					{
						int id = groundAnimalHabitatPoint.ID;
						WildGround user = groundAnimalHabitatPoint.User;
						WildAnimalData wildAnimalData;
						if (!dictionary3.TryGetValue(id, out wildAnimalData) || wildAnimalData == null)
						{
							WildAnimalData wildAnimalData2 = new WildAnimalData();
							dictionary3[id] = wildAnimalData2;
							wildAnimalData = wildAnimalData2;
						}
						wildAnimalData.CoolTime = ((!(user != null)) ? groundAnimalHabitatPoint.CoolTimeCounter : 0f);
						wildAnimalData.IsAdded = (user != null);
						if (!list.Contains(id))
						{
							list.Add(id);
						}
					}
				}
				if (!dictionary3.IsNullOrEmpty<int, WildAnimalData>())
				{
					List<int> list2 = ListPool<int>.Get();
					foreach (KeyValuePair<int, WildAnimalData> keyValuePair in dictionary3)
					{
						if (!list.Contains(keyValuePair.Key))
						{
							list2.Add(keyValuePair.Key);
						}
					}
					foreach (int key in list2)
					{
						dictionary3.Remove(key);
					}
					ListPool<int>.Release(list2);
				}
				ListPool<int>.Release(list);
			}
			if (!this.WildCatPopPoints.IsNullOrEmpty<GroundAnimalHabitatPoint>())
			{
				Dictionary<int, WildAnimalData> dictionary5;
				if (!dictionary.TryGetValue(AnimalTypes.Cat, out dictionary5) || dictionary5 == null)
				{
					Dictionary<int, WildAnimalData> dictionary4 = new Dictionary<int, WildAnimalData>();
					dictionary[AnimalTypes.Cat] = dictionary4;
					dictionary5 = dictionary4;
				}
				List<int> list3 = ListPool<int>.Get();
				foreach (GroundAnimalHabitatPoint groundAnimalHabitatPoint2 in this.WildCatPopPoints)
				{
					if (!(groundAnimalHabitatPoint2 == null))
					{
						int id2 = groundAnimalHabitatPoint2.ID;
						WildGround user2 = groundAnimalHabitatPoint2.User;
						WildAnimalData wildAnimalData3;
						if (!dictionary5.TryGetValue(id2, out wildAnimalData3) || wildAnimalData3 == null)
						{
							WildAnimalData wildAnimalData2 = new WildAnimalData();
							dictionary5[id2] = wildAnimalData2;
							wildAnimalData3 = wildAnimalData2;
						}
						wildAnimalData3.CoolTime = ((!(user2 != null)) ? groundAnimalHabitatPoint2.CoolTimeCounter : 0f);
						wildAnimalData3.IsAdded = (user2 != null);
						if (!list3.Contains(id2))
						{
							list3.Add(id2);
						}
					}
				}
				if (!dictionary5.IsNullOrEmpty<int, WildAnimalData>())
				{
					List<int> list4 = ListPool<int>.Get();
					foreach (KeyValuePair<int, WildAnimalData> keyValuePair2 in dictionary5)
					{
						if (!list3.Contains(keyValuePair2.Key))
						{
							list4.Add(keyValuePair2.Key);
						}
					}
					foreach (int key2 in list4)
					{
						dictionary5.Remove(key2);
					}
					ListPool<int>.Release(list4);
				}
				ListPool<int>.Release(list3);
			}
			if (!this.WildCatAndChickenPopPoints.IsNullOrEmpty<GroundAnimalHabitatPoint>())
			{
				AnimalTypes key3 = AnimalTypes.Cat | AnimalTypes.Chicken;
				Dictionary<int, WildAnimalData> dictionary6;
				if (!dictionary.TryGetValue(key3, out dictionary6) || dictionary6 == null)
				{
					Dictionary<int, WildAnimalData> dictionary4 = new Dictionary<int, WildAnimalData>();
					dictionary[key3] = dictionary4;
					dictionary6 = dictionary4;
				}
				List<int> list5 = ListPool<int>.Get();
				foreach (GroundAnimalHabitatPoint groundAnimalHabitatPoint3 in this.WildCatPopPoints)
				{
					if (!(groundAnimalHabitatPoint3 == null))
					{
						int id3 = groundAnimalHabitatPoint3.ID;
						WildGround user3 = groundAnimalHabitatPoint3.User;
						WildAnimalData wildAnimalData4;
						if (!dictionary6.TryGetValue(id3, out wildAnimalData4) || wildAnimalData4 == null)
						{
							WildAnimalData wildAnimalData2 = new WildAnimalData();
							dictionary6[id3] = wildAnimalData2;
							wildAnimalData4 = wildAnimalData2;
						}
						wildAnimalData4.CoolTime = ((!(user3 != null)) ? groundAnimalHabitatPoint3.CoolTimeCounter : 0f);
						wildAnimalData4.IsAdded = (user3 != null);
						if (!list5.Contains(id3))
						{
							list5.Add(id3);
						}
					}
				}
				if (!dictionary6.IsNullOrEmpty<int, WildAnimalData>())
				{
					List<int> list6 = ListPool<int>.Get();
					foreach (KeyValuePair<int, WildAnimalData> keyValuePair3 in dictionary6)
					{
						if (!list5.Contains(keyValuePair3.Key))
						{
							list6.Add(keyValuePair3.Key);
						}
					}
					foreach (int key4 in list6)
					{
						dictionary6.Remove(key4);
					}
					ListPool<int>.Release(list6);
				}
				ListPool<int>.Release(list5);
			}
			if (!this.FrogHabitatPoints.IsNullOrEmpty<FrogHabitatPoint>())
			{
				Dictionary<int, WildAnimalData> dictionary7;
				if (!dictionary.TryGetValue(AnimalTypes.Frog, out dictionary7) || dictionary7 == null)
				{
					Dictionary<int, WildAnimalData> dictionary4 = new Dictionary<int, WildAnimalData>();
					dictionary[AnimalTypes.Frog] = dictionary4;
					dictionary7 = dictionary4;
				}
				List<int> list7 = ListPool<int>.Get();
				foreach (FrogHabitatPoint frogHabitatPoint in this.FrogHabitatPoints)
				{
					if (!(frogHabitatPoint == null))
					{
						int id4 = frogHabitatPoint.ID;
						WildFrog user4 = frogHabitatPoint.User;
						WildAnimalData wildAnimalData5;
						if (!dictionary7.TryGetValue(id4, out wildAnimalData5) || wildAnimalData5 == null)
						{
							WildAnimalData wildAnimalData2 = new WildAnimalData();
							dictionary7[id4] = wildAnimalData2;
							wildAnimalData5 = wildAnimalData2;
						}
						wildAnimalData5.CoolTime = ((!(user4 != null)) ? frogHabitatPoint.CoolTimeCounter : 0f);
						wildAnimalData5.IsAdded = (user4 != null);
						if (!list7.Contains(id4))
						{
							list7.Add(id4);
						}
					}
				}
				if (!dictionary7.IsNullOrEmpty<int, WildAnimalData>())
				{
					List<int> list8 = ListPool<int>.Get();
					foreach (KeyValuePair<int, WildAnimalData> keyValuePair4 in dictionary7)
					{
						if (!list7.Contains(keyValuePair4.Key))
						{
							list8.Add(keyValuePair4.Key);
						}
					}
					foreach (int key5 in list8)
					{
						dictionary7.Remove(key5);
					}
					ListPool<int>.Release(list8);
				}
				ListPool<int>.Release(list7);
			}
			if (!this.MechaHabitatPoints.IsNullOrEmpty<MechaHabitatPoint>())
			{
				Dictionary<int, WildAnimalData> dictionary8;
				if (!dictionary.TryGetValue(AnimalTypes.Mecha, out dictionary8) || dictionary8 == null)
				{
					Dictionary<int, WildAnimalData> dictionary4 = new Dictionary<int, WildAnimalData>();
					dictionary[AnimalTypes.Mecha] = dictionary4;
					dictionary8 = dictionary4;
				}
				List<int> list9 = ListPool<int>.Get();
				foreach (MechaHabitatPoint mechaHabitatPoint in this.MechaHabitatPoints)
				{
					if (!(mechaHabitatPoint == null))
					{
						int id5 = mechaHabitatPoint.ID;
						WildMecha user5 = mechaHabitatPoint.User;
						WildAnimalData wildAnimalData6;
						if (!dictionary8.TryGetValue(id5, out wildAnimalData6) || wildAnimalData6 == null)
						{
							WildAnimalData wildAnimalData2 = new WildAnimalData();
							dictionary8[id5] = wildAnimalData2;
							wildAnimalData6 = wildAnimalData2;
						}
						wildAnimalData6.CoolTime = ((!(user5 != null)) ? mechaHabitatPoint.CoolTimeCounter : 0f);
						wildAnimalData6.IsAdded = (user5 != null);
						if (!list9.Contains(id5))
						{
							list9.Add(id5);
						}
					}
				}
				if (!dictionary8.IsNullOrEmpty<int, WildAnimalData>())
				{
					List<int> list10 = ListPool<int>.Get();
					foreach (KeyValuePair<int, WildAnimalData> keyValuePair5 in dictionary8)
					{
						if (!list9.Contains(keyValuePair5.Key))
						{
							list10.Add(keyValuePair5.Key);
						}
					}
					foreach (int key6 in list10)
					{
						dictionary8.Remove(key6);
					}
					ListPool<int>.Release(list10);
				}
				ListPool<int>.Release(list9);
			}
		}

		// Token: 0x04003A63 RID: 14947
		private Transform _animalRoot;

		// Token: 0x04003A76 RID: 14966
		private Dictionary<int, AnimalBase> animalTable = new Dictionary<int, AnimalBase>();

		// Token: 0x04003A77 RID: 14967
		private List<int> animalKeyList = new List<int>();

		// Token: 0x04003A86 RID: 14982
		private static Subject<Unit> _commandRefreshEvent;

		// Token: 0x04003A87 RID: 14983
		private CompositeDisposable enviromentSubscribeDisposable;

		// Token: 0x04003A88 RID: 14984
		private IDisposable mapAreaCheckDisposable;

		// Token: 0x020008DC RID: 2268
		public enum AnimalType
		{
			// Token: 0x04003A92 RID: 14994
			Cat,
			// Token: 0x04003A93 RID: 14995
			Chicken,
			// Token: 0x04003A94 RID: 14996
			Fish,
			// Token: 0x04003A95 RID: 14997
			Butterfly,
			// Token: 0x04003A96 RID: 14998
			Mecha,
			// Token: 0x04003A97 RID: 14999
			Frog,
			// Token: 0x04003A98 RID: 15000
			BirdFlock,
			// Token: 0x04003A99 RID: 15001
			CatWithFish,
			// Token: 0x04003A9A RID: 15002
			CatTank,
			// Token: 0x04003A9B RID: 15003
			Chick,
			// Token: 0x04003A9C RID: 15004
			Fairy,
			// Token: 0x04003A9D RID: 15005
			DarkSpirit
		}
	}
}
