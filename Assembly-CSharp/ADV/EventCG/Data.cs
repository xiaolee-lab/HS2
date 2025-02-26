using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using AIChara;
using Cinemachine;
using Illusion.Extensions;
using Illusion.Game.Elements.EasyLoader;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace ADV.EventCG
{
	// Token: 0x020006AD RID: 1709
	public class Data : MonoBehaviour
	{
		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x0600286D RID: 10349 RVA: 0x000EF85A File Offset: 0x000EDC5A
		// (set) Token: 0x0600286E RID: 10350 RVA: 0x000EF864 File Offset: 0x000EDC64
		public Transform camRoot
		{
			get
			{
				return this._camRoot;
			}
			set
			{
				if (base.transform.childCount == 0)
				{
					return;
				}
				Transform child = base.transform.GetChild(0);
				if (child.name != "camPos")
				{
					return;
				}
				this._camRoot = value;
				this.backupDic[this._camRoot] = Tuple.Create<Vector3, Quaternion>(this._camRoot.position, this._camRoot.rotation);
				this.cameraData = child.GetComponent<CameraData>();
				this.cameraData.enabled = true;
				CinemachineVirtualCamera component = this._camRoot.GetComponent<CinemachineVirtualCamera>();
				if (component != null)
				{
					CinemachineVirtualCamera cinemachineVirtualCamera = child.gameObject.AddComponent<CinemachineVirtualCamera>();
					cinemachineVirtualCamera.Priority = component.Priority + 1;
					cinemachineVirtualCamera.m_Lens.FieldOfView = this.cameraData.fieldOfView;
					cinemachineVirtualCamera.LookAt = null;
					this.cameraData.SetCameraData(component);
					component.m_Lens.FieldOfView = this.cameraData.fieldOfView;
					component.LookAt = null;
				}
				else
				{
					Camera component2 = this._camRoot.GetComponent<Camera>();
					this.cameraData.SetCameraData(component2);
					component2.fieldOfView = this.cameraData.fieldOfView;
					this._camRoot.SetPositionAndRotation(child.position, child.rotation);
					(from _ in child.UpdateAsObservable().TakeUntilDestroy(this._camRoot)
					where this.cameraData.initialized
					select _).Take(1).Subscribe(delegate(Unit _)
					{
						this._camRoot.SetPositionAndRotation(child.position, child.rotation);
					});
				}
			}
		}

		// Token: 0x0600286F RID: 10351 RVA: 0x000EFA17 File Offset: 0x000EDE17
		public static bool IsCharaPos(UnityEngine.Object child)
		{
			return child.name.IndexOf("chaPos") == 0;
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x06002870 RID: 10352 RVA: 0x000EFA2C File Offset: 0x000EDE2C
		// (set) Token: 0x06002871 RID: 10353 RVA: 0x000EFA34 File Offset: 0x000EDE34
		public Dictionary<int, List<GameObject>> itemList { get; private set; }

		// Token: 0x06002872 RID: 10354 RVA: 0x000EFA40 File Offset: 0x000EDE40
		public void SetChaRoot(Transform root, Dictionary<int, CharaData> charaDataDic)
		{
			this._chaRoot = root;
			this.itemList = new Dictionary<int, List<GameObject>>();
			List<Transform> list = base.transform.Children();
			IEnumerable<Transform> source = list;
			if (Data.<>f__mg$cache0 == null)
			{
				Data.<>f__mg$cache0 = new Func<Transform, bool>(Data.IsCharaPos);
			}
			Dictionary<int, Transform> dictionary = source.Where(Data.<>f__mg$cache0).Select((Transform t, int i) => new
			{
				t,
				i
			}).ToDictionary(v => v.i, v => v.t);
			Transform transform = list.Find((Transform p) => p.name == "playerPos");
			int num = -1;
			if (transform != null)
			{
				dictionary[num] = transform;
			}
			foreach (KeyValuePair<int, CharaData> keyValuePair in charaDataDic)
			{
				int key = (!keyValuePair.Value.data.isHeroine) ? num : keyValuePair.Key;
				Transform transform2;
				if (dictionary.TryGetValue(key, out transform2))
				{
					keyValuePair.Value.backup.Set();
					Transform transform3 = keyValuePair.Value.backup.transform;
					this.backupDic[transform3] = Tuple.Create<Vector3, Quaternion>(transform3.position, transform3.rotation);
					transform3.SetPositionAndRotation(transform2.position, transform2.rotation);
					this.itemList.Add(key, new List<GameObject>());
				}
			}
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x06002873 RID: 10355 RVA: 0x000EFC14 File Offset: 0x000EE014
		public Transform chaRoot
		{
			get
			{
				return this._chaRoot;
			}
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06002874 RID: 10356 RVA: 0x000EFC1C File Offset: 0x000EE01C
		public Camera targetCamera
		{
			get
			{
				return this.GetCacheObject(ref this._targetCamera, new Func<Camera>(base.GetComponentInChildren<Camera>));
			}
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06002875 RID: 10357 RVA: 0x000EFC36 File Offset: 0x000EE036
		// (set) Token: 0x06002876 RID: 10358 RVA: 0x000EFC3E File Offset: 0x000EE03E
		public CameraData cameraData { get; private set; }

		// Token: 0x06002877 RID: 10359 RVA: 0x000EFC48 File Offset: 0x000EE048
		public void Restore()
		{
			foreach (KeyValuePair<Transform, Tuple<Vector3, Quaternion>> keyValuePair in this.backupDic)
			{
				if (!(keyValuePair.Key == null))
				{
					if (keyValuePair.Key == this._camRoot && this.cameraData != null)
					{
						this.cameraData.RepairCameraData(this._camRoot);
					}
					keyValuePair.Key.SetPositionAndRotation(keyValuePair.Value.Item1, keyValuePair.Value.Item2);
				}
			}
		}

		// Token: 0x06002878 RID: 10360 RVA: 0x000EFD14 File Offset: 0x000EE114
		public void ItemClear()
		{
			foreach (List<GameObject> list in this.itemList.Values)
			{
				list.ForEach(delegate(GameObject item)
				{
					UnityEngine.Object.Destroy(item);
				});
				list.Clear();
			}
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06002879 RID: 10361 RVA: 0x000EFD98 File Offset: 0x000EE198
		public List<Transform> GetCharaPosChildren
		{
			get
			{
				IEnumerable<Transform> source = base.transform.Children();
				if (Data.<>f__mg$cache1 == null)
				{
					Data.<>f__mg$cache1 = new Func<Transform, bool>(Data.IsCharaPos);
				}
				return source.Where(Data.<>f__mg$cache1).ToList<Transform>();
			}
		}

		// Token: 0x0600287A RID: 10362 RVA: 0x000EFDCC File Offset: 0x000EE1CC
		public void Next(int index, Dictionary<int, CharaData> charaDataDic)
		{
			Data.Scene scene = this.scenes.SafeGet(index);
			if (scene == null)
			{
				return;
			}
			Data.Scene scene2 = this.scenes.SafeGet(index - 1);
			foreach (KeyValuePair<int, CharaData> keyValuePair in charaDataDic)
			{
				Data.Scene.Chara chara = scene.FindGet(keyValuePair.Key);
				if (chara != null)
				{
					Data.Scene.Chara chara2 = null;
					MotionIK motionIK = null;
					YureCtrlEx yureCtrl = null;
					bool flag = false;
					Data.Scene.Chara.MotionAndItem motionAndItem = chara.motionAndItem;
					if (motionAndItem.ik.bundle.IsNullOrEmpty() && scene2 != null)
					{
						chara2 = scene2.FindGet(keyValuePair.Key);
						flag = true;
						if (chara2 != null)
						{
							motionIK = chara2.motionAndItem.ik.motionIK;
						}
					}
					if (motionAndItem.yure.bundle.IsNullOrEmpty())
					{
						if (!flag && scene2 != null)
						{
							chara2 = scene2.FindGet(keyValuePair.Key);
						}
						if (chara2 != null)
						{
							yureCtrl = chara2.motionAndItem.yure.yureCtrl;
						}
					}
					motionAndItem.ik.Create(keyValuePair.Value.chaCtrl, motionIK, Array.Empty<MotionIK>());
					motionAndItem.yure.Create(keyValuePair.Value.chaCtrl, yureCtrl);
				}
			}
			foreach (KeyValuePair<int, CharaData> keyValuePair2 in charaDataDic)
			{
				Data.Scene.Chara chara3 = scene.FindGet(keyValuePair2.Key);
				if (chara3 != null)
				{
					chara3.Change(keyValuePair2.Value.chaCtrl, this.itemList[keyValuePair2.Key]);
				}
			}
		}

		// Token: 0x040029FF RID: 10751
		public const string ParentNameCamera = "camPos";

		// Token: 0x04002A00 RID: 10752
		public const string ParentNameChara = "chaPos";

		// Token: 0x04002A01 RID: 10753
		public const string ParentNamePlayer = "playerPos";

		// Token: 0x04002A02 RID: 10754
		private Transform _camRoot;

		// Token: 0x04002A03 RID: 10755
		public Data.Scene[] scenes;

		// Token: 0x04002A05 RID: 10757
		private Transform _chaRoot;

		// Token: 0x04002A06 RID: 10758
		private Camera _targetCamera;

		// Token: 0x04002A07 RID: 10759
		private Dictionary<Transform, Tuple<Vector3, Quaternion>> backupDic = new Dictionary<Transform, Tuple<Vector3, Quaternion>>();

		// Token: 0x04002A09 RID: 10761
		[CompilerGenerated]
		private static Func<Transform, bool> <>f__mg$cache0;

		// Token: 0x04002A0F RID: 10767
		[CompilerGenerated]
		private static Func<Transform, bool> <>f__mg$cache1;

		// Token: 0x020006AE RID: 1710
		[Serializable]
		public class Scene
		{
			// Token: 0x170005D8 RID: 1496
			public Data.Scene.Chara this[int index]
			{
				get
				{
					return this.charas.SafeGet(index);
				}
			}

			// Token: 0x06002882 RID: 10370 RVA: 0x000F0024 File Offset: 0x000EE424
			public Data.Scene.Chara FindGet(int no)
			{
				foreach (Data.Scene.Chara chara in this.charas)
				{
					if (chara.no == no)
					{
						return chara;
					}
				}
				return null;
			}

			// Token: 0x04002A10 RID: 10768
			public Data.Scene.Chara[] charas;

			// Token: 0x020006AF RID: 1711
			[Serializable]
			public class Chara
			{
				// Token: 0x06002884 RID: 10372 RVA: 0x000F0072 File Offset: 0x000EE472
				public void Change(ChaControl chaCtrl, List<GameObject> itemList)
				{
					this.motionAndItem.Change(chaCtrl, itemList);
				}

				// Token: 0x04002A11 RID: 10769
				public int no;

				// Token: 0x04002A12 RID: 10770
				public Data.Scene.Chara.MotionAndItem motionAndItem = new Data.Scene.Chara.MotionAndItem();

				// Token: 0x020006B0 RID: 1712
				[Serializable]
				public class MotionAndItem
				{
					// Token: 0x06002886 RID: 10374 RVA: 0x000F00AC File Offset: 0x000EE4AC
					public void Change(ChaControl chaCtrl, List<GameObject> itemList)
					{
						Animator animBody = chaCtrl.animBody;
						if (this.motion.Setting(animBody))
						{
							this.motion.Play(animBody);
							this.ik.Setting(chaCtrl, this.motion.state);
							this.yure.Setting(chaCtrl, this.motion.state);
						}
						foreach (Data.Scene.Chara.MotionAndItem.ItemSet itemSet in this.items)
						{
							itemSet.Execute(chaCtrl, itemList);
						}
						foreach (Data.Scene.Chara.MotionAndItem.ItemRemove itemRemove in this.removes)
						{
							itemRemove.Execute(chaCtrl, itemList);
						}
					}

					// Token: 0x04002A13 RID: 10771
					public Illusion.Game.Elements.EasyLoader.Motion motion = new Illusion.Game.Elements.EasyLoader.Motion();

					// Token: 0x04002A14 RID: 10772
					public IKMotion ik = new IKMotion();

					// Token: 0x04002A15 RID: 10773
					public YureMotion yure = new YureMotion();

					// Token: 0x04002A16 RID: 10774
					public Data.Scene.Chara.MotionAndItem.ItemSet[] items;

					// Token: 0x04002A17 RID: 10775
					public Data.Scene.Chara.MotionAndItem.ItemRemove[] removes;

					// Token: 0x020006B1 RID: 1713
					[Serializable]
					public class ItemSet
					{
						// Token: 0x06002888 RID: 10376 RVA: 0x000F0178 File Offset: 0x000EE578
						public void Execute(ChaControl chaCtrl, List<GameObject> itemList)
						{
							GameObject gameObject = this.data.Load(chaCtrl);
							gameObject.name = this.name;
							itemList.Add(gameObject);
						}

						// Token: 0x04002A18 RID: 10776
						public string name;

						// Token: 0x04002A19 RID: 10777
						public Item.Data data = new Item.Data();
					}

					// Token: 0x020006B2 RID: 1714
					[Serializable]
					public class ItemRemove
					{
						// Token: 0x0600288A RID: 10378 RVA: 0x000F01B0 File Offset: 0x000EE5B0
						public bool Execute(ChaControl chaCtrl, List<GameObject> itemList)
						{
							int index = itemList.FindIndex((GameObject p) => p.name == this.name);
							GameObject gameObject = itemList.SafeGet(index);
							if (gameObject == null)
							{
								return false;
							}
							itemList.RemoveAt(index);
							UnityEngine.Object.Destroy(gameObject);
							return true;
						}

						// Token: 0x04002A1A RID: 10778
						public string name;
					}
				}
			}
		}
	}
}
