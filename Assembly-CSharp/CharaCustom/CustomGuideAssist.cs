using System;

namespace CharaCustom
{
	// Token: 0x02000A10 RID: 2576
	public class CustomGuideAssist
	{
		// Token: 0x06004CAB RID: 19627 RVA: 0x001D78D4 File Offset: 0x001D5CD4
		public static void SetCameraMoveFlag(CameraControl_Ver2 _ctrl, bool _bPlay)
		{
			if (_ctrl == null)
			{
				return;
			}
			if (CustomGuideAssist.IsCameraMoveFlag(_ctrl) == _bPlay)
			{
				return;
			}
			_ctrl.NoCtrlCondition = (() => !_bPlay);
		}

		// Token: 0x06004CAC RID: 19628 RVA: 0x001D7920 File Offset: 0x001D5D20
		public static bool IsCameraMoveFlag(CameraControl_Ver2 _ctrl)
		{
			if (_ctrl == null)
			{
				return false;
			}
			BaseCameraControl_Ver2.NoCtrlFunc noCtrlCondition = _ctrl.NoCtrlCondition;
			bool flag = true;
			if (noCtrlCondition != null)
			{
				flag = noCtrlCondition();
			}
			return !flag;
		}

		// Token: 0x06004CAD RID: 19629 RVA: 0x001D7955 File Offset: 0x001D5D55
		public static bool IsCameraActionFlag(CameraControl_Ver2 _ctrl)
		{
			return !(_ctrl == null) && _ctrl.isControlNow;
		}
	}
}
