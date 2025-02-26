using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Illusion;
using IllusionUtility.GetUtility;
using UnityEngine;

namespace Studio
{
	// Token: 0x020011DA RID: 4570
	public class OptionItemCtrl : MonoBehaviour
	{
		// Token: 0x17001FCD RID: 8141
		// (get) Token: 0x06009629 RID: 38441 RVA: 0x003E0005 File Offset: 0x003DE405
		// (set) Token: 0x0600962A RID: 38442 RVA: 0x003E002F File Offset: 0x003DE42F
		public Animator animator
		{
			get
			{
				if (this.m_Animator == null)
				{
					this.m_Animator = base.gameObject.GetComponentInChildren<Animator>();
				}
				return this.m_Animator;
			}
			set
			{
				this.m_Animator = value;
			}
		}

		// Token: 0x17001FCE RID: 8142
		// (get) Token: 0x0600962B RID: 38443 RVA: 0x003E0038 File Offset: 0x003DE438
		// (set) Token: 0x0600962C RID: 38444 RVA: 0x003E0040 File Offset: 0x003DE440
		public OICharInfo oiCharInfo { get; set; }

		// Token: 0x17001FCF RID: 8143
		// (get) Token: 0x0600962D RID: 38445 RVA: 0x003E0049 File Offset: 0x003DE449
		public HashSet<OptionItemCtrl.ItemInfo> HashItem
		{
			[CompilerGenerated]
			get
			{
				return this.hashItem;
			}
		}

		// Token: 0x17001FD0 RID: 8144
		// (get) Token: 0x0600962E RID: 38446 RVA: 0x003E0051 File Offset: 0x003DE451
		// (set) Token: 0x0600962F RID: 38447 RVA: 0x003E005E File Offset: 0x003DE45E
		public bool visible
		{
			get
			{
				return this.oiCharInfo.animeOptionVisible;
			}
			set
			{
				this.oiCharInfo.animeOptionVisible = value;
				this.SetVisible(this.m_OutsideVisible & this.oiCharInfo.animeOptionVisible);
			}
		}

		// Token: 0x17001FD1 RID: 8145
		// (get) Token: 0x06009630 RID: 38448 RVA: 0x003E0084 File Offset: 0x003DE484
		// (set) Token: 0x06009631 RID: 38449 RVA: 0x003E008C File Offset: 0x003DE48C
		public bool outsideVisible
		{
			get
			{
				return this.m_OutsideVisible;
			}
			set
			{
				this.m_OutsideVisible = value;
				this.SetVisible(this.m_OutsideVisible & this.oiCharInfo.animeOptionVisible);
			}
		}

		// Token: 0x17001FD2 RID: 8146
		// (set) Token: 0x06009632 RID: 38450 RVA: 0x003E00B0 File Offset: 0x003DE4B0
		public float height
		{
			set
			{
				foreach (OptionItemCtrl.ItemInfo itemInfo in this.hashItem)
				{
					itemInfo.height = value;
				}
			}
		}

