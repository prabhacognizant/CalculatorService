using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CalculatorService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        [HttpGet("add")]
        public IActionResult Add(int num1, int num2)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid input"); // Improved validation
            if (num1 == 0) return BadRequest("Num1 is required");
            if (num2 == 0) return BadRequest("Num2 is required");
            try
            {
                checked // Prevents integer overflow
                {
                    return Ok(num1 + num2);
                }
            }
            catch (System.OverflowException)
            {
                return BadRequest("Integer overflow detected");
            }
            catch
            {
                return StatusCode(500, "An error occurred");
            }
        }

        [HttpGet("sub")]
        public IActionResult Sub(int num1, int num2)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid input");
            if (num1 == 0) return BadRequest("Num1 is required and cannot be zero");
            if (num2 == 0) return BadRequest("Num2 is required and cannot be zero");
            try
            {
                checked
                {
                    return Ok(num1 - num2);
                }
            }
            catch (System.OverflowException)
            {
                return BadRequest("Integer overflow detected");
            }
            catch
            {
                return StatusCode(500, "An error occurred");
            }
        }

        [HttpGet("multiply")]
        public IActionResult Multiply(int num1, int num2)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid input");
            if (num1 == 0) return BadRequest("Num1 is required and cannot be zero");
            if (num2 == 0) return BadRequest("Num2 is required and cannot be zero");
            try
            {
                checked
                {
                    long result = (long)num1 * (long)num2;
                    if (result > int.MaxValue || result < int.MinValue)
                        return BadRequest("Result out of range");
                    return Ok(result);
                }
            }
            catch (System.OverflowException)
            {
                return BadRequest("Integer overflow detected");
            }
            catch
            {
                return StatusCode(500, "An error occurred");
            }
        }

        [HttpGet("divide")]
        public IActionResult Divide(int num1, int num2)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid input");
            if (num1 == 0) return BadRequest("Num1 is required and cannot be zero");
            if (num2 == 0) return BadRequest("Num2 is required and cannot be zero");
            if (num2 == 0) return BadRequest("Division by zero is not allowed");
            try
            {
                // Handle integer division by zero is already checked
                checked
                {
                    return Ok(num1 / num2);
                }
            }
            catch (System.DivideByZeroException)
            {
                return BadRequest("Division by zero is not allowed");
            }
            catch (System.OverflowException)
            {
                return BadRequest("Integer overflow detected");
            }
            catch
            {
                return StatusCode(500, "An error occurred");
            }
        }
    }
}
