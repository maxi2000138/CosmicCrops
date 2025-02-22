using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Project.Scripts.Infrastructure.StaticData.Configs;
using FluentAssertions;
using NUnit.Framework;

public class ValidationTests
{
    [TestCaseSource(nameof(GetConfigsTypes))]
    public async Task FindMissingPrefabsInConfig(Type type)
    {
        var configLoader = new TestsConfigLoader();
        var prefabValidator = new PrefabValidator();

        var prefabs = await configLoader.GetPrefabsFromConfig(type);
        var missingPrefabs = new List<string>();

        foreach (var prefab in prefabs)
        {
            if(await prefabValidator.ExistsInAdressables(prefab.Name) == false)
                missingPrefabs.Add(prefab.Name);
        }

        missingPrefabs.Should().BeEmpty();
    }


    private static IEnumerable<Type> GetConfigsTypes()
    {
        var interfaceType = typeof(IConfigParser);

        if (!interfaceType.IsInterface)
            throw new ArgumentException($"Type {interfaceType.Name} is not an interface.");

        return AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => interfaceType.IsAssignableFrom(type) && !type.IsAbstract);
    }
}