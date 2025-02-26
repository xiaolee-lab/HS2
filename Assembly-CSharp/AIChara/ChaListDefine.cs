using System;

namespace AIChara
{
	// Token: 0x0200080F RID: 2063
	public static class ChaListDefine
	{
		// Token: 0x06003464 RID: 13412 RVA: 0x00134760 File Offset: 0x00132B60
		public static string GetCategoryName(int no)
		{
			switch (no)
			{
			case 300:
				return "後ろ髪";
			case 301:
				return "前髪";
			case 302:
				return "横髪";
			case 303:
				return "エクステ";
			default:
				switch (no)
				{
				case 348:
					return "パターンテクスチャ";
				default:
					switch (no)
					{
					case 0:
						return "キャラサンプル男";
					case 1:
						return "キャラサンプル女";
					default:
						switch (no)
						{
						case 240:
							return "女服上";
						case 241:
							return "女服下";
						case 242:
							return "女インナー上";
						case 243:
							return "女インナー下";
						case 244:
							return "女手袋";
						case 245:
							return "女パンスト";
						case 246:
							return "女靴下";
						case 247:
							return "女靴";
						default:
							switch (no)
							{
							case 500:
								return "カスタム男ポーズ";
							case 501:
								return "カスタム女ポーズ";
							case 502:
								return "カスタム男眉パターン";
							case 503:
								return "カスタム女眉パターン";
							case 504:
								return "カスタム男目パターン";
							case 505:
								return "カスタム女目パターン";
							case 506:
								return "カスタム男口パターン";
							case 507:
								return "カスタム女口パターン";
							default:
								switch (no)
								{
								case 140:
									return "男服上";
								case 141:
									return "男服下";
								default:
									switch (no)
									{
									case 110:
										return "男頭";
									case 111:
										return "男肌";
									case 112:
										return "男シワ";
									default:
										switch (no)
										{
										case 131:
											return "男肌";
										case 132:
											return "男肉感";
										case 133:
											return "男日焼け";
										default:
											switch (no)
											{
											case 210:
												return "女頭";
											case 211:
												return "女肌";
											case 212:
												return "女シワ";
											default:
												switch (no)
												{
												case 231:
													return "女肌";
												case 232:
													return "女肉感";
												case 233:
													return "女日焼け";
												default:
													switch (no)
													{
													case 334:
														return "乳首";
													case 335:
														return "アンダーヘア";
													case 336:
														return "肌のカラープリセット";
													default:
														if (no != 121)
														{
															return string.Empty;
														}
														return "男ヒゲ";
													}
													break;
												}
												break;
											}
											break;
										}
										break;
									}
									break;
								case 144:
									return "男手袋";
								case 147:
									return "男靴";
								}
								break;
							}
							break;
						}
						break;
					case 3:
						return "タイプ別初期値";
					case 4:
						return "願望設定初期値";
					case 5:
						return "サンプル音声";
					case 6:
						return "ホクロの配置設定";
					case 7:
						return "フェイスペイントの配置設定";
					case 8:
						return "ボディーペイントの配置設定";
					}
					break;
				case 350:
					return "アクセサリ(なし)";
				case 351:
					return "アクセサリ頭";
				case 352:
					return "アクセサリ耳";
				case 353:
					return "アクセサリ眼鏡";
				case 354:
					return "アクセサリ顔";
				case 355:
					return "アクセサリ首";
				case 356:
					return "アクセサリ肩";
				case 357:
					return "アクセサリ胸";
				case 358:
					return "アクセサリ腰";
				case 359:
					return "アクセサリ背中";
				case 360:
					return "アクセサリ腕";
				case 361:
					return "アクセサリ手";
				case 362:
					return "アクセサリ脚";
				case 363:
					return "アクセサリ股間";
				}
				break;
			case 305:
				return "髪の毛のカラープリセット";
			case 306:
				return "髪メッシュパターン";
			case 313:
				return "ペイント顔・体";
			case 314:
				return "眉毛";
			case 315:
				return "睫毛";
			case 316:
				return "アイシャドウ";
			case 317:
				return "瞳";
			case 318:
				return "瞳孔";
			case 319:
				return "ハイライト";
			case 320:
				return "チーク";
			case 322:
				return "リップ";
			case 323:
				return "ホクロ";
			}
		}

