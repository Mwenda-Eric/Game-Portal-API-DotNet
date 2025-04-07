# Game Portal API

A .NET backend system for an educational game portal that connects teachers and students through Unity-based games. The platform allows teachers to create, manage, and share questions with students through interactive game sessions, with advanced AI-powered question generation capabilities.

## Overview

The Game Portal API is built using ASP.NET Core and follows a clean architecture pattern with controllers, services, repositories, and data access layers. The system serves as the backend for Unity-based educational games, providing a robust platform for teacher-student interaction through gamified learning experiences.

### Key Features

The system enables teachers to:

- Create and manage their profiles with customizable details
- Generate questions using AI based on specified topics and subjects
- Review, edit, and enhance AI-generated questions before publishing
- Create educational game sessions with custom configurations
- Add manually created or AI-generated questions to sessions
- Organize questions by subject (Math, English, Science) and topics
- Retrieve questions for specific sessions and subjects
- Track student performance and engagement through game analytics

## System Architecture

The application follows a clean, layered architecture pattern that provides separation of concerns and maintainability:

```
GamePortalAPI/
├── Controllers/         # API endpoints and request handling
│   ├── GameApiController.cs      # Base API controller
│   ├── SessionController.cs      # Session management endpoints
│   └── TeachersController.cs     # Teacher and question endpoints
├── Services/            # Business logic implementation
│   ├── ApiService/           # Teacher and question services
│   │   ├── ApiService.cs
│   │   └── IApiService.cs
│   ├── SessionService/       # Session management services
│   │   ├── SessionService.cs
│   │   └── ISessionService.cs
│   └── AIService/            # AI question generation services
│       ├── AIQuestionService.cs
│       └── IAIQuestionService.cs
├── Repositories/        # Data access and persistence
│   ├── TeacherRepository/    # Teacher data operations
│   │   ├── TeacherRepository.cs
│   │   └── ITeacherRepository.cs
│   └── SessionRepository/    # Session data operations
│       ├── SessionRepository.cs
│       └── ISessionRepository.cs
├── Models/              # Domain entities
│   ├── Teacher.cs            # Teacher entity
│   ├── Question.cs           # Question entity
│   ├── Session.cs            # Session entity
│   └── Subject.cs            # Subject enumeration
├── DTOs/                # Data transfer objects
│   ├── QuestionDtos/         # Question request/response objects
│   ├── TeacherDtos/          # Teacher request/response objects
│   ├── SessionDtos/          # Session request/response objects
│   └── ServiceResponse/      # Standard response wrapper
├── Data/                # Database context and configuration
│   └── DataContext.cs        # Entity Framework DbContext
├── Helpers/             # Utility classes and extensions
│   └── AutoMapperProfile.cs  # DTO mapping configurations
└── Program.cs           # Application configuration and DI setup
```

### Key Components

#### 1. Controllers Layer
The controllers handle HTTP requests, validate inputs, and return appropriate responses. They don't contain business logic but delegate to services.

- **GameApiController**: Base controller with common functionality
- **TeachersController**: Manages teacher profiles and questions
- **SessionController**: Handles game session operations

#### 2. Services Layer
The services implement business logic, orchestrate operations across repositories, and ensure business rules are enforced.

- **ApiService**: Manages teacher-related operations and question handling
- **SessionService**: Manages game session operations and configurations
- **AIQuestionService**: Handles AI-powered question generation and enhancement

#### 3. Repositories Layer
The repositories abstract data access, perform CRUD operations, and manage database interactions.

- **TeacherRepository**: Handles teacher and question data operations
- **SessionRepository**: Manages session storage and retrieval

#### 4. Data Layer
Provides database access through Entity Framework Core.

- **DataContext**: DbContext configuration with entity relationships
- Database migration management

#### 5. Models
Domain entities representing the core business objects with properties and relationships.

- **Teacher**: Educational professional with profile information
- **Question**: Educational question with multiple-choice answers
- **Session**: Group of questions for a specific game instance
- **Subject**: Categorization of academic content

#### 6. DTOs (Data Transfer Objects)
Objects used for API communication, separating domain models from external representation.

- **Request DTOs**: Incoming data structures
- **Response DTOs**: Outgoing data structures
- **ServiceResponse**: Standard wrapper for all API responses

#### 7. Cross-Cutting Concerns
- **AutoMapper**: Object-to-object mapping between models and DTOs
- **Logging**: Application-wide logging implementation
- **Exception Handling**: Centralized error management

## Data Models

### Core Entities

#### Teacher
Represents an educator who creates questions and game sessions.

```csharp
public class Teacher
{
    public int Id { get; set; }
    public string TeachersName { get; set; } = String.Empty;
    public List<Question>? AllQuestions { get; set; }
    public List<Session>? GameSessions { get; set; }
    public string ProfilePictureUrl { get; set; } = String.Empty;
    public DateTime dateCreated { get; set; } = DateTime.Now;
    public DateTime lastUpdated { get; set; } = DateTime.Now;
}
```

#### Question
A multiple-choice educational question with three possible answers.

```csharp
public class Question
{
    public int QuestionId { get; set; }
    public string ActualQuestion { get; set; } = String.Empty;
    public string FirstAnswer { get; set; } = String.Empty;
    public string SecondAnswer { get; set; } = String.Empty;
    public string ThirdAnswer { get; set; } = String.Empty;
    public int correctAnswerIndex { get; set; }
    public Subject Subject { get; set; } = Subject.MATH;
    public Teacher? Teacher { get; set; }
    public int? TeacherId { get; set; }
    public Session? GameSession;
    public int SessionId { get; set; }
    public DateTime dateCreated { get; set; }
    public DateTime lastUpdated { get; set; }
}
```

#### Session
A collection of questions for a specific game instance.

```csharp
public class Session
{
    public int SessionId { get; set; }
    public string SessionName { get; set; } = String.Empty;
    public List<Question>? SessionQuestions { get; set; }
    public Subject SessionSubject { get; set; } = Subject.ENGLISH;
    public int teacherId { get; set; }
    public Teacher? teacher;
}
```

#### Subject
An enumeration of academic subjects supported by the system.

```csharp
public enum Subject
{
    MATH,
    ENGLISH,
    SCIENCE
}
```

### Data Transfer Objects (DTOs)

The system uses various DTOs to separate the domain models from the API contracts:

