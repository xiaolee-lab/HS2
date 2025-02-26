using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using Rewired.Platforms;
using Rewired.Utils;
using Rewired.Utils.Interfaces;
using UnityEngine;

namespace Rewired
{
	// Token: 0x0200058A RID: 1418
	[EditorBrowsable(EditorBrowsableState.Never)]
	public sealed class InputManager : InputManager_Base
	{
		// Token: 0x06002072 RID: 8306 RVA: 0x000B1AF0 File Offset: 0x000AFEF0
		protected override void DetectPlatform()
		{
			this.editorPlatform = EditorPlatform.None;
			this.platform = Platform.Unknown;
			this.webplayerPlatform = WebplayerPlatform.None;
			this.isEditor = false;
			string text = SystemInfo.deviceName ?? string.Empty;
			string text2 = SystemInfo.deviceModel ?? string.Empty;
			this.platform = Platform.Windows;
		}

		// Token: 0x06002073 RID: 8307 RVA: 0x000B1B44 File Offset: 0x000AFF44
		protected override void CheckRecompile()
		{
		}

		// Token: 0x06002074 RID: 8308 RVA: 0x000B1B46 File Offset: 0x000AFF46
		protected override IExternalTools GetExternalTools()
		{
			return new ExternalTools();
		}

		// Token: 0x06002075 RID: 8309 RVA: 0x000B1B4D File Offset: 0x000AFF4D
		private bool CheckDeviceName(string searchPattern, string deviceName, string deviceModel)
		{
			return Regex.IsMatch(deviceName, searchPattern, RegexOptions.IgnoreCase) || Regex.IsMatch(deviceModel, searchPattern, RegexOptions.IgnoreCase);
		}
	}
}
