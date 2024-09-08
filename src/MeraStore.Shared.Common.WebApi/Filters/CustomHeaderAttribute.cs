namespace MeraStore.Shared.Common.WebApi.Filters
{
  [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
  public class CustomHeaderAttribute(string name, bool required = false, string description = null)
    : Attribute
  {
    public string Name { get; } = name;
    public bool Required { get; } = required;
    public string Description { get; } = description;
  }
}