using LoginApiTest.Utilities;
using System.Collections.Generic;

namespace LoginApiTest.Services.Contract
{
    public interface ITestLog
    {
        UserResponseAPI AddUser(UserResponseAPI userResponse);
        string CreateToken(UserResponseAPI token);
        UserResponseAPI Verify(UserResponseAPI verifylogin);
        List<UserResponseAPI> GetList();
        List<CourseAndEnroRespAPI.CourseDTO> GetAllCoursesWithEnrollments();
    }
}