#### Question DTOs
- **GetQuestionResponseDto**: Sent to clients with question data
- **AddQuestionRequestDto**: Received from clients to create new questions

#### Teacher DTOs
- **GetTeacherResponseDto**: Sent to clients with teacher profile data
- **AddTeacherRequestDto**: Received from clients to create new teacher profiles
- **SingleTeacherResponseDto**: Simplified teacher response for specific operations

#### Session DTOs
- **GetSessionResponseDto**: Sent to clients with session data
- **CreateSessionRequestDto**: Received from clients to create new sessions

#### Service Response
A standardized wrapper for all API responses.

```csharp
public class ServiceResponse<T>
{
    public T? Data { get; set; }
    public string Message { get; set; } = String.Empty;
    public bool IsSuccessful { get; set; } = true;
}
```

### Entity Relationships

The system implements the following relationships between entities:

1. **Teacher to Questions (One-to-Many)**
   - A Teacher can author multiple Questions
   - Each Question is authored by one Teacher
   - Foreign key: `Question.TeacherId` references `Teacher.Id`

2. **Teacher to Sessions (One-to-Many)**
   - A Teacher can create multiple Sessions
   - Each Session belongs to one Teacher
   - Foreign key: `Session.teacherId` references `Teacher.Id`

3. **Session to Questions (One-to-Many)**
   - A Session contains multiple Questions
   - Each Question belongs to one Session
   - Foreign key: `Question.SessionId` references `Session.SessionId`

4. **Subject to Questions (One-to-Many)**
   - A Subject can categorize multiple Questions
   - Each Question belongs to one Subject (MATH, ENGLISH, or SCIENCE)
   - Implementation: `Question.Subject` as an enum property

### Database Schema Configuration

The database schema is configured using Entity Framework Core's Fluent API in the `DataContext` class:

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Question>().HasKey("QuestionId");

    // This will change the table name to Question
    var config = modelBuilder.Entity<Question>();
    config.ToTable("Question");
}
```

## API Endpoints

The Game Portal API provides a comprehensive set of RESTful endpoints to manage teachers, questions, and game sessions. All endpoints follow a consistent routing pattern and response format.

### Base URL Structure

All API endpoints follow this URL structure:
```
https://{host}/api/v1/{controller}/{action}
```

### Teacher Endpoints

| Method | Endpoint | Description | Authentication |
|--------|----------|-------------|----------------|
| GET | `/api/v1/Teachers/GetAllTeachers` | Retrieves all teacher profiles | Required |
| GET | `/api/v1/Teachers/GetTeachersWithSubject/{subject}` | Retrieves teachers with questions in a specific subject | Required |
| POST | `/api/v1/Teachers/CreateTeacher` | Creates a new teacher profile and returns all teachers | Required |
| POST | `/api/v1/Teachers/CreateNewTeacher` | Creates a new teacher profile and returns only the new teacher | Required |
| POST | `/api/v1/Teachers/AddQuestionForTeacher` | Adds a question to a teacher's collection | Required |
| GET | `/api/v1/Teachers/GetQuestionForTeacher/{teachersName}` | Retrieves all questions for a specific teacher | Required |
| GET | `/api/v1/Teachers/GetSessionQuestionsForTeacher/{teachersId}/{sessionId}/subject` | Retrieves session-specific questions for a teacher | Required |

### Session Endpoints

| Method | Endpoint | Description | Authentication |
|--------|----------|-------------|----------------|
| GET | `/api/v1/Session/GetAllSessions` | Retrieves all game sessions | Required |
| POST | `/api/v1/Session/CreateSession` | Creates a new game session | Required |

### AI Question Generation Endpoints

*These endpoints would be implemented in the AI service extension:*

| Method | Endpoint | Description | Authentication |
|--------|----------|-------------|----------------|
| POST | `/api/v1/AI/GenerateQuestions` | Generates questions using AI based on topic and subject | Required |
| POST | `/api/v1/AI/EnhanceQuestion` | Improves an existing question using AI | Required |
| POST | `/api/v1/AI/GenerateAnswerOptions` | Generates plausible answer options for a question | Required |

## Detailed Endpoint Specifications

### Teacher Management

#### GET /api/v1/Teachers/GetAllTeachers

Retrieves a list of all teacher profiles with their basic information.

**Response:**
```json
{
  "data": [
    {
      "id": 1,
      "teachersName": "John Smith",
      "profilePictureUrl": "https://example.com/profiles/jsmith.jpg"
    },
    {
      "id": 2,
      "teachersName": "Jane Doe",
      "profilePictureUrl": "https://example.com/profiles/jdoe.jpg"
    },
    {
      "id": 3,
      "teachersName": "Alex Johnson",
      "profilePictureUrl": "https://example.com/profiles/ajohnson.jpg"
    }
  ],
  "message": "Tr Alex Johnson Added Successfully!",
  "isSuccessful": true
}
```

#### POST /api/v1/Teachers/CreateNewTeacher

Creates a new teacher profile and returns only the new teacher.

**Request:**
```json
{
  "teachersName": "Sarah Wilson",
  "profilePictureUrl": "https://example.com/profiles/swilson.jpg"
}
```

**Response:**
```json
{
  "data": {
    "id": 4,
    "teachersName": "Sarah Wilson",
    "profilePictureUrl": "https://example.com/profiles/swilson.jpg"
  },
  "message": "4",
  "isSuccessful": true
}
```

#### POST /api/v1/Teachers/AddQuestionForTeacher

Adds a question to a teacher's collection.

**Request:**
```json
{
  "teacherId": 1,
  "sessionId": 2,
  "actualQuestion": "What is 2+2?",
  "firstAnswer": "3",
  "secondAnswer": "4",
  "thirdAnswer": "5",
  "correctAnswerIndex": 1,
  "subject": "MATH"
}
```

**Response:**
```json
{
  "data": [
    {
      "id": 1,
      "teachersName": "John Smith",
      "profilePictureUrl": "https://example.com/profiles/jsmith.jpg",
      "questions": [
        {
          "actualQuestion": "What is 2+2?",
          "firstAnswer": "3",
          "secondAnswer": "4",
          "thirdAnswer": "5",
          "correctAnswerIndex": 1,
          "subject": "MATH"
        }
      ]
    }
  ],
  "message": "Question for Tr John Smith Added Successfully!",
  "isSuccessful": true
}
```

#### GET /api/v1/Teachers/GetQuestionForTeacher/{teachersName}

Retrieves all questions for a specific teacher.

**Parameters:**
- `teachersName` (path parameter): The name of the teacher

**Response:**
```json
{
  "data": [
    {
      "actualQuestion": "What is 2+2?",
      "firstAnswer": "3",
      "secondAnswer": "4",
      "thirdAnswer": "5",
      "correctAnswerIndex": 1,
      "subject": "MATH"
    },
    {
      "actualQuestion": "What is the capital of France?",
      "firstAnswer": "London",
      "secondAnswer": "Paris",
      "thirdAnswer": "Berlin",
      "correctAnswerIndex": 1,
      "subject": "ENGLISH"
    }
  ],
  "message": "Successfully retrieved questions for Tr John Smith",
  "isSuccessful": true
}
```

#### GET /api/v1/Teachers/GetSessionQuestionsForTeacher/{teachersId}/{sessionId}/subject

Retrieves session-specific questions for a teacher with optional subject filtering.

**Parameters:**
- `teachersId` (path parameter): The ID of the teacher
- `sessionId` (path parameter): The ID of the session
- `subject` (query parameter): MATH, ENGLISH, or SCIENCE

**Response:**
```json
{
  "data": [
    {
      "actualQuestion": "What is 2+2?",
      "firstAnswer": "3",
      "secondAnswer": "4",
      "thirdAnswer": "5",
      "correctAnswerIndex": 1,
      "subject": "MATH"
    }
  ],
  "message": "Successfully retrieved questions for Tr 0",
  "isSuccessful": true
}
```

### Session Management

#### GET /api/v1/Session/GetAllSessions

Retrieves all game sessions.

**Response:**
```json
{
  "data": [
    {
      "sessionId": 1,
      "sessionName": "Math Quiz - Spring 2025",
      "sessionSubject": "MATH",
      "teacherId": 1
    },
    {
      "sessionId": 2,
      "sessionName": "English Literature Review",
      "sessionSubject": "ENGLISH",
      "teacherId": 2
    }
  ],
  "message": "Successfully Retrieved all Sessions",
  "isSuccessful": true
}
```

#### POST /api/v1/Session/CreateSession

Creates a new game session.

**Request:**
```json
{
  "sessionName": "Science Lab Quiz",
  "sessionSubject": "SCIENCE",
  "teacherId": 1
}
```

**Response:**
```json
{
  "data": {
    "sessionId": 3,
    "sessionName": "Science Lab Quiz",
    "sessionSubject": "SCIENCE",
    "teacherId": 1
  },
  "message": "",
  "isSuccessful": true
}
```

### AI Integration

#### POST /api/v1/AI/GenerateQuestions

Generates questions using AI based on topic and subject.

**Request:**
```json
{
  "subject": "MATH",
  "topic": "Algebra",
  "difficulty": "Medium",
  "count": 5,
  "teacherId": 1,
  "sessionId": 2
}
```

**Response:**
```json
{
  "data": [
    {
      "actualQuestion": "Solve for x: 2x + 5 = 15",
      "firstAnswer": "x = 5",
      "secondAnswer": "x = 10",
      "thirdAnswer": "x = 7.5",
      "correctAnswerIndex": 0,
      "subject": "MATH",
      "id": 101
    },
    {
      "actualQuestion": "Factor the expression: x² - 9",
      "firstAnswer": "(x-3)(x+3)",
      "secondAnswer": "(x-9)(x+1)",
      "thirdAnswer": "x(x-9)",
      "correctAnswerIndex": 0,
      "subject": "MATH",
      "id": 102
    }
    // Additional questions...
  ],
  "message": "Successfully generated 5 MATH questions on Algebra",
  "isSuccessful": true
}
```
    {
      "id": 1,
      "teachersName": "John Smith",
      "profilePictureUrl": "https://example.com/profiles/jsmith.jpg",
      "dateCreated": "2025-03-15T10:30:00",
      "lastUpdated": "2025-03-15T10:30:00"
    },
    {
      "id": 2,
      "teachersName": "Jane Doe",
      "profilePictureUrl": "https://example.com/profiles/jdoe.jpg",
      "dateCreated": "2025-03-16T14:20:00",
      "lastUpdated": "2025-03-16T14:20:00"
    }
  ],
  "message": "Success Retrieving all teachers",
  "isSuccessful": true
}
```

