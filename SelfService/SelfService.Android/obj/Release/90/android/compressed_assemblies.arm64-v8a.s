	.arch	armv8-a
	.file	"compressed_assemblies.arm64-v8a.arm64-v8a.s"
	.include	"compressed_assemblies.arm64-v8a-data.inc"

	.section	.data.compressed_assembly_descriptors,"aw",@progbits
	.type	.L.compressed_assembly_descriptors, @object
	.p2align	3
.L.compressed_assembly_descriptors:
	/* 0: Bolts.AppLinks.dll */
	/* uncompressed_file_size */
	.word	4608
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_0

	/* 1: Bolts.Tasks.dll */
	/* uncompressed_file_size */
	.word	4096
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_1

	/* 2: Correios.dll */
	/* uncompressed_file_size */
	.word	80384
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_2

	/* 3: FastAndroidCamera.dll */
	/* uncompressed_file_size */
	.word	10752
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_3

	/* 4: FormsViewGroup.dll */
	/* uncompressed_file_size */
	.word	13312
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_4

	/* 5: Google.ZXing.Core.dll */
	/* uncompressed_file_size */
	.word	56832
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_5

	/* 6: Java.Interop.dll */
	/* uncompressed_file_size */
	.word	164352
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_6

	/* 7: Microsoft.CSharp.dll */
	/* uncompressed_file_size */
	.word	300032
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_7

	/* 8: Mono.Android.dll */
	/* uncompressed_file_size */
	.word	2165760
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_8

	/* 9: Mono.Security.dll */
	/* uncompressed_file_size */
	.word	121856
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_9

	/* 10: Newtonsoft.Json.dll */
	/* uncompressed_file_size */
	.word	348672
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_10

	/* 11: Plugin.CurrentActivity.dll */
	/* uncompressed_file_size */
	.word	8704
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_11

	/* 12: Plugin.Fingerprint.dll */
	/* uncompressed_file_size */
	.word	89600
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_12

	/* 13: Plugin.Media.dll */
	/* uncompressed_file_size */
	.word	42496
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_13

	/* 14: Plugin.Permissions.dll */
	/* uncompressed_file_size */
	.word	15872
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_14

	/* 15: Rg.Plugins.Popup.Droid.dll */
	/* uncompressed_file_size */
	.word	12800
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_15

	/* 16: Rg.Plugins.Popup.dll */
	/* uncompressed_file_size */
	.word	38912
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_16

	/* 17: SQLite-net.dll */
	/* uncompressed_file_size */
	.word	61440
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_17

	/* 18: SQLitePCLRaw.batteries_v2.dll */
	/* uncompressed_file_size */
	.word	5120
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_18

	/* 19: SQLitePCLRaw.core.dll */
	/* uncompressed_file_size */
	.word	24576
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_19

	/* 20: SQLitePCLRaw.provider.e_sqlite3.dll */
	/* uncompressed_file_size */
	.word	37888
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_20

	/* 21: SelfService.Android.dll */
	/* uncompressed_file_size */
	.word	384512
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_21

	/* 22: SelfService.dll */
	/* uncompressed_file_size */
	.word	278016
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_22

	/* 23: System.Core.dll */
	/* uncompressed_file_size */
	.word	1073664
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_23

	/* 24: System.Data.dll */
	/* uncompressed_file_size */
	.word	742912
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_24

	/* 25: System.Drawing.Common.dll */
	/* uncompressed_file_size */
	.word	26112
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_25

	/* 26: System.Net.Http.dll */
	/* uncompressed_file_size */
	.word	228864
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_26

	/* 27: System.Numerics.dll */
	/* uncompressed_file_size */
	.word	39424
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_27

	/* 28: System.Runtime.Serialization.dll */
	/* uncompressed_file_size */
	.word	421376
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_28

	/* 29: System.ServiceModel.Internals.dll */
	/* uncompressed_file_size */
	.word	55808
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_29

	/* 30: System.ServiceModel.dll */
	/* uncompressed_file_size */
	.word	190976
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_30

	/* 31: System.Xml.Linq.dll */
	/* uncompressed_file_size */
	.word	64512
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_31

	/* 32: System.Xml.dll */
	/* uncompressed_file_size */
	.word	1402368
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_32

	/* 33: System.dll */
	/* uncompressed_file_size */
	.word	879104
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_33

	/* 34: Xamarin.AndroidX.Activity.dll */
	/* uncompressed_file_size */
	.word	18944
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_34

	/* 35: Xamarin.AndroidX.Annotation.dll */
	/* uncompressed_file_size */
	.word	6144
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_35

	/* 36: Xamarin.AndroidX.AppCompat.AppCompatResources.dll */
	/* uncompressed_file_size */
	.word	19456
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_36

	/* 37: Xamarin.AndroidX.AppCompat.dll */
	/* uncompressed_file_size */
	.word	481280
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_37

	/* 38: Xamarin.AndroidX.Arch.Core.Common.dll */
	/* uncompressed_file_size */
	.word	6656
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_38

	/* 39: Xamarin.AndroidX.Arch.Core.Runtime.dll */
	/* uncompressed_file_size */
	.word	6144
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_39

	/* 40: Xamarin.AndroidX.AsyncLayoutInflater.dll */
	/* uncompressed_file_size */
	.word	6144
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_40

	/* 41: Xamarin.AndroidX.Biometric.dll */
	/* uncompressed_file_size */
	.word	32256
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_41

	/* 42: Xamarin.AndroidX.Browser.dll */
	/* uncompressed_file_size */
	.word	69120
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_42

	/* 43: Xamarin.AndroidX.CardView.dll */
	/* uncompressed_file_size */
	.word	18432
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_43

	/* 44: Xamarin.AndroidX.Collection.dll */
	/* uncompressed_file_size */
	.word	6656
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_44

	/* 45: Xamarin.AndroidX.CoordinatorLayout.dll */
	/* uncompressed_file_size */
	.word	78848
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_45

	/* 46: Xamarin.AndroidX.Core.dll */
	/* uncompressed_file_size */
	.word	581632
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_46

	/* 47: Xamarin.AndroidX.CursorAdapter.dll */
	/* uncompressed_file_size */
	.word	6656
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_47

	/* 48: Xamarin.AndroidX.CustomView.dll */
	/* uncompressed_file_size */
	.word	10240
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_48

	/* 49: Xamarin.AndroidX.DocumentFile.dll */
	/* uncompressed_file_size */
	.word	6144
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_49

	/* 50: Xamarin.AndroidX.DrawerLayout.dll */
	/* uncompressed_file_size */
	.word	45056
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_50

	/* 51: Xamarin.AndroidX.Fragment.dll */
	/* uncompressed_file_size */
	.word	162304
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_51

	/* 52: Xamarin.AndroidX.Interpolator.dll */
	/* uncompressed_file_size */
	.word	6656
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_52

	/* 53: Xamarin.AndroidX.Legacy.Support.Core.UI.dll */
	/* uncompressed_file_size */
	.word	16896
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_53

	/* 54: Xamarin.AndroidX.Legacy.Support.Core.Utils.dll */
	/* uncompressed_file_size */
	.word	6144
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_54

	/* 55: Xamarin.AndroidX.Lifecycle.Common.dll */
	/* uncompressed_file_size */
	.word	16384
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_55

	/* 56: Xamarin.AndroidX.Lifecycle.LiveData.Core.dll */
	/* uncompressed_file_size */
	.word	16896
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_56

	/* 57: Xamarin.AndroidX.Lifecycle.LiveData.dll */
	/* uncompressed_file_size */
	.word	6144
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_57

	/* 58: Xamarin.AndroidX.Lifecycle.Runtime.dll */
	/* uncompressed_file_size */
	.word	6144
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_58

	/* 59: Xamarin.AndroidX.Lifecycle.ViewModel.dll */
	/* uncompressed_file_size */
	.word	18432
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_59

	/* 60: Xamarin.AndroidX.Lifecycle.ViewModelSavedState.dll */
	/* uncompressed_file_size */
	.word	6144
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_60

	/* 61: Xamarin.AndroidX.Loader.dll */
	/* uncompressed_file_size */
	.word	37888
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_61

	/* 62: Xamarin.AndroidX.LocalBroadcastManager.dll */
	/* uncompressed_file_size */
	.word	6144
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_62

	/* 63: Xamarin.AndroidX.Media.dll */
	/* uncompressed_file_size */
	.word	11776
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_63

	/* 64: Xamarin.AndroidX.Print.dll */
	/* uncompressed_file_size */
	.word	5632
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_64

	/* 65: Xamarin.AndroidX.RecyclerView.dll */
	/* uncompressed_file_size */
	.word	418816
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_65

	/* 66: Xamarin.AndroidX.SavedState.dll */
	/* uncompressed_file_size */
	.word	13824
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_66

	/* 67: Xamarin.AndroidX.SlidingPaneLayout.dll */
	/* uncompressed_file_size */
	.word	6656
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_67

	/* 68: Xamarin.AndroidX.SwipeRefreshLayout.dll */
	/* uncompressed_file_size */
	.word	40960
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_68

	/* 69: Xamarin.AndroidX.Transition.dll */
	/* uncompressed_file_size */
	.word	9728
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_69

	/* 70: Xamarin.AndroidX.VectorDrawable.Animated.dll */
	/* uncompressed_file_size */
	.word	7680
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_70

	/* 71: Xamarin.AndroidX.VectorDrawable.dll */
	/* uncompressed_file_size */
	.word	6144
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_71

	/* 72: Xamarin.AndroidX.VersionedParcelable.dll */
	/* uncompressed_file_size */
	.word	6656
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_72

	/* 73: Xamarin.AndroidX.ViewPager.dll */
	/* uncompressed_file_size */
	.word	59392
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_73

	/* 74: Xamarin.AndroidX.ViewPager2.dll */
	/* uncompressed_file_size */
	.word	7168
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_74

	/* 75: Xamarin.Auth.dll */
	/* uncompressed_file_size */
	.word	87552
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_75

	/* 76: Xamarin.Essentials.dll */
	/* uncompressed_file_size */
	.word	30720
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_76

	/* 77: Xamarin.Facebook.Android.dll */
	/* uncompressed_file_size */
	.word	5632
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_77

	/* 78: Xamarin.Facebook.AppLinks.Android.dll */
	/* uncompressed_file_size */
	.word	6144
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_78

	/* 79: Xamarin.Facebook.Common.Android.dll */
	/* uncompressed_file_size */
	.word	27136
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_79

	/* 80: Xamarin.Facebook.Core.Android.dll */
	/* uncompressed_file_size */
	.word	33792
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_80

	/* 81: Xamarin.Facebook.Login.Android.dll */
	/* uncompressed_file_size */
	.word	7680
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_81

	/* 82: Xamarin.Facebook.Messenger.Android.dll */
	/* uncompressed_file_size */
	.word	6656
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_82

	/* 83: Xamarin.Facebook.Places.Android.dll */
	/* uncompressed_file_size */
	.word	12288
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_83

	/* 84: Xamarin.Facebook.Share.Android.dll */
	/* uncompressed_file_size */
	.word	12288
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_84

	/* 85: Xamarin.Forms.Core.dll */
	/* uncompressed_file_size */
	.word	1191424
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_85

	/* 86: Xamarin.Forms.Platform.Android.dll */
	/* uncompressed_file_size */
	.word	792064
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_86

	/* 87: Xamarin.Forms.Platform.dll */
	/* uncompressed_file_size */
	.word	120320
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_87

	/* 88: Xamarin.Forms.Xaml.dll */
	/* uncompressed_file_size */
	.word	102400
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_88

	/* 89: Xamarin.Google.Android.Material.dll */
	/* uncompressed_file_size */
	.word	281088
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_89

	/* 90: Xamarin.Google.Guava.ListenableFuture.dll */
	/* uncompressed_file_size */
	.word	5120
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_90

	/* 91: ZXing.Net.Mobile.Forms.dll */
	/* uncompressed_file_size */
	.word	129024
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_91

	/* 92: ZXingNetMobile.dll */
	/* uncompressed_file_size */
	.word	98816
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_92

	/* 93: mscorlib.dll */
	/* uncompressed_file_size */
	.word	2193408
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_93

	/* 94: zxing.dll */
	/* uncompressed_file_size */
	.word	359936
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_94

	.size	.L.compressed_assembly_descriptors, 1520
	.section	.data.compressed_assemblies,"aw",@progbits
	.type	compressed_assemblies, @object
	.p2align	3
	.global	compressed_assemblies
compressed_assemblies:
	/* count */
	.word	95
	/* descriptors */
	.zero	4
	.xword	.L.compressed_assembly_descriptors
	.size	compressed_assemblies, 16
