using System;
using System.IO;
using System.Xml.Linq;
using UnityEngine;

namespace MeshBrush
{
	// Token: 0x020003F6 RID: 1014
	public static class TemplateMigrationUtility
	{
		// Token: 0x06001220 RID: 4640 RVA: 0x00070790 File Offset: 0x0006EB90
		public static bool TryMigrate(string filePath)
		{
			if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
			{
				return false;
			}
			try
			{
				XDocument xdocument = XDocument.Load(filePath);
				MeshBrush meshBrush = new GameObject("MeshBrush Template Migration Utility")
				{
					hideFlags = HideFlags.HideAndDontSave
				}.AddComponent<MeshBrush>();
				foreach (XElement xelement in xdocument.Descendants())
				{
					string localName = xelement.Name.LocalName;
					switch (localName)
					{
					case "meshBrushTemplate":
					{
						XAttribute xattribute = xelement.Attribute("version");
						if (xattribute != null && 1.9f <= float.Parse(xattribute.Value))
						{
							return false;
						}
						break;
					}
					case "active":
					case "isActive":
						meshBrush.active = (string.CompareOrdinal(xelement.Value, "true") == 0);
						break;
					case "groupName":
						meshBrush.groupName = xelement.Value;
						break;
					case "classicUI":
						meshBrush.classicUI = (string.CompareOrdinal(xelement.Value, "true") == 0);
						break;
					case "previewIconSize":
						meshBrush.previewIconSize = float.Parse(xelement.Value);
						break;
					case "lockSceneView":
						meshBrush.lockSceneView = (string.CompareOrdinal(xelement.Value, "true") == 0);
						break;
					case "trisCounter":
						meshBrush.stats = (string.CompareOrdinal(xelement.Value, "true") == 0);
						break;
					case "globalPaintingLayers":
					{
						int num2 = 0;
						foreach (XElement xelement2 in xelement.Elements())
						{
							meshBrush.layerMask[num2] = (string.CompareOrdinal(xelement2.Value, "false") != 0);
							num2++;
						}
						break;
					}
					case "paintKey":
						meshBrush.paintKey = (KeyCode)Enum.Parse(typeof(KeyCode), xelement.Value);
						break;
					case "deleteKey":
						meshBrush.deleteKey = (KeyCode)Enum.Parse(typeof(KeyCode), xelement.Value);
						break;
					case "combineAreaKey":
						meshBrush.combineKey = (KeyCode)Enum.Parse(typeof(KeyCode), xelement.Value);
						break;
					case "increaseRadiusKey":
						meshBrush.increaseRadiusKey = (KeyCode)Enum.Parse(typeof(KeyCode), xelement.Value);
						break;
					case "decreaseRadiusKey":
						meshBrush.decreaseRadiusKey = (KeyCode)Enum.Parse(typeof(KeyCode), xelement.Value);
						break;
					case "brushRadius":
						meshBrush.radius = float.Parse(xelement.Value);
						break;
					case "color":
					case "brushColor":
						meshBrush.color = new Color(float.Parse(xelement.Element("r").Value), float.Parse(xelement.Element("g").Value), float.Parse(xelement.Element("b").Value), float.Parse(xelement.Element("a").Value));
						break;
					case "useMeshDensity":
						meshBrush.useDensity = (string.CompareOrdinal(xelement.Value, "true") == 0);
						break;
					case "minMeshDensity":
						meshBrush.densityRange.x = float.Parse(xelement.Value);
						break;
					case "maxMeshDensity":
						meshBrush.densityRange.y = float.Parse(xelement.Value);
						break;
					case "minNrOfMeshes":
						meshBrush.quantityRange.x = float.Parse(xelement.Value);
						break;
					case "maxNrOfMeshes":
						meshBrush.quantityRange.y = float.Parse(xelement.Value);
						break;
					case "delay":
						meshBrush.delay = float.Parse(xelement.Value);
						break;
					case "verticalOffset":
					{
						float num3 = float.Parse(xelement.Value);
						meshBrush.offsetRange = new Vector2(num3, num3);
						break;
					}
					case "alignWithStroke":
						meshBrush.strokeAlignment = (string.CompareOrdinal(xelement.Value, "true") == 0);
						break;
					case "slopeInfluence":
					{
						float num4 = float.Parse(xelement.Value);
						meshBrush.slopeInfluenceRange = new Vector2(num4, num4);
						break;
					}
					case "useSlopeFilter":
						meshBrush.useSlopeFilter = (string.CompareOrdinal(xelement.Value, "true") == 0);
						break;
					case "maxSlopeFilterAngle":
					{
						float num5 = float.Parse(xelement.Value);
						meshBrush.angleThresholdRange = new Vector2(num5, num5);
						break;
					}
					case "inverseSlopeFilter":
						meshBrush.inverseSlopeFilter = (string.CompareOrdinal(xelement.Value, "true") == 0);
						break;
					case "manualReferenceVectorSampling":
						meshBrush.manualReferenceVectorSampling = (string.CompareOrdinal(xelement.Value, "true") == 0);
						break;
					case "showReferenceVectorInSceneGUI":
						meshBrush.showReferenceVectorInSceneView = (string.CompareOrdinal(xelement.Value, "true") == 0);
						break;
					case "slopeReferenceVector":
						meshBrush.slopeReferenceVector = new Vector3(float.Parse(xelement.Element("x").Value), float.Parse(xelement.Element("y").Value), float.Parse(xelement.Element("z").Value));
						break;
					case "slopeReferenceVector_HandleLocation":
						meshBrush.slopeReferenceVectorSampleLocation = new Vector3(float.Parse(xelement.Element("x").Value), float.Parse(xelement.Element("y").Value), float.Parse(xelement.Element("z").Value));
						break;
					case "yAxisIsTangent":
						meshBrush.yAxisTangent = (string.CompareOrdinal(xelement.Value, "true") == 0);
						break;
					case "scattering":
					{
						float num6 = float.Parse(xelement.Value);
						meshBrush.scatteringRange = new Vector2(num6, num6);
						break;
					}
					case "autoStatic":
						meshBrush.autoStatic = (string.CompareOrdinal(xelement.Value, "true") == 0);
						break;
					case "useOverlapFilter":
						meshBrush.useOverlapFilter = (string.CompareOrdinal(xelement.Value, "true") == 0);
						break;
					case "randomAbsMinDist":
						meshBrush.minimumAbsoluteDistanceRange = new Vector2(float.Parse(xelement.Element("x").Value), float.Parse(xelement.Element("y").Value));
						break;
					case "uniformScale":
						meshBrush.uniformRandomScale = (string.CompareOrdinal(xelement.Value, "true") == 0);
						break;
					case "constantUniformScale":
						meshBrush.uniformAdditiveScale = (string.CompareOrdinal(xelement.Value, "true") == 0);
						break;
					case "foldoutState_SetOfMeshesToPaint":
						meshBrush.meshesFoldout = (string.CompareOrdinal(xelement.Value, "true") == 0);
						break;
					case "foldoutState_Templates":
						meshBrush.templatesFoldout = (string.CompareOrdinal(xelement.Value, "true") == 0);
						break;
					case "foldoutState_CustomizeKeyboardShortcuts":
						meshBrush.keyBindingsFoldout = (string.CompareOrdinal(xelement.Value, "true") == 0);
						break;
					case "foldoutState_BrushSettings":
						meshBrush.brushFoldout = (string.CompareOrdinal(xelement.Value, "true") == 0);
						break;
					case "foldoutState_Slopes":
						meshBrush.slopesFoldout = (string.CompareOrdinal(xelement.Value, "true") == 0);
						break;
					case "foldoutState_Randomizers":
						meshBrush.randomizersFoldout = (string.CompareOrdinal(xelement.Value, "true") == 0);
						break;
					case "foldoutState_OverlapFilter":
						meshBrush.overlapFilterFoldout = (string.CompareOrdinal(xelement.Value, "true") == 0);
						break;
					case "foldoutState_ApplyAdditiveScale":
						meshBrush.additiveScaleFoldout = (string.CompareOrdinal(xelement.Value, "true") == 0);
						break;
					case "foldoutState_Optimize":
						meshBrush.optimizationFoldout = (string.CompareOrdinal(xelement.Value, "true") == 0);
						break;
					case "randomUniformRange":
						meshBrush.randomScaleRange = new Vector2(float.Parse(xelement.Element("x").Value), float.Parse(xelement.Element("y").Value));
						break;
					case "randomNonUniformRange":
						meshBrush.randomScaleRangeX = (meshBrush.randomScaleRangeZ = new Vector2(float.Parse(xelement.Element("x").Value), float.Parse(xelement.Element("y").Value)));
						meshBrush.randomScaleRangeY = new Vector2(float.Parse(xelement.Element("z").Value), float.Parse(xelement.Element("w").Value));
						break;
					case "constantAdditiveScale":
					{
						float num7 = float.Parse(xelement.Value);
						meshBrush.additiveScaleRange = new Vector2(num7, num7);
						break;
					}
					case "constantScaleXYZ":
						meshBrush.additiveScaleNonUniform = new Vector3(float.Parse(xelement.Element("x").Value), float.Parse(xelement.Element("y").Value), float.Parse(xelement.Element("z").Value));
						break;
					case "randomRotation":
					{
						float num8 = float.Parse(xelement.Value);
						meshBrush.randomRotationRange = new Vector2(num8, num8);
						break;
					}
					case "autoSelectOnCombine":
						meshBrush.autoSelectOnCombine = (string.CompareOrdinal(xelement.Value, "true") == 0);
						break;
					}
				}
				meshBrush.SaveTemplate(filePath.Replace(".meshbrush", "__migrated.xml"));
			}
			catch (Exception ex)
			{
				return false;
			}
			return true;
		}
	}
}
