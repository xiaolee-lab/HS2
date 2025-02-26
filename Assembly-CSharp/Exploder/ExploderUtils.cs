using System;
using System.Diagnostics;
using UnityEngine;

namespace Exploder
{
	// Token: 0x020003CA RID: 970
	public static class ExploderUtils
	{
		// Token: 0x06001134 RID: 4404 RVA: 0x00064FFC File Offset: 0x000633FC
		[Conditional("UNITY_EDITOR_NO_DEBUG")]
		public static void Assert(bool condition, string message)
		{
			if (!condition)
			{
				UnityEngine.Debug.LogError("Assert! " + message);
				UnityEngine.Debug.Break();
			}
		}

		// Token: 0x06001135 RID: 4405 RVA: 0x00065019 File Offset: 0x00063419
		[Conditional("UNITY_EDITOR_NO_DEBUG")]
		public static void Warning(bool condition, string message)
		{
			if (!condition)
			{
				UnityEngine.Debug.LogWarning("Warning! " + message);
			}
		}

		// Token: 0x06001136 RID: 4406 RVA: 0x00065031 File Offset: 0x00063431
		[Conditional("UNITY_EDITOR_NO_DEBUG")]
		public static void Log(string message)
		{
			UnityEngine.Debug.Log(message);
		}

		// Token: 0x06001137 RID: 4407 RVA: 0x0006503C File Offset: 0x0006343C
		public static Vector3 GetCentroid(GameObject obj)
		{
			MeshRenderer[] componentsInChildren = obj.GetComponentsInChildren<MeshRenderer>();
			Vector3 a = Vector3.zero;
			if (componentsInChildren != null && componentsInChildren.Length != 0)
			{
				foreach (MeshRenderer meshRenderer in componentsInChildren)
				{
					a += meshRenderer.bounds.center;
				}
				return a / (float)componentsInChildren.Length;
			}
			SkinnedMeshRenderer componentInChildren = obj.GetComponentInChildren<SkinnedMeshRenderer>();
			if (componentInChildren)
			{
				return componentInChildren.bounds.center;
			}
			return obj.transform.position;
		}

		// Token: 0x06001138 RID: 4408 RVA: 0x000650D8 File Offset: 0x000634D8
		public static void SetVisible(GameObject obj, bool status)
		{
			if (obj)
			{
				MeshRenderer[] componentsInChildren = obj.GetComponentsInChildren<MeshRenderer>();
				foreach (MeshRenderer meshRenderer in componentsInChildren)
				{
					meshRenderer.enabled = status;
				}
			}
		}

		// Token: 0x06001139 RID: 4409 RVA: 0x00065118 File Offset: 0x00063518
		public static void ClearLog()
		{
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x0006511A File Offset: 0x0006351A
		public static bool IsActive(GameObject obj)
		{
			return obj && obj.activeSelf;
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x00065130 File Offset: 0x00063530
		public static void SetActive(GameObject obj, bool status)
		{
			if (obj)
			{
				obj.SetActive(status);
			}
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x00065144 File Offset: 0x00063544
		public static void SetActiveRecursively(GameObject obj, bool status)
		{
			if (obj)
			{
				int childCount = obj.transform.childCount;
				for (int i = 0; i < childCount; i++)
				{
					ExploderUtils.SetActiveRecursively(obj.transform.GetChild(i).gameObject, status);
				}
				obj.SetActive(status);
			}
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x00065198 File Offset: 0x00063598
		public static void EnableCollider(GameObject obj, bool status)
		{
			if (obj)
			{
				Collider[] componentsInChildren = obj.GetComponentsInChildren<Collider>();
				foreach (Collider collider in componentsInChildren)
				{
					collider.enabled = status;
				}
			}
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x000651D8 File Offset: 0x000635D8
		public static bool IsExplodable(GameObject obj)
		{
			bool flag = obj.GetComponent<Explodable>() != null;
			if (!flag)
			{
				flag = obj.CompareTag(ExploderObject.Tag);
			}
			return flag;
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x00065208 File Offset: 0x00063608
		public static void CopyAudioSource(AudioSource src, AudioSource dst)
		{
			dst.bypassEffects = src.bypassEffects;
			dst.bypassListenerEffects = src.bypassListenerEffects;
			dst.bypassReverbZones = src.bypassReverbZones;
			dst.clip = src.clip;
			dst.dopplerLevel = src.dopplerLevel;
			dst.enabled = src.enabled;
			dst.ignoreListenerPause = src.ignoreListenerPause;
			dst.ignoreListenerVolume = src.ignoreListenerVolume;
			dst.loop = src.loop;
			dst.maxDistance = src.maxDistance;
			dst.minDistance = src.minDistance;
			dst.mute = src.mute;
			dst.outputAudioMixerGroup = src.outputAudioMixerGroup;
			dst.panStereo = src.panStereo;
			dst.pitch = src.pitch;
			dst.playOnAwake = src.playOnAwake;
			dst.priority = src.priority;
			dst.reverbZoneMix = src.reverbZoneMix;
			dst.rolloffMode = src.rolloffMode;
			dst.spatialBlend = src.spatialBlend;
			dst.spatialize = src.spatialize;
			dst.spread = src.spread;
			dst.time = src.time;
			dst.timeSamples = src.timeSamples;
			dst.velocityUpdateMode = src.velocityUpdateMode;
			dst.volume = src.volume;
		}
	}
}
