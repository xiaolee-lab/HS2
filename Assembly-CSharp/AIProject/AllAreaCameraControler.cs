using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using AIProject.Definitions;
using AIProject.Scene;
using Manager;
using ReMotion;
using Rewired;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AIProject
{
	// Token: 0x02000FAC RID: 4012
	public class AllAreaCameraControler : MonoBehaviour
	{
		// Token: 0x17001D3D RID: 7485
		// (get) Token: 0x06008590 RID: 34192 RVA: 0x00375A36 File Offset: 0x00373E36
		public Image ClickedLabelImage
		{
			[CompilerGenerated]
			get
			{
				return this.clickedLabelImage;
			}
		}

		// Token: 0x17001D3E RID: 7486
		// (get) Token: 0x06008591 RID: 34193 RVA: 0x00375A3E File Offset: 0x00373E3E
		public Text ClickedLabelText
		{
			[CompilerGenerated]
			get
			{
				return this.clickedLabelText;
			}
		}

		// Token: 0x06008592 RID: 34194 RVA: 0x00375A48 File Offset: 0x00373E48
		public void Init()
		{
			this.Input = Singleton<Manager.Input>.Instance;
			this.DefaultSize = this.cam.orthographicSize;
			if (this.ActionFilter != null)
			{
				this.ActionFilter.Clear();
			}
			for (int i = 0; i < 29; i++)
			{
				MapUIActionCategory key = (MapUIActionCategory)i;
				this.ActionFilter.Add(key, new BoolReactiveProperty(true));
			}
			this.ActionFilterShip = new BoolReactiveProperty(true);
			this.areaMapUI = MapUIContainer.AllAreaMapUI;
			AllAreaMapObjects component = this.areaMapUI.GetComponent<AllAreaMapObjects>();
			this.Cursor = component.Cursor;
			this.PutGirlName = component.PutGirlName;
			this.warpListUI = component.WarpListUI;
			this.clickedLabelImage = component.ClickedLabelImage;
			this.clickedLabelText = component.ClickedLabelText;
			Sprite sprite = this.clickedLabelImage.sprite;
			Singleton<Manager.Resources>.Instance.itemIconTables.ActorIconTable.TryGetValue(Singleton<Manager.Map>.Instance.Player.ID, out sprite);
			this.clickedLabelImage.sprite = sprite;
			this.clickedLabelText.text = "：プレイヤー";
			this.CursorDefaultSize = this.Cursor.rectTransform.sizeDelta;
			this.actionPointIcon = this.MiniMap.GetActionIconList();
			this.actionPointHousingIcon = this.MiniMap.GetActionHousingIconList();
			this.PetPointIcon = this.MiniMap.GetPetIconList();
			this.BasePointIcon = this.MiniMap.GetIconList(0);
			this.DevicePointIcon = this.MiniMap.GetIconList(1);
			this.FarmPointIcon = this.MiniMap.GetIconList(2);
			this.FarmPointHousingIcon = this.MiniMap.GetIconList(4);
			this.EventPointIcon = this.MiniMap.GetIconList(3);
			this.ShipPointIcon = this.MiniMap.GetIconList(5);
			this.CraftPointIcon = this.MiniMap.GetIconList(6);
			this.JukePointIcon = this.MiniMap.GetIconList(7);
			if (this.ActionPointIcon != null)
			{
				this.ActionPointIcon.Clear();
			}
			for (int j = 0; j < this.actionPointIcon.Count; j++)
			{
				this.ActionPointIcon.Add(this.actionPointIcon[j].Icon.GetComponentInChildren<Image>());
			}
			if (this.ActionPointHousingIcon != null)
			{
				this.ActionPointHousingIcon.Clear();
			}
			for (int k = 0; k < this.actionPointHousingIcon.Count; k++)
			{
				this.ActionPointHousingIcon.Add(this.actionPointHousingIcon[k].Icon.GetComponentInChildren<Image>());
			}
			this.InitPosition();
			Vector3 position = Singleton<Manager.Map>.Instance.Merchant.Position;
			RectTransformUtility.WorldToScreenPoint(this.camUI, position).z = 0f;
			this.GirlIcons = this.MiniMap.GetGirlsIcon();
			this.MerchantIcon = Singleton<MapUIContainer>.Instance.MinimapUI.GetMerchantIcon();
			this.MerchantIconCanvas = Singleton<MapUIContainer>.Instance.MinimapUI.GetMerchantCanvas();
			for (int l = 0; l < 30; l++)
			{
				MapUIActionCategory mapUIActionCategory = (MapUIActionCategory)l;
				if (mapUIActionCategory != MapUIActionCategory.HARBOR)
				{
					this.ActionFilter[mapUIActionCategory].Subscribe(delegate(bool x)
					{
						this.CheckShowIconsFilter();
					});
				}
				else
				{
					this.ActionFilterShip.Subscribe(delegate(bool x)
					{
						this.CheckShowIconsFilter();
					});
				}
			}
		}

		// Token: 0x06008593 RID: 34195 RVA: 0x00375DB8 File Offset: 0x003741B8
		public void InitPosition()
		{
			Vector3 position = base.transform.position;
			this.CameraMoveLimitMin = this._MinCameraMoveLimit[1];
			this.CameraMoveLimitMax = this._MaxCameraMoveLimit[1];
			Vector3 position2 = Singleton<Manager.Map>.Instance.Player.Position;
			position.x = position2.x;
			position.z = position2.z;
			this.IsLimit[0] = (position.x <= this.CameraMoveLimitMin.x);
			this.IsLimit[1] = (position.x >= this.CameraMoveLimitMax.x);
			this.IsLimit[2] = (position.z <= this.CameraMoveLimitMin.y);
			this.IsLimit[3] = (position.z >= this.CameraMoveLimitMax.y);
			if (this.IsLimit[0])
			{
				position.x = this.CameraMoveLimitMin.x;
			}
			else if (this.IsLimit[1])
			{
				position.x = this.CameraMoveLimitMax.x;
			}
			if (this.IsLimit[2])
			{
				position.z = this.CameraMoveLimitMin.y;
			}
			else if (this.IsLimit[3])
			{
				position.z = this.CameraMoveLimitMax.y;
			}
			base.transform.position = position;
			this.cursolWorldPos = position2;
			float num = this.cursolWorldPos.x - Singleton<Manager.Map>.Instance.Player.Position.x;
			float num2 = this.cursolWorldPos.z - Singleton<Manager.Map>.Instance.Player.Position.z;
			float distance = num * num + num2 * num2;
			this.OnActionPoint.Name = "プレイヤー";
			this.OnActionPoint.OnIcon = true;
			this.OnActionPoint.Distance = distance;
			this.OnActionPoint.Position = Singleton<Manager.Map>.Instance.Player.Position;
			this.OnActionPoint.CanWarp = false;
			this.OnActionPoint.TargetObj = Singleton<Manager.Map>.Instance.Player.gameObject;
			this.OnActionPoint.kind = 0;
			this.OnActionPoint.num = 0;
			this.TargetPos = this.OnActionPoint.Position;
			Vector2 vector = RectTransformUtility.WorldToScreenPoint(this.camUI, this.cursolWorldPos);
			position2.x = vector.x;
			position2.y = vector.y;
			position2.z = 0f;
			this.Cursor.transform.position = position2;
			this.Cursor.color = new Color(this.Cursor.color.r, this.Cursor.color.g, this.Cursor.color.b, 0f);
		}

		// Token: 0x06008594 RID: 34196 RVA: 0x003760C4 File Offset: 0x003744C4
		private void Update()
		{
			if (!this.Cursor.enabled)
			{
				this.Cursor.enabled = true;
			}
			Vector3 position = this.Cursor.transform.position;
			Vector3 position2 = GameCursor.pos;
			Vector3 position3 = base.transform.position;
			bool flag = Singleton<Game>.Instance.Dialog != null;
			if (!AllAreaCameraControler.NowWarp)
			{
				float num = this.Input.ScrollValue();
				if (!this.nowZooming)
				{
					bool flag2 = EventSystem.current != null && !EventSystem.current.IsPointerOverGameObject();
					if (num == 0f)
					{
						if (this.ZoomPattern > 0 && (this.Input.IsPressedKey(KeyCode.RightBracket) || this.Input.IsPressedKey(ActionID.RightShoulder2)))
						{
							this.ZoomPattern--;
							this.startZoom = this.cam.orthographicSize;
							this.NowZoomTime = 0f;
							this.targetZoom = this.ScreenSize[this.ZoomPattern];
						}
						else if (this.ZoomPattern < this.ScreenSize.Length - 1 && (this.Input.IsPressedKey(KeyCode.Equals) || this.Input.IsPressedKey(ActionID.LeftShoulder2)))
						{
							this.ZoomPattern++;
							this.startZoom = this.cam.orthographicSize;
							this.NowZoomTime = 0f;
							this.targetZoom = this.ScreenSize[this.ZoomPattern];
						}
						else if (this.Input.IsPressedKey(KeyCode.Semicolon))
						{
							this.ZoomPattern = this.DefaultScreenSizeID;
							this.startZoom = this.cam.orthographicSize;
							this.NowZoomTime = 0f;
							this.targetZoom = this.ScreenSize[this.ZoomPattern];
						}
					}
					else if (num != 0f && flag2)
					{
						float num2 = this.WheelZoomSpeed * num;
						if ((num > 0f && this.cam.orthographicSize != this.ScreenSize[0]) || (num < 0f && this.cam.orthographicSize != this.ScreenSize[this.ScreenSize.Length - 1]))
						{
							this.cam.orthographicSize -= num2;
							this.startZoom = this.targetZoom;
							if (num < 0f)
							{
								if (this.cam.orthographicSize > this.ScreenSize[this.ScreenSize.Length - 1])
								{
									num2 += this.cam.orthographicSize - this.ScreenSize[this.ScreenSize.Length - 1];
								}
							}
							else if (this.cam.orthographicSize < this.ScreenSize[0])
							{
								num2 -= this.cam.orthographicSize - this.ScreenSize[0];
							}
							this.cam.orthographicSize = Mathf.Clamp(this.cam.orthographicSize, this.ScreenSize[0], this.ScreenSize[this.ScreenSize.Length - 1]);
							if (this.cam.orthographicSize <= this.ScreenSize[1])
							{
								float t = Mathf.InverseLerp(this.ScreenSize[0], this.ScreenSize[1], this.cam.orthographicSize);
								this.CameraMoveLimitMin = Vector2.Lerp(this._MinCameraMoveLimit[0], this._MinCameraMoveLimit[1], t);
								this.CameraMoveLimitMax = Vector2.Lerp(this._MaxCameraMoveLimit[0], this._MaxCameraMoveLimit[1], t);
							}
							else
							{
								float t2 = Mathf.InverseLerp(this.ScreenSize[1], this.ScreenSize[2], this.cam.orthographicSize);
								this.CameraMoveLimitMin = Vector2.Lerp(this._MinCameraMoveLimit[1], this._MinCameraMoveLimit[2], t2);
								this.CameraMoveLimitMax = Vector2.Lerp(this._MaxCameraMoveLimit[1], this._MaxCameraMoveLimit[2], t2);
							}
						}
					}
				}
				this.CameraZoom();
				this.CheckShowIconsFilter();
				Vector2 cameraAxis = this.Input.CameraAxis;
				bool flag3 = true;
				Controller lastActiveController = ReInput.players.GetPlayer(0).controllers.GetLastActiveController();
				flag3 = ((lastActiveController == null) ? flag3 : (lastActiveController.type == ControllerType.Mouse));
				if (!flag && !flag3)
				{
					position3.x -= cameraAxis.x * this.cursorMoveSpeed;
					position3.z -= cameraAxis.y * this.cursorMoveSpeed;
				}
				position2 = this.cam.ScreenToWorldPoint(position2);
				if (this.Input.IsDown(ActionID.MouseLeft))
				{
					position3.x += this.Input.MouseAxis.x * this.dragMoveSpeed * this.cam.orthographicSize / this.ScreenSize[1];
					position3.z += this.Input.MouseAxis.y * this.dragMoveSpeed * this.cam.orthographicSize / this.ScreenSize[1];
				}
			}
			this.IsLimit[0] = (position3.x <= this.CameraMoveLimitMin.x);
			this.IsLimit[1] = (position3.x >= this.CameraMoveLimitMax.x);
			this.IsLimit[2] = (position3.z <= this.CameraMoveLimitMin.y);
			this.IsLimit[3] = (position3.z >= this.CameraMoveLimitMax.y);
			if (this.IsLimit[0])
			{
				position3.x = this.CameraMoveLimitMin.x;
			}
			else if (this.IsLimit[1])
			{
				position3.x = this.CameraMoveLimitMax.x;
			}
			if (this.IsLimit[2])
			{
				position3.z = this.CameraMoveLimitMin.y;
			}
			else if (this.IsLimit[3])
			{
				position3.z = this.CameraMoveLimitMax.y;
			}
			if (!flag)
			{
				base.transform.position = position3;
			}
			if (this.OnActionPoint.kind == 1)
			{
				this.TargetPos = this.OnActionPoint.TargetObj.GetComponentInChildren<AgentActor>().Position;
			}
			else if (this.OnActionPoint.kind == 2)
			{
				this.TargetPos = this.OnActionPoint.TargetObj.GetComponentInChildren<MerchantActor>().Position;
			}
			if (this.TargetPos.x != this.cursolWorldPos.x && this.TargetPos.z != this.cursolWorldPos.z)
			{
				this.cursolWorldPos = this.TargetPos;
			}
			position = RectTransformUtility.WorldToScreenPoint(this.camUI, this.cursolWorldPos);
			position.z = 0f;
			if (this.IsLimit[0])
			{
				float x3 = RectTransformUtility.WorldToScreenPoint(this.camUI, new Vector3(this.CameraMoveLimitMax.x + this.cam.orthographicSize * this.cam.aspect, 0f, 0f)).x;
				position.x = Mathf.Clamp(position.x, x3, (float)(Screen.width - 1) - this.CursorDefaultSize.x / 2f);
			}
			else if (this.IsLimit[1])
			{
				float x2 = RectTransformUtility.WorldToScreenPoint(this.camUI, new Vector3(this.CameraMoveLimitMin.x - this.cam.orthographicSize * this.cam.aspect, 0f, 0f)).x;
				position.x = Mathf.Clamp(position.x, this.CursorDefaultSize.x / 2f, x2 - this.CursorDefaultSize.x / 2f);
			}
			if (this.IsLimit[2])
			{
				float y = RectTransformUtility.WorldToScreenPoint(this.camUI, new Vector3(0f, 0f, this.CameraMoveLimitMax.y + this.cam.orthographicSize)).y;
				position.y = Mathf.Clamp(position.y, y + this.CursorDefaultSize.y / 2f, (float)(Screen.height - 1) - this.CursorDefaultSize.y / 2f);
			}
			else if (this.IsLimit[3])
			{
				float y2 = RectTransformUtility.WorldToScreenPoint(this.camUI, new Vector3(0f, 0f, this.CameraMoveLimitMin.y - this.cam.orthographicSize)).y;
				position.y = Mathf.Clamp(position.y, this.CursorDefaultSize.y / 2f, y2 - this.CursorDefaultSize.y / 2f);
			}
			this.Cursor.transform.position = position;
			this.ChangeMarkerRotAndSize();
			this.camUI.orthographicSize = this.cam.orthographicSize;
			if (this.Input.IsPressedKey(ActionID.MouseLeft) && !flag && !AllAreaCameraControler.NowWarp)
			{
				position2 = GameCursor.pos;
				position2.z = 0f;
				Vector3[] tmpPos = new Vector3[]
				{
					this.cam.ScreenToWorldPoint(position2),
					this.cam.transform.position
				};
				this.CheckNamePanel(tmpPos[0]);
				if (this.OnActionPoint.OnIcon && this.OnActionPoint.CanWarp)
				{
					tmpPos[0].y = tmpPos[1].y;
					if (this.WarpSelectSubscriber != null)
					{
						if (AllAreaCameraControler.NowWarp)
						{
							AllAreaCameraControler.NowWarp = false;
						}
						this.WarpSelectSubscriber.Dispose();
					}
					this.WarpSelectSubscriber = ObservableEasing.EaseOutQuint(1f, true).FrameTimeInterval(true).Subscribe(delegate(TimeInterval<float> x)
					{
						if (!AllAreaCameraControler.NowWarp)
						{
							AllAreaCameraControler.NowWarp = true;
						}
						this.cam.transform.position = Vector3.Lerp(tmpPos[1], tmpPos[0], x.Value);
					}, delegate()
					{
						AllAreaCameraControler.NowWarp = false;
					});
					ConfirmScene.Sentence = "このポイントに移動しますか";
					ConfirmScene.OnClickedYes = delegate()
					{
						MiniMapControler.OnWarp warpProc = this.MiniMap.WarpProc;
						if (warpProc != null)
						{
							warpProc(this.OnActionPoint.Point.GetComponent<BasePoint>());
						}
						this.MiniMap.WarpProc = null;
						this.MiniMap.ChangeCamera(false, true);
						this.Input.FocusLevel = 0;
						this.Input.ReserveState(Manager.Input.ValidType.Action);
						this.Input.SetupState();
						Singleton<Manager.Map>.Instance.Player.SetScheduledInteractionState(true);
						Singleton<Manager.Map>.Instance.Player.ReleaseInteraction();
						Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_L);
						if (AllAreaCameraControler.NowWarp)
						{
							AllAreaCameraControler.NowWarp = false;
						}
					};
					ConfirmScene.OnClickedNo = delegate()
					{
						this.Input.ClearMenuElements();
						this.Input.FocusLevel = this.warpListUI.FocusLevel;
						this.Input.MenuElements = this.warpListUI.MenuUIList;
						this.Input.ReserveState(Manager.Input.ValidType.UI);
						this.Input.SetupState();
						Singleton<Manager.Map>.Instance.Player.SetScheduledInteractionState(false);
						Singleton<Manager.Map>.Instance.Player.ReleaseInteraction();
						Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Cancel);
						if (AllAreaCameraControler.NowWarp)
						{
							AllAreaCameraControler.NowWarp = false;
						}
					};
					Singleton<Game>.Instance.LoadDialog();
				}
			}
		}

		// Token: 0x06008595 RID: 34197 RVA: 0x00376BEF File Offset: 0x00374FEF
		public void CursorSet()
		{
			this.ChangeMarkerMode = 1;
			this.sclTime = 0f;
		}

		// Token: 0x06008596 RID: 34198 RVA: 0x00376C04 File Offset: 0x00375004
		private void ChangeMarkerRotAndSize()
		{
			Vector3 localScale = new Vector3(1f, 1f, 1f);
			float y = this.Cursor.transform.localScale.x;
			Color color = this.Cursor.color;
			Outline component = this.Cursor.GetComponent<Outline>();
			Color effectColor = component.effectColor;
			if (this.ChangeMarkerMode == 1)
			{
				if (this.MarkerChangeWait)
				{
					if (this.MarkerChangeWaitTime < this.MaxMarkerWaitTime)
					{
						this.MarkerChangeWaitTime += Time.unscaledDeltaTime;
						return;
					}
					this.MarkerChangeWait = false;
					this.sclTime = 0f;
					this.ChangeMarkerMode = 0;
					return;
				}
				else
				{
					float num = this.sclTime / this.MarkerAppearTime;
					if (num >= 1f)
					{
						num = 1f;
					}
					y = Mathf.Lerp(2f, this.MarkerMinScl / (this.cam.orthographicSize / this.ScreenSize[0]), num);
					color.a = Mathf.Lerp(0f, 1f, num);
					effectColor.a = Mathf.Lerp(0f, 1f, num);
					if (Mathf.Approximately(num, 1f))
					{
						this.MarkerChangeWait = true;
						this.MarkerChangeWaitTime = 0f;
					}
					this.Cursor.color = color;
					component.effectColor = effectColor;
				}
			}
			else if (this.ChangeMarkerMode == 0)
			{
				if (Mathf.Approximately(color.a, 0f))
				{
					color.a = 1f;
					effectColor.a = 1f;
					this.Cursor.color = color;
					component.effectColor = effectColor;
				}
				float num2 = this.sclTime * 360f / this.MarkerChangeTime;
				y = (this.MarkerMaxScl - this.MarkerMinScl) * (Mathf.Sin(num2 * 0.017453292f) + 1f) * 0.5f + this.MarkerMinScl / (this.cam.orthographicSize / this.ScreenSize[0]);
			}
			this.sclTime += Time.unscaledDeltaTime;
			localScale.x = (localScale.y = y);
			this.Cursor.transform.localScale = localScale;
		}

		// Token: 0x06008597 RID: 34199 RVA: 0x00376E4C File Offset: 0x0037524C
		private void CheckNamePanel(Vector3 pos)
		{
			float[] array = new float[2];
			float num = 0f;
			bool flag = false;
			array[0] = this.iconSeachRange * this.iconSeachRange;
			array[1] = array[0];
			AllAreaCameraControler.CalcOnIconInfo[] array2 = new AllAreaCameraControler.CalcOnIconInfo[14];
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i] = default(AllAreaCameraControler.CalcOnIconInfo);
				array2[i].Name = string.Empty;
				array2[i].OnIcon = false;
				array2[i].Distance = array[0];
				array2[i].Position = Vector3.zero;
				array2[i].CanWarp = false;
				array2[i].TargetObj = null;
				array2[i].Point = null;
			}
			if (this.CheckSerch(pos, Singleton<Manager.Map>.Instance.Player.Position, array[1], ref num))
			{
				array2[0].Name = "プレイヤー";
				array2[0].OnIcon = true;
				array2[0].Distance = num;
				array2[0].Position = Singleton<Manager.Map>.Instance.Player.Position;
				array2[0].CanWarp = false;
				array2[0].TargetObj = Singleton<Manager.Map>.Instance.Player.gameObject;
				array2[0].kind = 0;
				array2[0].num = 0;
			}
			array[1] = array[0];
			foreach (KeyValuePair<int, AgentActor> keyValuePair in Singleton<Manager.Map>.Instance.AgentTable)
			{
				if (!(keyValuePair.Value == null))
				{
					if (this.CheckSerch(pos, keyValuePair.Value.Position, array[1], ref num))
					{
						array[1] = num;
						array2[1].Name = keyValuePair.Value.CharaName;
						array2[1].OnIcon = true;
						array2[1].Distance = num;
						array2[1].Position = keyValuePair.Value.Position;
						array2[1].CanWarp = false;
						array2[1].TargetObj = keyValuePair.Value.gameObject;
						array2[1].kind = 1;
						array2[1].num = keyValuePair.Key;
					}
				}
			}
			array[1] = array[0];
			if (this.CheckSerch(pos, Singleton<Manager.Map>.Instance.Merchant.Position, array[1], ref num))
			{
				array[1] = num;
				array2[2].Name = Singleton<Manager.Map>.Instance.Merchant.CharaName;
				array2[2].OnIcon = (Singleton<Manager.Map>.Instance.Merchant.CurrentMode != Merchant.ActionType.Absent);
				array2[2].Distance = num;
				array2[2].Position = Singleton<Manager.Map>.Instance.Merchant.Position;
				array2[2].CanWarp = false;
				array2[2].TargetObj = Singleton<Manager.Map>.Instance.Merchant.gameObject;
				array2[2].kind = 2;
				array2[2].num = 0;
			}
			array[1] = array[0];
			for (int j = 0; j < this.ActionPointIcon.Count; j++)
			{
				if (this.ActionPointIcon[j].enabled)
				{
					if (this.actionPointIcon[j].Icon.activeSelf)
					{
						if (this.CheckSerch(pos, this.ActionPointIcon[j].transform.position, array[1], ref num))
						{
							array[1] = num;
							array2[3].Name = this.actionPointIcon[j].Name;
							array2[3].OnIcon = true;
							array2[3].Distance = num;
							array2[3].Position = this.ActionPointIcon[j].transform.position;
							array2[3].CanWarp = false;
							array2[3].TargetObj = this.actionPointIcon[j].Point.gameObject;
							array2[3].Point = this.actionPointIcon[j].Point;
							array2[3].kind = 3;
							array2[3].num = j;
						}
					}
				}
			}
			array[1] = array[0];
			for (int k = 0; k < this.PetPointIcon.Count; k++)
			{
				Image componentInChildren = this.PetPointIcon[k].Icon.GetComponentInChildren<Image>();
				if (componentInChildren.enabled)
				{
					if (this.PetPointIcon[k].Icon.gameObject.activeSelf)
					{
						if (this.CheckSerch(pos, componentInChildren.transform.position, array[1], ref num))
						{
							array[1] = num;
							array2[4].Name = this.PetPointIcon[k].Name;
							array2[4].OnIcon = true;
							array2[4].Distance = num;
							array2[4].Position = componentInChildren.transform.position;
							array2[4].CanWarp = false;
							array2[4].TargetObj = this.PetPointIcon[k].obj;
							array2[4].kind = 4;
							array2[4].num = k;
						}
					}
				}
			}
			array[1] = array[0];
			for (int l = 0; l < this.BasePointIcon.Count; l++)
			{
				Image componentInChildren = this.BasePointIcon[l].Icon.GetComponentInChildren<Image>();
				if (componentInChildren.enabled)
				{
					if (this.BasePointIcon[l].Icon.gameObject.activeSelf)
					{
						if (this.CheckSerch(pos, componentInChildren.transform.position, array[1], ref num))
						{
							array[1] = num;
							array2[5].Name = this.BasePointIcon[l].Name;
							bool canWarp = false;
							BasePoint component = this.BasePointIcon[l].Point.GetComponent<BasePoint>();
							if (!Singleton<Manager.Map>.Instance.GetBasePointOpenState(component.ID, out canWarp))
							{
								canWarp = false;
							}
							array2[5].OnIcon = true;
							array2[5].Distance = num;
							array2[5].Position = componentInChildren.transform.position;
							array2[5].CanWarp = canWarp;
							array2[5].TargetObj = this.BasePointIcon[l].Point.gameObject;
							array2[5].Point = this.BasePointIcon[l].Point;
							array2[5].kind = 5;
							array2[5].num = l;
						}
					}
				}
			}
			array[1] = array[0];
			for (int m = 0; m < this.DevicePointIcon.Count; m++)
			{
				Image componentInChildren = this.DevicePointIcon[m].Icon.GetComponentInChildren<Image>();
				if (componentInChildren.enabled)
				{
					if (this.DevicePointIcon[m].Icon.gameObject.activeSelf)
					{
						if (this.CheckSerch(pos, componentInChildren.transform.position, array[1], ref num))
						{
							array[1] = num;
							array2[6].Name = this.DevicePointIcon[m].Name;
							array2[6].OnIcon = true;
							array2[6].Distance = num;
							array2[6].Position = componentInChildren.transform.position;
							array2[6].CanWarp = false;
							array2[6].TargetObj = this.DevicePointIcon[m].Point.gameObject;
							array2[6].Point = this.DevicePointIcon[m].Point;
							array2[6].kind = 6;
							array2[6].num = m;
						}
					}
				}
			}
			array[1] = array[0];
			for (int n = 0; n < this.FarmPointIcon.Count; n++)
			{
				Image componentInChildren = this.FarmPointIcon[n].Icon.GetComponentInChildren<Image>();
				if (componentInChildren.enabled)
				{
					if (this.FarmPointIcon[n].Icon.gameObject.activeSelf)
					{
						if (this.CheckSerch(pos, componentInChildren.transform.position, array[1], ref num))
						{
							array[1] = num;
							array2[7].Name = this.FarmPointIcon[n].Name;
							array2[7].OnIcon = true;
							array2[7].Distance = num;
							array2[7].Position = componentInChildren.transform.position;
							array2[7].CanWarp = false;
							array2[7].TargetObj = this.FarmPointIcon[n].Point.gameObject;
							array2[7].Point = this.FarmPointIcon[n].Point;
							array2[7].kind = 7;
							array2[7].num = n;
						}
					}
				}
			}
			for (int num2 = 0; num2 < this.EventPointIcon.Count; num2++)
			{
				Image componentInChildren = this.EventPointIcon[num2].Icon.GetComponentInChildren<Image>();
				if (componentInChildren.enabled)
				{
					if (this.EventPointIcon[num2].Icon.gameObject.activeSelf)
					{
						if (this.CheckSerch(pos, componentInChildren.transform.position, array[1], ref num))
						{
							array[1] = num;
							array2[8].Name = this.EventPointIcon[num2].Name;
							array2[8].OnIcon = true;
							array2[8].Distance = num;
							array2[8].Position = componentInChildren.transform.position;
							array2[8].CanWarp = false;
							array2[8].TargetObj = this.EventPointIcon[num2].Point.gameObject;
							array2[8].Point = this.EventPointIcon[num2].Point;
							array2[8].kind = 8;
							array2[8].num = num2;
						}
					}
				}
			}
			array[1] = array[0];
			for (int num3 = 0; num3 < this.ActionPointHousingIcon.Count; num3++)
			{
				if (this.ActionPointHousingIcon[num3].enabled)
				{
					if (this.actionPointHousingIcon[num3].Icon.activeSelf)
					{
						if (this.CheckSerch(pos, this.ActionPointHousingIcon[num3].transform.position, array[1], ref num))
						{
							array[1] = num;
							array2[9].Name = this.actionPointHousingIcon[num3].Name;
							array2[9].OnIcon = true;
							array2[9].Distance = num;
							array2[9].Position = this.ActionPointHousingIcon[num3].transform.position;
							array2[9].CanWarp = false;
							array2[9].TargetObj = this.actionPointHousingIcon[num3].Point.gameObject;
							array2[9].Point = this.actionPointHousingIcon[num3].Point;
							array2[9].kind = 3;
							array2[9].num = num3;
						}
					}
				}
			}
			array[1] = array[0];
			for (int num4 = 0; num4 < this.FarmPointHousingIcon.Count; num4++)
			{
				if (this.FarmPointHousingIcon[num4].Point.GetComponent<FarmPoint>().Kind != FarmPoint.FarmKind.Well)
				{
					Image componentInChildren = this.FarmPointHousingIcon[num4].Icon.GetComponentInChildren<Image>();
					if (componentInChildren.enabled)
					{
						if (this.FarmPointHousingIcon[num4].Icon.gameObject.activeSelf)
						{
							if (this.CheckSerch(pos, componentInChildren.transform.position, array[1], ref num))
							{
								array[1] = num;
								array2[10].Name = this.FarmPointHousingIcon[num4].Name;
								array2[10].OnIcon = true;
								array2[10].Distance = num;
								array2[10].Position = componentInChildren.transform.position;
								array2[10].CanWarp = false;
								array2[10].TargetObj = this.FarmPointHousingIcon[num4].Point.gameObject;
								array2[10].Point = this.FarmPointHousingIcon[num4].Point;
								array2[10].kind = 10;
								array2[10].num = num4;
							}
						}
					}
				}
			}
			array[1] = array[0];
			for (int num5 = 0; num5 < this.ShipPointIcon.Count; num5++)
			{
				Image componentInChildren = this.ShipPointIcon[num5].Icon.GetComponentInChildren<Image>();
				if (componentInChildren.enabled)
				{
					if (this.ShipPointIcon[num5].Icon.gameObject.activeSelf)
					{
						if (this.CheckSerch(pos, componentInChildren.transform.position, array[1], ref num))
						{
							array[1] = num;
							array2[11].Name = this.ShipPointIcon[num5].Name;
							array2[11].OnIcon = true;
							array2[11].Distance = num;
							array2[11].Position = componentInChildren.transform.position;
							array2[11].CanWarp = false;
							array2[11].TargetObj = this.ShipPointIcon[num5].Point.gameObject;
							array2[11].Point = this.ShipPointIcon[num5].Point;
							array2[11].kind = 11;
							array2[11].num = num5;
						}
					}
				}
			}
			array[1] = array[0];
			for (int num6 = 0; num6 < this.CraftPointIcon.Count; num6++)
			{
				Image componentInChildren = this.CraftPointIcon[num6].Icon.GetComponentInChildren<Image>();
				if (componentInChildren.enabled)
				{
					if (this.CraftPointIcon[num6].Icon.gameObject.activeSelf)
					{
						if (this.CheckSerch(pos, componentInChildren.transform.position, array[1], ref num))
						{
							array[1] = num;
							array2[12].Name = this.CraftPointIcon[num6].Name;
							array2[12].OnIcon = true;
							array2[12].Distance = num;
							array2[12].Position = componentInChildren.transform.position;
							array2[12].CanWarp = false;
							array2[12].TargetObj = this.CraftPointIcon[num6].Point.gameObject;
							array2[12].Point = this.CraftPointIcon[num6].Point;
							array2[12].kind = 12;
							array2[12].num = num6;
						}
					}
				}
			}
			array[1] = array[0];
			for (int num7 = 0; num7 < this.JukePointIcon.Count; num7++)
			{
				Image componentInChildren = this.JukePointIcon[num7].Icon.GetComponentInChildren<Image>();
				if (componentInChildren.enabled)
				{
					if (this.JukePointIcon[num7].Icon.gameObject.activeSelf)
					{
						if (this.CheckSerch(pos, componentInChildren.transform.position, array[1], ref num))
						{
							array[1] = num;
							array2[13].Name = this.JukePointIcon[num7].Name;
							array2[13].OnIcon = true;
							array2[13].Distance = num;
							array2[13].Position = componentInChildren.transform.position;
							array2[13].CanWarp = false;
							array2[13].TargetObj = this.JukePointIcon[num7].Point.gameObject;
							array2[13].Point = this.JukePointIcon[num7].Point;
							array2[13].kind = 13;
							array2[13].num = num7;
						}
					}
				}
			}
			for (int num8 = 0; num8 < array2.Length; num8++)
			{
				flag |= array2[num8].OnIcon;
			}
			int num9 = 0;
			if (flag)
			{
				for (int num10 = 1; num10 < array2.Length; num10++)
				{
					int num11 = num10;
					if (array2[num11].OnIcon)
					{
						num9 = ((array2[num9].Distance >= array2[num11].Distance) ? num11 : num9);
					}
				}
				this.TargetPos = array2[num9].Position;
				this.OnActionPoint.OnIcon = array2[num9].OnIcon;
				this.OnActionPoint.CanWarp = array2[num9].CanWarp;
				this.OnActionPoint.TargetObj = array2[num9].TargetObj;
				this.OnActionPoint.Position = array2[num9].Position;
				this.OnActionPoint.Distance = array2[num9].Distance;
				this.OnActionPoint.Point = array2[num9].Point;
				this.OnActionPoint.kind = array2[num9].kind;
				this.OnActionPoint.num = array2[num9].num;
				if (!this.OnActionPoint.OnIcon)
				{
					return;
				}
				this.clickedLabelText.text = string.Format("：{0}", array2[num9].Name);
				Sprite sprite = null;
				switch (this.OnActionPoint.kind)
				{
				case 0:
					Singleton<Manager.Resources>.Instance.itemIconTables.ActorIconTable.TryGetValue(Singleton<Manager.Map>.Instance.Player.ID, out sprite);
					break;
				case 1:
					Singleton<Manager.Resources>.Instance.itemIconTables.ActorIconTable.TryGetValue(Singleton<Manager.Map>.Instance.AgentTable[this.OnActionPoint.num].ID, out sprite);
					break;
				case 2:
					sprite = this.MerchantIconCanvas.GetComponentInChildren<Image>().sprite;
					break;
				case 3:
				case 9:
				{
					ActionPoint component2 = this.OnActionPoint.Point.GetComponent<ActionPoint>();
					int key = (component2.IDList.Length <= 0) ? component2.ID : component2.IDList[0];
					int key2;
					Singleton<Manager.Resources>.Instance.itemIconTables.MiniMapIcon.TryGetValue(key, out key2);
					Singleton<Manager.Resources>.Instance.itemIconTables.ActionIconTable.TryGetValue(key2, out sprite);
					break;
				}
				case 4:
					sprite = Singleton<Manager.Resources>.Instance.itemIconTables.CategoryIcon[9].Item2;
					break;
				case 5:
					Singleton<Manager.Resources>.Instance.itemIconTables.ActionIconTable.TryGetValue(Singleton<Manager.Resources>.Instance.DefinePack.MinimapUIDefine.BaseIconID, out sprite);
					break;
				case 6:
					Singleton<Manager.Resources>.Instance.itemIconTables.ActionIconTable.TryGetValue(Singleton<Manager.Resources>.Instance.DefinePack.MinimapUIDefine.DeviceIconID, out sprite);
					break;
				case 7:
				case 10:
				{
					int key3 = Singleton<Manager.Resources>.Instance.DefinePack.MinimapUIDefine.FarmIconID;
					FarmPoint.FarmKind kind = this.OnActionPoint.Point.GetComponent<FarmPoint>().Kind;
					if (kind == FarmPoint.FarmKind.ChickenCoop)
					{
						key3 = this.ChickenIconID;
					}
					Singleton<Manager.Resources>.Instance.itemIconTables.ActionIconTable.TryGetValue(key3, out sprite);
					break;
				}
				case 8:
					Singleton<Manager.Resources>.Instance.itemIconTables.ActionIconTable.TryGetValue(Singleton<Manager.Resources>.Instance.DefinePack.MinimapUIDefine.EventIconID, out sprite);
					break;
				case 11:
				{
					int shipIconID = Singleton<Manager.Resources>.Instance.CommonDefine.Icon.ShipIconID;
					Singleton<Manager.Resources>.Instance.itemIconTables.ActionIconTable.TryGetValue(shipIconID, out sprite);
					break;
				}
				case 12:
				{
					int key4 = this.DragDeskIconID;
					CraftPoint.CraftKind kind2 = this.OnActionPoint.Point.GetComponent<CraftPoint>().Kind;
					if (kind2 == CraftPoint.CraftKind.Pet)
					{
						key4 = this.PetUnionIconID;
					}
					Singleton<Manager.Resources>.Instance.itemIconTables.ActionIconTable.TryGetValue(key4, out sprite);
					break;
				}
				case 13:
				{
					int jukeIconID = this.JukeIconID;
					Singleton<Manager.Resources>.Instance.itemIconTables.ActionIconTable.TryGetValue(jukeIconID, out sprite);
					break;
				}
				}
				this.clickedLabelImage.sprite = sprite;
				this.CursorSet();
			}
			else
			{
				this.OnActionPoint.OnIcon = false;
			}
		}

		// Token: 0x06008598 RID: 34200 RVA: 0x00378584 File Offset: 0x00376984
		private bool CheckSerch(Vector3 CursorPos, Vector3 TargetPos, float minLength, ref float resultLength)
		{
			float num = CursorPos.x - TargetPos.x;
			float num2 = CursorPos.z - TargetPos.z;
			resultLength = num * num + num2 * num2;
			return resultLength < minLength;
		}

		// Token: 0x06008599 RID: 34201 RVA: 0x003785C4 File Offset: 0x003769C4
		public void Restart()
		{
			Vector3 position = base.transform.position;
			this.CameraMoveLimitMin = this._MinCameraMoveLimit[1];
			this.CameraMoveLimitMax = this._MaxCameraMoveLimit[1];
			this.cam.orthographicSize = this.DefaultSize;
			this.camUI.orthographicSize = this.cam.orthographicSize;
			Vector3 vector = Singleton<Manager.Map>.Instance.Player.Position;
			position.x = vector.x;
			position.z = vector.z;
			this.IsLimit[0] = (position.x <= this.CameraMoveLimitMin.x);
			this.IsLimit[1] = (position.x >= this.CameraMoveLimitMax.x);
			this.IsLimit[2] = (position.z <= this.CameraMoveLimitMin.y);
			this.IsLimit[3] = (position.z >= this.CameraMoveLimitMax.y);
			if (this.IsLimit[0])
			{
				position.x = this.CameraMoveLimitMin.x;
			}
			else if (this.IsLimit[1])
			{
				position.x = this.CameraMoveLimitMax.x;
			}
			if (this.IsLimit[2])
			{
				position.z = this.CameraMoveLimitMin.y;
			}
			else if (this.IsLimit[3])
			{
				position.z = this.CameraMoveLimitMax.y;
			}
			base.transform.position = position;
			vector = RectTransformUtility.WorldToScreenPoint(this.camUI, vector);
			vector.z = 0f;
			this.Cursor.transform.position = vector;
			this.GirlIcons = this.MiniMap.GetGirlsIcon();
			this.CheckShowIconsFilter();
		}

		// Token: 0x0600859A RID: 34202 RVA: 0x003787B4 File Offset: 0x00376BB4
		private void CameraZoom()
		{
			this.NowZoomTime += Time.unscaledDeltaTime;
			float num = this.NowZoomTime / this.ZoomTime;
			if (num >= 1f)
			{
				num = 1f;
			}
			if (this.startZoom < this.targetZoom)
			{
				float num2 = Mathf.Lerp(this.startZoom, this.targetZoom, num) - this.cam.orthographicSize;
				num2 = Mathf.Abs(num2);
				this.cam.orthographicSize += num2;
				this.nowZooming = true;
				if (this.cam.orthographicSize <= this.ScreenSize[1])
				{
					float t = Mathf.InverseLerp(this.ScreenSize[0], this.ScreenSize[1], this.cam.orthographicSize);
					this.CameraMoveLimitMin = Vector2.Lerp(this._MinCameraMoveLimit[0], this._MinCameraMoveLimit[1], t);
					this.CameraMoveLimitMax = Vector2.Lerp(this._MaxCameraMoveLimit[0], this._MaxCameraMoveLimit[1], t);
				}
				else
				{
					float t2 = Mathf.InverseLerp(this.ScreenSize[1], this.ScreenSize[2], this.cam.orthographicSize);
					this.CameraMoveLimitMin = Vector2.Lerp(this._MinCameraMoveLimit[1], this._MinCameraMoveLimit[2], t2);
					this.CameraMoveLimitMax = Vector2.Lerp(this._MaxCameraMoveLimit[1], this._MaxCameraMoveLimit[2], t2);
				}
			}
			else if (this.startZoom > this.targetZoom)
			{
				float num3 = Mathf.Lerp(this.startZoom, this.targetZoom, num) - this.cam.orthographicSize;
				num3 = Mathf.Abs(num3);
				this.cam.orthographicSize -= num3;
				this.nowZooming = true;
				if (this.cam.orthographicSize <= this.ScreenSize[1])
				{
					float t3 = Mathf.InverseLerp(this.ScreenSize[0], this.ScreenSize[1], this.cam.orthographicSize);
					this.CameraMoveLimitMin = Vector2.Lerp(this._MinCameraMoveLimit[0], this._MinCameraMoveLimit[1], t3);
					this.CameraMoveLimitMax = Vector2.Lerp(this._MaxCameraMoveLimit[0], this._MaxCameraMoveLimit[1], t3);
				}
				else
				{
					float t4 = Mathf.InverseLerp(this.ScreenSize[1], this.ScreenSize[2], this.cam.orthographicSize);
					this.CameraMoveLimitMin = Vector2.Lerp(this._MinCameraMoveLimit[1], this._MinCameraMoveLimit[2], t4);
					this.CameraMoveLimitMax = Vector2.Lerp(this._MaxCameraMoveLimit[1], this._MaxCameraMoveLimit[2], t4);
				}
			}
			if (num == 1f)
			{
				this.startZoom = this.targetZoom;
				this.nowZooming = false;
				if (this.ZoomPattern == this.DefaultScreenSizeID && this.PreZoomPattern != this.ZoomPattern)
				{
					this.CameraMoveLimitMin = this._MinCameraMoveLimit[1];
					this.CameraMoveLimitMax = this._MaxCameraMoveLimit[1];
				}
				this.PreZoomPattern = this.ZoomPattern;
			}
		}

		// Token: 0x0600859B RID: 34203 RVA: 0x00378B4B File Offset: 0x00376F4B
		public void ChangeTargetPos(Vector3 newPos)
		{
			this.TargetPos = newPos;
			this.CheckNamePanel(this.TargetPos);
		}

		// Token: 0x0600859C RID: 34204 RVA: 0x00378B60 File Offset: 0x00376F60
		public void ChangeactionFilter(MapUIActionCategory category, bool val)
		{
			if (category == MapUIActionCategory.HARBOR)
			{
				if (this.areaMapUI.GameClear)
				{
					this.ActionFilterShip.Value = val;
				}
			}
			else
			{
				this.ActionFilter[category].Value = val;
			}
		}

		// Token: 0x0600859D RID: 34205 RVA: 0x00378B9D File Offset: 0x00376F9D
		public void ChangeActionFilterAllTgl(bool val)
		{
			this.ActionFilter[MapUIActionCategory.ALL].Value = val;
		}

		// Token: 0x0600859E RID: 34206 RVA: 0x00378BB4 File Offset: 0x00376FB4
		private void CheckShowIconsFilter()
		{
			for (int i = 0; i < this.actionPointIcon.Count; i++)
			{
				this.ActionPointIcon[i].enabled = this.ActionFilter[this.actionPointIcon[i].Category].Value;
			}
			for (int j = 0; j < this.actionPointHousingIcon.Count; j++)
			{
				this.ActionPointHousingIcon[j].enabled = this.ActionFilter[this.actionPointHousingIcon[j].Category].Value;
			}
			if (this.GirlIcons != null)
			{
				for (int k = 0; k < this.GirlIcons.Length; k++)
				{
					this.GirlIcons[k].enabled = this.ActionFilter[MapUIActionCategory.HEROIN].Value;
				}
			}
			if (this.MerchantIcon != null && Singleton<Manager.Map>.Instance.Merchant.CurrentMode != Merchant.ActionType.Absent)
			{
				this.MerchantIcon.enabled = this.ActionFilter[MapUIActionCategory.SHOP].Value;
			}
			if (this.PetPointIcon != null && this.PetPointIcon.Count > 0)
			{
				for (int l = 0; l < this.PetPointIcon.Count; l++)
				{
					Image componentInChildren = this.PetPointIcon[l].Icon.GetComponentInChildren<Image>(true);
					if (componentInChildren != null)
					{
						componentInChildren.enabled = this.ActionFilter[MapUIActionCategory.PET].Value;
					}
				}
			}
			if (this.BasePointIcon != null && this.BasePointIcon.Count > 0)
			{
				for (int m = 0; m < this.BasePointIcon.Count; m++)
				{
					Image componentInChildren = this.BasePointIcon[m].Icon.GetComponentInChildren<Image>(true);
					if (componentInChildren != null)
					{
						componentInChildren.enabled = this.ActionFilter[MapUIActionCategory.WARP_POINT].Value;
					}
				}
			}
			if (this.DevicePointIcon != null && this.DevicePointIcon.Count > 0)
			{
				for (int n = 0; n < this.DevicePointIcon.Count; n++)
				{
					Image componentInChildren = this.DevicePointIcon[n].Icon.GetComponentInChildren<Image>(true);
					if (componentInChildren != null)
					{
						componentInChildren.enabled = this.ActionFilter[MapUIActionCategory.DRESSER].Value;
					}
				}
			}
			if (this.FarmPointIcon != null && this.FarmPointIcon.Count > 0)
			{
				bool value = this.ActionFilter[MapUIActionCategory.FARM].Value;
				bool value2 = this.ActionFilter[MapUIActionCategory.CHICKENHOUSE].Value;
				for (int num = 0; num < this.FarmPointIcon.Count; num++)
				{
					FarmPoint.FarmKind kind = this.FarmPointIcon[num].Point.GetComponent<FarmPoint>().Kind;
					if (kind != FarmPoint.FarmKind.Well)
					{
						Image componentInChildren = this.FarmPointIcon[num].Icon.GetComponentInChildren<Image>(true);
						if (componentInChildren != null)
						{
							if (kind == FarmPoint.FarmKind.Plant)
							{
								componentInChildren.enabled = value;
							}
							else if (kind == FarmPoint.FarmKind.ChickenCoop)
							{
								componentInChildren.enabled = value2;
							}
						}
					}
				}
			}
			if (this.FarmPointHousingIcon != null && this.FarmPointHousingIcon.Count > 0)
			{
				bool value3 = this.ActionFilter[MapUIActionCategory.FARM].Value;
				bool value4 = this.ActionFilter[MapUIActionCategory.CHICKENHOUSE].Value;
				for (int num2 = 0; num2 < this.FarmPointHousingIcon.Count; num2++)
				{
					FarmPoint.FarmKind kind2 = this.FarmPointHousingIcon[num2].Point.GetComponent<FarmPoint>().Kind;
					if (kind2 != FarmPoint.FarmKind.Well)
					{
						Image componentInChildren = this.FarmPointHousingIcon[num2].Icon.GetComponentInChildren<Image>(true);
						if (componentInChildren != null)
						{
							if (kind2 == FarmPoint.FarmKind.Plant)
							{
								componentInChildren.enabled = value3;
							}
							else if (kind2 == FarmPoint.FarmKind.ChickenCoop)
							{
								componentInChildren.enabled = value4;
							}
						}
					}
				}
			}
			if (this.ShipPointIcon != null && this.ShipPointIcon.Count > 0)
			{
				for (int num3 = 0; num3 < this.ShipPointIcon.Count; num3++)
				{
					Image componentInChildren = this.ShipPointIcon[num3].Icon.GetComponentInChildren<Image>(true);
					if (componentInChildren != null)
					{
						componentInChildren.enabled = this.ActionFilterShip.Value;
					}
				}
			}
			if (this.CraftPointIcon != null && this.CraftPointIcon.Count > 0)
			{
				bool value5 = this.ActionFilter[MapUIActionCategory.DRAGDESK].Value;
				bool value6 = this.ActionFilter[MapUIActionCategory.PETUNION].Value;
				bool value7 = this.ActionFilter[MapUIActionCategory.RECYCLE].Value;
				for (int num4 = 0; num4 < this.CraftPointIcon.Count; num4++)
				{
					CraftPoint.CraftKind kind3 = this.CraftPointIcon[num4].Point.GetComponent<CraftPoint>().Kind;
					Image componentInChildren = this.CraftPointIcon[num4].Icon.GetComponentInChildren<Image>(true);
					if (componentInChildren != null)
					{
						if (kind3 == CraftPoint.CraftKind.Medicine)
						{
							componentInChildren.enabled = value5;
						}
						else if (kind3 == CraftPoint.CraftKind.Pet)
						{
							componentInChildren.enabled = value6;
						}
						else if (kind3 == CraftPoint.CraftKind.Recycling)
						{
							componentInChildren.enabled = value7;
						}
					}
				}
			}
			if (this.JukePointIcon != null && this.JukePointIcon.Count > 0)
			{
				for (int num5 = 0; num5 < this.JukePointIcon.Count; num5++)
				{
					Image componentInChildren = this.JukePointIcon[num5].Icon.GetComponentInChildren<Image>(true);
					if (componentInChildren != null)
					{
						componentInChildren.enabled = this.ActionFilter[MapUIActionCategory.MUSIC].Value;
					}
				}
			}
		}

		// Token: 0x0600859F RID: 34207 RVA: 0x003791DC File Offset: 0x003775DC
		public void ShowAllIcon()
		{
			for (int i = 0; i < this.actionPointIcon.Count; i++)
			{
				if (this.ActionPointIcon[i] != null)
				{
					this.ActionPointIcon[i].enabled = true;
				}
			}
			for (int j = 0; j < this.actionPointHousingIcon.Count; j++)
			{
				if (this.ActionPointHousingIcon[j] != null)
				{
					this.ActionPointHousingIcon[j].enabled = true;
				}
			}
			this.GirlIcons = this.MiniMap.GetGirlsIcon();
			if (this.GirlIcons != null)
			{
				for (int k = 0; k < this.GirlIcons.Length; k++)
				{
					if (this.GirlIcons[k] != null)
					{
						this.GirlIcons[k].enabled = true;
					}
				}
			}
			if (this.MerchantIcon != null && Singleton<Manager.Map>.Instance.Merchant.CurrentMode != Merchant.ActionType.Absent && this.MerchantIcon != null)
			{
				this.MerchantIcon.enabled = true;
			}
			if (this.PetPointIcon != null)
			{
				for (int l = 0; l < this.PetPointIcon.Count; l++)
				{
					Image componentInChildren = this.PetPointIcon[l].Icon.GetComponentInChildren<Image>(true);
					if (componentInChildren != null)
					{
						componentInChildren.enabled = true;
					}
				}
			}
			if (this.BasePointIcon != null)
			{
				for (int m = 0; m < this.BasePointIcon.Count; m++)
				{
					Image componentInChildren = this.BasePointIcon[m].Icon.GetComponentInChildren<Image>(true);
					if (componentInChildren != null)
					{
						componentInChildren.enabled = true;
					}
				}
			}
			if (this.DevicePointIcon != null)
			{
				for (int n = 0; n < this.DevicePointIcon.Count; n++)
				{
					Image componentInChildren = this.DevicePointIcon[n].Icon.GetComponentInChildren<Image>(true);
					if (componentInChildren != null)
					{
						componentInChildren.enabled = true;
					}
				}
			}
			if (this.FarmPointIcon != null)
			{
				for (int num = 0; num < this.FarmPointIcon.Count; num++)
				{
					Image componentInChildren = this.FarmPointIcon[num].Icon.GetComponentInChildren<Image>(true);
					if (componentInChildren != null)
					{
						componentInChildren.enabled = true;
					}
				}
			}
			if (this.FarmPointHousingIcon != null)
			{
				for (int num2 = 0; num2 < this.FarmPointHousingIcon.Count; num2++)
				{
					Image componentInChildren = this.FarmPointHousingIcon[num2].Icon.GetComponentInChildren<Image>(true);
					if (componentInChildren != null)
					{
						componentInChildren.enabled = true;
					}
				}
			}
			if (this.EventPointIcon != null)
			{
				for (int num3 = 0; num3 < this.EventPointIcon.Count; num3++)
				{
					Image componentInChildren = this.EventPointIcon[num3].Icon.GetComponentInChildren<Image>(true);
					if (componentInChildren != null)
					{
						componentInChildren.enabled = true;
					}
				}
			}
			if (this.ShipPointIcon != null)
			{
				for (int num4 = 0; num4 < this.ShipPointIcon.Count; num4++)
				{
					Image componentInChildren = this.ShipPointIcon[num4].Icon.GetComponentInChildren<Image>(true);
					if (componentInChildren != null)
					{
						componentInChildren.enabled = true;
					}
				}
			}
			if (this.CraftPointIcon != null)
			{
				for (int num5 = 0; num5 < this.CraftPointIcon.Count; num5++)
				{
					Image componentInChildren = this.CraftPointIcon[num5].Icon.GetComponentInChildren<Image>(true);
					if (componentInChildren != null)
					{
						componentInChildren.enabled = true;
					}
				}
			}
			if (this.JukePointIcon != null)
			{
				for (int num6 = 0; num6 < this.JukePointIcon.Count; num6++)
				{
					Image componentInChildren = this.JukePointIcon[num6].Icon.GetComponentInChildren<Image>(true);
					if (componentInChildren != null)
					{
						componentInChildren.enabled = true;
					}
				}
			}
		}

		// Token: 0x060085A0 RID: 34208 RVA: 0x00379620 File Offset: 0x00377A20
		public void RefleshActionHousingIcon()
		{
			for (int i = 0; i < this.ActionPointHousingIcon.Count; i++)
			{
				UnityEngine.Object.Destroy(this.ActionPointHousingIcon[i].gameObject);
			}
			this.ActionPointHousingIcon.Clear();
			this.actionPointHousingIcon = this.MiniMap.GetActionHousingIconList();
			for (int j = 0; j < this.actionPointHousingIcon.Count; j++)
			{
				this.ActionPointHousingIcon.Add(this.actionPointHousingIcon[j].Icon.GetComponentInChildren<Image>());
			}
		}

		// Token: 0x060085A1 RID: 34209 RVA: 0x003796B8 File Offset: 0x00377AB8
		public void RefleshFarmHousingIcon()
		{
			this.FarmPointHousingIcon = this.MiniMap.GetIconList(4);
		}

		// Token: 0x060085A2 RID: 34210 RVA: 0x003796CC File Offset: 0x00377ACC
		public void RefleshCraftIcon()
		{
			this.CraftPointIcon = this.MiniMap.GetIconList(6);
		}

		// Token: 0x060085A3 RID: 34211 RVA: 0x003796E0 File Offset: 0x00377AE0
		public void RefleshJukeIcon()
		{
			this.JukePointIcon = this.MiniMap.GetIconList(7);
		}

		// Token: 0x060085A4 RID: 34212 RVA: 0x003796F4 File Offset: 0x00377AF4
		public void SetPetPointIcon()
		{
			this.PetPointIcon = this.MiniMap.GetPetIconList();
		}

		// Token: 0x060085A5 RID: 34213 RVA: 0x00379708 File Offset: 0x00377B08
		public void AllIconReflesh()
		{
			this.actionPointIcon = this.MiniMap.GetActionIconList();
			this.actionPointHousingIcon = this.MiniMap.GetActionHousingIconList();
			this.PetPointIcon = this.MiniMap.GetPetIconList();
			this.BasePointIcon = this.MiniMap.GetIconList(0);
			this.DevicePointIcon = this.MiniMap.GetIconList(1);
			this.FarmPointIcon = this.MiniMap.GetIconList(2);
			this.FarmPointHousingIcon = this.MiniMap.GetIconList(4);
			this.EventPointIcon = this.MiniMap.GetIconList(3);
			this.ShipPointIcon = this.MiniMap.GetIconList(5);
			this.CraftPointIcon = this.MiniMap.GetIconList(6);
			this.JukePointIcon = this.MiniMap.GetIconList(7);
			if (this.ActionPointIcon != null)
			{
				foreach (Image image in this.ActionPointIcon)
				{
					if (!(image.gameObject == null))
					{
						UnityEngine.Object.Destroy(image.gameObject);
					}
				}
				this.ActionPointIcon.Clear();
			}
			if (this.ActionPointHousingIcon != null)
			{
				foreach (Image image2 in this.ActionPointHousingIcon)
				{
					if (!(image2.gameObject == null))
					{
						UnityEngine.Object.Destroy(image2.gameObject);
					}
				}
				this.ActionPointHousingIcon.Clear();
			}
		}

		// Token: 0x060085A6 RID: 34214 RVA: 0x003798D4 File Offset: 0x00377CD4
		public void SetCameraCommandBuffer()
		{
			if (this.SetCamCommmandBuf)
			{
				return;
			}
			if (this.depthTexture != null)
			{
				this.depthTexture.Initialize();
			}
			if (this.cameraEffector != null)
			{
				this.cameraEffector.Initialize();
			}
			this.SetCamCommmandBuf = true;
		}

		// Token: 0x04006C0C RID: 27660
		private Image Cursor;

		// Token: 0x04006C0D RID: 27661
		private GameObject PutGirlName;

		// Token: 0x04006C0E RID: 27662
		private Image clickedLabelImage;

		// Token: 0x04006C0F RID: 27663
		private Text clickedLabelText;

		// Token: 0x04006C10 RID: 27664
		private AllAreaMapUI areaMapUI;

		// Token: 0x04006C11 RID: 27665
		private WarpListUI warpListUI;

		// Token: 0x04006C12 RID: 27666
		[SerializeField]
		private Camera cam;

		// Token: 0x04006C13 RID: 27667
		[SerializeField]
		private Camera camUI;

		// Token: 0x04006C14 RID: 27668
		[SerializeField]
		private MiniMapControler MiniMap;

		// Token: 0x04006C15 RID: 27669
		[SerializeField]
		private float[] ScreenSize;

		// Token: 0x04006C16 RID: 27670
		[SerializeField]
		private int DefaultScreenSizeID = 1;

		// Token: 0x04006C17 RID: 27671
		[Space]
		[SerializeField]
		private Vector2[] _MinCameraMoveLimit;

		// Token: 0x04006C18 RID: 27672
		[SerializeField]
		private Vector2[] _MaxCameraMoveLimit;

		// Token: 0x04006C19 RID: 27673
		private Vector2 CameraMoveLimitMin;

		// Token: 0x04006C1A RID: 27674
		private Vector2 CameraMoveLimitMax;

		// Token: 0x04006C1B RID: 27675
		[SerializeField]
		private float cursorMoveSpeed;

		// Token: 0x04006C1C RID: 27676
		[SerializeField]
		private float dragMoveSpeed = 1f;

		// Token: 0x04006C1D RID: 27677
		[Space]
		[SerializeField]
		private float iconSeachRange;

		// Token: 0x04006C1E RID: 27678
		[SerializeField]
		private float ZoomTime;

		// Token: 0x04006C1F RID: 27679
		[SerializeField]
		private float WheelZoomSpeed = 50f;

		// Token: 0x04006C20 RID: 27680
		[Space]
		[SerializeField]
		private float MarkerAppearTime;

		// Token: 0x04006C21 RID: 27681
		[SerializeField]
		private float MaxMarkerWaitTime;

		// Token: 0x04006C22 RID: 27682
		[Space]
		[SerializeField]
		private float MarkerChangeTime;

		// Token: 0x04006C23 RID: 27683
		[SerializeField]
		private float MarkerMinScl = 1f;

		// Token: 0x04006C24 RID: 27684
		[SerializeField]
		private float MarkerMaxScl = 1.2f;

		// Token: 0x04006C25 RID: 27685
		private float sclTime;

		// Token: 0x04006C26 RID: 27686
		private float MarkerChangeWaitTime;

		// Token: 0x04006C27 RID: 27687
		private bool MarkerChangeWait;

		// Token: 0x04006C28 RID: 27688
		private int ChangeMarkerMode;

		// Token: 0x04006C29 RID: 27689
		private Image[] GirlIcons;

		// Token: 0x04006C2A RID: 27690
		private Image MerchantIcon;

		// Token: 0x04006C2B RID: 27691
		private Canvas MerchantIconCanvas;

		// Token: 0x04006C2C RID: 27692
		private Manager.Input Input;

		// Token: 0x04006C2D RID: 27693
		private Vector3 cursolWorldPos;

		// Token: 0x04006C2E RID: 27694
		private float DefaultSize;

		// Token: 0x04006C2F RID: 27695
		private Vector2 CursorDefaultSize;

		// Token: 0x04006C30 RID: 27696
		private Vector3 TargetPos;

		// Token: 0x04006C31 RID: 27697
		private int PreZoomPattern = 1;

		// Token: 0x04006C32 RID: 27698
		private int ZoomPattern = 1;

		// Token: 0x04006C33 RID: 27699
		private float startZoom;

		// Token: 0x04006C34 RID: 27700
		private float targetZoom;

		// Token: 0x04006C35 RID: 27701
		private float NowZoomTime;

		// Token: 0x04006C36 RID: 27702
		private bool nowZooming;

		// Token: 0x04006C37 RID: 27703
		private bool SetCamCommmandBuf;

		// Token: 0x04006C38 RID: 27704
		[SerializeField]
		private MiniMapDepthTexture depthTexture;

		// Token: 0x04006C39 RID: 27705
		[SerializeField]
		private AllAreaCameraEffector cameraEffector;

		// Token: 0x04006C3A RID: 27706
		private Dictionary<MapUIActionCategory, BoolReactiveProperty> ActionFilter = new Dictionary<MapUIActionCategory, BoolReactiveProperty>();

		// Token: 0x04006C3B RID: 27707
		private BoolReactiveProperty ActionFilterShip = new BoolReactiveProperty(true);

		// Token: 0x04006C3C RID: 27708
		private List<MiniMapControler.PointIconInfo> actionPointIcon = new List<MiniMapControler.PointIconInfo>();

		// Token: 0x04006C3D RID: 27709
		private List<MiniMapControler.PointIconInfo> actionPointHousingIcon = new List<MiniMapControler.PointIconInfo>();

		// Token: 0x04006C3E RID: 27710
		private List<Image> ActionPointIcon = new List<Image>();

		// Token: 0x04006C3F RID: 27711
		private List<Image> ActionPointHousingIcon = new List<Image>();

		// Token: 0x04006C40 RID: 27712
		private List<MiniMapControler.PetIconInfo> PetPointIcon = new List<MiniMapControler.PetIconInfo>();

		// Token: 0x04006C41 RID: 27713
		private List<MiniMapControler.IconInfo> BasePointIcon = new List<MiniMapControler.IconInfo>();

		// Token: 0x04006C42 RID: 27714
		private List<MiniMapControler.IconInfo> DevicePointIcon = new List<MiniMapControler.IconInfo>();

		// Token: 0x04006C43 RID: 27715
		private List<MiniMapControler.IconInfo> FarmPointIcon = new List<MiniMapControler.IconInfo>();

		// Token: 0x04006C44 RID: 27716
		private List<MiniMapControler.IconInfo> FarmPointHousingIcon = new List<MiniMapControler.IconInfo>();

		// Token: 0x04006C45 RID: 27717
		private List<MiniMapControler.IconInfo> EventPointIcon = new List<MiniMapControler.IconInfo>();

		// Token: 0x04006C46 RID: 27718
		private List<MiniMapControler.IconInfo> ShipPointIcon = new List<MiniMapControler.IconInfo>();

		// Token: 0x04006C47 RID: 27719
		private List<MiniMapControler.IconInfo> CraftPointIcon = new List<MiniMapControler.IconInfo>();

		// Token: 0x04006C48 RID: 27720
		private List<MiniMapControler.IconInfo> JukePointIcon = new List<MiniMapControler.IconInfo>();

		// Token: 0x04006C49 RID: 27721
		private bool[] IsLimit = new bool[4];

		// Token: 0x04006C4A RID: 27722
		private AllAreaCameraControler.CalcOnIconInfo OnActionPoint;

		// Token: 0x04006C4B RID: 27723
		public static bool NowWarp;

		// Token: 0x04006C4C RID: 27724
		private const int nCameraPosYoffset = 100;

		// Token: 0x04006C4D RID: 27725
		[SerializeField]
		private int ChickenIconID = 35;

		// Token: 0x04006C4E RID: 27726
		[SerializeField]
		private int DragDeskIconID = 29;

		// Token: 0x04006C4F RID: 27727
		[SerializeField]
		private int PetUnionIconID = 30;

		// Token: 0x04006C50 RID: 27728
		[SerializeField]
		private int JukeIconID = 33;

		// Token: 0x04006C51 RID: 27729
		public IDisposable WarpSelectSubscriber;

		// Token: 0x02000FAD RID: 4013
		public struct CalcOnIconInfo
		{
			// Token: 0x04006C53 RID: 27731
			public string Name;

			// Token: 0x04006C54 RID: 27732
			public bool OnIcon;

			// Token: 0x04006C55 RID: 27733
			public float Distance;

			// Token: 0x04006C56 RID: 27734
			public Vector3 Position;

			// Token: 0x04006C57 RID: 27735
			public bool CanWarp;

			// Token: 0x04006C58 RID: 27736
			public GameObject TargetObj;

			// Token: 0x04006C59 RID: 27737
			public Point Point;

			// Token: 0x04006C5A RID: 27738
			public MiniMapControler.IconInfo info;

			// Token: 0x04006C5B RID: 27739
			public int kind;

			// Token: 0x04006C5C RID: 27740
			public int num;
		}
	}
}
