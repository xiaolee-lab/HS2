using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using AIProject.Animal;
using AIProject.SaveData;
using Manager;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using UnityEx;

namespace AIProject.UI
{
	// Token: 0x02000FDD RID: 4061
	[RequireComponent(typeof(CanvasGroup))]
	[RequireComponent(typeof(RectTransform))]
	public class TutorialGroupListUI : MenuUIBehaviour
	{
		// Token: 0x17001D9D RID: 7581
		// (get) Token: 0x06008770 RID: 34672 RVA: 0x00387A98 File Offset: 0x00385E98
		public Button CloseButton
		{
			[CompilerGenerated]
			get
			{
				return this._closeButton;
			}
		}

		// Token: 0x17001D9E RID: 7582
		// (get) Token: 0x06008771 RID: 34673 RVA: 0x00387AA0 File Offset: 0x00385EA0
		// (set) Token: 0x06008772 RID: 34674 RVA: 0x00387AC8 File Offset: 0x00385EC8
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

		// Token: 0x17001D9F RID: 7583
		// (get) Token: 0x06008773 RID: 34675 RVA: 0x00387AE7 File Offset: 0x00385EE7
		// (set) Token: 0x06008774 RID: 34676 RVA: 0x00387AEF File Offset: 0x00385EEF
		public int SelectIndex { get; protected set; } = -1;

		// Token: 0x17001DA0 RID: 7584
		// (get) Token: 0x06008775 RID: 34677 RVA: 0x00387AF8 File Offset: 0x00385EF8
		// (set) Token: 0x06008776 RID: 34678 RVA: 0x00387B00 File Offset: 0x00385F00
		public AIProject.Animal.Tuple<int, Button, Text, bool> SelectedElement { get; protected set; }

		// Token: 0x17001DA1 RID: 7585
		// (get) Token: 0x06008777 RID: 34679 RVA: 0x00387B09 File Offset: 0x00385F09
		public bool InputEnabled
		{
			[CompilerGenerated]
			get
			{
				return base.EnabledInput && Singleton<Manager.Input>.Instance.FocusLevel == this._focusLevel;
			}
		}

		// Token: 0x06008778 RID: 34680 RVA: 0x00387B2C File Offset: 0x00385F2C
		protected override void Awake()
		{
			base.Awake();
			if (this._canvasGroup == null)
			{
				this._canvasGroup = base.GetComponent<CanvasGroup>();
			}
			if (this._rectTransform == null)
			{
				this._rectTransform = base.GetComponent<RectTransform>();
			}
		}

		// Token: 0x06008779 RID: 34681 RVA: 0x00387B79 File Offset: 0x00385F79
		protected override void OnBeforeStart()
		{
			base.OnBeforeStart();
			(from _ in this._closeButton.OnClickAsObservable()
			where base.EnabledInput
			select _).Subscribe(delegate(Unit _)
			{
				this._tutorialUI.DoClose();
			});
		}

		// Token: 0x0600877A RID: 34682 RVA: 0x00387BB0 File Offset: 0x00385FB0
		protected override void OnAfterStart()
		{
			base.OnAfterStart();
			IEnumerator coroutine = this.CreateElements();
			Observable.FromCoroutine(() => coroutine, false).Subscribe<Unit>().AddTo(this);
		}

