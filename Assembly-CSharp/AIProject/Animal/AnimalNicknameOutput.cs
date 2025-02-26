using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Manager;
using UniRx;
using UnityEngine;
using UnityEx;

namespace AIProject.Animal
{
	// Token: 0x02000B89 RID: 2953
	public class AnimalNicknameOutput : MonoBehaviour
	{
		// Token: 0x17001034 RID: 4148
		// (get) Token: 0x060057DC RID: 22492 RVA: 0x0025DF78 File Offset: 0x0025C378
		public CanvasGroup CanvasGroup
		{
			[CompilerGenerated]
			get
			{
				return this._canvasGroup;
			}
		}

		// Token: 0x17001035 RID: 4149
		// (get) Token: 0x060057DD RID: 22493 RVA: 0x0025DF80 File Offset: 0x0025C380
		// (set) Token: 0x060057DE RID: 22494 RVA: 0x0025DFA8 File Offset: 0x0025C3A8
		public float CanvasAlpha
		{
			get
			{
				return (!(this._canvasGroup != null)) ? 0f : this._canvasGroup.alpha;
			}
			set
			{
				if (this._canvasGroup != null)
				{
					this._canvasGroup.alpha = value;
				}
			}
		}

		// Token: 0x060057DF RID: 22495 RVA: 0x0025DFC8 File Offset: 0x0025C3C8
		private void Awake()
		{
			Observable.EveryUpdate().TakeUntilDestroy(this).SkipWhile((long _) => Map.GetCameraComponent() == null).Take(1).Subscribe(delegate(long _)
			{
				if (!Singleton<Map>.IsInstance())
				{
					return;
				}
				this._player = Map.GetPlayer();
				this._camera = Map.GetCameraComponent(this._player);
				if (this._player != null && this._camera != null)
				{
					Observable.EveryUpdate().TakeUntilDestroy(this).TakeUntilDestroy(this._player).TakeUntilDestroy(this._camera).Subscribe(delegate(long __)
					{
						this.OnUpdate();
					});
					Observable.Interval(TimeSpan.FromSeconds((double)this._elmRefreshInterval)).TakeUntilDestroy(this).TakeUntilDestroy(this._player).TakeUntilDestroy(this._camera).Subscribe(delegate(long __)
					{
						this._elmList.RemoveAll((INicknameObject x) => x == null);
						this._showList.RemoveAll((INicknameObject x) => x == null);
						this._hideList.RemoveAll((INicknameObject x) => x == null);
						List<int> list = ListPool<int>.Get();
						foreach (KeyValuePair<int, UnityEx.ValueTuple<INicknameObject, AnimalNicknameUI>> keyValuePair in this._showTable)
						{
							UnityEx.ValueTuple<INicknameObject, AnimalNicknameUI> value = keyValuePair.Value;
							if (value.Item1 == null)
							{
								this.ReturnUI(value.Item2);
								list.Add(keyValuePair.Key);
							}
						}
						foreach (int item in list)
						{
							list.Remove(item);
						}
						ListPool<int>.Release(list);
					});
				}
			});
		}

		// Token: 0x060057E0 RID: 22496 RVA: 0x0025E01C File Offset: 0x0025C41C
		private void OnUpdate()
		{
			List<INicknameObject> list = ListPool<INicknameObject>.Get();
			List<INicknameObject> list2 = ListPool<INicknameObject>.Get();
			for (int i = 0; i < this._hideList.Count; i++)
			{
				INicknameObject nicknameObject = this._hideList[i];
				if (nicknameObject == null)
				{
					this._hideList.RemoveAt(i);
					i--;
				}
				else if (this.ShowState(nicknameObject))
				{
					list2.Add(nicknameObject);
					this._hideList.RemoveAt(i);
					i--;
				}
			}
			for (int j = 0; j < this._showList.Count; j++)
			{
				INicknameObject nicknameObject2 = this._showList[j];
				if (nicknameObject2 == null)
				{
					this._showList.RemoveAt(j);
					j--;
				}
				else if (this.HideState(nicknameObject2))
				{
					list.Add(nicknameObject2);
					this._showList.RemoveAt(j);
					j--;
				}
			}
			for (int k = 0; k < list2.Count; k++)
			{
				INicknameObject nicknameObject3 = list2[k];
				AnimalNicknameUI ui = this.GetUI(nicknameObject3);
				int instanceID = nicknameObject3.InstanceID;
				this._showTable[instanceID] = new UnityEx.ValueTuple<INicknameObject, AnimalNicknameUI>(nicknameObject3, ui);
			}
			if (!list2.IsNullOrEmpty<INicknameObject>())
			{
				this._showList.AddRange(list2);
			}
			for (int l = 0; l < list.Count; l++)
			{
				INicknameObject nicknameObject4 = list[l];
				int instanceID2 = nicknameObject4.InstanceID;
				UnityEx.ValueTuple<INicknameObject, AnimalNicknameUI> valueTuple;
				if (this._showTable.TryGetValue(instanceID2, out valueTuple))
				{
					this.ReturnUI(valueTuple.Item2);
					this._showTable.Remove(instanceID2);
				}
			}
			if (!list.IsNullOrEmpty<INicknameObject>())
			{
				this._hideList.AddRange(list);
			}
			ListPool<INicknameObject>.Release(list);
			ListPool<INicknameObject>.Release(list2);
		}

		// Token: 0x060057E1 RID: 22497 RVA: 0x0025E204 File Offset: 0x0025C604
		public void AddElement(INicknameObject addElm)
		{
			if (addElm == null)
			{
				return;
			}
			if (this._elmList.Contains(addElm))
			{
				return;
			}
			this._elmList.Add(addElm);
			this._hideList.Add(addElm);
			addElm.ChangeNickNameEvent = delegate()
			{
				this.Rename(addElm);
			};
		}

