using REG.Abstraction;
using System;
namespace REG.Exceptions;
public class TargetNotOverriddenException(string overrideName, string originalName) : Exception($"Override {originalName} before call {overrideName} Revert Method.") {
 public static void ThrowIfNotOverridden(IOverride ov) {
  if(!ov.IsOverridden) {
   throw new TargetNotOverriddenException(ov.Name, ov.TargetName);
  }
 }
}