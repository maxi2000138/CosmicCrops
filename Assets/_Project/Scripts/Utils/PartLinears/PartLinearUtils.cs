namespace _Project.Scripts.Utils.PartLinears
{
	public static class PartLinearUtils
	{
		private static PartLinearsConfig _config;
		
		public static void SetConfig(PartLinearsConfig config) =>
			_config = config;

		public static PartLinearData Get(string id) => _config.Data[id];
	}
}