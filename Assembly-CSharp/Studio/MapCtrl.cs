using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Studio
{
	// Token: 0x0200132B RID: 4907
	public class MapCtrl : Singleton<MapCtrl>
	{
		// Token: 0x17002273 RID: 8819
		// (set) Token: 0x0600A421 RID: 42017 RVA: 0x00430655 File Offset: 0x0042EA55
		public bool active
		{
			set
			{
				base.gameObject.SetActive(value);
				if (value)
				{
					this.UpdateUI();
				}
			}
		}

		// Token: 0x0600A422 RID: 42018 RVA: 0x00430670 File Offset: 0x0042EA70
		public void UpdateUI()
		{
			this.isUpdate = true;
			this.SetInputTextPos();
			this.SetInputTextRot();
			if (Singleton<Map>.Instance.IsOption)
			{
				this.toggleOption.interactable = true;
				this.toggleOption.isOn = Singleton<Studio>.Instance.sceneInfo.mapInfo.option;
			}
			else
			{
				this.toggleOption.interactable = false;
				this.toggleOption.isOn = false;
			}
			this.isUpdate = false;
		}

		// Token: 0x0600A423 RID: 42019 RVA: 0x004306F0 File Offset: 0x0042EAF0
		public void Reflect()
		{
			GameObject mapRoot = Singleton<Map>.Instance.MapRoot;
			if (mapRoot != null)
			{
				Transform transform = mapRoot.transform;
				transform.localPosition = Singleton<Studio>.Instance.sceneInfo.mapInfo.ca.pos;
				transform.localRotation = Quaternion.Euler(Singleton<Studio>.Instance.sceneInfo.mapInfo.ca.rot);
			}
			Singleton<Map>.Instance.VisibleOption = Singleton<Studio>.Instance.sceneInfo.mapInfo.option;
			this.UpdateUI();
		}

		// Token: 0x0600A424 RID: 42020 RVA: 0x00430784 File Offset: 0x0042EB84
		public void OnEndEditPos(int _target)
		{
			float num = this.InputToFloat(this.inputPos[_target]);
			Vector3 pos = Singleton<Studio>.Instance.sceneInfo.mapInfo.ca.pos;
			if (pos[_target] != num)
			{
				Vector3 vector = pos;
				pos[_target] = num;
				Singleton<Studio>.Instance.sceneInfo.mapInfo.ca.pos = pos;
				Singleton<UndoRedoManager>.Instance.Push(new MapCommand.MoveEqualsCommand(new MapCommand.EqualsInfo
				{
					newValue = pos,
					oldValue = vector
				}));
			}
			this.SetInputTextPos();
		}

		// Token: 0x0600A425 RID: 42021 RVA: 0x00430818 File Offset: 0x0042EC18
		public void OnEndEditRot(int _target)
		{
			float num = this.InputToFloat(this.inputRot[_target]) % 360f;
			Vector3 rot = Singleton<Studio>.Instance.sceneInfo.mapInfo.ca.rot;
			if (rot[_target] != num)
			{
				Vector3 vector = rot;
				rot[_target] = num;
				Singleton<Studio>.Instance.sceneInfo.mapInfo.ca.rot = rot;
				Singleton<UndoRedoManager>.Instance.Push(new MapCommand.RotationEqualsCommand(new MapCommand.EqualsInfo
				{
					newValue = rot,
					oldValue = vector
				}));
			}
			this.SetInputTextRot();
		}

		// Token: 0x0600A426 RID: 42022 RVA: 0x004308B4 File Offset: 0x0042ECB4
		private float InputToFloat(TMP_InputField _input)
		{
			float num = 0f;
			return (!float.TryParse(_input.text, out num)) ? 0f : num;
		}

		// Token: 0x0600A427 RID: 42023 RVA: 0x004308E4 File Offset: 0x0042ECE4
		private void SetInputTextPos()
		{
			Vector3 pos = Singleton<Studio>.Instance.sceneInfo.mapInfo.ca.pos;
			for (int i = 0; i < 3; i++)
			{
				this.inputPos[i].text = pos[i].ToString("0.000");
			}
		}

		// Token: 0x0600A428 RID: 42024 RVA: 0x00430940 File Offset: 0x0042ED40
		private void SetInputTextRot()
		{
			Vector3 rot = Singleton<Studio>.Instance.sceneInfo.mapInfo.ca.rot;
			for (int i = 0; i < 3; i++)
			{
				this.inputRot[i].text = rot[i].ToString("0.000");
			}
		}

		// Token: 0x0600A429 RID: 42025 RVA: 0x0043099B File Offset: 0x0042ED9B
		private void OnBeginDragTrans()
		{
			this.oldValue = Singleton<Studio>.Instance.sceneInfo.mapInfo.ca.pos;
			this.transMap = Singleton<Map>.Instance.MapRoot.transform;
		}

		// Token: 0x0600A42A RID: 42026 RVA: 0x004309D4 File Offset: 0x0042EDD4
		private void OnEndDragTrans()
		{
			Singleton<UndoRedoManager>.Instance.Push(new MapCommand.MoveEqualsCommand(new MapCommand.EqualsInfo
			{
				newValue = Singleton<Studio>.Instance.sceneInfo.mapInfo.ca.pos,
				oldValue = this.oldValue
			}));
			this.transMap = null;
		}

		// Token: 0x0600A42B RID: 42027 RVA: 0x00430A2C File Offset: 0x0042EE2C
		private void OnDragTransXZ()
		{
			Vector3 direction = new Vector3(Input.GetAxis("Mouse X"), 0f, Input.GetAxis("Mouse Y"));
			Singleton<Studio>.Instance.sceneInfo.mapInfo.ca.pos += this.transMap.TransformDirection(direction);
		}

		// Token: 0x0600A42C RID: 42028 RVA: 0x00430A8C File Offset: 0x0042EE8C
		private void OnDragTransY()
		{
			Vector3 direction = new Vector3(0f, Input.GetAxis("Mouse Y"), 0f);
			Singleton<Studio>.Instance.sceneInfo.mapInfo.ca.pos += this.transMap.TransformDirection(direction);
		}

		// Token: 0x0600A42D RID: 42029 RVA: 0x00430AE4 File Offset: 0x0042EEE4
		private void OnBeginDragRot()
		{
			this.oldValue = Singleton<Studio>.Instance.sceneInfo.mapInfo.ca.rot;
		}

		// Token: 0x0600A42E RID: 42030 RVA: 0x00430B08 File Offset: 0x0042EF08
		private void OnEndDragRot()
		{
			Singleton<UndoRedoManager>.Instance.Push(new MapCommand.RotationEqualsCommand(new MapCommand.EqualsInfo
			{
				newValue = Singleton<Studio>.Instance.sceneInfo.mapInfo.ca.rot,
				oldValue = this.oldValue
			}));
		}

		// Token: 0x0600A42F RID: 42031 RVA: 0x00430B58 File Offset: 0x0042EF58
		private void OnDragRotX()
		{
			Vector3 rot = Singleton<Studio>.Instance.sceneInfo.mapInfo.ca.rot;
			rot.x = (rot.x + Input.GetAxis("Mouse Y")) % 360f;
			Singleton<Studio>.Instance.sceneInfo.mapInfo.ca.rot = rot;
		}

		// Token: 0x0600A430 RID: 42032 RVA: 0x00430BB8 File Offset: 0x0042EFB8
		private void OnDragRotY()
		{
			Vector3 rot = Singleton<Studio>.Instance.sceneInfo.mapInfo.ca.rot;
			rot.y = (rot.y + Input.GetAxis("Mouse X")) % 360f;
			Singleton<Studio>.Instance.sceneInfo.mapInfo.ca.rot = rot;
		}

		// Token: 0x0600A431 RID: 42033 RVA: 0x00430C18 File Offset: 0x0042F018
		private void OnDragRotZ()
		{
			Vector3 rot = Singleton<Studio>.Instance.sceneInfo.mapInfo.ca.rot;
			rot.z = (rot.z + Input.GetAxis("Mouse X")) % 360f;
			Singleton<Studio>.Instance.sceneInfo.mapInfo.ca.rot = rot;
		}

		// Token: 0x0600A432 RID: 42034 RVA: 0x00430C78 File Offset: 0x0042F078
		private void OnValueChangedOption(bool _value)
		{
			if (this.isUpdate)
			{
				return;
			}
			Singleton<Map>.Instance.VisibleOption = _value;
		}

		// Token: 0x0600A433 RID: 42035 RVA: 0x00430C94 File Offset: 0x0042F094
		protected override void Awake()
		{
			if (!base.CheckInstance())
			{
				return;
			}
			MapDragButton mapDragButton = this.mapDragButton[0];
			mapDragButton.onBeginDragFunc = (Action)Delegate.Combine(mapDragButton.onBeginDragFunc, new Action(this.OnBeginDragTrans));
			MapDragButton mapDragButton2 = this.mapDragButton[0];
			mapDragButton2.onDragFunc = (Action)Delegate.Combine(mapDragButton2.onDragFunc, new Action(this.OnDragTransXZ));
			MapDragButton mapDragButton3 = this.mapDragButton[0];
			mapDragButton3.onEndDragFunc = (Action)Delegate.Combine(mapDragButton3.onEndDragFunc, new Action(this.OnEndDragTrans));
			MapDragButton mapDragButton4 = this.mapDragButton[1];
			mapDragButton4.onBeginDragFunc = (Action)Delegate.Combine(mapDragButton4.onBeginDragFunc, new Action(this.OnBeginDragTrans));
			MapDragButton mapDragButton5 = this.mapDragButton[1];
			mapDragButton5.onDragFunc = (Action)Delegate.Combine(mapDragButton5.onDragFunc, new Action(this.OnDragTransY));
			MapDragButton mapDragButton6 = this.mapDragButton[1];
			mapDragButton6.onEndDragFunc = (Action)Delegate.Combine(mapDragButton6.onEndDragFunc, new Action(this.OnEndDragTrans));
			for (int i = 0; i < 3; i++)
			{
				MapDragButton mapDragButton7 = this.mapDragButton[2 + i];
				mapDragButton7.onBeginDragFunc = (Action)Delegate.Combine(mapDragButton7.onBeginDragFunc, new Action(this.OnBeginDragRot));
				MapDragButton mapDragButton8 = this.mapDragButton[2 + i];
				mapDragButton8.onEndDragFunc = (Action)Delegate.Combine(mapDragButton8.onEndDragFunc, new Action(this.OnEndDragRot));
			}
			MapDragButton mapDragButton9 = this.mapDragButton[2];
			mapDragButton9.onDragFunc = (Action)Delegate.Combine(mapDragButton9.onDragFunc, new Action(this.OnDragRotX));
			MapDragButton mapDragButton10 = this.mapDragButton[3];
			mapDragButton10.onDragFunc = (Action)Delegate.Combine(mapDragButton10.onDragFunc, new Action(this.OnDragRotY));
			MapDragButton mapDragButton11 = this.mapDragButton[4];
			mapDragButton11.onDragFunc = (Action)Delegate.Combine(mapDragButton11.onDragFunc, new Action(this.OnDragRotZ));
			this.toggleOption.onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChangedOption));
		}

		// Token: 0x0400815A RID: 33114
		[SerializeField]
		private TMP_InputField[] inputPos;

		// Token: 0x0400815B RID: 33115
		[SerializeField]
		private TMP_InputField[] inputRot;

		// Token: 0x0400815C RID: 33116
		[SerializeField]
		private MapDragButton[] mapDragButton;

		// Token: 0x0400815D RID: 33117
		[SerializeField]
		private Toggle toggleOption;

		// Token: 0x0400815E RID: 33118
		[SerializeField]
		private Toggle toggleLight;

		// Token: 0x0400815F RID: 33119
		private Vector3 oldValue = Vector3.zero;

		// Token: 0x04008160 RID: 33120
		private Transform transMap;

		// Token: 0x04008161 RID: 33121
		private bool isUpdate;
	}
}