		// Token: 0x04003435 RID: 13365
		public static readonly Version CheckItemVersion = new Version("0.0.0");

		// Token: 0x04003436 RID: 13366
		public static readonly string CheckItemFile = "save/checkitem.dat";

		// Token: 0x02000810 RID: 2064
		public enum CategoryNo
		{
			// Token: 0x04003438 RID: 13368
			unknown = -1,
			// Token: 0x04003439 RID: 13369
			cha_sample_m,
			// Token: 0x0400343A RID: 13370
			cha_sample_f,
			// Token: 0x0400343B RID: 13371
			init_type_param = 3,
			// Token: 0x0400343C RID: 13372
			init_wish_param,
			// Token: 0x0400343D RID: 13373
			cha_sample_voice,
			// Token: 0x0400343E RID: 13374
			mole_layout,
			// Token: 0x0400343F RID: 13375
			facepaint_layout,
			// Token: 0x04003440 RID: 13376
			bodypaint_layout,
			// Token: 0x04003441 RID: 13377
			mo_head = 110,
			// Token: 0x04003442 RID: 13378
			mt_skin_f,
			// Token: 0x04003443 RID: 13379
			mt_detail_f,
			// Token: 0x04003444 RID: 13380
			mt_beard = 121,
			// Token: 0x04003445 RID: 13381
			mt_skin_b = 131,
			// Token: 0x04003446 RID: 13382
			mt_detail_b,
			// Token: 0x04003447 RID: 13383
			mt_sunburn,
			// Token: 0x04003448 RID: 13384
			mo_top = 140,
			// Token: 0x04003449 RID: 13385
			mo_bot,
			// Token: 0x0400344A RID: 13386
			mo_gloves = 144,
			// Token: 0x0400344B RID: 13387
			mo_shoes = 147,
			// Token: 0x0400344C RID: 13388
			fo_head = 210,
			// Token: 0x0400344D RID: 13389
			ft_skin_f,
			// Token: 0x0400344E RID: 13390
			ft_detail_f,
			// Token: 0x0400344F RID: 13391
			ft_skin_b = 231,
			// Token: 0x04003450 RID: 13392
			ft_detail_b,
			// Token: 0x04003451 RID: 13393
			ft_sunburn,
			// Token: 0x04003452 RID: 13394
			fo_top = 240,
			// Token: 0x04003453 RID: 13395
			fo_bot,
			// Token: 0x04003454 RID: 13396
			fo_inner_t,
			// Token: 0x04003455 RID: 13397
			fo_inner_b,
			// Token: 0x04003456 RID: 13398
			fo_gloves,
			// Token: 0x04003457 RID: 13399
			fo_panst,
			// Token: 0x04003458 RID: 13400
			fo_socks,
			// Token: 0x04003459 RID: 13401
			fo_shoes,
			// Token: 0x0400345A RID: 13402
			so_hair_b = 300,
			// Token: 0x0400345B RID: 13403
			so_hair_f,
			// Token: 0x0400345C RID: 13404
			so_hair_s,
			// Token: 0x0400345D RID: 13405
			so_hair_o,
			// Token: 0x0400345E RID: 13406
			preset_hair_color = 305,
			// Token: 0x0400345F RID: 13407
			st_hairmeshptn,
			// Token: 0x04003460 RID: 13408
			st_paint = 313,
			// Token: 0x04003461 RID: 13409
			st_eyebrow,
			// Token: 0x04003462 RID: 13410
			st_eyelash,
			// Token: 0x04003463 RID: 13411
			st_eyeshadow,
			// Token: 0x04003464 RID: 13412
			st_eye,
			// Token: 0x04003465 RID: 13413
			st_eyeblack,
			// Token: 0x04003466 RID: 13414
			st_eye_hl,
			// Token: 0x04003467 RID: 13415
			st_cheek,
			// Token: 0x04003468 RID: 13416
			st_lip = 322,
			// Token: 0x04003469 RID: 13417
			st_mole,
			// Token: 0x0400346A RID: 13418
			st_nip = 334,
			// Token: 0x0400346B RID: 13419
			st_underhair,
			// Token: 0x0400346C RID: 13420
			preset_skin_color,
			// Token: 0x0400346D RID: 13421
			st_pattern = 348,
			// Token: 0x0400346E RID: 13422
			ao_none = 350,
			// Token: 0x0400346F RID: 13423
			ao_head,
			// Token: 0x04003470 RID: 13424
			ao_ear,
			// Token: 0x04003471 RID: 13425
			ao_glasses,
			// Token: 0x04003472 RID: 13426
			ao_face,
			// Token: 0x04003473 RID: 13427
			ao_neck,
			// Token: 0x04003474 RID: 13428
			ao_shoulder,
			// Token: 0x04003475 RID: 13429
			ao_chest,
			// Token: 0x04003476 RID: 13430
			ao_waist,
			// Token: 0x04003477 RID: 13431
			ao_back,
			// Token: 0x04003478 RID: 13432
			ao_arm,
			// Token: 0x04003479 RID: 13433
			ao_hand,
			// Token: 0x0400347A RID: 13434
			ao_leg,
			// Token: 0x0400347B RID: 13435
			ao_kokan,
			// Token: 0x0400347C RID: 13436
			custom_pose_m = 500,
			// Token: 0x0400347D RID: 13437
			custom_pose_f,
			// Token: 0x0400347E RID: 13438
			custom_eyebrow_m,
			// Token: 0x0400347F RID: 13439
			custom_eyebrow_f,
			// Token: 0x04003480 RID: 13440
			custom_eye_m,
			// Token: 0x04003481 RID: 13441
			custom_eye_f,
			// Token: 0x04003482 RID: 13442
			custom_mouth_m,
			// Token: 0x04003483 RID: 13443
			custom_mouth_f
		}

