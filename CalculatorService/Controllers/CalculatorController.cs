using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace CalculatorService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        private readonly ILogger<CalculatorController> _logger;

        public CalculatorController(ILogger<CalculatorController> logger)
        {
            _logger = logger;
        }

        [HttpGet("add")]
        public IActionResult Add(int num1, int num2)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid query parameters");
            if (!IsValidInput(num1) || !IsValidInput(num2))
                return BadRequest("Inputs must be valid non-zero integers between -1000000 and 1000000");
            try
            {
                checked
                {
                    return Ok(num1 + num2);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in Add");
                return StatusCode(500, "An error occurred");
            }
        }

        [HttpGet("sub")]
        public IActionResult Sub(int num1, int num2)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid query parameters");
            if (!IsValidInput(num1) || !IsValidInput(num2))
                return BadRequest("Inputs must be valid non-zero integers between -1000000 and 1000000");
            try
            {
                checked
                {
                    return Ok(num1 - num2);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in Sub");
                return StatusCode(500, "An error occurred");
            }
        }

        [HttpGet("multiply")]
        public IActionResult Multiply(int num1, int num2)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid query parameters");
            if (!IsValidInput(num1) || !IsValidInput(num2))
                return BadRequest("Inputs must be valid non-zero integers between -1000000 and 1000000");
            try
            {
                checked
                {
                    long result = (long)num1 * (long)num2;
                    return Ok(result);
                }
            }
            catch (OverflowException ex)
            {
                _logger.LogError(ex, "Overflow in Multiply");
                return BadRequest("Multiplication resulted in overflow");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in Multiply");
                return StatusCode(500, "An error occurred");
            }
        }

        [HttpGet("divide")]
        public IActionResult Divide(int num1, int num2)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid query parameters");
            if (!IsValidInput(num1))
                return BadRequest("Num1 must be a valid non-zero integer between -1000000 and 1000000");
            if (!IsValidInput(num2))
                return BadRequest("Num2 must be a valid non-zero integer between -1000000 and 1000000");
            if (num2 == 0)
            {
                return BadRequest("Division by zero is not allowed");
            }
            try
            {
                return Ok(num1 / num2);
            }
            catch (DivideByZeroException ex)
            {
                _logger.LogError(ex, "Division by zero");
                return BadRequest("Division by zero error");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in Divide");
                return StatusCode(500, "An error occurred");
            }
        }

        private bool IsValidInput(int value)
        {
            // Acceptable non-zero integer range, adjust as per requirements
            return value >= -1000000 && value <= 1000000 && value != 0;
        }
    }
}
