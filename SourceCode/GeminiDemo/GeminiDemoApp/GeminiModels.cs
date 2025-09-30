// Copyright (c) 2025 Linton Lazartuk. All rights reserved.
// Author: Linton Lazartuk

using System.Collections.Generic;

namespace GeminiDemoApp
{
    public class GeminiRequest
    {
        public List<ContentRequest>? Contents { get; set; }
        public GenerationConfig? GenerationConfig { get; set; }
        public List<SafetySetting>? SafetySettings { get; set; }
    }

    public class ContentRequest
    {
        public string? Role { get; set; }
        public List<Part>? Parts { get; set; }
    }

    public class Part
    {
        public string? Text { get; set; }
        // Add other fields for multimodal if needed
    }

    public class GenerationConfig
    {
        public int Temperature { get; set; }
        public int MaxOutputTokens { get; set; }
        public double TopP { get; set; }
        public int TopK { get; set; }
        public List<string>? StopSequences { get; set; }
    }

    public class SafetySetting
    {
        public string? Category { get; set; }
        public string? Threshold { get; set; }
    }

    public class GeminiResponse
    {
        public Candidate[]? Candidates { get; set; }
        // Add other metadata, safety ratings, etc. as needed
    }

    public class Candidate
    {
        public Content? Content { get; set; }
        public string? FinishReason { get; set; }
        public int Index { get; set; }
    }

    public class Content
    {
        public List<Part>? Parts { get; set; }
        public string? Role { get; set; }
    }
}
