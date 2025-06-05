using System;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
namespace REG.Tests;
[TestClass]
public class DynamicMethodTest {
 [TestMethod]
 public void CreatePtrProvideMethod() {
  var myObject = new object();
  var ptr = GetObjectPointer(myObject);

  Console.WriteLine($"Object Ptr: {ptr}");
  Console.WriteLine($"Objcet Hash: {myObject.GetHashCode()}");

  var dynamicMethod = new DynamicMethod(
      name: "PtrProvideMethod",
      returnType: typeof(IntPtr),
      parameterTypes: null,
      typeof(DynamicMethodTest).Module
  );

  var il = dynamicMethod.GetILGenerator();
  il.Emit(OpCodes.Ldc_I8, ptr.ToInt64());
  il.Emit(OpCodes.Conv_I);
  il.Emit(OpCodes.Ret);

  var ptrGetter = (Func<IntPtr>)dynamicMethod.CreateDelegate(typeof(Func<IntPtr>));
  var resultPtr = ptrGetter();

  var handle = GCHandle.FromIntPtr(resultPtr);
  var resultObject = handle.Target!;

  Console.WriteLine($"Func Result: {resultPtr}");
  Console.WriteLine($"Func Result Object Hash: {resultObject.GetHashCode()}");
 }
 private static IntPtr GetObjectPointer(object obj) {
  var handle = GCHandle.Alloc(obj, GCHandleType.Normal);
  return GCHandle.ToIntPtr(handle);
 }
}