		// Token: 0x02000811 RID: 2065
		public enum KeyType
		{
			// Token: 0x04003485 RID: 13445
			Unknown = -1,
			// Token: 0x04003486 RID: 13446
			ListIndex,
			// Token: 0x04003487 RID: 13447
			Category,
			// Token: 0x04003488 RID: 13448
			DistributionNo,
			// Token: 0x04003489 RID: 13449
			ID,
			// Token: 0x0400348A RID: 13450
			Kind,
			// Token: 0x0400348B RID: 13451
			Possess,
			// Token: 0x0400348C RID: 13452
			Name,
			// Token: 0x0400348D RID: 13453
			EN_US,
			// Token: 0x0400348E RID: 13454
			ZH_CN,
			// Token: 0x0400348F RID: 13455
			ZH_TW,
			// Token: 0x04003490 RID: 13456
			JA_JP_PT,
			// Token: 0x04003491 RID: 13457
			EN_US_PT,
			// Token: 0x04003492 RID: 13458
			ZH_CN_PT,
			// Token: 0x04003493 RID: 13459
			ZH_TW_PT,
			// Token: 0x04003494 RID: 13460
			MainManifest,
			// Token: 0x04003495 RID: 13461
			MainAB,
			// Token: 0x04003496 RID: 13462
			MainData,
			// Token: 0x04003497 RID: 13463
			MainData02,
			// Token: 0x04003498 RID: 13464
			Weights,
			// Token: 0x04003499 RID: 13465
			RingOff,
			// Token: 0x0400349A RID: 13466
			AddScale,
			// Token: 0x0400349B RID: 13467
			AddTex,
			// Token: 0x0400349C RID: 13468
			CenterScale,
			// Token: 0x0400349D RID: 13469
			CenterX,
			// Token: 0x0400349E RID: 13470
			CenterY,
			// Token: 0x0400349F RID: 13471
			ColorMask02Tex,
			// Token: 0x040034A0 RID: 13472
			ColorMask03Tex,
			// Token: 0x040034A1 RID: 13473
			ColorMaskTex,
			// Token: 0x040034A2 RID: 13474
			Coordinate,
			// Token: 0x040034A3 RID: 13475
			Data01,
			// Token: 0x040034A4 RID: 13476
			Data02,
			// Token: 0x040034A5 RID: 13477
			Data03,
			// Token: 0x040034A6 RID: 13478
			Eye01,
			// Token: 0x040034A7 RID: 13479
			Eye02,
			// Token: 0x040034A8 RID: 13480
			Eye03,
			// Token: 0x040034A9 RID: 13481
			EyeMax01,
			// Token: 0x040034AA RID: 13482
			EyeMax02,
			// Token: 0x040034AB RID: 13483
			EyeMax03,
			// Token: 0x040034AC RID: 13484
			Eyebrow01,
			// Token: 0x040034AD RID: 13485
			Eyebrow02,
			// Token: 0x040034AE RID: 13486
			Eyebrow03,
			// Token: 0x040034AF RID: 13487
			EyeHiLight01,
			// Token: 0x040034B0 RID: 13488
			EyeHiLight02,
			// Token: 0x040034B1 RID: 13489
			EyeHiLight03,
			// Token: 0x040034B2 RID: 13490
			GlossTex,
			// Token: 0x040034B3 RID: 13491
			HeadID,
			// Token: 0x040034B4 RID: 13492
			KokanHide,
			// Token: 0x040034B5 RID: 13493
			MainTex,
			// Token: 0x040034B6 RID: 13494
			MainTex02,
			// Token: 0x040034B7 RID: 13495
			MainTex03,
			// Token: 0x040034B8 RID: 13496
			MainTexAB,
			// Token: 0x040034B9 RID: 13497
			MatData,
			// Token: 0x040034BA RID: 13498
			Mouth01,
			// Token: 0x040034BB RID: 13499
			Mouth02,
			// Token: 0x040034BC RID: 13500
			Mouth03,
			// Token: 0x040034BD RID: 13501
			MouthMax01,
			// Token: 0x040034BE RID: 13502
			MouthMax02,
			// Token: 0x040034BF RID: 13503
			MouthMax03,
			// Token: 0x040034C0 RID: 13504
			MoveX,
			// Token: 0x040034C1 RID: 13505
			MoveY,
			// Token: 0x040034C2 RID: 13506
			NormalMapTex,
			// Token: 0x040034C3 RID: 13507
			NotBra,
			// Token: 0x040034C4 RID: 13508
			OcclusionMapTex,
			// Token: 0x040034C5 RID: 13509
			OverBodyMask,
			// Token: 0x040034C6 RID: 13510
			OverBodyMaskAB,
			// Token: 0x040034C7 RID: 13511
			OverBraMask,
			// Token: 0x040034C8 RID: 13512
			OverBraMaskAB,
			// Token: 0x040034C9 RID: 13513
			BreakDisableMask,
			// Token: 0x040034CA RID: 13514
			OverInnerTBMask,
			// Token: 0x040034CB RID: 13515
			OverInnerTBMaskAB,
			// Token: 0x040034CC RID: 13516
			OverInnerBMask,
			// Token: 0x040034CD RID: 13517
			OverInnerBMaskAB,
			// Token: 0x040034CE RID: 13518
			OverPanstMask,
			// Token: 0x040034CF RID: 13519
			OverPanstMaskAB,
			// Token: 0x040034D0 RID: 13520
			OverBodyBMask,
			// Token: 0x040034D1 RID: 13521
			OverBodyBMaskAB,
			// Token: 0x040034D2 RID: 13522
			Parent,
			// Token: 0x040034D3 RID: 13523
			PosX,
			// Token: 0x040034D4 RID: 13524
			PosY,
			// Token: 0x040034D5 RID: 13525
			Preset,
			// Token: 0x040034D6 RID: 13526
			Rot,
			// Token: 0x040034D7 RID: 13527
			Scale,
			// Token: 0x040034D8 RID: 13528
			SetHair,
			// Token: 0x040034D9 RID: 13529
			ShapeAnime,
			// Token: 0x040034DA RID: 13530
			StateType,
			// Token: 0x040034DB RID: 13531
			ThumbAB,
			// Token: 0x040034DC RID: 13532
			ThumbTex,
			// Token: 0x040034DD RID: 13533
			Clip,
			// Token: 0x040034DE RID: 13534
			OverBraType,
			// Token: 0x040034DF RID: 13535
			Pattern,
			// Token: 0x040034E0 RID: 13536
			Target,
			// Token: 0x040034E1 RID: 13537
			Correct,
			// Token: 0x040034E2 RID: 13538
			TempLow,
			// Token: 0x040034E3 RID: 13539
			TempUp,
			// Token: 0x040034E4 RID: 13540
			MoodLow,
			// Token: 0x040034E5 RID: 13541
			MoodUp,
			// Token: 0x040034E6 RID: 13542
			TexManifest,
			// Token: 0x040034E7 RID: 13543
			TexAB,
			// Token: 0x040034E8 RID: 13544
			TexD,
			// Token: 0x040034E9 RID: 13545
			TexC,
			// Token: 0x040034EA RID: 13546
			FS_00,
			// Token: 0x040034EB RID: 13547
			FS_01,
			// Token: 0x040034EC RID: 13548
			FS_02,
			// Token: 0x040034ED RID: 13549
			FS_03,
			// Token: 0x040034EE RID: 13550
			FS_04,
			// Token: 0x040034EF RID: 13551
			FS_05,
			// Token: 0x040034F0 RID: 13552
			FS_06,
			// Token: 0x040034F1 RID: 13553
			FS_07,
			// Token: 0x040034F2 RID: 13554
			DD_00,
			// Token: 0x040034F3 RID: 13555
			DD_01,
			// Token: 0x040034F4 RID: 13556
			DD_02,
			// Token: 0x040034F5 RID: 13557
			DD_03,
			// Token: 0x040034F6 RID: 13558
			DD_04,
			// Token: 0x040034F7 RID: 13559
			DD_05,
			// Token: 0x040034F8 RID: 13560
			DD_06,
			// Token: 0x040034F9 RID: 13561
			DD_07,
			// Token: 0x040034FA RID: 13562
			DD_08,
			// Token: 0x040034FB RID: 13563
			DD_09,
			// Token: 0x040034FC RID: 13564
			DD_10,
			// Token: 0x040034FD RID: 13565
			DD_11,
			// Token: 0x040034FE RID: 13566
			DD_12,
			// Token: 0x040034FF RID: 13567
			DD_13,
			// Token: 0x04003500 RID: 13568
			DD_14,
			// Token: 0x04003501 RID: 13569
			DD_15,
			// Token: 0x04003502 RID: 13570
			DB_00,
			// Token: 0x04003503 RID: 13571
			DB_01,
			// Token: 0x04003504 RID: 13572
			DB_02,
			// Token: 0x04003505 RID: 13573
			DB_03,
			// Token: 0x04003506 RID: 13574
			DB_04,
			// Token: 0x04003507 RID: 13575
			DB_05,
			// Token: 0x04003508 RID: 13576
			DB_06,
			// Token: 0x04003509 RID: 13577
			DB_07,
			// Token: 0x0400350A RID: 13578
			DB_08,
			// Token: 0x0400350B RID: 13579
			DB_09,
			// Token: 0x0400350C RID: 13580
			DB_10,
			// Token: 0x0400350D RID: 13581
			DB_11,
			// Token: 0x0400350E RID: 13582
			DB_12,
			// Token: 0x0400350F RID: 13583
			DB_13,
			// Token: 0x04003510 RID: 13584
			DB_14,
			// Token: 0x04003511 RID: 13585
			DB_15,
			// Token: 0x04003512 RID: 13586
			Motivation,
			// Token: 0x04003513 RID: 13587
			Immoral,
			// Token: 0x04003514 RID: 13588
			SampleH,
			// Token: 0x04003515 RID: 13589
			SampleS,
			// Token: 0x04003516 RID: 13590
			SampleV,
			// Token: 0x04003517 RID: 13591
			TopH,
			// Token: 0x04003518 RID: 13592
			TopS,
			// Token: 0x04003519 RID: 13593
			TopV,
			// Token: 0x0400351A RID: 13594
			BaseH,
			// Token: 0x0400351B RID: 13595
			BaseS,
			// Token: 0x0400351C RID: 13596
			BaseV,
			// Token: 0x0400351D RID: 13597
			UnderH,
			// Token: 0x0400351E RID: 13598
			UnderS,
			// Token: 0x0400351F RID: 13599
			UnderV,
			// Token: 0x04003520 RID: 13600
			SpecularH,
			// Token: 0x04003521 RID: 13601
			SpecularS,
			// Token: 0x04003522 RID: 13602
			SpecularV,
			// Token: 0x04003523 RID: 13603
			Metallic,
			// Token: 0x04003524 RID: 13604
			Smoothness,
			// Token: 0x04003525 RID: 13605
			IKAB,
			// Token: 0x04003526 RID: 13606
			IKData
		}
	}
}
