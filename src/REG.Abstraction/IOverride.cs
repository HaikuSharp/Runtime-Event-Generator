using System;
namespace REG.Abstraction;
public interface IOverride : IDisposable {
 string Name { get; }
 string TargetName { get; }
 IntPtr Ptr { get; }
 IntPtr TargetPtr { get; }
 bool IsOverridden { get; }
 void Override();
 void Revert();
}
