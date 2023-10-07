using System;
using GamePortalAPI.DTOs.ServiceResponse;
using GamePortalAPI.DTOs.TeacherDtos;
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

            var databaseTeachers = await _context.Teachers.ToListAsync();

            serviceResponse.Data = databaseTeachers.Select(teacher => _mapper.Map<GetTeacherResponseDto>(teacher)).ToList();
            serviceResponse.Message = $"Tr {addTeacherRequestDto.TeachersName} Added Successfully!";
            serviceResponse.IsSuccessful = true;

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetTeacherResponseDto>>> GetAllTeachers()
        {
            var serviceResponse = new ServiceResponse<List<GetTeacherResponseDto>>();

            var databaseTeachers = await _context.Teachers.ToListAsync();

            serviceResponse.Data = databaseTeachers.Select(teacher => _mapper.Map<GetTeacherResponseDto>(teacher)).ToList();
            serviceResponse.Message = "Success Retrieving all teachers";
            serviceResponse.IsSuccessful = true;

            return serviceResponse;
        }
    }
}

