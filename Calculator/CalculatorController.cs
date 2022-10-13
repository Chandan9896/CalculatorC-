using Microsoft.AspNetCore.Mvc;
using System;

namespace CalculatorApp
{
    [Route("api/[controller]")]

    public class CalculatorController : ControllerContext
    {
        CalculatorDataAccessLayer objCalculator = new CalculatorDataAccessLayer();

        [HttpPost]
        [Route("api/Calculator/Create")]
        public void Create([FromBody] CalculatorController calculator)
        {
            objCalculator.Calculator(calculator);
        }
    }

    internal class CalculatorDataAccessLayer
    {
        internal void Calculator(CalculatorController calculator)
        {
            throw new NotImplementedException();
        }
    }
}