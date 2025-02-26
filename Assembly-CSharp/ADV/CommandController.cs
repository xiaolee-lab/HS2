using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Illusion.Elements.Reference;
using Illusion.Extensions;
using Manager;
using UnityEngine;

namespace ADV
{
	// Token: 0x020006CC RID: 1740
	public class CommandController : MonoBehaviour
	{
		// Token: 0x0600296F RID: 10607 RVA: 0x000F2EDC File Offset: 0x000F12DC
		public bool GetV3Dic(string arg, out Vector3 pos)
		{
			pos = Vector3.zero;
			float num;
			return !arg.IsNullOrEmpty() && !float.TryParse(arg, out num) && this.V3Dic.TryGetValue(arg, out pos);
		}

		// Token: 0x06002970 RID: 10608 RVA: 0x000F2F1C File Offset: 0x000F131C
		public CharaData GetChara(int no)
		{
			if (no < 0)
			{
				TextScenario.ParamData paramData = null;
				if (no == -1)
				{
					paramData = this.scenario.player;
				}
				else
				{
					paramData = this.scenario.heroineList[Mathf.Abs(no + 2)];
				}
				if (paramData != null)
				{
					foreach (KeyValuePair<int, CharaData> keyValuePair in this.Characters)
					{
						if (keyValuePair.Value.data == paramData)
						{
							return keyValuePair.Value;
						}
					}
					return new CharaData(paramData, this.scenario, null);
				}
			}
			CharaData charaData;
			return (!this.Characters.TryGetValue(no, out charaData)) ? this.scenario.currentChara : charaData;
		}

		// Token: 0x06002971 RID: 10609 RVA: 0x000F3008 File Offset: 0x000F1408
		public void AddChara(int no, CharaData data)
		{
			this.RemoveChara(no);
			this.Characters[no] = data;
		}

