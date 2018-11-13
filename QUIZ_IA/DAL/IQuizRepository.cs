﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace DAL
{
    public interface IQuizRepository
    {
        List<Question> GetAllQuestions();
        List<Question> Get20Questions();
        int CalculateMaxMark();
    }
}
