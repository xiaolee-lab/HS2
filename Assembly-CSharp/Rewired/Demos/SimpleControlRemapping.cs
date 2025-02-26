using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Rewired.Demos
{
	// Token: 0x02000524 RID: 1316
	[AddComponentMenu("")]
	public class SimpleControlRemapping : MonoBehaviour
	{
		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06001949 RID: 6473 RVA: 0x0009C0F9 File Offset: 0x0009A4F9
		private Player player
		{
			get
			{
				return ReInput.players.GetPlayer(0);
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x0600194A RID: 6474 RVA: 0x0009C108 File Offset: 0x0009A508
		private ControllerMap controllerMap
		{
			get
			{
				if (this.controller == null)
				{
					return null;
				}
				return this.player.controllers.maps.GetMap(this.controller.type, this.controller.id, "Default", "Default");
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x0600194B RID: 6475 RVA: 0x0009C157 File Offset: 0x0009A557
		private Controller controller
		{
			get
			{
				return this.player.controllers.GetController(this.selectedControllerType, this.selectedControllerId);
			}
		}

		// Token: 0x0600194C RID: 6476 RVA: 0x0009C178 File Offset: 0x0009A578
		private void OnEnable()
		{
			if (!ReInput.isReady)
			{
				return;
			}
			this.inputMapper.options.timeout = 5f;
			this.inputMapper.options.ignoreMouseXAxis = true;
			this.inputMapper.options.ignoreMouseYAxis = true;
			ReInput.ControllerConnectedEvent += this.OnControllerChanged;
			ReInput.ControllerDisconnectedEvent += this.OnControllerChanged;
			this.inputMapper.InputMappedEvent += this.OnInputMapped;
			this.inputMapper.StoppedEvent += this.OnStopped;
			this.InitializeUI();
		}

		// Token: 0x0600194D RID: 6477 RVA: 0x0009C21D File Offset: 0x0009A61D
		private void OnDisable()
		{
			this.inputMapper.Stop();
			this.inputMapper.RemoveAllEventListeners();
			ReInput.ControllerConnectedEvent -= this.OnControllerChanged;
			ReInput.ControllerDisconnectedEvent -= this.OnControllerChanged;
		}

		// Token: 0x0600194E RID: 6478 RVA: 0x0009C258 File Offset: 0x0009A658
		private void RedrawUI()
		{
			if (this.controller == null)
			{
				this.ClearUI();
				return;
			}
			this.controllerNameUIText.text = this.controller.name;
			for (int i = 0; i < this.rows.Count; i++)
			{
				SimpleControlRemapping.Row row = this.rows[i];
				InputAction action = this.rows[i].action;
				string text = string.Empty;
				int actionElementMapId = -1;
				foreach (ActionElementMap actionElementMap in this.controllerMap.ElementMapsWithAction(action.id))
				{
					if (actionElementMap.ShowInField(row.actionRange))
					{
						text = actionElementMap.elementIdentifierName;
						actionElementMapId = actionElementMap.id;
						break;
					}
				}
				row.text.text = text;
				row.button.onClick.RemoveAllListeners();
				int index = i;
				row.button.onClick.AddListener(delegate()
				{
					this.OnInputFieldClicked(index, actionElementMapId);
				});
			}
		}

		// Token: 0x0600194F RID: 6479 RVA: 0x0009C3A8 File Offset: 0x0009A7A8
		private void ClearUI()
		{
			if (this.selectedControllerType == ControllerType.Joystick)
			{
				this.controllerNameUIText.text = "No joysticks attached";
			}
			else
			{
				this.controllerNameUIText.text = string.Empty;
			}
			for (int i = 0; i < this.rows.Count; i++)
			{
				this.rows[i].text.text = string.Empty;
			}
		}

		// Token: 0x06001950 RID: 6480 RVA: 0x0009C420 File Offset: 0x0009A820
		private void InitializeUI()
		{
			IEnumerator enumerator = this.actionGroupTransform.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					Transform transform = (Transform)obj;
					UnityEngine.Object.Destroy(transform.gameObject);
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
			IEnumerator enumerator2 = this.fieldGroupTransform.GetEnumerator();
			try
			{
				while (enumerator2.MoveNext())
				{
					object obj2 = enumerator2.Current;
					Transform transform2 = (Transform)obj2;
					UnityEngine.Object.Destroy(transform2.gameObject);
				}
			}
			finally
			{
				IDisposable disposable2;
				if ((disposable2 = (enumerator2 as IDisposable)) != null)
				{
					disposable2.Dispose();
				}
			}
			foreach (InputAction inputAction in ReInput.mapping.Actions)
			{
				if (inputAction.type == InputActionType.Axis)
				{
					this.CreateUIRow(inputAction, AxisRange.Full, inputAction.descriptiveName);
					this.CreateUIRow(inputAction, AxisRange.Positive, string.IsNullOrEmpty(inputAction.positiveDescriptiveName) ? (inputAction.descriptiveName + " +") : inputAction.positiveDescriptiveName);
					this.CreateUIRow(inputAction, AxisRange.Negative, string.IsNullOrEmpty(inputAction.negativeDescriptiveName) ? (inputAction.descriptiveName + " -") : inputAction.negativeDescriptiveName);
				}
				else if (inputAction.type == InputActionType.Button)
				{
					this.CreateUIRow(inputAction, AxisRange.Positive, inputAction.descriptiveName);
				}
			}
			this.RedrawUI();
		}

		// Token: 0x06001951 RID: 6481 RVA: 0x0009C5E4 File Offset: 0x0009A9E4
		private void CreateUIRow(InputAction action, AxisRange actionRange, string label)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.textPrefab);
			gameObject.transform.SetParent(this.actionGroupTransform);
			gameObject.transform.SetAsLastSibling();
			gameObject.GetComponent<Text>().text = label;
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.buttonPrefab);
			gameObject2.transform.SetParent(this.fieldGroupTransform);
			gameObject2.transform.SetAsLastSibling();
			this.rows.Add(new SimpleControlRemapping.Row
			{
				action = action,
				actionRange = actionRange,
				button = gameObject2.GetComponent<Button>(),
				text = gameObject2.GetComponentInChildren<Text>()
			});
		}

		// Token: 0x06001952 RID: 6482 RVA: 0x0009C688 File Offset: 0x0009AA88
		private void SetSelectedController(ControllerType controllerType)
		{
			bool flag = false;
			if (controllerType != this.selectedControllerType)
			{
				this.selectedControllerType = controllerType;
				flag = true;
			}
			int num = this.selectedControllerId;
			if (this.selectedControllerType == ControllerType.Joystick)
			{
				if (this.player.controllers.joystickCount > 0)
				{
					this.selectedControllerId = this.player.controllers.Joysticks[0].id;
				}
				else
				{
					this.selectedControllerId = -1;
				}
			}
			else
			{
				this.selectedControllerId = 0;
			}
			if (this.selectedControllerId != num)
			{
				flag = true;
			}
			if (flag)
			{
				this.inputMapper.Stop();
				this.RedrawUI();
			}
		}

		// Token: 0x06001953 RID: 6483 RVA: 0x0009C733 File Offset: 0x0009AB33
		public void OnControllerSelected(int controllerType)
		{
			this.SetSelectedController((ControllerType)controllerType);
		}

		// Token: 0x06001954 RID: 6484 RVA: 0x0009C73C File Offset: 0x0009AB3C
		private void OnInputFieldClicked(int index, int actionElementMapToReplaceId)
		{
			if (index < 0 || index >= this.rows.Count)
			{
				return;
			}
			if (this.controller == null)
			{
				return;
			}
			this.inputMapper.Start(new InputMapper.Context
			{
				actionId = this.rows[index].action.id,
				controllerMap = this.controllerMap,
				actionRange = this.rows[index].actionRange,
				actionElementMapToReplace = this.controllerMap.GetElementMap(actionElementMapToReplaceId)
			});
			this.statusUIText.text = "Listening...";
		}

		// Token: 0x06001955 RID: 6485 RVA: 0x0009C7E2 File Offset: 0x0009ABE2
		private void OnControllerChanged(ControllerStatusChangedEventArgs args)
		{
			this.SetSelectedController(this.selectedControllerType);
		}

		// Token: 0x06001956 RID: 6486 RVA: 0x0009C7F0 File Offset: 0x0009ABF0
		private void OnInputMapped(InputMapper.InputMappedEventData data)
		{
			this.RedrawUI();
		}

		// Token: 0x06001957 RID: 6487 RVA: 0x0009C7F8 File Offset: 0x0009ABF8
		private void OnStopped(InputMapper.StoppedEventData data)
		{
			this.statusUIText.text = string.Empty;
		}

		// Token: 0x04001C35 RID: 7221
		private const string category = "Default";

		// Token: 0x04001C36 RID: 7222
		private const string layout = "Default";

		// Token: 0x04001C37 RID: 7223
		private InputMapper inputMapper = new InputMapper();

		// Token: 0x04001C38 RID: 7224
		public GameObject buttonPrefab;

		// Token: 0x04001C39 RID: 7225
		public GameObject textPrefab;

		// Token: 0x04001C3A RID: 7226
		public RectTransform fieldGroupTransform;

		// Token: 0x04001C3B RID: 7227
		public RectTransform actionGroupTransform;

		// Token: 0x04001C3C RID: 7228
		public Text controllerNameUIText;

		// Token: 0x04001C3D RID: 7229
		public Text statusUIText;

		// Token: 0x04001C3E RID: 7230
		private ControllerType selectedControllerType;

		// Token: 0x04001C3F RID: 7231
		private int selectedControllerId;

		// Token: 0x04001C40 RID: 7232
		private List<SimpleControlRemapping.Row> rows = new List<SimpleControlRemapping.Row>();

		// Token: 0x02000525 RID: 1317
		private class Row
		{
			// Token: 0x04001C41 RID: 7233
			public InputAction action;

			// Token: 0x04001C42 RID: 7234
			public AxisRange actionRange;

			// Token: 0x04001C43 RID: 7235
			public Button button;

			// Token: 0x04001C44 RID: 7236
			public Text text;
		}
	}
}
