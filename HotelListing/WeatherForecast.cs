using System;

namespace HotelListing
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }
    }
}

////Update Staff User - Service
//public async Task<StaffUserDto> UpdateStaffInfo(int id, StaffUserDto staffUser)
//{
//    try
//    {
//        var result = await _db.StaffUsers.Where(x => x.Id == id).FirstOrDefaultAsync();

//        if (result != null)
//        {
//            result.Fullname = staffUser.Fullname;
//            result.FirstName = staffUser.FirstName;
//            result.LastName = staffUser.LastName;
//            result.JobTitle = staffUser.JobTitle;
//            result.DepartmentName = staffUser.DepartmentName;

//            _db.StaffUsers.Update(result);

//            await _db.SaveChangesAsync();

//            var staffUpdatedResult = _mapper.Map<StaffUserDto>(result);

//            return staffUpdatedResult;

//        }

//        throw new Exception("Staff record not found!");

//    }
//    catch (Exception ex)
//    {
//        throw new Exception("Something went wrong!");
//    }

//}


//...............
//Update Staff User - Controller

//[HttpPost("update/staff-user-info/{id:int}")]
//[ProducesResponseType(typeof(ResponseModel), 200)]
//[ProducesResponseType(typeof(ResponseModel), 401)]
//public async Task<IActionResult> UpdateStaffInfo(int id, [FromBody] StaffUserDto staffUserDto)
//{
//    try
//    {

//        var staffUser = await _adminService.UpdateStaffInfo(id, staffUserDto);

//        return Ok(StandardResponse.Ok("Updated successfully", staffUser));

//    }
//    catch (Exception ex)
//    {
//        return BadRequest(StandardResponse.BadRequest("Error occured", ex.Message)); ;
//    }

//}


//.....................................................................................................................
//Fetch All Staff Loan History

//public async Task<IEnumerable<LoanAppHistoryDto>> GetAllStaffLoanHistory(string username)
//{

//    try
//    {
//        var staffLoans = await _db.LoanApplications.Include(x => x.StaffUser)
//            .Where((u => u.StaffUser.Username == username)).ToListAsync();


//        if (staffLoans.Count <= 0)
//        {
//            throw new Exception("Staff loan not found!");
//        }

//        var loanHistoryList = new List<LoanAppHistoryDto>();

//        foreach (var loanHistory in staffLoans)
//        {
//            var loan = new LoanAppHistoryDto
//            {
//                LoanAmount = loanHistory.LoanAmount,
//                LoanRepayment = loanHistory.LoanRepayment,
//                LoanType = loanHistory.LoanType,
//                MaturityDateOfLoan = loanHistory.MaturityDateOfLoan
//            };

//            loanHistoryList.Add(loan);
//        }

//        return loanHistoryList;
//    }
//    catch (Exception e)
//    {
//        throw new Exception("Something went wrong!");
//    }
//}

//............................ Controller
//[Authorize(AuthenticationSchemes = "Bearer")]
//[HttpGet("fetch-staff-loan-history")]
//[ProducesResponseType(typeof(ResponseModel), 200)]
//[ProducesResponseType(StatusCodes.Status401Unauthorized)]
//public async Task<IActionResult> FetchStaffLoanHistory()
//{
//    var identity = HttpContext.User.Identity as ClaimsIdentity;
//    if (identity == null)
//    {
//        throw new UnauthorizedAccessException("User Unauthorized");
//    }
//    var tokenUser = identity.FindFirst("userName").Value;

//    var staffLoanHistory = await _loan.GetAllStaffLoanHistory(tokenUser);
//    if (staffLoanHistory != null)
//        return Ok(StandardResponse.Ok("Successfully fetch loan history", staffLoanHistory));
//    else
//        return BadRequest(StandardResponse.BadRequest("Loan history not found"));
//}



