﻿using SpeechPathology.Data;
using SpeechPathology.Models;
using SpeechPathology.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SpeechPathology.Services.AgeCalculator
{
    public class AgeCalculatorService : IAgeCalculatorService
    {
        private IRepository<AgeCalculation> _ageCalculation;

        public AgeCalculatorService(IRepository<AgeCalculation> ageCalculation)
        {
            _ageCalculation = ageCalculation;
        }

        public async Task<List<AgeCalculation>> GetAllAsync()
        {
            var ac = await _ageCalculation.GetAllAsync();

            return ac;
        }
    }
}
