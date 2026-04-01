using Xunit.v3;

namespace Xunit;

sealed partial class ClassDataAttribute
{ }

partial class ClassDataAttribute<TClass> : DataAttribute
{ }
