using _Project.Scripts.Infrastructure.StaticData.Configs;
using VContainer;

namespace _Project.Scripts.Utils.Extensions
{
  public static class VContainerExtensions
  {
    public static RegistrationBuilder RegisterConfig<T>(this IContainerBuilder builder, Lifetime lifetime) where T : IConfigParser
    {
      return builder.Register<T>(lifetime).As<IConfigParser, T>();
    }
  }
}