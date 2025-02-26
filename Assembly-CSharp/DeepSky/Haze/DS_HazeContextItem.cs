using System;
using System.Reflection;
using UnityEngine;

namespace DeepSky.Haze
{
	// Token: 0x020002EA RID: 746
	[AddComponentMenu("")]
	[Serializable]
	public class DS_HazeContextItem
	{
		// Token: 0x06000CA3 RID: 3235 RVA: 0x000339DC File Offset: 0x00031DDC
		public DS_HazeContextItem()
		{
			this.m_Name = "New";
			this.m_Weight = new AnimationCurve(new Keyframe[]
			{
				new Keyframe(0.25f, 0f),
				new Keyframe(0.5f, 1f),
				new Keyframe(0.75f, 0f)
			});
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x00033B00 File Offset: 0x00031F00
		public static float MultiplierAsFloat(DS_HazeContextItem.Multiplier mult)
		{
			switch (mult)
			{
			case DS_HazeContextItem.Multiplier.OneTenth:
				return 0.1f;
			case DS_HazeContextItem.Multiplier.OneFifth:
				return 0.2f;
			case DS_HazeContextItem.Multiplier.OneHalf:
				return 0.5f;
			case DS_HazeContextItem.Multiplier.One:
				return 1f;
			case DS_HazeContextItem.Multiplier.Two:
				return 2f;
			case DS_HazeContextItem.Multiplier.Five:
				return 5f;
			case DS_HazeContextItem.Multiplier.Ten:
				return 10f;
			case DS_HazeContextItem.Multiplier.OneHundredth:
				return 0.01f;
			default:
				return 1f;
			}
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x00033B70 File Offset: 0x00031F70
		public static float ParamWithMultiplier(float param, DS_HazeContextItem.Multiplier mult)
		{
			switch (mult)
			{
			case DS_HazeContextItem.Multiplier.OneTenth:
				return param * 0.1f;
			case DS_HazeContextItem.Multiplier.OneFifth:
				return param * 0.2f;
			case DS_HazeContextItem.Multiplier.OneHalf:
				return param * 0.5f;
			case DS_HazeContextItem.Multiplier.One:
				return param * 1f;
			case DS_HazeContextItem.Multiplier.Two:
				return param * 2f;
			case DS_HazeContextItem.Multiplier.Five:
				return param * 5f;
			case DS_HazeContextItem.Multiplier.Ten:
				return param * 10f;
			case DS_HazeContextItem.Multiplier.OneHundredth:
				return param * 0.01f;
			default:
				return param * 1f;
			}
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x00033BF0 File Offset: 0x00031FF0
		public void Lerp(DS_HazeContextItem other, float dt)
		{
			if (other == null)
			{
				return;
			}
			dt = Mathf.Clamp01(dt);
			float num = 1f - dt;
			this.m_AirScatteringScale = this.m_AirScatteringScale * num + other.m_AirScatteringScale * dt;
			this.m_AirDensityHeightFalloff = this.m_AirDensityHeightFalloff * num + other.m_AirDensityHeightFalloff * dt;
			this.m_HazeScatteringScale = this.m_HazeScatteringScale * num + other.m_HazeScatteringScale * dt;
			this.m_HazeDensityHeightFalloff = this.m_HazeDensityHeightFalloff * num + other.m_HazeDensityHeightFalloff * dt;
			this.m_HazeScatteringDirection = this.m_HazeScatteringDirection * num + other.m_HazeScatteringDirection * dt;
			this.m_HazeSecondaryScatteringRatio = this.m_HazeSecondaryScatteringRatio * num + other.m_HazeSecondaryScatteringRatio * dt;
			this.m_FogOpacity = this.m_FogOpacity * num + other.m_FogOpacity * dt;
			this.m_FogScatteringScale = this.m_FogScatteringScale * num + other.m_FogScatteringScale * dt;
			this.m_FogExtinctionScale = this.m_FogExtinctionScale * num + other.m_FogExtinctionScale * dt;
			this.m_FogDensityHeightFalloff = this.m_FogDensityHeightFalloff * num + other.m_FogDensityHeightFalloff * dt;
			this.m_FogStartDistance = this.m_FogStartDistance * num + other.m_FogStartDistance * dt;
			this.m_FogScatteringDirection = this.m_FogScatteringDirection * num + other.m_FogScatteringDirection * dt;
			this.m_FogStartHeight = this.m_FogStartHeight * num + other.m_FogStartHeight * dt;
			this.m_FogAmbientColour = this.m_FogAmbientColour * num + other.m_FogAmbientColour * dt;
			this.m_FogLightColour = this.m_FogLightColour * num + other.m_FogLightColour * dt;
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x00033D88 File Offset: 0x00032188
		public void CopyFrom(DS_HazeContextItem other)
		{
			if (other == null)
			{
				return;
			}
			Type type = base.GetType();
			Type type2 = other.GetType();
			foreach (FieldInfo fieldInfo in type.GetFields())
			{
				FieldInfo field = type2.GetField(fieldInfo.Name);
				fieldInfo.SetValue(this, field.GetValue(other));
			}
			this.m_Weight = new AnimationCurve(this.m_Weight.keys);
		}

		// Token: 0x04000BA9 RID: 2985
		[SerializeField]
		public string m_Name;

		// Token: 0x04000BAA RID: 2986
		[SerializeField]
		public AnimationCurve m_Weight;

		// Token: 0x04000BAB RID: 2987
		[SerializeField]
		[Range(0f, 8f)]
		public float m_AirScatteringScale = 1f;

		// Token: 0x04000BAC RID: 2988
		[SerializeField]
		public DS_HazeContextItem.Multiplier m_AirScatteringMultiplier = DS_HazeContextItem.Multiplier.One;

		// Token: 0x04000BAD RID: 2989
		[SerializeField]
		[Range(0.0001f, 0.1f)]
		public float m_AirDensityHeightFalloff = 0.001f;

		// Token: 0x04000BAE RID: 2990
		[SerializeField]
		[Range(0f, 8f)]
		public float m_HazeScatteringScale = 1f;

		// Token: 0x04000BAF RID: 2991
		[SerializeField]
		public DS_HazeContextItem.Multiplier m_HazeScatteringMultiplier = DS_HazeContextItem.Multiplier.One;

		// Token: 0x04000BB0 RID: 2992
		[SerializeField]
		[Range(0.0001f, 0.1f)]
		public float m_HazeDensityHeightFalloff = 0.003f;

		// Token: 0x04000BB1 RID: 2993
		[SerializeField]
		[Range(-0.99f, 0.99f)]
		public float m_HazeScatteringDirection = 0.8f;

		// Token: 0x04000BB2 RID: 2994
		[SerializeField]
		[Range(0f, 1f)]
		public float m_HazeSecondaryScatteringRatio = 0.8f;

		// Token: 0x04000BB3 RID: 2995
		[SerializeField]
		[Range(0f, 1f)]
		public float m_FogOpacity = 1f;

		// Token: 0x04000BB4 RID: 2996
		[SerializeField]
		[Range(0f, 8f)]
		public float m_FogScatteringScale = 1f;

		// Token: 0x04000BB5 RID: 2997
		[SerializeField]
		[Range(0f, 8f)]
		public float m_FogExtinctionScale = 1f;

		// Token: 0x04000BB6 RID: 2998
		[SerializeField]
		public DS_HazeContextItem.Multiplier m_FogExtinctionMultiplier = DS_HazeContextItem.Multiplier.One;

		// Token: 0x04000BB7 RID: 2999
		[SerializeField]
		[Range(0.0001f, 1f)]
		public float m_FogDensityHeightFalloff = 0.01f;

		// Token: 0x04000BB8 RID: 3000
		[SerializeField]
		[Range(0f, 1f)]
		public float m_FogStartDistance;

		// Token: 0x04000BB9 RID: 3001
		[SerializeField]
		[Range(-0.99f, 0.99f)]
		public float m_FogScatteringDirection = 0.7f;

		// Token: 0x04000BBA RID: 3002
		[SerializeField]
		[Range(-10000f, 10000f)]
		public float m_FogStartHeight;

		// Token: 0x04000BBB RID: 3003
		[SerializeField]
		public Color m_FogAmbientColour = Color.white;

		// Token: 0x04000BBC RID: 3004
		[SerializeField]
		public Color m_FogLightColour = Color.white;

		// Token: 0x020002EB RID: 747
		public enum Multiplier
		{
			// Token: 0x04000BBE RID: 3006
			OneTenth,
			// Token: 0x04000BBF RID: 3007
			OneFifth,
			// Token: 0x04000BC0 RID: 3008
			OneHalf,
			// Token: 0x04000BC1 RID: 3009
			One,
			// Token: 0x04000BC2 RID: 3010
			Two,
			// Token: 0x04000BC3 RID: 3011
			Five,
			// Token: 0x04000BC4 RID: 3012
			Ten,
			// Token: 0x04000BC5 RID: 3013
			OneHundredth
		}
	}
}