		// Token: 0x06009633 RID: 38451 RVA: 0x003E010C File Offset: 0x003DE50C
		public void LoadAnimeItem(Info.AnimeLoadInfo _info, string _clip, float _height, float _motion)
		{
			this.ReleaseAllItem();
			if (_info.option.IsNullOrEmpty<Info.OptionItemInfo>())
			{
				return;
			}
			for (int i = 0; i < _info.option.Count; i++)
			{
				Info.OptionItemInfo optionItemInfo = _info.option[i];
				GameObject gameObject = Utility.LoadAsset<GameObject>(optionItemInfo.bundlePath, optionItemInfo.fileName, optionItemInfo.manifest);
				if (!(gameObject == null))
				{
					OptionItemCtrl.ItemInfo itemInfo = new OptionItemCtrl.ItemInfo(_height);
					itemInfo.gameObject = gameObject;
					itemInfo.scale = gameObject.transform.localScale;
					itemInfo.animator = gameObject.GetComponentInChildren<Animator>();
					if (itemInfo.animator != null)
					{
						if (optionItemInfo.anmInfo.Check)
						{
							RuntimeAnimatorController runtimeAnimatorController = CommonLib.LoadAsset<RuntimeAnimatorController>(optionItemInfo.anmInfo.bundlePath, optionItemInfo.anmInfo.fileName, false, string.Empty);
							if (runtimeAnimatorController != null)
							{
								itemInfo.animator.runtimeAnimatorController = runtimeAnimatorController;
							}
							AssetBundleManager.UnloadAssetBundle(optionItemInfo.anmInfo.bundlePath, false, null, false);
							if (optionItemInfo.anmOveride.Check)
							{
								CommonLib.LoadAsset<RuntimeAnimatorController>(optionItemInfo.anmOveride.bundlePath, optionItemInfo.anmOveride.fileName, false, string.Empty).SafeProc(delegate(RuntimeAnimatorController _rac)
								{
									itemInfo.animator.runtimeAnimatorController = Illusion.Utils.Animator.SetupAnimatorOverrideController(itemInfo.animator.runtimeAnimatorController, _rac);
								});
								AssetBundleManager.UnloadAssetBundle(optionItemInfo.anmOveride.bundlePath, false, null, false);
							}
							itemInfo.animator.Play(_clip);
						}
						itemInfo.animator.SetFloat("height", _height);
						itemInfo.IsSync = optionItemInfo.isAnimeSync;
					}
					else
					{
						itemInfo.IsSync = false;
					}
					if (optionItemInfo.parentageInfo.IsNullOrEmpty<Info.ParentageInfo>())
					{
						GameObject gameObject2 = base.gameObject;
						GameObject gameObject3 = gameObject;
						gameObject3.transform.SetParent(gameObject2.transform);
						gameObject3.transform.localPosition = Vector3.zero;
						gameObject3.transform.localRotation = Quaternion.identity;
						if (optionItemInfo.counterScale)
						{
							itemInfo.DefaultScaleOption = true;
						}
						else
						{
							gameObject3.transform.localScale = itemInfo.scale;
						}
					}
					else
					{
						for (int j = 0; j < optionItemInfo.parentageInfo.Length; j++)
						{
							GameObject gameObject4 = base.gameObject.transform.FindLoop(optionItemInfo.parentageInfo[j].parent);
							GameObject gameObject5 = gameObject;
							if (!optionItemInfo.parentageInfo[j].child.IsNullOrEmpty())
							{
								gameObject5 = gameObject5.transform.FindLoop(optionItemInfo.parentageInfo[j].child);
								itemInfo.child.Add(new OptionItemCtrl.ChildInfo(gameObject5.transform.localScale, gameObject5));
							}
							gameObject5.transform.SetParent(gameObject4.transform);
							gameObject5.transform.localPosition = Vector3.zero;
							gameObject5.transform.localRotation = Quaternion.identity;
							if (optionItemInfo.counterScale)
							{
								itemInfo.DefaultScaleOption = true;
							}
							else
							{
								gameObject5.transform.localScale = itemInfo.scale;
							}
						}
					}
					itemInfo.SetRender();
					this.hashItem.Add(itemInfo);
				}
			}
			this.SetVisible(this.visible);
		}

		// Token: 0x06009634 RID: 38452 RVA: 0x003E0494 File Offset: 0x003DE894
		public void ReleaseAllItem()
		{
			foreach (OptionItemCtrl.ItemInfo itemInfo in this.hashItem)
			{
				if (itemInfo != null)
				{
					itemInfo.Release();
				}
			}
			this.hashItem.Clear();
		}

		// Token: 0x06009635 RID: 38453 RVA: 0x003E0508 File Offset: 0x003DE908
		public void PlayAnime()
		{
			foreach (OptionItemCtrl.ItemInfo itemInfo in from v in this.hashItem
			where v.IsAnime
			select v)
			{
				itemInfo.RestartAnime();
			}
		}

		// Token: 0x06009636 RID: 38454 RVA: 0x003E0584 File Offset: 0x003DE984
		public void SetMotion(float _motion)
		{
			foreach (OptionItemCtrl.ItemInfo itemInfo in this.hashItem)
			{
				if (itemInfo.animator && itemInfo.IsSync)
				{
					itemInfo.animator.SetFloat("motion", _motion);
				}
			}
		}

		// Token: 0x06009637 RID: 38455 RVA: 0x003E0608 File Offset: 0x003DEA08
		public void ChangeScale(Vector3 _value)
		{
			foreach (OptionItemCtrl.ItemInfo itemInfo in this.hashItem)
			{
				Transform transform = itemInfo.gameObject.transform;
				transform.localScale = itemInfo.scale;
			}
		}

		// Token: 0x06009638 RID: 38456 RVA: 0x003E0678 File Offset: 0x003DEA78
		public void ReCounterScale()
		{
			foreach (OptionItemCtrl.ItemInfo itemInfo in this.hashItem)
			{
				itemInfo.ReCounterScale();
			}
		}

