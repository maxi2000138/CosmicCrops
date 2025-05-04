using _Project.Scripts.Game.Features.Weapon.Data;

namespace _Project.Scripts.Utils.Collision
{
  public static class CollisionExtensions
  {
    public static bool Matches(this int mask, CollisionLayer layer) =>
      (mask & layer.AsMask()) != 0;

    public static int AsMask(this CollisionLayer layer) =>
      1 << (int)layer;
  }
}