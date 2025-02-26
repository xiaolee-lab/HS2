using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Studio
{
	// Token: 0x02001248 RID: 4680
	public class GuideInput : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
	{
		// Token: 0x170020E9 RID: 8425
		// (set) Token: 0x06009A26 RID: 39462 RVA: 0x003F5306 File Offset: 0x003F3706
		public GuideObject guideObject
		{
			set
			{
				if (this.SetGuideObject(value))
				{
					this.UpdateUI();
				}
			}
		}

		// Token: 0x170020EA RID: 8426
		// (set) Token: 0x06009A27 RID: 39463 RVA: 0x003F531A File Offset: 0x003F371A
		public GuideObject deselectObject
		{
			set
			{
				if (this.DeselectGuideObject(value))
				{
					this.UpdateUI();
				}
			}
		}

		// Token: 0x170020EB RID: 8427
		// (get) Token: 0x06009A28 RID: 39464 RVA: 0x003F532E File Offset: 0x003F372E
		// (set) Token: 0x06009A29 RID: 39465 RVA: 0x003F533B File Offset: 0x003F373B
		public bool outsideVisible
		{
			get
			{
				return this._outsideVisible.Value;
			}
			set
			{
				this._outsideVisible.Value = value;
			}
		}

		// Token: 0x170020EC RID: 8428
		// (get) Token: 0x06009A2A RID: 39466 RVA: 0x003F5349 File Offset: 0x003F3749
		// (set) Token: 0x06009A2B RID: 39467 RVA: 0x003F5356 File Offset: 0x003F3756
		public bool visible
		{
			get
			{
				return this._visible.Value;
			}
			set
			{
				this._visible.Value = value;
			}
		}

		// Token: 0x170020ED RID: 8429
		// (get) Token: 0x06009A2C RID: 39468 RVA: 0x003F5364 File Offset: 0x003F3764
		// (set) Token: 0x06009A2D RID: 39469 RVA: 0x003F536C File Offset: 0x003F376C
		public int selectIndex
		{
			get
			{
				return this._selectIndex;
			}
			set
			{
				this._selectIndex = ((this._selectIndex != value) ? value : -1);
			}
		}

		// Token: 0x170020EE RID: 8430
		// (get) Token: 0x06009A2E RID: 39470 RVA: 0x003F5387 File Offset: 0x003F3787
		// (set) Token: 0x06009A2F RID: 39471 RVA: 0x003F538F File Offset: 0x003F378F
		private TMP_InputField[] arrayInput { get; set; }

		// Token: 0x06009A30 RID: 39472 RVA: 0x003F5398 File Offset: 0x003F3798
		public void Stop()
		{
			this.hashSelectObject.Clear();
			this.visible = false;
		}

		// Token: 0x06009A31 RID: 39473 RVA: 0x003F53AC File Offset: 0x003F37AC
		public void UpdateUI()
		{
			if (this.hashSelectObject.Count != 0)
			{
				this.SetInputText();
			}
			else
			{
				this.visible = false;
			}
		}

		// Token: 0x06009A32 RID: 39474 RVA: 0x003F53D0 File Offset: 0x003F37D0
		public void OnEndEditPos(int _target)
		{
			if (this.hashSelectObject.Count == 0)
			{
				return;
			}
			float num = this.InputToFloat(this.inputPos[_target]);
			List<GuideCommand.EqualsInfo> list = new List<GuideCommand.EqualsInfo>();
			foreach (GuideObject guideObject in this.hashSelectObject)
			{
				if (guideObject.enablePos)
				{
					Vector3 pos = guideObject.changeAmount.pos;
					if (pos[_target] != num)
					{
						pos[_target] = num;
						Vector3 pos2 = guideObject.changeAmount.pos;
						guideObject.changeAmount.pos = pos;
						list.Add(new GuideCommand.EqualsInfo
						{
							dicKey = guideObject.dicKey,
							oldValue = pos2,
							newValue = pos
						});
					}
				}
			}
			if (!list.IsNullOrEmpty<GuideCommand.EqualsInfo>())
			{
				Singleton<UndoRedoManager>.Instance.Push(new GuideCommand.MoveEqualsCommand(list.ToArray()));
			}
			this.SetInputTextPos();
		}

		// Token: 0x06009A33 RID: 39475 RVA: 0x003F54F4 File Offset: 0x003F38F4
		public void OnEndEditRot(int _target)
		{
			if (this.hashSelectObject.Count == 0)
			{
				return;
			}
			float num = this.InputToFloat(this.inputRot[_target]) % 360f;
			List<GuideCommand.EqualsInfo> list = new List<GuideCommand.EqualsInfo>();
			foreach (GuideObject guideObject in this.hashSelectObject)
			{
				if (guideObject.enableRot)
				{
					Vector3 rot = guideObject.changeAmount.rot;
					if (rot[_target] != num)
					{
						rot[_target] = num;
						Vector3 rot2 = guideObject.changeAmount.rot;
						guideObject.changeAmount.rot = rot;
						list.Add(new GuideCommand.EqualsInfo
						{
							dicKey = guideObject.dicKey,
							oldValue = rot2,
							newValue = rot
						});
					}
				}
			}
			if (!list.IsNullOrEmpty<GuideCommand.EqualsInfo>())
			{
				Singleton<UndoRedoManager>.Instance.Push(new GuideCommand.RotationEqualsCommand(list.ToArray()));
			}
			this.SetInputTextRot();
		}

		// Token: 0x06009A34 RID: 39476 RVA: 0x003F561C File Offset: 0x003F3A1C
		public void OnEndEditScale(int _target)
		{
			if (this.hashSelectObject.Count == 0)
			{
				return;
			}
			float num = Mathf.Max(this.InputToFloat(this.inputScale[_target]), 0.01f);
			List<GuideCommand.EqualsInfo> list = new List<GuideCommand.EqualsInfo>();
			foreach (GuideObject guideObject in this.hashSelectObject)
			{
				if (guideObject.enableScale)
				{
					Vector3 scale = guideObject.changeAmount.scale;
					if (scale[_target] != num)
					{
						scale[_target] = num;
						Vector3 scale2 = guideObject.changeAmount.scale;
						guideObject.changeAmount.scale = scale;
						list.Add(new GuideCommand.EqualsInfo
						{
							dicKey = guideObject.dicKey,
							oldValue = scale2,
							newValue = scale
						});
					}
				}
			}
			if (!list.IsNullOrEmpty<GuideCommand.EqualsInfo>())
			{
				Singleton<UndoRedoManager>.Instance.Push(new GuideCommand.ScaleEqualsCommand(list.ToArray()));
			}
			this.SetInputTextScale(Vector3.zero);
		}

		// Token: 0x06009A35 RID: 39477 RVA: 0x003F5750 File Offset: 0x003F3B50
		public void SetInputText()
		{
			this.visible = true;
			bool flag = this.hashSelectObject.Any((GuideObject v) => !v.enablePos);
			bool flag2 = this.hashSelectObject.Any((GuideObject v) => !v.enableRot);
			bool flag3 = this.hashSelectObject.Any((GuideObject v) => !v.enableScale);
			this.SetInputTextPos();
			for (int i = 0; i < 3; i++)
			{
				this.inputPos[i].interactable = !flag;
			}
			this.SetInputTextRot();
			for (int j = 0; j < 3; j++)
			{
				this.inputRot[j].interactable = !flag2;
			}
			this.SetInputTextScale(Vector3.zero);
			for (int k = 0; k < 3; k++)
			{
				this.inputScale[k].interactable = !flag3;
			}
		}

		// Token: 0x06009A36 RID: 39478 RVA: 0x003F5869 File Offset: 0x003F3C69
		public void AddSelectMultiple(GuideObject _object)
		{
			if (this.hashSelectObject.Contains(_object))
			{
				return;
			}
			this.AddObject(_object);
			this.SetInputText();
		}

		// Token: 0x06009A37 RID: 39479 RVA: 0x003F588C File Offset: 0x003F3C8C
		private bool SetGuideObject(GuideObject _object)
		{
			bool flag = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);
			bool key = Input.GetKey(KeyCode.X);
			if (flag && !key)
			{
				if (this.hashSelectObject.Contains(_object))
				{
					return false;
				}
				this.AddObject(_object);
			}
			else
			{
				foreach (GuideObject guideObject in this.hashSelectObject)
				{
					ChangeAmount changeAmount = guideObject.changeAmount;
					ChangeAmount changeAmount2 = changeAmount;
					changeAmount2.onChangePos = (Action)Delegate.Remove(changeAmount2.onChangePos, new Action(this.SetInputTextPos));
					ChangeAmount changeAmount3 = changeAmount;
					changeAmount3.onChangeRot = (Action)Delegate.Remove(changeAmount3.onChangeRot, new Action(this.SetInputTextRot));
					ChangeAmount changeAmount4 = changeAmount;
					changeAmount4.onChangeScale = (Action<Vector3>)Delegate.Remove(changeAmount4.onChangeScale, new Action<Vector3>(this.SetInputTextScale));
				}
				this.hashSelectObject.Clear();
				this.AddObject(_object);
			}
			return true;
		}

		// Token: 0x06009A38 RID: 39480 RVA: 0x003F59B8 File Offset: 0x003F3DB8
		private bool DeselectGuideObject(GuideObject _object)
		{
			if (_object == null)
			{
				return false;
			}
			if (!this.hashSelectObject.Contains(_object))
			{
				return false;
			}
			ChangeAmount changeAmount = _object.changeAmount;
			ChangeAmount changeAmount2 = changeAmount;
			changeAmount2.onChangePos = (Action)Delegate.Remove(changeAmount2.onChangePos, new Action(this.SetInputTextPos));
			ChangeAmount changeAmount3 = changeAmount;
			changeAmount3.onChangeRot = (Action)Delegate.Remove(changeAmount3.onChangeRot, new Action(this.SetInputTextRot));
			ChangeAmount changeAmount4 = changeAmount;
			changeAmount4.onChangeScale = (Action<Vector3>)Delegate.Remove(changeAmount4.onChangeScale, new Action<Vector3>(this.SetInputTextScale));
			this.hashSelectObject.Remove(_object);
			return true;
		}

		// Token: 0x06009A39 RID: 39481 RVA: 0x003F5A64 File Offset: 0x003F3E64
		private void AddObject(GuideObject _object)
		{
			if (_object == null)
			{
				return;
			}
			ChangeAmount changeAmount = _object.changeAmount;
			ChangeAmount changeAmount2 = changeAmount;
			changeAmount2.onChangePos = (Action)Delegate.Combine(changeAmount2.onChangePos, new Action(this.SetInputTextPos));
			ChangeAmount changeAmount3 = changeAmount;
			changeAmount3.onChangeRot = (Action)Delegate.Combine(changeAmount3.onChangeRot, new Action(this.SetInputTextRot));
			ChangeAmount changeAmount4 = changeAmount;
			changeAmount4.onChangeScale = (Action<Vector3>)Delegate.Combine(changeAmount4.onChangeScale, new Action<Vector3>(this.SetInputTextScale));
			this.hashSelectObject.Add(_object);
		}

		// Token: 0x06009A3A RID: 39482 RVA: 0x003F5AF8 File Offset: 0x003F3EF8
		private void SetInputTextPos()
		{
			GuideInput.<SetInputTextPos>c__AnonStorey1 <SetInputTextPos>c__AnonStorey = new GuideInput.<SetInputTextPos>c__AnonStorey1();
			GuideObject guideObject = this.hashSelectObject.ElementAtOrDefault(0);
			<SetInputTextPos>c__AnonStorey.baseValue = ((!(guideObject != null)) ? Vector3.zero : guideObject.changeAmount.pos);
			IEnumerable<Vector3> source = from v in this.hashSelectObject
			select v.changeAmount.pos;
			int i;
			for (i = 0; i < 3; i++)
			{
				this.inputPos[i].text = ((!source.All((Vector3 _v) => Mathf.Approximately(_v[i], <SetInputTextPos>c__AnonStorey.baseValue[i]))) ? "-" : <SetInputTextPos>c__AnonStorey.baseValue[i].ToString("0.#####"));
			}
		}

		// Token: 0x06009A3B RID: 39483 RVA: 0x003F5BF0 File Offset: 0x003F3FF0
		private void SetInputTextRot()
		{
			GuideInput.<SetInputTextRot>c__AnonStorey3 <SetInputTextRot>c__AnonStorey = new GuideInput.<SetInputTextRot>c__AnonStorey3();
			GuideObject guideObject = this.hashSelectObject.ElementAtOrDefault(0);
			<SetInputTextRot>c__AnonStorey.baseValue = ((!(guideObject != null)) ? Vector3.zero : guideObject.changeAmount.rot);
			IEnumerable<Vector3> source = from v in this.hashSelectObject
			select v.changeAmount.rot;
			int i;
			for (i = 0; i < 3; i++)
			{
				this.inputRot[i].text = ((!source.All((Vector3 _v) => Mathf.Approximately(_v[i], <SetInputTextRot>c__AnonStorey.baseValue[i]))) ? "-" : <SetInputTextRot>c__AnonStorey.baseValue[i].ToString("0.#####"));
			}
		}

		// Token: 0x06009A3C RID: 39484 RVA: 0x003F5CE8 File Offset: 0x003F40E8
		private void SetInputTextScale(Vector3 _value)
		{
			GuideInput.<SetInputTextScale>c__AnonStorey5 <SetInputTextScale>c__AnonStorey = new GuideInput.<SetInputTextScale>c__AnonStorey5();
			GuideObject guideObject = this.hashSelectObject.ElementAtOrDefault(0);
			<SetInputTextScale>c__AnonStorey.baseValue = ((!(guideObject != null)) ? Vector3.zero : guideObject.changeAmount.scale);
			IEnumerable<Vector3> source = from v in this.hashSelectObject
			select v.changeAmount.scale;
			int i;
			for (i = 0; i < 3; i++)
			{
				this.inputScale[i].text = ((!source.All((Vector3 _v) => Mathf.Approximately(_v[i], <SetInputTextScale>c__AnonStorey.baseValue[i]))) ? "-" : <SetInputTextScale>c__AnonStorey.baseValue[i].ToString("0.#####"));
			}
		}

		// Token: 0x06009A3D RID: 39485 RVA: 0x003F5DE0 File Offset: 0x003F41E0
		private float InputToFloat(TMP_InputField _input)
		{
			float num = 0f;
			return (!float.TryParse(_input.text, out num)) ? 0f : num;
		}

		// Token: 0x06009A3E RID: 39486 RVA: 0x003F5E10 File Offset: 0x003F4210
		private bool Vector3Equals(ref Vector3 _a, ref Vector3 _b)
		{
			return _a.x == _b.x && _a.y == _b.y && _a.z == _b.z;
		}

		// Token: 0x06009A3F RID: 39487 RVA: 0x003F5E48 File Offset: 0x003F4248
		private void SetVisible()
		{
			bool flag = this.outsideVisible & this.visible;
			if (this.canvasParent)
			{
				this.canvasParent.enabled = flag;
			}
			else
			{
				base.gameObject.SetActive(flag);
			}
		}

		// Token: 0x06009A40 RID: 39488 RVA: 0x003F5E90 File Offset: 0x003F4290
		public void OnPointerDown(PointerEventData eventData)
		{
			SortCanvas.select = this.canvasParent;
		}

		// Token: 0x06009A41 RID: 39489 RVA: 0x003F5EA0 File Offset: 0x003F42A0
		private void ChangeTarget()
		{
			bool flag = Input.GetKey(KeyCode.LeftShift) | Input.GetKey(KeyCode.RightShift) | Input.GetKeyDown(KeyCode.LeftShift) | Input.GetKeyDown(KeyCode.RightShift);
			if (flag)
			{
				this.selectIndex--;
				if (this.selectIndex < 0)
				{
					this.selectIndex = this.arrayInput.Length - 1;
				}
			}
			else
			{
				this.selectIndex = (this.selectIndex + 1) % this.arrayInput.Length;
			}
			if (this.arrayInput[this.selectIndex])
			{
				this.arrayInput[this.selectIndex].Select();
			}
		}

		// Token: 0x06009A42 RID: 39490 RVA: 0x003F5F50 File Offset: 0x003F4350
		private void OnClickInitPos()
		{
			if (this.hashSelectObject.Count == 0)
			{
				return;
			}
			GuideCommand.EqualsInfo[] array = (from v in this.hashSelectObject
			where v.enablePos
			select new GuideCommand.EqualsInfo
			{
				dicKey = v.dicKey,
				oldValue = v.changeAmount.pos,
				newValue = Vector3.zero
			}).ToArray<GuideCommand.EqualsInfo>();
			if (!array.IsNullOrEmpty<GuideCommand.EqualsInfo>())
			{
				Singleton<UndoRedoManager>.Instance.Do(new GuideCommand.MoveEqualsCommand(array));
			}
			this.SetInputTextPos();
		}

		// Token: 0x06009A43 RID: 39491 RVA: 0x003F5FE0 File Offset: 0x003F43E0
		private void OnClickInitRot()
		{
			if (this.hashSelectObject.Count == 0)
			{
				return;
			}
			GuideCommand.EqualsInfo[] array = (from v in this.hashSelectObject
			where v.enableRot
			select new GuideCommand.EqualsInfo
			{
				dicKey = v.dicKey,
				oldValue = v.changeAmount.rot,
				newValue = v.changeAmount.defRot
			}).ToArray<GuideCommand.EqualsInfo>();
			if (!array.IsNullOrEmpty<GuideCommand.EqualsInfo>())
			{
				Singleton<UndoRedoManager>.Instance.Do(new GuideCommand.RotationEqualsCommand(array));
			}
			this.SetInputTextRot();
		}

		// Token: 0x06009A44 RID: 39492 RVA: 0x003F6070 File Offset: 0x003F4470
		private void OnClickInitScale()
		{
			if (this.hashSelectObject.Count == 0)
			{
				return;
			}
			GuideCommand.EqualsInfo[] array = (from v in this.hashSelectObject
			where v.enableScale
			select new GuideCommand.EqualsInfo
			{
				dicKey = v.dicKey,
				oldValue = v.changeAmount.scale,
				newValue = Vector3.one
			}).ToArray<GuideCommand.EqualsInfo>();
			if (!array.IsNullOrEmpty<GuideCommand.EqualsInfo>())
			{
				Singleton<UndoRedoManager>.Instance.Do(new GuideCommand.ScaleEqualsCommand(array));
			}
			this.SetInputTextScale(Vector3.zero);
		}

		// Token: 0x06009A45 RID: 39493 RVA: 0x003F6104 File Offset: 0x003F4504
		private void Awake()
		{
			this._outsideVisible.Subscribe(delegate(bool _)
			{
				this.SetVisible();
			});
			this._visible.Subscribe(delegate(bool _b)
			{
				this.SetVisible();
				if (this.onVisible != null)
				{
					this.onVisible(_b);
				}
			});
			this.buttonInit[0].onClick.AddListener(new UnityAction(this.OnClickInitPos));
			this.buttonInit[1].onClick.AddListener(new UnityAction(this.OnClickInitRot));
			this.buttonInit[2].onClick.AddListener(new UnityAction(this.OnClickInitScale));
			this.visible = false;
			List<TMP_InputField> list = new List<TMP_InputField>();
			list.AddRange(this.inputPos);
			list.AddRange(this.inputRot);
			list.AddRange(this.inputScale);
			this.arrayInput = list.ToArray();
			this.selectIndex = -1;
		}

		// Token: 0x06009A46 RID: 39494 RVA: 0x003F61E0 File Offset: 0x003F45E0
		private void Start()
		{
			(from _ in this.UpdateAsObservable()
			where this.selectIndex != -1
			where Input.GetKeyDown(KeyCode.Tab)
			select _).Subscribe(delegate(Unit _)
			{
				this.ChangeTarget();
			});
		}

		// Token: 0x04007B0E RID: 31502
		[SerializeField]
		protected TMP_InputField[] inputPos;

		// Token: 0x04007B0F RID: 31503
		[SerializeField]
		protected TMP_InputField[] inputRot;

		// Token: 0x04007B10 RID: 31504
		[SerializeField]
		protected TMP_InputField[] inputScale;

		// Token: 0x04007B11 RID: 31505
		[SerializeField]
		private Button[] buttonInit;

		// Token: 0x04007B12 RID: 31506
		[Space]
		[SerializeField]
		private Canvas canvasParent;

		// Token: 0x04007B13 RID: 31507
		private HashSet<GuideObject> hashSelectObject = new HashSet<GuideObject>();

		// Token: 0x04007B14 RID: 31508
		private BoolReactiveProperty _outsideVisible = new BoolReactiveProperty(true);

		// Token: 0x04007B15 RID: 31509
		private BoolReactiveProperty _visible = new BoolReactiveProperty(true);

		// Token: 0x04007B16 RID: 31510
		public GuideInput.OnVisible onVisible;

		// Token: 0x04007B17 RID: 31511
		private int _selectIndex = -1;

		// Token: 0x02001249 RID: 4681
		// (Invoke) Token: 0x06009A59 RID: 39513
		public delegate void OnVisible(bool _value);
	}
}