		// Token: 0x06009639 RID: 38457 RVA: 0x003E06D4 File Offset: 0x003DEAD4
		private void SetVisible(bool _visible)
		{
			foreach (OptionItemCtrl.ItemInfo itemInfo in this.hashItem)
			{
				itemInfo.active = _visible;
			}
		}

		// Token: 0x0600963A RID: 38458 RVA: 0x003E0730 File Offset: 0x003DEB30
		private void Awake()
		{
			this.m_OutsideVisible = true;
		}

		// Token: 0x0600963B RID: 38459 RVA: 0x003E073C File Offset: 0x003DEB3C
		private void LateUpdate()
		{
			if (this.animator == null || this.hashItem.Count == 0)
			{
				return;
			}
			AnimatorStateInfo currentAnimatorStateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
			foreach (OptionItemCtrl.ItemInfo itemInfo in this.hashItem)
			{
				if (itemInfo.IsAnime && itemInfo.IsSync)
				{
					itemInfo.SyncAnime(currentAnimatorStateInfo.shortNameHash, 0, currentAnimatorStateInfo.normalizedTime);
				}
				itemInfo.CounterScale();
			}
		}

		// Token: 0x040078C9 RID: 30921
		private Animator m_Animator;

		// Token: 0x040078CB RID: 30923
		private HashSet<OptionItemCtrl.ItemInfo> hashItem = new HashSet<OptionItemCtrl.ItemInfo>();

		// Token: 0x040078CC RID: 30924
		private bool m_OutsideVisible = true;

		// Token: 0x020011DB RID: 4571
		public class ChildInfo
		{
			// Token: 0x0600963D RID: 38461 RVA: 0x003E07FC File Offset: 0x003DEBFC
			public ChildInfo(Vector3 _scale, GameObject _obj)
			{
				this.scale = _scale;
				this.obj = _obj;
			}

			// Token: 0x040078CE RID: 30926
			public Vector3 scale = Vector3.one;

			// Token: 0x040078CF RID: 30927
			public GameObject obj;
		}

		// Token: 0x020011DC RID: 4572
		public class ItemInfo
		{
			// Token: 0x0600963E RID: 38462 RVA: 0x003E081D File Offset: 0x003DEC1D
			public ItemInfo(float _height)
			{
				this.m_Height = _height;
			}

			// Token: 0x17001FD3 RID: 8147
			// (get) Token: 0x0600963F RID: 38463 RVA: 0x003E0849 File Offset: 0x003DEC49
			// (set) Token: 0x06009640 RID: 38464 RVA: 0x003E0851 File Offset: 0x003DEC51
			public Vector3 scale { get; set; }

			// Token: 0x17001FD4 RID: 8148
			// (get) Token: 0x06009641 RID: 38465 RVA: 0x003E085A File Offset: 0x003DEC5A
			// (set) Token: 0x06009642 RID: 38466 RVA: 0x003E0862 File Offset: 0x003DEC62
			public bool IsScale { get; private set; }

			// Token: 0x17001FD5 RID: 8149
			// (get) Token: 0x06009643 RID: 38467 RVA: 0x003E086B File Offset: 0x003DEC6B
			// (set) Token: 0x06009644 RID: 38468 RVA: 0x003E0873 File Offset: 0x003DEC73
			public bool DefaultScaleOption
			{
				get
				{
					return this.scaleOption;
				}
				set
				{
					this.scaleOption = value;
					this.IsScale = value;
				}
			}

			// Token: 0x17001FD6 RID: 8150
			// (get) Token: 0x06009645 RID: 38469 RVA: 0x003E0883 File Offset: 0x003DEC83
			// (set) Token: 0x06009646 RID: 38470 RVA: 0x003E088B File Offset: 0x003DEC8B
			public bool IsSync { get; set; }

			// Token: 0x17001FD7 RID: 8151
			// (get) Token: 0x06009647 RID: 38471 RVA: 0x003E0894 File Offset: 0x003DEC94
			public bool IsAnime
			{
				[CompilerGenerated]
				get
				{
					return this.animator != null;
				}
			}

			// Token: 0x17001FD8 RID: 8152
			// (get) Token: 0x06009648 RID: 38472 RVA: 0x003E08A4 File Offset: 0x003DECA4
			private Transform Transform
			{
				[CompilerGenerated]
				get
				{
					Transform result;
					if ((result = this._transform) == null)
					{
						result = (this._transform = this.gameObject.transform);
					}
					return result;
				}
			}

