using System;
using GamePortalAPI.DTOs.QuestionDtos;
using GamePortalAPI.DTOs.ServiceResponse;
using GamePortalAPI.DTOs.TeacherDtos;
using GamePortalAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace GamePortalAPI.Services.ApiService
{
	public interface IApiService
	{
        Task<ServiceResponse<List<GetTeacherResponseDto>>> GetAllTeachers();

        Task<ServiceResponse<List<GetTeacherResponseDto>>> CreateTeacher(AddTeacherRequestDto addTeacherRequestDto);

        Task<ServiceResponse<List<GetTeacherResponseDto>>> AddQuestionForTeacher(AddQuestionRequestDto addQuestionRequestDto);

        Task<ServiceResponse<List<GetQuestionResponseDto>>> GetQuestionsForTeacher(string teachersName);

        Task<ServiceResponse<List<GetQuestionResponseDto>>>
            GetSessionQuestionsForTeacher(int teachersId, int sessionId, Subject subject);

    }
}

