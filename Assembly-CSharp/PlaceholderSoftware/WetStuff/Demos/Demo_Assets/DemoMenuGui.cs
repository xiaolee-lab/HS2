using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PlaceholderSoftware.WetStuff.Demos.Demo_Assets
{
	// Token: 0x020004CB RID: 1227
	public class DemoMenuGui : MonoBehaviour
	{
		// Token: 0x060016AC RID: 5804 RVA: 0x0008B574 File Offset: 0x00089974
		private void OnGUI()
		{
			using (new GUILayout.HorizontalScope(Array.Empty<GUILayoutOption>()))
			{
				GUILayout.Space(10f);
				using (new GUILayout.VerticalScope(Array.Empty<GUILayoutOption>()))
				{
					GUILayout.Space(10f);
					DemoMenuGui.SceneButton("1. Puddle");
					DemoMenuGui.SceneButton("2. Timeline");
					DemoMenuGui.SceneButton("3. Rain");
					DemoMenuGui.SceneButton("4. Particles (Splat)");
					DemoMenuGui.SceneButton("5. Particles (Drip Drip Drip)");
					DemoMenuGui.SceneButton("6. Triplanar Mapping");
					DemoMenuGui.SceneButton("7. Dry Decals");
				}
			}
		}

		// Token: 0x060016AD RID: 5805 RVA: 0x0008B634 File Offset: 0x00089A34
		private static void SceneButton(string scene)
		{
			if (GUILayout.Button(scene, Array.Empty<GUILayoutOption>()))
			{
				SceneManager.LoadScene(scene);
			}
		}
	}
}
