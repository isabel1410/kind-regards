using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Style", "IDE0090: Use 'new(...)'", Justification = "Unity does not support C# 9.")]
[assembly: SuppressMessage("Style", "IDE0044: Make field readonly", Justification = "Serialized fields will not show up in the Unity inspector when marked as readonly.")]
[assembly: SuppressMessage("Test", "CS0162: Unreachable code detected", Justification = "Used for temporary thrown exceptions for methods that will be refactored in the future.")]
[assembly: SuppressMessage("Test", "CS0649: Field 'field' is never assigned to, and will always have its devailt value null", Justification = "Field is assigned through the inspector.")]