// See https://aka.ms/new-console-template for more information
using ApiGetter;

Console.WriteLine(await ApiGet.ApiRequest($"https://pokeapi.co/api/v2/pokemon?offset=20&limit=20"));
