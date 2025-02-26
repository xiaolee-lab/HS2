using System;

namespace ADV
{
	// Token: 0x02000794 RID: 1940
	public enum Command
	{
		// Token: 0x04002C03 RID: 11267
		None,
		// Token: 0x04002C04 RID: 11268
		VAR,
		// Token: 0x04002C05 RID: 11269
		RandomVar,
		// Token: 0x04002C06 RID: 11270
		Calc,
		// Token: 0x04002C07 RID: 11271
		Clamp,
		// Token: 0x04002C08 RID: 11272
		Min,
		// Token: 0x04002C09 RID: 11273
		Max,
		// Token: 0x04002C0A RID: 11274
		Lerp,
		// Token: 0x04002C0B RID: 11275
		LerpAngle,
		// Token: 0x04002C0C RID: 11276
		InverseLerp,
		// Token: 0x04002C0D RID: 11277
		LerpV3,
		// Token: 0x04002C0E RID: 11278
		LerpAngleV3,
		// Token: 0x04002C0F RID: 11279
		Tag,
		// Token: 0x04002C10 RID: 11280
		Format,
		// Token: 0x04002C11 RID: 11281
		IF,
		// Token: 0x04002C12 RID: 11282
		Switch,
		// Token: 0x04002C13 RID: 11283
		Text,
		// Token: 0x04002C14 RID: 11284
		Voice,
		// Token: 0x04002C15 RID: 11285
		Motion,
		// Token: 0x04002C16 RID: 11286
		Expression,
		// Token: 0x04002C17 RID: 11287
		ExpressionIcon,
		// Token: 0x04002C18 RID: 11288
		Open,
		// Token: 0x04002C19 RID: 11289
		Close,
		// Token: 0x04002C1A RID: 11290
		Jump,
		// Token: 0x04002C1B RID: 11291
		Choice,
		// Token: 0x04002C1C RID: 11292
		Wait,
		// Token: 0x04002C1D RID: 11293
		TextClear,
		// Token: 0x04002C1E RID: 11294
		FontColor,
		// Token: 0x04002C1F RID: 11295
		WindowActive,
		// Token: 0x04002C20 RID: 11296
		WindowImage,
		// Token: 0x04002C21 RID: 11297
		Scene,
		// Token: 0x04002C22 RID: 11298
		Regulate,
		// Token: 0x04002C23 RID: 11299
		Replace,
		// Token: 0x04002C24 RID: 11300
		Reset,
		// Token: 0x04002C25 RID: 11301
		Vector,
		// Token: 0x04002C26 RID: 11302
		NullLoad,
		// Token: 0x04002C27 RID: 11303
		NullRelease,
		// Token: 0x04002C28 RID: 11304
		NullSet,
		// Token: 0x04002C29 RID: 11305
		BGLoad,
		// Token: 0x04002C2A RID: 11306
		BGRelease,
		// Token: 0x04002C2B RID: 11307
		BGVisible,
		// Token: 0x04002C2C RID: 11308
		InfoAudioEco,
		// Token: 0x04002C2D RID: 11309
		InfoAnimePlay,
		// Token: 0x04002C2E RID: 11310
		Fade,
		// Token: 0x04002C2F RID: 11311
		SceneFade,
		// Token: 0x04002C30 RID: 11312
		SceneFadeRegulate,
		// Token: 0x04002C31 RID: 11313
		FadeWait,
		// Token: 0x04002C32 RID: 11314
		FilterImageLoad,
		// Token: 0x04002C33 RID: 11315
		FilterImageRelease,
		// Token: 0x04002C34 RID: 11316
		FadeSet,
		// Token: 0x04002C35 RID: 11317
		FilterSet,
		// Token: 0x04002C36 RID: 11318
		Filter,
		// Token: 0x04002C37 RID: 11319
		ImageLoad,
		// Token: 0x04002C38 RID: 11320
		ImageRelease,
		// Token: 0x04002C39 RID: 11321
		EjaculationEffect,
		// Token: 0x04002C3A RID: 11322
		EcstacyEffect,
		// Token: 0x04002C3B RID: 11323
		EcstacySyncEffect,
		// Token: 0x04002C3C RID: 11324
		CrossFade,
		// Token: 0x04002C3D RID: 11325
		BlurEffect,
		// Token: 0x04002C3E RID: 11326
		DOFTarget,
		// Token: 0x04002C3F RID: 11327
		SepiaEffect,
		// Token: 0x04002C40 RID: 11328
		CameraActive,
		// Token: 0x04002C41 RID: 11329
		CameraAspect,
		// Token: 0x04002C42 RID: 11330
		CameraChange,
		// Token: 0x04002C43 RID: 11331
		CameraDirectionAdd,
		// Token: 0x04002C44 RID: 11332
		CameraDirectionSet,
		// Token: 0x04002C45 RID: 11333
		CameraLerpNullMove,
		// Token: 0x04002C46 RID: 11334
		CameraLerpNullSet,
		// Token: 0x04002C47 RID: 11335
		CameraLerpAdd,
		// Token: 0x04002C48 RID: 11336
		CameraLerpSet,
		// Token: 0x04002C49 RID: 11337
		CameraLerpAnimationAdd,
		// Token: 0x04002C4A RID: 11338
		CameraLerpAnimationSet,
		// Token: 0x04002C4B RID: 11339
		CameraLerpTargetAdd,
		// Token: 0x04002C4C RID: 11340
		CameraLerpTargetSet,
		// Token: 0x04002C4D RID: 11341
		CameraPositionAdd,
		// Token: 0x04002C4E RID: 11342
		CameraPositionSet,
		// Token: 0x04002C4F RID: 11343
		CameraRotationAdd,
		// Token: 0x04002C50 RID: 11344
		CameraRotationSet,
		// Token: 0x04002C51 RID: 11345
		CameraDefault,
		// Token: 0x04002C52 RID: 11346
		CameraParent,
		// Token: 0x04002C53 RID: 11347
		CameraNull,
		// Token: 0x04002C54 RID: 11348
		CameraTarget,
		// Token: 0x04002C55 RID: 11349
		CameraTargetFront,
		// Token: 0x04002C56 RID: 11350
		CameraTargetCharacter,
		// Token: 0x04002C57 RID: 11351
		CameraReset,
		// Token: 0x04002C58 RID: 11352
		CameraLock,
		// Token: 0x04002C59 RID: 11353
		CameraGetFov,
		// Token: 0x04002C5A RID: 11354
		CameraSetFov,
		// Token: 0x04002C5B RID: 11355
		BGMPlay,
		// Token: 0x04002C5C RID: 11356
		BGMStop,
		// Token: 0x04002C5D RID: 11357
		EnvPlay,
		// Token: 0x04002C5E RID: 11358
		EnvStop,
		// Token: 0x04002C5F RID: 11359
		SE2DPlay,
		// Token: 0x04002C60 RID: 11360
		SE2DStop,
		// Token: 0x04002C61 RID: 11361
		SE3DPlay,
		// Token: 0x04002C62 RID: 11362
		SE3DStop,
		// Token: 0x04002C63 RID: 11363
		CharaCreate,
		// Token: 0x04002C64 RID: 11364
		CharaFixCreate,
		// Token: 0x04002C65 RID: 11365
		CharaMobCreate,
		// Token: 0x04002C66 RID: 11366
		CharaDelete,
		// Token: 0x04002C67 RID: 11367
		CharaStand,
		// Token: 0x04002C68 RID: 11368
		CharaStandFind,
		// Token: 0x04002C69 RID: 11369
		CharaPositionAdd,
		// Token: 0x04002C6A RID: 11370
		CharaPositionSet,
		// Token: 0x04002C6B RID: 11371
		CharaPositionLocalAdd,
		// Token: 0x04002C6C RID: 11372
		CharaPositionLocalSet,
		// Token: 0x04002C6D RID: 11373
		CharaMotion,
		// Token: 0x04002C6E RID: 11374
		CharaMotionDefault,
		// Token: 0x04002C6F RID: 11375
		CharaMotionWait,
		// Token: 0x04002C70 RID: 11376
		CharaMotionLayerWeight,
		// Token: 0x04002C71 RID: 11377
		CharaMotionSetParam,
		// Token: 0x04002C72 RID: 11378
		CharaMotionIKSetPartner,
		// Token: 0x04002C73 RID: 11379
		CharaExpression,
		// Token: 0x04002C74 RID: 11380
		CharaFixEyes,
		// Token: 0x04002C75 RID: 11381
		CharaFixMouth,
		// Token: 0x04002C76 RID: 11382
		CharaExpressionIcon,
		// Token: 0x04002C77 RID: 11383
		CharaGetShape,
		// Token: 0x04002C78 RID: 11384
		CharaCoordinate,
		// Token: 0x04002C79 RID: 11385
		CharaClothState,
		// Token: 0x04002C7A RID: 11386
		CharaSiruState,
		// Token: 0x04002C7B RID: 11387
		CharaVoicePlay,
		// Token: 0x04002C7C RID: 11388
		CharaVoiceStop,
		// Token: 0x04002C7D RID: 11389
		CharaVoiceStopAll,
		// Token: 0x04002C7E RID: 11390
		CharaVoiceWait,
		// Token: 0x04002C7F RID: 11391
		CharaVoiceWaitAll,
		// Token: 0x04002C80 RID: 11392
		CharaLookEyes,
		// Token: 0x04002C81 RID: 11393
		CharaLookEyesTarget,
		// Token: 0x04002C82 RID: 11394
		CharaLookEyesTargetChara,
		// Token: 0x04002C83 RID: 11395
		CharaLookNeck,
		// Token: 0x04002C84 RID: 11396
		CharaLookNeckTarget,
		// Token: 0x04002C85 RID: 11397
		CharaLookNeckTargetChara,
		// Token: 0x04002C86 RID: 11398
		CharaLookNeckSkip,
		// Token: 0x04002C87 RID: 11399
		CharaItemCreate,
		// Token: 0x04002C88 RID: 11400
		CharaItemDelete,
		// Token: 0x04002C89 RID: 11401
		CharaItemAnime,
		// Token: 0x04002C8A RID: 11402
		CharaItemFind,
		// Token: 0x04002C8B RID: 11403
		EventCGSetting,
		// Token: 0x04002C8C RID: 11404
		EventCGRelease,
		// Token: 0x04002C8D RID: 11405
		EventCGNext,
		// Token: 0x04002C8E RID: 11406
		MotionLoad,
		// Token: 0x04002C8F RID: 11407
		ExpLoad,
		// Token: 0x04002C90 RID: 11408
		Chara2DCreate,
		// Token: 0x04002C91 RID: 11409
		Chara2DDelete,
		// Token: 0x04002C92 RID: 11410
		Chara2DStand,
		// Token: 0x04002C93 RID: 11411
		Chara2DPositionAdd,
		// Token: 0x04002C94 RID: 11412
		Chara2DPositionSet,
		// Token: 0x04002C95 RID: 11413
		ObjectCreate,
		// Token: 0x04002C96 RID: 11414
		ObjectLoad,
		// Token: 0x04002C97 RID: 11415
		ObjectDelete,
		// Token: 0x04002C98 RID: 11416
		ObjectPosition,
		// Token: 0x04002C99 RID: 11417
		ObjectRotation,
		// Token: 0x04002C9A RID: 11418
		ObjectScale,
		// Token: 0x04002C9B RID: 11419
		ObjectParent,
		// Token: 0x04002C9C RID: 11420
		ObjectComponent,
		// Token: 0x04002C9D RID: 11421
		ObjectAnimeParam,
		// Token: 0x04002C9E RID: 11422
		MoviePlay,
		// Token: 0x04002C9F RID: 11423
		CharaActive,
		// Token: 0x04002CA0 RID: 11424
		CharaVisible,
		// Token: 0x04002CA1 RID: 11425
		CharaColor,
		// Token: 0x04002CA2 RID: 11426
		CharaParam,
		// Token: 0x04002CA3 RID: 11427
		CharaChange,
		// Token: 0x04002CA4 RID: 11428
		CharaNameGet,
		// Token: 0x04002CA5 RID: 11429
		CharaEvent,
		// Token: 0x04002CA6 RID: 11430
		HeroineCallNameChange,
		// Token: 0x04002CA7 RID: 11431
		HeroineFinCG,
		// Token: 0x04002CA8 RID: 11432
		HeroineParam,
		// Token: 0x04002CA9 RID: 11433
		PlayerParam,
		// Token: 0x04002CAA RID: 11434
		CycleChange,
		// Token: 0x04002CAB RID: 11435
		WeekChange,
		// Token: 0x04002CAC RID: 11436
		MapChange,
		// Token: 0x04002CAD RID: 11437
		MapUnload,
		// Token: 0x04002CAE RID: 11438
		MapVisible,
		// Token: 0x04002CAF RID: 11439
		MapObjectActive,
		// Token: 0x04002CB0 RID: 11440
		DayTimeChange,
		// Token: 0x04002CB1 RID: 11441
		GetGatePos,
		// Token: 0x04002CB2 RID: 11442
		CameraLookAt,
		// Token: 0x04002CB3 RID: 11443
		MozVisible,
		// Token: 0x04002CB4 RID: 11444
		LookAtDankonAdd,
		// Token: 0x04002CB5 RID: 11445
		LookAtDankonRemove,
		// Token: 0x04002CB6 RID: 11446
		HMotionShakeAdd,
		// Token: 0x04002CB7 RID: 11447
		HMotionShakeRemove,
		// Token: 0x04002CB8 RID: 11448
		HitReaction,
		// Token: 0x04002CB9 RID: 11449
		AddPosture,
		// Token: 0x04002CBA RID: 11450
		AddCollider,
		// Token: 0x04002CBB RID: 11451
		ColliderSetActive,
		// Token: 0x04002CBC RID: 11452
		AddNavMeshAgent,
		// Token: 0x04002CBD RID: 11453
		NavMeshAgentSetActive,
		// Token: 0x04002CBE RID: 11454
		BundleCheck,
		// Token: 0x04002CBF RID: 11455
		CharaPersonal,
		// Token: 0x04002CC0 RID: 11456
		HNamaOK,
		// Token: 0x04002CC1 RID: 11457
		HNamaNG,
		// Token: 0x04002CC2 RID: 11458
		CameraShakePos,
		// Token: 0x04002CC3 RID: 11459
		CameraShakeRot,
		// Token: 0x04002CC4 RID: 11460
		Prob,
		// Token: 0x04002CC5 RID: 11461
		Probs,
		// Token: 0x04002CC6 RID: 11462
		FormatVAR,
		// Token: 0x04002CC7 RID: 11463
		CharaKaraokePlay,
		// Token: 0x04002CC8 RID: 11464
		CharaKaraokeStop,
		// Token: 0x04002CC9 RID: 11465
		Task,
		// Token: 0x04002CCA RID: 11466
		TaskWait,
		// Token: 0x04002CCB RID: 11467
		TaskEnd,
		// Token: 0x04002CCC RID: 11468
		ParameterFile,
		// Token: 0x04002CCD RID: 11469
		Log,
		// Token: 0x04002CCE RID: 11470
		HSafeDaySet,
		// Token: 0x04002CCF RID: 11471
		HDangerDaySet,
		// Token: 0x04002CD0 RID: 11472
		HeroineWeddingInfo,
		// Token: 0x04002CD1 RID: 11473
		CameraLightOffset,
		// Token: 0x04002CD2 RID: 11474
		CharaSetShape,
		// Token: 0x04002CD3 RID: 11475
		CharaCoordinateChange,
		// Token: 0x04002CD4 RID: 11476
		CharaShoesChange,
		// Token: 0x04002CD5 RID: 11477
		CameraAnimeLoad,
		// Token: 0x04002CD6 RID: 11478
		CameraAnimePlay,
		// Token: 0x04002CD7 RID: 11479
		CameraAnimeWait,
		// Token: 0x04002CD8 RID: 11480
		CameraAnimeLayerWeight,
		// Token: 0x04002CD9 RID: 11481
		CameraAnimeSetParam,
		// Token: 0x04002CDA RID: 11482
		CameraAnimeRelease,
		// Token: 0x04002CDB RID: 11483
		CameraLightActive,
		// Token: 0x04002CDC RID: 11484
		CameraLightAngle,
		// Token: 0x04002CDD RID: 11485
		InfoAudio,
		// Token: 0x04002CDE RID: 11486
		CharaCreateEmpty,
		// Token: 0x04002CDF RID: 11487
		CharaCreateDummy,
		// Token: 0x04002CE0 RID: 11488
		CharaFixCreateDummy,
		// Token: 0x04002CE1 RID: 11489
		CharaMobCreateDummy,
		// Token: 0x04002CE2 RID: 11490
		ReplaceLanguage,
		// Token: 0x04002CE3 RID: 11491
		InfoText,
		// Token: 0x04002CE4 RID: 11492
		SendCommandData,
		// Token: 0x04002CE5 RID: 11493
		SendCommandDataList,
		// Token: 0x04002CE6 RID: 11494
		PlaySystemSE,
		// Token: 0x04002CE7 RID: 11495
		PlayActionSE,
		// Token: 0x04002CE8 RID: 11496
		PlayEnviroSE,
		// Token: 0x04002CE9 RID: 11497
		PlayFootStepSE,
		// Token: 0x04002CEA RID: 11498
		InventoryCheck,
		// Token: 0x04002CEB RID: 11499
		SetPresent,
		// Token: 0x04002CEC RID: 11500
		SetPresentBirthday,
		// Token: 0x04002CED RID: 11501
		ClearItems,
		// Token: 0x04002CEE RID: 11502
		AddItem,
		// Token: 0x04002CEF RID: 11503
		RemoveItem,
		// Token: 0x04002CF0 RID: 11504
		ChangeADVFixedAngleCamera,
		// Token: 0x04002CF1 RID: 11505
		InventoryGiveItem,
		// Token: 0x04002CF2 RID: 11506
		SetItemScrounge,
		// Token: 0x04002CF3 RID: 11507
		CharaSetting,
		// Token: 0x04002CF4 RID: 11508
		AddItemInPlayer,
		// Token: 0x04002CF5 RID: 11509
		SetItemRandomEvent
	}
}
