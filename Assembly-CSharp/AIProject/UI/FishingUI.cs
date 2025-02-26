using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using AIProject.Definitions;
using AIProject.MiniGames.Fishing;
using AIProject.SaveData;
using Cinemachine;
using ConfigScene;
using Manager;
using ReMotion;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using UnityEx;

namespace AIProject.UI
{
	// Token: 0x02000F33 RID: 3891
	public class FishingUI : MenuUIBehaviour
	{
		// Token: 0x170019D1 RID: 6609
		// (get) Token: 0x06008085 RID: 32901 RVA: 0x003675E4 File Offset: 0x003659E4
		private MenuUIBehaviour[] MenuUIElements
		{
			[CompilerGenerated]
			get
			{
				MenuUIBehaviour[] result;
				if ((result = this._menuUIElements) == null)
				{
					result = (this._menuUIElements = new MenuUIBehaviour[]
					{
						this
					});
				}
				return result;
			}
		}

		// Token: 0x170019D2 RID: 6610
		// (get) Token: 0x06008086 RID: 32902 RVA: 0x00367611 File Offset: 0x00365A11
		// (set) Token: 0x06008087 RID: 32903 RVA: 0x00367619 File Offset: 0x00365A19
		private List<FishFoodInfo> FoodInfoList { get; set; }

		// Token: 0x170019D3 RID: 6611
		// (get) Token: 0x06008088 RID: 32904 RVA: 0x00367622 File Offset: 0x00365A22
		// (set) Token: 0x06008089 RID: 32905 RVA: 0x0036762A File Offset: 0x00365A2A
		public FishFoodInfo SelectedFishFood { get; private set; }

		// Token: 0x170019D4 RID: 6612
		// (get) Token: 0x0600808A RID: 32906 RVA: 0x00367633 File Offset: 0x00365A33
		// (set) Token: 0x0600808B RID: 32907 RVA: 0x0036763B File Offset: 0x00365A3B
		public bool IsActive { get; private set; }

		// Token: 0x170019D5 RID: 6613
		// (get) Token: 0x0600808C RID: 32908 RVA: 0x00367644 File Offset: 0x00365A44
		private bool DisplayShortcutUI
		{
			[CompilerGenerated]
			get
			{
				return Singleton<Game>.IsInstance() && Singleton<Game>.Instance.MapShortcutUI != null;
			}
		}

		// Token: 0x170019D6 RID: 6614
		// (get) Token: 0x0600808D RID: 32909 RVA: 0x00367664 File Offset: 0x00365A64
		public bool FocusInOn
		{
			[CompilerGenerated]
			get
			{
				return this.IsActive && Singleton<Manager.Input>.Instance.FocusLevel == base.FocusLevel && this.IsActiveControl && base.EnabledInput && !this.DisplayShortcutUI;
			}
		}

		// Token: 0x0600808E RID: 32910 RVA: 0x003676B3 File Offset: 0x00365AB3
		protected override void Awake()
		{
			base.Awake();
			this.OpenAllCanvas();
			this.CloseAllCanvas();
			this.FoodInfoList = ListPool<FishFoodInfo>.Get();
		}

