using System;
using UnityEngine;

namespace Rewired.Demos
{
	// Token: 0x0200050E RID: 1294
	[AddComponentMenu("")]
	public class CustomControllerDemo : MonoBehaviour
	{
		// Token: 0x060018B2 RID: 6322 RVA: 0x00098E94 File Offset: 0x00097294
		private void Awake()
		{
			if (SystemInfo.deviceType == DeviceType.Handheld && Screen.orientation != ScreenOrientation.LandscapeLeft)
			{
				Screen.orientation = ScreenOrientation.LandscapeLeft;
			}
			this.Initialize();
		}

		// Token: 0x060018B3 RID: 6323 RVA: 0x00098EB8 File Offset: 0x000972B8
		private void Initialize()
		{
			ReInput.InputSourceUpdateEvent += this.OnInputSourceUpdate;
			this.joysticks = base.GetComponentsInChildren<TouchJoystickExample>();
			this.buttons = base.GetComponentsInChildren<TouchButtonExample>();
			this.axisCount = this.joysticks.Length * 2;
			this.buttonCount = this.buttons.Length;
			this.axisValues = new float[this.axisCount];
			this.buttonValues = new bool[this.buttonCount];
			Player player = ReInput.players.GetPlayer(this.playerId);
			this.controller = player.controllers.GetControllerWithTag<CustomController>(this.controllerTag);
			if (this.controller == null)
			{
				UnityEngine.Debug.LogError("A matching controller was not found for tag \"" + this.controllerTag + "\"");
			}
			if (this.controller.buttonCount != this.buttonValues.Length || this.controller.axisCount != this.axisValues.Length)
			{
				UnityEngine.Debug.LogError("Controller has wrong number of elements!");
			}
			if (this.useUpdateCallbacks && this.controller != null)
			{
				this.controller.SetAxisUpdateCallback(new Func<int, float>(this.GetAxisValueCallback));
				this.controller.SetButtonUpdateCallback(new Func<int, bool>(this.GetButtonValueCallback));
			}
			this.initialized = true;
		}

		// Token: 0x060018B4 RID: 6324 RVA: 0x00099000 File Offset: 0x00097400
		private void Update()
		{
			if (!ReInput.isReady)
			{
				return;
			}
			if (!this.initialized)
			{
				this.Initialize();
			}
		}

		// Token: 0x060018B5 RID: 6325 RVA: 0x0009901E File Offset: 0x0009741E
		private void OnInputSourceUpdate()
		{
			this.GetSourceAxisValues();
			this.GetSourceButtonValues();
			if (!this.useUpdateCallbacks)
			{
				this.SetControllerAxisValues();
				this.SetControllerButtonValues();
			}
		}

		// Token: 0x060018B6 RID: 6326 RVA: 0x00099044 File Offset: 0x00097444
		private void GetSourceAxisValues()
		{
			for (int i = 0; i < this.axisValues.Length; i++)
			{
				if (i % 2 != 0)
				{
					this.axisValues[i] = this.joysticks[i / 2].position.y;
				}
				else
				{
					this.axisValues[i] = this.joysticks[i / 2].position.x;
				}
			}
		}

		// Token: 0x060018B7 RID: 6327 RVA: 0x000990B8 File Offset: 0x000974B8
		private void GetSourceButtonValues()
		{
			for (int i = 0; i < this.buttonValues.Length; i++)
			{
				this.buttonValues[i] = this.buttons[i].isPressed;
			}
		}

		// Token: 0x060018B8 RID: 6328 RVA: 0x000990F4 File Offset: 0x000974F4
		private void SetControllerAxisValues()
		{
			for (int i = 0; i < this.axisValues.Length; i++)
			{
				this.controller.SetAxisValue(i, this.axisValues[i]);
			}
		}

		// Token: 0x060018B9 RID: 6329 RVA: 0x00099130 File Offset: 0x00097530
		private void SetControllerButtonValues()
		{
			for (int i = 0; i < this.buttonValues.Length; i++)
			{
				this.controller.SetButtonValue(i, this.buttonValues[i]);
			}
		}

		// Token: 0x060018BA RID: 6330 RVA: 0x0009916A File Offset: 0x0009756A
		private float GetAxisValueCallback(int index)
		{
			if (index >= this.axisValues.Length)
			{
				return 0f;
			}
			return this.axisValues[index];
		}

		// Token: 0x060018BB RID: 6331 RVA: 0x00099188 File Offset: 0x00097588
		private bool GetButtonValueCallback(int index)
		{
			return index < this.buttonValues.Length && this.buttonValues[index];
		}

		// Token: 0x04001B9C RID: 7068
		public int playerId;

		// Token: 0x04001B9D RID: 7069
		public string controllerTag;

		// Token: 0x04001B9E RID: 7070
		public bool useUpdateCallbacks;

		// Token: 0x04001B9F RID: 7071
		private int buttonCount;

		// Token: 0x04001BA0 RID: 7072
		private int axisCount;

		// Token: 0x04001BA1 RID: 7073
		private float[] axisValues;

		// Token: 0x04001BA2 RID: 7074
		private bool[] buttonValues;

		// Token: 0x04001BA3 RID: 7075
		private TouchJoystickExample[] joysticks;

		// Token: 0x04001BA4 RID: 7076
		private TouchButtonExample[] buttons;

		// Token: 0x04001BA5 RID: 7077
		private CustomController controller;

		// Token: 0x04001BA6 RID: 7078
		[NonSerialized]
		private bool initialized;
	}
}
