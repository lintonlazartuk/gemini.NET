// Copyright (c) 2025 Linton Lazartuk. All rights reserved.
// Author: Linton Lazartuk

using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace GeminiDemoApp
{
    public interface IGeminiService
    {
        Task<string> GenerateContentAsync(string prompt);
    }

    public class GeminiService : IGeminiService
    {
        private readonly HttpClient _httpClient;
        private readonly GeminiOptions _options;

        public GeminiService(HttpClient httpClient, IOptions<GeminiOptions> options)
        {
            _httpClient = httpClient;
            _options = options.Value;
        }

        public async Task<string> GenerateContentAsync(string prompt)
        {
            if (_options == null)
                return "Gemini configuration is missing.";
            if (string.IsNullOrWhiteSpace(_options.BaseUrl))
                return $"Gemini BaseUrl is missing in configuration. Current value: '{_options.BaseUrl}'";
            if (string.IsNullOrWhiteSpace(_options.ApiKey))
                return $"Gemini ApiKey is missing in configuration. Current value: '{_options.ApiKey}'";

            var request = new GeminiRequest
            {
                Contents = new List<ContentRequest>
                {
                    new ContentRequest
                    {
                        Role = "user",
                        Parts = new List<Part> { new Part { Text = prompt } }
                    }
                },
                GenerationConfig = new GenerationConfig
                {
                    Temperature = 1,
                    MaxOutputTokens = 256,
                    TopP = 0.95,
                    TopK = 40,
                    StopSequences = new List<string>()
                },
                SafetySettings = new List<SafetySetting>()
            };

            var json = JsonSerializer.Serialize(request, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Clear();

            // Append API key as query parameter
            var uri = _options.BaseUrl.Contains("?")
                ? _options.BaseUrl + "&key=" + _options.ApiKey
                : _options.BaseUrl + "?key=" + _options.ApiKey;

            var response = await _httpClient.PostAsync(uri, content);
            
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                return $"API Error ({response.StatusCode}): {errorContent}";
            }

            var respJson = await response.Content.ReadAsStringAsync();
            var geminiResp = JsonSerializer.Deserialize<GeminiResponse>(respJson, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            if (geminiResp?.Candidates == null || geminiResp.Candidates.Length == 0)
                return "No candidates returned by Gemini API.";
            var geminiContent = geminiResp.Candidates[0]?.Content;
            if (geminiContent?.Parts == null || geminiContent.Parts.Count == 0)
                return "No content parts returned by Gemini API.";
            return geminiContent.Parts[0]?.Text ?? "No text returned by Gemini API.";
        }
    }
}