#### GET /api/v1/Teachers/GetTeachersWithSubject/{subject}

Retrieves teachers who have created questions in a specific subject.

**Parameters:**
- `subject` (path parameter): MATH, ENGLISH, or SCIENCE

**Response:**
```json
{
  "data": [
    {
      "id": 1,
      "teachersName": "John Smith",
      "profilePictureUrl": "https://example.com/profiles/jsmith.jpg"
    }
  ],
  "message": "Success Retrieving all teachers",
  "isSuccessful": true
}
```

#### POST /api/v1/Teachers/CreateTeacher

Creates a new teacher profile and returns all teachers.

**Request:**
```json
{
  "teachersName": "Alex Johnson",
  "profilePictureUrl": "https://example.com/profiles/ajohnson.jpg"
}
```

**Response:**
```json
{
  "data": [

## Technology Stack

### Backend
- **ASP.NET Core**: Web API framework for building RESTful services
- **Entity Framework Core**: ORM for database operations and data modeling
- **AutoMapper**: Object-to-object mapping for DTO transformations
- **SQL Server**: Relational database (hosted on Azure)
- **Azure App Service**: Cloud hosting platform for the API
- **Azure Cognitive Services**: Integration for AI-powered question generation

### Frontend/Client
- **Unity Engine**: Game development platform for creating interactive educational games
- **C#**: Scripting language for Unity game logic
- **Unity WebGL**: Web deployment of games for browser-based play
- **Unity UI Framework**: For building game interfaces and question displays

## Setup and Deployment

### Prerequisites

- **.NET 6.0 SDK** or later for API development
- **SQL Server** (local or Azure SQL) for database operations
- **Visual Studio 2022** or **Visual Studio Code** with C# extensions
- **Unity Editor 2021.3 LTS** or later for game client development
- **Azure Subscription** for cloud hosting and services
- **Azure Cognitive Services API Key** for AI question generation

### API Configuration

The application uses `appsettings.json` for configuration:

```json
{
  "ConnectionStrings": {
    "DatabaseConnection": "Server=your-server;Database=GamePortalDB;User Id=your-user;Password=your-password;"
  },
  "AzureAI": {
    "CognitiveServicesEndpoint": "https://your-cognitive-services.cognitiveservices.azure.com/",
    "ApiKey": "your-cognitive-services-api-key",
    "Region": "eastus"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "Cors": {
    "AllowedOrigins": [
      "https://yourgameportal.com",
      "https://dev.yourgameportal.com"
    ]
  }
}
```

### Database Migration

Run the following commands to create or update the database:

```bash
# Add a new migration
dotnet ef migrations add InitialCreate --project GamePortalAPI

# Apply migrations to the database
dotnet ef database update --project GamePortalAPI

# For production deployment
dotnet ef database update --connection "Server=production-server;Database=GamePortalDB;User Id=prod-user;Password=secure-password;"
```

### Local Development

#### API Setup

```bash
# Clone the repository
git clone https://github.com/yourusername/game-portal-api.git

# Navigate to the project folder
cd game-portal-api

# Restore dependencies
dotnet restore

# Run the application
dotnet run --project GamePortalAPI
```

#### Unity Client Setup

1. Open Unity Hub and add the `GamePortalClient` project
2. Open the project in Unity Editor
3. Navigate to `Assets/Scripts/Config`
4. Update the `ApiEndpoint.cs` file with your local API endpoint:

```csharp
public static class ApiEndpoint
{
    #if DEVELOPMENT
        public const string BaseUrl = "https://localhost:7123/api/v1/";
    #elif STAGING
        public const string BaseUrl = "https://gameportal-staging.azurewebsites.net/api/v1/";
    #else
        public const string BaseUrl = "https://gameportal.azurewebsites.net/api/v1/";
    #endif
}
```

5. Run the game in Unity Editor for testing

### Azure Deployment

#### API Deployment

1. Create an Azure App Service (Standard S1 tier or higher recommended)
   ```bash
   az group create --name GamePortalResourceGroup --location eastus
   az appservice plan create --name GamePortalAppPlan --resource-group GamePortalResourceGroup --sku S1
   az webapp create --name GamePortalAPI --resource-group GamePortalResourceGroup --plan GamePortalAppPlan
   ```

2. Set up an Azure SQL Database
   ```bash
   az sql server create --name gameportal-sql --resource-group GamePortalResourceGroup --admin-user adminuser --admin-password securePassword123!
   az sql db create --name GamePortalDB --resource-group GamePortalResourceGroup --server gameportal-sql --service-objective S1
   ```

3. Configure connection strings in Azure
   ```bash
   az webapp config connection-string set --name GamePortalAPI --resource-group GamePortalResourceGroup --connection-string-type SQLAzure --settings DatabaseConnection="Server=tcp:gameportal-sql.database.windows.net,1433;Database=GamePortalDB;User Id=adminuser;Password=securePassword123!;Encrypt=true;Connection Timeout=30;"
   ```

4. Deploy the API using Visual Studio publishing tools or Azure DevOps pipelines
   - Right-click the project in Visual Studio
   - Select "Publish"
   - Choose "Azure App Service" as the target
   - Select your subscription and App Service

#### Unity Game Deployment

1. Open the Unity project in Unity Editor
2. Set the build target to WebGL (File > Build Settings > WebGL)
3. Configure the build for Azure Web App:
   - Set compression format to gzip
   - Enable "Decompression Fallback"
   - Set memory size to 512MB or higher

4. Build the project:
   ```
   File > Build Settings > Build
   ```

5. Deploy the WebGL build to Azure Static Web App or Azure Storage with CDN
   ```bash
   # Example using Azure CLI to deploy to Azure Storage
   az storage blob upload-batch --account-name gameportalstatic --account-key YOUR_ACCOUNT_KEY --destination '$web' --source ./WebGLBuild
   ```

### CI/CD Pipeline Setup

For automated deployments, set up Azure DevOps pipelines:

1. Create `azure-pipelines.yml` in your repository:

```yaml
trigger:
- main

pool:
  vmImage: 'ubuntu-latest'

variables:
  buildConfiguration: 'Release'
  dotnetSdkVersion: '6.0.x'

stages:
- stage: Build
  jobs:
  - job: BuildAPI
    steps:
    - task: UseDotNet@2
      inputs:
        packageType: 'sdk'
        version: '$(dotnetSdkVersion)'
    - script: dotnet restore
      displayName: 'Restore NuGet packages'
    - script: dotnet build --configuration $(buildConfiguration)
      displayName: 'Build .NET project'
    - script: dotnet test
      displayName: 'Run unit tests'
    - task: DotNetCoreCLI@2
      inputs:
        command: 'publish'
        publishWebProjects: true
        arguments: '--configuration $(buildConfiguration) --output $(Build.ArtifactStagingDirectory)'
    - task: PublishBuildArtifacts@1
      inputs:
        pathToPublish: '$(Build.ArtifactStagingDirectory)'
        artifactName: 'api'

- stage: Deploy
  dependsOn: Build
  jobs:
  - job: DeployAPI
    steps:
    - task: DownloadBuildArtifacts@0
      inputs:
        buildType: 'current'
        downloadType: 'single'
        artifactName: 'api'
        downloadPath: '$(System.ArtifactsDirectory)'
    - task: AzureRmWebAppDeployment@4
      inputs:
        ConnectionType: 'AzureRM'
        azureSubscription: 'Your-Azure-Subscription'
        appType: 'webApp'
        WebAppName: 'GamePortalAPI'
        packageForLinux: '$(System.ArtifactsDirectory)/api/*.zip'
```

2. Configure Azure DevOps to use this pipeline for automated deployments

## AI-Powered Question Generation

The Game Portal API integrates advanced AI capabilities for question generation, allowing teachers to:

1. **Topic-Based Generation**: Generate questions by specifying a subject and topic
2. **Difficulty Adjustment**: Set the desired difficulty level for generated questions
3. **Question Review & Editing**: Review, edit, and enhance AI-generated questions
4. **Bulk Generation**: Create multiple questions at once to populate game sessions quickly
5. **Custom Answer Options**: Generate plausible but incorrect answer options for multiple-choice questions

### AI Generation Workflow

1. Teacher selects a subject (Math, English, Science) and specific topic
2. Teacher specifies the number and difficulty of questions to generate
3. The API processes the request and generates questions using Azure AI services
4. Generated questions are presented to the teacher for review
5. Teacher can edit, enhance, or reject questions before finalizing
6. Approved questions are saved to the database and associated with the teacher's profile
7. Questions become available for inclusion in game sessions

## Unity Game Integration

The Game Portal API is designed to work seamlessly with Unity-based educational games. The integration allows for a rich, interactive learning experience while maintaining a robust backend for content management.

### Unity Client Architecture

The Unity client follows a modular architecture:

```
GamePortalClient/
├── Assets/
│   ├── Scripts/
│   │   ├── API/                 # API communication components
│   │   │   ├── ApiClient.cs     # Core API client
│   │   │   ├── SessionApi.cs    # Session-specific API calls
│   │   │   └── TeacherApi.cs    # Teacher-specific API calls
│   │   ├── Models/              # Data models matching API DTOs
│   │   │   ├── QuestionModel.cs
│   │   │   ├── SessionModel.cs
│   │   │   └── TeacherModel.cs
│   │   ├── UI/                  # UI components and controllers
│   │   │   ├── QuestionPanel.cs
│   │   │   ├── TeacherProfile.cs
│   │   │   └── GameHUD.cs
│   │   ├── Game/                # Game mechanics
│   │   │   ├── GameManager.cs
│   │   │   ├── ScoreManager.cs
│   │   │   └── TimeManager.cs
│   │   └── Utils/               # Utility classes
│   │       ├── JsonHelper.cs
│   │       └── ServiceResponseParser.cs
│   ├── Prefabs/                 # Reusable game objects
│   ├── Scenes/                  # Game scenes
│   └── Resources/               # Static resources
└── ProjectSettings/             # Unity project settings
```

### Integration Components

1. **RESTful API Integration**
   - Unity games communicate with the backend via RESTful API calls using UnityWebRequest
   - JSON serialization/deserialization for API data exchange
   - Custom ServiceResponse parser for handling standardized API responses

2. **Authentication Flow**
   - Secure authentication for teachers and students within the Unity client
   - Token-based authentication with JWT storage
   - Session management and token refresh functionality

3. **Question Management**
   - Dynamic question fetching based on session IDs and subjects
   - Local caching for performance optimization
   - Support for different question types and media content

4. **Game Interaction**
   - Students submit answers through the Unity interface to the API
   - Real-time feedback with answer validation
   - Score tracking and performance metrics

5. **Progress Tracking**
   - Game progress and student performance data are sent back to the API
   - Analytics for teacher review and student assessment
   - Persistent progress across game sessions

6. **Real-time Updates**
   - Changes made by teachers are reflected in the games immediately
   - Websocket integration for live session updates
   - Notification system for new content availability

### Unity Integration Examples

#### API Client Implementation

```csharp
// Core API client for handling requests to the Game Portal API
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Threading.Tasks;

public class GameAPIClient : MonoBehaviour
{
    private string baseUrl = "https://gameportalapi.azurewebsites.net/api/v1/";
    private string authToken = string.Empty;
    
    // Fetch questions for a specific session
    public IEnumerator GetSessionQuestions(int teacherId, int sessionId, string subject)
    {
        string url = $"{baseUrl}Teachers/GetSessionQuestionsForTeacher/{teacherId}/{sessionId}/{subject}";
        
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            // Add authorization header if authenticated
            if (!string.IsNullOrEmpty(authToken))
            {
                webRequest.SetRequestHeader("Authorization", $"Bearer {authToken}");
            }
            
            yield return webRequest.SendWebRequest();
            
            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = webRequest.downloadHandler.text;
                ServiceResponse<List<QuestionModel>> response = 
                    JsonConvert.DeserializeObject<ServiceResponse<List<QuestionModel>>>(jsonResponse);
                
                if (response.IsSuccessful && response.Data != null)
                {
                    // Cache questions locally
                    QuestionCache.Instance.StoreQuestions(sessionId, response.Data);
                    
                    // Notify game manager that questions are ready
                    GameManager.Instance.OnQuestionsLoaded(response.Data);
                }
                else
                {
                    Debug.LogError($"API Error: {response.Message}");
                    UIManager.Instance.ShowError($"Failed to load questions: {response.Message}");
                }
            }
            else
            {
                Debug.LogError($"Network Error: {webRequest.error}");
                UIManager.Instance.ShowError("Network error. Please check your connection.");
            }
        }
    }
    
    // Submit a student's answer to a question
    public IEnumerator SubmitAnswer(int questionId, int answerIndex, float timeSpent)
    {
        string url = $"{baseUrl}Students/SubmitAnswer";
        
        AnswerSubmissionModel submission = new AnswerSubmissionModel
        {
            QuestionId = questionId,
            SelectedAnswerIndex = answerIndex,
            TimeSpentInSeconds = timeSpent,
            SubmissionTimestamp = System.DateTime.UtcNow
        };
        
        string jsonData = JsonConvert.SerializeObject(submission);
        
        using (UnityWebRequest webRequest = UnityWebRequest.Post(url, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");
            
            if (!string.IsNullOrEmpty(authToken))
            {
                webRequest.SetRequestHeader("Authorization", $"Bearer {authToken}");
            }
            
            yield return webRequest.SendWebRequest();
            
            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = webRequest.downloadHandler.text;
                ServiceResponse<AnswerResultModel> response = 
                    JsonConvert.DeserializeObject<ServiceResponse<AnswerResultModel>>(jsonResponse);
                
                if (response.IsSuccessful && response.Data != null)
                {
                    // Update score and show feedback
                    ScoreManager.Instance.UpdateScore(response.Data.IsCorrect, response.Data.PointsAwarded);
                    GameManager.Instance.ShowAnswerFeedback(response.Data);
                }
                else
                {
                    Debug.LogError($"API Error: {response.Message}");
                }
            }
            else
            {
                Debug.LogError($"Network Error: {webRequest.error}");
            }
        }
    }
    
    // Create a new game session from Unity teacher portal
    public IEnumerator CreateGameSession(string sessionName, string subject, int teacherId, List<int> questionIds)
    {
        string url = $"{baseUrl}Session/CreateSession";
        
        CreateSessionRequestModel sessionRequest = new CreateSessionRequestModel
        {
            SessionName = sessionName,
            SessionSubject = subject,
            TeacherId = teacherId,
            QuestionIds = questionIds
        };
        
        string jsonData = JsonConvert.SerializeObject(sessionRequest);
        
        using (UnityWebRequest webRequest = UnityWebRequest.Post(url, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.SetRequestHeader("Authorization", $"Bearer {authToken}");
            
            yield return webRequest.SendWebRequest();
            
            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = webRequest.downloadHandler.text;
                ServiceResponse<SessionModel> response = 
                    JsonConvert.DeserializeObject<ServiceResponse<SessionModel>>(jsonResponse);
                
                if (response.IsSuccessful && response.Data != null)
                {
                    TeacherUIManager.Instance.OnSessionCreated(response.Data);
                    Debug.Log($"Created session: {response.Data.SessionName} with ID: {response.Data.SessionId}");
                }
                else
                {
                    Debug.LogError($"API Error: {response.Message}");
                    TeacherUIManager.Instance.ShowError($"Failed to create session: {response.Message}");
                }
            }
            else
            {
                Debug.LogError($"Network Error: {webRequest.error}");
                TeacherUIManager.Instance.ShowError("Network error. Please check your connection.");
            }
        }
    }
    
    // Get available AI-generated questions based on a topic
    public IEnumerator GetAIGeneratedQuestions(string subject, string topic, string difficulty, int count)
    {
        string url = $"{baseUrl}AI/GenerateQuestions";
        
        AIQuestionRequestModel requestModel = new AIQuestionRequestModel
        {
            Subject = subject,
            Topic = topic,
            Difficulty = difficulty,
            Count = count
        };
        
        string jsonData = JsonConvert.SerializeObject(requestModel);
        
        using (UnityWebRequest webRequest = UnityWebRequest.Post(url, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.SetRequestHeader("Authorization", $"Bearer {authToken}");
            
            yield return webRequest.SendWebRequest();
            
            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = webRequest.downloadHandler.text;
                ServiceResponse<List<QuestionModel>> response = 
                    JsonConvert.DeserializeObject<ServiceResponse<List<QuestionModel>>>(jsonResponse);
                
                if (response.IsSuccessful && response.Data != null)
                {
                    AIQuestionManager.Instance.DisplayGeneratedQuestions(response.Data);
                }
                else
                {
                    Debug.LogError($"AI Generation Error: {response.Message}");
                    TeacherUIManager.Instance.ShowError($"AI couldn't generate questions: {response.Message}");
                }
            }
            else
            {
                Debug.LogError($"Network Error: {webRequest.error}");
                TeacherUIManager.Instance.ShowError("Network error during AI question generation.");
            }
        }
    }
}
```

## AI Integration for Question Generation

The Game Portal API includes sophisticated AI capabilities for generating educational questions, providing teachers with powerful tools to create engaging content efficiently.

### AI Question Generation Flow

```
Teacher Input → AI Processing → Question Generation → Teacher Review → Refinement → Final Questions
```

1. **Teacher Input**
   - Subject selection (Math, English, Science)
   - Topic specification (e.g., "Algebra - Linear Equations")
   - Difficulty level (Easy, Medium, Hard)
   - Number of questions needed
   - Optional keywords or concepts to include

2. **AI Processing**
   - Natural language processing of the topic
   - Educational curriculum alignment
   - Age-appropriate content generation
   - Pedagogical best practices application

3. **Question Generation**
   - Multiple-choice question creation
   - Plausible distractor answers
   - Varying question complexity based on difficulty
   - Explanations for correct answers

4. **Teacher Review Interface**
   - Preview generated questions
   - Edit question text directly
   - Modify answer options
   - Adjust difficulty or complexity
   - Accept/reject individual questions

5. **AI Refinement**
   - Learning from teacher edits
   - Improving future question generation
   - Adapting to teaching style preferences

### Technical Implementation

The AI question generation system leverages Azure Cognitive Services:

1. **Language Understanding**
   - Topic analysis using natural language understanding
   - Educational concept mapping
   - Curriculum standards alignment

2. **Content Generation**
   - GPT-based question formulation
   - Educational context-aware generation
   - Controlled difficulty scaling

3. **Answer Generation**
   - Correct answer determination
   - Plausible distractor creation
   - Educational feedback formulation

4. **Integration Points**
   - REST API endpoints for generation requests
   - Webhook callbacks for long-running processes
   - Caching for similar question requests

### Example AI Generation Request

```json
POST /api/v1/AI/GenerateQuestions
{
  "subject": "MATH",
  "topic": "Quadratic Equations",
  "difficulty": "Medium",
  "gradeLevel": "9",
  "count": 5,
  "keywords": ["factoring", "completing the square", "quadratic formula"],
  "excludeTopics": ["complex numbers"],
  "teacherId": 42
}
```

### Example AI Generation Response

```json
{
  "data": [
    {
      "actualQuestion": "Which of the following is a solution to the quadratic equation x² - 5x + 6 = 0?",
      "firstAnswer": "x = 2",
      "secondAnswer": "x = 3",
      "thirdAnswer": "x = 4",
      "correctAnswerIndex": 0,
      "subject": "MATH",
      "difficulty": "Medium",
      "explanation": "To solve x² - 5x + 6 = 0, factor into (x-2)(x-3)=0, which gives solutions x=2 or x=3. Only x=2 is listed among the options.",
      "conceptTags": ["factoring", "roots", "quadratic equations"]
    },
    {
      "actualQuestion": "Which method is most efficient to solve the quadratic equation 2x² + 7x - 4 = 0?",
      "firstAnswer": "Factoring",
      "secondAnswer": "Completing the square",
      "thirdAnswer": "Quadratic formula",
      "correctAnswerIndex": 0,
      "subject": "MATH",
      "difficulty": "Medium",
      "explanation": "This equation can be factored as (2x-1)(x+4)=0, making factoring the most efficient method compared to completing the square or using the quadratic formula.",
      "conceptTags": ["factoring", "problem-solving strategies", "quadratic equations"]
    }
    // Additional questions...
  ],
  "message": "Successfully generated 5 MATH questions on Quadratic Equations",
  "isSuccessful": true,
  "generationMetrics": {
    "timeElapsed": "2.4 seconds",
    "difficultyDistribution": {
      "easy": 0,
      "medium": 5,
      "hard": 0
    },
    "conceptCoverage": ["factoring", "quadratic formula", "roots", "problem-solving strategies"]
  }
}
```

## Future Enhancements

### 1. Advanced AI Capabilities
- **Multimodal Questions**: Generate questions with images, diagrams, and multimedia elements
- **Adaptive Learning**: AI-powered difficulty adjustment based on student performance
- **Personalized Question Generation**: Create questions tailored to individual student needs
- **Question Quality Analysis**: AI evaluation of question effectiveness and discrimination

### 2. Student Experience
- **Student Authentication & Profiles**: Comprehensive user management for students
- **Progress Tracking**: Detailed tracking of student performance and improvement over time
- **Personalized Learning Paths**: Custom educational journeys based on performance
- **Parent Portal**: Access for parents to monitor progress and engagement

### 3. Multiplayer & Gamification
- **Real-time Multiplayer**: SignalR implementation for synchronous multiplayer games
- **Classroom Competitions**: Teacher-moderated competitive game formats
- **Achievements System**: Badges, levels, and rewards for student accomplishments
- **Leaderboards**: Optional competitive elements with privacy controls

### 4. Analytics & Insights
- **Teacher Dashboard**: Comprehensive analytics on student performance
- **Question Effectiveness**: Metrics on question difficulty and discrimination
- **Student Progress Visualization**: Visual representations of learning progress
- **AI-Powered Insights**: Automatic identification of learning gaps and recommendations

### 5. Platform Expansion
- **Mobile Applications**: Native mobile apps for iOS and Android
- **Offline Mode**: Support for low-connectivity environments
- **Custom Game Templates**: Teacher-created game formats and mechanics
- **Content Marketplace**: Exchange platform for teachers to share and discover content

### 6. Administration & Management
- **School Administrator Portal**: School-wide management and reporting
- **Curriculum Alignment**: Mapping questions to educational standards
- **Multi-language Support**: Internationalization and localization
- **Accessibility Enhancements**: WCAG compliance and assistive technology support

#### Game Manager Implementation

```csharp
// Game manager for orchestrating the educational game experience
using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [SerializeField] private QuestionPanel questionPanel;
    [SerializeField] private ScoreDisplay scoreDisplay;
    [SerializeField] private FeedbackPanel feedbackPanel;
    [SerializeField] private TimerDisplay timerDisplay;
    
    private List<QuestionModel> currentQuestions = new List<QuestionModel>();
    private int currentQuestionIndex = 0;
    private float questionStartTime;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        // Load configuration from PlayerPrefs or server
        int teacherId = PlayerPrefs.GetInt("TeacherId", 1);
        int sessionId = PlayerPrefs.GetInt("SessionId", 1);
        string subject = PlayerPrefs.GetString("Subject", "MATH");
        
        // Request questions from API
        StartCoroutine(FindObjectOfType<GameAPIClient>().GetSessionQuestions(
            teacherId, sessionId, subject));
    }
    
    // Called when questions are loaded from the API
    public void OnQuestionsLoaded(List<QuestionModel> questions)
    {
        currentQuestions = questions;
        
        if (currentQuestions.Count > 0)
        {
            StartGame();
        }
        else
        {
            UIManager.Instance.ShowError("No questions available for this session.");
        }
    }
    
    private void StartGame()
    {
        currentQuestionIndex = 0;
        ScoreManager.Instance.ResetScore();
        DisplayCurrentQuestion();
    }
    
    private void DisplayCurrentQuestion()
    {
        if (currentQuestionIndex < currentQuestions.Count)
        {
            QuestionModel question = currentQuestions[currentQuestionIndex];
            questionPanel.DisplayQuestion(question);
            timerDisplay.StartTimer();
            questionStartTime = Time.time;
        }
        else
        {
            // Game complete
            ShowGameSummary();
        }
    }
    
    public void OnAnswerSelected(int answerIndex)
    {
        float timeSpent = Time.time - questionStartTime;
        timerDisplay.StopTimer();
        
        QuestionModel question = currentQuestions[currentQuestionIndex];
        
        // Submit answer to the API
        StartCoroutine(FindObjectOfType<GameAPIClient>().SubmitAnswer(
            question.QuestionId, answerIndex, timeSpent));
    }
    
    public void ShowAnswerFeedback(AnswerResultModel result)
    {
        feedbackPanel.ShowFeedback(result.IsCorrect, result.CorrectAnswerIndex, result.Explanation);
        
        // Wait for feedback acknowledgment or auto-proceed after delay
        Invoke("ProceedToNextQuestion", 3.0f);
    }
    
    private void ProceedToNextQuestion()
    {
        currentQuestionIndex++;
        DisplayCurrentQuestion();
    }
    
    private void ShowGameSummary()
    {
        // Display game completion panel with score and stats
        UIManager.Instance.ShowGameSummary(ScoreManager.Instance.CurrentScore, currentQuestions.Count);
        
        // Submit game results to the API
        StartCoroutine(FindObjectOfType<GameAPIClient>().SubmitGameResults(
            PlayerPrefs.GetInt("SessionId", 1),
            ScoreManager.Instance.CurrentScore,
            ScoreManager.Instance.CorrectAnswers,
            ScoreManager.Instance.GetAverageResponseTime()
        ));
    }
}
```

#### AI Question Manager

```csharp
// AI Question Manager for handling AI-generated question reviews
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class AIQuestionManager : MonoBehaviour
{
    public static AIQuestionManager Instance { get; private set; }
    
    [SerializeField] private GameObject questionItemPrefab;
    [SerializeField] private Transform questionContainer;
    [SerializeField] private Button generateButton;
    [SerializeField] private Button saveAllButton;
    [SerializeField] private InputField topicInput;
    [SerializeField] private Dropdown subjectDropdown;
    [SerializeField] private Dropdown difficultyDropdown;
    [SerializeField] private Slider countSlider;
    [SerializeField] private Text countLabel;
    
    private List<QuestionModel> generatedQuestions = new List<QuestionModel>();
    private List<QuestionItemController> questionItems = new List<QuestionItemController>();
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        generateButton.onClick.AddListener(OnGenerateClick);
        saveAllButton.onClick.AddListener(OnSaveAllClick);
        countSlider.onValueChanged.AddListener(OnCountChanged);
        
        // Initialize with default values
        OnCountChanged(countSlider.value);
    }
    
    private void OnCountChanged(float value)
    {
        int count = Mathf.RoundToInt(value);
        countLabel.text = count.ToString();
    }
    
    private void OnGenerateClick()
    {
        // Clear previous questions
        ClearQuestionItems();
        
        string subject = subjectDropdown.options[subjectDropdown.value].text;
        string topic = topicInput.text;
        string difficulty = difficultyDropdown.options[difficultyDropdown.value].text;
        int count = Mathf.RoundToInt(countSlider.value);
        
        if (string.IsNullOrEmpty(topic))
        {
            UIManager.Instance.ShowError("Please enter a topic.");
            return;
        }
        
        // Disable controls during generation
        SetControlsInteractable(false);
        
        // Request AI-generated questions
        StartCoroutine(FindObjectOfType<GameAPIClient>().GetAIGeneratedQuestions(
            subject, topic, difficulty, count));
    }
    
    public void DisplayGeneratedQuestions(List<QuestionModel> questions)
    {
        generatedQuestions = questions;
        
        foreach (var question in questions)
        {
            // Instantiate question item prefab
            GameObject itemGO = Instantiate(questionItemPrefab, questionContainer);
            QuestionItemController item = itemGO.GetComponent<QuestionItemController>();
            
            // Setup the item with question data
            item.SetQuestion(question);
            questionItems.Add(item);
        }
        
        // Re-enable controls
        SetControlsInteractable(true);
    }
    
    private void OnSaveAllClick()
    {
        List<QuestionModel> questionsToSave = new List<QuestionModel>();
        
        foreach (var item in questionItems)
        {
            if (item.IsSelected)
            {
                questionsToSave.Add(item.GetUpdatedQuestion());
            }
        }
        
        if (questionsToSave.Count == 0)
        {
            UIManager.Instance.ShowError("Please select at least one question to save.");
            return;
        }
        
        // Save selected questions
        int teacherId = PlayerPrefs.GetInt("TeacherId", 1);
        int sessionId = PlayerPrefs.GetInt("CurrentSessionId", 0);
        
        StartCoroutine(FindObjectOfType<GameAPIClient>().SaveQuestions(
            teacherId, sessionId, questionsToSave));
    }
    
    private void ClearQuestionItems()
    {
        foreach (var item in questionItems)
        {
            Destroy(item.gameObject);
        }
        
        questionItems.Clear();
    }
    
    private void SetControlsInteractable(bool interactable)
    {
        generateButton.interactable = interactable;
        saveAllButton.interactable = interactable;
        topicInput.interactable = interactable;
        subjectDropdown.interactable = interactable;
        difficultyDropdown.interactable = interactable;
        countSlider.interactable = interactable;
    }
}
```

## Contributing

### How to Contribute

We welcome contributions to the Game Portal API project! To contribute:

1. Fork the repository
2. Create a feature branch: `git checkout -b feature/your-feature-name`
3. Commit your changes: `git commit -m 'Add some feature'`
4. Push to the branch: `git push origin feature/your-feature-name`
5. Submit a pull request

### Development Guidelines

- Follow C# coding conventions and naming standards
- Add XML documentation comments to all public methods and classes
- Write unit tests for new features
- Update documentation for API changes
- Follow RESTful API design principles for new endpoints

### Bug Reports

If you find a bug, please create an issue with:

1. A clear title and description
2. Steps to reproduce the bug
3. Expected and actual behavior
4. Screenshots or code samples if applicable

## Performance Considerations

### API Optimization

The Game Portal API is designed with performance in mind, implementing several optimizations:

1. **Database Indexing**
   - Indexes on frequently queried columns (TeacherId, SessionId, Subject)
   - Composite indexes for complex queries
   - Regular maintenance of index statistics

2. **Response Caching**
   - Client-side caching of infrequently changing data
   - Server-side caching for common queries
   - ETags for efficient cache validation

3. **Pagination**
   - Limit large result sets with pagination
   - Use Skip/Take parameters for large collections
   - Return total count for proper pagination UI

4. **Eager Loading**
   - Use Entity Framework Include() to avoid N+1 query problems
   - Select only required fields for specific operations
   - Balance between over-fetching and under-fetching

5. **Async/Await Pattern**
   - All repository and service methods use async/await
   - Non-blocking I/O operations for improved throughput
   - Task-based asynchronous pattern throughout the codebase

### Azure Scaling

The system leverages Azure's scaling capabilities:

1. **Horizontal Scaling**
   - Multiple App Service instances behind a load balancer
   - Auto-scaling based on CPU utilization and request queue length
   - Stateless design for seamless scaling

2. **Database Scaling**
   - Azure SQL elastic pools for cost-effective database scaling
   - Read replicas for high-read scenarios
   - Periodic performance tuning

3. **CDN Integration**
   - Static assets served through Azure CDN
   - Reduced latency for global users
   - Edge caching for improved performance

## Security Considerations

### Data Protection

1. **Authentication & Authorization**
   - JWT-based authentication
   - Role-based access control (RBAC)
   - Token expiration and refresh mechanisms

2. **Data Encryption**
   - TLS/SSL for all API communications
   - Encryption at rest for sensitive data
   - Secure connection strings and credential storage

3. **Input Validation**
   - Request model validation
   - Sanitization of user inputs
   - Protection against common injection attacks

4. **Secure Development Practices**
   - Regular security code reviews
   - Dependency scanning for vulnerabilities
   - OWASP security guidelines implementation

### Student Data Protection

The system is designed with student privacy in mind:

1. **Data Minimization**
   - Collection of only necessary student information
   - Anonymization of performance data when possible
   - Configurable data retention policies

2. **Access Controls**
   - Granular permissions for teacher access to student data
   - Audit logging for sensitive data access
   - Parental controls and visibility options

## Monitoring and Maintenance

### Application Insights Integration

The API integrates with Azure Application Insights for comprehensive monitoring:

1. **Performance Monitoring**
   - Request timing and dependency tracking
   - Server response time metrics
   - Database query performance analysis

2. **Error Tracking**
   - Exception logging and alerting
   - Error rate monitoring
   - Custom event tracking for business processes

3. **Usage Analytics**
   - User behavior tracking
   - Feature usage statistics
   - Session duration and engagement metrics

### Logging Strategy

The system implements a structured logging approach:

1. **Log Levels**
   - Error: For exceptions and critical failures
   - Warning: For potential issues that don't stop execution
   - Information: For significant application events
   - Debug: For detailed troubleshooting information

2. **Log Storage**
   - Short-term logs in Application Insights
   - Long-term archival in Azure Blob Storage
   - Log rotation and retention policies

3. **Alerting**
   - Real-time alerts for critical errors
   - Rate-based alerts for performance degradation
   - Custom thresholds for business metrics

## License

This project is licensed under the [MIT License](LICENSE).

Copyright (c) 2025 Game Portal Team

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
