// Copyright (c) 2025 Linton Lazartuk. All rights reserved.
// Author: Linton Lazartuk

using GeminiDemoApp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
	.UseContentRoot(AppContext.BaseDirectory)
	.ConfigureAppConfiguration((context, config) =>
	{
		config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
		config.AddEnvironmentVariables();
	})
	.ConfigureServices((context, services) =>
	{
		services.Configure<GeminiOptions>(context.Configuration.GetSection("Gemini"));
		services.AddHttpClient<IGeminiService, GeminiService>();
	})
	.Build();

var gemini = host.Services.GetRequiredService<IGeminiService>();

// Debug: Print current directory and check if appsettings.json exists
Console.WriteLine($"Current directory: {Directory.GetCurrentDirectory()}");
Console.WriteLine($"appsettings.json exists: {File.Exists("appsettings.json")}");

// Debug: Check configuration values
var config = host.Services.GetRequiredService<IConfiguration>();
Console.WriteLine($"Gemini:ApiKey from config: '{config.GetSection("Gemini:ApiKey").Value}'");
Console.WriteLine($"Gemini:BaseUrl from config: '{config.GetSection("Gemini:BaseUrl").Value}'");

Console.WriteLine("Enter a prompt for Gemini:");
var prompt = Console.ReadLine();
var result = await gemini.GenerateContentAsync(prompt ?? "Say hello to .NET and Gemini!");
Console.WriteLine($"Gemini response: {result}");
