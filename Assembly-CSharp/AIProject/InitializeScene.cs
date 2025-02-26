using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using Manager;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AIProject
{
	// Token: 0x02000B42 RID: 2882
	public class InitializeScene : BaseLoader
	{
		// Token: 0x06005453 RID: 21587 RVA: 0x00252D2C File Offset: 0x0025112C
		private void Start()
		{
			if (SystemInfo.graphicsShaderLevel < 30)
			{
				MonoBehaviour.print("shaders Non support");
			}
			if (File.Exists("UserData/setup.xml"))
			{
				try
				{
					this._xml = XElement.Load("UserData/setup.xml");
					if (this._xml != null)
					{
						IEnumerable<XElement> enumerable = this._xml.Elements();
						foreach (XElement xelement in enumerable)
						{
							string text = xelement.Name.ToString();
							if (text != null)
							{
								if (!(text == "Width"))
								{
									if (!(text == "Height"))
									{
										if (!(text == "FullScreen"))
										{
											if (text == "Quality")
											{
												this._quality = int.Parse(xelement.Value);
											}
										}
										else
										{
											this._full = bool.Parse(xelement.Value);
										}
									}
									else
									{
										this._height = int.Parse(xelement.Value);
									}
								}
								else
								{
									this._width = int.Parse(xelement.Value);
								}
							}
						}
						Screen.SetResolution(this._width, this._height, this._full);
						int quality = this._quality;
						if (quality != 0)
						{
							if (quality != 1)
							{
								if (quality == 2)
								{
									QualitySettings.SetQualityLevel(4);
								}
							}
							else
							{
								QualitySettings.SetQualityLevel(2);
							}
						}
						else
						{
							QualitySettings.SetQualityLevel(0);
						}
					}
				}
				catch (XmlException ex)
				{
					File.Delete("UserData/setup.xml");
				}
			}
			Singleton<Manager.Scene>.Instance.SetFadeColor(this._fadeColor);
			string scenePathByBuildIndex = SceneUtility.GetScenePathByBuildIndex(this._loadLevel);
			int num = scenePathByBuildIndex.LastIndexOf("/");
			int num2 = scenePathByBuildIndex.LastIndexOf(".");
			string levelName = scenePathByBuildIndex.Substring(num + 1, num2 - num - 1);
			Manager.Scene.Data data = new Manager.Scene.Data
			{
				levelName = levelName,
				isFade = true
			};
			Singleton<Manager.Scene>.Instance.LoadReserve(data, false);
		}

		// Token: 0x04004F26 RID: 20262
		private XElement _xml;

		// Token: 0x04004F27 RID: 20263
		private int _width = 1280;

		// Token: 0x04004F28 RID: 20264
		private int _height = 720;

		// Token: 0x04004F29 RID: 20265
		private int _quality = 2;

		// Token: 0x04004F2A RID: 20266
		private bool _full;

		// Token: 0x04004F2B RID: 20267
		[SerializeField]
		[SceneEnum]
		private int _loadLevel = 1;

		// Token: 0x04004F2C RID: 20268
		private const string SetupFilePath = "UserData/setup.xml";

		// Token: 0x04004F2D RID: 20269
		[SerializeField]
		private Color _fadeColor = Color.black;
	}
}
