﻿using QuizCreation.Entities.Concrete;
using QuizCreation.Entities.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizCreation.Business.Services.QuestionAnswers
{
    public interface IQuestionAnswerService
    {
        PagedResult<QuestionAnswerViewModel> GetAll(int pageNumber, int pageSize);
        Task<QuestionAnswerViewModel> AddAsync(QuestionAnswerViewModel questionAnswerVM);
        IEnumerable<QuestionAnswerViewModel> GetAllQuestionAnswersByExamId(int examId);
        bool IsExamAttended(int examId, int studentId);
    }
}
