using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using Rewired.Internal;
using Rewired.Utils.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Rewired.Utils
{
	// Token: 0x0200058B RID: 1419
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class ExternalTools : IExternalTools
	{
		// Token: 0x06002077 RID: 8311 RVA: 0x000B1B6F File Offset: 0x000AFF6F
		public object GetPlatformInitializer()
		{
			return null;
		}

		// Token: 0x06002078 RID: 8312 RVA: 0x000B1B72 File Offset: 0x000AFF72
		public string GetFocusedEditorWindowTitle()
		{
			return string.Empty;
		}

		// Token: 0x06002079 RID: 8313 RVA: 0x000B1B79 File Offset: 0x000AFF79
		public bool IsEditorSceneViewFocused()
		{
			return false;
		}

		// Token: 0x0600207A RID: 8314 RVA: 0x000B1B7C File Offset: 0x000AFF7C
		public bool LinuxInput_IsJoystickPreconfigured(string name)
		{
			return false;
		}

		// Token: 0x14000077 RID: 119
		// (add) Token: 0x0600207B RID: 8315 RVA: 0x000B1B80 File Offset: 0x000AFF80
		// (remove) Token: 0x0600207C RID: 8316 RVA: 0x000B1BB8 File Offset: 0x000AFFB8
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<uint, bool> XboxOneInput_OnGamepadStateChange;

		// Token: 0x0600207D RID: 8317 RVA: 0x000B1BEE File Offset: 0x000AFFEE
		public int XboxOneInput_GetUserIdForGamepad(uint id)
		{
			return 0;
		}

		// Token: 0x0600207E RID: 8318 RVA: 0x000B1BF1 File Offset: 0x000AFFF1
		public ulong XboxOneInput_GetControllerId(uint unityJoystickId)
		{
			return 0UL;
		}

		// Token: 0x0600207F RID: 8319 RVA: 0x000B1BF5 File Offset: 0x000AFFF5
		public bool XboxOneInput_IsGamepadActive(uint unityJoystickId)
		{
			return false;
		}

		// Token: 0x06002080 RID: 8320 RVA: 0x000B1BF8 File Offset: 0x000AFFF8
		public string XboxOneInput_GetControllerType(ulong xboxControllerId)
		{
			return string.Empty;
		}

		// Token: 0x06002081 RID: 8321 RVA: 0x000B1BFF File Offset: 0x000AFFFF
		public uint XboxOneInput_GetJoystickId(ulong xboxControllerId)
		{
			return 0U;
		}

		// Token: 0x06002082 RID: 8322 RVA: 0x000B1C02 File Offset: 0x000B0002
		public void XboxOne_Gamepad_UpdatePlugin()
		{
		}

		// Token: 0x06002083 RID: 8323 RVA: 0x000B1C04 File Offset: 0x000B0004
		public bool XboxOne_Gamepad_SetGamepadVibration(ulong xboxOneJoystickId, float leftMotor, float rightMotor, float leftTriggerLevel, float rightTriggerLevel)
		{
			return false;
		}

		// Token: 0x06002084 RID: 8324 RVA: 0x000B1C07 File Offset: 0x000B0007
		public void XboxOne_Gamepad_PulseVibrateMotor(ulong xboxOneJoystickId, int motorInt, float startLevel, float endLevel, ulong durationMS)
		{
		}

		// Token: 0x06002085 RID: 8325 RVA: 0x000B1C09 File Offset: 0x000B0009
		public Vector3 PS4Input_GetLastAcceleration(int id)
		{
			return Vector3.zero;
		}

		// Token: 0x06002086 RID: 8326 RVA: 0x000B1C10 File Offset: 0x000B0010
		public Vector3 PS4Input_GetLastGyro(int id)
		{
			return Vector3.zero;
		}

		// Token: 0x06002087 RID: 8327 RVA: 0x000B1C17 File Offset: 0x000B0017
		public Vector4 PS4Input_GetLastOrientation(int id)
		{
			return Vector4.zero;
		}

		// Token: 0x06002088 RID: 8328 RVA: 0x000B1C1E File Offset: 0x000B001E
		public void PS4Input_GetLastTouchData(int id, out int touchNum, out int touch0x, out int touch0y, out int touch0id, out int touch1x, out int touch1y, out int touch1id)
		{
			touchNum = 0;
			touch0x = 0;
			touch0y = 0;
			touch0id = 0;
			touch1x = 0;
			touch1y = 0;
			touch1id = 0;
		}

		// Token: 0x06002089 RID: 8329 RVA: 0x000B1C3A File Offset: 0x000B003A
		public void PS4Input_GetPadControllerInformation(int id, out float touchpixelDensity, out int touchResolutionX, out int touchResolutionY, out int analogDeadZoneLeft, out int analogDeadZoneright, out int connectionType)
		{
			touchpixelDensity = 0f;
			touchResolutionX = 0;
			touchResolutionY = 0;
			analogDeadZoneLeft = 0;
			analogDeadZoneright = 0;
			connectionType = 0;
		}

		// Token: 0x0600208A RID: 8330 RVA: 0x000B1C56 File Offset: 0x000B0056
		public void PS4Input_PadSetMotionSensorState(int id, bool bEnable)
		{
		}

		// Token: 0x0600208B RID: 8331 RVA: 0x000B1C58 File Offset: 0x000B0058
		public void PS4Input_PadSetTiltCorrectionState(int id, bool bEnable)
		{
		}

		// Token: 0x0600208C RID: 8332 RVA: 0x000B1C5A File Offset: 0x000B005A
		public void PS4Input_PadSetAngularVelocityDeadbandState(int id, bool bEnable)
		{
		}

		// Token: 0x0600208D RID: 8333 RVA: 0x000B1C5C File Offset: 0x000B005C
		public void PS4Input_PadSetLightBar(int id, int red, int green, int blue)
		{
		}

		// Token: 0x0600208E RID: 8334 RVA: 0x000B1C5E File Offset: 0x000B005E
		public void PS4Input_PadResetLightBar(int id)
		{
		}

		// Token: 0x0600208F RID: 8335 RVA: 0x000B1C60 File Offset: 0x000B0060
		public void PS4Input_PadSetVibration(int id, int largeMotor, int smallMotor)
		{
		}

		// Token: 0x06002090 RID: 8336 RVA: 0x000B1C62 File Offset: 0x000B0062
		public void PS4Input_PadResetOrientation(int id)
		{
		}

		// Token: 0x06002091 RID: 8337 RVA: 0x000B1C64 File Offset: 0x000B0064
		public bool PS4Input_PadIsConnected(int id)
		{
			return false;
		}

		// Token: 0x06002092 RID: 8338 RVA: 0x000B1C67 File Offset: 0x000B0067
		public void PS4Input_GetUsersDetails(int slot, object loggedInUser)
		{
		}

		// Token: 0x06002093 RID: 8339 RVA: 0x000B1C69 File Offset: 0x000B0069
		public int PS4Input_GetDeviceClassForHandle(int handle)
		{
			return -1;
		}

		// Token: 0x06002094 RID: 8340 RVA: 0x000B1C6C File Offset: 0x000B006C
		public string PS4Input_GetDeviceClassString(int intValue)
		{
			return null;
		}

		// Token: 0x06002095 RID: 8341 RVA: 0x000B1C6F File Offset: 0x000B006F
		public int PS4Input_PadGetUsersHandles2(int maxControllers, int[] handles)
		{
			return 0;
		}

		// Token: 0x06002096 RID: 8342 RVA: 0x000B1C72 File Offset: 0x000B0072
		public Vector3 PS4Input_GetLastMoveAcceleration(int id, int index)
		{
			return Vector3.zero;
		}

		// Token: 0x06002097 RID: 8343 RVA: 0x000B1C79 File Offset: 0x000B0079
		public Vector3 PS4Input_GetLastMoveGyro(int id, int index)
		{
			return Vector3.zero;
		}

		// Token: 0x06002098 RID: 8344 RVA: 0x000B1C80 File Offset: 0x000B0080
		public int PS4Input_MoveGetButtons(int id, int index)
		{
			return 0;
		}

		// Token: 0x06002099 RID: 8345 RVA: 0x000B1C83 File Offset: 0x000B0083
		public int PS4Input_MoveGetAnalogButton(int id, int index)
		{
			return 0;
		}

		// Token: 0x0600209A RID: 8346 RVA: 0x000B1C86 File Offset: 0x000B0086
		public bool PS4Input_MoveIsConnected(int id, int index)
		{
			return false;
		}

		// Token: 0x0600209B RID: 8347 RVA: 0x000B1C89 File Offset: 0x000B0089
		public int PS4Input_MoveGetUsersMoveHandles(int maxNumberControllers, int[] primaryHandles, int[] secondaryHandles)
		{
			return 0;
		}

		// Token: 0x0600209C RID: 8348 RVA: 0x000B1C8C File Offset: 0x000B008C
		public int PS4Input_MoveGetUsersMoveHandles(int maxNumberControllers, int[] primaryHandles)
		{
			return 0;
		}

		// Token: 0x0600209D RID: 8349 RVA: 0x000B1C8F File Offset: 0x000B008F
		public int PS4Input_MoveGetUsersMoveHandles(int maxNumberControllers)
		{
			return 0;
		}

		// Token: 0x0600209E RID: 8350 RVA: 0x000B1C92 File Offset: 0x000B0092
		public IntPtr PS4Input_MoveGetControllerInputForTracking()
		{
			return IntPtr.Zero;
		}

		// Token: 0x0600209F RID: 8351 RVA: 0x000B1C99 File Offset: 0x000B0099
		public void GetSpecialControllerInformation(int id, int padIndex, object controllerInformation)
		{
		}

		// Token: 0x060020A0 RID: 8352 RVA: 0x000B1C9B File Offset: 0x000B009B
		public Vector3 PS4Input_SpecialGetLastAcceleration(int id)
		{
			return Vector3.zero;
		}

		// Token: 0x060020A1 RID: 8353 RVA: 0x000B1CA2 File Offset: 0x000B00A2
		public Vector3 PS4Input_SpecialGetLastGyro(int id)
		{
			return Vector3.zero;
		}

		// Token: 0x060020A2 RID: 8354 RVA: 0x000B1CA9 File Offset: 0x000B00A9
		public Vector4 PS4Input_SpecialGetLastOrientation(int id)
		{
			return Vector4.zero;
		}

		// Token: 0x060020A3 RID: 8355 RVA: 0x000B1CB0 File Offset: 0x000B00B0
		public int PS4Input_SpecialGetUsersHandles(int maxNumberControllers, int[] handles)
		{
			return 0;
		}

		// Token: 0x060020A4 RID: 8356 RVA: 0x000B1CB3 File Offset: 0x000B00B3
		public int PS4Input_SpecialGetUsersHandles2(int maxNumberControllers, int[] handles)
		{
			return 0;
		}

		// Token: 0x060020A5 RID: 8357 RVA: 0x000B1CB6 File Offset: 0x000B00B6
		public bool PS4Input_SpecialIsConnected(int id)
		{
			return false;
		}

		// Token: 0x060020A6 RID: 8358 RVA: 0x000B1CB9 File Offset: 0x000B00B9
		public void PS4Input_SpecialResetLightSphere(int id)
		{
		}

		// Token: 0x060020A7 RID: 8359 RVA: 0x000B1CBB File Offset: 0x000B00BB
		public void PS4Input_SpecialResetOrientation(int id)
		{
		}

		// Token: 0x060020A8 RID: 8360 RVA: 0x000B1CBD File Offset: 0x000B00BD
		public void PS4Input_SpecialSetAngularVelocityDeadbandState(int id, bool bEnable)
		{
		}

		// Token: 0x060020A9 RID: 8361 RVA: 0x000B1CBF File Offset: 0x000B00BF
		public void PS4Input_SpecialSetLightSphere(int id, int red, int green, int blue)
		{
		}

		// Token: 0x060020AA RID: 8362 RVA: 0x000B1CC1 File Offset: 0x000B00C1
		public void PS4Input_SpecialSetMotionSensorState(int id, bool bEnable)
		{
		}

		// Token: 0x060020AB RID: 8363 RVA: 0x000B1CC3 File Offset: 0x000B00C3
		public void PS4Input_SpecialSetTiltCorrectionState(int id, bool bEnable)
		{
		}

		// Token: 0x060020AC RID: 8364 RVA: 0x000B1CC5 File Offset: 0x000B00C5
		public void PS4Input_SpecialSetVibration(int id, int largeMotor, int smallMotor)
		{
		}

		// Token: 0x060020AD RID: 8365 RVA: 0x000B1CC7 File Offset: 0x000B00C7
		public void GetDeviceVIDPIDs(out List<int> vids, out List<int> pids)
		{
			vids = new List<int>();
			pids = new List<int>();
		}

		// Token: 0x060020AE RID: 8366 RVA: 0x000B1CD7 File Offset: 0x000B00D7
		public int GetAndroidAPILevel()
		{
			return -1;
		}

		// Token: 0x060020AF RID: 8367 RVA: 0x000B1CDA File Offset: 0x000B00DA
		public bool UnityUI_Graphic_GetRaycastTarget(object graphic)
		{
			return !(graphic as Graphic == null) && (graphic as Graphic).raycastTarget;
		}

		// Token: 0x060020B0 RID: 8368 RVA: 0x000B1CFA File Offset: 0x000B00FA
		public void UnityUI_Graphic_SetRaycastTarget(object graphic, bool value)
		{
			if (graphic as Graphic == null)
			{
				return;
			}
			(graphic as Graphic).raycastTarget = value;
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x060020B1 RID: 8369 RVA: 0x000B1D1A File Offset: 0x000B011A
		public bool UnityInput_IsTouchPressureSupported
		{
			get
			{
				return Input.touchPressureSupported;
			}
		}

		// Token: 0x060020B2 RID: 8370 RVA: 0x000B1D21 File Offset: 0x000B0121
		public float UnityInput_GetTouchPressure(ref Touch touch)
		{
			return touch.pressure;
		}

		// Token: 0x060020B3 RID: 8371 RVA: 0x000B1D29 File Offset: 0x000B0129
		public float UnityInput_GetTouchMaximumPossiblePressure(ref Touch touch)
		{
			return touch.maximumPossiblePressure;
		}

		// Token: 0x060020B4 RID: 8372 RVA: 0x000B1D31 File Offset: 0x000B0131
		public IControllerTemplate CreateControllerTemplate(Guid typeGuid, object payload)
		{
			return ControllerTemplateFactory.Create(typeGuid, payload);
		}

		// Token: 0x060020B5 RID: 8373 RVA: 0x000B1D3A File Offset: 0x000B013A
		public Type[] GetControllerTemplateTypes()
		{
			return ControllerTemplateFactory.templateTypes;
		}

		// Token: 0x060020B6 RID: 8374 RVA: 0x000B1D41 File Offset: 0x000B0141
		public Type[] GetControllerTemplateInterfaceTypes()
		{
			return ControllerTemplateFactory.templateInterfaceTypes;
		}
	}
}
