using System;
using System.Diagnostics;
using Manager;
using UnityEngine;

// Token: 0x020008D8 RID: 2264
public static class InitAddComponent
{
	// Token: 0x06003B88 RID: 15240 RVA: 0x0015CD20 File Offset: 0x0015B120
	public static void AddComponents(GameObject go)
	{
		go.AddComponent<GameSystem>();
		go.AddComponent<Sound>();
		go.AddComponent<SoundPlayer>();
		go.AddComponent<Config>();
		go.AddComponent<Voice>();
		go.AddComponent<Scene>();
		go.AddComponent<Character>();
		go.AddComponent<AnimalManager>();
		go.AddComponent<Map>();
		go.AddComponent<ADV>();
		go.AddComponent<Housing>();
		go.AddComponent<Game>();
		go.AddComponent<Manager.Input>();
		go.AddComponent<HSceneManager>();
	}

	// Token: 0x06003B89 RID: 15241 RVA: 0x0015CD8F File Offset: 0x0015B18F
	[Conditional("__DEBUG_PROC__")]
	private static void DebugAddComponents(GameObject go)
	{
		go.AddComponent<DebugShow>();
		go.AddComponent<DebugRenderLog>();
	}
}
