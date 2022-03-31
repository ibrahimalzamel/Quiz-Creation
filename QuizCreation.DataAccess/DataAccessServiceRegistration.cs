using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuizCreation.DataAccess.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizCreation.DataAccess.Abstract;

namespace QuizCreation.DataAccess
{
    public static class DataAccessServiceRegistration
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services, IConfiguration configuration)
        {
           
           
            services.AddScoped<IUserDal, EfUserRepository>();
            services.AddScoped<IExamDal, EfExamRepository>();
            services.AddScoped<IExamResultDal, EfExamResultRepository>();
            services.AddScoped<IQuestionAnswerDal, EfQuestionAnswerRepository>();
            services.AddScoped<IStudentDal, EfStudentRepository>();
            services.AddScoped<IGroupDal, EfGroupRepository>();


            return services;
        }
    }
}