		// Token: 0x0600877B RID: 34683 RVA: 0x00387BF4 File Offset: 0x00385FF4
		private IEnumerator CreateElements()
		{
			this.CanvasAlpha = 0f;
			while (!Singleton<Manager.Resources>.IsInstance())
			{
				yield return null;
			}
			for (;;)
			{
				Manager.Resources.PopupInfoTable popupInfo = Singleton<Manager.Resources>.Instance.PopupInfo;
				if (((popupInfo != null) ? popupInfo.TutorialPrefabTable : null) != null)
				{
					break;
				}
				yield return null;
			}
			ReadOnlyDictionary<int, UnityEx.ValueTuple<string, GameObject[]>> prefabTable = Singleton<Manager.Resources>.Instance.PopupInfo.TutorialPrefabTable;
			if (prefabTable.IsNullOrEmpty<int, UnityEx.ValueTuple<string, GameObject[]>>())
			{
				yield break;
			}
			this._prefabTable = prefabTable;
			for (int i = 0; i < prefabTable.Count; i++)
			{
				string arg = "概要なし";
				UnityEx.ValueTuple<string, GameObject[]> valueTuple;
				if (prefabTable.TryGetValue(i, out valueTuple))
				{
					arg = valueTuple.Item1;
				}
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this._elementPrefab, this._elementRoot, false);
				gameObject.SetActive(true);
				gameObject.gameObject.name = string.Format("Element[{0:00}] {1}", i, arg);
				Button button = (gameObject != null) ? gameObject.GetComponent<Button>() : null;
				Text text = (button != null) ? button.GetComponentInChildren<Text>(true) : null;
				if (button != null && text != null)
				{
					AIProject.Animal.Tuple<int, Button, Text, bool> newElement = new AIProject.Animal.Tuple<int, Button, Text, bool>(i, button, text, false);
					this._elements.Add(newElement);
					(from _ in button.OnClickAsObservable()
					where this.InputEnabled
					select _).Subscribe(delegate(Unit _)
					{
						this.ClickSelect(newElement);
					});
					(from _ in button.OnClickAsObservable()
					where this.InputEnabled
					select _).Subscribe(delegate(Unit _)
					{
						this.ChangeGroupUI(newElement);
					});
					(from _ in button.OnClickAsObservable()
					where this.InputEnabled
					select _).Subscribe(delegate(Unit _)
					{
						this.PlaySE(SoundPack.SystemSE.OK_S);
					});
				}
			}
			this._lastIndex = prefabTable.Count;
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this._elementPrefab, this._elementRoot, false);
			gameObject2.SetActive(true);
			gameObject2.gameObject.name = string.Format("Element[{0}] {1}", prefabTable.Count, this._lastElementStr);
			Button button2 = (gameObject2 != null) ? gameObject2.GetComponent<Button>() : null;
			Text text2 = (button2 != null) ? button2.GetComponentInChildren<Text>(true) : null;
			if (button2 != null && text2 != null)
			{
				AIProject.Animal.Tuple<int, Button, Text, bool> item = new AIProject.Animal.Tuple<int, Button, Text, bool>(this._lastIndex, button2, text2, false);
				this._elements.Add(item);
				(from _ in button2.OnClickAsObservable()
				where this.InputEnabled
				select _).Subscribe(delegate(Unit _)
				{
					this.OpenLoadingImageUI();
				});
			}
			this.RefreshElements();
			this.SelectButton(-1);
			this.CanvasAlpha = 1f;
			this.SetActive(base.gameObject, false);
			yield break;
		}

		// Token: 0x0600877C RID: 34684 RVA: 0x00387C10 File Offset: 0x00386010
		public void RefreshElements()
		{
			if (this._prefabTable.IsNullOrEmpty<int, UnityEx.ValueTuple<string, GameObject[]>>())
			{
				return;
			}
			foreach (KeyValuePair<int, UnityEx.ValueTuple<string, GameObject[]>> keyValuePair in this._prefabTable)
			{
				AIProject.Animal.Tuple<int, Button, Text, bool> element = this._elements.GetElement(keyValuePair.Key);
				if (element != null)
				{
					UnityEx.ValueTuple<string, GameObject[]> value = keyValuePair.Value;
					element.Item4 = this.GetTutorialOpenState(keyValuePair.Key);
					if (element.Item3 != null)
					{
						bool item = element.Item4;
						element.Item3.text = ((!item) ? this._lockStr : value.Item1);
					}
				}
			}
			AIProject.Animal.Tuple<int, Button, Text, bool> element2 = this._elements.GetElement(this._prefabTable.Count);
			if (((element2 != null) ? element2.Item3 : null) != null)
			{
				element2.Item3.text = this._lastElementStr;
			}
		}

		// Token: 0x0600877D RID: 34685 RVA: 0x00387D30 File Offset: 0x00386130
		private void ChangeGroupUI(AIProject.Animal.Tuple<int, Button, Text, bool> value)
		{
			if (value == null)
			{
				return;
			}
			if (value.Item4)
			{
				if (this._tutorialUI.OpenElementNumber == value.Item1)
				{
					this.ClickSelect(null);
					this._tutorialUI.ChangeUIGroup(-1);
				}
				else
				{
					this._tutorialUI.ChangeUIGroup(value.Item1);
				}
			}
		}

		// Token: 0x0600877E RID: 34686 RVA: 0x00387D90 File Offset: 0x00386190
		private void ClickSelect(AIProject.Animal.Tuple<int, Button, Text, bool> elm)
		{
			if (elm != null && !elm.Item4 && elm.Item1 != this._lastIndex)
			{
				return;
			}
			if (this.SelectedElement != null && this.SelectedElement.Item3 != null)
			{
				this.SelectedElement.Item3.color = this._whiteColor;
			}
			this.SelectedElement = elm;
			if (elm != null)
			{
				if (elm.Item3 != null)
				{
					elm.Item3.color = this._yellowColor;
				}
				this.SelectIndex = elm.Item1;
			}
			else
			{
				this.SelectIndex = -1;
			}
		}

		// Token: 0x0600877F RID: 34687 RVA: 0x00387E3E File Offset: 0x0038623E
		public void SelectButton(int index)
		{
			this.ClickSelect(this._elements.GetElement(index));
		}

		// Token: 0x06008780 RID: 34688 RVA: 0x00387E52 File Offset: 0x00386252
		private void OpenLoadingImageUI()
		{
			this.PlaySE(SoundPack.SystemSE.OK_S);
			this._loadingImageUI.IsActiveControl = true;
		}

		// Token: 0x06008781 RID: 34689 RVA: 0x00387E68 File Offset: 0x00386268
		private bool GetTutorialOpenState(int id)
		{
			if (!Singleton<Game>.IsInstance())
			{
				return false;
			}
			WorldData worldData = Singleton<Game>.Instance.WorldData;
			Dictionary<int, bool> dictionary = (worldData == null) ? null : worldData.TutorialOpenStateTable;
			if (dictionary.IsNullOrEmpty<int, bool>())
			{
				return false;
			}
			bool result;
			if (!dictionary.TryGetValue(id, out result))
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06008782 RID: 34690 RVA: 0x00387EBD File Offset: 0x003862BD
		private void SetInteractable(bool active)
		{
			if (this._canvasGroup.interactable != active)
			{
				this._canvasGroup.interactable = active;
			}
		}

		// Token: 0x06008783 RID: 34691 RVA: 0x00387EDC File Offset: 0x003862DC
		private void SetBlockRaycasts(bool active)
		{
			if (this._canvasGroup.blocksRaycasts != active)
			{
				this._canvasGroup.blocksRaycasts = active;
			}
		}

		// Token: 0x06008784 RID: 34692 RVA: 0x00387EFB File Offset: 0x003862FB
		private void SetActive(GameObject obj, bool active)
		{
			if (obj == null)
			{
				return;
			}
			if (obj.activeSelf != active)
			{
				obj.SetActive(active);
			}
		}

		// Token: 0x06008785 RID: 34693 RVA: 0x00387F1D File Offset: 0x0038631D
		private void SetActive(Component com, bool active)
		{
			if (com == null || com.gameObject == null)
			{
				return;
			}
			if (com.gameObject.activeSelf != active)
			{
				com.gameObject.SetActive(active);
			}
		}

		// Token: 0x06008786 RID: 34694 RVA: 0x00387F5C File Offset: 0x0038635C
		private void PlaySE(SoundPack.SystemSE se)
		{
			SoundPack soundPack = (!Singleton<Manager.Resources>.IsInstance()) ? null : Singleton<Manager.Resources>.Instance.SoundPack;
			if (soundPack != null)
			{
				soundPack.Play(se);
			}
		}

		// Token: 0x04006E01 RID: 28161
		[SerializeField]
		private CanvasGroup _canvasGroup;

		// Token: 0x04006E02 RID: 28162
		[SerializeField]
		private RectTransform _rectTransform;

		// Token: 0x04006E03 RID: 28163
		[SerializeField]
		private Button _closeButton;

		// Token: 0x04006E04 RID: 28164
		[SerializeField]
		private Transform _elementRoot;

		// Token: 0x04006E05 RID: 28165
		[SerializeField]
		private GameObject _elementPrefab;

		// Token: 0x04006E06 RID: 28166
		[SerializeField]
		private Scrollbar _scrollbar;

		// Token: 0x04006E07 RID: 28167
		[SerializeField]
		private Image _scrollbarHandleImage;

		// Token: 0x04006E08 RID: 28168
		[SerializeField]
		private string _lockStr = string.Empty;

		// Token: 0x04006E09 RID: 28169
		[SerializeField]
		private string _lastElementStr = string.Empty;

		// Token: 0x04006E0A RID: 28170
		[SerializeField]
		private Color _whiteColor = Color.white;

		// Token: 0x04006E0B RID: 28171
		[SerializeField]
		private Color _yellowColor = Color.yellow;

		// Token: 0x04006E0C RID: 28172
		[SerializeField]
		private TutorialUI _tutorialUI;

		// Token: 0x04006E0D RID: 28173
		[SerializeField]
		private TutorialLoadingImageUI _loadingImageUI;

		// Token: 0x04006E0E RID: 28174
		private List<AIProject.Animal.Tuple<int, Button, Text, bool>> _elements = new List<AIProject.Animal.Tuple<int, Button, Text, bool>>();

		// Token: 0x04006E11 RID: 28177
		private int _lastIndex = -1;

		// Token: 0x04006E12 RID: 28178
		private ReadOnlyDictionary<int, UnityEx.ValueTuple<string, GameObject[]>> _prefabTable;
	}
}
