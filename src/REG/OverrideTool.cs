using REG.Abstraction;
using Sugar.Object.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
namespace REG;
internal class OverrideTool {
 private static readonly List<IOverride> m_Overrides = [];
 private static readonly Dictionary<IntPtr, IOverride> m_OverridesByPtr = [];
 private static readonly Dictionary<IntPtr, IOverride> m_OverridesByTargetPtr = [];
 internal static bool IsOverride(IntPtr ptr) {
  return m_Overrides.Any(o => o.Ptr == ptr);
 }
 internal static bool IsOverridden(IntPtr targetPtr) {
  return m_Overrides.Any(o => o.TargetPtr == targetPtr);
 }
 internal static IOverride OverrideMethod(MethodInfo target, MethodInfo destination) {
  return RegisterAndIntializeOverride(new MethodOverride(target, destination));
 }
 internal static IOverride RegisterAndIntializeOverride(IOverride ov) {
  RegisterOverride(ov).Override();
  return ov;
 }
 internal static IOverride RegisterOverride(IOverride ov) {
  m_Overrides.Add(ov);
  return ov;
 }
 internal static void RevertOverrideByPtr(IntPtr ptr) {
  m_Overrides.RemoveAll(o => RevertOverrideByPtrPredicate(o, ptr)).Forget();
  m_OverridesByPtr.Remove(ptr).Forget();
 }
 internal static void RevertOverrideByTargetPtr(IntPtr targetPtr) {
  m_Overrides.RemoveAll(o => RevertOverrideByTargetPtrPredicate(o, targetPtr)).Forget();
  m_OverridesByTargetPtr.Remove(targetPtr).Forget();
 }
 internal static IOverride GetOverrideByPtr(IntPtr ptr) {
  if(m_OverridesByPtr.TryGetValue(ptr, out var ov)) {
   return ov;
  }
  ov = FindOverrideByPtr(ptr);
  m_OverridesByPtr.Add(ptr, ov);
  return ov;
 }
 internal static IOverride GetOverrideByTargetPtr(IntPtr targetPtr) {
  if(m_OverridesByTargetPtr.TryGetValue(targetPtr, out var ov)) {
   return ov;
  }
  ov = FindOverrideByTargetPtr(targetPtr);
  m_OverridesByPtr.Add(targetPtr, ov);
  return ov;
 }
 internal static IOverride FindOverrideByPtr(IntPtr ptr) {
  return m_Overrides.FirstOrDefault(o => o.Ptr == ptr);
 }
 internal static IOverride FindOverrideByTargetPtr(IntPtr targetPtr) {
  return m_Overrides.FirstOrDefault(o => o.TargetPtr == targetPtr);
 }
 private static bool RevertOverrideByPtrPredicate(IOverride ov, IntPtr ptr) {
  if(ov.Ptr == ptr) {
   ov.Revert();
   return true;
  }
  return false;
 }
 private static bool RevertOverrideByTargetPtrPredicate(IOverride ov, IntPtr targetPtr) {
  if(ov.TargetPtr == targetPtr) {
   ov.Revert();
   return true;
  }
  return false;
 }
}
public class RuntimeEvent {

}
