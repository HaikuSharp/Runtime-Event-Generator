using REG.Abstraction;
using System;
namespace REG.Exceptions;
public class TargetAlreadyOverriddenExeption(string overrideName, string originalName) : Exception($"Revert {overrideName} before call {originalName} Override Method.") {
 public static void ThrowIfOverridden(IOverride ov) {
  if(ov.IsOverridden) {
   throw new TargetAlreadyOverriddenExeption(ov.Name, ov.TargetName);
  }
 }
}