		// Token: 0x060057E2 RID: 22498 RVA: 0x0025E284 File Offset: 0x0025C684
		public void RemoveElement(INicknameObject remelm)
		{
			if (remelm == null)
			{
				return;
			}
			this._showList.Remove(remelm);
			this._hideList.Remove(remelm);
			if (this._showTable.ContainsKey(remelm.InstanceID))
			{
				this.ReturnUI(this._showTable[remelm.InstanceID].Item2);
				this._showTable.Remove(remelm.InstanceID);
			}
			this._elmList.Remove(remelm);
			remelm.ChangeNickNameEvent = null;
		}

		// Token: 0x060057E3 RID: 22499 RVA: 0x0025E310 File Offset: 0x0025C710
		public bool ShowState(INicknameObject elm)
		{
			if (elm == null)
			{
				return false;
			}
			Transform nicknameRoot = elm.NicknameRoot;
			if (nicknameRoot == null)
			{
				return false;
			}
			if (!elm.NicknameEnabled)
			{
				return false;
			}
			if ((nicknameRoot.position - this._player.Position).sqrMagnitude > this._showDistance * this._showDistance)
			{
				return false;
			}
			Transform transform = this._camera.transform;
			return Vector3.Angle(transform.forward, nicknameRoot.position - transform.position) * 2f <= this._showAngle;
		}

		// Token: 0x060057E4 RID: 22500 RVA: 0x0025E3BC File Offset: 0x0025C7BC
		public bool HideState(INicknameObject elm)
		{
			if (elm == null)
			{
				return true;
			}
			Transform nicknameRoot = elm.NicknameRoot;
			if (nicknameRoot == null)
			{
				return true;
			}
			if (!elm.NicknameEnabled)
			{
				return true;
			}
			bool flag = this._hideDistance * this._hideDistance < (nicknameRoot.position - this._player.Position).sqrMagnitude;
			if (flag)
			{
				return true;
			}
			Transform transform = this._camera.transform;
			return this._hideAngle < Vector3.Angle(transform.forward, nicknameRoot.position - transform.position) * 2f;
		}

		// Token: 0x060057E5 RID: 22501 RVA: 0x0025E460 File Offset: 0x0025C860
		private void Rename(INicknameObject elm)
		{
			if (elm == null)
			{
				return;
			}
			UnityEx.ValueTuple<INicknameObject, AnimalNicknameUI> valueTuple;
			if (this._showTable.TryGetValue(elm.InstanceID, out valueTuple))
			{
				AnimalNicknameUI item = valueTuple.Item2;
				if (item != null)
				{
					item.Nickname = elm.Nickname;
				}
			}
		}

		// Token: 0x060057E6 RID: 22502 RVA: 0x0025E4AC File Offset: 0x0025C8AC
		private AnimalNicknameUI GetUI(INicknameObject elm)
		{
			AnimalNicknameUI animalNicknameUI = this._uiPool.PopFront<AnimalNicknameUI>();
			if (animalNicknameUI == null)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this._prefab, base.transform);
				animalNicknameUI = gameObject.GetComponent<AnimalNicknameUI>();
			}
			animalNicknameUI.Nickname = elm.Nickname;
			animalNicknameUI.TargetObject = elm.NicknameRoot;
			animalNicknameUI.TargetCamera = this._camera;
			animalNicknameUI.PlayFadeIn(this._fadeTime);
			return animalNicknameUI;
		}

		// Token: 0x060057E7 RID: 22503 RVA: 0x0025E51B File Offset: 0x0025C91B
		private void ReturnUI(AnimalNicknameUI ui)
		{
			if (ui == null)
			{
				return;
			}
			if (!this._uiPool.Contains(ui))
			{
				ui.PlayFadeOut(this._fadeTime);
				this._uiPool.Add(ui);
			}
		}

		// Token: 0x040050C2 RID: 20674
		[SerializeField]
		private CanvasGroup _canvasGroup;

		// Token: 0x040050C3 RID: 20675
		[SerializeField]
		private float _showDistance = 15f;

		// Token: 0x040050C4 RID: 20676
		[SerializeField]
		private float _hideDistance = 20f;

		// Token: 0x040050C5 RID: 20677
		[SerializeField]
		private float _showAngle = 180f;

		// Token: 0x040050C6 RID: 20678
		[SerializeField]
		private float _hideAngle = 200f;

		// Token: 0x040050C7 RID: 20679
		[SerializeField]
		private float _fadeTime = 0.25f;

		// Token: 0x040050C8 RID: 20680
		[SerializeField]
		private float _elmRefreshInterval = 1f;

		// Token: 0x040050C9 RID: 20681
		[SerializeField]
		private GameObject _prefab;

		// Token: 0x040050CA RID: 20682
		private List<AnimalNicknameUI> _uiPool = new List<AnimalNicknameUI>();

		// Token: 0x040050CB RID: 20683
		private List<INicknameObject> _elmList = new List<INicknameObject>();

		// Token: 0x040050CC RID: 20684
		private List<INicknameObject> _showList = new List<INicknameObject>();

		// Token: 0x040050CD RID: 20685
		private List<INicknameObject> _hideList = new List<INicknameObject>();

		// Token: 0x040050CE RID: 20686
		private Dictionary<int, UnityEx.ValueTuple<INicknameObject, AnimalNicknameUI>> _showTable = new Dictionary<int, UnityEx.ValueTuple<INicknameObject, AnimalNicknameUI>>();

		// Token: 0x040050CF RID: 20687
		private PlayerActor _player;

		// Token: 0x040050D0 RID: 20688
		private Camera _camera;
	}
}
