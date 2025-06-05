namespace REG.Tests;
[TestClass]
public sealed class MethodOverrideTest {
 // private static readonly MethodInfo s_TargetMethod = typeof(Class).GetMethod(nameof(Class.Method), BindingFlags.Instance | BindingFlags.Public);
 // private static readonly MethodInfo s_OverrideMethod = typeof(Override).GetMethod(nameof(Override.Method), BindingFlags.Static | BindingFlags.Public);
 // private static readonly MethodInfo s_TargetPropertyGetMethod = typeof(Class).GetProperty(nameof(Class.Value), BindingFlags.Instance | BindingFlags.Public).GetGetMethod();
 // private static readonly MethodInfo s_OverridePropertyGetMethod = typeof(Override).GetMethod(nameof(Override.GetValue), BindingFlags.Static | BindingFlags.Public);
 [TestMethod]
 public void TestOverrideMethod() {
  // MethodOverride ov = new(s_TargetMethod, s_OverrideMethod);
  // Class c = new(1);
  // Assert.AreEqual(1, c.Method());
  // ov.Override();
  // Assert.AreEqual(0, c.Method());
  // ov.Revert();
  // Assert.AreEqual(1, c.Method());
 }
 [TestMethod]
 public void TestOverrideProperty() {
  // MethodOverride ov = new(s_TargetPropertyGetMethod, s_OverridePropertyGetMethod);
  // Class c = new(1);
  // Assert.AreEqual(1, c.Value);
  // ov.Override();
  // Assert.AreEqual(2, c.Value);
  // ov.Revert();
  // Assert.AreEqual(1, c.Value);
 }
}
