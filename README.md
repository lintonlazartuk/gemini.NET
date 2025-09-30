# gemini.NET
POC for Gemini integration with .NET

A .NET 8 console application demonstrating integration with Google's Gemini AI API using HTTP client and dependency injection.

## Features

- ✅ .NET 8 Console Application
- ✅ Google Gemini API Integration
- ✅ Configuration Management with `appsettings.json`
- ✅ Dependency Injection with `IServiceCollection`
- ✅ HTTP Client Factory pattern
- ✅ Proper error handling and null safety
- ✅ JSON serialization with camelCase naming

## Prerequisites

1. **.NET 8 SDK** - [Download here](https://dotnet.microsoft.com/download/dotnet/8.0)
2. **Google Gemini API Key** - See setup instructions below

## Getting Your Gemini API Key

### Option 1: Google AI Studio (Recommended for quick start)
1. Visit [Google AI Studio](https://aistudio.google.com/app/apikey)
2. Sign in with your Google account
3. Click **"Create API Key"**
4. Copy the generated API key

### Option 2: Google Cloud Console (For production use)
1. Visit [Google Cloud Console](https://console.cloud.google.com/)
2. Create a new project or select an existing one
3. Enable the **Generative Language API**
4. Go to **APIs & Services > Credentials**
5. Click **"Create Credentials" > "API Key"**
6. Copy the generated API key

## Setup Instructions

1. **Clone the repository:**
   ```bash
   git clone https://github.com/lintonlazartuk/gemini.NET.git
   cd gemini.NET
   ```

2. **Add your API key:**
   Open `SourceCode/GeminiDemo/GeminiDemoApp/appsettings.json` and replace `YOUR_API_KEY` with your actual Gemini API key:
   ```json
   {
     "Gemini": {
       "ApiKey": "your-actual-api-key-here",
       "BaseUrl": "https://generativelanguage.googleapis.com/v1beta/models/gemini-flash-latest:generateContent"
     }
   }
   ```

## How to Test

### Method 1: Using the shell script (recommended)
```bash
./start-gemini-demo.sh
```

### Method 2: Using dotnet CLI directly
```bash
dotnet run --project SourceCode/GeminiDemo/GeminiDemoApp/GeminiDemoApp.csproj
```

### Method 3: Using Visual Studio Code
1. Open the project in VS Code
2. Press `F5` or use the Run and Debug panel
3. Select the GeminiDemoApp configuration

## Usage Example

```
Enter a prompt for Gemini:
Hello, can you help me with .NET development?

Gemini response: Hello! I'd be happy to help you with .NET development. I can assist with:

- C# programming concepts and best practices
- ASP.NET Core web development
- Entity Framework and database integration
- Dependency injection and design patterns
- Performance optimization
- Testing strategies
- And much more!

What specific .NET development topic would you like help with?
```

## Project Structure

```
gemini.NET/
├── SourceCode/
│   └── GeminiDemo/
│       ├── GeminiDemo.sln
│       └── GeminiDemoApp/
│           ├── GeminiDemoApp.csproj
│           ├── Program.cs
│           ├── GeminiService.cs
│           ├── GeminiModels.cs
│           ├── GeminiOptions.cs
│           └── appsettings.json
├── start-gemini-demo.sh
└── README.md
```

## Architecture

- **Program.cs**: Entry point with host configuration and dependency injection setup
- **GeminiService.cs**: HTTP client service for Gemini API communication
- **GeminiModels.cs**: Request/response models for Gemini API
- **GeminiOptions.cs**: Configuration binding class
- **appsettings.json**: Configuration file for API key and endpoints

## Dependencies

- Microsoft.Extensions.Hosting (9.0.9)
- Microsoft.Extensions.Http (9.0.9)
- Microsoft.Extensions.Options.ConfigurationExtensions (9.0.9)

## API Documentation

For more details about the Gemini API, visit:
- [Google AI Studio Documentation](https://ai.google.dev/docs)
- [Generative Language API Reference](https://ai.google.dev/api)

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Test with your own API key
5. Submit a pull request

## License

This project is for demonstration purposes. Please check Google's terms of service for Gemini API usage.