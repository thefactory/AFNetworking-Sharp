using System;
using MonoTouch.ObjCRuntime;

[assembly: LinkWith ("libAFNetworking.a", LinkTarget.Simulator | LinkTarget.ArmV7, ForceLoad = true, Frameworks = "SystemConfiguration MobileCoreServices")]
