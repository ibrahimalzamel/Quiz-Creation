using Microsoft.Extensions.DependencyInjection;
using QuizCreation.Business.Services.Account;
using QuizCreation.Business.Services.Exams;
using QuizCreation.Business.Services.Groups;
using QuizCreation.Business.Services.QuestionAnswers;
using QuizCreation.Business.Services.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QuizCreation.Business
{
    public static class BusinessServiceRegistraation
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            //services.AddAutoMapper(Assembly.GetExecutingAssembly());
            //services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddScoped<IExamService, ExamManager>();
            services.AddScoped<IGroupService, GroupManager>();
            services.AddScoped<IStudentService, StudentManager>();
            services.AddScoped<IAccountService, AccountManager>();
            services.AddScoped<IQuestionAnswerService, QuestionAnswerManager>();
            //services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