		// Token: 0x0600808F RID: 32911 RVA: 0x003676D4 File Offset: 0x00365AD4
		protected override void Start()
		{
			base.OnActiveChangedAsObservable().Subscribe(delegate(bool x)
			{
				this.SetActiveControl(x);
			});
			if (this.howToElements == null || this.howToElements.Length == 0)
			{
				this.howToElements = new FishingHowToUI[3];
				for (int i = 0; i < 3; i++)
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.howToElementPrefab, this.howToGroup.transform);
					this.howToElements[i] = gameObject.GetComponent<FishingHowToUI>();
					this.howToElements[i].gameObject.SetActive(false);
				}
			}
			base.Start();
		}

		// Token: 0x06008090 RID: 32912 RVA: 0x0036776D File Offset: 0x00365B6D
		protected void OnDestroy()
		{
			ListPool<FishFoodInfo>.Release(this.FoodInfoList);
		}

		// Token: 0x06008091 RID: 32913 RVA: 0x0036777C File Offset: 0x00365B7C
		private void SetActiveControl(bool isActive)
		{
			IEnumerator _coroutine = (!isActive) ? this.DoStop() : this.DoSetup();
			if (this.fadeSubscriber != null)
			{
				this.fadeSubscriber.Dispose();
			}
			this.fadeSubscriber = Observable.FromCoroutine(() => _coroutine, false).Subscribe(delegate(Unit _)
			{
			}, delegate(Exception ex)
			{
			});
		}

		// Token: 0x06008092 RID: 32914 RVA: 0x0036781C File Offset: 0x00365C1C
		private void SetSelectedFood(int _selectedIndex)
		{
			if (_selectedIndex < 0)
			{
				_selectedIndex = this.FoodInfoList.Count - 1;
			}
			else if (this.FoodInfoList.Count <= _selectedIndex)
			{
				_selectedIndex = 0;
			}
			this.selectFishFoodIndex = _selectedIndex;
			FishFoodInfo element = this.FoodInfoList.GetElement(this.selectFishFoodIndex);
			if (element == null)
			{
				this.SelectedFishFood = null;
				return;
			}
			this.SelectedFishFood = element;
		}

		// Token: 0x06008093 RID: 32915 RVA: 0x00367888 File Offset: 0x00365C88
		private IEnumerator DoSetup()
		{
			this.CloseAllCanvas();
			GameConfigSystem configData = Config.GameData;
			this.displayGuide = (configData == null || configData.ActionGuide);
			PlayerActor _player = Singleton<Manager.Map>.Instance.Player;
			_player.PlayerData.FishingSkill.CalculationNextExp = ((int x) => Singleton<Manager.Resources>.Instance.FishingDefinePack.SystemParam.NextExperience);
			this.FoodInfoList.Clear();
			List<StuffItem> _foodList = null;
			_foodList = (from x in _player.PlayerData.ItemList.FindAll((StuffItem x) => x != null && x.CategoryID == this._baitItemCategoryID)
			select new StuffItem(x.CategoryID, x.ID, x.Count)).ToList<StuffItem>();
			_foodList.Insert(0, new StuffItem(this._fakeBaitItemID.categoryID, this._fakeBaitItemID.itemID, 1));
			if (!_foodList.IsNullOrEmpty<StuffItem>())
			{
				Dictionary<int, Tuple<string, Sprite>> itemIcon = Singleton<Manager.Resources>.Instance.itemIconTables.ItemIcon;
				Manager.Resources.GameInfoTables gameInfo = Singleton<Manager.Resources>.Instance.GameInfo;
				Dictionary<int, FishFoodInfo> fishFoodInfoTable = Singleton<Manager.Resources>.Instance.Fishing.FishFoodInfoTable;
				using (List<StuffItem>.Enumerator enumerator = _foodList.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						StuffItem _item = enumerator.Current;
						if (_item != null)
						{
							StuffItemInfo item = gameInfo.GetItem(_item.CategoryID, _item.ID);
							if (item != null)
							{
								FishFoodInfo fishFoodInfo = this.FoodInfoList.Find((FishFoodInfo x) => x != null && x.CategoryID == _item.CategoryID && x.ItemID == _item.ID);
								if (fishFoodInfo == null)
								{
									FishFoodInfo fishFoodInfo2 = null;
									if (fishFoodInfoTable.TryGetValue(_item.ID, out fishFoodInfo2) && fishFoodInfo2 != null)
									{
										Tuple<string, Sprite> tuple = null;
										itemIcon.TryGetValue(item.IconID, out tuple);
										bool isInfinity = item.Rate < 0;
										this.FoodInfoList.Add(new FishFoodInfo(_item, (tuple != null) ? tuple.Item2 : null, fishFoodInfo2, isInfinity));
									}
								}
								else
								{
									fishFoodInfo.AddCount(_item.Count);
								}
							}
						}
					}
				}
				if (2 <= this.FoodInfoList.Count)
				{
					this.FoodInfoList.Sort(delegate(FishFoodInfo x, FishFoodInfo y)
					{
						StuffItem stuffItem = x.StuffItem;
						int? num = (stuffItem != null) ? new int?(stuffItem.ID) : null;
						int num2 = (num == null) ? -1 : num.Value;
						StuffItem stuffItem2 = y.StuffItem;
						int? num3 = (stuffItem2 != null) ? new int?(stuffItem2.ID) : null;
						return num2 - ((num3 == null) ? -1 : num3.Value);
					});
				}
				this.selectFishFoodIndex = Mathf.Clamp(this.selectFishFoodIndex, 0, this.FoodInfoList.Count - 1);
				this.SelectedFishFood = this.FoodInfoList.GetElement(this.selectFishFoodIndex);
			}
			ListPool<StuffItem>.Release(_foodList);
			this.InitializeUI();
			this._focusLevel = ++Singleton<Manager.Input>.Instance.FocusLevel;
			Singleton<Manager.Input>.Instance.MenuElements = this.MenuUIElements;
			this.IsActive = true;
			if (this.updateDisposable != null)
			{
				this.updateDisposable.Dispose();
			}
			if (this.lateUpdateDisposable != null)
			{
				this.lateUpdateDisposable.Dispose();
			}
			yield return null;
			yield return null;
			CinemachineBrain cinemachineBrain;
			if (Singleton<Manager.Map>.IsInstance())
			{
				PlayerActor player = Singleton<Manager.Map>.Instance.Player;
				if (player == null)
				{
					cinemachineBrain = null;
				}
				else
				{
					ActorCameraControl cameraControl = player.CameraControl;
					cinemachineBrain = ((cameraControl != null) ? cameraControl.CinemachineBrain : null);
				}
			}
			else
			{
				cinemachineBrain = null;
			}
			CinemachineBrain cameraBrain = cinemachineBrain;
			for (;;)
			{
				CinemachineBrain cinemachineBrain2 = cameraBrain;
				bool? flag = (cinemachineBrain2 != null) ? new bool?(cinemachineBrain2.IsBlending) : null;
				if (flag == null || !flag.Value)
				{
					break;
				}
				yield return null;
			}
			yield return MapUIContainer.DrawOnceTutorialUI(9, null);
			this.updateDisposable = (from _ in Observable.EveryUpdate().TakeUntilDestroy(base.gameObject)
			where base.isActiveAndEnabled
			select _).Subscribe(delegate(long _)
			{
				this.OnUpdate();
			});
			this.lateUpdateDisposable = (from _ in Observable.EveryLateUpdate().TakeUntilDestroy(base.gameObject)
			where base.isActiveAndEnabled
			select _).Subscribe(delegate(long _)
			{
				this.OnLateUpdate();
			});
			base.EnabledInput = true;
			yield break;
		}

		// Token: 0x06008094 RID: 32916 RVA: 0x003678A4 File Offset: 0x00365CA4
		private IEnumerator DoStop()
		{
			base.EnabledInput = false;
			this.CloseAllCanvas();
			if (this.updateDisposable != null)
			{
				this.updateDisposable.Dispose();
			}
			if (this.lateUpdateDisposable != null)
			{
				this.lateUpdateDisposable.Dispose();
			}
			this.DestroyGetFishModel();
			Singleton<Manager.Input>.Instance.ClearMenuElements();
			Singleton<Manager.Input>.Instance.FocusLevel--;
			this.IsActive = false;
			yield break;
		}

		// Token: 0x06008095 RID: 32917 RVA: 0x003678BF File Offset: 0x00365CBF
		private void DestroyGetFishModel()
		{
			if (this.getFishModel != null)
			{
				UnityEngine.Object.Destroy(this.getFishModel);
				this.getFishModel = null;
			}
		}

		// Token: 0x06008096 RID: 32918 RVA: 0x003678E4 File Offset: 0x00365CE4
		public void UseFishFood()
		{
			FishFoodInfo selectedFishFood = this.SelectedFishFood;
			if (selectedFishFood != null)
			{
				selectedFishFood.UseFood();
			}
			this.ResetSelectFoodUI();
		}

		// Token: 0x170019D7 RID: 6615
		// (get) Token: 0x06008097 RID: 32919 RVA: 0x00367900 File Offset: 0x00365D00
		public int FishFoodNum
		{
			[CompilerGenerated]
			get
			{
				FishFoodInfo selectedFishFood = this.SelectedFishFood;
				int? num = (selectedFishFood != null) ? new int?(selectedFishFood.Count) : null;
				return (num == null) ? 0 : num.Value;
			}
		}

		// Token: 0x06008098 RID: 32920 RVA: 0x0036794C File Offset: 0x00365D4C
		public bool HaveSomeFishFood()
		{
			foreach (FishFoodInfo fishFoodInfo in this.FoodInfoList)
			{
				if (0 < fishFoodInfo.Count)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06008099 RID: 32921 RVA: 0x003679B8 File Offset: 0x00365DB8
		public bool FilledItemSlot()
		{
			List<StuffItem> itemList = Singleton<Manager.Map>.Instance.Player.PlayerData.ItemList;
			int inventorySlotMax = Singleton<Manager.Map>.Instance.Player.PlayerData.InventorySlotMax;
			return inventorySlotMax <= itemList.Count;
		}

		// Token: 0x0600809A RID: 32922 RVA: 0x003679FC File Offset: 0x00365DFC
		private IEnumerator ChangeFood(int _moveIndex)
		{
			this.selectFishFoodIndex -= _moveIndex;
			this.SetSelectedFood(this.selectFishFoodIndex);
			this.ResetSelectFoodUI();
			if (_moveIndex != 0)
			{
				FishingUI.ArrowAnimParam arrowAnimParam = null;
				if (!this.AnimTable.TryGetValue(_moveIndex, out arrowAnimParam))
				{
					FishingUI.ArrowAnimParam arrowAnimParam2 = new FishingUI.ArrowAnimParam(_moveIndex, (0 >= _moveIndex) ? this.selectFoodGroup.selectDownImage : this.selectFoodGroup.selectUpImage, Color.white, Vector3.one, this.greenColor, Vector3.one * 1.3f);
					this.AnimTable[_moveIndex] = arrowAnimParam2;
					arrowAnimParam = arrowAnimParam2;
				}
				foreach (KeyValuePair<int, FishingUI.ArrowAnimParam> keyValuePair in this.AnimTable)
				{
					FishingUI.ArrowAnimParam value = keyValuePair.Value;
					if (value.moveIndex != _moveIndex)
					{
						value.PlayCancel();
					}
				}
				arrowAnimParam.PlayFadeIn();
			}
			yield return null;
			this.foodChangeSubscriber = null;
			yield break;
		}

		// Token: 0x0600809B RID: 32923 RVA: 0x00367A1E File Offset: 0x00365E1E
		private float Lerp(float a, float b, float t)
		{
			return Mathf.Lerp(a, b, t);
		}

		// Token: 0x0600809C RID: 32924 RVA: 0x00367A28 File Offset: 0x00365E28
		private float InverseLerp(float a, float b, float value)
		{
			return Mathf.InverseLerp(a, b, value);
		}

		// Token: 0x0600809D RID: 32925 RVA: 0x00367A34 File Offset: 0x00365E34
		private Color Lerp(Color a, Color b, float t)
		{
			return new Color(Mathf.Lerp(a.r, b.r, t), Mathf.Lerp(a.g, b.g, t), Mathf.Lerp(a.b, b.b, t), Mathf.Lerp(a.a, b.a, t));
		}

		// Token: 0x0600809E RID: 32926 RVA: 0x00367A98 File Offset: 0x00365E98
		private void EasingImage(Image _image, float _count, float _limit, Color _color1, Color _color2, Vector3 _scale1, Vector3 _scale2)
		{
			float num = Mathf.InverseLerp(0f, _limit, _count);
			num = EasingFunctions.EaseOutQuint(num, 1f);
			_image.color = this.Lerp(_color1, _color2, num);
			_image.transform.localScale = Vector3.Lerp(_scale1, _scale2, num);
		}

		// Token: 0x0600809F RID: 32927 RVA: 0x00367AEC File Offset: 0x00365EEC
		private void OnUpdate()
		{
			if (!Singleton<Manager.Map>.IsInstance())
			{
				return;
			}
			FishingManager fishingSystem = Singleton<Manager.Map>.Instance.FishingSystem;
			FishingManager.FishingScene scene = fishingSystem.scene;
			if (scene == FishingManager.FishingScene.SelectFood)
			{
				float axis = fishingSystem.GetAxis(ActionID.MouseWheel);
				if (2 <= this.FoodInfoList.Count && this.foodChangeSubscriber == null)
				{
					if (axis != 0f)
					{
						IEnumerator _coroutine = this.ChangeFood((int)Mathf.Sign(axis));
						this.foodChangeSubscriber = Observable.FromCoroutine(() => _coroutine, false).Subscribe<Unit>();
					}
					else if (fishingSystem.IsPressedVertical)
					{
						if (fishingSystem.IsPressedAxis(ActionID.SelectVertical))
						{
							IEnumerator _coroutine = this.ChangeFood((int)Mathf.Sign(fishingSystem.GetAxis(ActionID.SelectVertical)));
							this.foodChangeSubscriber = Observable.FromCoroutine(() => _coroutine, false).Subscribe<Unit>();
						}
						else if (fishingSystem.IsPressedAxis(ActionID.MoveVertical))
						{
							IEnumerator _coroutine = this.ChangeFood((int)Mathf.Sign(fishingSystem.GetAxis(ActionID.MoveVertical)));
							this.foodChangeSubscriber = Observable.FromCoroutine(() => _coroutine, false).Subscribe<Unit>();
						}
					}
				}
				if (0 < this.SelectedFishFood.Count && fishingSystem.NextButtonDown)
				{
					this.selectFoodGroup.Hide();
					fishingSystem.ChangeFishScene(FishingManager.FishingScene.StartMotion);
				}
				else if (fishingSystem.BackButtonDown)
				{
					fishingSystem.EndFishing();
				}
			}
		}

		// Token: 0x060080A0 RID: 32928 RVA: 0x00367C80 File Offset: 0x00366080
		private void OnLateUpdate()
		{
			FishingManager fishingSystem = Singleton<Manager.Map>.Instance.FishingSystem;
			FishingManager.FishingScene scene = fishingSystem.scene;
			if (scene != FishingManager.FishingScene.SelectFood)
			{
				if (scene != FishingManager.FishingScene.Fishing)
				{
					if (scene != FishingManager.FishingScene.Success)
					{
					}
				}
				else
				{
					float x = this.fishingGroup.heartPointImageWidth * fishingSystem.HeartPointScale;
					foreach (RectTransform rectTransform in this.fishingGroup.heartPointRects)
					{
						Vector2 sizeDelta = rectTransform.sizeDelta;
						sizeDelta.x = x;
						rectTransform.sizeDelta = sizeDelta;
					}
					float timeScale = fishingSystem.TimeScale;
					Color timeColor = this.GetTimeColor(fishingSystem.TimeScale);
					foreach (Image image in this.fishingGroup.imageOutlines)
					{
						float a = image.color.a;
						timeColor.a = a;
						image.color = timeColor;
					}
					foreach (CircleOutline circleOutline in this.fishingGroup.circleOutlines)
					{
						float a2 = circleOutline.effectColor.a;
						timeColor.a = a2;
						circleOutline.effectColor = timeColor;
					}
					float a3 = this.fishingGroup.timerImage.color.a;
					timeColor.a = a3;
					this.fishingGroup.timerImage.color = timeColor;
					this.fishingGroup.timerCircleImage.fillAmount = timeScale;
				}
			}
		}

		// Token: 0x060080A1 RID: 32929 RVA: 0x00367E20 File Offset: 0x00366220
		private Color GetTimeColor(float t)
		{
			t = Mathf.Clamp01(t);
			if (0.5 < (double)t)
			{
				t = Mathf.InverseLerp(0.5f, 1f, t);
				return Color.Lerp(this.yellowColor, this.lightGreenColor, t);
			}
			t = Mathf.InverseLerp(0f, 0.5f, t);
			return Color.Lerp(this.redColor, this.yellowColor, t);
		}

		// Token: 0x060080A2 RID: 32930 RVA: 0x00367E8E File Offset: 0x0036628E
		private void InitializeUI()
		{
			this.InitSelectFoodUI();
			this.InitHitWaitUI();
			this.InitFishingUI();
			this.InitResultUI();
		}

		// Token: 0x060080A3 RID: 32931 RVA: 0x00367EA8 File Offset: 0x003662A8
		public void InitSelectFoodUI()
		{
			FishingUI.SelectFoodGroup selectFoodGroup = this.selectFoodGroup;
			bool flag = this.FoodInfoList.Count <= 1;
			this.SetActive(selectFoodGroup.selectUpImage, !flag);
			this.SetActive(selectFoodGroup.selectDownImage, !flag);
			this.ResetSelectFoodUI();
		}

		// Token: 0x060080A4 RID: 32932 RVA: 0x00367EF4 File Offset: 0x003662F4
		public void ResetSelectFoodUI()
		{
			FishingUI.SelectFoodGroup selectFoodGroup = this.selectFoodGroup;
			int num = (!Singleton<Manager.Resources>.IsInstance()) ? 999 : Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.ItemSlotMax;
			if (this.SelectedFishFood != null)
			{
				selectFoodGroup.fishFoodNameText.text = this.SelectedFishFood.FoodName;
				this.fishFoodImage.sprite = this.SelectedFishFood.Icon;
				if (this.SelectedFishFood.IsInfinity)
				{
					this.fishFoodNumText.color = this.redColor;
					this.fishFoodNumText.text = "∞";
					this.fishFoodNumText.fontSize = this._foodCountInfinityFontSize;
					this.SetActive(this.fishFoodNumOverText, false);
				}
				else
				{
					int count = this.SelectedFishFood.Count;
					this.fishFoodNumText.color = ((count > 0) ? this.whiteColor : this.redColor);
					this.fishFoodNumText.text = string.Format("{0}", Mathf.Clamp(count, 0, num));
					this.fishFoodNumText.fontSize = this._foodCountNormalFontSize;
					this.SetActive(this.fishFoodNumOverText, num < count);
				}
			}
			else
			{
				selectFoodGroup.fishFoodNameText.text = string.Empty;
				this.fishFoodImage.sprite = null;
				int num2 = 0;
				this.fishFoodNumText.color = ((num2 > 0) ? this.whiteColor : this.redColor);
				this.fishFoodNumText.text = string.Format("{0}", Mathf.Clamp(num2, 0, num));
				this.SetActive(this.fishFoodNumOverText, num < num2);
			}
		}

		// Token: 0x060080A5 RID: 32933 RVA: 0x003680A6 File Offset: 0x003664A6
		public void InitHitWaitUI()
		{
		}

		// Token: 0x060080A6 RID: 32934 RVA: 0x003680A8 File Offset: 0x003664A8
		public void InitFishingUI()
		{
			FishingUI.FishingGroup fishingGroup = this.fishingGroup;
			foreach (RectTransform rectTransform in fishingGroup.heartPointRects)
			{
				Vector2 sizeDelta = rectTransform.sizeDelta;
				sizeDelta.x = fishingGroup.heartPointImageWidth;
				rectTransform.sizeDelta = sizeDelta;
			}
			fishingGroup.timerCircleImage.fillAmount = 1f;
			Color timeColor = this.GetTimeColor(1f);
			foreach (CircleOutline circleOutline in fishingGroup.circleOutlines)
			{
				float a = circleOutline.effectColor.a;
				timeColor.a = a;
				circleOutline.effectColor = timeColor;
			}
			foreach (Image image in fishingGroup.imageOutlines)
			{
				float a2 = image.color.a;
				timeColor.a = a2;
				image.color = timeColor;
			}
			float a3 = fishingGroup.timerImage.color.a;
			timeColor.a = a3;
			fishingGroup.timerImage.color = timeColor;
			this.SetAlpha(fishingGroup.hitImage, 0f);
		}

		// Token: 0x060080A7 RID: 32935 RVA: 0x003681EC File Offset: 0x003665EC
		public void InitResultUI()
		{
			FishingUI.ResultGroup resultGroup = this.resultGroup;
			this.SetAlpha(resultGroup.getImage, 0f);
			resultGroup.resultWindow.alpha = 0f;
			resultGroup.fishNameText.text = string.Empty;
			resultGroup.rarelityText.text = string.Empty;
			resultGroup.fishImage.texture = null;
			resultGroup.flavorText.text = string.Empty;
			resultGroup.experiencePointText.text = string.Empty;
			resultGroup.levelText.text = string.Empty;
			resultGroup.experienceBarImage.fillAmount = 0f;
			resultGroup.experienceBarImage.color = this.blueColor;
			resultGroup.levelUpText.color = this.yellowColor;
			this.SetAlpha(resultGroup.levelUpText, 0f);
		}

		// Token: 0x060080A8 RID: 32936 RVA: 0x003682C0 File Offset: 0x003666C0
		private void SetFishingHowToUI(FishingHowToUI _ui, string _text, FishingUI.MouseInputType _type)
		{
			Sprite sprite = null;
			Singleton<Manager.Resources>.Instance.itemIconTables.InputIconTable.TryGetValue((int)_type, out sprite);
			_ui.image.sprite = sprite;
			_ui.text.text = _text;
			this.SetEnable(_ui.image, sprite != null);
		}

		// Token: 0x060080A9 RID: 32937 RVA: 0x00368314 File Offset: 0x00366714
		private void ResetHowToUI(FishingManager.FishingScene _scene)
		{
			switch (_scene)
			{
			case FishingManager.FishingScene.SelectFood:
			{
				this.SetActive(this.howToGroup, this.displayGuide);
				this.SetActiveHowToElement(3);
				FishingHowToUI[] array = this.howToElements;
				this.SetFishingHowToUI(array[0], "釣る", FishingUI.MouseInputType.Left);
				this.SetFishingHowToUI(array[1], "やめる", FishingUI.MouseInputType.Right);
				this.SetFishingHowToUI(array[2], "エサ変更", FishingUI.MouseInputType.Wheel);
				return;
			}
			case FishingManager.FishingScene.WaitHit:
			{
				this.SetActive(this.howToGroup, this.displayGuide);
				this.SetActiveHowToElement(2);
				FishingHowToUI[] array2 = this.howToElements;
				this.SetFishingHowToUI(array2[0], "ウキの移動", FishingUI.MouseInputType.Move);
				this.SetFishingHowToUI(array2[1], "もどる", FishingUI.MouseInputType.Right);
				return;
			}
			case FishingManager.FishingScene.Fishing:
				this.SetActive(this.howToGroup, this.displayGuide);
				this.SetActiveHowToElement(1);
				this.SetFishingHowToUI(this.howToElements[0], "力の方向", FishingUI.MouseInputType.Move);
				return;
			}
			this.SetActive(this.howToGroup, false);
		}

		// Token: 0x060080AA RID: 32938 RVA: 0x00368418 File Offset: 0x00366818
		private void ResetFishFoodTypeUI(FishingManager.FishingScene _scene)
		{
			switch (_scene)
			{
			case FishingManager.FishingScene.SelectFood:
			case FishingManager.FishingScene.StartMotion:
			case FishingManager.FishingScene.WaitHit:
			case FishingManager.FishingScene.Fishing:
			case FishingManager.FishingScene.Success:
			case FishingManager.FishingScene.Failure:
				this.SetActive(this.fishFoodTypeGroup, true);
				break;
			default:
				this.SetActive(this.fishFoodTypeGroup, false);
				break;
			}
		}

		// Token: 0x060080AB RID: 32939 RVA: 0x0036846C File Offset: 0x0036686C
		private void SetActiveHowToElement(int _index)
		{
			for (int i = 0; i < this.howToElements.Length; i++)
			{
				this.SetActive(this.howToElements[i], i < _index);
			}
		}

		// Token: 0x060080AC RID: 32940 RVA: 0x003684A4 File Offset: 0x003668A4
		public void ChangeFishScene(FishingManager.FishingScene _scene)
		{
			if (_scene == FishingManager.FishingScene.None)
			{
				return;
			}
			this.CloseAllCanvas();
			switch (_scene)
			{
			case FishingManager.FishingScene.SelectFood:
				this.StartSelectFood();
				break;
			case FishingManager.FishingScene.WaitHit:
				this.StartWaitHit();
				break;
			case FishingManager.FishingScene.Fishing:
				this.StartFishing();
				this.StartDrawHitText();
				break;
			}
			this.ApplyFishScene(_scene);
		}

		// Token: 0x060080AD RID: 32941 RVA: 0x00368524 File Offset: 0x00366924
		public void ApplyFishScene(FishingManager.FishingScene _scene)
		{
			this.ResetHowToUI(_scene);
			this.ResetFishFoodTypeUI(_scene);
		}

		// Token: 0x060080AE RID: 32942 RVA: 0x00368534 File Offset: 0x00366934
		public void StartSelectFood()
		{
			this.ResetSelectFoodUI();
			this.selectFoodGroup.Show();
		}

		// Token: 0x060080AF RID: 32943 RVA: 0x00368547 File Offset: 0x00366947
		public void StartWaitHit()
		{
			this.hitWaitGroup.Show();
		}

		// Token: 0x060080B0 RID: 32944 RVA: 0x00368554 File Offset: 0x00366954
		public void StartFishing()
		{
			this.fishingGroup.Show();
		}

		// Token: 0x060080B1 RID: 32945 RVA: 0x00368564 File Offset: 0x00366964
		private void StartDrawHitText()
		{
			if (this.drawHitTextSubscriber != null)
			{
				this.drawHitTextSubscriber.Dispose();
			}
			IEnumerator _coroutine = this.DrawHitText();
			this.drawHitTextSubscriber = Observable.FromCoroutine(() => _coroutine, false).Subscribe<Unit>();
		}

		// Token: 0x060080B2 RID: 32946 RVA: 0x003685B8 File Offset: 0x003669B8
		private IEnumerator DrawHitText()
		{
			Image _hitImage = this.fishingGroup.hitImage;
			Vector3 _scale = Vector3.one * 3f;
			_hitImage.transform.localScale = _scale;
			this.SetAlpha(_hitImage, 0f);
			this.fishingGroup.Show();
			for (float _count = 0f; _count < 0.25f; _count += Time.deltaTime)
			{
				float _t = Mathf.InverseLerp(0f, 0.25f, _count);
				_t = EasingFunctions.EaseInExpo(_t, 1f);
				_hitImage.transform.localScale = Vector3.Lerp(_scale, Vector3.one, _t);
				this.SetAlpha(_hitImage, _t);
				yield return null;
			}
			this.SetAlpha(_hitImage, 1f);
			_hitImage.transform.localScale = Vector3.one;
			yield return Observable.Timer(TimeSpan.FromSeconds(1.0)).ToYieldInstruction<long>();
			this.SetAlpha(_hitImage, 0f);
			yield break;
		}

		// Token: 0x060080B3 RID: 32947 RVA: 0x003685D4 File Offset: 0x003669D4
		private void SetGetFishUI(FishInfo _info)
		{
			if (!Singleton<Manager.Map>.IsInstance())
			{
				return;
			}
			FishingManager fishingSystem = Singleton<Manager.Map>.Instance.FishingSystem;
			this.getFishInfo = _info;
			this.DestroyGetFishModel();
			Manager.Resources.FishingTable fishing = Singleton<Manager.Resources>.Instance.Fishing;
			Dictionary<int, Tuple<GameObject, RuntimeAnimatorController>> fishModelTable = fishing.FishModelTable;
			Renderer renderer = null;
			float y = -50f;
			int modelID = _info.ModelID;
			Tuple<GameObject, RuntimeAnimatorController> tuple;
			if (fishModelTable.TryGetValue(modelID, out tuple) && tuple.Item1 != null)
			{
				this.getFishModel = UnityEngine.Object.Instantiate<GameObject>(tuple.Item1, new Vector3(0f, y, 0f), Quaternion.identity);
				this.getFishModel.transform.SetParent(fishingSystem.RootObject.transform, true);
				this.SetLayer(this.getFishModel.transform, LayerMask.NameToLayer("Fishing"));
				Animator componentInChildren = this.getFishModel.GetComponentInChildren<Animator>(true);
				if (componentInChildren != null && tuple.Item2 != null)
				{
					componentInChildren.runtimeAnimatorController = tuple.Item2;
				}
				renderer = this.getFishModel.GetComponentInChildren<Renderer>(true);
			}
			Camera fishModelCamera = fishingSystem.fishModelCamera;
			if (renderer != null)
			{
				float num = fishModelCamera.nearClipPlane + 5f;
				Bounds bounds = renderer.bounds;
				Vector3 center = bounds.center;
				center.x -= num + bounds.extents.x;
				fishModelCamera.transform.position = center;
				fishModelCamera.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
				fishModelCamera.orthographic = true;
				float a = Mathf.Max(bounds.extents.z, bounds.extents.y);
				fishModelCamera.orthographicSize = Mathf.Max(a, fishing.ResultFishReferenceExtent) * 1.2f;
			}
			else
			{
				fishModelCamera.transform.position = new Vector3(0f, y, 0f);
				fishModelCamera.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
			}
			this.resultGroup.fishImage.texture = fishingSystem.fishModelCamera.targetTexture;
			StuffItemInfo item = Singleton<Manager.Resources>.Instance.GameInfo.GetItem(this.getFishInfo.CategoryID, this.getFishInfo.ItemID);
			if (item != null)
			{
				string[] rarelityLabelList = Singleton<Manager.Resources>.Instance.FishingDefinePack.UIParam.RarelityLabelList;
				this.resultGroup.rarelityText.text = (rarelityLabelList.GetElement((int)item.Grade) ?? "☆☆☆");
				this.resultGroup.fishNameText.text = item.Name;
				this.resultGroup.flavorText.text = item.Explanation;
			}
			else
			{
				this.resultGroup.rarelityText.text = "☆☆☆";
				this.resultGroup.fishNameText.text = string.Empty;
				this.resultGroup.flavorText.text = string.Empty;
			}
			this.getExperience = (float)UnityEngine.Random.Range(this.getFishInfo.MinExPoint, this.getFishInfo.MaxExPoint);
			this.resultGroup.experiencePointText.text = string.Format("{0}", this.getExperience);
			this.UpdateExperienceUI();
		}

		// Token: 0x060080B4 RID: 32948 RVA: 0x00368948 File Offset: 0x00366D48
		private void SetLayer(Transform _t, int _layer)
		{
			if (_t == null)
			{
				return;
			}
			for (int i = 0; i < _t.childCount; i++)
			{
				this.SetLayer(_t.GetChild(i), _layer);
			}
			_t.gameObject.layer = _layer;
		}

		// Token: 0x060080B5 RID: 32949 RVA: 0x00368994 File Offset: 0x00366D94
		private void UpdateExperienceUI()
		{
			AIProject.SaveData.Skill fishingSkill = Singleton<Manager.Map>.Instance.Player.PlayerData.FishingSkill;
			int maxLevel = Singleton<Manager.Resources>.Instance.FishingDefinePack.SystemParam.MaxLevel;
			if (maxLevel <= fishingSkill.Level)
			{
				this.resultGroup.levelText.color = this.redColor;
				this.resultGroup.levelText.text = "Lv Max";
				this.resultGroup.experienceBarImage.fillAmount = 1f;
			}
			else
			{
				this.resultGroup.levelText.color = this.whiteColor;
				this.resultGroup.levelText.text = string.Format("Lv {0,2}", fishingSkill.Level);
				this.resultGroup.experienceBarImage.fillAmount = fishingSkill.Experience / (float)fishingSkill.NextExperience;
			}
		}

		// Token: 0x060080B6 RID: 32950 RVA: 0x00368A78 File Offset: 0x00366E78
		public void StartDrawResult(FishInfo _info)
		{
			if (Singleton<Manager.Map>.IsInstance())
			{
				List<StuffItem> itemList = Singleton<Manager.Map>.Instance.Player.PlayerData.ItemList;
				itemList.AddItem(new StuffItem(_info.CategoryID, _info.ItemID, 1));
			}
			this.CloseAllCanvas();
			this.SetGetFishUI(_info);
			this.PlaySystemSE(SoundPack.SystemSE.Fishing_Result);
			this.StartDrawResultWindow();
		}

		// Token: 0x060080B7 RID: 32951 RVA: 0x00368ADC File Offset: 0x00366EDC
		private void StartDrawGetText()
		{
			if (this.drawGetTextDisposable != null)
			{
				this.drawGetTextDisposable.Dispose();
			}
			IEnumerator _coroutine = this.DrawGetText();
			this.drawGetTextDisposable = Observable.FromCoroutine(() => _coroutine, false).Subscribe<Unit>();
		}

		// Token: 0x060080B8 RID: 32952 RVA: 0x00368B30 File Offset: 0x00366F30
		private void StartDrawResultWindow()
		{
			if (this.drawResultWindowDisposable != null)
			{
				this.drawResultWindowDisposable.Dispose();
			}
			IEnumerator _coroutine = this.DrawFishParameter();
			this.drawResultWindowDisposable = Observable.FromCoroutine(() => _coroutine, false).Subscribe<Unit>();
		}

		// Token: 0x060080B9 RID: 32953 RVA: 0x00368B84 File Offset: 0x00366F84
		private IEnumerator DrawGetText()
		{
			Image _getImage = this.resultGroup.getImage;
			Vector3 _scale = Vector3.one * 3f;
			_getImage.transform.localScale = _scale;
			this.SetAlpha(_getImage, 0f);
			this.resultGroup.resultWindow.alpha = 0f;
			this.resultGroup.Show();
			for (float _count = 0f; _count < 0.5f; _count += Time.deltaTime)
			{
				float _t = Mathf.InverseLerp(0f, 0.5f, _count);
				_t = EasingFunctions.EaseInExpo(_t, 1f);
				_getImage.transform.localScale = Vector3.Lerp(_scale, Vector3.one, _t);
				this.SetAlpha(_getImage, _t);
				yield return null;
			}
			_getImage.transform.localScale = Vector3.one;
			this.SetAlpha(_getImage, 1f);
			this.StartDrawResultWindow();
			yield return Observable.Timer(TimeSpan.FromSeconds(2.0)).ToYieldInstruction<long>();
			float _startAlpha = _getImage.color.a;
			IConnectableObservable<float> _stream = ObservableEasing.Linear(0.25f, true).Publish<float>();
			_stream.Connect();
			_stream.Subscribe(delegate(float x)
			{
				float a = Mathf.Lerp(_startAlpha, 0f, x);
				this.SetAlpha(_getImage, a);
			});
			yield return _stream.ToYieldInstruction<float>();
			this.SetAlpha(_getImage, 0f);
			yield break;
		}

		// Token: 0x060080BA RID: 32954 RVA: 0x00368BA0 File Offset: 0x00366FA0
		private UnityEx.ValueTuple<int, float> GetNextFishingSkill(int _getExperience)
		{
			if (!Singleton<Manager.Map>.IsInstance())
			{
				return new UnityEx.ValueTuple<int, float>(1, 0f);
			}
			AIProject.SaveData.Skill fishingSkill = Singleton<Manager.Map>.Instance.Player.PlayerData.FishingSkill;
			int level = fishingSkill.Level;
			float num = (float)_getExperience + fishingSkill.Experience;
			int num2 = 0;
			for (;;)
			{
				float num3 = (float)fishingSkill.CalculationNextExp(level + num2);
				if (num3 > num)
				{
					break;
				}
				num -= num3;
				num2++;
			}
			return new UnityEx.ValueTuple<int, float>(level + num2, num);
		}

		// Token: 0x060080BB RID: 32955 RVA: 0x00368C28 File Offset: 0x00367028
		private IEnumerator DrawFishParameter()
		{
			bool _skipFlag = false;
			FishingManager FishingSystem = Singleton<Manager.Map>.Instance.FishingSystem;
			this.resultGroup.resultWindow.alpha = 0f;
			this.resultGroup.Show();
			float _fadeTime = 0.7f;
			_fadeTime = 0.7f;
			for (float count = 0f; count < 1f; count += Time.deltaTime * (1f / _fadeTime))
			{
				if (FishingSystem.NextButtonDown && !_skipFlag)
				{
					_skipFlag = true;
					break;
				}
				_skipFlag = false;
				this.resultGroup.resultWindow.alpha = count;
				yield return null;
			}
			this.resultGroup.resultWindow.alpha = 1f;
			for (float count2 = 0f; count2 < 1f; count2 += Time.deltaTime)
			{
				if (_skipFlag)
				{
					break;
				}
				if (FishingSystem.NextButtonDown && !_skipFlag)
				{
					_skipFlag = true;
					yield return null;
					break;
				}
				yield return null;
			}
			AIProject.SaveData.Skill _fishingSkill = Singleton<Manager.Map>.Instance.Player.PlayerData.FishingSkill;
			UnityEx.ValueTuple<int, float> _nextSkillInfo = this.GetNextFishingSkill((int)this.getExperience);
			int _maxLevel = Singleton<Manager.Resources>.Instance.FishingDefinePack.SystemParam.MaxLevel;
			if (_fishingSkill.Level < _maxLevel)
			{
				if (_maxLevel <= _nextSkillInfo.Item1)
				{
					_nextSkillInfo.Item1 = _maxLevel;
					_nextSkillInfo.Item2 = (float)_fishingSkill.CalculationNextExp(_maxLevel);
				}
				AIProject.SaveData.Skill skill = _fishingSkill;
				skill.OnLevelChanged = (Action<int, int>)Delegate.Combine(skill.OnLevelChanged, new Action<int, int>(this.DrawLevelUpText));
				float _experienceBarAddParSecond = Singleton<Manager.Resources>.Instance.FishingDefinePack.UIParam.ExperienceAddParSecond;
				_experienceBarAddParSecond = Mathf.Max(_experienceBarAddParSecond, 0f);
				float _t = _experienceBarAddParSecond / 100f;
				while (!FishingSystem.NextButtonDown || _skipFlag)
				{
					_skipFlag = false;
					if (this.getExperience > 0f)
					{
						float _nextExperience = (float)_fishingSkill.NextExperience;
						float _addValue = _nextExperience * _t * Time.deltaTime;
						this.getExperience -= _addValue;
						if (this.getExperience <= _addValue)
						{
							_addValue = this.getExperience;
							this.getExperience = 0f;
						}
						_fishingSkill.AddExperience(_addValue);
						if (_maxLevel > _fishingSkill.Level)
						{
							this.UpdateExperienceUI();
							yield return null;
							continue;
						}
						_fishingSkill.Level = _maxLevel;
						_fishingSkill.Experience = (float)_fishingSkill.CalculationNextExp(_maxLevel);
					}
					IL_44E:
					if (0f < this.getExperience)
					{
						_fishingSkill.AddExperience(this.getExperience);
					}
					AIProject.SaveData.Skill skill2 = _fishingSkill;
					skill2.OnLevelChanged = (Action<int, int>)Delegate.Remove(skill2.OnLevelChanged, new Action<int, int>(this.DrawLevelUpText));
					this.getExperience = 0f;
					if (_fishingSkill.Level < _nextSkillInfo.Item1)
					{
						this.DrawLevelUpText(_fishingSkill.Level, _nextSkillInfo.Item1);
					}
					_fishingSkill.Level = _nextSkillInfo.Item1;
					_fishingSkill.Experience = _nextSkillInfo.Item2;
					this.UpdateExperienceUI();
					goto IL_56B;
				}
				_skipFlag = true;
				goto IL_44E;
			}
			_fishingSkill.Level = _maxLevel;
			_fishingSkill.Experience = (float)_fishingSkill.CalculationNextExp(_maxLevel);
			this.UpdateExperienceUI();
			IL_56B:
			while (_skipFlag)
			{
				_skipFlag = false;
				yield return null;
			}
			if (this.finishingFishingWaitKeyDisposable != null)
			{
				this.finishingFishingWaitKeyDisposable.Dispose();
			}
			IEnumerator _coroutine = this.FinishingFishingWaitKey();
			this.finishingFishingWaitKeyDisposable = Observable.FromCoroutine(() => _coroutine, false).Subscribe<Unit>();
			yield break;
			yield break;
		}

		// Token: 0x060080BC RID: 32956 RVA: 0x00368C44 File Offset: 0x00367044
		private void DrawLevelUpText(int _prevLevel, int _newLevel)
		{
			if (this.drawLevelUpTextDisposable != null)
			{
				this.drawLevelUpTextDisposable.Dispose();
			}
			IEnumerator _coroutine = this.DrawLevelUpTextCoroutine();
			this.drawLevelUpTextDisposable = Observable.FromCoroutine(() => _coroutine, false).Subscribe<Unit>();
		}

		// Token: 0x060080BD RID: 32957 RVA: 0x00368C98 File Offset: 0x00367098
		private float EaseInCirc(float _count, float _limit)
		{
			float time = Mathf.InverseLerp(0f, _limit, _count);
			return EasingFunctions.EaseInCirc(time, 1f);
		}

		// Token: 0x060080BE RID: 32958 RVA: 0x00368CC4 File Offset: 0x003670C4
		private float EaseOutCirc(float _count, float _limit)
		{
			float time = Mathf.InverseLerp(0f, _limit, _count);
			return EasingFunctions.EaseOutCirc(time, 1f);
		}

		// Token: 0x060080BF RID: 32959 RVA: 0x00368CF0 File Offset: 0x003670F0
		private IEnumerator DrawLevelUpTextCoroutine()
		{
			Text _levelUpText = this.resultGroup.levelUpText;
			float _startAlpha = this.GetAlpha(_levelUpText);
			float _startCount = Mathf.Lerp(0f, 0.5f, Mathf.InverseLerp(0f, 1f, _startAlpha));
			this.PlaySystemSE(SoundPack.SystemSE.LevelUP);
			for (float _count = _startCount; _count < 0.5f; _count += Time.deltaTime)
			{
				this.SetAlpha(_levelUpText, this.EaseOutCirc(_count, 0.5f));
				yield return null;
			}
			float _count2 = 0.5f;
			while (0f <= _count2)
			{
				this.SetAlpha(_levelUpText, this.EaseOutCirc(_count2, 0.5f));
				yield return null;
				_count2 -= Time.deltaTime;
			}
			this.SetAlpha(_levelUpText, 0f);
			this.drawLevelUpTextDisposable = null;
			yield break;
		}

		// Token: 0x060080C0 RID: 32960 RVA: 0x00368D0C File Offset: 0x0036710C
		private IEnumerator FinishingFishingWaitKey()
		{
			FishingManager FishingSystem = Singleton<Manager.Map>.Instance.FishingSystem;
			while (FishingSystem.scene == FishingManager.FishingScene.Success)
			{
				if (FishingSystem.NextButtonDown)
				{
					this.PlaySystemSE(SoundPack.SystemSE.Cancel);
					yield return null;
					this.DestroyGetFishModel();
					bool _haveFood = this.HaveSomeFishFood();
					bool _filledSlot = this.FilledItemSlot();
					if (_haveFood && !_filledSlot)
					{
						FishingSystem.SelectFishFoodScene();
					}
					else
					{
						if (_filledSlot)
						{
							MapUIContainer.PushWarningMessage(Popup.Warning.Type.PouchIsFull);
						}
						else if (!_haveFood)
						{
							MapUIContainer.PushWarningMessage(Popup.Warning.Type.NonFishFood);
						}
						FishingSystem.EndFishing();
					}
					StuffItemInfo _itemInfo = Singleton<Manager.Resources>.Instance.GameInfo.GetItem(this.getFishInfo.CategoryID, this.getFishInfo.ItemID);
					if (_itemInfo != null)
					{
						PlayerActor player = Singleton<Manager.Map>.Instance.Player;
						StuffItem stuffItem = new StuffItem(_itemInfo.CategoryID, _itemInfo.ID, 1);
						MapUIContainer.AddSystemItemLog(_itemInfo, stuffItem.Count, true);
					}
					yield break;
				}
				yield return null;
			}
			yield break;
		}

		// Token: 0x060080C1 RID: 32961 RVA: 0x00368D28 File Offset: 0x00367128
		private void OpenAllCanvas()
		{
			this.SetActive(this.howToGroup, true);
			this.SetActive(this.fishFoodTypeGroup, true);
			this.selectFoodGroup.Show();
			this.hitWaitGroup.Show();
			this.fishingGroup.Show();
			this.resultGroup.Show();
		}

		// Token: 0x060080C2 RID: 32962 RVA: 0x00368D7C File Offset: 0x0036717C
		private void CloseAllCanvas()
		{
			this.SetActive(this.howToGroup, false);
			this.SetActive(this.fishFoodTypeGroup, false);
			this.selectFoodGroup.Hide();
			this.hitWaitGroup.Hide();
			this.fishingGroup.Hide();
			this.resultGroup.Hide();
		}

		// Token: 0x060080C3 RID: 32963 RVA: 0x00368DCF File Offset: 0x003671CF
		private void SetActive(Component _c, bool _a)
		{
			if (_c == null || _c.gameObject == null)
			{
				return;
			}
			if (_c.gameObject.activeSelf != _a)
			{
				_c.gameObject.SetActive(_a);
			}
		}

		// Token: 0x060080C4 RID: 32964 RVA: 0x00368E0C File Offset: 0x0036720C
		private void SetActive(GameObject _g, bool _a)
		{
			if (_g == null)
			{
				return;
			}
			if (_g.activeSelf != _a)
			{
				_g.SetActive(_a);
			}
		}

		// Token: 0x060080C5 RID: 32965 RVA: 0x00368E2E File Offset: 0x0036722E
		private void SetEnable(Behaviour _b, bool _a)
		{
			if (_b == null)
			{
				return;
			}
			if (_b.enabled != _a)
			{
				_b.enabled = _a;
			}
		}

		// Token: 0x060080C6 RID: 32966 RVA: 0x00368E50 File Offset: 0x00367250
		public void SetAlpha(Graphic _g, float _a)
		{
			if (_g == null)
			{
				return;
			}
			Color color = _g.color;
			color.a = _a;
			_g.color = color;
		}

		// Token: 0x060080C7 RID: 32967 RVA: 0x00368E80 File Offset: 0x00367280
		public float GetAlpha(Graphic _g)
		{
			if (_g == null)
			{
				return 0f;
			}
			return _g.color.a;
		}

		// Token: 0x060080C8 RID: 32968 RVA: 0x00368EAD File Offset: 0x003672AD
		private void PlaySystemSE(SoundPack.SystemSE se)
		{
			object obj = (!Singleton<Manager.Resources>.IsInstance()) ? null : Singleton<Manager.Resources>.Instance.SoundPack;
			if (obj != null)
			{
				obj.Play(se);
			}
		}

		// Token: 0x04006757 RID: 26455
		[SerializeField]
		private Color whiteColor = Color.white;

		// Token: 0x04006758 RID: 26456
		[SerializeField]
		private Color redColor = Color.red;

		// Token: 0x04006759 RID: 26457
		[SerializeField]
		private Color greenColor = Color.green;

		// Token: 0x0400675A RID: 26458
		[SerializeField]
		private Color lightGreenColor = Color.green;

		// Token: 0x0400675B RID: 26459
		[SerializeField]
		private Color blueColor = Color.blue;

		// Token: 0x0400675C RID: 26460
		[SerializeField]
		private Color yellowColor = Color.yellow;

		// Token: 0x0400675D RID: 26461
		[Space(5f)]
		[SerializeField]
		private CanvasGroup howToGroup;

		// Token: 0x0400675E RID: 26462
		[SerializeField]
		private GameObject howToElementPrefab;

		// Token: 0x0400675F RID: 26463
		[SerializeField]
		private CanvasGroup fishFoodTypeGroup;

		// Token: 0x04006760 RID: 26464
		private FishingHowToUI[] howToElements = new FishingHowToUI[0];

		// Token: 0x04006761 RID: 26465
		[SerializeField]
		private Image fishFoodImage;

		// Token: 0x04006762 RID: 26466
		[SerializeField]
		private Text fishFoodNumText;

		// Token: 0x04006763 RID: 26467
		[SerializeField]
		private Text fishFoodNumOverText;

		// Token: 0x04006764 RID: 26468
		[Space(5f)]
		[SerializeField]
		private FishingUI.SelectFoodGroup selectFoodGroup = new FishingUI.SelectFoodGroup();

		// Token: 0x04006765 RID: 26469
		[Space(5f)]
		[SerializeField]
		private FishingUI.HitWaitGroup hitWaitGroup = new FishingUI.HitWaitGroup();

		// Token: 0x04006766 RID: 26470
		[Space(5f)]
		[SerializeField]
		private FishingUI.FishingGroup fishingGroup = new FishingUI.FishingGroup();

		// Token: 0x04006767 RID: 26471
		[Space(5f)]
		[SerializeField]
		private FishingUI.ResultGroup resultGroup = new FishingUI.ResultGroup();

		// Token: 0x04006768 RID: 26472
		[SerializeField]
		private int _baitItemCategoryID;

		// Token: 0x04006769 RID: 26473
		[SerializeField]
		private ItemIDKeyPair _fakeBaitItemID = default(ItemIDKeyPair);

		// Token: 0x0400676A RID: 26474
		[SerializeField]
		private int _foodCountNormalFontSize = 40;

		// Token: 0x0400676B RID: 26475
		[SerializeField]
		private int _foodCountInfinityFontSize = 48;

		// Token: 0x0400676C RID: 26476
		private MenuUIBehaviour[] _menuUIElements;

		// Token: 0x0400676E RID: 26478
		private GameObject getFishModel;

		// Token: 0x0400676F RID: 26479
		private bool displayGuide = true;

		// Token: 0x04006770 RID: 26480
		private int selectFishFoodIndex;

		// Token: 0x04006772 RID: 26482
		private IDisposable updateDisposable;

		// Token: 0x04006773 RID: 26483
		private IDisposable lateUpdateDisposable;

		// Token: 0x04006775 RID: 26485
		private IDisposable fadeSubscriber;

		// Token: 0x04006776 RID: 26486
		private Dictionary<int, FishingUI.ArrowAnimParam> AnimTable = new Dictionary<int, FishingUI.ArrowAnimParam>();

		// Token: 0x04006777 RID: 26487
		private IDisposable foodChangeSubscriber;

		// Token: 0x04006778 RID: 26488
		private FishInfo getFishInfo = default(FishInfo);

		// Token: 0x04006779 RID: 26489
		private float getExperience;

		// Token: 0x0400677A RID: 26490
		private IDisposable drawHitTextSubscriber;

		// Token: 0x0400677B RID: 26491
		private IDisposable drawGetTextDisposable;

		// Token: 0x0400677C RID: 26492
		private IDisposable drawResultWindowDisposable;

		// Token: 0x0400677D RID: 26493
		private IDisposable drawLevelUpTextDisposable;

		// Token: 0x0400677E RID: 26494
		private IDisposable finishingFishingWaitKeyDisposable;

		// Token: 0x02000F34 RID: 3892
		public enum FadeType
		{
			// Token: 0x04006782 RID: 26498
			None,
			// Token: 0x04006783 RID: 26499
			FadeIn,
			// Token: 0x04006784 RID: 26500
			Wait,
			// Token: 0x04006785 RID: 26501
			FadeOut
		}

		// Token: 0x02000F35 RID: 3893
		public class ArrowAnimParam
		{
			// Token: 0x060080CC RID: 32972 RVA: 0x00368EE5 File Offset: 0x003672E5
			public ArrowAnimParam()
			{
			}

			// Token: 0x060080CD RID: 32973 RVA: 0x00368F24 File Offset: 0x00367324
			public ArrowAnimParam(int _moveIndex, Image _image)
			{
				this.moveIndex = _moveIndex;
				this.image = _image;
			}

			// Token: 0x060080CE RID: 32974 RVA: 0x00368F7C File Offset: 0x0036737C
			public ArrowAnimParam(int _moveIndex, Image _image, Color _normalColor, Vector3 _normalScale, Color _changeColor, Vector3 _changeScale)
			{
				this.moveIndex = _moveIndex;
				this.image = _image;
				this.normalColor = _normalColor;
				this.normalScale = _normalScale;
				this.changeColor = _changeColor;
				this.changeScale = _changeScale;
			}

			// Token: 0x170019D8 RID: 6616
			// (get) Token: 0x060080CF RID: 32975 RVA: 0x00368FF2 File Offset: 0x003673F2
			// (set) Token: 0x060080D0 RID: 32976 RVA: 0x00368FFA File Offset: 0x003673FA
			public FishingUI.FadeType FadeType { get; private set; }

			// Token: 0x170019D9 RID: 6617
			// (get) Token: 0x060080D1 RID: 32977 RVA: 0x00369003 File Offset: 0x00367403
			private static FishingDefinePack.UIParamGroup UIParam
			{
				[CompilerGenerated]
				get
				{
					return Singleton<Manager.Resources>.Instance.FishingDefinePack.UIParam;
				}
			}

			// Token: 0x170019DA RID: 6618
			// (get) Token: 0x060080D2 RID: 32978 RVA: 0x00369014 File Offset: 0x00367414
			// (set) Token: 0x060080D3 RID: 32979 RVA: 0x0036903C File Offset: 0x0036743C
			public Color IColor
			{
				get
				{
					return (!(this.image != null)) ? Color.white : this.image.color;
				}
				private set
				{
					if (this.image != null)
					{
						this.image.color = value;
					}
				}
			}

			// Token: 0x170019DB RID: 6619
			// (get) Token: 0x060080D4 RID: 32980 RVA: 0x0036905B File Offset: 0x0036745B
			// (set) Token: 0x060080D5 RID: 32981 RVA: 0x00369088 File Offset: 0x00367488
			public Vector3 IScale
			{
				get
				{
					return (!(this.image != null)) ? Vector3.one : this.image.transform.localScale;
				}
				set
				{
					if (this.image != null)
					{
						this.image.transform.localScale = value;
					}
				}
			}

			// Token: 0x170019DC RID: 6620
			// (get) Token: 0x060080D6 RID: 32982 RVA: 0x003690AC File Offset: 0x003674AC
			public static float DeltaTime
			{
				[CompilerGenerated]
				get
				{
					return Time.deltaTime;
				}
			}

			// Token: 0x060080D7 RID: 32983 RVA: 0x003690B4 File Offset: 0x003674B4
			public void Reset()
			{
				this.fadeTime = 0f;
				this.waitTime = 0f;
				this.FadeType = FishingUI.FadeType.None;
				if (this.disposable != null)
				{
					this.disposable.Dispose();
				}
				this.IColor = this.normalColor;
				this.IScale = this.normalScale;
			}

			// Token: 0x060080D8 RID: 32984 RVA: 0x00369110 File Offset: 0x00367510
			public void PlayFadeIn()
			{
				this.waitTime = 0f;
				FishingUI.FadeType fadeType = this.FadeType;
				if (fadeType != FishingUI.FadeType.FadeOut)
				{
					if (fadeType == FishingUI.FadeType.None)
					{
						this.fadeTime = 0f;
						if (this.disposable != null)
						{
							this.disposable.Dispose();
						}
						IEnumerator _coroutine = this.FadeIn();
						this.disposable = Observable.FromCoroutine(() => _coroutine, false).Subscribe<Unit>();
					}
				}
				else
				{
					if (this.disposable != null)
					{
						this.disposable.Dispose();
					}
					IEnumerator _coroutine = this.FadeIn();
					this.disposable = Observable.FromCoroutine(() => _coroutine, false).Subscribe<Unit>();
				}
			}

			// Token: 0x060080D9 RID: 32985 RVA: 0x003691E4 File Offset: 0x003675E4
			private IEnumerator FadeIn()
			{
				Color _startColor = this.IColor;
				Vector3 _startScale = this.IScale;
				this.FadeType = FishingUI.FadeType.FadeIn;
				while (this.fadeTime <= 1f)
				{
					this.EasingImage(this.image, this.fadeTime, 1f, _startColor, this.changeColor, _startScale, this.changeScale);
					yield return null;
					this.fadeTime += FishingUI.ArrowAnimParam.DeltaTime / FishingUI.ArrowAnimParam.UIParam.ArrowAnimFadeInTimeLimit;
				}
				this.SetImageState(this.changeColor, this.changeScale);
				this.FadeType = FishingUI.FadeType.Wait;
				this.waitTime = 0f;
				while (this.waitTime < FishingUI.ArrowAnimParam.UIParam.ArrowAnimWaitTimeLimit)
				{
					yield return null;
					this.waitTime += FishingUI.ArrowAnimParam.DeltaTime;
				}
				IEnumerator _coroutine = this.FadeOut();
				this.disposable = Observable.FromCoroutine(() => _coroutine, false).Subscribe<Unit>();
				yield break;
			}

			// Token: 0x060080DA RID: 32986 RVA: 0x00369200 File Offset: 0x00367600
			public void PlayFadeOut()
			{
				this.waitTime = FishingUI.ArrowAnimParam.UIParam.ArrowAnimWaitTimeLimit;
				if (this.disposable != null)
				{
					this.disposable.Dispose();
				}
				IEnumerator _coroutine = this.FadeOut();
				this.disposable = Observable.FromCoroutine(() => _coroutine, false).Subscribe<Unit>();
			}

			// Token: 0x060080DB RID: 32987 RVA: 0x00369264 File Offset: 0x00367664
			private IEnumerator FadeOut()
			{
				this.FadeType = FishingUI.FadeType.FadeOut;
				Color _startColor = this.IColor;
				Vector3 _startScale = this.IScale;
				while (0f <= this.fadeTime)
				{
					this.EasingImage(this.image, this.fadeTime, 1f, this.normalColor, _startColor, this.normalScale, _startScale);
					yield return null;
					this.fadeTime -= FishingUI.ArrowAnimParam.DeltaTime / FishingUI.ArrowAnimParam.UIParam.ArrowAnimFadeOutTimeLimit;
				}
				this.SetImageState(this.normalColor, this.normalScale);
				this.FadeType = FishingUI.FadeType.None;
				yield break;
			}

			// Token: 0x060080DC RID: 32988 RVA: 0x00369280 File Offset: 0x00367680
			public void PlayCancel()
			{
				this.waitTime = FishingUI.ArrowAnimParam.UIParam.ArrowAnimWaitTimeLimit;
				if (this.disposable != null)
				{
					this.disposable.Dispose();
				}
				IEnumerator _coroutine = this.Cancel();
				this.disposable = Observable.FromCoroutine(() => _coroutine, false).Subscribe<Unit>();
			}

			// Token: 0x060080DD RID: 32989 RVA: 0x003692E4 File Offset: 0x003676E4
			private IEnumerator Cancel()
			{
				this.FadeType = FishingUI.FadeType.FadeOut;
				Color _startColor = this.IColor;
				Vector3 _startScale = this.IScale;
				while (0f <= this.fadeTime)
				{
					this.EasingImage(this.image, this.fadeTime, 1f, this.normalColor, _startColor, this.normalScale, _startScale);
					yield return null;
					this.fadeTime -= FishingUI.ArrowAnimParam.DeltaTime / FishingUI.ArrowAnimParam.UIParam.ArrowAnimCancelFadeTimeLimit;
				}
				this.SetImageState(this.normalColor, this.normalScale);
				this.FadeType = FishingUI.FadeType.None;
				yield break;
			}

			// Token: 0x060080DE RID: 32990 RVA: 0x003692FF File Offset: 0x003676FF
			private void SetImageState(Color _color, Vector3 _scale)
			{
				this.IColor = _color;
				this.IScale = _scale;
			}

			// Token: 0x060080DF RID: 32991 RVA: 0x00369310 File Offset: 0x00367710
			private Color Lerp(Color a, Color b, float t)
			{
				return new Color(Mathf.Lerp(a.r, b.r, t), Mathf.Lerp(a.g, b.g, t), Mathf.Lerp(a.b, b.b, t), Mathf.Lerp(a.a, b.a, t));
			}

			// Token: 0x060080E0 RID: 32992 RVA: 0x00369374 File Offset: 0x00367774
			private void EasingImage(Image _image, float _count, float _limit, Color _color1, Color _color2, Vector3 _scale1, Vector3 _scale2)
			{
				float num = Mathf.InverseLerp(0f, _limit, _count);
				num = EasingFunctions.EaseOutQuint(num, 1f);
				this.IColor = this.Lerp(_color1, _color2, num);
				this.IScale = Vector3.Lerp(_scale1, _scale2, num);
			}

			// Token: 0x04006786 RID: 26502
			public int moveIndex;

			// Token: 0x04006787 RID: 26503
			public float fadeTime;

			// Token: 0x04006788 RID: 26504
			public float waitTime;

			// Token: 0x04006789 RID: 26505
			public IDisposable disposable;

			// Token: 0x0400678A RID: 26506
			public Image image;

			// Token: 0x0400678C RID: 26508
			public Color normalColor = Color.white;

			// Token: 0x0400678D RID: 26509
			public Color changeColor = Color.green;

			// Token: 0x0400678E RID: 26510
			public Vector3 normalScale = Vector3.one;

			// Token: 0x0400678F RID: 26511
			public Vector3 changeScale = Vector3.one * 1.3f;
		}

		// Token: 0x02000F36 RID: 3894
		public enum MouseInputType
		{
			// Token: 0x04006791 RID: 26513
			Left,
			// Token: 0x04006792 RID: 26514
			Right,
			// Token: 0x04006793 RID: 26515
			Wheel,
			// Token: 0x04006794 RID: 26516
			Move
		}

		// Token: 0x02000F37 RID: 3895
		[Serializable]
		public abstract class GroupBase
		{
			// Token: 0x060080E2 RID: 32994 RVA: 0x003698F9 File Offset: 0x00367CF9
			public void Show()
			{
				if (!this.group.gameObject.activeSelf)
				{
					this.group.gameObject.SetActive(true);
				}
			}

			// Token: 0x060080E3 RID: 32995 RVA: 0x00369921 File Offset: 0x00367D21
			public void Hide()
			{
				if (this.group.gameObject.activeSelf)
				{
					this.group.gameObject.SetActive(false);
				}
			}

			// Token: 0x060080E4 RID: 32996 RVA: 0x00369949 File Offset: 0x00367D49
			public void SetAlpha(float _f)
			{
				this.group.alpha = _f;
			}

			// Token: 0x170019DD RID: 6621
			// (get) Token: 0x060080E5 RID: 32997 RVA: 0x00369958 File Offset: 0x00367D58
			// (set) Token: 0x060080E6 RID: 32998 RVA: 0x003699A9 File Offset: 0x00367DA9
			public float Alpha
			{
				get
				{
					float? num = (this.group != null) ? new float?(this.group.alpha) : null;
					return (num == null) ? 0f : num.Value;
				}
				set
				{
					if (this.group)
					{
						this.group.alpha = value;
					}
				}
			}

			// Token: 0x04006795 RID: 26517
			public CanvasGroup group;
		}

		// Token: 0x02000F38 RID: 3896
		[Serializable]
		public class SelectFoodGroup : FishingUI.GroupBase
		{
			// Token: 0x04006796 RID: 26518
			public Text fishFoodNameText;

			// Token: 0x04006797 RID: 26519
			public Image selectUpImage;

			// Token: 0x04006798 RID: 26520
			public Image selectDownImage;
		}

		// Token: 0x02000F39 RID: 3897
		[Serializable]
		public class HitWaitGroup : FishingUI.GroupBase
		{
		}

		// Token: 0x02000F3A RID: 3898
		[Serializable]
		public class FishingGroup : FishingUI.GroupBase
		{
			// Token: 0x04006799 RID: 26521
			public float heartPointImageWidth = 800f;

			// Token: 0x0400679A RID: 26522
			public RectTransform[] heartPointRects = new RectTransform[0];

			// Token: 0x0400679B RID: 26523
			public Image timerImage;

			// Token: 0x0400679C RID: 26524
			public Image timerCircleImage;

			// Token: 0x0400679D RID: 26525
			public Image[] imageOutlines;

			// Token: 0x0400679E RID: 26526
			public CircleOutline[] circleOutlines;

			// Token: 0x0400679F RID: 26527
			public Image hitImage;
		}

		// Token: 0x02000F3B RID: 3899
		[Serializable]
		public class ResultGroup : FishingUI.GroupBase
		{
			// Token: 0x040067A0 RID: 26528
			public Image getImage;

			// Token: 0x040067A1 RID: 26529
			public CanvasGroup resultWindow;

			// Token: 0x040067A2 RID: 26530
			public Text fishNameText;

			// Token: 0x040067A3 RID: 26531
			public Text rarelityText;

			// Token: 0x040067A4 RID: 26532
			public RawImage fishImage;

			// Token: 0x040067A5 RID: 26533
			public Text flavorText;

			// Token: 0x040067A6 RID: 26534
			public Text experiencePointText;

			// Token: 0x040067A7 RID: 26535
			public Text levelText;

			// Token: 0x040067A8 RID: 26536
			public Image experienceBarImage;

			// Token: 0x040067A9 RID: 26537
			public Text levelUpText;
		}
	}
}
