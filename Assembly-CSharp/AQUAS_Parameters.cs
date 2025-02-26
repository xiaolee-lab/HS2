using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

// Token: 0x02000066 RID: 102
[Serializable]
public class AQUAS_Parameters
{
	// Token: 0x02000067 RID: 103
	[Serializable]
	public class UnderWaterParameters
	{
		// Token: 0x040001BB RID: 443
		[Header("The following parameters apply for underwater only!")]
		[Space(5f)]
		public float fogDensity = 0.1f;

		// Token: 0x040001BC RID: 444
		public Color fogColor;

		// Token: 0x040001BD RID: 445
		[Space(5f)]
		[Header("Post Processing Profiles (Must NOT be empty!)")]
		[Space(5f)]
		public PostProcessProfile underwaterProfile;

		// Token: 0x040001BE RID: 446
		public PostProcessProfile defaultProfile;
	}

	// Token: 0x02000068 RID: 104
	[Serializable]
	public class GameObjects
	{
		// Token: 0x040001BF RID: 447
		[Header("Set the game objects required for underwater mode.")]
		[Space(5f)]
		public GameObject mainCamera;

		// Token: 0x040001C0 RID: 448
		public GameObject waterLens;

		// Token: 0x040001C1 RID: 449
		public GameObject airLens;

		// Token: 0x040001C2 RID: 450
		public GameObject bubble;

		// Token: 0x040001C3 RID: 451
		[Space(5f)]
		[Header("Set waterplanes array size = number of waterplanes")]
		public List<GameObject> waterPlanes = new List<GameObject>();

		// Token: 0x040001C4 RID: 452
		public bool useSquaredPlanes;
	}

	// Token: 0x02000069 RID: 105
	[Serializable]
	public class WetLens
	{
		// Token: 0x040001C5 RID: 453
		[Header("Set how long the lens stays wet after diving up.")]
		public float wetTime = 1f;

		// Token: 0x040001C6 RID: 454
		[Space(5f)]
		[Header("Set how long the lens needs to dry.")]
		public float dryingTime = 1.5f;

		// Token: 0x040001C7 RID: 455
		[Space(5f)]
		public Texture2D[] sprayFrames;

		// Token: 0x040001C8 RID: 456
		public Texture2D[] sprayFramesCutout;

		// Token: 0x040001C9 RID: 457
		public float rundownSpeed = 72f;
	}

	// Token: 0x0200006A RID: 106
	[Serializable]
	public class CausticSettings
	{
		// Token: 0x040001CA RID: 458
		[Header("The following values are 'Afloat'/'Underwater'")]
		public Vector2 causticIntensity = new Vector2(0.6f, 0.2f);

		// Token: 0x040001CB RID: 459
		public Vector2 causticTiling = new Vector2(300f, 100f);

		// Token: 0x040001CC RID: 460
		public float maxCausticDepth;
	}

	// Token: 0x0200006B RID: 107
	[Serializable]
	public class Audio
	{
		// Token: 0x040001CD RID: 461
		public AudioClip[] sounds;

		// Token: 0x040001CE RID: 462
		[Range(0f, 1f)]
		public float underwaterVolume;

		// Token: 0x040001CF RID: 463
		[Range(0f, 1f)]
		public float surfacingVolume;

		// Token: 0x040001D0 RID: 464
		[Range(0f, 1f)]
		public float diveVolume;
	}

	// Token: 0x0200006C RID: 108
	[Serializable]
	public class BubbleSpawnCriteria
	{
		// Token: 0x040001D1 RID: 465
		[Header("Spawn Criteria for big bubbles")]
		public int minBubbleCount = 20;

		// Token: 0x040001D2 RID: 466
		public int maxBubbleCount = 40;

		// Token: 0x040001D3 RID: 467
		[Space(5f)]
		public float maxSpawnDistance = 1f;

		// Token: 0x040001D4 RID: 468
		public float averageUpdrift = 3f;

		// Token: 0x040001D5 RID: 469
		[Space(5f)]
		public float baseScale = 0.06f;

		// Token: 0x040001D6 RID: 470
		public float avgScaleSummand = 0.15f;

		// Token: 0x040001D7 RID: 471
		[Space(5f)]
		[Header("Spawn Timer for initial dive")]
		public float minSpawnTimer = 0.005f;

		// Token: 0x040001D8 RID: 472
		public float maxSpawnTimer = 0.03f;

		// Token: 0x040001D9 RID: 473
		[Space(5f)]
		[Header("Spawn Timer for long dive")]
		public float minSpawnTimerL = 0.1f;

		// Token: 0x040001DA RID: 474
		public float maxSpawnTimerL = 0.5f;
	}
}
