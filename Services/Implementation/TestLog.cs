using LoginApiTest.Models;
using LoginApiTest.Services.Contract;
using LoginApiTest.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace LoginApiTest.Services.Implementation
{
    public class TestLog : ITestLog
    {
        private readonly LoginContext _context;

        public TestLog(LoginContext context)
        {
            _context = context;
        }

        public UserResponseAPI AddUser(UserResponseAPI userResponse)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userResponse.PasswordHash);
            var newUser = new User
            {
                PasswordHash = hashedPassword,
                Username = userResponse.UserName,
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            userResponse.PasswordHash = hashedPassword;
            return userResponse;
        }

        public string CreateToken(UserResponseAPI token)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, token.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisismySecretKeyasdfghjklqwertyuiopzxcvbnm,.123456789sdfghjkvbnmcvbnmhftftftyfytyytyftf34w6red,"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var jwtToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMonths(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }

        public UserResponseAPI Verify(UserResponseAPI verifylogin)
        {
            var storedUser = _context.Users.FirstOrDefault(c => c.Username == verifylogin.UserName);
            if (storedUser == null)
                return new UserResponseAPI { UserName = "wrong" };

            if (!BCrypt.Net.BCrypt.Verify(verifylogin.PasswordHash, storedUser.PasswordHash))
                return new UserResponseAPI { UserName = "Wrong Password" };

            var token = CreateToken(verifylogin);
            return new UserResponseAPI
            {
                Id = storedUser.Id,
                UserName = storedUser.Username,
                Token = token,
                Log = "User verified successfully"
            };
        }

        public List<UserResponseAPI> GetList()
        {
            var users = _context.Users.Include(x => x.Enrollments).ToList();
            return users.Select(user => new UserResponseAPI
            {
                Id = user.Id,
                UserName = user.Username,
                PasswordHash = user.PasswordHash
            }).ToList();
        }

        public List<CourseAndEnroRespAPI.CourseDTO> GetAllCoursesWithEnrollments()
        {
            var courses = _context.Courses
                .Include(c => c.Enrollments)
                .ThenInclude(e => e.User)
                .ToList();

            return courses.Select(c => new CourseAndEnroRespAPI.CourseDTO
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Instructor = c.Instructor,
                Duration = c.Duration,
                Enrollments = c.Enrollments.Select(e => new CourseAndEnroRespAPI.EnrollmentDTO
                {
                    Id = e.Id,
                    CourseId = e.CourseId,
                    UserId = e.UserId,
                    DateEnrolled = e.DateEnrolled
                }).ToList()
            }).ToList();
        }
    }
}
