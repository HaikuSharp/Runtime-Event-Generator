using System.Runtime.CompilerServices;
namespace REG.Tests;
public class Class(int value) {
 public int Value { get; } = value;
 [MethodImpl(MethodImplOptions.AggressiveInlining)]
 public int Method() {
  return this.Value;
 }
}