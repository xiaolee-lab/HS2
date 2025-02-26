using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AIChara;
using Illusion;
using Manager;
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Studio
{
	// Token: 0x020012FA RID: 4858
	public class MPCharCtrl : MonoBehaviour
	{
		// Token: 0x1700222C RID: 8748
		// (get) Token: 0x0600A20D RID: 41485 RVA: 0x00427257 File Offset: 0x00425657
		// (set) Token: 0x0600A20E RID: 41486 RVA: 0x0042725F File Offset: 0x0042565F
		public OCIChar ociChar
		{
			get
			{
				return this.m_OCIChar;
			}
			set
			{
				this.m_OCIChar = value;
				if (this.m_OCIChar != null)
				{
					this.UpdateInfo();
				}
			}
		}

		// Token: 0x1700222D RID: 8749
		// (get) Token: 0x0600A20F RID: 41487 RVA: 0x00427279 File Offset: 0x00425679
		// (set) Token: 0x0600A210 RID: 41488 RVA: 0x00427286 File Offset: 0x00425686
		public bool active
		{
			get
			{
				return base.gameObject.activeSelf;
			}
			set
			{
				if (base.gameObject.activeSelf != value)
				{
					base.gameObject.SetActive(value);
					if (!base.gameObject.activeSelf)
					{
						this.OnClickRoot(-1);
					}
				}
			}
		}

		// Token: 0x0600A211 RID: 41489 RVA: 0x004272BC File Offset: 0x004256BC
		public void OnClickRoot(int _idx)
		{
			this.select = _idx;
			for (int i = 0; i < this.rootButtonInfo.Length; i++)
			{
				this.rootButtonInfo[i].active = (i == _idx);
			}
			this.animeControl.active = (_idx == 2);
			this.voiceControl.active = (_idx == 3);
			switch (_idx)
			{
			case 0:
				this.stateInfo.UpdateInfo(this.m_OCIChar);
				break;
			case 1:
				this.fkInfo.UpdateInfo(this.m_OCIChar);
				this.ikInfo.UpdateInfo(this.m_OCIChar);
				this.lookAtInfo.UpdateInfo(this.m_OCIChar);
				this.neckInfo.UpdateInfo(this.m_OCIChar);
				this.poseInfo.UpdateInfo(this.m_OCIChar);
				this.etcInfo.UpdateInfo(this.m_OCIChar);
				this.handInfo.UpdateInfo(this.m_OCIChar);
				break;
			case 2:
				this.animeGroupList.InitList((AnimeGroupList.SEX)this.m_OCIChar.oiCharInfo.sex);
				this.animeControl.objectCtrlInfo = this.m_OCIChar;
				break;
			case 3:
				this.voiceControl.ociChar = this.m_OCIChar;
				break;
			case 4:
				this.costumeInfo.UpdateInfo(this.m_OCIChar);
				break;
			case 5:
				this.jointInfo.UpdateInfo(this.m_OCIChar);
				break;
			}
		}

		// Token: 0x0600A212 RID: 41490 RVA: 0x00427440 File Offset: 0x00425840
		public void OnClickKinematic(int _idx)
		{
			if (this.kinematic == _idx)
			{
				return;
			}
			MPCharCtrl.CommonInfo[] array = new MPCharCtrl.CommonInfo[]
			{
				this.fkInfo,
				this.ikInfo,
				this.lookAtInfo,
				this.neckInfo,
				null,
				this.etcInfo,
				this.handInfo,
				this.poseInfo
			};
			if (MathfEx.RangeEqualOn<int>(0, this.kinematic, array.Length - 1) && array[this.kinematic] != null)
			{
				array[this.kinematic].active = false;
				this.buttonKinematic[this.kinematic].image.color = Color.white;
			}
			this.kinematic = _idx;
			if (array[this.kinematic] != null)
			{
				array[this.kinematic].active = true;
				array[this.kinematic].UpdateInfo(this.m_OCIChar);
				this.buttonKinematic[this.kinematic].image.color = Color.green;
			}
		}

		// Token: 0x0600A213 RID: 41491 RVA: 0x0042753E File Offset: 0x0042593E
		public void LoadAnime(AnimeGroupList.SEX _sex, int _group, int _category, int _no)
		{
			this.m_OCIChar.LoadAnime(_group, _category, _no, 0f);
			this.animeControl.UpdateInfo();
		}

		// Token: 0x0600A214 RID: 41492 RVA: 0x0042755F File Offset: 0x0042595F
		public bool Deselect(OCIChar _ociChar)
		{
			if (this.m_OCIChar != _ociChar)
			{
				return false;
			}
			this.ociChar = null;
			this.active = false;
			return true;
		}

		// Token: 0x0600A215 RID: 41493 RVA: 0x0042757E File Offset: 0x0042597E
		private void UpdateInfo()
		{
			this.OnClickRoot(this.select);
		}

		// Token: 0x0600A216 RID: 41494 RVA: 0x0042758C File Offset: 0x0042598C
		private void SetCopyBoneFK(OIBoneInfo.BoneGroup _group)
		{
			if (this.disposableFK != null)
			{
				this.disposableFK.Dispose();
				this.disposableFK = null;
			}
			this.disposableFK = new SingleAssignmentDisposable();
			this.disposableFK.Disposable = this.LateUpdateAsObservable().Take(1).Subscribe(delegate(Unit _)
			{
				this.CopyBoneFK(_group);
			}, delegate()
			{
				this.disposableFK.Dispose();
				this.disposableFK = null;
			});
		}

		// Token: 0x0600A217 RID: 41495 RVA: 0x0042760C File Offset: 0x00425A0C
		private void SetCopyBoneIK(OIBoneInfo.BoneGroup _group)
		{
			if (this.disposableIK != null)
			{
				this.disposableIK.Dispose();
				this.disposableIK = null;
			}
			this.disposableIK = new SingleAssignmentDisposable();
			this.disposableIK.Disposable = this.LateUpdateAsObservable().Take(1).Subscribe(delegate(Unit _)
			{
				this.CopyBoneIK(_group);
			}, delegate()
			{
				this.disposableIK.Dispose();
				this.disposableIK = null;
			});
		}

		// Token: 0x0600A218 RID: 41496 RVA: 0x00427689 File Offset: 0x00425A89
		private void CopyBoneFK(OIBoneInfo.BoneGroup _group)
		{
			if (this.m_OCIChar != null)
			{
				this.m_OCIChar.fkCtrl.CopyBone(_group);
			}
		}

		// Token: 0x0600A219 RID: 41497 RVA: 0x004276A7 File Offset: 0x00425AA7
		private void CopyBoneIK(OIBoneInfo.BoneGroup _group)
		{
			if (this.m_OCIChar != null)
			{
				this.m_OCIChar.ikCtrl.CopyBone(_group);
			}
		}

		// Token: 0x0600A21A RID: 41498 RVA: 0x004276C8 File Offset: 0x00425AC8
		private void Awake()
		{
			this.fkInfo.Init();
			this.fkInfo.buttonAnime.onClick.AddListener(delegate()
			{
				this.SetCopyBoneFK((OIBoneInfo.BoneGroup)353);
			});
			this.fkInfo.buttonAnimeSingle[0].onClick.AddListener(delegate()
			{
				this.SetCopyBoneFK(OIBoneInfo.BoneGroup.Body);
			});
			this.fkInfo.buttonAnimeSingle[1].onClick.AddListener(delegate()
			{
				this.SetCopyBoneFK(OIBoneInfo.BoneGroup.Neck);
			});
			this.fkInfo.buttonAnimeSingle[2].onClick.AddListener(delegate()
			{
				this.SetCopyBoneFK(OIBoneInfo.BoneGroup.LeftHand);
			});
			this.fkInfo.buttonAnimeSingle[3].onClick.AddListener(delegate()
			{
				this.SetCopyBoneFK(OIBoneInfo.BoneGroup.RightHand);
			});
			this.fkInfo.buttonReflectIK.onClick.AddListener(delegate()
			{
				this.CopyBoneIK((OIBoneInfo.BoneGroup)31);
			});
			this.ikInfo.Init();
			this.ikInfo.buttonAnime.onClick.AddListener(delegate()
			{
				this.SetCopyBoneIK((OIBoneInfo.BoneGroup)31);
			});
			this.ikInfo.buttonAnimeSingle[0].onClick.AddListener(delegate()
			{
				this.SetCopyBoneIK(OIBoneInfo.BoneGroup.Body);
			});
			this.ikInfo.buttonAnimeSingle[1].onClick.AddListener(delegate()
			{
				this.SetCopyBoneIK(OIBoneInfo.BoneGroup.LeftArm);
			});
			this.ikInfo.buttonAnimeSingle[2].onClick.AddListener(delegate()
			{
				this.SetCopyBoneIK(OIBoneInfo.BoneGroup.RightArm);
			});
			this.ikInfo.buttonAnimeSingle[3].onClick.AddListener(delegate()
			{
				this.SetCopyBoneIK(OIBoneInfo.BoneGroup.LeftLeg);
			});
			this.ikInfo.buttonAnimeSingle[4].onClick.AddListener(delegate()
			{
				this.SetCopyBoneIK(OIBoneInfo.BoneGroup.RightLeg);
			});
			this.ikInfo.buttonReflectFK.onClick.AddListener(delegate()
			{
				this.CopyBoneFK((OIBoneInfo.BoneGroup)353);
			});
			this.stateInfo.Init();
			this.lookAtInfo.Init();
			this.neckInfo.Init();
			this.etcInfo.Init();
			this.handInfo.Init();
			this.costumeInfo.Init();
			this.jointInfo.Init();
			this.select = -1;
		}

		// Token: 0x04008013 RID: 32787
		[SerializeField]
		private MPCharCtrl.RootButtonInfo[] rootButtonInfo;

		// Token: 0x04008014 RID: 32788
		[SerializeField]
		private AnimeGroupList animeGroupList;

		// Token: 0x04008015 RID: 32789
		[SerializeField]
		private AnimeControl animeControl;

		// Token: 0x04008016 RID: 32790
		[SerializeField]
		private VoiceControl voiceControl;

		// Token: 0x04008017 RID: 32791
		[SerializeField]
		private MPCharCtrl.StateInfo stateInfo = new MPCharCtrl.StateInfo();

		// Token: 0x04008018 RID: 32792
		[SerializeField]
		private MPCharCtrl.FKInfo fkInfo = new MPCharCtrl.FKInfo();

		// Token: 0x04008019 RID: 32793
		[SerializeField]
		private MPCharCtrl.IKInfo ikInfo = new MPCharCtrl.IKInfo();

		// Token: 0x0400801A RID: 32794
		[SerializeField]
		private MPCharCtrl.LookAtInfo lookAtInfo = new MPCharCtrl.LookAtInfo();

		// Token: 0x0400801B RID: 32795
		[SerializeField]
		private MPCharCtrl.NeckInfo neckInfo = new MPCharCtrl.NeckInfo();

		// Token: 0x0400801C RID: 32796
		[SerializeField]
		private MPCharCtrl.PoseInfo poseInfo = new MPCharCtrl.PoseInfo();

		// Token: 0x0400801D RID: 32797
		[SerializeField]
		private MPCharCtrl.EtcInfo etcInfo = new MPCharCtrl.EtcInfo();

		// Token: 0x0400801E RID: 32798
		[SerializeField]
		private MPCharCtrl.HandInfo handInfo = new MPCharCtrl.HandInfo();

		// Token: 0x0400801F RID: 32799
		[SerializeField]
		private Button[] buttonKinematic;

		// Token: 0x04008020 RID: 32800
		[SerializeField]
		private MPCharCtrl.CostumeInfo costumeInfo = new MPCharCtrl.CostumeInfo();

		// Token: 0x04008021 RID: 32801
		[SerializeField]
		private MPCharCtrl.JointInfo jointInfo = new MPCharCtrl.JointInfo();

		// Token: 0x04008022 RID: 32802
		private OCIChar m_OCIChar;

		// Token: 0x04008023 RID: 32803
		private int kinematic = -1;

		// Token: 0x04008024 RID: 32804
		private int select = -1;

		// Token: 0x04008025 RID: 32805
		private SingleAssignmentDisposable disposableFK;

		// Token: 0x04008026 RID: 32806
		private SingleAssignmentDisposable disposableIK;

		// Token: 0x020012FB RID: 4859
		[Serializable]
		private class CommonInfo
		{
			// Token: 0x1700222E RID: 8750
			// (set) Token: 0x0600A229 RID: 41513 RVA: 0x0042798C File Offset: 0x00425D8C
			public virtual bool active
			{
				set
				{
					if (this.objRoot.activeSelf != value)
					{
						this.objRoot.SetActive(value);
					}
				}
			}

			// Token: 0x1700222F RID: 8751
			// (get) Token: 0x0600A22A RID: 41514 RVA: 0x004279AB File Offset: 0x00425DAB
			// (set) Token: 0x0600A22B RID: 41515 RVA: 0x004279B3 File Offset: 0x00425DB3
			public bool isUpdateInfo { get; set; }

			// Token: 0x17002230 RID: 8752
			// (get) Token: 0x0600A22C RID: 41516 RVA: 0x004279BC File Offset: 0x00425DBC
			// (set) Token: 0x0600A22D RID: 41517 RVA: 0x004279C4 File Offset: 0x00425DC4
			public OCIChar ociChar { get; set; }

			// Token: 0x0600A22E RID: 41518 RVA: 0x004279CD File Offset: 0x00425DCD
			public virtual void Init()
			{
				this.isUpdateInfo = false;
			}

			// Token: 0x0600A22F RID: 41519 RVA: 0x004279D6 File Offset: 0x00425DD6
			public virtual void UpdateInfo(OCIChar _char)
			{
				this.ociChar = _char;
			}

			// Token: 0x04008027 RID: 32807
			public GameObject objRoot;
		}

		// Token: 0x020012FC RID: 4860
		[Serializable]
		private class RootButtonInfo
		{
			// Token: 0x17002231 RID: 8753
			// (get) Token: 0x0600A231 RID: 41521 RVA: 0x004279E7 File Offset: 0x00425DE7
			// (set) Token: 0x0600A232 RID: 41522 RVA: 0x004279F4 File Offset: 0x00425DF4
			public bool active
			{
				get
				{
					return this.root.activeSelf;
				}
				set
				{
					if (this.root.activeSelf != value)
					{
						this.root.SetActive(value);
						this.button.image.color = ((!this.root.activeSelf) ? Color.white : Color.green);
					}
				}
			}

			// Token: 0x0400802A RID: 32810
			public Button button;

			// Token: 0x0400802B RID: 32811
			public GameObject root;
		}

		// Token: 0x020012FD RID: 4861
		[Serializable]
		public class StateCommonInfo
		{
			// Token: 0x17002232 RID: 8754
			// (get) Token: 0x0600A234 RID: 41524 RVA: 0x00427A5C File Offset: 0x00425E5C
			// (set) Token: 0x0600A235 RID: 41525 RVA: 0x00427A64 File Offset: 0x00425E64
			public Sprite[] spriteVisible { get; set; }

			// Token: 0x17002233 RID: 8755
			// (get) Token: 0x0600A236 RID: 41526 RVA: 0x00427A6D File Offset: 0x00425E6D
			// (set) Token: 0x0600A237 RID: 41527 RVA: 0x00427A75 File Offset: 0x00425E75
			public bool isOpen
			{
				get
				{
					return this.m_Open;
				}
				set
				{
					if (Utility.SetStruct<bool>(ref this.m_Open, value))
					{
						this.Change();
					}
				}
			}

			// Token: 0x17002234 RID: 8756
			// (set) Token: 0x0600A238 RID: 41528 RVA: 0x00427A90 File Offset: 0x00425E90
			public bool active
			{
				set
				{
					GameObject gameObject = this.buttonOpen.transform.parent.gameObject;
					if (gameObject.activeSelf != value)
					{
						gameObject.SetActive(value);
						bool flag = value & this.m_Open;
						if (this.objOpen.activeSelf != flag)
						{
							this.objOpen.SetActive(flag);
						}
					}
				}
			}

			// Token: 0x0600A239 RID: 41529 RVA: 0x00427AEC File Offset: 0x00425EEC
			public virtual void Init(Sprite[] _spriteVisible)
			{
				this.spriteVisible = _spriteVisible;
				this.buttonOpen.onClick.AddListener(new UnityAction(this.OnClick));
				this.m_Open = true;
			}

			// Token: 0x0600A23A RID: 41530 RVA: 0x00427B18 File Offset: 0x00425F18
			public virtual void UpdateInfo(OCIChar _char)
			{
			}

			// Token: 0x0600A23B RID: 41531 RVA: 0x00427B1A File Offset: 0x00425F1A
			private void OnClick()
			{
				this.isOpen = !this.isOpen;
			}

			// Token: 0x0600A23C RID: 41532 RVA: 0x00427B2C File Offset: 0x00425F2C
			private void Change()
			{
				if (this.objOpen.activeSelf != this.m_Open)
				{
					this.objOpen.SetActive(this.m_Open);
				}
				this.buttonOpen.image.sprite = this.spriteVisible[(!this.m_Open) ? 0 : 1];
			}

			// Token: 0x0400802D RID: 32813
			public Button buttonOpen;

			// Token: 0x0400802E RID: 32814
			public GameObject objOpen;

			// Token: 0x0400802F RID: 32815
			private bool m_Open = true;
		}

		// Token: 0x020012FE RID: 4862
		[Serializable]
		public class StateButtonInfo
		{
			// Token: 0x17002235 RID: 8757
			// (set) Token: 0x0600A23E RID: 41534 RVA: 0x00427B94 File Offset: 0x00425F94
			public bool interactable
			{
				set
				{
					for (int i = 0; i < this.buttons.Length; i++)
					{
						this.buttons[i].interactable = value;
					}
				}
			}

			// Token: 0x17002236 RID: 8758
			// (set) Token: 0x0600A23F RID: 41535 RVA: 0x00427BC8 File Offset: 0x00425FC8
			public int select
			{
				set
				{
					int num = Mathf.Clamp(value, 0, this.buttons.Length - 1);
					for (int i = 0; i < this.buttons.Length; i++)
					{
						this.buttons[i].image.color = ((!this.buttons[i].interactable || i != num) ? Color.white : Color.green);
					}
				}
			}

			// Token: 0x17002237 RID: 8759
			// (set) Token: 0x0600A240 RID: 41536 RVA: 0x00427C3A File Offset: 0x0042603A
			public bool active
			{
				set
				{
					if (this.root && this.root.activeSelf != value)
					{
						this.root.SetActive(value);
					}
				}
			}

			// Token: 0x0600A241 RID: 41537 RVA: 0x00427C69 File Offset: 0x00426069
			public void Interactable(int _state, bool _flag)
			{
				this.buttons[_state].interactable = _flag;
			}

			// Token: 0x0600A242 RID: 41538 RVA: 0x00427C7C File Offset: 0x0042607C
			public void Interactable(params int[] _state)
			{
				if (_state.IsNullOrEmpty<int>())
				{
					this.interactable = false;
					return;
				}
				for (int i = 0; i < this.buttons.Length; i++)
				{
					this.buttons[i].interactable = _state.Contains(i);
				}
			}

			// Token: 0x04008030 RID: 32816
			public GameObject root;

			// Token: 0x04008031 RID: 32817
			public Button[] buttons;
		}

		// Token: 0x020012FF RID: 4863
		[Serializable]
		public class StateSliderInfo
		{
			// Token: 0x17002238 RID: 8760
			// (set) Token: 0x0600A244 RID: 41540 RVA: 0x00427CD1 File Offset: 0x004260D1
			public bool active
			{
				set
				{
					if (this.root && this.root.activeSelf != value)
					{
						this.root.SetActive(value);
					}
				}
			}

			// Token: 0x04008032 RID: 32818
			public GameObject root;

			// Token: 0x04008033 RID: 32819
			public Slider slider;
		}

		// Token: 0x02001300 RID: 4864
		[Serializable]
		public class StateToggleInfo
		{
			// Token: 0x17002239 RID: 8761
			// (set) Token: 0x0600A246 RID: 41542 RVA: 0x00427D08 File Offset: 0x00426108
			public bool active
			{
				set
				{
					if (this.root && this.root.activeSelf != value)
					{
						this.root.SetActive(value);
					}
				}
			}

			// Token: 0x04008034 RID: 32820
			public GameObject root;

			// Token: 0x04008035 RID: 32821
			public Toggle toggle;
		}

		// Token: 0x02001301 RID: 4865
		[Serializable]
		public class ClothingDetailsInfo : MPCharCtrl.StateCommonInfo
		{
			// Token: 0x1700223A RID: 8762
			// (get) Token: 0x0600A248 RID: 41544 RVA: 0x00427DB0 File Offset: 0x004261B0
			private MPCharCtrl.StateButtonInfo[] infoArray
			{
				get
				{
					return new MPCharCtrl.StateButtonInfo[]
					{
						this.top,
						this.buttom,
						this.bra,
						this.shorts,
						this.gloves,
						this.pantyhose,
						this.socks,
						this.shoes
					};
				}
			}

			// Token: 0x0600A249 RID: 41545 RVA: 0x00427E0C File Offset: 0x0042620C
			public void Init(Sprite[] _spriteVisible, MPCharCtrl.ClothingDetailsInfo.OnClickFunc _func)
			{
				base.Init(_spriteVisible);
				MPCharCtrl.StateButtonInfo[] infoArray = this.infoArray;
				for (int i = 0; i < infoArray.Length; i++)
				{
					int id = i;
					this.SetFunc(infoArray[i], _func, id);
				}
			}

			// Token: 0x0600A24A RID: 41546 RVA: 0x00427E48 File Offset: 0x00426248
			public override void UpdateInfo(OCIChar _char)
			{
				base.UpdateInfo(_char);
				this.ociChar = _char;
				MPCharCtrl.StateButtonInfo[] infoArray = this.infoArray;
				for (int i = 0; i < infoArray.Length; i++)
				{
					infoArray[i].active = true;
					if (i != 3)
					{
						infoArray[i].interactable = _char.charInfo.IsClothesStateKind(i);
					}
					else
					{
						Dictionary<byte, string> clothesStateKind = _char.charInfo.GetClothesStateKind(i);
						MPCharCtrl.StateButtonInfo stateButtonInfo = infoArray[i];
						int[] state;
						if (clothesStateKind != null)
						{
							state = (from v in clothesStateKind.Keys
							select (int)v).ToArray<int>();
						}
						else
						{
							state = null;
						}
						stateButtonInfo.Interactable(state);
					}
					infoArray[i].select = (int)_char.charFileStatus.clothesState[i];
				}
			}

			// Token: 0x0600A24B RID: 41547 RVA: 0x00427F14 File Offset: 0x00426314
			private void SetFunc(MPCharCtrl.StateButtonInfo _info, MPCharCtrl.ClothingDetailsInfo.OnClickFunc _func, int _id)
			{
				for (int i = 0; i < _info.buttons.Length; i++)
				{
					byte state = (byte)i;
					_info.buttons[i].onClick.AddListener(delegate()
					{
						_func(_id, state);
					});
					_info.buttons[i].onClick.AddListener(new UnityAction(this.UpdateState));
				}
			}

			// Token: 0x0600A24C RID: 41548 RVA: 0x00427FA0 File Offset: 0x004263A0
			private void UpdateState()
			{
				if (this.ociChar == null)
				{
					return;
				}
				MPCharCtrl.StateButtonInfo[] infoArray = this.infoArray;
				for (int i = 0; i < infoArray.Length; i++)
				{
					infoArray[i].select = (int)this.ociChar.charFileStatus.clothesState[i];
				}
			}

			// Token: 0x04008036 RID: 32822
			public MPCharCtrl.StateButtonInfo top = new MPCharCtrl.StateButtonInfo();

			// Token: 0x04008037 RID: 32823
			public MPCharCtrl.StateButtonInfo buttom = new MPCharCtrl.StateButtonInfo();

			// Token: 0x04008038 RID: 32824
			public MPCharCtrl.StateButtonInfo bra = new MPCharCtrl.StateButtonInfo();

			// Token: 0x04008039 RID: 32825
			public MPCharCtrl.StateButtonInfo shorts = new MPCharCtrl.StateButtonInfo();

			// Token: 0x0400803A RID: 32826
			public MPCharCtrl.StateButtonInfo pantyhose = new MPCharCtrl.StateButtonInfo();

			// Token: 0x0400803B RID: 32827
			public MPCharCtrl.StateButtonInfo gloves = new MPCharCtrl.StateButtonInfo();

			// Token: 0x0400803C RID: 32828
			public MPCharCtrl.StateButtonInfo socks = new MPCharCtrl.StateButtonInfo();

			// Token: 0x0400803D RID: 32829
			public MPCharCtrl.StateButtonInfo cloth = new MPCharCtrl.StateButtonInfo();

			// Token: 0x0400803E RID: 32830
			public MPCharCtrl.StateButtonInfo shoes = new MPCharCtrl.StateButtonInfo();

			// Token: 0x0400803F RID: 32831
			private OCIChar ociChar;

			// Token: 0x02001302 RID: 4866
			// (Invoke) Token: 0x0600A24F RID: 41551
			public delegate void OnClickFunc(int _id, byte _state);
		}

		// Token: 0x02001303 RID: 4867
		[Serializable]
		public class AccessoriesInfo : MPCharCtrl.StateCommonInfo
		{
			// Token: 0x0600A253 RID: 41555 RVA: 0x0042803C File Offset: 0x0042643C
			public void Init(Sprite[] _spriteVisible, MPCharCtrl.AccessoriesInfo.OnClickFunc _func)
			{
				base.Init(_spriteVisible);
				for (int i = 0; i < this.slots.Length; i++)
				{
					int id = i;
					for (int j = 0; j < 2; j++)
					{
						bool flag = j == 0;
						this.slots[i].buttons[j].onClick.AddListener(delegate()
						{
							_func(id, flag);
						});
						int state = j;
						this.slots[i].buttons[j].onClick.AddListener(delegate()
						{
							this.slots[id].select = state;
						});
					}
				}
			}

			// Token: 0x0600A254 RID: 41556 RVA: 0x00428114 File Offset: 0x00426514
			public override void UpdateInfo(OCIChar _char)
			{
				base.UpdateInfo(_char);
				for (int i = 0; i < this.slots.Length; i++)
				{
					this.slots[i].interactable = (_char.charInfo.objAccessory[i] != null);
				}
				for (int j = 0; j < this.slots.Length; j++)
				{
					this.slots[j].select = ((!_char.charFileStatus.showAccessory[j]) ? 1 : 0);
				}
			}

			// Token: 0x04008041 RID: 32833
			public MPCharCtrl.StateButtonInfo[] slots = new MPCharCtrl.StateButtonInfo[20];

			// Token: 0x02001304 RID: 4868
			// (Invoke) Token: 0x0600A256 RID: 41558
			public delegate void OnClickFunc(int _id, bool _flag);
		}

		// Token: 0x02001305 RID: 4869
		[Serializable]
		public class LiquidInfo : MPCharCtrl.StateCommonInfo
		{
			// Token: 0x0600A25A RID: 41562 RVA: 0x00428244 File Offset: 0x00426644
			public void Init(Sprite[] _spriteVisible, MPCharCtrl.LiquidInfo.OnClickFunc _func)
			{
				base.Init(_spriteVisible);
				this.SetFunc(this.face, _func, ChaFileDefine.SiruParts.SiruKao);
				this.SetFunc(this.breast, _func, ChaFileDefine.SiruParts.SiruFrontTop);
				this.SetFunc(this.back, _func, ChaFileDefine.SiruParts.SiruBackTop);
				this.SetFunc(this.belly, _func, ChaFileDefine.SiruParts.SiruFrontBot);
				this.SetFunc(this.hip, _func, ChaFileDefine.SiruParts.SiruBackBot);
			}

			// Token: 0x0600A25B RID: 41563 RVA: 0x004282A0 File Offset: 0x004266A0
			public override void UpdateInfo(OCIChar _char)
			{
				base.UpdateInfo(_char);
				if (_char.oiCharInfo.sex == 1)
				{
					base.active = true;
					this.face.select = (int)_char.GetSiruFlags(ChaFileDefine.SiruParts.SiruKao);
					this.breast.select = (int)_char.GetSiruFlags(ChaFileDefine.SiruParts.SiruFrontTop);
					this.back.select = (int)_char.GetSiruFlags(ChaFileDefine.SiruParts.SiruBackTop);
					this.belly.select = (int)_char.GetSiruFlags(ChaFileDefine.SiruParts.SiruFrontBot);
					this.hip.select = (int)_char.GetSiruFlags(ChaFileDefine.SiruParts.SiruBackBot);
				}
				else
				{
					base.active = false;
				}
			}

			// Token: 0x0600A25C RID: 41564 RVA: 0x00428334 File Offset: 0x00426734
			private void SetFunc(MPCharCtrl.StateButtonInfo _info, MPCharCtrl.LiquidInfo.OnClickFunc _func, ChaFileDefine.SiruParts _parts)
			{
				for (int i = 0; i < _info.buttons.Length; i++)
				{
					byte state = (byte)i;
					_info.buttons[i].onClick.AddListener(delegate()
					{
						_func(_parts, state);
					});
					_info.buttons[i].onClick.AddListener(delegate()
					{
						_info.select = (int)state;
					});
				}
			}

			// Token: 0x04008042 RID: 32834
			public MPCharCtrl.StateButtonInfo face = new MPCharCtrl.StateButtonInfo();

			// Token: 0x04008043 RID: 32835
			public MPCharCtrl.StateButtonInfo breast = new MPCharCtrl.StateButtonInfo();

			// Token: 0x04008044 RID: 32836
			public MPCharCtrl.StateButtonInfo back = new MPCharCtrl.StateButtonInfo();

			// Token: 0x04008045 RID: 32837
			public MPCharCtrl.StateButtonInfo belly = new MPCharCtrl.StateButtonInfo();

			// Token: 0x04008046 RID: 32838
			public MPCharCtrl.StateButtonInfo hip = new MPCharCtrl.StateButtonInfo();

			// Token: 0x02001306 RID: 4870
			// (Invoke) Token: 0x0600A25E RID: 41566
			public delegate void OnClickFunc(ChaFileDefine.SiruParts _parts, byte _state);
		}

		// Token: 0x02001307 RID: 4871
		[Serializable]
		public class OtherInfo : MPCharCtrl.StateCommonInfo
		{
			// Token: 0x0600A262 RID: 41570 RVA: 0x00428498 File Offset: 0x00426898
			public override void UpdateInfo(OCIChar _char)
			{
				base.UpdateInfo(_char);
				bool flag = _char.oiCharInfo.sex == 1;
				this.nipple.active = flag;
				this.skin.active = true;
				this.wet.active = true;
				this.single.active = true;
				this.color.active = true;
				this.son.active = true;
				this.sonLen.active = true;
				this.tears.slider.value = _char.GetTears();
				this.cheek.slider.value = _char.GetHohoAkaRate();
				this.son.toggle.isOn = _char.oiCharInfo.visibleSon;
				this.sonLen.slider.value = _char.GetSonLength();
				if (flag)
				{
					this.nipple.slider.value = _char.oiCharInfo.nipple;
				}
				this.skin.slider.value = _char.oiCharInfo.SkinTuyaRate;
				this.wet.slider.value = _char.oiCharInfo.WetRate;
				this.single.toggle.isOn = _char.GetVisibleSimple();
				this.SetSimpleColor(_char.oiCharInfo.simpleColor);
			}

			// Token: 0x0600A263 RID: 41571 RVA: 0x004285EA File Offset: 0x004269EA
			public void SetSimpleColor(Color _color)
			{
				this.color.buttons[0].image.color = _color;
			}

			// Token: 0x04008047 RID: 32839
			public MPCharCtrl.StateSliderInfo tears = new MPCharCtrl.StateSliderInfo();

			// Token: 0x04008048 RID: 32840
			public MPCharCtrl.StateSliderInfo cheek = new MPCharCtrl.StateSliderInfo();

			// Token: 0x04008049 RID: 32841
			public MPCharCtrl.StateSliderInfo nipple = new MPCharCtrl.StateSliderInfo();

			// Token: 0x0400804A RID: 32842
			public MPCharCtrl.StateSliderInfo skin = new MPCharCtrl.StateSliderInfo();

			// Token: 0x0400804B RID: 32843
			public MPCharCtrl.StateSliderInfo wet = new MPCharCtrl.StateSliderInfo();

			// Token: 0x0400804C RID: 32844
			public MPCharCtrl.StateToggleInfo single = new MPCharCtrl.StateToggleInfo();

			// Token: 0x0400804D RID: 32845
			public MPCharCtrl.StateButtonInfo color = new MPCharCtrl.StateButtonInfo();

			// Token: 0x0400804E RID: 32846
			public MPCharCtrl.StateToggleInfo son = new MPCharCtrl.StateToggleInfo();

			// Token: 0x0400804F RID: 32847
			public MPCharCtrl.StateSliderInfo sonLen = new MPCharCtrl.StateSliderInfo();

			// Token: 0x02001308 RID: 4872
			// (Invoke) Token: 0x0600A265 RID: 41573
			public delegate void OnClickTears(byte _state);
		}

		// Token: 0x02001309 RID: 4873
		[Serializable]
		private class StateInfo : MPCharCtrl.CommonInfo
		{
			// Token: 0x0600A269 RID: 41577 RVA: 0x00428638 File Offset: 0x00426A38
			public override void Init()
			{
				base.Init();
				this.buttonCosState[0].onClick.AddListener(delegate()
				{
					this.OnClickCosState(0);
				});
				this.buttonCosState[1].onClick.AddListener(delegate()
				{
					this.OnClickCosState(1);
				});
				this.buttonCosState[2].onClick.AddListener(delegate()
				{
					this.OnClickCosState(2);
				});
				this.clothingDetailsInfo.Init(this.spriteVisible, new MPCharCtrl.ClothingDetailsInfo.OnClickFunc(this.OnClickClothingDetails));
				this.accessoriesInfo.Init(this.spriteVisible, new MPCharCtrl.AccessoriesInfo.OnClickFunc(this.OnClickAccessories));
				this.liquidInfo.Init(this.spriteVisible, new MPCharCtrl.LiquidInfo.OnClickFunc(this.OnClickLiquid));
				this.otherInfo.Init(this.spriteVisible);
				this.otherInfo.tears.slider.onValueChanged.AddListener(new UnityAction<float>(this.OnValueChangedTears));
				this.otherInfo.cheek.slider.onValueChanged.AddListener(new UnityAction<float>(this.OnValueChangedCheek));
				this.otherInfo.nipple.slider.onValueChanged.AddListener(new UnityAction<float>(this.OnValueChangedNipple));
				this.otherInfo.skin.slider.onValueChanged.AddListener(new UnityAction<float>(this.OnValueChangedSkin));
				this.otherInfo.wet.slider.onValueChanged.AddListener(new UnityAction<float>(this.OnValueChangedWet));
				this.otherInfo.single.toggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChangedSimple));
				this.otherInfo.color.buttons[0].onClick.AddListener(new UnityAction(this.OnClickSimpleColor));
				this.otherInfo.son.toggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChangedSon));
				this.otherInfo.sonLen.slider.onValueChanged.AddListener(new UnityAction<float>(this.OnValueChangedSonLength));
			}

			// Token: 0x0600A26A RID: 41578 RVA: 0x00428868 File Offset: 0x00426C68
			public override void UpdateInfo(OCIChar _char)
			{
				base.UpdateInfo(_char);
				base.isUpdateInfo = true;
				this.clothingDetailsInfo.UpdateInfo(_char);
				this.accessoriesInfo.UpdateInfo(_char);
				this.liquidInfo.UpdateInfo(_char);
				this.otherInfo.UpdateInfo(_char);
				base.isUpdateInfo = false;
			}

			// Token: 0x0600A26B RID: 41579 RVA: 0x004288BA File Offset: 0x00426CBA
			private void OnClickCosState(int _value)
			{
				base.ociChar.SetClothesStateAll(_value);
				this.clothingDetailsInfo.UpdateInfo(base.ociChar);
			}

			// Token: 0x0600A26C RID: 41580 RVA: 0x004288D9 File Offset: 0x00426CD9
			private void OnClickClothingDetails(int _id, byte _state)
			{
				base.ociChar.SetClothesState(_id, _state);
			}

			// Token: 0x0600A26D RID: 41581 RVA: 0x004288E8 File Offset: 0x00426CE8
			private void OnClickAccessories(int _id, bool _flag)
			{
				base.ociChar.ShowAccessory(_id, _flag);
			}

			// Token: 0x0600A26E RID: 41582 RVA: 0x004288F7 File Offset: 0x00426CF7
			private void OnClickLiquid(ChaFileDefine.SiruParts _parts, byte _state)
			{
				base.ociChar.SetSiruFlags(_parts, _state);
			}

			// Token: 0x0600A26F RID: 41583 RVA: 0x00428906 File Offset: 0x00426D06
			private void OnValueChangedTears(float _value)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				base.ociChar.SetTears(_value);
			}

			// Token: 0x0600A270 RID: 41584 RVA: 0x00428920 File Offset: 0x00426D20
			private void OnValueChangedCheek(float _value)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				base.ociChar.SetHohoAkaRate(_value);
			}

			// Token: 0x0600A271 RID: 41585 RVA: 0x0042893A File Offset: 0x00426D3A
			private void OnValueChangedNipple(float _value)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				base.ociChar.SetNipStand(_value);
			}

			// Token: 0x0600A272 RID: 41586 RVA: 0x00428954 File Offset: 0x00426D54
			private void OnValueChangedSkin(float _value)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				base.ociChar.SetTuyaRate(_value);
			}

			// Token: 0x0600A273 RID: 41587 RVA: 0x0042896E File Offset: 0x00426D6E
			private void OnValueChangedWet(float _value)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				base.ociChar.SetWetRate(_value);
			}

			// Token: 0x0600A274 RID: 41588 RVA: 0x00428988 File Offset: 0x00426D88
			private void OnValueChangedSimple(bool _value)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				base.ociChar.SetVisibleSimple(_value);
			}

			// Token: 0x0600A275 RID: 41589 RVA: 0x004289A4 File Offset: 0x00426DA4
			private void OnClickSimpleColor()
			{
				Singleton<Studio>.Instance.colorPalette.Setup("単色", base.ociChar.oiCharInfo.simpleColor, new Action<Color>(this.OnValueChangeSimpleColor), true);
				Singleton<Studio>.Instance.colorPalette.visible = true;
			}

			// Token: 0x0600A276 RID: 41590 RVA: 0x004289F2 File Offset: 0x00426DF2
			private void OnValueChangeSimpleColor(Color _color)
			{
				base.ociChar.SetSimpleColor(_color);
				this.otherInfo.SetSimpleColor(_color);
			}

			// Token: 0x0600A277 RID: 41591 RVA: 0x00428A0C File Offset: 0x00426E0C
			private void OnValueChangedSon(bool _value)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				base.ociChar.SetVisibleSon(_value);
			}

			// Token: 0x0600A278 RID: 41592 RVA: 0x00428A26 File Offset: 0x00426E26
			private void OnValueChangedSonLength(float _value)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				base.ociChar.SetSonLength(_value);
			}

			// Token: 0x04008050 RID: 32848
			public Sprite[] spriteVisible;

			// Token: 0x04008051 RID: 32849
			public Button[] buttonCosState;

			// Token: 0x04008052 RID: 32850
			public MPCharCtrl.ClothingDetailsInfo clothingDetailsInfo = new MPCharCtrl.ClothingDetailsInfo();

			// Token: 0x04008053 RID: 32851
			public MPCharCtrl.AccessoriesInfo accessoriesInfo = new MPCharCtrl.AccessoriesInfo();

			// Token: 0x04008054 RID: 32852
			public MPCharCtrl.LiquidInfo liquidInfo = new MPCharCtrl.LiquidInfo();

			// Token: 0x04008055 RID: 32853
			public MPCharCtrl.OtherInfo otherInfo = new MPCharCtrl.OtherInfo();
		}

		// Token: 0x0200130A RID: 4874
		[Serializable]
		private class FKInfo : MPCharCtrl.CommonInfo
		{
			// Token: 0x0600A27D RID: 41597 RVA: 0x00428A64 File Offset: 0x00426E64
			public override void Init()
			{
				base.Init();
				this.toggleFunction.onValueChanged.AddListener(new UnityAction<bool>(this.OnChangeValueFunction));
				this.toggleHair.onValueChanged.AddListener(delegate(bool b)
				{
					this.OnChangeValueIndividual(OIBoneInfo.BoneGroup.Hair, b);
				});
				this.toggleNeck.onValueChanged.AddListener(delegate(bool b)
				{
					this.OnChangeValueIndividual(OIBoneInfo.BoneGroup.Neck, b);
				});
				this.toggleBreast.onValueChanged.AddListener(delegate(bool b)
				{
					this.OnChangeValueIndividual(OIBoneInfo.BoneGroup.Breast, b);
				});
				this.toggleBody.onValueChanged.AddListener(delegate(bool b)
				{
					this.OnChangeValueIndividual(OIBoneInfo.BoneGroup.Body, b);
				});
				this.toggleRightHand.onValueChanged.AddListener(delegate(bool b)
				{
					this.OnChangeValueIndividual(OIBoneInfo.BoneGroup.RightHand, b);
				});
				this.toggleLeftHand.onValueChanged.AddListener(delegate(bool b)
				{
					this.OnChangeValueIndividual(OIBoneInfo.BoneGroup.LeftHand, b);
				});
				this.toggleSkirt.onValueChanged.AddListener(delegate(bool b)
				{
					this.OnChangeValueIndividual(OIBoneInfo.BoneGroup.Skirt, b);
				});
				this.sliderSize.onValueChanged.AddListener(new UnityAction<float>(this.OnValueChangedSize));
				this.buttonInitSingle[0].onClick.AddListener(delegate()
				{
					this.OnClickInitSingle(OIBoneInfo.BoneGroup.Hair);
				});
				this.buttonInitSingle[1].onClick.AddListener(delegate()
				{
					this.OnClickInitSingle(OIBoneInfo.BoneGroup.Skirt);
				});
				this.toggleVisible.gameObject.SetActive(false);
				this.array = new Toggle[]
				{
					this.toggleHair,
					this.toggleNeck,
					this.toggleBreast,
					this.toggleBody,
					this.toggleRightHand,
					this.toggleLeftHand,
					this.toggleSkirt
				};
			}

			// Token: 0x0600A27E RID: 41598 RVA: 0x00428C0C File Offset: 0x0042700C
			public override void UpdateInfo(OCIChar _char)
			{
				base.UpdateInfo(_char);
				base.isUpdateInfo = true;
				this.toggleFunction.isOn = _char.oiCharInfo.enableFK;
				for (int i = 0; i < this.array.Length; i++)
				{
					this.array[i].isOn = _char.oiCharInfo.activeFK[i];
				}
				this.buttonReflectIK.interactable = _char.oiCharInfo.enableFK;
				this.toggleHair.interactable = (_char.oiCharInfo.sex != 0 || _char.IsFKGroup(OIBoneInfo.BoneGroup.Hair));
				this.toggleBreast.interactable = (_char.oiCharInfo.sex != 0 || _char.IsFKGroup(OIBoneInfo.BoneGroup.Breast));
				this.toggleSkirt.interactable = (_char.oiCharInfo.sex != 0 || _char.IsFKGroup(OIBoneInfo.BoneGroup.Skirt));
				base.isUpdateInfo = false;
			}

			// Token: 0x0600A27F RID: 41599 RVA: 0x00428D11 File Offset: 0x00427111
			private void OnChangeValueFunction(bool _value)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				base.ociChar.ActiveKinematicMode(OICharInfo.KinematicMode.FK, _value, false);
				this.buttonReflectIK.interactable = _value;
			}

			// Token: 0x0600A280 RID: 41600 RVA: 0x00428D39 File Offset: 0x00427139
			private void OnChangeValueIndividual(OIBoneInfo.BoneGroup _group, bool _value)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				base.ociChar.ActiveFK(_group, _value, false);
			}

			// Token: 0x0600A281 RID: 41601 RVA: 0x00428D58 File Offset: 0x00427158
			private void OnValueChangedSize(float _value)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				int count = base.ociChar.listBones.Count;
				for (int i = 0; i < count; i++)
				{
					base.ociChar.listBones[i].scaleRate = _value;
				}
			}

			// Token: 0x0600A282 RID: 41602 RVA: 0x00428DAB File Offset: 0x004271AB
			private void OnClickInitSingle(OIBoneInfo.BoneGroup _group)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				base.ociChar.InitFKBone(_group);
			}

			// Token: 0x04008056 RID: 32854
			public Toggle toggleFunction;

			// Token: 0x04008057 RID: 32855
			public Toggle toggleHair;

			// Token: 0x04008058 RID: 32856
			public Toggle toggleNeck;

			// Token: 0x04008059 RID: 32857
			public Toggle toggleBreast;

			// Token: 0x0400805A RID: 32858
			public Toggle toggleBody;

			// Token: 0x0400805B RID: 32859
			public Toggle toggleRightHand;

			// Token: 0x0400805C RID: 32860
			public Toggle toggleLeftHand;

			// Token: 0x0400805D RID: 32861
			public Toggle toggleSkirt;

			// Token: 0x0400805E RID: 32862
			public Slider sliderSize;

			// Token: 0x0400805F RID: 32863
			public Button buttonAnime;

			// Token: 0x04008060 RID: 32864
			public Button buttonReflectIK;

			// Token: 0x04008061 RID: 32865
			public Button[] buttonAnimeSingle;

			// Token: 0x04008062 RID: 32866
			public Button[] buttonInitSingle;

			// Token: 0x04008063 RID: 32867
			[Space]
			public Toggle toggleVisible;

			// Token: 0x04008064 RID: 32868
			private Toggle[] array;
		}

		// Token: 0x0200130B RID: 4875
		[Serializable]
		private class IKInfo : MPCharCtrl.CommonInfo
		{
			// Token: 0x0600A28D RID: 41613 RVA: 0x00428E40 File Offset: 0x00427240
			public override void Init()
			{
				base.Init();
				this.toggleFunction.onValueChanged.AddListener(new UnityAction<bool>(this.OnChangeValueFunction));
				this.toggleAll.onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChangedAll));
				this.toggleBody.onValueChanged.AddListener(delegate(bool b)
				{
					this.OnChangeValueIndividual(0, b);
				});
				this.toggleRightLeg.onValueChanged.AddListener(delegate(bool b)
				{
					this.OnChangeValueIndividual(1, b);
				});
				this.toggleLeftLeg.onValueChanged.AddListener(delegate(bool b)
				{
					this.OnChangeValueIndividual(2, b);
				});
				this.toggleRightHand.onValueChanged.AddListener(delegate(bool b)
				{
					this.OnChangeValueIndividual(3, b);
				});
				this.toggleLeftHand.onValueChanged.AddListener(delegate(bool b)
				{
					this.OnChangeValueIndividual(4, b);
				});
				this.sliderSize.onValueChanged.AddListener(new UnityAction<float>(this.OnValueChangedSize));
				this.toggleVisible.gameObject.SetActive(false);
				this.array = new Toggle[]
				{
					this.toggleBody,
					this.toggleRightLeg,
					this.toggleLeftLeg,
					this.toggleRightHand,
					this.toggleLeftHand
				};
			}

			// Token: 0x0600A28E RID: 41614 RVA: 0x00428F80 File Offset: 0x00427380
			public override void UpdateInfo(OCIChar _char)
			{
				base.UpdateInfo(_char);
				base.isUpdateInfo = true;
				this.toggleFunction.isOn = base.ociChar.oiCharInfo.enableIK;
				bool flag = false;
				for (int i = 0; i < 5; i++)
				{
					this.array[i].isOn = base.ociChar.oiCharInfo.activeIK[i];
					flag |= base.ociChar.oiCharInfo.activeIK[i];
				}
				this.toggleAll.isOn = flag;
				this.buttonReflectFK.interactable = base.ociChar.oiCharInfo.enableIK;
				this.toggleAll.interactable = base.ociChar.oiCharInfo.enableIK;
				for (int j = 0; j < 5; j++)
				{
					this.array[j].interactable = base.ociChar.oiCharInfo.enableIK;
				}
				base.isUpdateInfo = false;
			}

			// Token: 0x0600A28F RID: 41615 RVA: 0x00429078 File Offset: 0x00427478
			private void OnChangeValueFunction(bool _value)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				base.ociChar.ActiveKinematicMode(OICharInfo.KinematicMode.IK, _value, false);
				this.buttonReflectFK.interactable = _value;
				this.toggleAll.interactable = _value;
				for (int i = 0; i < 5; i++)
				{
					this.array[i].interactable = _value;
				}
			}

			// Token: 0x0600A290 RID: 41616 RVA: 0x004290D8 File Offset: 0x004274D8
			private void OnValueChangedAll(bool _value)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				for (int i = 0; i < 5; i++)
				{
					this.array[i].isOn = _value;
				}
			}

			// Token: 0x0600A291 RID: 41617 RVA: 0x00429114 File Offset: 0x00427514
			private void OnChangeValueIndividual(int _no, bool _value)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				base.ociChar.ActiveIK((OIBoneInfo.BoneGroup)(1 << _no), _value, false);
				base.isUpdateInfo = true;
				bool flag = false;
				for (int i = 0; i < 5; i++)
				{
					flag |= base.ociChar.oiCharInfo.activeIK[i];
				}
				this.toggleAll.isOn = flag;
				base.isUpdateInfo = false;
			}

			// Token: 0x0600A292 RID: 41618 RVA: 0x00429184 File Offset: 0x00427584
			private void OnValueChangedSize(float _value)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				int count = base.ociChar.listIKTarget.Count;
				for (int i = 0; i < count; i++)
				{
					base.ociChar.listIKTarget[i].scaleRate = _value;
				}
			}

			// Token: 0x04008065 RID: 32869
			public Toggle toggleFunction;

			// Token: 0x04008066 RID: 32870
			public Toggle toggleAll;

			// Token: 0x04008067 RID: 32871
			public Toggle toggleBody;

			// Token: 0x04008068 RID: 32872
			public Toggle toggleRightHand;

			// Token: 0x04008069 RID: 32873
			public Toggle toggleLeftHand;

			// Token: 0x0400806A RID: 32874
			public Toggle toggleRightLeg;

			// Token: 0x0400806B RID: 32875
			public Toggle toggleLeftLeg;

			// Token: 0x0400806C RID: 32876
			public Slider sliderSize;

			// Token: 0x0400806D RID: 32877
			public Button buttonAnime;

			// Token: 0x0400806E RID: 32878
			public Button buttonReflectFK;

			// Token: 0x0400806F RID: 32879
			public Button[] buttonAnimeSingle;

			// Token: 0x04008070 RID: 32880
			[Space]
			public Toggle toggleVisible;

			// Token: 0x04008071 RID: 32881
			private Toggle[] array;
		}

		// Token: 0x0200130C RID: 4876
		[Serializable]
		private class LookAtInfo : MPCharCtrl.CommonInfo
		{
			// Token: 0x0600A299 RID: 41625 RVA: 0x00429214 File Offset: 0x00427614
			public override void Init()
			{
				base.Init();
				for (int i = 0; i < this.buttonMode.Length; i++)
				{
					int no = i;
					this.buttonMode[i].onClick.AddListener(delegate()
					{
						this.OnClick(no);
					});
				}
				this.sliderSize.onValueChanged.AddListener(new UnityAction<float>(this.OnVauleChangeSize));
			}

			// Token: 0x0600A29A RID: 41626 RVA: 0x00429290 File Offset: 0x00427690
			public override void UpdateInfo(OCIChar _char)
			{
				base.UpdateInfo(_char);
				base.isUpdateInfo = true;
				int eyesLookPtn = base.ociChar.charFileStatus.eyesLookPtn;
				for (int i = 0; i < this.buttonMode.Length; i++)
				{
					this.buttonMode[i].image.color = ((i != eyesLookPtn) ? Color.white : Color.green);
				}
				this.sliderSize.value = _char.lookAtInfo.guideObject.scaleRate;
				this.sliderSize.interactable = (_char.charFileStatus.eyesLookPtn == 4);
				base.isUpdateInfo = false;
			}

			// Token: 0x0600A29B RID: 41627 RVA: 0x00429338 File Offset: 0x00427738
			private void OnClick(int _no)
			{
				int eyesLookPtn = base.ociChar.charFileStatus.eyesLookPtn;
				base.ociChar.ChangeLookEyesPtn(_no, false);
				this.sliderSize.interactable = (_no == 4);
				this.buttonMode[eyesLookPtn].image.color = Color.white;
				this.buttonMode[_no].image.color = Color.green;
			}

			// Token: 0x0600A29C RID: 41628 RVA: 0x004293A0 File Offset: 0x004277A0
			private void OnVauleChangeSize(float _value)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				base.ociChar.lookAtInfo.guideObject.scaleRate = _value;
			}

			// Token: 0x04008072 RID: 32882
			public Button[] buttonMode;

			// Token: 0x04008073 RID: 32883
			public Slider sliderSize;
		}

		// Token: 0x0200130D RID: 4877
		[Serializable]
		private class NeckInfo : MPCharCtrl.CommonInfo
		{
			// Token: 0x0600A29E RID: 41630 RVA: 0x00429400 File Offset: 0x00427800
			public override void Init()
			{
				base.Init();
				for (int i = 0; i < this.buttonMode.Length; i++)
				{
					int no = i;
					this.buttonMode[i].onClick.AddListener(delegate()
					{
						this.OnClick(no);
					});
				}
			}

			// Token: 0x0600A29F RID: 41631 RVA: 0x00429460 File Offset: 0x00427860
			public override void UpdateInfo(OCIChar _char)
			{
				base.UpdateInfo(_char);
				base.isUpdateInfo = true;
				int no = base.ociChar.charFileStatus.neckLookPtn;
				no = Array.FindIndex<int>(this.patterns, (int v) => v == no);
				for (int i = 0; i < this.buttonMode.Length; i++)
				{
					this.buttonMode[i].image.color = ((i != no) ? Color.white : Color.green);
					this.buttonMode[i].interactable = (!_char.oiCharInfo.enableFK || !_char.oiCharInfo.activeFK[1]);
				}
				base.isUpdateInfo = false;
			}

			// Token: 0x0600A2A0 RID: 41632 RVA: 0x00429534 File Offset: 0x00427934
			private void OnClick(int _idx)
			{
				int old = base.ociChar.charFileStatus.neckLookPtn;
				old = Array.FindIndex<int>(this.patterns, (int v) => v == old);
				base.ociChar.ChangeLookNeckPtn(this.patterns[_idx]);
				this.buttonMode[old].image.color = Color.white;
				this.buttonMode[_idx].image.color = Color.green;
			}

			// Token: 0x04008074 RID: 32884
			public Button[] buttonMode;

			// Token: 0x04008075 RID: 32885
			private int[] patterns = new int[]
			{
				0,
				1,
				3,
				4
			};
		}

		// Token: 0x0200130E RID: 4878
		[Serializable]
		public class PatternInfo
		{
			// Token: 0x1700223B RID: 8763
			// (get) Token: 0x0600A2A2 RID: 41634 RVA: 0x0042961C File Offset: 0x00427A1C
			// (set) Token: 0x0600A2A3 RID: 41635 RVA: 0x00429624 File Offset: 0x00427A24
			public int ptn
			{
				get
				{
					return this.m_Ptn;
				}
				set
				{
					if (Utility.SetStruct<int>(ref this.m_Ptn, value))
					{
						this.textPtn.text = string.Format("{00:0}", this.m_Ptn);
					}
				}
			}

			// Token: 0x1700223C RID: 8764
			// (get) Token: 0x0600A2A4 RID: 41636 RVA: 0x00429657 File Offset: 0x00427A57
			// (set) Token: 0x0600A2A5 RID: 41637 RVA: 0x0042965F File Offset: 0x00427A5F
			public int num { get; set; }

			// Token: 0x0600A2A6 RID: 41638 RVA: 0x00429668 File Offset: 0x00427A68
			public void Init()
			{
				this.buttons[0].onClick.AddListener(delegate()
				{
					this.OnClick(-1);
				});
				this.buttons[1].onClick.AddListener(delegate()
				{
					this.OnClick(1);
				});
			}

			// Token: 0x0600A2A7 RID: 41639 RVA: 0x004296A8 File Offset: 0x00427AA8
			private void OnClick(int _add)
			{
				int num = this.m_Ptn + _add;
				this.ptn = ((num >= 0) ? (num % this.num) : (this.num - 1));
				if (this.onClickFunc != null)
				{
					this.onClickFunc(this.m_Ptn);
				}
			}

			// Token: 0x04008076 RID: 32886
			public Button[] buttons = new Button[2];

			// Token: 0x04008077 RID: 32887
			public TextMeshProUGUI textPtn;

			// Token: 0x04008078 RID: 32888
			private int m_Ptn = -1;

			// Token: 0x0400807A RID: 32890
			public MPCharCtrl.PatternInfo.OnClickFunc onClickFunc;

			// Token: 0x0200130F RID: 4879
			// (Invoke) Token: 0x0600A2AB RID: 41643
			public delegate void OnClickFunc(int _no);
		}

		// Token: 0x02001310 RID: 4880
		[Serializable]
		private class EtcInfo : MPCharCtrl.CommonInfo
		{
			// Token: 0x0600A2AF RID: 41647 RVA: 0x00429738 File Offset: 0x00427B38
			public override void Init()
			{
				base.Init();
				this.piEyebrows.Init();
				MPCharCtrl.PatternInfo patternInfo = this.piEyebrows;
				patternInfo.onClickFunc = (MPCharCtrl.PatternInfo.OnClickFunc)Delegate.Combine(patternInfo.onClickFunc, new MPCharCtrl.PatternInfo.OnClickFunc(this.ChangeEyebrowsPtn));
				this.piEyes.Init();
				MPCharCtrl.PatternInfo patternInfo2 = this.piEyes;
				patternInfo2.onClickFunc = (MPCharCtrl.PatternInfo.OnClickFunc)Delegate.Combine(patternInfo2.onClickFunc, new MPCharCtrl.PatternInfo.OnClickFunc(this.ChangeEyesPtn));
				this.sliderEyesOpen.onValueChanged.AddListener(new UnityAction<float>(this.OnValueChangedEyesOpen));
				this.toggleBlink.onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChangedEyesBlink));
				this.piMouth.Init();
				MPCharCtrl.PatternInfo patternInfo3 = this.piMouth;
				patternInfo3.onClickFunc = (MPCharCtrl.PatternInfo.OnClickFunc)Delegate.Combine(patternInfo3.onClickFunc, new MPCharCtrl.PatternInfo.OnClickFunc(this.ChangeMouthPtn));
				this.sliderMouthOpen.onValueChanged.AddListener(new UnityAction<float>(this.OnValueChangedMouthOpen));
				this.toggleLipSync.onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChangedLipSync));
			}

			// Token: 0x0600A2B0 RID: 41648 RVA: 0x00429854 File Offset: 0x00427C54
			public override void UpdateInfo(OCIChar _char)
			{
				base.UpdateInfo(_char);
				base.isUpdateInfo = true;
				FBSCtrlEyebrow eyebrowCtrl = _char.charInfo.eyebrowCtrl;
				Dictionary<int, ListInfoBase> categoryInfo = Singleton<Character>.Instance.chaListCtrl.GetCategoryInfo((_char.sex != 0) ? ChaListDefine.CategoryNo.custom_eyebrow_f : ChaListDefine.CategoryNo.custom_eyebrow_m);
				this.eyebrowsKeys = categoryInfo.Keys.ToArray<int>();
				this.piEyebrows.num = categoryInfo.Count;
				this.piEyebrows.ptn = Mathf.Clamp(Array.FindIndex<int>(this.eyebrowsKeys, (int _i) => _i == _char.charInfo.GetEyebrowPtn()), 0, this.eyebrowsKeys.Length - 1);
				FBSCtrlEyes eyesCtrl = _char.charInfo.eyesCtrl;
				Dictionary<int, ListInfoBase> categoryInfo2 = Singleton<Character>.Instance.chaListCtrl.GetCategoryInfo((_char.sex != 0) ? ChaListDefine.CategoryNo.custom_eye_f : ChaListDefine.CategoryNo.custom_eye_m);
				this.eyesKeys = categoryInfo2.Keys.ToArray<int>();
				this.piEyes.num = categoryInfo2.Count;
				this.piEyes.ptn = Mathf.Clamp(Array.FindIndex<int>(this.eyesKeys, (int _i) => _i == _char.charInfo.GetEyesPtn()), 0, this.eyesKeys.Length - 1);
				this.sliderEyesOpen.value = _char.charFileStatus.eyesOpenMax;
				this.toggleBlink.isOn = _char.charFileStatus.eyesBlink;
				FBSCtrlMouth mouthCtrl = _char.charInfo.mouthCtrl;
				this.piMouth.num = mouthCtrl.GetMaxPtn();
				this.piMouth.ptn = _char.charInfo.GetMouthPtn();
				this.sliderMouthOpen.value = mouthCtrl.FixedRate;
				this.toggleLipSync.isOn = _char.oiCharInfo.lipSync;
				base.isUpdateInfo = false;
			}

			// Token: 0x0600A2B1 RID: 41649 RVA: 0x00429A55 File Offset: 0x00427E55
			private void ChangeEyebrowsPtn(int _no)
			{
				base.ociChar.charInfo.ChangeEyebrowPtn(this.eyebrowsKeys[Mathf.Clamp(_no, 0, this.eyebrowsKeys.Length - 1)], true);
			}

			// Token: 0x0600A2B2 RID: 41650 RVA: 0x00429A80 File Offset: 0x00427E80
			private void ChangeEyesPtn(int _no)
			{
				base.ociChar.charInfo.ChangeEyesPtn(this.eyesKeys[Mathf.Clamp(_no, 0, this.eyesKeys.Length - 1)], true);
			}

			// Token: 0x0600A2B3 RID: 41651 RVA: 0x00429AAB File Offset: 0x00427EAB
			private void OnValueChangedEyesOpen(float _value)
			{
				base.ociChar.ChangeEyesOpen(_value);
			}

			// Token: 0x0600A2B4 RID: 41652 RVA: 0x00429AB9 File Offset: 0x00427EB9
			private void OnValueChangedEyesBlink(bool _value)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				base.ociChar.ChangeBlink(_value);
			}

			// Token: 0x0600A2B5 RID: 41653 RVA: 0x00429AD3 File Offset: 0x00427ED3
			private void ChangeMouthPtn(int _no)
			{
				base.ociChar.charInfo.ChangeMouthPtn(_no, true);
			}

			// Token: 0x0600A2B6 RID: 41654 RVA: 0x00429AE7 File Offset: 0x00427EE7
			private void OnValueChangedMouthOpen(float _value)
			{
				base.ociChar.ChangeMouthOpen(_value);
			}

			// Token: 0x0600A2B7 RID: 41655 RVA: 0x00429AF5 File Offset: 0x00427EF5
			private void OnValueChangedLipSync(bool _value)
			{
				if (base.isUpdateInfo)
				{
					return;
				}
				base.ociChar.ChangeLipSync(_value);
			}

			// Token: 0x0400807B RID: 32891
			public MPCharCtrl.PatternInfo piEyebrows = new MPCharCtrl.PatternInfo();

			// Token: 0x0400807C RID: 32892
			public MPCharCtrl.PatternInfo piEyes = new MPCharCtrl.PatternInfo();

			// Token: 0x0400807D RID: 32893
			public Slider sliderEyesOpen;

			// Token: 0x0400807E RID: 32894
			public Toggle toggleBlink;

			// Token: 0x0400807F RID: 32895
			public MPCharCtrl.PatternInfo piMouth = new MPCharCtrl.PatternInfo();

			// Token: 0x04008080 RID: 32896
			public Slider sliderMouthOpen;

			// Token: 0x04008081 RID: 32897
			public Toggle toggleLipSync;

			// Token: 0x04008082 RID: 32898
			private int[] eyebrowsKeys;

			// Token: 0x04008083 RID: 32899
			private int[] eyesKeys;
		}

		// Token: 0x02001311 RID: 4881
		[Serializable]
		private class HandInfo : MPCharCtrl.CommonInfo
		{
			// Token: 0x0600A2B9 RID: 41657 RVA: 0x00429B60 File Offset: 0x00427F60
			public override void Init()
			{
				base.Init();
				this.piRightHand.Init();
				MPCharCtrl.PatternInfo patternInfo = this.piRightHand;
				patternInfo.onClickFunc = (MPCharCtrl.PatternInfo.OnClickFunc)Delegate.Combine(patternInfo.onClickFunc, new MPCharCtrl.PatternInfo.OnClickFunc(this.ChangeRightHandAnime));
				this.piLeftHand.Init();
				MPCharCtrl.PatternInfo patternInfo2 = this.piLeftHand;
				patternInfo2.onClickFunc = (MPCharCtrl.PatternInfo.OnClickFunc)Delegate.Combine(patternInfo2.onClickFunc, new MPCharCtrl.PatternInfo.OnClickFunc(this.ChangeLeftHandAnime));
			}

			// Token: 0x0600A2BA RID: 41658 RVA: 0x00429BD8 File Offset: 0x00427FD8
			public override void UpdateInfo(OCIChar _char)
			{
				base.UpdateInfo(_char);
				base.isUpdateInfo = true;
				this.piRightHand.num = _char.HandAnimeNum + 1;
				this.piRightHand.ptn = _char.oiCharInfo.handPtn[1];
				this.piLeftHand.num = _char.HandAnimeNum + 1;
				this.piLeftHand.ptn = _char.oiCharInfo.handPtn[0];
				base.isUpdateInfo = false;
			}

			// Token: 0x0600A2BB RID: 41659 RVA: 0x00429C50 File Offset: 0x00428050
			private void ChangeLeftHandAnime(int _no)
			{
				base.ociChar.ChangeHandAnime(0, _no);
			}

			// Token: 0x0600A2BC RID: 41660 RVA: 0x00429C5F File Offset: 0x0042805F
			private void ChangeRightHandAnime(int _no)
			{
				base.ociChar.ChangeHandAnime(1, _no);
			}

			// Token: 0x04008084 RID: 32900
			public MPCharCtrl.PatternInfo piRightHand = new MPCharCtrl.PatternInfo();

			// Token: 0x04008085 RID: 32901
			public MPCharCtrl.PatternInfo piLeftHand = new MPCharCtrl.PatternInfo();
		}

		// Token: 0x02001312 RID: 4882
		[Serializable]
		private class PoseInfo : MPCharCtrl.CommonInfo
		{
			// Token: 0x1700223D RID: 8765
			// (set) Token: 0x0600A2BE RID: 41662 RVA: 0x00429C76 File Offset: 0x00428076
			public override bool active
			{
				set
				{
					this.pauseRegistrationList.active = value;
				}
			}

			// Token: 0x0600A2BF RID: 41663 RVA: 0x00429C84 File Offset: 0x00428084
			public override void Init()
			{
				base.Init();
			}

			// Token: 0x0600A2C0 RID: 41664 RVA: 0x00429C8C File Offset: 0x0042808C
			public override void UpdateInfo(OCIChar _char)
			{
				base.UpdateInfo(_char);
				base.isUpdateInfo = true;
				this.pauseRegistrationList.ociChar = _char;
				base.isUpdateInfo = false;
			}

			// Token: 0x04008086 RID: 32902
			public PauseRegistrationList pauseRegistrationList;
		}

		// Token: 0x02001313 RID: 4883
		[Serializable]
		private class CostumeInfo : MPCharCtrl.CommonInfo
		{
			// Token: 0x0600A2C2 RID: 41666 RVA: 0x00429CCC File Offset: 0x004280CC
			public override void Init()
			{
				base.Init();
				this.buttonSort[0].onClick.AddListener(delegate()
				{
					this.OnClickSort(0);
				});
				this.buttonSort[1].onClick.AddListener(delegate()
				{
					this.OnClickSort(1);
				});
				this.buttonLoad.onClick.AddListener(new UnityAction(this.OnClickLoad));
				this.sex = -1;
			}

			// Token: 0x0600A2C3 RID: 41667 RVA: 0x00429D3E File Offset: 0x0042813E
			public override void UpdateInfo(OCIChar _char)
			{
				base.UpdateInfo(_char);
				base.isUpdateInfo = true;
				this.InitList(_char.oiCharInfo.sex);
				base.isUpdateInfo = false;
			}

			// Token: 0x0600A2C4 RID: 41668 RVA: 0x00429D68 File Offset: 0x00428168
			private void InitList(int _sex)
			{
				if (this.sex == _sex)
				{
					return;
				}
				this.fileSort.DeleteAllNode();
				this.InitFileList(_sex);
				int count = this.fileSort.cfiList.Count;
				for (int i = 0; i < count; i++)
				{
					CharaFileInfo info = this.fileSort.cfiList[i];
					info.index = i;
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefabNode);
					if (!gameObject.activeSelf)
					{
						gameObject.SetActive(true);
					}
					gameObject.transform.SetParent(this.fileSort.root, false);
					info.node = gameObject.GetComponent<ListNode>();
					info.button = gameObject.GetComponent<Button>();
					info.node.AddActionToButton(delegate
					{
						this.OnSelect(info.index);
					});
					info.node.text = info.name;
					info.node.listEnterAction.Add(delegate
					{
						this.LoadImage(info.index);
					});
				}
				this.sex = _sex;
				this.fileSort.Sort(0, false);
				this.buttonLoad.interactable = false;
				this.imageThumbnail.color = Color.clear;
			}

			// Token: 0x0600A2C5 RID: 41669 RVA: 0x00429EC8 File Offset: 0x004282C8
			private void InitFileList(int _sex)
			{
				List<string> list = new List<string>();
				string folder = UserData.Path + ((_sex != 0) ? "coordinate/female/" : "coordinate/male/");
				Illusion.Utils.File.GetAllFiles(folder, "*.png", ref list);
				this.fileSort.cfiList.Clear();
				int count = list.Count;
				ChaFileCoordinate chaFileCoordinate = new ChaFileCoordinate();
				for (int i = 0; i < count; i++)
				{
					if (chaFileCoordinate.LoadFile(list[i]))
					{
						this.fileSort.cfiList.Add(new CharaFileInfo(list[i], chaFileCoordinate.coordinateName)
						{
							time = File.GetLastWriteTime(list[i])
						});
					}
				}
			}

			// Token: 0x0600A2C6 RID: 41670 RVA: 0x00429F8E File Offset: 0x0042838E
			private void OnSelect(int _idx)
			{
				if (this.fileSort.select == _idx)
				{
					return;
				}
				this.fileSort.select = _idx;
				this.buttonLoad.interactable = true;
			}

			// Token: 0x0600A2C7 RID: 41671 RVA: 0x00429FBC File Offset: 0x004283BC
			private void LoadImage(int _idx)
			{
				CharaFileInfo charaFileInfo = this.fileSort.cfiList[_idx];
				this.imageThumbnail.texture = PngAssist.LoadTexture(charaFileInfo.file);
				this.imageThumbnail.color = Color.white;
				UnityEngine.Resources.UnloadUnusedAssets();
				GC.Collect();
			}

			// Token: 0x0600A2C8 RID: 41672 RVA: 0x0042A00C File Offset: 0x0042840C
			private void OnClickLoad()
			{
				base.ociChar.LoadClothesFile(this.fileSort.selectPath);
			}

			// Token: 0x0600A2C9 RID: 41673 RVA: 0x0042A024 File Offset: 0x00428424
			private void OnClickSort(int _type)
			{
				this.fileSort.select = -1;
				this.buttonLoad.interactable = false;
				this.fileSort.Sort(_type);
			}

			// Token: 0x04008087 RID: 32903
			public CharaFileSort fileSort = new CharaFileSort();

			// Token: 0x04008088 RID: 32904
			public GameObject prefabNode;

			// Token: 0x04008089 RID: 32905
			public RawImage imageThumbnail;

			// Token: 0x0400808A RID: 32906
			public Button[] buttonSort;

			// Token: 0x0400808B RID: 32907
			public Button buttonLoad;

			// Token: 0x0400808C RID: 32908
			private int sex = -1;
		}

		// Token: 0x02001314 RID: 4884
		[Serializable]
		private class JointInfo : MPCharCtrl.CommonInfo
		{
			// Token: 0x0600A2CD RID: 41677 RVA: 0x0042A09C File Offset: 0x0042849C
			public override void Init()
			{
				base.Init();
				for (int i = 0; i < this.toggles.Length; i++)
				{
					int idx = i;
					this.toggles[i].onValueChanged.AddListener(delegate(bool b)
					{
						this.OnValueChanged(idx, b);
					});
				}
			}

			// Token: 0x0600A2CE RID: 41678 RVA: 0x0042A0FC File Offset: 0x004284FC
			public override void UpdateInfo(OCIChar _char)
			{
				base.UpdateInfo(_char);
				base.isUpdateInfo = true;
				for (int i = 0; i < this.toggles.Length; i++)
				{
					this.toggles[i].isOn = base.ociChar.oiCharInfo.expression[i];
				}
				base.isUpdateInfo = false;
			}

			// Token: 0x0600A2CF RID: 41679 RVA: 0x0042A156 File Offset: 0x00428556
			private void OnValueChanged(int _group, bool _value)
			{
				base.ociChar.EnableExpressionCategory(_group, _value);
			}

			// Token: 0x0400808D RID: 32909
			public Toggle[] toggles;
		}
	}
}
