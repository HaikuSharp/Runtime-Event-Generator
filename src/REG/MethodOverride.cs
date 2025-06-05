using REG.Abstraction;
using REG.Exceptions;
using System;
using System.Reflection;
namespace REG;
public class MethodOverride : IMethodOverride {
 private readonly MethodInfo m_Original;
 private readonly MethodInfo m_Destination;
 private readonly IntPtr m_OriginalPtr;
 private readonly IntPtr m_OverriddenPtr;
 private readonly ulong m_OffsetedA;
 private readonly ulong m_OffsetedB;
 internal MethodOverride(MethodInfo original, MethodInfo destination) {
  this.m_Original = original;
  this.m_Destination = destination;
  this.m_OriginalPtr = original.MethodHandle.GetFunctionPointer();
  this.m_OverriddenPtr = destination.MethodHandle.GetFunctionPointer();
  unsafe {
   var ptrMethod = (byte*)this.m_OriginalPtr.ToPointer();
   this.m_OffsetedA = *(ulong*)ptrMethod;
   this.m_OffsetedB = *(ulong*)(ptrMethod + 8);
  }
 }
 public string Name {
  get {
   return this.m_Destination.Name;
  }
 }
 public string TargetName {
  get {
   return this.m_Original.Name;
  }
 }
 public IntPtr Ptr {
  get {
   return this.m_OriginalPtr;
  }
 }
 public IntPtr TargetPtr {
  get {
   return this.m_OverriddenPtr;
  }
 }
 public bool IsOverridden { get; private set; }
 public void Override() {
  TargetAlreadyOverriddenExeption.ThrowIfOverridden(this);
  this.InternalOverride();
  this.IsOverridden = true;
 }
 public void Revert() {
  TargetNotOverriddenException.ThrowIfNotOverridden(this);
  this.InternalRevert();
  this.IsOverridden = false;
 }
 public void Dispose() {
  this.Revert();
  GC.SuppressFinalize(this);
 }
 private void InternalOverride() {
  if(IntPtr.Size == sizeof(int)) {
   this.InternalOverride32();
  } else {
   this.InternalOverride64();
  }
 }
 private unsafe void InternalOverride32() {
  var ptrFrom = (byte*)this.m_OriginalPtr.ToPointer();
  *ptrFrom = 0x68;
  *(uint*)(ptrFrom + 1) = (uint)this.m_OverriddenPtr.ToInt32();
  *(ptrFrom + 5) = 0xC3;
 }
 private unsafe void InternalOverride64() {
  var ptrFrom = (byte*)this.m_OriginalPtr.ToPointer();
  *ptrFrom = 0x48;
  *(ptrFrom + 1) = 0xB8;
  *(ulong*)(ptrFrom + 2) = (ulong)this.m_OverriddenPtr.ToInt64();
  *(ptrFrom + 10) = 0xFF;
  *(ptrFrom + 11) = 0xE0;
 }
 private unsafe void InternalRevert() {
  var ptrOriginal = (byte*)this.m_OriginalPtr.ToPointer();
  *(ulong*)ptrOriginal = this.m_OffsetedA;
  *(ulong*)(ptrOriginal + 8) = this.m_OffsetedB;
 }
}