		// Token: 0x06002972 RID: 10610 RVA: 0x000F3020 File Offset: 0x000F1420
		public void RemoveChara(int no)
		{
			CharaData charaData;
			if (this.Characters.TryGetValue(no, out charaData))
			{
				charaData.Release();
				this.loadingCharaList.Remove(charaData);
			}
			this.Characters.Remove(no);
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x06002973 RID: 10611 RVA: 0x000F3060 File Offset: 0x000F1460
		private Root rootPosition
		{
			get
			{
				return this.GetCacheObject(ref this._rootPosition, delegate
				{
					this.rootPositionLoaded = true;
					return Root.Load(Singleton<Map>.Instance.MapRoot);
				});
			}
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x06002974 RID: 10612 RVA: 0x000F307A File Offset: 0x000F147A
		public Transform CharaRoot
		{
			[CompilerGenerated]
			get
			{
				return this.rootPosition.charaRoot;
			}
		}

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x06002975 RID: 10613 RVA: 0x000F3087 File Offset: 0x000F1487
		public Dictionary<string, Transform> characterStandNulls
		{
			[CompilerGenerated]
			get
			{
				return this.rootPosition.characterStandNulls;
			}
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x06002976 RID: 10614 RVA: 0x000F3094 File Offset: 0x000F1494
		public Transform EventCGRoot
		{
			[CompilerGenerated]
			get
			{
				return this.rootPosition.eventCGRoot;
			}
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x06002977 RID: 10615 RVA: 0x000F30A1 File Offset: 0x000F14A1
		public Transform ObjectRoot
		{
			[CompilerGenerated]
			get
			{
				return this.rootPosition.objectRoot;
			}
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x06002978 RID: 10616 RVA: 0x000F30AE File Offset: 0x000F14AE
		public Transform NullRoot
		{
			[CompilerGenerated]
			get
			{
				return this.rootPosition.nullRoot;
			}
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06002979 RID: 10617 RVA: 0x000F30BB File Offset: 0x000F14BB
		public Transform BaseRoot
		{
			[CompilerGenerated]
			get
			{
				return this.rootPosition.baseRoot;
			}
		}

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x0600297A RID: 10618 RVA: 0x000F30C8 File Offset: 0x000F14C8
		public Transform CameraRoot
		{
			[CompilerGenerated]
			get
			{
				return this.rootPosition.cameraRoot;
			}
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x0600297B RID: 10619 RVA: 0x000F30D5 File Offset: 0x000F14D5
		public CommandList NowCommandList
		{
			get
			{
				return this.nowCommandList;
			}
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x0600297C RID: 10620 RVA: 0x000F30DD File Offset: 0x000F14DD
		public CommandList BackGroundCommandList
		{
			get
			{
				return this.backGroundCommandList;
			}
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x0600297D RID: 10621 RVA: 0x000F30E5 File Offset: 0x000F14E5
		public List<CharaData> LoadingCharaList
		{
			[CompilerGenerated]
			get
			{
				return this.loadingCharaList;
			}
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x0600297E RID: 10622 RVA: 0x000F30ED File Offset: 0x000F14ED
		private List<CharaData> loadingCharaList { get; } = new List<CharaData>();

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x0600297F RID: 10623 RVA: 0x000F30F5 File Offset: 0x000F14F5
		public Dictionary<int, CharaData> Characters { get; } = new Dictionary<int, CharaData>();

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x06002980 RID: 10624 RVA: 0x000F30FD File Offset: 0x000F14FD
		public Dictionary<string, GameObject> Objects { get; } = new Dictionary<string, GameObject>();

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06002981 RID: 10625 RVA: 0x000F3105 File Offset: 0x000F1505
		public Dictionary<string, Vector3> V3Dic { get; } = new Dictionary<string, Vector3>();

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06002982 RID: 10626 RVA: 0x000F310D File Offset: 0x000F150D
		public Dictionary<string, Transform> NullDic { get; } = new Dictionary<string, Transform>();

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x06002983 RID: 10627 RVA: 0x000F3115 File Offset: 0x000F1515
		public Dictionary<string, Game.Expression> expDic { get; } = new Dictionary<string, Game.Expression>();

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x06002984 RID: 10628 RVA: 0x000F311D File Offset: 0x000F151D
		public Dictionary<string, string[]> motionDic { get; } = new Dictionary<string, string[]>();

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x06002985 RID: 10629 RVA: 0x000F3125 File Offset: 0x000F1525
		public CommandController.FontColor fontColor { get; } = new CommandController.FontColor();

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x06002986 RID: 10630 RVA: 0x000F312D File Offset: 0x000F152D
		// (set) Token: 0x06002987 RID: 10631 RVA: 0x000F3135 File Offset: 0x000F1535
		public bool useCorrectCamera { get; set; }

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06002988 RID: 10632 RVA: 0x000F313E File Offset: 0x000F153E
		// (set) Token: 0x06002989 RID: 10633 RVA: 0x000F3146 File Offset: 0x000F1546
		private TextScenario scenario { get; set; }

		// Token: 0x0600298A RID: 10634 RVA: 0x000F3150 File Offset: 0x000F1550
		public void Initialize()
		{
			this.useCorrectCameraBakup = this._useCorrectCamera;
			if (this.scenario == null)
			{
				this.scenario = base.GetComponent<TextScenario>();
				this.nowCommandList = new CommandList(this.scenario);
				this.backGroundCommandList = new CommandList(this.scenario);
			}
			else
			{
				this.nowCommandList.Clear();
				this.backGroundCommandList.Clear();
			}
			this.loadingCharaList.Clear();
			this.Objects.Clear();
			this.Characters.Clear();
			this.V3Dic.Clear();
			this.NullDic.Clear();
			this.expDic.Clear();
			this.motionDic.Clear();
			this.fontColor.Clear();
			this.rootPosition.SetBackup();
		}

		// Token: 0x0600298B RID: 10635 RVA: 0x000F3228 File Offset: 0x000F1628
		public void SetObject(GameObject go)
		{
			GameObject obj;
			if (this.Objects.TryGetValue(go.name, out obj))
			{
				UnityEngine.Object.Destroy(obj);
			}
			go.transform.SetParent(this.ObjectRoot, false);
			this.Objects[go.name] = go;
		}

		// Token: 0x0600298C RID: 10636 RVA: 0x000F3278 File Offset: 0x000F1678
		public void SetNull(Transform nullT)
		{
			Transform transform;
			if (this.NullDic.TryGetValue(nullT.name, out transform) && transform != null)
			{
				UnityEngine.Object.Destroy(transform.gameObject);
			}
			nullT.SetParent(this.NullRoot, false);
			this.NullDic[nullT.name] = nullT;
		}

		// Token: 0x0600298D RID: 10637 RVA: 0x000F32D4 File Offset: 0x000F16D4
		public void ReleaseObject()
		{
			foreach (GameObject gameObject in this.Objects.Values)
			{
				if (gameObject != null)
				{
					UnityEngine.Object.Destroy(gameObject);
				}
			}
			this.Objects.Clear();
		}

		// Token: 0x0600298E RID: 10638 RVA: 0x000F334C File Offset: 0x000F174C
		public void ReleaseNull()
		{
			foreach (Transform transform in this.NullDic.Values)
			{
				if (transform != null)
				{
					UnityEngine.Object.Destroy(transform.gameObject);
				}
			}
			this.NullDic.Clear();
		}

		// Token: 0x0600298F RID: 10639 RVA: 0x000F33C8 File Offset: 0x000F17C8
		public void ReleaseChara()
		{
			foreach (CharaData charaData in this.Characters.Values)
			{
				charaData.Release();
			}
			this.Characters.Clear();
		}

		// Token: 0x06002990 RID: 10640 RVA: 0x000F3434 File Offset: 0x000F1834
		public void ReleaseEventCG()
		{
			if (!this.rootPositionLoaded)
			{
				return;
			}
			if (this._rootPosition == null)
			{
				return;
			}
			Transform eventCGRoot = this._rootPosition.eventCGRoot;
			if (eventCGRoot == null)
			{
				return;
			}
			eventCGRoot.gameObject.Children().ForEach(delegate(GameObject go)
			{
				UnityEngine.Object.Destroy(go);
			});
		}

		// Token: 0x06002991 RID: 10641 RVA: 0x000F34A5 File Offset: 0x000F18A5
		public void Release()
		{
			this.ReleaseObject();
			this.ReleaseNull();
			this.ReleaseChara();
			this.ReleaseEventCG();
			this._useCorrectCamera = this.useCorrectCameraBakup;
		}

		// Token: 0x06002992 RID: 10642 RVA: 0x000F34CB File Offset: 0x000F18CB
		private void OnDestroy()
		{
			this.Release();
			if (this.rootPositionLoaded && this._rootPosition != null)
			{
				UnityEngine.Object.Destroy(this._rootPosition.gameObject);
			}
		}

		// Token: 0x04002A87 RID: 10887
		private bool rootPositionLoaded;

		// Token: 0x04002A88 RID: 10888
		private Root _rootPosition;

		// Token: 0x04002A89 RID: 10889
		private CommandList nowCommandList;

		// Token: 0x04002A8A RID: 10890
		private CommandList backGroundCommandList;

		// Token: 0x04002A94 RID: 10900
		[SerializeField]
		private bool _useCorrectCamera = true;

		// Token: 0x04002A95 RID: 10901
		[SerializeField]
		private CommandController.CharaCorrectHeightCamera _correctCamera = new CommandController.CharaCorrectHeightCamera();

		// Token: 0x04002A96 RID: 10902
		private bool useCorrectCameraBakup = true;

		// Token: 0x020006CD RID: 1741
		public class FontColor : AutoIndexer<Tuple<int, Color>>
		{
			// Token: 0x06002995 RID: 10645 RVA: 0x000F3703 File Offset: 0x000F1B03
			public FontColor() : base(Tuple.Create<int, Color>(-1, Color.white))
			{
			}

			// Token: 0x06002996 RID: 10646 RVA: 0x000F3716 File Offset: 0x000F1B16
			public void Set(string key, Color color)
			{
				this.Set(key, this.initializeValue.Item1, color);
			}

			// Token: 0x06002997 RID: 10647 RVA: 0x000F372B File Offset: 0x000F1B2B
			public void Set(string key, int configIndex)
			{
				this.Set(key, configIndex, this.initializeValue.Item2);
			}

			// Token: 0x06002998 RID: 10648 RVA: 0x000F3740 File Offset: 0x000F1B40
			private void Set(string key, int configIndex, Color color)
			{
				this.dic[key] = Tuple.Create<int, Color>(configIndex, color);
			}

			// Token: 0x06002999 RID: 10649 RVA: 0x000F3758 File Offset: 0x000F1B58
			private Color? GetConfigColor(int configIndex)
			{
				return null;
			}

			// Token: 0x1700063D RID: 1597
			public Color this[string key]
			{
				get
				{
					if (key != null)
					{
						if (CommandController.FontColor.<>f__switch$map9 == null)
						{
							CommandController.FontColor.<>f__switch$map9 = new Dictionary<string, int>(10)
							{
								{
									"[P]",
									0
								},
								{
									"[P姓]",
									0
								},
								{
									"[P名]",
									0
								},
								{
									"[P名前]",
									0
								},
								{
									"[Pあだ名]",
									0
								},
								{
									"[H]",
									1
								},
								{
									"[H姓]",
									1
								},
								{
									"[H名]",
									1
								},
								{
									"[H名前]",
									1
								},
								{
									"[Hあだ名]",
									1
								}
							};
						}
						int num;
						if (CommandController.FontColor.<>f__switch$map9.TryGetValue(key, out num))
						{
							if (num == 0)
							{
								return this.GetConfigColor(0).Value;
							}
							if (num == 1)
							{
								return this.GetConfigColor(1).Value;
							}
						}
					}
					Tuple<int, Color> tuple = base[key];
					Color? configColor = this.GetConfigColor(tuple.Item1);
					return (configColor == null) ? tuple.Item2 : configColor.Value;
				}
				private set
				{
				}
			}
		}

		// Token: 0x020006CE RID: 1742
		[Serializable]
		private class CharaCorrectHeightCamera
		{
			// Token: 0x0600299D RID: 10653 RVA: 0x000F3898 File Offset: 0x000F1C98
			public bool Calculate(IEnumerable<CharaData> datas, out Vector3 pos, out Vector3 ang)
			{
				if (datas == null || !datas.Any<CharaData>())
				{
					pos = Vector3.zero;
					ang = Vector3.zero;
					return false;
				}
				float shape = datas.Average((CharaData item) => item.chaCtrl.GetShapeBodyValue(0));
				pos = MathfEx.GetShapeLerpPositionValue(shape, this.pos.min, this.pos.max);
				ang = MathfEx.GetShapeLerpAngleValue(shape, this.ang.min, this.ang.max);
				return true;
			}

			// Token: 0x04002A9A RID: 10906
			[SerializeField]
			private CommandController.CharaCorrectHeightCamera.Pair pos;

			// Token: 0x04002A9B RID: 10907
			[SerializeField]
			private CommandController.CharaCorrectHeightCamera.Pair ang;

			// Token: 0x020006CF RID: 1743
			[Serializable]
			private struct Pair
			{
				// Token: 0x04002A9D RID: 10909
				public Vector3 min;

				// Token: 0x04002A9E RID: 10910
				public Vector3 max;
			}
		}
	}
}