			// Token: 0x17001FD9 RID: 8153
			// (get) Token: 0x06009649 RID: 38473 RVA: 0x003E08D2 File Offset: 0x003DECD2
			// (set) Token: 0x0600964A RID: 38474 RVA: 0x003E08DA File Offset: 0x003DECDA
			public float height
			{
				get
				{
					return this.m_Height;
				}
				set
				{
					this.m_Height = value;
				}
			}

			// Token: 0x17001FDA RID: 8154
			// (get) Token: 0x0600964B RID: 38475 RVA: 0x003E08E3 File Offset: 0x003DECE3
			// (set) Token: 0x0600964C RID: 38476 RVA: 0x003E08EC File Offset: 0x003DECEC
			public bool active
			{
				get
				{
					return this.m_Active;
				}
				set
				{
					if (this.m_Active != value)
					{
						this.m_Active = value;
						for (int i = 0; i < this.renderer.Length; i++)
						{
							this.renderer[i].enabled = value;
						}
					}
				}
			}

			// Token: 0x17001FDB RID: 8155
			// (get) Token: 0x0600964D RID: 38477 RVA: 0x003E0933 File Offset: 0x003DED33
			public AnimatorStateInfo CurrentAnimatorStateInfo
			{
				[CompilerGenerated]
				get
				{
					return this.animator.GetCurrentAnimatorStateInfo(0);
				}
			}

			// Token: 0x0600964E RID: 38478 RVA: 0x003E0944 File Offset: 0x003DED44
			public void Release()
			{
				UnityEngine.Object.DestroyImmediate(this.gameObject);
				for (int i = 0; i < this.child.Count; i++)
				{
					UnityEngine.Object.DestroyImmediate(this.child[i].obj);
				}
			}

			// Token: 0x0600964F RID: 38479 RVA: 0x003E0990 File Offset: 0x003DED90
			public void SetRender()
			{
				List<Renderer> list = new List<Renderer>();
				Renderer[] componentsInChildren = this.gameObject.GetComponentsInChildren<Renderer>();
				if (!componentsInChildren.IsNullOrEmpty<Renderer>())
				{
					list.AddRange(componentsInChildren);
				}
				for (int i = 0; i < this.child.Count; i++)
				{
					componentsInChildren = this.child[i].obj.GetComponentsInChildren<Renderer>();
					if (!componentsInChildren.IsNullOrEmpty<Renderer>())
					{
						list.AddRange(componentsInChildren);
					}
				}
				this.renderer = (from v in list
				where v.enabled
				select v).ToArray<Renderer>();
			}

			// Token: 0x06009650 RID: 38480 RVA: 0x003E0A34 File Offset: 0x003DEE34
			public void RestartAnime()
			{
				AnimatorStateInfo currentAnimatorStateInfo = this.CurrentAnimatorStateInfo;
				this.animator.Play(currentAnimatorStateInfo.shortNameHash, 0, 0f);
			}

			// Token: 0x06009651 RID: 38481 RVA: 0x003E0A60 File Offset: 0x003DEE60
			public void SyncAnime(int stateNameHash, int layer, float normalizedTime)
			{
				this.animator.Play(stateNameHash, layer, normalizedTime);
			}

			// Token: 0x06009652 RID: 38482 RVA: 0x003E0A70 File Offset: 0x003DEE70
			public void CounterScale()
			{
				if (!this.IsScale)
				{
					return;
				}
				Vector3 localScale = this.Transform.localScale;
				Vector3 lossyScale = this.Transform.lossyScale;
				this.Transform.localScale = new Vector3(localScale.x / lossyScale.x * this.scale.x, localScale.y / lossyScale.y * this.scale.y, localScale.z / lossyScale.z * this.scale.z);
				this.IsScale = false;
			}

			// Token: 0x06009653 RID: 38483 RVA: 0x003E0B13 File Offset: 0x003DEF13
			public void ReCounterScale()
			{
				this.DefaultScaleOption = this.DefaultScaleOption;
			}

			// Token: 0x040078D0 RID: 30928
			public GameObject gameObject;

			// Token: 0x040078D1 RID: 30929
			public Animator animator;

			// Token: 0x040078D2 RID: 30930
			public List<OptionItemCtrl.ChildInfo> child = new List<OptionItemCtrl.ChildInfo>();

			// Token: 0x040078D3 RID: 30931
			private Renderer[] renderer;

			// Token: 0x040078D6 RID: 30934
			private bool scaleOption;

			// Token: 0x040078D8 RID: 30936
			private Transform _transform;

			// Token: 0x040078D9 RID: 30937
			public float m_Height = 0.5f;

			// Token: 0x040078DA RID: 30938
			private bool m_Active = true;
		}
	}
}
