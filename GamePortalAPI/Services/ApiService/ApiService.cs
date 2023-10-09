using System;
using GamePortalAPI.DTOs.ServiceResponse;
using GamePortalAPI.DTOs.TeacherDtos;
using GamePortalAPI.DTOs.QuestionDtos;
using AutoMapper;
using GamePortalAPI.Models;
using GamePortalAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace GamePortalAPI.Services.ApiService
{
	public class ApiService : IApiService
	{
        private readonly IMapper _mapper;
        private readonly DataContext _context;

		public ApiService(IMapper mapper, DataContext context)
		{
            _mapper = mapper;
            _context = context;
		}

        public async Task<ServiceResponse<List<GetTeacherResponseDto>>> CreateTeacher(AddTeacherRequestDto addTeacherRequestDto)
        {
            var serviceResponse = new ServiceResponse<List<GetTeacherResponseDto>>();

            var newTeacher =  _mapper.Map<Teacher>(addTeacherRequestDto);
            newTeacher.dateCreated = DateTime.Now;
            newTeacher.lastUpdated = DateTime.Now;

            await _context.AddAsync(newTeacher);

            await _context.SaveChangesAsync();

            var databaseTeachers = await _context.Teachers
                .Include(teacher => teacher.AllQuestions)
                .ToListAsync();

            serviceResponse.Data = databaseTeachers.Select(teacher => _mapper.Map<GetTeacherResponseDto>(teacher)).ToList();
            serviceResponse.Message = $"Tr {addTeacherRequestDto.TeachersName} Added Successfully!";
            serviceResponse.IsSuccessful = true;

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetTeacherResponseDto>>> AddQuestionForTeacher(AddQuestionRequestDto addQuestionRequestDto)
        {
            var serviceResponse = new ServiceResponse<List<GetTeacherResponseDto>>();

            var newQuestion = _mapper.Map<Question>(addQuestionRequestDto);
            newQuestion.dateCreated = DateTime.Now;
            newQuestion.lastUpdated = DateTime.Now;

            var teacherToAddQuestion = await _context.Teachers
                .Where(teacher => teacher.Id == addQuestionRequestDto.TeacherId)
                .FirstAsync();

            await _context.AddAsync(newQuestion);

            await _context.SaveChangesAsync();

            var databaseTeachers = await _context.Teachers
                .Include(teacher => teacher.AllQuestions)
                .ToListAsync();

            serviceResponse.Data = databaseTeachers.Select(teacher => _mapper.Map<GetTeacherResponseDto>(teacher)).ToList();
            serviceResponse.Message = $"Question for Tr {teacherToAddQuestion.TeachersName} Added Successfully!";
            serviceResponse.IsSuccessful = true;

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetTeacherResponseDto>>> GetAllTeachers()
        {
            var serviceResponse = new ServiceResponse<List<GetTeacherResponseDto>>();

            var databaseTeachers = await _context.Teachers
                .Include(teacher => teacher.AllQuestions)
                .ToListAsync();

            serviceResponse.Data = databaseTeachers.Select(teacher => _mapper.Map<GetTeacherResponseDto>(teacher)).ToList();
            serviceResponse.Message = "Success Retrieving all teachers";
            serviceResponse.IsSuccessful = true;

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetQuestionResponseDto>>> GetQuestionsForTeacher(string teachersName)
        {
            var serviceResponse = new ServiceResponse<List<GetQuestionResponseDto>>();
            Teacher teacher = new();

            try
            {
                teacher = await _context.Teachers
                .Where(teacher => teacher.TeachersName == teachersName)
                .Include(questions => questions.AllQuestions)
                .FirstAsync();

                serviceResponse.Data = teacher.AllQuestions?.Select(_mapper.Map<GetQuestionResponseDto>).ToList();
                serviceResponse.Message = $"Successfully retrieved questions for Tr {teachersName}";
                serviceResponse.IsSuccessful = true;
            }
            catch (Exception e)
            {
                serviceResponse.Data = null;
                serviceResponse.Message = "Unable to Get Questions Error is : " + e.Message;
                serviceResponse.IsSuccessful = false;
                return serviceResponse;
            }

            return serviceResponse;
        }
    }
}

