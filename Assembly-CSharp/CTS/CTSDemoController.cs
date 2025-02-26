using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace CTS
{
	// Token: 0x0200068C RID: 1676
	public class CTSDemoController : MonoBehaviour
	{
		// Token: 0x0600273A RID: 10042 RVA: 0x000E667C File Offset: 0x000E4A7C
		private void Awake()
		{
			if (this.m_target == null)
			{
				this.m_target = Camera.main.gameObject;
			}
			try
			{
				if (this.m_postFX != null)
				{
					Camera camera = Camera.main;
					if (camera == null)
					{
						camera = UnityEngine.Object.FindObjectOfType<Camera>();
					}
					if (camera != null)
					{
						Type type = CTSDemoController.GetType("UnityEngine.PostProcessing.PostProcessingBehaviour");
						if (type != null)
						{
							GameObject gameObject = camera.gameObject;
							this.m_postProcessingComponent = gameObject.GetComponent(type);
							if (this.m_postProcessingComponent == null)
							{
								this.m_postProcessingComponent = gameObject.AddComponent(type);
							}
							if (this.m_postProcessingComponent != null)
							{
								FieldInfo field = type.GetField("profile", BindingFlags.Instance | BindingFlags.Public);
								if (field != null)
								{
									field.SetValue(this.m_postProcessingComponent, this.m_postFX);
								}
								((MonoBehaviour)this.m_postProcessingComponent).enabled = false;
							}
						}
					}
				}
			}
			catch (Exception)
			{
			}
			if (this.m_flyController == null)
			{
				this.m_flyController = this.m_target.GetComponent<CTSFly>();
			}
			if (this.m_flyController == null)
			{
				this.m_flyController = this.m_target.AddComponent<CTSFly>();
			}
			this.m_flyController.enabled = false;
			if (this.m_characterController == null)
			{
				this.m_characterController = this.m_target.GetComponent<CharacterController>();
			}
			if (this.m_characterController == null)
			{
				this.m_characterController = this.m_target.AddComponent<CharacterController>();
				this.m_characterController.height = 4f;
			}
			this.m_characterController.enabled = false;
			if (this.m_walkController == null)
			{
				this.m_walkController = this.m_target.GetComponent<CTSWalk>();
			}
			if (this.m_walkController == null)
			{
				this.m_walkController = this.m_target.AddComponent<CTSWalk>();
				this.m_walkController.m_controller = this.m_characterController;
			}
			this.m_walkController.enabled = false;
			if (this.m_lookController == null)
			{
				this.m_lookController = this.m_target.GetComponent<CTSLook>();
			}
			if (this.m_lookController == null)
			{
				this.m_lookController = this.m_target.AddComponent<CTSLook>();
				this.m_lookController._playerRootT = this.m_target.transform;
				this.m_lookController._cameraT = this.m_target.transform;
			}
			this.m_lookController.enabled = false;
			if (this.m_instructions != null)
			{
				string text = string.Empty;
				if (this.m_unityProfile != null)
				{
					text += "Controls: 1. Unity";
				}
				if (this.m_liteProfile != null)
				{
					if (text.Length > 0)
					{
						text += ", 2. Lite";
					}
					else
					{
						text = "Controls: 2. Lite";
					}
				}
				if (this.m_basicProfile != null)
				{
					if (text.Length > 0)
					{
						text += ", 3. Basic";
					}
					else
					{
						text = "Controls: 3. Basic";
					}
				}
				if (this.m_advancedProfile != null)
				{
					if (text.Length > 0)
					{
						text += ", 4. Advanced";
					}
					else
					{
						text = "Controls: 4. Advanced";
					}
				}
				if (this.m_tesselatedProfile != null)
				{
					if (text.Length > 0)
					{
						text += ", 5. Tesselated";
					}
					else
					{
						text = "Controls: 5. Tesselated";
					}
				}
				if (this.m_flyController != null)
				{
					if (text.Length > 0)
					{
						text += ", 6. Fly";
					}
					else
					{
						text = "Controls: 6. Fly";
					}
				}
				if (this.m_walkController != null)
				{
					if (text.Length > 0)
					{
						text += ", 7. Walk";
					}
					else
					{
						text = "Controls: 7. Walk";
					}
				}
				if (this.m_postProcessingComponent != null)
				{
					if (text.Length > 0)
					{
						text += ", P. Post FX";
					}
					else
					{
						text = "Controls: P. Post FX";
					}
				}
				if (text.Length > 0)
				{
					text += ", ESC. Exit.";
				}
				else
				{
					text = "Controls: ESC. Exit.";
				}
				this.m_instructions.text = text;
			}
			this.SelectBasic();
			if (this.m_flyController != null)
			{
				this.m_flyController.enabled = false;
			}
			if (this.m_walkController != null)
			{
				this.m_walkController.enabled = false;
			}
			if (this.m_characterController != null)
			{
				this.m_characterController.enabled = false;
			}
			if (this.m_lookController != null)
			{
				this.m_lookController.enabled = false;
			}
		}

		// Token: 0x0600273B RID: 10043 RVA: 0x000E6B8C File Offset: 0x000E4F8C
		public void SelectUnity()
		{
			if (this.m_unityProfile != null)
			{
				CTSSingleton<CTSTerrainManager>.Instance.BroadcastProfileSelect(this.m_unityProfile);
				if (this.m_mode != null)
				{
					this.m_mode.text = "Unity";
				}
			}
		}

		// Token: 0x0600273C RID: 10044 RVA: 0x000E6BDC File Offset: 0x000E4FDC
		public void SelectLite()
		{
			if (this.m_liteProfile != null)
			{
				CTSSingleton<CTSTerrainManager>.Instance.BroadcastProfileSelect(this.m_liteProfile);
				if (this.m_mode != null)
				{
					this.m_mode.text = "Lite";
				}
			}
		}

		// Token: 0x0600273D RID: 10045 RVA: 0x000E6C2C File Offset: 0x000E502C
		public void SelectBasic()
		{
			if (this.m_basicProfile != null)
			{
				CTSSingleton<CTSTerrainManager>.Instance.BroadcastProfileSelect(this.m_basicProfile);
				if (this.m_mode != null)
				{
					this.m_mode.text = "Basic";
				}
			}
		}

		// Token: 0x0600273E RID: 10046 RVA: 0x000E6C7C File Offset: 0x000E507C
		public void SelectAdvanced()
		{
			if (this.m_advancedProfile != null)
			{
				CTSSingleton<CTSTerrainManager>.Instance.BroadcastProfileSelect(this.m_advancedProfile);
				if (this.m_mode != null)
				{
					this.m_mode.text = "Advanced";
				}
			}
		}

		// Token: 0x0600273F RID: 10047 RVA: 0x000E6CCC File Offset: 0x000E50CC
		public void SelectTesselated()
		{
			if (this.m_tesselatedProfile != null)
			{
				CTSSingleton<CTSTerrainManager>.Instance.BroadcastProfileSelect(this.m_tesselatedProfile);
				if (this.m_mode != null)
				{
					this.m_mode.text = "Tesselated";
				}
			}
		}

		// Token: 0x06002740 RID: 10048 RVA: 0x000E6D1C File Offset: 0x000E511C
		public void Fly()
		{
			if (this.m_flyController != null && !this.m_flyController.isActiveAndEnabled)
			{
				if (this.m_characterController != null)
				{
					this.m_characterController.enabled = false;
				}
				if (this.m_walkController != null && this.m_walkController.isActiveAndEnabled)
				{
					this.m_walkController.enabled = false;
				}
				if (this.m_lookController != null)
				{
					this.m_lookController.enabled = true;
				}
				this.m_flyController.enabled = true;
			}
		}

		// Token: 0x06002741 RID: 10049 RVA: 0x000E6DC0 File Offset: 0x000E51C0
		public void Walk()
		{
			if (this.m_walkController != null && !this.m_walkController.isActiveAndEnabled)
			{
				if (this.m_flyController != null && this.m_flyController.isActiveAndEnabled)
				{
					this.m_flyController.enabled = false;
				}
				if (this.m_characterController != null)
				{
					this.m_characterController.enabled = true;
				}
				if (this.m_lookController != null)
				{
					this.m_lookController.enabled = true;
				}
				this.m_walkController.enabled = true;
			}
		}

		// Token: 0x06002742 RID: 10050 RVA: 0x000E6E64 File Offset: 0x000E5264
		public void PostFX()
		{
			if (this.m_postProcessingComponent != null)
			{
				if (((MonoBehaviour)this.m_postProcessingComponent).isActiveAndEnabled)
				{
					((MonoBehaviour)this.m_postProcessingComponent).enabled = false;
				}
				else
				{
					((MonoBehaviour)this.m_postProcessingComponent).enabled = true;
				}
			}
		}

		// Token: 0x06002743 RID: 10051 RVA: 0x000E6EC0 File Offset: 0x000E52C0
		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Alpha1))
			{
				this.SelectUnity();
			}
			else if (Input.GetKeyDown(KeyCode.Alpha2))
			{
				this.SelectLite();
			}
			else if (Input.GetKeyDown(KeyCode.Alpha3))
			{
				this.SelectBasic();
			}
			else if (Input.GetKeyDown(KeyCode.Alpha4))
			{
				this.SelectAdvanced();
			}
			else if (Input.GetKeyDown(KeyCode.Alpha5))
			{
				this.SelectTesselated();
			}
			else if (Input.GetKeyDown(KeyCode.Alpha6))
			{
				this.Fly();
			}
			else if (Input.GetKeyDown(KeyCode.Alpha7))
			{
				this.Walk();
			}
			else if (Input.GetKeyDown(KeyCode.P))
			{
				this.PostFX();
			}
			else if (Input.GetKeyDown(KeyCode.Escape))
			{
				Application.Quit();
			}
		}

		// Token: 0x06002744 RID: 10052 RVA: 0x000E6F98 File Offset: 0x000E5398
		public static Type GetType(string TypeName)
		{
			Type type = Type.GetType(TypeName);
			if (type != null)
			{
				return type;
			}
			if (TypeName.Contains("."))
			{
				string assemblyString = TypeName.Substring(0, TypeName.IndexOf('.'));
				try
				{
					Assembly assembly = Assembly.Load(assemblyString);
					if (assembly == null)
					{
						return null;
					}
					type = assembly.GetType(TypeName);
					if (type != null)
					{
						return type;
					}
				}
				catch (Exception)
				{
				}
			}
			Assembly callingAssembly = Assembly.GetCallingAssembly();
			if (callingAssembly != null)
			{
				type = callingAssembly.GetType(TypeName);
				if (type != null)
				{
					return type;
				}
			}
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			for (int i = 0; i < assemblies.GetLength(0); i++)
			{
				type = assemblies[i].GetType(TypeName);
				if (type != null)
				{
					return type;
				}
			}
			AssemblyName[] referencedAssemblies = callingAssembly.GetReferencedAssemblies();
			foreach (AssemblyName assemblyRef in referencedAssemblies)
			{
				Assembly assembly2 = Assembly.Load(assemblyRef);
				if (assembly2 != null)
				{
					type = assembly2.GetType(TypeName);
					if (type != null)
					{
						return type;
					}
				}
			}
			return null;
		}

		// Token: 0x0400279B RID: 10139
		[Header("Target")]
		public GameObject m_target;

		// Token: 0x0400279C RID: 10140
		[Header("Walk Controller")]
		public CTSWalk m_walkController;

		// Token: 0x0400279D RID: 10141
		private CharacterController m_characterController;

		// Token: 0x0400279E RID: 10142
		[Header("Fly Controller")]
		public CTSFly m_flyController;

		// Token: 0x0400279F RID: 10143
		[Header("Look Controller")]
		public CTSLook m_lookController;

		// Token: 0x040027A0 RID: 10144
		[Header("Profiles")]
		public CTSProfile m_unityProfile;

		// Token: 0x040027A1 RID: 10145
		public CTSProfile m_liteProfile;

		// Token: 0x040027A2 RID: 10146
		public CTSProfile m_basicProfile;

		// Token: 0x040027A3 RID: 10147
		public CTSProfile m_advancedProfile;

		// Token: 0x040027A4 RID: 10148
		public CTSProfile m_tesselatedProfile;

		// Token: 0x040027A5 RID: 10149
		[Header("UX Text")]
		public Text m_mode;

		// Token: 0x040027A6 RID: 10150
		public Text m_readme;

		// Token: 0x040027A7 RID: 10151
		public Text m_instructions;

		// Token: 0x040027A8 RID: 10152
		[Header("Post FX")]
		public ScriptableObject m_postFX;

		// Token: 0x040027A9 RID: 10153
		private Component m_postProcessingComponent;
	}
}
